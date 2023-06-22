#region License
/*
Copyright 2022-2023 Dmitrii Evdokimov
Open source software

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

namespace ReturnReplace;

public static class Worker
{
    #region config
    // Our Bank
    private static string OurUIC { get; }

    // Service Bank
    private static string CorUIC { get; }
    private static string CorACC { get; }
    private static string CorACC2 { get; }
    #endregion config

    static Worker()
    {
        try
        {
            OurUIC = AppContext.GetData(nameof(OurUIC)) as string ?? throw new ArgumentNullException(nameof(OurUIC));

            CorUIC = AppContext.GetData(nameof(CorUIC)) as string ?? throw new ArgumentNullException(CorUIC);
            CorACC = AppContext.GetData(nameof(CorACC)) as string ?? throw new ArgumentNullException(CorACC);
            CorACC2 = AppContext.GetData(nameof(CorACC2)) as string ?? throw new ArgumentNullException(CorACC2);
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

        XDocument xdoc = XDocument.Load(inFile);
        XElement? packet = xdoc.Root;

        if (packet == null)
        {
            Console.WriteLine($"Input \"{inFile}\" not XML");
            return;
        }

        switch (packet.Name.LocalName)
        {
            case "PacketEPD":
                PacketEPD(packet, inFile);
                break;

            case "PacketESID":
                PacketESID(packet, inFile);
                break;

            case "ED211":
                ED211(packet);
                break;

            default:
                Console.WriteLine($"Input \"{inFile}\" unknown type \"{packet.Name.LocalName}\"");
                break;
        }

        int tries = 5;

        while (tries-- > 0)
        {
            try
            {
                xdoc.Save(outFile);
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения файла \"{outFile}\".");
                Console.WriteLine(ex.ToString());
                Console.WriteLine($"Еще попыток: {tries} через 2 сек.");

                Thread.Sleep(2000);
            }
        }

        string result = File.Exists(outFile) ? "done." : "FAIL!";

        Console.WriteLine();
        Console.WriteLine($"[Output \"{outFile}\" {result} Press Spacebar.]");
        Console.WriteLine();
    }

    private static void ED211(XElement packet)
    {
        // Файл выписки _150422_ED211_30109810300000000063.xml
        // Для обработки файлов выписки по корсчёту нужно модифицировать следующим образом:
        // 1. В случае отсутствия кредитового оборота по корсчёту (CreditSum= "0" в элементе ED211 – заголовок выписки) атрибут целиком удаляем.
        // 1a. В случае отсутствия дебитового оборота по корсчёту (DebetSum= "0" в элементе ED211 – заголовок выписки) атрибут целиком удаляем.
        // 2. Добавить проверку на наличие атрибута " EDAuthor="4030702000" в каждом элементе TransInfo. Если его нет, то добавить.
        // 3. Установить атрибут EDDate = AbstractDate.

        XNamespace ns = packet.GetDefaultNamespace();
        XAttribute? CreditSum = packet.Attribute(nameof(CreditSum));

        if (CreditSum != null && CreditSum.Value == "0")
        {
            CreditSum.Remove();
        }

        XAttribute? DebetSum = packet.Attribute(nameof(DebetSum));

        if (DebetSum != null && DebetSum.Value == "0")
        {
            DebetSum.Remove();
        }

        XAttribute? EDDate = packet.Attribute(nameof(EDDate));
        XAttribute? AbstractDate = packet.Attribute(nameof(AbstractDate));

        if (EDDate != null && AbstractDate != null && EDDate.Value != AbstractDate.Value)
        {
            EDDate.Value = AbstractDate.Value;
        }

        // 2022-06-06
        // 1. Установить атрибут EndTime = (EndTime – 04:00H) в элементе ED211

        XAttribute? EndTime = packet.Attribute(nameof(EndTime));

        if (EndTime != null)
        {
            var time = DateTime.Parse(EndTime.Value);
            time = time.AddHours(-4.0);

            EndTime.Value = time.ToString("hh:MM:ss");
        }

        XNode? node = packet.FirstNode;

        while (node != null)
        {
            XElement ed = (XElement)node;

            if (ed.Name.LocalName == "TransInfo")
            {
                XElement? EDRefID = ed.Element(ns + nameof(EDRefID));

                if (EDRefID != null && EDRefID.Attribute("EDAuthor") == null)
                {
                    EDRefID.Add(new XAttribute("EDAuthor", OurUIC));
                }
            }

            node = node.NextNode;
        }
    }

    private static void PacketESID(XElement packet, string inFile)
    {
        // Файл Квитовки документов _150422_ESID_30109810300000000063.xml
        // Для обработки файлов файлов квитовки платёжных документов их нужно модифицировать следующим образом:
        // 1. Тэг «PaytKind="0"» удалить
        // 2. К тэгу TransDate="YYYY-MM-DD" добавляем тэг TransTime для получения TransDate="YYYY-MM-DD" TransTime="HH:MI:SS"

        // 2022-06-06
        // 1. Заменять в элементе PacketEPD атрибут EDDate="YYYY-MM-DD" для получения следующего вида
        // <PacketEPD xmlns="urn:cbr-ru:ed:v2.0" EDNo="20" EDDate="2022-04-14" EDAuthor="4525769000" EDQuantity="6" Sum="4502602317" SystemCode="02">
        // в соответствии с датой набора документов, указанной в наименовании файла.

        string TransTime = File.GetLastWriteTime(inFile).ToString("HH:mm:ss");
        string date = GetFileNameDate(inFile);

        XAttribute? EDDate = packet.Attribute(nameof(EDDate));

        if (EDDate != null && EDDate.Value != date)
        {
            EDDate.Value = date;
        }

        XNode? node = packet.FirstNode;

        while (node != null)
        {
            XElement ed = (XElement)node;
            XAttribute? PaytKind = ed.Attribute(nameof(PaytKind));

            PaytKind?.Remove();

            ed.Add(new XAttribute(nameof(TransTime), TransTime));

            node = node.NextNode;
        }
    }

    private static void PacketEPD(XElement packet, string inFile)
    {
        // Файл входящих кредитовых документов _150422_EPD_30109810300000000063.xml
        // Для обработки файлов входящих платёжных документов их нужно модифицировать следующим образом:
        // 1. Поменять значение всех атрибутов EDAuthor="4030702000" на EDAuthor="4525769000"
        // 2. Поменять значение всех атрибутов EDReceiver="4525769000" на EDReceiver="4030702000"
        // 3. Добавить в элементPacketEPD атрибут SystemCode="02 для получения следующего вида
        // <PacketEPD xmlns="urn:cbr-ru:ed:v2.0" EDNo="20" EDDate="2022-04-14" EDAuthor="4525769000" EDQuantity="6" Sum="4502602317" SystemCode="02">
        // 4. Добавить в каждый элемент ED101 за атрибутом Sum="Sum_In_Kopecks" атрибут PaymentPrecedence="79"
        // 5. Добавить в каждый элемент ED101 за атрибутом ChargeOffDate="YYYY-MM-DD" атрибут SystemCode="02"

        // 2022-06-06
        // 1. Заменять значение всех атрибутов PaytKind="4" на PaytKind="0"
        // 2. Заменять в элементе PacketEPD атрибут EDDate="YYYY-MM-DD" для получения следующего вида
        // <PacketEPD xmlns="urn:cbr-ru:ed:v2.0" EDNo="20" EDDate="2022-04-14" EDAuthor="4525769000" EDQuantity="6" Sum="4502602317" SystemCode="02">
        // в соответствии с датой набора документов, указанной в наименовании файла.

        // 2022-06-07
        // 1. Удалять все атрибуты PaytKind вместе с их значениями.
        // 2. Заменять в элементе ED101 в элементе Payee в элементе Bank атрибут CorrespAcc="30101810745250000769" на CorrespAcc="30110810700000000769".

        // 2023-06-22
        // 1. Если получателем указан Банк, а в Назначении можно добыть ИНН и Р/с Клиента, то подставить их в атрибуты Payee.

        XNamespace ns = packet.GetDefaultNamespace();
        XAttribute? EDAuthor = packet.Attribute(nameof(EDAuthor));

        if (EDAuthor != null && EDAuthor.Value == OurUIC)
        {
            EDAuthor.Value = CorUIC;
        }

        XAttribute? EDReceiver = packet.Attribute(nameof(EDReceiver));

        if (EDReceiver != null && EDReceiver.Value == CorUIC)
        {
            EDReceiver.Value = OurUIC;
        }

        string date = GetFileNameDate(inFile);
        XAttribute? EDDate = packet.Attribute(nameof(EDDate));

        if (EDDate != null && EDDate.Value != date)
        {
            EDDate.Value = date;
        }

        packet.Add(new XAttribute("SystemCode", "02"));
        XNode? node = packet.FirstNode;

        while (node != null)
        {
            XElement ed = (XElement)node;
            EDAuthor = ed.Attribute(nameof(EDAuthor));

            if (EDAuthor != null && EDAuthor.Value == OurUIC)
            {
                EDAuthor.Value = CorUIC;
            }

            EDReceiver = ed.Attribute(nameof(EDReceiver));

            if (EDReceiver != null && EDReceiver.Value == CorUIC)
            {
                EDReceiver.Value = OurUIC;
            }

            XAttribute? PaytKind = ed.Attribute(nameof(PaytKind));

            //if (PaytKind != null && PaytKind.Value == "4")
            //{
            //    PaytKind.Value = "0";
            //}

            PaytKind?.Remove();

            ed.Add(new XAttribute("PaymentPrecedence", "79"));
            ed.Add(new XAttribute("SystemCode", "02"));

            XElement? Payee = ed.Element(ns + nameof(Payee));

            if (Payee != null)
            {
                XAttribute? INN = Payee.Attribute(nameof(INN));
                XAttribute? KPP = Payee.Attribute(nameof(KPP));
                XAttribute? PersonalAcc = Payee.Attribute(nameof(PersonalAcc));

                XElement? Bank = Payee.Element(ns + nameof(Bank));

                if (Bank != null)
                {
                    XAttribute? CorrespAcc = Bank.Attribute(nameof(CorrespAcc));

                    if (CorrespAcc != null && CorrespAcc.Value == CorACC)
                    {
                        CorrespAcc.Value = CorACC2;
                    }
                }

                XElement? Purpose = ed.Element(ns + nameof(Purpose));

                if (Purpose != null)
                {
                    //string s = "АО Сити Инвест Банк (Клиент ИП \"Бла Бла\" ИНН 7831001422 Р/С 30109810300000000063)";

                    //var pattern = @"^АО Сити Инвест Банк \((.*) ИНН (\d+) Р\/С (\d+)\)$";
                    //var match = Regex.Match(s, pattern);

                    //Console.WriteLine(match.Groups[1]); //Name
                    //Console.WriteLine(match.Groups[2]); //INN
                    //Console.WriteLine(match.Groups[3]); //PersonalAcc
                    string purpose = Purpose.Value;

                    var inn = RegexHelper.RegexINN().Match(purpose);

                    if (inn.Success)
                    {
                        if (INN != null)
                        {
                            INN.Value = inn.Value;
                            KPP?.Remove();
                        }
                        else
                        {
                            Payee.Add(new XAttribute(nameof(INN), inn.Value));
                        }
                    }

                    var acc = RegexHelper.RegexAcc().Match(purpose);

                    if (acc.Success)
                    {
                        if (PersonalAcc != null)
                        {
                            PersonalAcc.Value = acc.Value;
                        }
                        else
                        {
                            Payee.Add(new XAttribute(nameof(PersonalAcc), acc.Value));
                        }
                    }
                }
            }

            node = node.NextNode;
        }
    }

    static string GetFileNameDate(string fileName)
    {
        var file = new FileInfo(fileName); // _020622_EPD_30109810300000000063.xml

        string name = file.Name;
        string dd = name[1..2]; //.Substring(1, 2);
        string mm = name[3..4]; //.Substring(3, 2);
        string yy = name[5..6]; //.Substring(5, 2);

        return $"20{yy}-{mm}-{dd}";
    }
}

//string s = "АО Сити Инвест Банк (Клиент ИП \"Бла Бла\" ИНН 7831001422 Р/С 30109810300000000063)";

//var pattern = @"^АО Сити Инвест Банк \((.*) ИНН (\d+) Р\/С (\d+)\)$";
//var match = Regex.Match(s, pattern);

//Console.WriteLine(match.Groups[1]); //Name
//Console.WriteLine(match.Groups[2]); //INN
//Console.WriteLine(match.Groups[3]); //PersonalAcc
