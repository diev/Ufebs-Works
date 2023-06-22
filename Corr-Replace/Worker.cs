#region License
/*
Copyright 2022-2023 Dmitrii Evdokimov

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
#endregion

using System.Xml.Linq;

using Lib;

namespace CorrReplace;

public static class Worker
{
    #region config
    // Our Bank
    private static string OurINN { get; }
    private static string OurKPP { get; }
    private static string OurACC { get; }
    private static string OurNAME { get; }
    private static string OurBIC { get; }

    // Service Bank
    private static string CorBIC { get; }
    private static string CorACC { get; }
    #endregion config

    static Worker()
    {
        try
        {
            OurINN = AppContext.GetData(nameof(OurINN)) as string ?? throw new ArgumentNullException(nameof(OurINN));
            OurKPP = AppContext.GetData(nameof(OurKPP)) as string ?? throw new ArgumentNullException(nameof(OurKPP));
            OurACC = AppContext.GetData(nameof(OurACC)) as string ?? throw new ArgumentNullException(nameof(OurACC));
            OurNAME = AppContext.GetData(nameof(OurNAME)) as string ?? throw new ArgumentNullException(nameof(OurNAME));
            OurBIC = AppContext.GetData(nameof(OurBIC)) as string ?? throw new ArgumentNullException(nameof(OurBIC));

            CorBIC = AppContext.GetData(nameof(CorBIC)) as string ?? throw new ArgumentNullException(CorBIC);
            CorACC = AppContext.GetData(nameof(CorACC)) as string ?? throw new ArgumentNullException(CorACC);
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"В конфиге не указан параметр \"{ex.ParamName}\"!");
            Environment.Exit(2);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Environment.Exit(2);
        }
    }

    public static void ProcessFile(string inFile, string outFile)
    {
        if (Path.EndsInDirectorySeparator(outFile)) // Directory.Exists(outFile)
        {
            var file = new FileInfo(inFile);
            int len = file.Extension.Length;
            outFile = Path.Combine(outFile, file.Name[..^len] + "_" + file.Extension);
        }

        if (File.Exists(outFile))
        {
            Console.WriteLine($"Output \"{outFile}\" overwritten");
        }

        var xdoc = XDocument.Load(inFile);
        var root = xdoc.Root;

        if (root == null)
        {
            return;
        }

        XNamespace ns = root.GetDefaultNamespace();
        XNode? node;
        XElement ED;

        int EDQuantity = -1;
        long PacketSum = 0;

        if (root.Name.LocalName == "PacketEPD")
        {
            node = root.FirstNode;

            if (node == null)
            {
                return;
            }

            ED = (XElement)node;
            EDQuantity = int.Parse(ED.Attribute(nameof(EDQuantity))?.Value ?? "0");
            PacketSum = long.Parse(ED.Attribute("Sum")?.Value ?? "0");
        }
        else
        {
            node = root;
        }

        int total = 0;
        long sum = 0;

        do
        {
            #region Get Values
            // <ED101 ... EDDate="2022-04-13" EDNo="10442" Sum="164839" TransKind="01" xmlns="urn:cbr-ru:ed:v2.0">
            ED = (XElement)node;
            XAttribute? EDNo = ED.Attribute(nameof(EDNo));
            XAttribute? Sum = ED.Attribute(nameof(Sum));
            XAttribute? TransKind = ED.Attribute(nameof(TransKind));

            // <AccDoc AccDocDate="2022-04-12" AccDocNo="1517"/>
            XElement? AccDoc = ED.Element(ns + nameof(AccDoc));
            XAttribute? AccDocNo = AccDoc?.Attribute(nameof(AccDocNo));

            // <Payer INN="470608634408" KPP="470301001" PersonalAcc="30109810300000000063">
            XElement? Payer = ED.Element(ns + nameof(Payer));
            XAttribute? INN = Payer?.Attribute(nameof(INN));
            XAttribute? KPP = Payer?.Attribute(nameof(KPP));
            XAttribute? PersonalAcc = Payer?.Attribute(nameof(PersonalAcc));

            // <Name>Ван ден Бринк Виллеке</Name>
            XElement? Name = Payer?.Element(ns + nameof(Name));

            // <Bank BIC="044525769" CorrespAcc="30109810300000000063"/>
            XElement? Bank = Payer?.Element(ns + nameof(Bank));

            XAttribute? BIC = Bank?.Attribute(nameof(BIC));
            XAttribute? CorrespAcc = Bank?.Attribute(nameof(CorrespAcc));

            // <Payee INN="470608634408" KPP="470301001" PersonalAcc="30109810300000000063">
            XElement? Payee = ED.Element(ns + nameof(Payee));
            XAttribute? PayeeINN = Payee?.Attribute(nameof(INN));
            XAttribute? PayeeKPP = Payee?.Attribute(nameof(KPP));
            //XAttribute? PayeePersonalAcc = Payee?.Attribute(nameof(PersonalAcc));

            // <Purpose>Взносы по страхованию...</Purpose>
            XElement? Purpose = ED.Element(ns + nameof(Purpose));

            // <DepartmentalInfo CBC="39310202050071000160" DocDate="0" DocNo="0" DrawerStatus="13" OKATO="41625156" PaytReason="0" TaxPaytKind="0" TaxPeriod="0"/>
            XElement? DepartmentalInfo = ED.Element(ns + nameof(DepartmentalInfo));
            XAttribute? TaxPaytKind = DepartmentalInfo?.Attribute(nameof(TaxPaytKind));
            #endregion Get Values

            #region Calc Values
            string title = $"#{EDNo?.Value} (N{AccDocNo?.Value}, ${Sum?.Value})";

            string name = Name?.Value ?? string.Empty;
            string textName = OurNAME;
            bool nameChanged = false;

            string? textPurpose = Purpose?.Value;
            bool purposeChanged = false;

            if (INN != null && INN.Value != OurINN) // Плательщик не наш Банк
            {
                name = name
                    .Replace("Общество с ограниченной ответственностью", "ООО", StringComparison.OrdinalIgnoreCase)
                    .Replace("Акционерное общество", "АО", StringComparison.OrdinalIgnoreCase)
                    .Replace("Индивидуальный предприниматель", "ИП", StringComparison.OrdinalIgnoreCase);

                textName += $" ИНН {OurINN} ({name} Р/С {PersonalAcc?.Value})";
                textName = EditText(title, textName, 160);

                nameChanged = true;

                if (DepartmentalInfo != null) // Платеж "налоговый" - за третье лицо
                {
                    textPurpose = $"{OurINN}//{OurKPP}//{name}//{textPurpose}"
                        .Replace("////", "//");
                    textPurpose = EditText(title, textPurpose, 210);

                    purposeChanged = true;
                }
            }
            #endregion Calc Values

            #region Set Values
            if (ED.Name.LocalName != "ED101")
            {
                ED.Name = ns + "ED101";
            }

            if (TransKind != null && TransKind.Value != "01")
            {
                TransKind.Value = "01";
            }

            if (KPP != null && INN != null && INN.Value.Length == 12) // физлицо или ИП
            {
                KPP.Remove();
            }

            if (PayeeKPP != null && PayeeINN != null && PayeeINN.Value.Length == 12) // физлицо или ИП
            {
                PayeeKPP.Remove();
            }

            if (PersonalAcc != null)
            {
                PersonalAcc.Value = CorACC;
            }

            if (Name != null && nameChanged)
            {
                Name.Value = textName;
            }

            if (BIC != null)
            {
                BIC.Value = CorBIC;
            }

            if (CorrespAcc != null)
            {
                CorrespAcc.Value = CorACC;
            }

            if (Purpose != null && purposeChanged)
            {
                Purpose.Value = textPurpose ?? String.Empty;
            }

            if (TaxPaytKind != null && TaxPaytKind.Value == "0")
            {
                TaxPaytKind.Remove();
            }
            #endregion Set Values

            total++;
            sum += long.Parse(Sum?.Value ?? "0");

            node = node.NextNode;
        }
        while (node != null);

        xdoc.Save(outFile);

        Console.WriteLine();
        if (EDQuantity > 0 && (total != EDQuantity || sum != PacketSum))
        {
            Console.WriteLine("[Wrong total number of ED or Sum!]");
        }
        Console.WriteLine($"[\"{outFile}\" done {total} (${sum}). Press Spacebar.]");
        Console.WriteLine();
    }

    private static string EditText(string title, string text, int maxLength)
    {
        while (text.Length > maxLength)
        {
            int len = text.Length - maxLength;
            Console.WriteLine();
            Console.WriteLine($"{title} [-{len}]: {text}");

            string label = $"Надо сократить до {maxLength}";
            string result = text;

            if (InputBox.Query(title, label, ref result))
            {
                text = result;
            }
        }

        return text;
    }
}
