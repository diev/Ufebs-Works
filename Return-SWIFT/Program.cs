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
using static CorrLib.SWIFT.SwiftHelpers;
using static CorrLib.SWIFT.SwiftTranslit;
using CorrLib.UFEBS;

using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using CorrLib.UFEBS.DTO;

namespace Return_SWIFT;

internal class Program
{
    static Dictionary<string, TransInfo> _d = new();
    static Dictionary<string, TransInfo> _c = new();
    static string? _o950;

    static int _edNo = 0;

    static readonly XmlWriterSettings _xmlSettings = new()
    {
        Encoding = Encoding.GetEncoding(1251),
        Indent = true
    };

    static void Main(string[] args)
    {
        string path, outPath;

        if (args.Length == 0 || args[0] == "/?" || args[0] == "-?") // nothing
        {
            Console.WriteLine("Usage: Input|* [Output_]");
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

        if (_o950 != null)
        {
            ProcessO950(_o950, outPath + ".O950.ED211");
        }

        #region finish
        //while (true)
        //{
        //    if (Console.ReadKey().Key == ConsoleKey.Spacebar)
        //    {
        //        break;
        //    }
        //}
        #endregion finish
    }

    private static void ProcessFile(string inFile, string outFile)
    {
        #region Files start
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
        #endregion Files start

        //TODO

        string text = File.ReadAllText(inFile);
        string pattern = @"{2:([IO]\d{3})";
        string mt = Regex.Match(text, pattern).Groups[1].Value;

        switch (mt)
        {
            case "I103":
                ProcessI103(inFile, $"{outFile}.{mt}.ED101");
                break;

            case "O103":
                ProcessO103(inFile, $"{outFile}.{mt}.ED101");
                break;

            case "O900":
                ProcessO900(inFile, $"{outFile}.{mt}.ED206");
                break;

            case "O950":
                _o950 = inFile;
                //ProcessO950(inFile, $"{outFile}.{mt}.ED211");
                break;

            case "O196":
                ProcessO196(inFile, $"{outFile}.{mt}.Note");
                break;

            case "O199":
                ProcessO199(inFile, $"{outFile}.{mt}.Note");
                break;

            case "O299":
                ProcessO299(inFile, $"{outFile}.{mt}.Note");
                break;

            default:
                if (mt.Length > 0)
                {
                    Console.WriteLine($"Unknown {mt} in \"{inFile}\"!");
                }
                else
                {
                    ProcessKvit(inFile, outFile);
                }
                break;
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

    private static void ProcessI103(string inFile, string outFile)
    {
        /*
{1:F01CITVRU2PXXXX0080000003}{2:O1031339220805ALFARUMMXXXX00805156072208051339N}{3:{113:RUR6}{108:1OP1E00033125402}{111:001}{121:37aef0d4-be5b-4a48-8c7e-91cd752c32de}}{4:
:20:+OP1ED285001G0KM
:23B:CRED
:32A:220805RUB6000,
:50K:/30109810300000000063
INN7831001422.KPP784101001
AO mSITI INVEST BANKm 
:52D://RU044525769.30101810745250000769
BBR BANK (AO)
G MOSKVA
:53B:/C/30109810200000000654
:59:INN7831001422.KPP784101001
AO mSITI INVEST BANKm 
:70:PODKREPLENIE KORRESPONDENTSKOGO ScE
TA AO mSITI INVEST BANKm. NDS NE OB
LAGAETSa. 
:71A:SHA
:72:/RPP/3273.220805.5.ELEK.01
/DAS/220805.220805.000000.000000
-}{5:{MAC:00000000}{CHK:0000558B0EB5}}
         */

        var lines = File.ReadAllLines(inFile);
        ED100 ed = new(lines) //TODO load a source XML file!
        {
            CodePurpose = "1", //TODO ??
            EDAuthor = "4525593000", //TODO ALFA
            EDNo = (++_edNo).ToString(),
            EDReceiver = "4030702000"
        };
        ed.EDDate = ed.ChargeOffDate;

        ed.PayeeBIC ??= "044525593"; //TODO ALFA
        ed.PayeeCorrespAcc ??= "30101810200000000593"; //TODO ALFA

        TransInfo ti = new(ed, "1");
        _d.Add(ed.SwiftId!, ti);

        PacketEPD packet = new()
        {
            EDAuthor = ed.EDAuthor,
            EDDate = ed.EDDate,
            EDNo = (++_edNo).ToString(),
            EDQuantity = "1",
            Sum = ed.Sum,
            SystemCode = ed.SystemCode,
            Elements = new ED100[1]
        };

        packet.Elements[0] = ed;

        //outFile = $"_{date}_EPD_30109810200000000654_.xml"; // ALFA

        var settings = new XmlWriterSettings
        {
            Encoding = Encoding.GetEncoding(1251),
            Indent = true
            //NewLineOnAttributes = true
        };
        using var writer = XmlWriter.Create(outFile, settings);
        packet.WriteXML(writer);
    }

    private static void ProcessO103(string inFile, string outFile)
    {
        /*
{1:F01CITVRU2PXXXX0080000003}{2:O1031339220805ALFARUMMXXXX00805156072208051339N}{3:{113:RUR6}{108:1OP1E00033125402}{111:001}{121:37aef0d4-be5b-4a48-8c7e-91cd752c32de}}{4:
:20:+OP1ED285001G0KM
:23B:CRED
:32A:220805RUB6000,
:50K:/30109810300000000063
INN7831001422.KPP784101001
AO mSITI INVEST BANKm 
:52D://RU044525769.30101810745250000769
BBR BANK (AO)
G MOSKVA
:53B:/C/30109810200000000654
:59:INN7831001422.KPP784101001
AO mSITI INVEST BANKm 
:70:PODKREPLENIE KORRESPONDENTSKOGO ScE
TA AO mSITI INVEST BANKm. NDS NE OB
LAGAETSa. 
:71A:SHA
:72:/RPP/3273.220805.5.ELEK.01
/DAS/220805.220805.000000.000000
-}{5:{MAC:00000000}{CHK:0000558B0EB5}}
         */

        var lines = File.ReadAllLines(inFile);
        ED100 ed = new(lines)
        {
            CodePurpose = "1",
            EDAuthor = "4525593000" //TODO ALFA
        };
        ed.EDDate = ed.ChargeOffDate;
        ed.EDNo = (++_edNo).ToString();
        ed.EDReceiver = "4030702000";

        ed.PayeeBIC ??= "044525593"; //TODO ALFA
        ed.PayeeCorrespAcc ??= "30101810200000000593"; //TODO ALFA

        TransInfo ti = new(ed, "2");
        _c.Add(ed.SwiftId!, ti);

        PacketEPD packet = new()
        {
            EDAuthor = ed.EDAuthor,
            EDDate = ed.EDDate,
            EDNo = (++_edNo).ToString(),
        EDQuantity = "1",
            Sum = ed.Sum,
            SystemCode = ed.SystemCode,
            Elements = new ED100[1]
        };

        packet.Elements[0] = ed;

        //outFile = $"_{date}_EPD_30109810200000000654_.xml"; // ALFA

        using var writer = XmlWriter.Create(outFile, _xmlSettings);
        packet.WriteXML(writer);
    }

    private static void ProcessO900(string inFile, string outFile)
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

        File.Copy(inFile, outFile, true);
    }

    private static void ProcessO950(string inFile, string outFile)
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
        string line = lines[n];
        string pattern;
        Match match;

        ED211 ed = new();

        // Счет корреспондентский

        while (!line.StartsWith(":25:")) line = lines[n++];
        ed.Acc = line[4..];

        // Входящий остаток

        while (!line.StartsWith(":60F:")) line = lines[n++];
        //string enterBal = UfebsSum(line[15..]);
        pattern = @"^:60F:([CD])(\d{6})RUB(\d+,\d{0,2})";
        match = Regex.Match(line, pattern);

        //match.Groups[1].Value //Debit|Credit
        //edDate = UfebsDate(match.Groups[2].Value);
        ed.EnterBal = UfebsSum(match.Groups[3].Value);

        // Подсчитываем число движений

        int qty = 0;

        while (!line.StartsWith(":62F:"))
        {
            if (line.StartsWith(":61:"))
            {
                qty++;
            }

            line = lines[n++];
        }

        // Исходящий остаток (использовать 62F или 64?)

        while (!line.StartsWith(":62F:")) line = lines[n++];
        //string outBal = UfebsSum(line[15..]);
        pattern = @"^:62F:([CD])(\d{6})RUB(\d+,\d{0,2})";
        match = Regex.Match(line, pattern);

        //match.Groups[1].Value //Debit|Credit
        ed.AbstractDate = UfebsDate(match.Groups[2].Value);
        ed.OutBal = UfebsSum(match.Groups[3].Value);

        // Движения средств (имея итоги, читаем заново)

        ed.Elements = new TransInfo[qty];
        int i211 = 0;

        PacketESID packet = new();
        packet.Elements = new ED206[_d.Count];
        int i206 = 0;

        n = 0;
        line = lines[n++];

        while (!line.StartsWith(":62F:"))
        {
            if (line.StartsWith(":61:"))
            {
                pattern = @"^:61:(\d{6})([CD])(\d+,\d{0,2})([^/]+)//(.+)$";
                match = Regex.Match(line, pattern);

                //chargeOffDate = UfebsDate(match.Groups[1].Value);
                string dc = match.Groups[2].Value == "D" ? "1" : "2";
                string sum = UfebsSum(match.Groups[3].Value);
                string ourId = match.Groups[4].Value[4..]; //NONREF | +220804000012157
                string corrId = match.Groups[5].Value;
                string accDocNo;

                TransInfo ti = new();
                bool found = false;

                if (dc == "1" && _d.TryGetValue(ourId, out TransInfo di))
                {
                    found = true;
                    ti = di with { };

                    ED206 ed206 = new(ti);
                    ed206.ActualReceiver = "4030702000"; //TODO
                    ed206.Acc = "30109810200000000654"; //TODO ALFA
                    packet.Elements[i206++] = ed206;
                }
                else if (dc == "2" && _c.TryGetValue(corrId, out TransInfo ci))
                {
                    found = true;
                    ti = ci with { };
                }

                if (!found) //TODO
                {
                    line = lines[n++];
                    accDocNo = line;

                    ti.AccDocNo = accDocNo;
                    ti.DC = dc;
                    ti.Sum = sum;
                }

                ed.Elements[i211++] = ti;
            }

            line = lines[n++];
        }

        ed.BIC = "044525593"; //TODO ALFA
        ed.EDAuthor = "4525593000"; //TODO ALFA
        ed.EDDate = ed.AbstractDate;
        ed.EDNo = (++_edNo).ToString();

        packet.EDAuthor = ed.EDAuthor;
        packet.EDDate = ed.EDDate;
        packet.EDNo = (++_edNo).ToString();
        packet.EDReceiver = "4030702000"; //TODO

        //TODO ED211
        //File.WriteAllText(outFile, "ED211", Encoding.GetEncoding(1251));

        using var writer = XmlWriter.Create(outFile + ".ED211", _xmlSettings); //TODO outFile name
        ed.WriteXML(writer);

        using var writer2 = XmlWriter.Create(outFile + ".ED206", _xmlSettings); //TODO outFile name
        packet.WriteXML(writer2);

        /*
<?xml version="1.0" encoding="windows-1251"?>
<ED211 xmlns="urn:cbr-ru:ed:v2.0" EDNo="5" EDDate="2022-08-05" EDAuthor="4525593000" EDReceiver="4030702000" CreditSum="600000" DebetSum="588000" OutBal="62000" EnterBal="50000" BIC="044525593" Acc="30109810200000000654" EndTime="01:08:28" AbstractDate="2022-08-05" LastMovetDate="2022-08-05" AbstractKind="0">
  <TransInfo AccDocNo="3268" BICCorr="046577952" DC="1" PayeePersonalAcc="40702810400280008837" PayerPersonalAcc="60311810000000000665" Sum="588000" TransKind="01" TurnoverKind="1">
    <EDRefID EDNo="10285" EDDate="2022-08-05" EDAuthor="4030702000" />
  </TransInfo>
  <TransInfo AccDocNo="3273" BICCorr="044525769" DC="2" PayeePersonalAcc="30109810200000000654" PayerPersonalAcc="30109810300000000063" Sum="600000" TransKind="01" TurnoverKind="1">
    <EDRefID EDNo="10286?" EDDate="2022-08-05" EDAuthor="4030702000" />
  </TransInfo>
</ED211>
        */
    }

    private static void ProcessO196(string inFile, string outFile)
    {
        /*
{1:F01CITVRU2PXXXX0208000001}{2:O1961654220802AVTBRUMMAXXX02080218312208021654N}{3:{113:RUR6}}{4:
:20:+XCIT22080225732
:21:220622-289F195
:76:'ATTN. INVESTIGATIONS DEPT.
RE YOUR MT195 DD 220622 WITH REF
AS ABOVE STATED CONC. YOUR MT103
FOR USD 8575.00 DD 220601 WITH REF
010622-259F103'
:77A:'PLEASE BE INFORMED WE RECEIVED
FOLLOWING REQUEST FROM IRVTUS3N.
PLEASE PROVIDE ADD INFO AS
REQUESTED AND SCANNED COPIES OF
THE SUPPORTING DOCUMENTS TO
FUNDSTRANSFER(AT)URALSIB.RU.
YOU NEED TO SPECIFY THE PHRASE':
'HEREWITH WE CONFIRM OUR CONSENT FO
R
PROCESSING OF DATA AND ALSO FOR
TRANSFER OF DATA AND SUPPORTING
DOCUMENTS TO THIRD PARTIES UNDER
THIS PAYMENT.
BEST REGARDS,
INVESTIGATIONS/ POPOVA EKATERINA,
URALSIB BANK , MOSCOW RU'
:11R:199
220706
:79:20:'BKI-220616004067
':21:'CPIRVT2206010971
':79:'ATTN INVESTIGATIONS
BOFAUS3N STATES QUOTE
20 BML220615-003495 21 F5S2206019600500 79 ATTN.
ATTN OFAC DEPT OUR REF. 2022060100405029 YR PYMT
VAL 06/01/2022 AMT USD 8575.00 0048809 REQUIRE
ADDL DETAILS PLEASE PROVIDE A DETAILED PURPOSE OF
PAYMENT AND ALL INVOICES WITH THIS TRANSACTION.
REGARDS,
FT - OFAC
BNYM TRN..F5S2206019600500
ORGREF..CPIRVT2206010971
UETR..30104E46-BC0A-4C2A-8C84-756DF988E1DB
CHIPS SSN..404939
INSTR DATE..22/06/01 VALUE DATE..22/06/01
AMOUNT..8,575.00/USD
ORD CUST..40817840800000000634
ORD BANK..ABIC/CITVRU2PXXX
DR PARTY..URALSIB BANK
DR ADDRESS..MOSCOW 119048, RUSSIA
CR PARTY..BANK OF AMERICA N.A.
CR ADDRESS..NEW YORK, N.Y. 10001
BENE/BENE BNK..002424968997
BENE/BENE BNK..ALEXANDER SOIBEL
DETAILS PYMT..PRIVATE PAYMENT
'
-}{5:}
        */

        File.Copy(inFile, outFile, true);
    }

    private static void ProcessO199(string inFile, string outFile)
    {
        /*
{1:F01CITVRU2PXXXX0063000001}{2:O1992255220630ALFARUMMXXXX00630219332206302255N}{3:{113:RUR6}{108:1ZLTH00031376433}}{4:
:20:010222A710200070
:21:NONREF
:79:UVEDOMLENIE O PRINaTII REQENIa OB OSUqESTVLENII
DEaTELXNOSTI UPOLNOMOcENNOGO BANKA V CELaH
ISPOLNENIa UKAZA PREZIDENTA ROSSIiSKOi FEDERACII
OT 27.05.2022n 322 O VREMENNOM PORaDKE ISPOLNENIa
OBaZATELXSTV PERED NEKOTORYMI PRAVOOBLADATELaMI
.
NASTOaqIM AKCIONERNOE OBqESTVO ALXFA-BANK V
SOOTVETSTVII S ABZACEM TRETXIM PUNKTA 1
POSTANOVLENIa PRAVITELXSTVA RF OT 06.06.2022 n
1031  O REALIZACII NEKOTORYH POLOJENIi UKAZA
PREZIDENTA ROSSIiSKOi FEDERACII OT 27 MAa 2022 G.
'N 322 'O VREMENNOM PORaDKE ISPOLNENIa OBaZATELXST
V PERED NEKOTORYMI PRAVOOBLADATELaMI  I VNESENII
IZMENENIi V PRAVILA VYDAcI PRAVITELXSTVENNOi
KOMISSIEi PO KONTROLu ZA OSUqESTVLENIEM
INOSTRANNYH INVESTICIi V ROSSIiSKOi FEDERACII
RAZREQENIi V CELaH REALIZACII DOPOLNITELXNYH
VREMENNYH MER eKONOMIcESKOGO HARAKTERA PO
OBESPEcENIu FINANSOVOi STABILXNOSTI ROSSIiSKOi
FEDERACII I INYH RAZREQENIi, PREDUSMOTRENNYH
OTDELXNYMI UKAZAMI PREZIDENTA ROSSIiSKOi FEDERACII
UVEDOMLaET O PRINaTII REQENIa OSUqESTVLaTX
DEaTELXNOSTX UPOLNOMOcENNOGO BANKA V CELaH
OTKRYTIa SPECIALXNYH ScETOV TIPA O DLa ISPOLNENIa
DOLJNIKAMI DENEJNYH OBaZATELXSTV,
SVaZANNYH S ISPOLXZOVANIEM IMI REZULXTATOV
INTELLEKTUALXNOi DEaTELXNOSTI I (ILI) SREDSTV
INDIVIDUALIZACII, V CELaH ISPOLNENIa UKAZA
PREZIDENTA RF OT 27.05.2022 n 322 O VREMENNOM
PORaDKE ISPOLNENIa OBaZATELXSTV PERED NEKOTORYMI
PRAVOOBLADATELaMI
-}{5:{MAC:00000000}{CHK:0000ACD24BFC}}
        */

        var lines = File.ReadAllLines(inFile);
        int n = 0;
        string line = lines[n];

        while (!line.StartsWith(":79:")) line = lines[n++];
        StringBuilder sb = new(line[4..]);
        line = lines[n++];

        while (!line.StartsWith("-}"))
        {
            sb.AppendLine(Cyr(line));
            line = lines[n++];
        }

        File.WriteAllText(outFile, sb.ToString(), Encoding.GetEncoding(1251));
    }

    private static void ProcessO299(string inFile, string outFile)
    {
        /*
{1:F01CITVRU2PXXXX0070000001}{2:O2991651220707ALFARUMMXXXX00707179002207071651N}{3:{113:RUR6}{108:1AO5400031708864}}{4:
:20:AO54D2277000000O
:21:NONREF
:79:VNIMANIu: KORRESPONDENTSKIE ScETA,
MEJDUNARODNYE RAScETY
TEMA: NOVAa REDAKCIa TARIFOV RAScETNO-
KASSOVOGO OBSLUJIVANIa KREDITNYH
ORGANIZACIi OT 01.08.2022
.
AO ALXFA-BANK UVEDOMLaET O TOM, cTO 01.08.2022
VSTUPAET V DEiSTVIE NOVAa REDAKCIa TARIFOV
RAScETNO-KASSOVOGO OBSLUJIVANIa KREDITNYH
ORGANIZACIi.
V TARIFY VNESENY SLEDUuqIE IZMENENIa:
(A) USTANOVLEN TARIF ZA PEREVOD SO ScETA TIPA
mSm (P. 2.2.1.2)v
(B) TARIFY ZA VNEQNIE PEREVODY V 'CNY ('P. 2.2.2.3
) I RASSLEDOVANIa (P. 2.2.2.6) USTANOVLENY V
VALuTE 'CNY'v
(V) ISKLucENY TARIFY NA USLUGI, KOTORYE
BANK NE OKAZYVAET V NASTOaqEE VREMa.
.
NOVAa REDAKCIa TARIFOV RAZMEqENA NA SAiTE
'ALFABANK.RU 'V RAZDELE FINANSOVYM
ORGANIZACIaM-KORRESPONDENTSKIE ScETA V
ALXFA BANKE.
.
S UVAJENIEM,
KOMANDA PRODAJ NA FINANSOVYH RYNKAH
+7 495 7953688, +7 499 6811709
'FI(AT)ALFABANK.RU'
-}{5:{MAC:00000000}{CHK:0000CB1282DF}}
        */

        var lines = File.ReadAllLines(inFile);
        int n = 0;
        string line = lines[n];

        while (!line.StartsWith(":79:")) line = lines[n++];
        StringBuilder sb = new(line[4..]);
        line = lines[n++];

        while (!line.StartsWith("-}"))
        {
            sb.AppendLine(Cyr(line));
            line = lines[n++];
        }

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
