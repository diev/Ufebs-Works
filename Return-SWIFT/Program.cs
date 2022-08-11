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

using CorrLib.UFEBS;
using CorrLib.UFEBS.DTO;

using System.Text;
using System.Xml;

using static CorrLib.SWIFT.SwiftHelpers;
using static CorrLib.SWIFT.SwiftTranslit;
using static CorrLib.UFEBS.EDHelpers;

namespace ReturnSWIFT;

internal class Program
{
    static readonly Dictionary<string, TransInfo> _d = new();
    static readonly Dictionary<string, TransInfo> _c = new();
    
    static readonly List<string> _o950in = new();
    static readonly List<string> _o950out = new();

    static readonly string? _acc = AppContext.GetData("Acc") as string;
    static readonly string? _bic = AppContext.GetData("BIC") as string;
    static readonly string? _corr = AppContext.GetData("Corr") as string;
    static readonly string? _author = AppContext.GetData("EDAuthor") as string;
    static readonly string? _receiver = AppContext.GetData("EDReceiver") as string;

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
            string mask = Path.GetFileName(path) ?? "4030702000ED503*.txt";

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

        Console.WriteLine();

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

        //if (File.Exists(outFile))
        //{
        //    Console.WriteLine($"Output \"{outFile}\" overwritten");
        //}
        #endregion Files start

        //TODO
        try
        {
            string text = File.ReadAllText(inFile);
            string? mt = ParseMT(text);

            switch (mt)
            {
                case "I103":
                    Process103(inFile, $"{outFile}_.{mt}.ED101.xml", "1");
                    break;

                case "O103":
                    Process103(inFile, $"{outFile}_.{mt}.ED101.xml", "2");
                    break;

                case "O900":
                    Process900(inFile, $"{outFile}.{mt}.ED206.txt");
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
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка в файле \"{inFile}\".\n{ex.Message}");
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
            EDAuthor = _author!,
            EDReceiver = _receiver,
        };

        if (ed.DC == "2")
        {
            ed.EDDate = ed.ChargeOffDate;
            ed.EDNo = NextEDNo();
        }

        ed.PayeeBIC ??= _bic;
        ed.PayeeCorrespAcc ??= _corr;

        TransInfo ti = new(ed, dc)
        {
            EDRefAuthor = _receiver!
        };

        Console.WriteLine($"{ed.SwiftId}  {inFile}");

        var dictionary = dc == "1" ? _d : _c;
        dictionary.Add(ed.SwiftId!, ti);

        PacketEPD packet = new()
        {
            EDAuthor = ed.EDAuthor,
            EDDate = ed.EDDate,
            EDNo = packNo,
            EDQuantity = "1",
            EDReceiver = _receiver,
            Sum = ed.Sum,
            Elements = new ED100[1]
        };

        packet.Elements[0] = ed;

        //outFile = $"_{date}_EPD_30109810200000000654_.xml"; // ALFA

        using var writer = XmlWriter.Create(outFile, _xmlSettings);
        packet.WriteXML(writer);
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
            .AppendLine(Cyr(sc.ToString()))
            .AppendLine(line);

        File.WriteAllText(outFile, sb.ToString(), Encoding.GetEncoding(1251));
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

        Console.WriteLine($"Выписка {inFile}");

        var lines = File.ReadAllLines(inFile);
        int n = 0;
        string line = lines[n++];
        
        string time = ParseTime(line);

        ED211 ed211 = new();
        decimal debetSum = 0;
        decimal creditSum = 0;

        // Счет корреспондентский

        while (!line.StartsWith(":25:")) line = lines[n++];
        ed211.Acc = line[4..];

        // Входящий остаток

        while (!line.StartsWith(":60F:")) line = lines[n++];
        ed211.EnterBal = ParseBal(line[5..]).sum;

        // Подсчитываем число движений

        int startN = n;
        int qty = 0;

        while (!line.StartsWith(":62F:"))
        {
            if (line.StartsWith(":61:"))
            {
                qty++;
            }

            line = lines[n++];
        }

        // Исходящий остаток

        while (!line.StartsWith(":62F:")) line = lines[n++];
        int finishN = n - 1;

        var (date, bal) = ParseBal(line[5..]);
        ed211.AbstractDate = date;
        ed211.OutBal = bal;

        // Движения средств (имея итоги, читаем заново)

        ed211.Elements = new TransInfo[qty];
        qty = 0;

        for (n = startN; n < finishN; n++)
        {
            line = lines[n];

            if (line.StartsWith(":61:"))
            {
                var (dc, id) = ParseTrans(line[4..]);
                bool debet = dc == "1";
                bool found = debet
                    ? _d.TryGetValue(id, out TransInfo? ti) // ourId
                    : _c.TryGetValue(id, out ti); // corrId

                if (found)
                {
                    Console.WriteLine($"  {id}  {dc} {ti!.AccDocNo, 9}  на {ti.Sum, 18}");
                }
                else
                {
                    string accDocNo = lines[n + 1];
                    Console.WriteLine($"  {id}  {dc} {accDocNo, 9}  ?  (строка {n + 1})");
                    // throw new ArgumentException($"Документ не найден.", id);

                    ti = new()
                    {
                        AccDocDate = date,
                        AccDocNo = accDocNo,
                        BICCorr = "0",
                        CorrAcc = "0",
                        DC = dc,
                        EDRefAuthor = _receiver!,
                        EDRefDate = date,
                        EDRefNo = "0",
                        PayeePersonalAcc = debet ? "0" : _acc,
                        PayerPersonalAcc = "0"
                    };
                }

                ed211.Elements[qty++] = ti!;

                decimal kop = decimal.Parse(ti.Sum);

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

        ed211.BIC = _bic!;
        ed211.EDAuthor = _author!;
        ed211.EDDate = date;
        ed211.EDNo = NextEDNo();
        ed211.EndTime = time;
        ed211.EDReceiver = _receiver;

        ed211.DebetSum = debetSum > 0 ? debetSum.ToString() : "0";
        ed211.CreditSum = creditSum > 0 ? creditSum.ToString() : "0";

        using var writer = XmlWriter.Create(outFile + ".ED211.xml", _xmlSettings);
        ed211.WriteXML(writer);
        writer.Close();

        // PacketESID / ED206

        PacketESID packetESID = new()
        {
            EDAuthor = _author!,
            EDDate = date,
            EDNo = NextEDNo()
        };

        using var writer2 = XmlWriter.Create(outFile + ".ED206.xml", _xmlSettings);
        packetESID.WriteXML(writer2, false);

        foreach (var ti in ed211.Elements)
        {
            if (ti.DC == "1")
            {
                ED206 ed206 = new(ti)
                {
                    Acc = _acc,
                    EDAuthor = _author!,
                    EDDate = date,
                    EDNo = NextEDNo(),
                    EDRefAuthor = _receiver!,
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

        sb.Append(Cyr(sc.ToString()));
        sb.AppendLine(line);

        File.WriteAllText(outFile, sb.ToString(), Encoding.GetEncoding(1251));
    }

    private static void ProcessKvit(string inFile, string outFile)
    {
        /*
{1:F21CITVRU2PXXXX0804012157}
{4:{177:2208041611}
{451:0}}
        */

        File.Copy(inFile, outFile, true);
    }
}
