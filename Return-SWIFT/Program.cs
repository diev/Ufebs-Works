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

namespace Return_SWIFT;

internal class Program
{
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
        string pattern = @"{2:O(\d{3})";
        string mt = Regex.Match(text, pattern).Groups[1].Value;

        switch (mt)
        {
            case "103":
                ProcessMT103(inFile, outFile);
                break;

            case "900":
                ProcessMT900(inFile, outFile);
                break;

            case "950":
                ProcessMT950(inFile, outFile);
                break;

            case "196":
                ProcessMT196(inFile, outFile);
                break;

            case "199":
                ProcessMT199(inFile, outFile);
                break;

            case "299":
                ProcessMT299(inFile, outFile);
                break;

            default:
                if (mt.Length > 0)
                {
                    Console.WriteLine($"Unknown MT{mt} in \"{inFile}\"!");
                }
                else
                {
                    ProcessKvit(inFile, outFile);
                }
                break;
        }

        #region Files end
        Console.WriteLine();

        if (File.Exists(outFile))
        {
            Console.WriteLine($"[Output \"{outFile}\" done. Press Spacebar.]");
        }
        else
        {
            Console.WriteLine($"[Output \"{outFile}\" FAIL! Press Spacebar.]");
        }

        Console.WriteLine();
        #endregion Files end
    }

    private static void ProcessMT103(string inFile, string outFile)
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

        ED100 ed = new();

        var lines = File.ReadAllLines(inFile);
        int n = 0;
        string line = lines[n];
        string pattern;
        Match match;

        // Tax

        while (!line.StartsWith(":32A:"))
        {
            line = lines[n++];

            if (line.StartsWith(":26T:"))
            {
                pattern = @"^:26T:S(\d*)$";
                match = Regex.Match(line, pattern);

                ed.DrawerStatus = match.Groups[1].Value;
            }
        }

        // Sum

        pattern = @"^:32A:(\d{6})RUB(\d+,\d{0,2})$";
        match = Regex.Match(line, pattern);

        string date = match.Groups[1].Value;
        ed.ChargeOffDate = UfebsDate(date);
        ed.Sum = UfebsSum(match.Groups[2].Value);

        // Payer

        while (!line.StartsWith(":50K:")) line = lines[n++];
        ed.PayerPersonalAcc = line[^20..];

        line = lines[n++];
        pattern = @"^INN(\d*)(\.KPP(\d*)){0,1}$";
        match = Regex.Match(line, pattern);

        ed.PayerINN = match.Groups[1].Value;
        ed.PayerKPP = match.Groups.Count > 2
            ? match.Groups[3].Value
            : null;

        string name = string.Empty;
        line = lines[n++];

        while (!line.StartsWith(":52D:"))
        {
            name += line;
            line = lines[n++];
        }

        ed.PayerName = Cyr(name);

        pattern = @"^:52D://RU(\d*)(\.(\d*))$";
        match = Regex.Match(line, pattern);

        ed.PayerBIC = match.Groups[1].Value;
        ed.PayerCorrespAcc = match.Groups.Count > 2
            ? match.Groups[3].Value
            : null;

        // Payee

        while (!line.StartsWith(":53B:")) line = lines[n++];
        ed.PayeePersonalAcc = line[^20..];

        while (!line.StartsWith(":59:")) line = lines[n++];
        pattern = @"^:59:INN(\d*)(\.KPP(\d*)){0,1}$";
        match = Regex.Match(line, pattern);

        ed.PayeeINN = match.Groups[1].Value;
        ed.PayeeKPP = match.Groups.Count > 2
            ? match.Groups[3].Value
            : null;

        name = string.Empty;
        line = lines[n++];

        while (!line.StartsWith(":70:"))
        {
            name += line;
            line = lines[n++];
        }

        ed.PayeeName = Cyr(name);

        // Purpose

        name = line[4..];
        line = lines[n++];

        while (!line.StartsWith(":71A:"))
        {
            name += line;
            line = lines[n++];
        }

        // Extra
        
        while (!line.StartsWith(":72:")) line = lines[n++];
        line = line[4..];
        bool nzp = false;

        while (line.StartsWith('/'))
        {
            if (line.StartsWith("/RPP/"))
            {
                nzp = false;

                pattern = @"^/RPP/(\d*)\.(\d*)\.(\d)\.(\w{4})\.(\d*)";
                match = Regex.Match(line, pattern);

                ed.AccDocNo = match.Groups[1].Value;
                ed.AccDocDate = UfebsDate(match.Groups[2].Value);
                ed.Priority = match.Groups[3].Value;

                if (match.Groups[4].Value == "BESP")
                {
                    ed.PaytKind = "?"; //TODO
                    ed.PaymentPrecedence = "69";
                }

                ed.TransKind = match.Groups.Count > 4
                    ? match.Groups[5].Value
                    : "01";
            }
            else if (line.StartsWith("/DAS/"))
            {
                nzp = false;

                pattern = @"^/DAS/(\d*)\.(\d*)";
                match = Regex.Match(line, pattern);

                //ed.ChargeOffDate = UfebsDate(match.Groups[1].Value);
                ed.ReceiptDate = UfebsDate(match.Groups[2].Value);
            }
            else if (line.StartsWith("/UIP/"))
            {
                nzp = false;

                pattern = @"^/UIP/(\d*)";
                match = Regex.Match(line, pattern);

                ed.PaymentID = match.Groups[1].Value;
            }
            else if (line.StartsWith("/NZP/"))
            {
                nzp = true;
                name += line[5..];
            }
            else if (line.StartsWith("//") && nzp)
            {
                name += line[2..];
            }
            else
            {
                nzp = false;
            }

            line = lines[n++];
        }

        ed.Purpose = Cyr(name);

        if (ed.Tax)
        {
            while (!line.StartsWith(":77B:")) line = lines[n++];
            line = line[5..];

            while (line.StartsWith("/N"))
            {
                pattern = @"/N4/([^/]*)";
                match = Regex.Match(line, pattern);

                if (match.Success)
                {
                    ed.CBC = Cyr(match.Value);
                }

                pattern = @"/N5/([^/]*)";
                match = Regex.Match(line, pattern);

                if (match.Success)
                {
                    ed.OKATO = Cyr(match.Value);
                }

                pattern = @"/N6/([^/]*)";
                match = Regex.Match(line, pattern);

                if (match.Success)
                {
                    ed.PaytReason = Cyr(match.Value);
                }

                pattern = @"/N7/([^/]*)";
                match = Regex.Match(line, pattern);

                if (match.Success)
                {
                    ed.TaxPeriod = Cyr(match.Value);
                }

                pattern = @"/N8/([^/]*)";
                match = Regex.Match(line, pattern);

                if (match.Success)
                {
                    ed.DocNo = Cyr(match.Value);
                }

                pattern = @"/N9/([^/]*)";
                match = Regex.Match(line, pattern);

                if (match.Success)
                {
                    ed.DocDate = Cyr(match.Value);
                }

                pattern = @"/N10/([^/]*)";
                match = Regex.Match(line, pattern);

                if (match.Success)
                {
                    ed.TaxPaytKind = Cyr(match.Value);
                }
            }
        }

        ed.CodePurpose = "1";
        ed.EDAuthor = "452559300"; // ALFA
        ed.EDDate = ed.ChargeOffDate;
        ed.EDNo = "1"; //TODO inc
        ed.EDReceiver = "4030702000";

        ed.PayeeBIC ??= "044525593"; // ALFA
        ed.PayeeCorrespAcc ??= "30101810200000000593"; // ALFA

        PacketEPD packet = new()
        {
            EDAuthor = ed.EDAuthor,
            EDDate = ed.EDDate,
            EDNo = "2", //TODO inc
            EDQuantity = "1",
            Sum = ed.Sum,
            SystemCode = ed.SystemCode,
            Elements = new ED100[1]
        };

        packet.Elements[0] = ed;

        outFile = $"_{date}_EPD_30109810200000000654_.xml"; // ALFA

        var settings = new XmlWriterSettings
        {
            Indent = true
            //NewLineOnAttributes = true
        };
        using var writer = XmlWriter.Create(outFile, settings);
        packet.WriteXML(writer);
    }

    private static void ProcessMT900(string inFile, string outFile)
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
    }

    private static void ProcessMT950(string inFile, string outFile)
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
        int max = lines.Length;

        // Счет корреспондентский

        while (!lines[n].StartsWith(":25:")) n++;
        string ourAcc = lines[n][4..];

        // Входящий остаток

        while (!lines[n].StartsWith(":60F:")) n++;
        string enterBal = UfebsSum(lines[n][15..]);

        // Пока не исходящий остаток - движения средств

        string chargeOffDate, dc, sum, accDocNo;
        string outBal;
        StringBuilder sb = new();

        while (n++ < lines.Length)
        {
            string line = lines[n];

            if (line.StartsWith(":62F:"))
            {
                outBal = line[15..]; //TODO SwiftSum()
                break;
            }

            if (line.StartsWith(":61:"))
            {
                string pattern = @"^:61:(\d{6})([CD])(\d+,\d{0,2})";
                var matches = Regex.Match(line, pattern);

                chargeOffDate = matches.Groups[1].Value;
                dc = matches.Groups[2].Value;
                sum = matches.Groups[3].Value;
            }
            else
            {
                accDocNo = line;

                sb.Append("<TransInfo AccDocNo=");
            }

        }

    }

    private static void ProcessMT196(string inFile, string outFile)
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
    }

    private static void ProcessMT199(string inFile, string outFile)
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
    }

    private static void ProcessMT299(string inFile, string outFile)
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
    }

    private static void ProcessKvit(string inFile, string outFile)
    {
        /*
{1:F21CITVRU2PXXXX0804012157}
{4:{177:2208041611}
{451:0}}
        */
    }
}
