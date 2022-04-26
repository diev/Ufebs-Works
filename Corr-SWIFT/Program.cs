using static Lib.Swift;

using System.Text;
using System.Xml;

namespace Corr_SWIFT;

public static class Program
{
    public static void Main(string[] args)
    {
        #region test
        string test = "Просим подтвердить получение данного тестового сообщения.";
        string tran = Lat(test);
        return;
        #endregion test

        #region args
        string inFile;
        string outFile;

        if (args.Length == 0)
        {
            Console.WriteLine("Usage: Input [output]");
            return;
        }
        else
        {
            inFile = args[0];
        }

        if (args.Length == 1)
        {
            var file = new FileInfo(inFile);
            int len = file.Extension.Length;
            outFile = file.FullName[..^len] + "_" + file.Extension;
        }
        else
        {
            outFile = args[1];
        }

        if (!File.Exists(inFile))
        {
            Console.WriteLine($"Input \"{inFile}\" not found");
            return;
        }

        if (File.Exists(outFile))
        {
            Console.WriteLine($"Output \"{outFile}\" overwritten");
        }
        #endregion args

        #region navigator
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); //enable Windows-1251

        var packet = new XmlDocument();
        packet.Load(inFile);
        var navigator = packet.CreateNavigator();

        if (navigator == null)
        {
            Console.WriteLine("Xml error");
            return;
        }

        navigator.MoveToFirstChild(); // Root -> PacketEPD || ED101

        if (navigator.LocalName == "PacketEPD")
        {
            navigator.MoveToFirstChild(); // PacketEPD -> ED101
        }
        int total = 0;
        #endregion navigator

        do
        {
            var ed = navigator.ReadSubtree();
            ed.Read();
            string edDate = ed.GetAttribute("EDDate") ?? string.Empty;
            string edNo = ed.GetAttribute("EDNo") ?? string.Empty;
            string sum = ed.GetAttribute("Sum") ?? "0";

            ed.ReadToDescendant("AccDoc");
            string date = ed.GetAttribute("AccDocDate") ?? string.Empty;
            string no = ed.GetAttribute("AccDocNo") ?? string.Empty;

            string title = $"{edNo} (N{no}, ${sum})";

            ed.ReadToFollowing("Payer");

            var payer = ed.ReadSubtree();
            payer.Read();
            string inn1 = payer.GetAttribute("INN") ?? string.Empty;
            string kpp1 = payer.GetAttribute("KPP") ?? string.Empty;
            string acc1 = payer.GetAttribute("PersonalAcc") ?? string.Empty;

            payer.ReadToDescendant("Name");
            string name1 = payer.ReadElementContentAsString() ?? string.Empty;

            //payer.ReadToFollowing("Bank"); //////
            string bic1 = payer.GetAttribute("BIC") ?? string.Empty;
            string bac1 = payer.GetAttribute("CorrespAcc") ?? string.Empty;

            ed.ReadToFollowing("Payee");

            var payee = ed.ReadSubtree();
            payee.Read();
            string inn2 = payee.GetAttribute("INN") ?? string.Empty;
            string kpp2 = payee.GetAttribute("KPP") ?? string.Empty;
            string acc2 = payee.GetAttribute("PersonalAcc") ?? string.Empty;

            payee.ReadToDescendant("Name");
            string name2 = payee.ReadElementContentAsString() ?? string.Empty;

            //payee.ReadToFollowing("Bank"); //////
            string bic2 = payee.GetAttribute("BIC") ?? string.Empty;
            string bac2 = payee.GetAttribute("CorrespAcc") ?? string.Empty;

            ed.ReadToFollowing("Purpose");
            string purpose = ed.ReadElementContentAsString() ?? string.Empty;

            bool tax = ed.LocalName == "DepartmentalInfo";
            //string taxcbc, taxdate, taxno, taxstatus, taxokato, taxreason, taxperiod;

            //if (tax)
            //{
            //    taxcbc = ed.GetAttribute("CBC") ?? "0";
            //    taxdate = ed.GetAttribute("DocDate") ?? "0";
            //    taxno = ed.GetAttribute("DocNo") ?? "0";
            //    taxstatus = ed.GetAttribute("DrawerStatus") ?? "0";
            //    taxokato = ed.GetAttribute("OKATO") ?? "0";
            //    taxreason = ed.GetAttribute("PaytReason") ?? "0";
            //    taxperiod = ed.GetAttribute("TaxPeriod") ?? "0";
            //}

            var result = new StringBuilder();
            result
                .AppendLine("{1:F01CITVRU2PAXXX0000000103}{2:I103JSBSRU2PAXXXN}{3:{113:RUR6}}{4:")
                .AppendLine($":20:+{XDate(edDate)}-{edNo}")
                .AppendLine(":23B:CRED");

            if (tax)
            {
                string status = ed.GetAttribute("DrawerStatus") ?? "00";
                result.Append(":26T:S").AppendLine(status);
            }

            result
                .AppendLine($":32A:{XDate(edDate)}RUB{XSum(sum)}")
                .AppendLine($":33B:RUB{XSum(sum)}")//??
                .AppendLine()
                //.AppendLine("# Реквизиты Плательщика")
                .AppendLine($":50K:/{acc1}")
                .Append("INN").Append(inn1);

            if (kpp1.Length > 1)
            {
                result.Append(".KPP").Append(kpp1);
            }

            result
                .AppendLine()
                .AppendLine(Lat(name1))
                .AppendLine()
                .AppendLine($":53B:/{bac1}")
                .AppendLine()
                //.AppendLine("# Реквизиты банка получателя")
                .AppendLine($":57D:/RU{bic2}.{bac2}")////РКЦ без счета
                .AppendLine()
                //.AppendLine("# Реквизиты получателя платежа")
                .AppendLine($":59K:/{acc2}")
                .Append("INN").Append(inn2);

            if (kpp2.Length > 1)
            {
                result.Append(".KPP").Append(kpp2);
            }
                
            result
                .AppendLine()
                .AppendLine(Lat(name2))
                .AppendLine()
                //.AppendLine("# Назначение платежа")
                .AppendLine($":70:{Lat(purpose)}")
                .AppendLine(":71A:OUR")
                .AppendLine($":72:/RPP/000461.220513.5.ELEK.01")
                .AppendLine($"/DAS/220513.220513.000000.000000");

            if (tax)
            {
                result
                    .AppendLine()
                    .AppendLine(":77B:");
            }

            result
                .AppendLine("-}{5:}");

            Console.WriteLine(result);

            File.WriteAllText($"{edDate}-{edNo}.swt", result.ToString(), Encoding.ASCII);
            total++;
        }
        while (navigator.MoveToNext());

        #region finish
        //Console.WriteLine($"[{outFile} done {total}. Press a space.]");

        //while (true)
        //{
        //    if (Console.ReadKey().Key == ConsoleKey.Spacebar)
        //    {
        //        break;
        //    }
        //}
        #endregion finish
    }
}
