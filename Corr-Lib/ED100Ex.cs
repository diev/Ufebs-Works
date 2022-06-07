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

using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace CorrLib;

public static class ED100Ex
{
    public static void Load(this ED100 ed, XElement xElement)
    {
        ed.EDType = xElement.Name.LocalName;

        ed.ChargeOffDate = xElement.Attribute("ChargeOffDate")?.Value;
        ed.EDAuthor = xElement.Attribute("EDAuthor")?.Value;
        ed.EDDate = xElement.Attribute("EDDate")?.Value;
        ed.EDNo = xElement.Attribute("EDNo")?.Value;
        ed.PaymentID = xElement.Attribute("PaymentID")?.Value;
        ed.PaymentPrecedence = xElement.Attribute("PaymentPrecedence")?.Value;
        ed.PaytKind = xElement.Attribute("PaytKind")?.Value;
        ed.Priority = xElement.Attribute("Priority")?.Value;
        ed.ReceiptDate = xElement.Attribute("ReceiptDate")?.Value;
        ed.Sum = xElement.Attribute("Sum")?.Value;
        ed.SystemCode = xElement.Attribute("SystemCode")?.Value;
        ed.TransKind = xElement.Attribute("TransKind")?.Value;
        ed.Xmlns = xElement.Attribute("xmlns")?.Value;

        foreach (var e in xElement.Elements())
        {
            switch (e.Name.LocalName)
            {
                case "AccDoc":
                    ed.AccDocDate = e.Attribute("AccDocDate")?.Value;
                    ed.AccDocNo = e.Attribute("AccDocNo")?.Value;
                    break;

                case "Payer":
                    ed.PayerINN = e.Attribute("INN")?.Value;
                    ed.PayerKPP = e.Attribute("KPP")?.Value;
                    ed.PayerPersonalAcc = e.Attribute("PersonalAcc")?.Value;

                    foreach (var p in e.Elements())
                    {
                        switch (p.Name.LocalName)
                        {
                            case "Name":
                                ed.PayerName = p.Value.Replace("  ", " ");
                                break;

                            case "Bank":
                                ed.PayerBIC = p.Attribute("BIC")?.Value;
                                ed.PayerCorrespAcc = p.Attribute("CorrespAcc")?.Value;
                                break;

                            default:
                                break;
                        }
                    }
                    break;

                case "Payee":
                    ed.PayeeINN = e.Attribute("INN")?.Value;
                    ed.PayeeKPP = e.Attribute("KPP")?.Value;
                    ed.PayeePersonalAcc = e.Attribute("PersonalAcc")?.Value;

                    foreach (var p in e.Elements())
                    {
                        switch (p.Name.LocalName)
                        {
                            case "Name":
                                ed.PayeeName = p.Value.Replace("  ", " ");
                                break;

                            case "Bank":
                                ed.PayeeBIC = p.Attribute("BIC")?.Value;
                                ed.PayeeCorrespAcc = p.Attribute("CorrespAcc")?.Value;
                                break;

                            default:
                                break;
                        }
                    }
                    break;

                case "Purpose":
                    ed.Purpose = e.Value.Replace("  ", " ");
                    break;

                case "DepartmentalInfo":
                    ed.CBC = e.Attribute("CBC")?.Value;
                    ed.DocDate = e.Attribute("DocDate")?.Value;
                    ed.DocNo = e.Attribute("DocNo")?.Value;
                    ed.DrawerStatus = e.Attribute("DrawerStatus")?.Value;
                    ed.OKATO = e.Attribute("OKATO")?.Value;
                    ed.PaytReason = e.Attribute("PaytReason")?.Value;
                    ed.TaxPeriod = e.Attribute("TaxPeriod")?.Value;
                    ed.TaxPaytKind = e.Attribute("TaxPaytKind")?.Value;
                    break;

                default:
                    break;
            }
        }
    }

