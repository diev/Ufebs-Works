#region License
/*
Copyright 2022-2024 Dmitrii Evdokimov
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

namespace CorrLib.SWIFT;

/// <summary>
/// SWIFT-RUR 6: MT950 ("ВЫПИСКА").
/// </summary>
public static class SwiftMT950
{
    /// <summary>
    /// MT 950 Выписка.
    /// </summary>
    /// <param name="ed"></param>
    /// <param name="lines"></param>
    /// <returns></returns>
    public static ED211 Load(this ED211 ed, string[] lines)
    {
        /*
{1:F01CITVRU2PXXXX0126000003}{2:O9500139240126ALFARUMMXXXX01260002262401260139N}{3:{113:RUR6}{108:1OP1EE0006939724}}{4:
:20:+501240000180452
:25:30109810200000000654
:28C:00005/1
:60F:C240125RUB12089875,43
:61:240125D2500000,S103+240125000010280//OP1ED241P003XYER
18
:61:240125D3500000,S103+240125000010279//OP1ED241P003XYRO
19
:61:240125D5500000,S103+240124000010148//OP1ED241O003BCEY
16
:61:240125D6500000,S103+240124000010147//OP1EN241P0001FP5
17
:61:240125C12000000,NTRFNONREF//+OP1ED41P00358SD
297
:62F:C240125RUB6089875,43
:64:C240125RUB6089875,43
-}{5:{MAC:00000000}{CHK:000019A7384E}}
        */

        int n = 0;
        string line = lines[n++];

        string time = line.UParseDateTime().time;

        long debetSum = 0;
        long creditSum = 0;

        // Проход 1: Общий

        // Счет корреспондентский

        while (!line.StartsWith(":25:")) line = lines[n++];
        ed.Acc = line[4..];

        // Входящий остаток

        while (!line.StartsWith(":60F:")) line = lines[n++];
        ed.EnterBal = line[5..].UParseBal().sum;

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
        ed.AbstractDate = date;
        ed.OutBal = bal;

        //Console.WriteLine($"\n---- Выписка ---- {date} {Path.GetFileName(inFile)}");

        // Движения средств (имея итоги, читаем заново)

        ed.Elements = new TransInfo[qty];
        qty = 0;

        // Проход 2: Трансакции

        for (n = startN; n < finishN; n++)
        {
            line = lines[n];

            if (line.StartsWith(":61:"))
            {
                var (dc, sum, id) = line[4..].ParseTrans();
                bool debet = dc == "1";
                bool order = id == "NONREF"; // Банковский ордер (как вариант)
                bool found = debet
                    ? TransInfoEx.D.TryGetValue(id, out TransInfo? ti) // ourId
                    : TransInfoEx.C.TryGetValue(id, out ti); // corrId

                if (found && !order)
                {
                    //Console.WriteLine($"{id}  {dc}{ti!.AccDocNo,9} {ti.Sum.DisplaySum(),18}");
                }
                else
                {
                    string accDocNo = lines[n + 1];
                    //Console.WriteLine($"{id,16} !{dc}{accDocNo,9} {sum.DisplaySum(),18} ? (строка {n + 1})");
                    // throw new ArgumentException($"Документ не найден.", id);

                    ti = new()
                    {
                        AccDocNo = accDocNo,
                        BICCorr = CorrBank.ProfileBIC!,
                        DC = dc,
                        EDRefAuthor = CorrBank.UIC!,
                        EDRefDate = date,
                        EDRefNo = accDocNo,
                        //PayeePersonalAcc = debet ? "0" : CorrBank.ProfilePayAcc!,
                        //PayerPersonalAcc = debet ? CorrBank.ProfilePayAcc! : "0",
                        PayeePersonalAcc = debet ? CorrBank.ProfileCorrAcc! : CorrBank.ProfilePayAcc!,
                        PayerPersonalAcc = debet ? CorrBank.ProfilePayAcc! : CorrBank.ProfileCorrAcc!,
                        Sum = sum,
                        TransKind = order ? "17" : "01",

                        // Extensions
                        AccDocDate = date, //TODO ?
                        CorrAcc = CorrBank.ProfileCorrAcc,
                        SwiftRefId = id,
                    };
                }

                ed.Elements[qty++] = ti!;

                long kop = long.Parse(ti!.Sum);

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

        ed.BIC = CorrBank.BIC!;
        ed.EDAuthor = CorrBank.ProfileUIC!;
        ed.EDDate = date;
        ed.EDNo = EDHelpers.NextEDNo();
        ed.EndTime = time;
        ed.EDReceiver = CorrBank.UIC!;

        ed.DebetSum = debetSum > 0 ? debetSum.ToString() : "0";
        ed.CreditSum = creditSum > 0 ? creditSum.ToString() : "0";

        return ed;
    }
}
