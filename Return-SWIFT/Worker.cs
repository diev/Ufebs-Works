#region License
/*
Copyright 2022-2025 Dmitrii Evdokimov
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

using System.Text;
using System.Xml;
using System.Xml.Linq;

using CorrLib;
using CorrLib.SWIFT;
using CorrLib.UFEBS;
using CorrLib.UFEBS.DTO;

namespace ReturnSWIFT;

public static class Worker
{
    //static readonly Dictionary<string, TransInfo> _d = [];
    //static readonly Dictionary<string, TransInfo> _c = [];

    static readonly Encoding _encoding = Encoding.GetEncoding(1251);

    static readonly XmlWriterSettings _xmlSettings = new()
    {
        Encoding = _encoding,
        Indent = true,
        NamespaceHandling = NamespaceHandling.OmitDuplicates,
        WriteEndDocumentOnClose = true
    };

    public static void PreprocessFile(string inFile, string outFile)
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
            string inFile2 = Repository.GetInStoreFile(outFile, date, ".xml");

            File.Copy(inFile, inFile2, true);

            if (!name.Equals("ED574"))
            {
                inFile2 = inFile + ".txt";
                ED100 ed = new(inFile);
                string mt103 = ed.ToStringMT103(
                    CorrBank.SWIFT!,
                    CorrBank.ProfileSWIFT!,
                    CorrBank.ProfilePayAcc!);

                File.WriteAllText(inFile2, mt103, Encoding.ASCII);
            }
        }

        File.Move(inFile, Path.ChangeExtension(inFile, ext2));
    }

    public static void ProcessFile(string inFile, string outFile)
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
                case "I103": // ED101 Платежное поручение
                    string path = outFile + $"_.{mt}.ED101.xml";
                    Process103(inFile, path, "1");
                    break;

                case "O103": // ED103 Платежное требование
                    path = outFile + $"_.{mt}.ED101.xml";
                    Process103(inFile, path, "2");
                    break;

                case "O900": // ED206 Подтверждение дебета/кредита
                    path = outFile + $"_.{mt}.ED206.txt";
                    Process900AsText(inFile, path);
                    Program.O900in.Add(inFile);
                    path = outFile + $"_.{mt}";
                    Program.O900out.Add(path);
                    break;

                case "O950": // ED211 Извещение об операциях по счету
                    Program.O950in.Add(inFile);
                    path = outFile + $"_.{mt}";
                    Program.O950out.Add(path);
                    break;

                case "O196":
                case "O199":
                case "O299":
                    path = outFile + $"_.{mt}.txt";
                    Process199AsText(inFile, path);
                    break;

                default:
                    if (mt != null)
                    {
                        Console.WriteLine(@$"Unknown {mt} in ""{inFile}""!");
                    }
                    else
                    {
                        path = outFile + "_.txt";
                        ProcessKvit(inFile, path);
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
            Console.WriteLine(@$"Ошибка в файле ""{inFile}"".{Environment.NewLine}{ex.Message}");

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
        string packNo = EDHelpers.NextEDNo();

        var lines = File.ReadAllLines(inFile);
        ED100 ed = new(lines)
        {
            EDAuthor = CorrBank.ProfileUIC!,
            EDReceiver = CorrBank.UIC,
        };

        if (ed.DC == "2")
        {
            ed.EDDate = ed.ChargeOffDate!;
            ed.EDNo = EDHelpers.NextEDNo();
        }

        ed.PayeeBIC ??= CorrBank.BIC;
        ed.PayeeCorrespAcc ??= CorrBank.CorrAcc;

        TransInfo ti = new(ed, dc)
        {
            EDRefAuthor = CorrBank.UIC!
        };

        var dictionary = dc == "1"
            ? TransInfoEx.D
            : TransInfoEx.C;

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
                EDDate = ed.EDDate!,
                EDNo = packNo,
                EDQuantity = "1",
                EDReceiver = CorrBank.UIC,
                Sum = ed.Sum,
                Elements = new ED100[1]
            };

            packet.Elements[0] = ed;

            string path = dc == "1"
                ? Repository.GetOutStoreFile(outFile, packet.EDDate)
                : Repository.GetInStoreFile(outFile, packet.EDDate);

            using var writer = XmlWriter.Create(path, _xmlSettings);
            packet.WriteXML(writer);
        }
        else // BESP
        {
            string path = dc == "1"
                ? Repository.GetOutStoreFile(outFile, ed.EDDate)
                : Repository.GetInStoreFile(outFile, ed.EDDate);

            using var writer = XmlWriter.Create(path, _xmlSettings);
            ed.WriteXML(writer);
        }
    }

    public static void Process900AsText(string inFile, string outFile)
    {
        var lines = File.ReadAllLines(inFile);
        var (date, text) = SwiftMT900.ToString(lines);
        string path = Repository.GetInStoreFile(outFile, date);

        File.WriteAllText(path, text, _encoding);
    }

    public static void Process900(string inFile, string outFile)
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

        // Text dump

        var lines = File.ReadAllLines(inFile);
        //int n = 0;
        //string line = lines[n++];
        //var (date, time) = line.UParseDateTime(); // Плохое время - формирования файла SWIFT MT900

        // PacketESID / ED206

        ED206 ed206 = new(lines)
        {
            BICCorr = CorrBank.ProfileBIC!,
            CorrAcc = CorrBank.ProfileCorrAcc!,
            EDAuthor = CorrBank.ProfileUIC!,
        };

        string date = ed206.TransDate!;

        //TODO string path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(inFile), @$"..\OUT\{date}"));
        string id = ed206.SwiftRefId!;
        string? ufebs = Repository.GetOutUfebsFileBySwiftId(id);

        if (File.Exists(ufebs))
        {
            ED100 xml = new(ufebs);

            ed206.BICCorr = xml.PayeeBIC ?? string.Empty;
            ed206.CorrAcc = xml.PayeeCorrespAcc ?? string.Empty;

            ed206.AccDocDate = xml.AccDocDate;
            ed206.AccDocNo = xml.AccDocNo;

            ed206.EDRefAuthor = xml.EDAuthor;
            ed206.EDRefDate = xml.EDDate;
            ed206.EDRefNo = xml.EDNo;
        }

        PacketESID packetESID = new()
        {
            EDAuthor = CorrBank.ProfileUIC!,
            EDDate = date,
            EDNo = EDHelpers.NextTimedEDNo()
        };

        string path = Repository.GetInStoreFile(outFile, date, ".ED206.xml");
        using var writer = XmlWriter.Create(path, _xmlSettings);
        packetESID.WriteXML(writer, false);
        ed206.EDNo = EDHelpers.NextTimedEDNo();
        ed206.WriteXML(writer);
        writer.Close();
    }

    public static void Process950(string inFile, string outFile)
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
        ED211 ed211 = new(lines);
        string date = ed211.AbstractDate; //TODO period, not a day only

        foreach (var ti in ed211.Elements)
        {
            if (ti.DC == "1") //Debet (OUR)
            {
                string id = ti.SwiftRefId!;
                string? ufebs = Repository.GetOutUfebsFileBySwiftId(id);

                if (File.Exists(ufebs))
                {
                    ED100 xml = new(ufebs);

                    ti.BICCorr = xml.PayeeBIC ?? string.Empty;
                    ti.CorrAcc = xml.PayeeCorrespAcc ?? string.Empty;
                    ti.PayeePersonalAcc = xml.PayeePersonalAcc ?? string.Empty;

                    ti.AccDocDate = xml.AccDocDate;
                    ti.AccDocNo = xml.AccDocNo;

                    ti.EDRefAuthor = xml.EDAuthor;
                    ti.EDRefDate = xml.EDDate;
                    ti.EDRefNo = xml.EDNo;
                }
            }
        }

        string path = Repository.GetInStoreFile(outFile, date, ".ED211.xml");
        using var writer = XmlWriter.Create(path, _xmlSettings);
        ed211.WriteXML(writer);
        writer.Close();

        // PacketESID / ED206

        PacketESID packetESID = new()
        {
            EDAuthor = CorrBank.ProfileUIC!,
            EDDate = date,
            EDNo = EDHelpers.NextEDNo()
        };

        path = Repository.GetInStoreFile(outFile, date, ".ED206.xml"); //TODO required?
        using var writer2 = XmlWriter.Create(path, _xmlSettings);
        packetESID.WriteXML(writer2, false);

        foreach (var ti in ed211.Elements)
        {
            if (ti.DC == "1") //Debet (OUR)
            {
                //string id = ti.SwiftRefId!;
                //string ufebs = Repository.GetOutUfebsFileBySwiftId(id);

                //if (File.Exists(ufebs))
                //{
                //    ED100 xml = new(ufebs);
                //    ED206 ed206 = new(ti)
                //    {
                //        BICCorr = xml.PayeeBIC ?? string.Empty,
                //        CorrAcc = xml.PayeeCorrespAcc ?? string.Empty,

                //        AccDocDate = xml.AccDocDate,
                //        AccDocNo = xml.AccDocNo,

                //        EDRefAuthor = xml.EDAuthor, //CorrBank.UIC!,
                //        EDRefDate = xml.EDDate,
                //        EDRefNo = xml.EDNo,

                //        Acc = ed211.Acc, //CorrBank.OurAcc,

                //        EDAuthor = CorrBank.ProfileUIC!,
                //        EDDate = date,
                //        EDNo = EDHelpers.NextEDNo(),

                //        TransDate = date,
                //        //TransTime = time
                //    };

                //    ed206.WriteXML(writer2);
                //}

                ED206 ed206 = new(ti)
                {
                    Acc = ed211.Acc, //CorrBank.OurAcc,

                    EDAuthor = CorrBank.ProfileUIC!,
                    EDDate = date,
                    EDNo = EDHelpers.NextEDNo(),

                    TransDate = date
                    //TransTime = time
                };

                ed206.WriteXML(writer2);
            }
        }

        writer2.Close();
    }

    private static void Process199AsText(string inFile, string outFile)
    {
        var lines = File.ReadAllLines(inFile);
        string text = SwiftMT199.ToString(lines);

        string date = DateTime.Now.ToString("yyyy-MM-dd");
        string path = Repository.GetInStoreFile(outFile, date);

        File.WriteAllText(path, text, _encoding);
    }

    private static void ProcessKvit(string inFile, string outFile)
    {
        /*
{1:F21CITVRU2PXXXX0804012157}
{4:{177:2208041611}
{451:0}}

{1:F21CITVRU2PXXXX0125010279}
{4:{177:2401252315}
{451:0}}
        */

        var lines = File.ReadAllLines(inFile);
        (string swiftid, string date, string time) = SwiftKvit.ToTransTime(lines);
        (string eddate, string edno) = swiftid.ParseSwiftId();
        string path = Repository.GetInStoreFile(outFile, date, $".{swiftid}.kvit.txt");

        StringBuilder sb = new();
        sb.AppendLine()
            .AppendLine(swiftid)
            .AppendLine(@$"EDDate=""{eddate}"" EDNo=""{edno}""")
            .AppendLine(@$"TransDate=""{date}"" TransTime=""{time}""");

        File.Copy(inFile, path, true);
        File.AppendAllText(path, sb.ToString(), _encoding);
    }
}