    public static void WriteXML(this ED100 ed, XmlWriter writer)
    {
        // ED101
        writer.WriteStartElement(ed.EDType, ed.Xmlns);

        writer.WriteAttributeString("ChargeOffDate", ed.ChargeOffDate); //71
        writer.WriteAttributeString("EDAuthor", ed.EDAuthor);
        writer.WriteAttributeString("EDDate", ed.EDDate);
        writer.WriteAttributeString("EDNo", ed.EDNo);
        writer.WriteAttributeString("PaymentID", ed.PaymentID); //22
        writer.WriteAttributeString("PaymentPrecedence", ed.PaymentPrecedence);
        writer.WriteAttributeString("PaytKind", ed.PaytKind); //5
        writer.WriteAttributeString("Priority", ed.Priority); //21
        writer.WriteAttributeString("ReceiptDate", ed.ReceiptDate); //62
        writer.WriteAttributeString("Sum", ed.Sum); //7
        writer.WriteAttributeString("SystemCode", ed.SystemCode);
        writer.WriteAttributeString("TransKind", ed.TransKind); //18

        // AccDoc
        writer.WriteStartElement("AccDoc");
        writer.WriteAttributeString("AccDocDate", ed.AccDocDate); //4
        writer.WriteAttributeString("AccDocNo", ed.AccDocNo); //3
        writer.WriteEndElement(); // AccDoc

        // Payer
        writer.WriteStartElement("Payer");
        writer.WriteAttributeString("INN", ed.PayerINN); //60

        if (ed.PayerINN != null && ed.PayerINN.Length != 12)
        {
            writer.WriteAttributeString("KPP", ed.PayerKPP); //102
        }

        writer.WriteAttributeString("PersonalAcc", ed.PayerPersonalAcc);  //9

        writer.WriteElementString("Name", ed.PayerName); //8

        writer.WriteStartElement("Bank");
        writer.WriteAttributeString("BIC", ed.PayerBIC); //11
        writer.WriteAttributeString("CorrespAcc", ed.PayerCorrespAcc); //12
        writer.WriteEndElement(); // Bank
        writer.WriteEndElement(); // Payer

        // Payee
        writer.WriteStartElement("Payee");
        writer.WriteAttributeString("INN", ed.PayeeINN); //61
        writer.WriteAttributeString("KPP", ed.PayeeKPP); //103
        writer.WriteAttributeString("PersonalAcc", ed.PayeePersonalAcc); //17

        writer.WriteElementString("Name", ed.PayeeName); //16

        writer.WriteStartElement("Bank");
        writer.WriteAttributeString("BIC", ed.PayeeBIC); //14
        writer.WriteAttributeString("CorrespAcc", ed.PayeeCorrespAcc); //15
        writer.WriteEndElement(); // Bank
        writer.WriteEndElement(); // Payee

        // Purpose
        writer.WriteElementString("Purpose", ed.Purpose); //24

        if (ed.Tax)
        {
            // DepartmentalInfo
            writer.WriteStartElement("DepartmentalInfo");

            writer.WriteAttributeString("CBC", ed.CBC); //104
            writer.WriteAttributeString("DocDate", ed.DocDate); //109
            writer.WriteAttributeString("DocNo", ed.DocNo); //108
            writer.WriteAttributeString("DrawerStatus", ed.DrawerStatus); //101
            writer.WriteAttributeString("OKATO", ed.OKATO); //105
            writer.WriteAttributeString("PaytReason", ed.PaytReason); //106
            writer.WriteAttributeString("TaxPeriod", ed.TaxPeriod); //107

            if (ed.TaxPaytKind != null && ed.TaxPaytKind != "0") //fix
            {
                writer.WriteAttributeString("TaxPaytKind", ed.TaxPaytKind); //110
            }

            writer.WriteEndElement(); // DepartmentalInfo
        }

        writer.WriteEndElement(); // ED101
        writer.Flush();
    }

