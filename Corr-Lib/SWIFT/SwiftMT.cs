﻿#region License
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

using static CorrLib.SWIFT.SwiftHelpers;
using static CorrLib.SWIFT.SwiftTranslit;

namespace CorrLib.SWIFT;

/// <summary>
/// SWIFT-RUR 6: MT103 ("ОДНОКРАТНОЕ ЗАЧИСЛЕНИЕ КЛИЕНТСКИХ СРЕДСТВ").
/// SWIFT-RUR 6: MT202 ("ОБЩИЙ МЕЖБАНКОВСКИЙ ПЕРЕВОД").
/// </summary>
public static class SwiftMT
{
    public static ED100 Load(this ED100 ed, string[] lines)
    {
        int n = 0;
        string line = lines[n];

        while (!line.StartsWith(":20:")) line = lines[n++];
        ed.SwiftId = line[4..];

        // Tax

        while (!line.StartsWith(":32A:"))
        {
            line = lines[n++];

            if (line.StartsWith(":26T:S")) // optional!
            {
                ed.DrawerStatus = line[6..]; // Tax
            }
        }

        // Sum

        var (date, sum) = ParseDateSum(line[5..]); // :32A:
        ed.ChargeOffDate = UfebsDate(date);
        ed.Sum = UfebsSum(sum);

        // Payer

        while (!line.StartsWith(":50K:/")) line = lines[n++];
        ed.PayerPersonalAcc = line[^20..];

        line = lines[n++];
        var (INN, KPP) = ParseINNKPP(line);
        ed.PayerINN = INN;
        ed.PayerKPP = KPP;

        string name = string.Empty;
        line = lines[n++];

        while (!line.StartsWith(":52A:") && !line.StartsWith(":52D:"))
        {
            name += line;
            line = lines[n++];
        }

        ed.PayerName = Cyr(name);

        if (line.Equals(":52A:CITVRU2P")) //TODO BIC, CorrAcc
        {
            ed.PayerBIC = "044030702";
            ed.PayerCorrespAcc = "30101810600000000702";
        }
        else
        {
            var (bic, acc) = ParseBICAcc(line[5..]); // :52D:
            ed.PayerBIC = bic;
            ed.PayerCorrespAcc = acc;
        }

        // Payee

        while (!line.StartsWith(":53B:")) line = lines[n++];
        ed.DC = line[6] == 'D' ? "1" : "2"; // :53B:/D/30109810200000000654

        if (ed.DC == "1")
        {
            var e = SwiftID.Id(ed.SwiftId);
            ed.EDDate = e.EDDate;
            ed.EDNo = e.EDNo;
        }

        ed.PayeePersonalAcc = line[^20..];

        while (!line.StartsWith(":59:"))
        {
            if (line.StartsWith(":57D:"))
            {
                var (bic, acc) = ParseBICAcc(line[5..]); // :57D:
                ed.PayeeBIC = bic;
                ed.PayeeCorrespAcc = acc;
            }

            line = lines[n++];
        }

        if (line.StartsWith(":59:/"))
        {
            ed.PayeePersonalAcc = line[5..];
            line = lines[n++];

            if (line.StartsWith("INN"))
            {
                (INN, KPP) = ParseINNKPP(line);
                ed.PayeeINN = INN;
                ed.PayeeKPP = KPP;
            }
        }
        else if (line.StartsWith(":59:INN"))
        {
            (INN, KPP) = ParseINNKPP(line[4..]);
            ed.PayeeINN = INN;
            ed.PayeeKPP = KPP;
        }

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
            if (line.StartsWith("/RPP/")) // /RPP/3261.220804.5.ELEK[.01]
            {
                nzp = false;
                var (no, dt, priority, besp, tkind) = ParseRPP(line);

                ed.AccDocNo = no;
                ed.AccDocDate = UfebsDate(dt);
                ed.Priority = priority;
                ed.TransKind = tkind;

                ed.EDType = tkind switch
                {
                    "01" => "ED101", // 01 – платежное поручение (ED101, default)
                    "02" => "ED103", // 02 – платежное требование (ED103)
                    "06" => "ED104", // 06 – инкассовое поручение (ED104)
                    "16" => "ED105", // 16 – платежный ордер (ED105)
                    _ => "ED101"
                };

                if (besp)
                {
                    ed.PaytKind = "?"; //TODO
                    ed.PaymentPrecedence = "69";
                }
            }
            else if (line.StartsWith("/DAS/"))
            {
                nzp = false;
                ed.ReceiptDate = UfebsDate(line[12..18]); // /DAS/220804.220804.000000.000000
            }
            else if (line.StartsWith("/UIP/"))
            {
                nzp = false;
                ed.PaymentID = line[5..];
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
                if (ParseTax(line, "N4", out string n4))
                    ed.CBC = Cyr(n4);

                if (ParseTax(line, "N5", out string n5))
                    ed.OKATO = Cyr(n5);

                if (ParseTax(line, "N6", out string n6))
                    ed.PaytReason = Cyr(n6);

                if (ParseTax(line, "N7", out string n7))
                    ed.TaxPeriod = Cyr(n7);

                if (ParseTax(line, "N8", out string n8))
                    ed.DocNo = Cyr(n8);

                if (ParseTax(line, "N9", out string n9))
                    ed.DocDate = Cyr(n9);

                if (ParseTax(line, "N10", out string n10))
                    ed.TaxPaytKind = Cyr(n10);

                line = lines[n++];
            }
        }

