using System.Text;
using System.Xml.Linq;

namespace Return_Replace;

public static class Program
{
    #region consts
    // Our Bank
    const string OurINN = "7831001422";
    const string OurKPP = "784101001";
    const string OurACC = "30101810600000000702";
    const string OurNAME = "АО \"Сити Инвест Банк\"";
    const string OurBIC = "044030702";
    const string OurUIC = "4030702000";

    // Service Bank
    const string CorBIC = "044525769";
    const string CorACC = "30109810300000000063";
    const string CorUIC = "4525769000";
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

        XDocument xdoc = XDocument.Load(inFile);
        XElement? root = xdoc.Root;

        if (root == null)
        {
            Console.WriteLine($"Input \"{inFile}\" not XML");
            return;
        }

        XNamespace ns = root.GetDefaultNamespace();
        XNode? node;
        XElement? e;

        switch (root.Name.LocalName)
        {
            case "PacketEPD":
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

                node = root;
                e = (XElement)node;

                XAttribute? EDAuthor = e.Attribute(nameof(EDAuthor));

                if (EDAuthor != null && EDAuthor.Value == OurUIC)
                {
                    EDAuthor.Value = CorUIC;
                }

                XAttribute? EDReceiver = e.Attribute(nameof(EDReceiver));

                if (EDReceiver != null && EDReceiver.Value == CorUIC)
                {
                    EDReceiver.Value = OurUIC;
                }

                string date = GetFileNameDate(inFile);
                XAttribute? EDDate = e.Attribute(nameof(EDDate));

                if (EDDate != null && EDDate.Value != date)
                {
                    EDDate.Value = date;
                }

                e.Add(new XAttribute("SystemCode", "02"));

                node = root.FirstNode;

                do
                {
                    e = (XElement?)node;
                    EDAuthor = e?.Attribute(nameof(EDAuthor));

                    if (EDAuthor != null && EDAuthor.Value == OurUIC)
                    {
                        EDAuthor.Value = CorUIC;
                    }

                    EDReceiver = e?.Attribute(nameof(EDReceiver));

                    if (EDReceiver != null && EDReceiver.Value == CorUIC)
                    {
                        EDReceiver.Value = OurUIC;
                    }

                    XAttribute? PaytKind = e?.Attribute(nameof(PaytKind));

                    if (PaytKind != null && PaytKind.Value == "4")
                    {
                        PaytKind.Value = "0";
                    }

                    e?.Add(new XAttribute("PaymentPrecedence", "79"));
                    e?.Add(new XAttribute("SystemCode", "02"));

                    node = node?.NextNode;
                }
                while (node != null);

                break;

            case "PacketESID":
                // Файл Квитовки документов _150422_ESID_30109810300000000063.xml
                // Для обработки файлов файлов квитовки платёжных документов их нужно модифицировать следующим образом:
                // 1. Тэг «PaytKind="0"» удалить
                // 2. К тэгу TransDate="YYYY-MM-DD" добавляем тэг TransTime для получения TransDate="YYYY-MM-DD" TransTime="HH:MI:SS"

                node = root.FirstNode;
                string TransTime = File.GetLastWriteTime(inFile).ToString("HH:mm:ss");

                // 2022-06-06
                // 1. Заменять в элементе PacketEPD атрибут EDDate="YYYY-MM-DD" для получения следующего вида
                // <PacketEPD xmlns="urn:cbr-ru:ed:v2.0" EDNo="20" EDDate="2022-04-14" EDAuthor="4525769000" EDQuantity="6" Sum="4502602317" SystemCode="02">
                // в соответствии с датой набора документов, указанной в наименовании файла.

                if (node != null)
                {
                    e = (XElement)node;

                    date = GetFileNameDate(inFile);
                    EDDate = e.Attribute(nameof(EDDate));

                    if (EDDate != null && EDDate.Value != date)
                    {
                        EDDate.Value = date;
                    }
                }

                do
                {
                    e = (XElement?)node;
                    XAttribute? PaytKind = e?.Attribute(nameof(PaytKind));

                    if (PaytKind != null && PaytKind.Value == "0")
                    {
                        PaytKind.Remove();
                    }

                    e?.Add(new XAttribute(nameof(TransTime), TransTime));

                    node = node?.NextNode;
                }
                while (node != null);

                break;

            case "ED211":
                // Файл выписки _150422_ED211_30109810300000000063.xml
                // Для обработки файлов выписки по корсчёту нужно модифицировать следующим образом:
                // 1. В случае отсутствия кредитового оборота по корсчёту (CreditSum= "0" в элементе ED211 – заголовок выписки) атрибут целиком удаляем.
                // 1a. В случае отсутствия дебитового оборота по корсчёту (DebetSum= "0" в элементе ED211 – заголовок выписки) атрибут целиком удаляем.
                // 2. Добавить проверку на наличие атрибута " EDAuthor="4030702000" в каждом элементе TransInfo. Если его нет, то добавить.
                // 3. Установить атрибут EDDate = AbstractDate.

                XAttribute? CreditSum = root.Attribute(nameof(CreditSum));

                if (CreditSum != null && CreditSum.Value == "0")
                {
                    CreditSum.Remove();
                }

                XAttribute? DebetSum = root.Attribute(nameof(DebetSum));

                if (DebetSum != null && DebetSum.Value == "0")
                {
                    DebetSum.Remove();
                }

                EDDate = root.Attribute(nameof(EDDate));
                XAttribute? AbstractDate = root.Attribute(nameof(AbstractDate));

                if (EDDate != null && AbstractDate != null && EDDate.Value != AbstractDate.Value)
                {
                    EDDate.Value = AbstractDate.Value;
                }

                // 2022-06-06
                // 1. Установить атрибут EndTime = (EndTime – 04:00H) в элементе ED211

                XAttribute? EndTime = root.Attribute(nameof(EndTime));

                if (EndTime != null)
                {
                    var time = DateTime.Parse(EndTime.Value);
                    time = time.AddHours(-4.0);

                    EndTime.Value = time.ToString("hh:MM:ss");
                }

                node = root.FirstNode;

                do
                {
                    e = (XElement?)node;
                    if (e?.Name.LocalName == "TransInfo")
                    {
                        XElement? EDRefID = e?.Element(ns + nameof(EDRefID));
                        if (EDRefID != null && EDRefID.Attribute(nameof(EDAuthor)) == null)
                        {
                            EDRefID.Add(new XAttribute(nameof(EDAuthor), OurUIC));
                        }
                    }

                    node = node?.NextNode;
                }
                while (node != null);

                break;

            default:
                Console.WriteLine($"Input \"{inFile}\" unknown type \"{root.Name.LocalName}\"");
                break;
        }
        xdoc.Save(outFile);

        Console.WriteLine();
        Console.WriteLine($"[Output \"{outFile}\" done. Press Spacebar.]");
        Console.WriteLine();
    }

    static string GetFileNameDate(string fileName)
    {
        var file = new FileInfo(fileName); // _020622_EPD_30109810300000000063.xml

        string name = file.Name;
        string dd = name.Substring(1, 2);
        string mm = name.Substring(3, 2);
        string yy = name.Substring(5, 2);
        
        return $"20{yy}-{mm}-{dd}";
    }
}

//string s = "АО Сити Инвест Банк (Клиент ИП \"Бла Бла\" ИНН 7831001422 Р/С 30109810300000000063)";

//var pattern = @"^АО Сити Инвест Банк \((.*) ИНН (\d+) Р\/С (\d+)\)$";
//var match = Regex.Match(s, pattern);

//Console.WriteLine(match.Groups[1]); //Name
//Console.WriteLine(match.Groups[2]); //INN
//Console.WriteLine(match.Groups[3]); //PersonalAcc
