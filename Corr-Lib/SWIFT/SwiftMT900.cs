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

using System.Text;

using CorrLib.UFEBS.DTO;

namespace CorrLib.SWIFT;

/// <summary>
/// SWIFT-RUR 6: MT900 ("ДЕБЕТОВОЕ АВИЗО").
/// </summary>
public static class SwiftMT900
{
    /// <summary>
    /// MT 900 Дебетовое авизо
    /// </summary>
    /// <param name="ed"></param>
    /// <param name="lines"></param>
    /// <returns></returns>
    public static ED206 Load(this ED206 ed, string[] lines)
    {
        /*
        Статус Номер поля Название поля Формат/ Опции N
        О 20 Референс операции 16x 1
        О 21 Связанный референс 16x 2
        О 25 Номер счета 35x 3
        О 32A Дата валютирования, код валюты, сумма 6!n3!a15d 4
        Н 52а Банк-Плательщик A или D 5
        Н 72 Информация Отправителя Получателю 6*35x 6
         * 
{1:F01CITVRU2PXXXX0123000104}{2:O9000944240123ALFARUMMXXXX01230264502401230944N}{3:{113:RUR6}{108:1OP1E00005509537}}{4:
:20:+P1ED241M003LDEF
:21:+240122600010264
:25:30109810200000000654
:32A:240123RUB6000000,
:72:/NZP/cASTIcNYi VOZVRAT OSNOVNOGO DO
//LGA PO SOGLAQENIu O ZAMENE OBaZAT
//ELXSTVA B/N OT 11.11.2019G. SUMMA
// 6000000-00 BEZ NALOGA (NDS)
-}{5:{MAC:00000000}{CHK:0000F41BF6CB}}
        */

        int n = 0;
        string line = lines[n++];

        // Id Референс операции

        while (!line.StartsWith(":20:")) line = lines[n++];
        ed.SwiftId = line[4..];

        // RefId Связанный референс

        while (!line.StartsWith(":21:")) line = lines[n++];
        ed.SwiftRefId = line[4..];

        // Acc Номер счета

        while (!line.StartsWith(":25:")) line = lines[n++];
        ed.Acc = line[^20..];

        // Sum+ Дата валютирования, валюта, сумма

        while (!line.StartsWith(":32A:")) line = lines[n++];
        var (date, sum) = line[5..].UParseDateSum(); // :32A:
        ed.TransDate = date;
        //ed.TransTime = DateTime.Now.ToString("HH:mm:ss"); // Unknown! Set 23:59:59 by default
        ed.Sum = sum;

        //ed.DC = "1"; // by default 1

        var e = SwiftID.Id(ed.SwiftRefId);
        ed.EDRefDate = e.EDDate;
        ed.EDRefNo = e.EDNo;

        // defaults to replace later if any

        ed.EDDate = date;
        ed.AccDocDate = date;
        ed.AccDocNo = "1";

        return ed;
    }

    public static (string date, string text) ToString(string[] lines)
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

        return (date, sb.ToString());
    }
}