    /// <summary>
    /// SWIFT-RUR 6: MT103 ("ОДНОКРАТНОЕ ЗАЧИСЛЕНИЕ КЛИЕНТСКИХ СРЕДСТВ")
    /// </summary>
    /// <param name="ed"></param>
    /// <returns></returns>
    public static string ToSWIFT(this ED100 ed)
    {
        var sb = new StringBuilder();

        /*
        {1:F01CITVRU2PAXXX0000036558}{2:I103AVTBRUMMXXXXN}{3:{113:RUR6}{121:de302129-4181-49b0-8053-0546a8c0af58}}{4:
        :20:+220428-0853F103
        :23B:CRED
        :32A:220428RUB2000,
        :50K:/40703810000000001470
        INN7803018248.KPP784101001
        ASSOCIACIa VEKTOR ROGA I KOPYTA TOV
        ARIqESTVO SOBSTVENNIKOV JILXa I eKS
        PLUATANTOV PODZEMNYH I VODNYH ARTER
        :52A:CITVRU2P
        :53B:/D/30109810800010001378
        :57D://RU044030653.30101810500000000653
        SEVERO-ZAPADNYi BANK PAO SBERBANK
        G.SANKT-PETERBURG
        :59:/40817810555004092061
        INN780105207066.KPP000000000
        MEJOV ALEKSEi ALEKSANDROVIc PREDSTA
        VITELX RAZUMNOi JIZNI NA PLANETE ZE
        MLa I SPUINIKAH uPITERA I DRUGIH NA
        :70:DLa ZAcISLENIa NA ScET MEJOVA ALEKS
        Ea ALEKSANDROVIcA I ZARABOTNAa PLAT
        A ZA MAi 2022 G.ZA VYPOLNENNUu RAB
        OTU PO PEREKLADKE TRUB I VYKAPYVANI
        :71A:OUR
        :72:/RPP/31.220428.3
        /DAS/220428.220427.000000.000000
        /NZP/u I ZAKAPYVANIu KANAV V GRUNT
        //E NA GORE. SUMMA 2000-00 BEZ NALO
        //GA (NDS)
        /DAS/220428.220427.000000.000000
        -}{5:}
        */

        string date = SwiftTranslit.XDate(ed.EDDate);
        string docdate = SwiftTranslit.XDate(ed.AccDocDate);
        string charge = SwiftTranslit.XDate(ed.ChargeOffDate);

        sb.Append("{1:F01CITVRU2PAXXX0000036558}")
            .Append("{2:I103AVTBRUMMXXXXN}")
            .Append("{3:{113:RUR6}{121:de302129-4181-49b0-8053-0546a8c0af58}}")
            .AppendLine("{4:");

        // Референс Отправителя (16x)
        sb.AppendLine($":20:+{date}-{ed.EDNo}"); //..F103 is over 16x

        // Код банковской операции
        sb.AppendLine(":23B:CRED");

        if (ed.Tax)
        {
            // Код типа операции
            sb.AppendLine($":26T:S{ed.DrawerStatus}");
        }

        // Дата валютирования/Валюта/Сумма межбанковского расчета
        sb.AppendLine($":32A:{charge}RUB{SwiftTranslit.XSum(ed.Sum)}");

        // Плательщик
        sb.AppendLine($":50K:/{ed.PayerPersonalAcc}"); // или :50F: с адресом и страной

        if (ed.PayerINN != null)
        {
            sb.Append($"INN{ed.PayerINN}"); // или KIO

            if (ed.PayerKPP != null)
            {
                sb.Append($".KPP{ed.PayerKPP}");
            }

            sb.AppendLine();
        }

        // (3*35x!)
        // ООО "Название юрлица"
        // или при отсутствии ИНН у физлица:
        // Ф.И.О. полностью//адрес места жительства (регистрации) или места пребывания//
        string s = ed.PayerName.L();
        int n = 0;

        while (s.Length > 35 && ++n <= 3) // не более 3 строк - остальное отбрасывается
        {
            sb.AppendLine(s[..35]);
            s = s.Remove(0, 35);
        }

        if (s.Length > 0 && ++n <= 3) // если остаток влезает
        {
            sb.AppendLine(s);
        }

        // Банк Плательщика
        // (финансовая организация, обслуживающая Плательщика, в тех случаях, когда она отлична от Отправителя)
        //sb.AppendLine(":52A:CITVRU2P");
        sb.AppendLine($":52D://RU{ed.PayerBIC}.{ed.PayerCorrespAcc}"); // OurBIC.OurCorrACC
            //.AppendLine(SwiftTranslit.Lat("АО Сити Инвест Банк"))
            //.AppendLine(SwiftTranslit.Lat("г.Санкт-Петербург"));

        // Корреспондент Отправителя (реквизиты счета, который должен быть использован при исполнении платежных инструкций)
        // Если Отправитель и Получатель сообщения обслуживают рублевые счета друг друга, и необходимо определить,
        // будет ли производиться зачисление или списание средств, в данном поле после слеша “/” указывается код операции
        // (С - кредитование или D - дебетование) и далее следует еще один слеш “/”и номер соответствующего счета.
        // При наличии у Отправителя и Получателя единственного прямого корреспондентского счета в рублях данное поле не используется,
        // если только иное особо не оговорено в двустороннем соглашении.
        sb.AppendLine($":53B:/D/{ed.PayeePersonalAcc}"); //??

        // Банк-Посредник (опционально в РФ, предпочтительна опция A, а не D)
        // В этом поле определяется сторона между Получателем сообщения и Банком Бенефициара, через которую должна быть проведена операция.
        //sb.AppendLine($":56D:/{ed.PayeePersonalAcc}") //??
            //.AppendLine(SwiftTranslit.Lat("ББР"))
            //.AppendLine(SwiftTranslit.Lat("г.Санкт-Петербург"));

        // Банк Бенефициара
        // (финансовая организация, обслуживающая счет клиента-бенефициара - в том случае, если она отлична от Получателя сообщения)
        sb.AppendLine($":57D://RU{ed.PayeeBIC}.{ed.PayeeCorrespAcc}");
        //.AppendLine(SwiftTranslit.Lat("АО Сити Инвест Банк"))
        //.AppendLine(SwiftTranslit.Lat("г.Санкт-Петербург"));

        // Бенефициар
        // (клиент, которому будут выплачены средства)
        sb.AppendLine($":59:/{ed.PayeePersonalAcc}");

        if (ed.PayeeINN != null)
        {
            sb.Append($"INN{ed.PayeeINN}");

            if (ed.PayeeKPP != null)
            {
                sb.Append($".KPP{ed.PayeeKPP}");
            }

            sb.AppendLine();
        }

        s = ed.PayeeName.L();
        n = 0;

        while (s.Length > 35 && ++n <= 3) // не более 3 строк - остальное отбрасывается
        {
            sb.AppendLine(s[..35]);
            s = s.Remove(0, 35);
        }

        if (s.Length > 0 && ++n <= 3) // если остаток влезает
        {
            sb.AppendLine(s);
        }

        // Информация о платеже (4*35x + :72:/NZP/)
        // При недостаточной размерности поля допускается продолжение информации о назначении платежа в поле 72 с кодовым словом /NZP/.
        // Суммарный объем информации о назначении платежа, содержащийся в поле 70 и в поле 72 с кодовым словом /NZP/,
        // после транслитерации не должен превышать 210 знаков.
        //
        // В соответствии с Инструкцией Банка России №117-И от 15.06.2004 г. при составлении платежных инструкций для осуществления расчетов
        // в российских рублях по валютным операциям в поле 70 должен быть указан код вида валютной операции, и может указываться номер паспорта сделки.
        // Перед значением кода вида валютной операции проставляется разделительный символ VO, а перед номером паспорта сделки, если он указывается,
        // - разделительный символ PS. Разделительные символы VO, PS указываются прописными латинскими буквами.
        // Эта информация должна быть заключена в фигурные скобки и помещена в начале поля «Назначение платежа» в следующем виде:
        // { VO<код>[PS < номер паспорта сделки >]}. Пробелы внутри фигурных скобок не допускаются.
        // Однако, символы фигурных скобок не могут содержаться в тексте сообщений SWIFT. Поэтому применяется следующее исключение из правил транслитерации.
        // Применяется только для поля 70 в сообщениях SWIFT МТ101 и МТ103 и для поля 72 с кодом /NZP/ МТ202 в связи с Инструкцией Банка России №117-И от 15.06.2004г.
        // На основании разъяснений Банка России символы фигурных скобок, ограничивающие закодированную информацию валютной операции в поле «Назначение платежа»
        // платежного поручения, процессом транслитерации с кириллицы на латиницу отображаются круглыми скобками в соответствующем поле(поле 70) сообщения SWIFT.
        // А при обратной транслитерации круглые скобки отображаются символами фигурных скобок. Условием для этого является наличие следующей комбинации,
        // расположенной, начиная с первой позиции поля 70: апостроф - круглая скобка - VO<код>[PS < номер паспорта сделки >] - круглая скобка - апостроф.
        // ‘(VO10010)’OPLATA PO DOGOVORU
        // ‘(VO10040PS04060001/0001/0000/1/0)’OPLATA PO DOGOVORU
        sb.Append(":70:");
        s = ed.Purpose.L();
        n = 0;

        while (s.Length > 35 && ++n <= 4) // не более 4 строк - остальное идет в поле 72
        {
            sb.AppendLine(s[..35]);
            s = s.Remove(0, 35);
        }

        if (s.Length > 0 && ++n <= 4) // если остаток влезает
        {
            sb.AppendLine(s);
            s = string.Empty; // ничего не осталось продолжать
        }

        // Детали расходов
        // Все расходы по данной операции относятся на счет Плательщика
        sb.AppendLine($":71A:OUR");

        // Информация Отправителя Получателю (6*35x)
        // Используются следующие кодовые слова:

        // /RPP/ — Реквизиты расчетного документа в соответствии с требованиями Банка России.
        // "Номер расчетного документа.Дата расчетного документа в формате ГГММДД.Очередность платежа.Вид платежа.Дата проведения платежа.Вид операции"
        // Данное подполе может иметь значение
        // 01 – платежное поручение
        // 02 – платежное требование
        // 06 – инкассовое поручение
        // 16 – платежный ордер
        // Если подполе не используется, то по умолчанию считается, что документ представляет собой платежное поручение.
        // ?? ed.PaytKind ?? Вид платежа: Используется один из следующих кодов:
        // POST - почтой
        // TELG – телеграфом
        // ELEK – электронными средствами связи
        // BESP – по системе БЭСП.

        // /RPO/ — Реквизиты платежного ордера
        // "Номер частичного платежа.Шифр расчетного документа.Номер расчетного документа.Дата расчетного документа.Сумма остатка платежа"
        // Шифр расчетного документа» и может иметь значение:
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

        sb.Append(":72:")
            .AppendLine($"/RPP/{ed.AccDocNo}.{docdate}.{ed.Priority}.ELEK.{charge}.{ed.TransKind}") // ??
            .AppendLine($"/DAS/{date}.{date}.000000.000000"); // ??

        if (s.Length > 0)
        {
            sb.Append("/NZP/");
            n = 0;

            while (s.Length > 35)
            {
                if (++n > 1)
                {
                    sb.Append("//");
                }

                sb.AppendLine(s[..35]);
                s = s.Remove(0, 35);
            }

            if (s.Length > 0) // остаток
            {
                if (++n > 1)
                {
                    sb.Append("//");
                }

                sb.AppendLine(s);
            }
        }

        if (ed.Tax)
        {
            sb.AppendLine($":77B:/N10/{ed.TaxPaytKind.L()}/N4/{ed.CBC}") // ??
                .AppendLine($"/N5/{ed.OKATO}/N6/{ed.PaytReason.L()}/N7/{ed.TaxPeriod.L()}")
                .AppendLine($"/N8/{ed.DocNo}/N9/{ed.DocDate}");
        }

        sb.AppendLine("-}{5:}");

        return sb.ToString();
    }
}
