#region License
/*
Copyright 2022 Dmitrii Evdokimov
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

using CorrLib;
using CorrLib.SWIFT;
using CorrLib.UFEBS;
using CorrLib.UFEBS.DTO;

using System.Text;
using System.Xml;
using System.Xml.Linq;

using static CorrLib.SWIFT.SwiftTranslit;
using static CorrLib.UFEBS.EDHelpers;

namespace ReturnSWIFT;

internal class Program
{
    static readonly Dictionary<string, TransInfo> _d = new();
    static readonly Dictionary<string, TransInfo> _c = new();

    static readonly List<string> _o950in = new();
    static readonly List<string> _o950out = new();

    static readonly XmlWriterSettings _xmlSettings = new()
    {
        Encoding = Encoding.GetEncoding(1251),
        Indent = true,
        NamespaceHandling = NamespaceHandling.OmitDuplicates,
        WriteEndDocumentOnClose = true
    };

    static void Main(string[] args)
    {
        string path, outPath;

        if (args.Length == 0 || args[0] == "/?" || args[0] == "-?") // nothing
        {
            Console.WriteLine("Usage: Input|* [Output_.xml]");
            Console.WriteLine("(Use input mask: 4030702000ED503[dd]*.txt)");
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

            if (Path.EndsInDirectorySeparator(outPath))
            {
                Directory.CreateDirectory(outPath);
            }
        }

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); //enable Windows-1251

        Console.WriteLine($"--- Документы --- {path}");

        if (Directory.Exists(path))
        {
            foreach (string file in Directory.GetFiles(path))
            {
                PreprocessFile(file, outPath);
            }

            foreach (string file in Directory.GetFiles(path))
            {
                ProcessFile(file, outPath);
            }
        }
        else if (path.Contains('*') || path.Contains('?'))
        {
            string dir = Path.GetDirectoryName(path) ?? Directory.GetCurrentDirectory();
            string mask = Path.GetFileName(path) ?? "4030702000ED503*.txt";

            if (Directory.Exists(dir))
            {
                foreach (string file in Directory.GetFiles(dir, "*"))
                {
                    PreprocessFile(file, outPath);
                }

                foreach (string file in Directory.GetFiles(dir, mask))
                {
                    ProcessFile(file, outPath);
                }
            }
            else
            {
                Console.WriteLine($"Input dir \"{dir}\" not found");
            }
        }
        else if (File.Exists(path))
        {
            PreprocessFile(path, outPath);
            ProcessFile(path, outPath);
        }
        else
        {
            Console.WriteLine($"Input file \"{path}\" not found");
        }

        for (int i = 0; i < _o950in.Count; i++)
        {
            string inFile = _o950in[i];
            string outFile = _o950out[i];

            try
            {
                Process950(inFile, outFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка в файле выписки \"{inFile}\". {ex.Message}");
            }
        }

        #region finish
        Console.WriteLine("\nJob done. Press Spacebar.");

        while (true)
        {
            if (Console.ReadKey().Key == ConsoleKey.Spacebar)
            {
                break;
            }
        }
        #endregion finish
    }

    private static void PreprocessFile(string inFile, string outFile)
    {
        #region Files start
        if (Path.EndsInDirectorySeparator(outFile)) // Directory.Exists(outFile)
        {
            var file = new FileInfo(inFile);
            int len = file.Extension.Length;
            //outFile = Path.Combine(outFile, file.Name[..^len] + "_" + file.Extension);
            outFile = Path.Combine(outFile, file.Name[..^len]);
        }
        #endregion Files start

        string ext = Path.GetExtension(inFile);
        string ext2 = ".~" + ext[1..];

        if (ext.StartsWith(".~"))
        {
            return;
        }

        else if (ext.Equals(".txt", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        else if (ext.Equals(".SF", StringComparison.OrdinalIgnoreCase))
        {
            string inFile2 = inFile + ".ED574.xml";
            SenObject.Extract(inFile, inFile2);
            PreprocessFile(inFile2, outFile + ".xml");
        }

        else if (ext.Equals(".SFD", StringComparison.OrdinalIgnoreCase))
        {
            string inFile2 = inFile + ".ED503";
            SenObject.Extract(inFile, inFile2);
            PreprocessFile(inFile2, outFile);
        }

        else if (ext.Equals(".ED503", StringComparison.OrdinalIgnoreCase))
        {
            string inFile2 = inFile + ".txt";
            SenObject.Extract(inFile, inFile2, "SWIFTDocument");
        }

        //else if (ext.Equals(".ED574", StringComparison.OrdinalIgnoreCase))
        //{
        //    string inFile2 = inFile + ".xml";
        //    SenObject.Extract(inFile, inFile2, "SWIFTDocument");
        //}

        else if (ext.Equals(".xml", StringComparison.OrdinalIgnoreCase))
        {
            var xdoc = XDocument.Load(inFile);
            var root = xdoc.Root;

            if (root is null)
            {
                return;
            }

            string name = root.Name.LocalName;
            string date = root.Attribute("EDDate")!.Value;
            string inFile2 = MakePath(outFile, date, ".xml");

            if (name == "ED574")
            {
                File.Copy(inFile, inFile2, true);
            }
            else
            {
                File.Copy(inFile, inFile2, true);

                inFile2 = inFile + ".txt";
                ED100 ed = new(inFile);
                string mt103 = ed.ToStringMT103(
                    CorrBank.SWIFT!, CorrBank.ProfileSWIFT!, CorrBank.ProfilePayAcc!);

                File.WriteAllText(inFile2, mt103, Encoding.ASCII);
            }
        }

        File.Move(inFile, Path.ChangeExtension(inFile, ext2));
    }

    private static void ProcessFile(string inFile, string outFile)
    {
        #region Files start
        if (Path.EndsInDirectorySeparator(outFile)) // Directory.Exists(outFile)
        {
            var file = new FileInfo(inFile);
            int len = file.Extension.Length;
            //outFile = Path.Combine(outFile, file.Name[..^len] + "_" + file.Extension);
            outFile = Path.Combine(outFile, file.Name[..^len]);
        }

        if (Path.GetExtension(inFile).StartsWith(".~"))
        {
            return;
        }

        //if (File.Exists(outFile))
        //{
        //    Console.WriteLine($"Output \"{outFile}\" overwritten");
        //}

        string p = Path.GetDirectoryName(inFile) ?? Directory.GetCurrentDirectory();
        string f = Path.GetFileName(inFile);

        string bakPath = Path.Combine(p, "BAK");
        string errPath = Path.Combine(p, "ERR");

        string bakFile = Path.Combine(bakPath, f);
        string errFile = Path.Combine(errPath, f);
        #endregion Files start

        //TODO
        try
        {
            string text = File.ReadAllText(inFile);
            string? mt = text.ParseMT();

            switch (mt)
            {
                case "I103":
                    Process103(inFile, $"{outFile}_.{mt}.ED101.xml", "1");
                    break;

                case "O103":
                    Process103(inFile, $"{outFile}_.{mt}.ED101.xml", "2");
                    break;

                case "O900":
                    Process900(inFile, $"{outFile}_.{mt}.ED206.txt");
                    break;

                case "O950":
                    _o950in.Add(inFile);
                    _o950out.Add($"{outFile}_.{mt}");
                    break;

                case "O196":
                case "O199":
                case "O299":
                    Process199(inFile, $"{outFile}_.{mt}.txt");
                    break;

                default:
                    if (mt != null)
                    {
                        Console.WriteLine($"Unknown {mt} in \"{inFile}\"!");
                    }
                    else
                    {
                        ProcessKvit(inFile, $"{outFile}_.kvit.txt");
                    }
                    break;
            }

            if (!Directory.Exists(bakPath))
            {
                Directory.CreateDirectory(bakPath);
            }

            //File.Move(inFile, bakFile, true);
            File.Copy(inFile, bakFile, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка в файле \"{inFile}\".\n{ex.Message}");

            if (!Directory.Exists(errPath))
            {
                Directory.CreateDirectory(errPath);
            }

            File.Move(inFile, errFile, true);
        }

        #region Files end
        //Console.WriteLine();

        //if (File.Exists(outFile))
        //{
        //    Console.WriteLine($"[Output \"{outFile}\" done. Press Spacebar.]");
        //}
        //else
        //{
        //    Console.WriteLine($"[Output \"{outFile}\" FAIL! Press Spacebar.]"); //TODO add if .O900.ED206
        //}

        //Console.WriteLine();
        #endregion Files end
    }

    private static void Process103(string inFile, string outFile, string dc)
    {
        string packNo = NextEDNo();

        var lines = File.ReadAllLines(inFile);
        ED100 ed = new(lines)
        {
            EDAuthor = CorrBank.ProfileUIC!,
            EDReceiver = CorrBank.UIC,
        };

        if (ed.DC == "2")
        {
            ed.EDDate = ed.ChargeOffDate;
            ed.EDNo = NextEDNo();
        }

        ed.PayeeBIC ??= CorrBank.BIC;
        ed.PayeeCorrespAcc ??= CorrBank.CorrAcc;

        TransInfo ti = new(ed, dc)
        {
            EDRefAuthor = CorrBank.UIC!
        };

        var dictionary = dc == "1" ? _d : _c;
        string id = ed.SwiftId!;

        if (dictionary.ContainsKey(id))
        {
            Console.WriteLine($"{id} !{ed.EDDate} {Path.GetFileName(inFile)} (double)");
            return;
        }

        Console.WriteLine($"{id}  {ed.EDDate} {Path.GetFileName(inFile)}");

        dictionary.Add(id, ti);

        if (ed.SystemCode == "02") // ELEK
        {
            PacketEPD packet = new()
            {
                EDAuthor = ed.EDAuthor,
                EDDate = ed.EDDate,
                EDNo = packNo,
                EDQuantity = "1",
                EDReceiver = CorrBank.UIC,
                Sum = ed.Sum,
                Elements = new ED100[1]
            };

            packet.Elements[0] = ed;

            using var writer = XmlWriter.Create(MakePath(outFile, packet.EDDate), _xmlSettings);
            packet.WriteXML(writer);
        }
        else // BESP
        {
            using var writer = XmlWriter.Create(MakePath(outFile, ed.EDDate), _xmlSettings);
            ed.WriteXML(writer);
        }
    }

    private static void Process900(string inFile, string outFile)
    {
        /*
{1:F01CITVRU2PXXXX0080000001}{2:O9000135220805ALFARUMMXXXX00804219152208050135N}{3:{113:RUR6}{108:1OP1EE0033080923}}{4:
:20:+P1ED2284001JMZC
:21:+220804000012157
:25:30109810200000000654
:32A:220804RUB10000,
:72:/NZP/OPLATA ZA REPOZITARNYE USLUGI
//ZA IuLX 2022G. PO DOG. n 2016-6 O
//T 18.10.2016 ScET n 430 OT 31.07.
//2022G. BEZ NDS.
-}{5:{MAC:00000000}{CHK:00002A77E432}}
        */

        var lines = File.ReadAllLines(inFile);
        int n = 0;
        string line = lines[n++];
        StringBuilder sb = new();

        while (!line.StartsWith(":21:"))
        {
            sb.AppendLine(line);
            line = lines[n++];
        }

        var (date, _) = SwiftID.Id(line[4..]);

        while (!line.StartsWith(":72:/NZP/"))
        {
            sb.AppendLine(line);
            line = lines[n++];
        }

        StringBuilder sc = new(line[9..]);
        line = lines[n++];

        while (line.StartsWith("//"))
        {
            sc.Append(line[2..]);
            line = lines[n++];
        }

        sb.Append(":72:")
            .AppendLine(sc.Cyr())
            .AppendLine(line);

        File.WriteAllText(MakePath(outFile, date), sb.ToString(), Encoding.GetEncoding(1251));
    }

    private static void Process950(string inFile, string outFile)
    {
        /*
{1:F01CITVRU2PXXXX0080000002}{2:O9500136220805ALFARUMMXXXX00804219572208050136N}{3:{113:RUR6}{108:1OP1EE0033081056}}{4:
:20:+408220000240174
:25:30109810200000000654
:28C:40174/1
:60F:C220803RUB500,
:61:220804D10000,S103+220804000012157//OP1ED2284001JMZC
3258
:61:220804C10000,NTRFNONREF//+OP1ED284001FR8F
3261
:62F:C220804RUB500,
:64:C220804RUB500,
-}{5:{MAC:00000000}{CHK:000081FE3115}}
         */

        var lines = File.ReadAllLines(inFile);
        int n = 0;
        string line = lines[n++];
        
        string time = line.UParseDateTime().time;

        ED211 ed211 = new();
        long debetSum = 0;
        long creditSum = 0;

        // Счет корреспондентский

        while (!line.StartsWith(":25:")) line = lines[n++];
        ed211.Acc = line[4..];

        // Входящий остаток

        while (!line.StartsWith(":60F:")) line = lines[n++];
        ed211.EnterBal = line[5..].UParseBal().sum;

        // Подсчитываем число движений

        int startN = n;
        int qty = 0;

        while (!line.StartsWith(":62F:"))
        {
            if (line.StartsWith(":61:")) qty++;

            line = lines[n++];
        }

        // Исходящий остаток

        //while (!line.StartsWith(":62F:")) line = lines[n++];
        int finishN = n - 1;

        var (date, bal) = line[5..].UParseBal();
        ed211.AbstractDate = date;
        ed211.OutBal = bal;

        Console.WriteLine($"\n---- Выписка ---- {date} {Path.GetFileName(inFile)}");

        // Движения средств (имея итоги, читаем заново)

        ed211.Elements = new TransInfo[qty];
        qty = 0;

        for (n = startN; n < finishN; n++)
        {
            line = lines[n];

            if (line.StartsWith(":61:"))
            {
                var (dc, sum, id) = line[4..].ParseTrans();
                bool debet = dc == "1";
                bool order = id == "NONREF"; // Банковский ордер (как вариант)
                bool found = debet
                    ? _d.TryGetValue(id, out TransInfo? ti) // ourId
                    : _c.TryGetValue(id, out ti); // corrId

                if (found && !order)
                {
                    Console.WriteLine($"{id}  {dc}{ti!.AccDocNo, 9} {ti.Sum.DisplaySum(), 18}");
                }
                else
                {
                    string accDocNo = lines[n + 1];
                    Console.WriteLine($"{id,16} !{dc}{accDocNo, 9} {sum.DisplaySum(),18} ? (строка {n + 1})");
                    // throw new ArgumentException($"Документ не найден.", id);

                    ti = new()
                    {
                        AccDocDate = date,
                        AccDocNo = accDocNo,
                        BICCorr = CorrBank.ProfileBIC,
                        CorrAcc = CorrBank.ProfileCorrAcc,
                        DC = dc,
                        EDRefAuthor = CorrBank.UIC!,
                        EDRefDate = date,
                        EDRefNo = accDocNo,
                        PayeePersonalAcc = debet ? "0" : CorrBank.ProfilePayAcc,
                        PayerPersonalAcc = debet ? CorrBank.ProfilePayAcc : "0",
                        Sum = sum,
                        TransKind = order ? "17" : "01"
                    };
                }

                ed211.Elements[qty++] = ti!;

                long kop = long.Parse(ti.Sum);

                if (debet)
                {
                    debetSum += kop;
                }
                else
                {
                    creditSum += kop;
                }
            }
        }

        // ED211 /TransInfo

        ed211.BIC = CorrBank.BIC!;
        ed211.EDAuthor = CorrBank.ProfileUIC!;
        ed211.EDDate = date;
        ed211.EDNo = NextEDNo();
        ed211.EndTime = time;
        ed211.EDReceiver = CorrBank.UIC;

        ed211.DebetSum = debetSum > 0 ? debetSum.ToString() : "0";
        ed211.CreditSum = creditSum > 0 ? creditSum.ToString() : "0";

        using var writer = XmlWriter.Create(MakePath(outFile, ed211.AbstractDate, ".ED211.xml"), _xmlSettings);
        ed211.WriteXML(writer);
        writer.Close();

        // PacketESID / ED206

        PacketESID packetESID = new()
        {
            EDAuthor = CorrBank.ProfileUIC!,
            EDDate = date,
            EDNo = NextEDNo()
        };

        using var writer2 = XmlWriter.Create(MakePath(outFile, ed211.AbstractDate, ".ED206.xml"), _xmlSettings);
        packetESID.WriteXML(writer2, false);

        foreach (var ti in ed211.Elements)
        {
            if (ti.DC == "1")
            {
                ED206 ed206 = new(ti)
                {
                    Acc = ed211.Acc, //CorrBank.OurAcc,
                    EDAuthor = CorrBank.ProfileUIC!,
                    EDDate = date,
                    EDNo = NextEDNo(),
                    EDRefAuthor = CorrBank.UIC!,
                    TransDate = date,
                    TransTime = time
                };
                ed206.WriteXML(writer2);
            }
        }

        writer2.Close();
    }

    private static void Process199(string inFile, string outFile)
    {
        var lines = File.ReadAllLines(inFile);
        int n = 0;
        string line = lines[n++];
        StringBuilder sb = new();

        while (!line.StartsWith(":76:") && !line.StartsWith(":79:"))
        {
            sb.AppendLine(line);
            line = lines[n++];
        }

        sb.Append(line[..4]);

        StringBuilder sc = new();
        sc.AppendLine(line[4..]);
        line = lines[n++];

        while (!line.StartsWith("-}"))
        {
            sc.AppendLine(line);
            line = lines[n++];
        }

        sb.Append(sc.Cyr());
        sb.AppendLine(line);

        File.WriteAllText(outFile, sb.ToString(), Encoding.GetEncoding(1251)); //TODO
    }

    private static void ProcessKvit(string inFile, string outFile)
    {
        /*
{1:F21CITVRU2PXXXX0804012157}
{4:{177:2208041611}
{451:0}}
        */

        File.Copy(inFile, outFile, true); //TODO
    }

    private static string MakePath(string path, string date, string ext = "")
    {
        string dir = Path.Combine(Path.GetDirectoryName(path)!, date);

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        string file = ext == ""
            ? Path.Combine(dir, Path.GetFileName(path))
            : Path.Combine(dir, Path.GetFileNameWithoutExtension(path) + ext);

        return file;
    }
}