        return ed;
    }

    /// <summary>
    /// SWIFT-RUR 6: MT103 ("ОДНОКРАТНОЕ ЗАЧИСЛЕНИЕ КЛИЕНТСКИХ СРЕДСТВ")
    /// </summary>
    /// <param name="ed"></param>
    /// <returns></returns>
    public static string ToStringMT103(this ED100 ed)
    {
        string sum = ed.Sum; // save for BESP if over 100 000 000.00
        var (Num, Id) = SwiftID.ID(ed);

        // Block Structure

        var sb = new StringBuilder(1024);
        sb
            // Basic header
            .Append("{1:F01") // Block 1 identifier : Application identifier
            .Append(Config.BankSWIFT) // Service identifier
            .Append("AXXX") // Logical terminal address
            .Append(Num) // Session number
            .Append('}')

            // Application header
            .Append("{2:I103") // Block 2 identifier : In, MT103
            .Append(Config.CorrSWIFT.PadRight(12, 'X')) // Destination address with default Logical terminal address XXX X
            .Append("N}") // Message priority (Normal)

            // User header
            .Append("{3:{113:RUR6}{121:") // Block 3 identifier : Version
            .Append(Guid.NewGuid()) // uuid v4
            .Append("}}")

            // Text
            .AppendLine("{4:"); // Block 4 identifier : TEXT between CRLF and '-' ("-}"?)

        #region SWIFT TEXT

        // Референс Отправителя (16x)

        sb.AppendLine($":20:+{Id}");

        // Код банковской операции

        sb.AppendLine(":23B:CRED");

        // Код типа операции

        sb.AppendLineIf(ed.Tax, $":26T:S{ed.DrawerStatus}");

        // Дата валютирования/Валюта/Сумма межбанковского расчета

        sb.AppendLine($":32A:{SwiftDate(ed.ChargeOffDate)}RUB{SwiftSum(ed.Sum)}");

        // Плательщик

        // - Счет - номер счета Плательщика. Допускается указание международного номера банковского счета (IBAN) Плательщика.
        // - INN – идентификационный номер налогоплательщика (ИНН) или код иностранной организации (КИО), если присвоен Плательщику;
        // - KPP – код причины постановки на учет налогоплательщика (КПП). Если код причины постановки на учет отсутствует, кодовое слово KPP и точка не ставятся.

        // Форматы указания INN:
        // - ИНН для юридических лиц - INN10!n
        // - ИНН для физических лиц - INN12!n
        // - КИО для юридических лиц – INN5!n

        // Если поле 26Т заполнено, то ИНН и КПП плательщика обязательны к заполнению для плательщиков–юридических лиц.
        // Если же плательщиком является физическое лицо, при отсутствии ИНН после кодового слова ИНН указывается ноль («0»),
        // после кодового слова КПП указывается ноль («0»).

        // ВНИМАНИЕ! Если плательщиком выступает сам банк-респондент, то указание в поле «50» счета ЛОРО и BIC-кода не допускается.

        sb.AppendLine($":50K:/{ed.PayerPersonalAcc}") // или :50F: с адресом и страной
            .AppendLineIf(ed.PayerINN, $"INN{ed.PayerINN}{ed.PayerKPP.AddKPPNotEmptyNorZeros()}");

        // (3*35x! ??)
        // ООО "Название юрлица"
        // или при отсутствии ИНН у физлица:
        // Ф.И.О. полностью//адрес места жительства (регистрации) или места пребывания//

        sb.AppendLineIf(Lat(ed.PayerName)!.Div35(Config.SwiftNameLimit));

        // Банк Плательщика
        // (финансовая организация, обслуживающая Плательщика, в тех случаях, когда она отлична от Отправителя)

        sb.AppendLine($":52A:{Config.BankSWIFT}");
        //sb.AppendLine($":52D://RU{ed.PayerBIC}.{ed.PayerCorrespAcc}"); // OurBIC.OurCorrACC
        //.AppendLine(SwiftTranslit.Lat("АО Сити Инвест Банк"))
        //.AppendLine(SwiftTranslit.Lat("г.Санкт-Петербург"));

        // Корреспондент Отправителя (реквизиты счета, который должен быть использован при исполнении платежных инструкций)
        // Если Отправитель и Получатель сообщения обслуживают рублевые счета друг друга, и необходимо определить,
        // будет ли производиться зачисление или списание средств, в данном поле после слеша “/” указывается код операции
        // (С - кредитование или D - дебетование) и далее следует еще один слеш “/”и номер соответствующего счета.
        // При наличии у Отправителя и Получателя единственного прямого корреспондентского счета в рублях данное поле не используется,
        // если только иное особо не оговорено в двустороннем соглашении.

        sb.AppendLine($":53B:/D/{Config.CorrAccount}"); // Корсчет нашего банка в том банке

        // Банк-Посредник (опционально в РФ, предпочтительна опция A, а не D)
        // В этом поле определяется сторона между Получателем сообщения и Банком Бенефициара, через которую должна быть проведена операция.
        //sb.AppendLine($":56D:/{ed.PayeePersonalAcc}") //??
        //.AppendLine(SwiftTranslit.Lat("ББР"))
        //.AppendLine(SwiftTranslit.Lat("г.Санкт-Петербург"));

        // Банк Бенефициара
        // (финансовая организация, обслуживающая счет клиента-бенефициара - в том случае, если она отлична от Получателя сообщения)

        sb.AppendLine($":57D://RU{ed.PayeeBIC}{ed.PayeeCorrespAcc.AddNotEmpty()}");

        // Указание местонахождения Банка бенефициара при проведении платежа через расчетную систему Банка России обязательно.
        // Указание адреса Банка бенефициара является необязательным, при наличии, отделяется запятой от наименования.

        // 4*32x
        //.AppendLine(SwiftTranslit.Lat("Какой-то банк получателя,")) // Надо ли брать из Справочника БИК?
        //.AppendLine(SwiftTranslit.Lat("г.Город"));

        var bankInfo = ED807Finder.Find(ed.PayeeBIC!, true) ?? new BankInfo("BANK", "G"); //TODO BIC not found
        sb.AppendLine(bankInfo.Name.Div35())
            .AppendLine(bankInfo.Place);

        // Бенефициар
        // (клиент, которому будут выплачены средства)

        sb.AppendLine($":59:/{ed.PayeePersonalAcc}")
            .AppendLineIf(ed.PayeeINN, $"INN{ed.PayeeINN}{ed.PayeeKPP.AddKPPNotEmptyNorZeros()}")
            .AppendLineIf(ed.PayeeName, Lat(ed.PayeeName)!.Div35(Config.SwiftNameLimit));

        // Информация о платеже (4*35x + :72:/NZP/)
        // При недостаточной размерности поля допускается продолжение информации о назначении платежа в поле 72 с кодовым словом /NZP/.
        // Суммарный объем информации о назначении платежа, содержащийся в поле 70 и в поле 72 с кодовым словом /NZP/,
        // после транслитерации не должен превышать 210 знаков.
        //
        // В соответствии с Инструкцией Банка России N117-И от 15.06.2004 г. при составлении платежных инструкций для осуществления расчетов
        // в российских рублях по валютным операциям в поле 70 должен быть указан код вида валютной операции, и может указываться номер паспорта сделки.
        // Перед значением кода вида валютной операции проставляется разделительный символ VO, а перед номером паспорта сделки, если он указывается,
        // - разделительный символ PS. Разделительные символы VO, PS указываются прописными латинскими буквами.
        // Эта информация должна быть заключена в фигурные скобки и помещена в начале поля «Назначение платежа» в следующем виде:
        // {VO<код>[PS <номер паспорта сделки>]}. Пробелы внутри фигурных скобок не допускаются.
        // Однако, символы фигурных скобок не могут содержаться в тексте сообщений SWIFT. Поэтому применяется следующее исключение из правил транслитерации.
        // Применяется только для поля 70 в сообщениях SWIFT МТ101 и МТ103 и для поля 72 с кодом /NZP/ МТ202 в связи с Инструкцией Банка России N117-И от 15.06.2004г.
        // На основании разъяснений Банка России символы фигурных скобок, ограничивающие закодированную информацию валютной операции в поле «Назначение платежа»
        // платежного поручения, процессом транслитерации с кириллицы на латиницу отображаются круглыми скобками в соответствующем поле(поле 70) сообщения SWIFT.
        // А при обратной транслитерации круглые скобки отображаются символами фигурных скобок. Условием для этого является наличие следующей комбинации,
        // расположенной, начиная с первой позиции поля 70: апостроф - круглая скобка - VO<код>[PS < номер паспорта сделки >] - круглая скобка - апостроф.
        // '(VO10010)'OPLATA PO DOGOVORU
        // '(VO10040PS04060001/0001/0000/1/0)'OPLATA PO DOGOVORU


        int purposeLine = 0;
        ReadOnlySpan<char> purpose = string.Empty;

        if (ed.Purpose != null)
        {
            purpose = Lat(ed.Purpose)!.Prepare35();

            if (Config.SwiftPurposeField == "70")
            {
                sb.Append(":70:");

                for (; purposeLine < 4; purposeLine++)
                {
                    var s35 = purpose.Slice(purposeLine * 35, 35).TrimEnd();

                    if (s35.Length == 0) break;

                    sb.AppendLine(s35.ToString());
                }
            }
        }

        // Детали расходов
        // Все расходы по данной операции относятся на счет Плательщика

        sb.AppendLine(":71A:OUR");

        // Информация Отправителя Получателю (6*35x)
        // Используются следующие кодовые слова:

        // /RPP/ — Реквизиты расчетного документа в соответствии с требованиями Банка России.
        // "Номер расчетного документа.Дата расчетного документа в формате ГГММДД.Очередность платежа.Вид платежа[[.Дата проведения платежа].Вид операции]"
        // Данное подполе может иметь значение
        // 01 – платежное поручение (ED101, default)
        // 02 – платежное требование (ED103)
        // 06 – инкассовое поручение (ED104)
        // 16 – платежный ордер (ED105)
        // Если подполе не используется, то по умолчанию считается, что документ представляет собой платежное поручение.
        // ?? ed.PaytKind ?? Вид платежа: Используется один из следующих кодов:
        // POST - почтой
        // TELG – телеграфом
        // ELEK – электронными средствами связи
        // BESP – по системе БЭСП (Использование сервиса срочного перевода является обязательным для платежей на сумму от 100 000 000 рублей!).

        // /RPO/ — Реквизиты платежного ордера
        // "Номер частичного платежа.Шифр расчетного документа.Номер расчетного документа.Дата расчетного документа.Сумма остатка платежа"
        // Шифр расчетного документа может иметь значение:
        // 01 – платежное поручение
        // 02 – платежное требование
        // 06 – инкассовое поручение

        // /DAS/ — Даты из расчетного документа
        // Все подполя этого кодового поля представляют собой даты в формате ГГММДД
        // "Дата списания со счета плательщика (71).Поступило в банк плательщика (62).Отметка банка получателя (48).Дата помещения в картотеку (63)"
        // 1) Номера полей расчетного документа Банка России приведены согласно Положению о безналичных расчетах в Российской Федерации №2-П от 3 октября 2002г.
        // 2) В случае, когда формат исходного расчетного документа не содержит каких - либо из указанных полей, либо информация в них отсутствует,
        // в сообщении SWIFT соответствующие подполя заполняются символом нуль.
        // 3) Если исходный расчетный документ не содержит информации ни в одном из указанных полей, то в сообщении SWIFT кодовое слово DAS не используется.
        // /NZP/ Продолжение поля 70 Назначение платежа. Суммарный объем информации о назначении платежа, содержащийся в поле 70 и в поле 72 с кодовым словом /NZP/,
        // после транслитерации не должен превышать 210 знаков.

        string paytKind = ed.PaytKind != null || sum.Length > 10
            ? "BESP"
            : "ELEK";

        string transKind = ed.TransKind == "01"
            ? string.Empty
            : $".{ed.TransKind}";

        sb.Append(":72:")
            // Реквизиты расчетного документа
            .AppendLine($"/RPP/{ed.AccDocNo}.{SwiftDate(ed.AccDocDate)}.{ed.Priority}.{paytKind}{transKind}") //.{SwiftDate(ed.ChargeOffDate)}.{ed.TransKind}")
            // Даты из расчетного документа
            .AppendLine($"/DAS/{SwiftDate(ed.ChargeOffDate)}.{SwiftDate(ed.ReceiptDate)}.000000.000000");

        //TODO Реквизиты платежного ордера
        // .AppendLine($"/RPO/...

        // Уникальный Идентификатор Платежа (УИН)
        sb.AppendLineIf(ed.Tax, $"/UIP/{ed.PaymentID ?? "0"}");

        // Назначение платежа (продолжение поля 70)
        if (!purpose.IsEmpty)
        {
            string nzp = "/NZP/";

            for (; purposeLine < 6; purposeLine++)
            {
                var s35 = purpose.Slice(purposeLine * 35, 35).TrimEnd();

                if (s35.Length == 0) break;

                sb.Append(nzp).AppendLine(s35.ToString());
                nzp = "//";
            }
        }

        //TODO Дополнительная информация для получателя сообщения
        // .AppendLine($"/REC/...

        if (ed.Tax)
        {
            sb.Append(":77B:")
                .AppendLine($"/N10/{Lat(ed.TaxPaytKind) ?? "0"}/N4/{Lat(ed.CBC) ?? "0"}")
                .AppendLine($"/N5/{Lat(ed.OKATO) ?? "0"}/N6/{Lat(ed.PaytReason) ?? "0"}/N7/{Lat(ed.TaxPeriod) ?? "0"}")
                .AppendLine($"/N8/{Lat(ed.DocNo) ?? "0"}/N9/{Lat(ed.DocDate) ?? "0"}");
        }

        #endregion SWIFT TEXT

        sb.Append("-}")

            // Trailers
            .Append("{5:}"); // Block 5 identifier : NO \n before EOF!!! 

        return sb.ToString();
    }

    /// <summary>
    /// SWIFT-RUR 6: MT202 ("ОБЩИЙ МЕЖБАНКОВСКИЙ ПЕРЕВОД")
    /// </summary>
    /// <param name="ed"></param>
    /// <returns></returns>
    public static string ToStringMT202(this ED100 ed) //TODO !!!
    {
        string sum = ed.Sum; // save for BESP if over 100 000 000.00
        var (Num, Id) = SwiftID.ID(ed);

        //ed.Translit(); //TODO !!!

        // Block Structure

        var sb = new StringBuilder();
        sb
            // Basic header
            .Append("{1:F01") // Block 1 identifier : Application identifier
            .Append(Config.BankSWIFT) // Service identifier
            .Append("AXXX") // Logical terminal address
            .Append(Num) // Session number (shorten for {1: })
            .Append('}')

            // Application header
            .Append("{2:I202") // Block 2 identifier : In, MT202
            .Append(Config.CorrSWIFT.PadRight(12, 'X')) // Destination address with default Logical terminal address XXX
            .Append("N}") // Message priority (Normal)

            // User header
            .Append("{3:{113:RUR6}{121:") // Block 3 identifier : Version
            .Append(Guid.NewGuid()) // uuid v4
            .Append("}}")

            // Text
            .AppendLine("{4:"); // Block 4 identifier : 

        #region SWIFT TEXT

        // Референс Отправителя (16x)

        sb.AppendLine($":20:+{Id}");

        // Связанный референс (16x)

        sb.AppendLine(":21:NONREF");

        // Дата валютирования/Валюта/Сумма межбанковского расчета

        sb.AppendLine($":32A:{ed.ChargeOffDate}RUB{ed.Sum}");

        // Банк-плательщик

        sb.AppendLine($":52D://RU{ed.PayerBIC}.{ed.PayerCorrespAcc}") // OurBIC.OurCorrACC
            .AppendLine(Lat("АО Сити Инвест Банк"))
            .AppendLine(Lat("г.Санкт-Петербург"));

        // Корреспондент Отправителя (реквизиты счета, который должен быть использован при исполнении платежных инструкций)
        // Если Отправитель и Получатель сообщения обслуживают рублевые счета друг друга, и необходимо определить,
        // будет ли производиться зачисление или списание средств, в данном поле после слеша “/” указывается код операции
        // (С - кредитование или D - дебетование) и далее следует еще один слеш “/”и номер соответствующего счета.
        // При наличии у Отправителя и Получателя единственного прямого корреспондентского счета в рублях данное поле не используется,
        // если только иное особо не оговорено в двустороннем соглашении.

        sb.AppendLine($":53B:/D/{Config.CorrAccount}"); // Корсчет нашего банка в том банке

        // Банк-Посредник (опционально в РФ, предпочтительна опция A, а не D)
        // В этом поле определяется сторона между Получателем сообщения и Банком Бенефициара, через которую должна быть проведена операция.
        //sb.AppendLine($":56D:/{ed.PayeePersonalAcc}") //??
        //.AppendLine(SwiftTranslit.Lat("ББР"))
        //.AppendLine(SwiftTranslit.Lat("г.Санкт-Петербург"));

        // Банк Бенефициара
        // (финансовая организация, обслуживающая счет клиента-бенефициара - в том случае, если она отлична от Получателя сообщения)

        sb.AppendLine($":57D://RU{ed.PayeeBIC}{ed.PayeeCorrespAcc.AddNotEmpty()}");

        // Указание местонахождения Банка бенефициара при проведении платежа через расчетную систему Банка России обязательно.
        // Указание адреса Банка бенефициара является необязательным, при наличии, отделяется запятой от наименования.

        // 4*32x
        //.AppendLine(SwiftTranslit.Lat("Какой-то банк получателя,")) // Надо ли брать из Справочника БИК?
        //.AppendLine(SwiftTranslit.Lat("г.Город"));

        //sb.AppendLine("BANK"); //TODO Название банка и его город по его БИК (пока просто заглушка)

        // Банк-Бенефициар

        sb.AppendLine($":58D:/{ed.PayeePersonalAcc}")
            .AppendLineIf(ed.PayeeINN != null, $"INN{ed.PayeeINN}{ed.PayeeKPP.AddKPPNotEmpty()}");

        if (ed.PayeeName != null)
        {
            var s = ed.PayeeName.Prepare35();

            for (int i = 0; i < 3; i++)
            {
                var s35 = s.Slice(i * 35, 35).TrimEnd();

                if (s35.Length == 0) break;

                sb.AppendLine(s35.ToString());
            }
        }

        // Информация о платеже (4*35x + :72:/NZP/)
        // При недостаточной размерности поля допускается продолжение информации о назначении платежа в поле 72 с кодовым словом /NZP/.
        // Суммарный объем информации о назначении платежа, содержащийся в поле 70 и в поле 72 с кодовым словом /NZP/,
        // после транслитерации не должен превышать 210 знаков.
        //
        // В соответствии с Инструкцией Банка России N117-И от 15.06.2004 г. при составлении платежных инструкций для осуществления расчетов
        // в российских рублях по валютным операциям в поле 70 должен быть указан код вида валютной операции, и может указываться номер паспорта сделки.
        // Перед значением кода вида валютной операции проставляется разделительный символ VO, а перед номером паспорта сделки, если он указывается,
        // - разделительный символ PS. Разделительные символы VO, PS указываются прописными латинскими буквами.
        // Эта информация должна быть заключена в фигурные скобки и помещена в начале поля «Назначение платежа» в следующем виде:
        // {VO<код>[PS <номер паспорта сделки>]}. Пробелы внутри фигурных скобок не допускаются.
        // Однако, символы фигурных скобок не могут содержаться в тексте сообщений SWIFT. Поэтому применяется следующее исключение из правил транслитерации.
        // Применяется только для поля 70 в сообщениях SWIFT МТ101 и МТ103 и для поля 72 с кодом /NZP/ МТ202 в связи с Инструкцией Банка России N117-И от 15.06.2004г.
        // На основании разъяснений Банка России символы фигурных скобок, ограничивающие закодированную информацию валютной операции в поле «Назначение платежа»
        // платежного поручения, процессом транслитерации с кириллицы на латиницу отображаются круглыми скобками в соответствующем поле(поле 70) сообщения SWIFT.
        // А при обратной транслитерации круглые скобки отображаются символами фигурных скобок. Условием для этого является наличие следующей комбинации,
        // расположенной, начиная с первой позиции поля 70: апостроф - круглая скобка - VO<код>[PS < номер паспорта сделки >] - круглая скобка - апостроф.
        // '(VO10010)'OPLATA PO DOGOVORU
        // '(VO10040PS04060001/0001/0000/1/0)'OPLATA PO DOGOVORU

        // Информация Отправителя Получателю (6*35x)
        // Используются следующие кодовые слова:

        // /RPP/ — Реквизиты расчетного документа в соответствии с требованиями Банка России.
        // "Номер расчетного документа.Дата расчетного документа в формате ГГММДД.Очередность платежа.Вид платежа[[.Дата проведения платежа].Вид операции]"
        // Данное подполе может иметь значение
        // 01 – платежное поручение (ED101, default)
        // 02 – платежное требование (ED103)
        // 06 – инкассовое поручение (ED104)
        // 16 – платежный ордер (ED105)
        // Если подполе не используется, то по умолчанию считается, что документ представляет собой платежное поручение.
        // ?? ed.PaytKind ?? Вид платежа: Используется один из следующих кодов:
        // POST - почтой
        // TELG – телеграфом
        // ELEK – электронными средствами связи
        // BESP – по системе БЭСП (Использование сервиса срочного перевода является обязательным для платежей на сумму от 100 000 000 рублей!).

        // /RPO/ — Реквизиты платежного ордера
        // "Номер частичного платежа.Шифр расчетного документа.Номер расчетного документа.Дата расчетного документа.Сумма остатка платежа"
        // Шифр расчетного документа может иметь значение:
        // 01 – платежное поручение
        // 02 – платежное требование
        // 06 – инкассовое поручение

        // /DAS/ — Даты из расчетного документа
        // Все подполя этого кодового поля представляют собой даты в формате ГГММДД
        // "Дата списания со счета плательщика (71).Поступило в банк плательщика (62).Отметка банка получателя (48).Дата помещения в картотеку (63)"
        // 1) Номера полей расчетного документа Банка России приведены согласно Положению о безналичных расчетах в Российской Федерации №2-П от 3 октября 2002г.
        // 2) В случае, когда формат исходного расчетного документа не содержит каких - либо из указанных полей, либо информация в них отсутствует,
        // в сообщении SWIFT соответствующие подполя заполняются символом нуль.
        // 3) Если исходный расчетный документ не содержит информации ни в одном из указанных полей, то в сообщении SWIFT кодовое слово DAS не используется.
        // /NZP/ Продолжение поля 70 Назначение платежа. Суммарный объем информации о назначении платежа, содержащийся в поле 70 и в поле 72 с кодовым словом /NZP/,
        // после транслитерации не должен превышать 210 знаков.

        string paytKind = ed.PaytKind != null || sum.Length > 10
            ? "BESP"
            : "ELEK";

        string transKind = ed.TransKind == "01"
            ? string.Empty
            : $".{ed.TransKind}";

        sb.Append(":72:")
            // Реквизиты расчетного документа
            .AppendLine($"/RPP/{ed.AccDocNo}.{ed.AccDocDate}.{ed.Priority}.{paytKind}{transKind}") //.{ed.ChargeOffDate}.{ed.TransKind}")
                                                                                                   // Даты из расчетного документа
            .AppendLine($"/DAS/{ed.ChargeOffDate}.{ed.ReceiptDate}.000000.000000");

        //TODO Реквизиты платежного ордера
        // .AppendLine($"/RPO/...

        // Назначение платежа (в поле 72 только)
        if (ed.Purpose != null)
        {
            string nzp = "/NZP/"; //TODO строка 35 - это с /NZP/ или без? есть разночтения в разных SWIFT-RUR...
            ReadOnlySpan<char> purpose = ed.Purpose.Prepare35();

            for (int purposeLine = 0; purposeLine < 6; purposeLine++)
            {
                var s35 = purpose.Slice(purposeLine * 35, 35).TrimEnd();

                if (s35.Length == 0) break;

                sb.Append(nzp).AppendLine(s35.ToString());
                nzp = "//";
            }
        }

        //TODO Дополнительная информация для получателя сообщения
        // .AppendLine($"/REC/...

        #endregion SWIFT TEXT

        sb.Append("-}")

            // Trailers
            .Append("{5:}"); // Block 5 identifier : NO \n before EOF!!!

        return sb.ToString();
    }

    //private static void Translit(this ED100 ed)
    //{
    //    if (ed.Lat) return;

    //    ed.PayerName = Lat(ed.PayerName);
    //    ed.PayeeName = Lat(ed.PayeeName);

    //    ed.Sum = SwiftSum(ed.Sum);

    //    ed.ChargeOffDate = SwiftDate(ed.ChargeOffDate)!;
    //    ed.EDDate = SwiftDate(ed.EDDate);
    //    ed.FileDate = SwiftDate(ed.FileDate);
    //    ed.ReceiptDate = SwiftDate(ed.ReceiptDate)!;
    //    ed.AccDocDate = SwiftDate(ed.AccDocDate);
    //    ed.Purpose = Lat(ed.Purpose);

    //    //if (ed.Tax)

    //    ed.CBC = Lat(ed.CBC);
    //    ed.DocDate = Lat(ed.DocDate);
    //    ed.DocNo = Lat(ed.DocNo);
    //    ed.OKATO = Lat(ed.OKATO);
    //    ed.PaytReason = Lat(ed.PaytReason);
    //    ed.TaxPeriod = Lat(ed.TaxPeriod);
    //    ed.TaxPaytKind = Lat(ed.TaxPaytKind);

    //    ed.Lat = true;
    //}
}
