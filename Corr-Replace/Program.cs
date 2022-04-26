using Lib;

using System.Text;
using System.Xml.Linq;

namespace Corr_Replace;

public static class Program
{
    #region consts
    // Our Bank
    const string OurINN = "7831001422";
    const string OurKPP = "784101001";
    const string OurACC = "30101810600000000702";
    const string OurNAME = "АО \"Сити Инвест Банк\"";
    const string OurBIC = "044030702";

    //const string Dopo = "АО \"Сити Инвест Банк\" ( ИНН 007831001422 Р/С 30109810300000000063)";

    // Service Bank
    const string CorBIC = "044525769";
    const string CorACC = "30109810300000000063";
    #endregion consts

    public static void Main(string[] args)
    {
        string path, outPath;

        if (args.Length == 0 || args[0] == "/?" || args[0] == "-?") // nothing
        {
            Console.WriteLine("Usage: Input|* [Output_]");
            return;
        }
        else // Input specified
        {
            path = Path.GetFullPath(args[0]);
        }

        if (args.Length == 1) // Output default
        {
            string p = Path.GetDirectoryName(path) ?? Directory.GetCurrentDirectory();
            outPath = Path.EndsInDirectorySeparator(p) ? p : p + Path.DirectorySeparatorChar;
        }
        else if (Directory.Exists(args[1]))
        {
            string p = args[1];
            outPath = Path.EndsInDirectorySeparator(p) ? p : p + Path.DirectorySeparatorChar;
        }
        else
        {
            outPath = args[1];
        }

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); //enable Windows-1251

        if (Directory.Exists(path))
        {
            foreach (string file in Directory.GetFiles(path))
            {
                ProcessFile(file, outPath);
            }
        }
        else if (path.Contains('*') || path.Contains('?'))
        {
            string dir = Path.GetDirectoryName(path) ?? Directory.GetCurrentDirectory();
            string mask = Path.GetFileName(path) ?? "*";

            foreach (string file in Directory.GetFiles(dir, mask))
            {
                ProcessFile(file, outPath);
            }
        }
        else if (File.Exists(path))
        {
            ProcessFile(path, outPath);
        }
        else
        {
            Console.WriteLine($"Input \"{path}\" not found");
        }

        #region finish
        while (true)
        {
            if (Console.ReadKey().Key == ConsoleKey.Spacebar)
            {
                break;
            }
        }
        #endregion finish
    }

    private static void ProcessFile(string inFile, string outFile)
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
