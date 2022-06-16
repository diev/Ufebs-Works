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

using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace CorrLib;

public static class ED100Ex
{
    public static void Load(this ED100 ed, XElement x)
    {
        ed.EDType = x.Name.LocalName; // required

        ed.ChargeOffDate = x.Attribute("ChargeOffDate")?.Value;
        ed.EDAuthor = x.Attribute("EDAuthor")!.Value; // required
        ed.EDDate = x.Attribute("EDDate")!.Value; // required
        ed.EDNo = x.Attribute("EDNo")!.Value; // required
        ed.FileDate = x.Attribute("FileDate")?.Value;
        ed.OperationID = x.Attribute("OperationID")?.Value;
        ed.PaymentID = x.Attribute("PaymentID")?.Value;
        ed.PaymentPrecedence = x.Attribute("PaymentPrecedence")!.Value; // required
        ed.PaytKind = x.Attribute("PaytKind")?.Value;
        ed.Priority = x.Attribute("Priority")!.Value; // required
        ed.ReceiptDate = x.Attribute("ReceiptDate")?.Value;
        ed.Sum = x.Attribute("Sum")!.Value; // required
        ed.SystemCode = x.Attribute("SystemCode")!.Value; // required
        ed.TransKind = x.Attribute("TransKind")!.Value; // required
        ed.Xmlns = x.Attribute("xmlns")?.Value;

        foreach (var e in x.Elements())
        {
            switch (e.Name.LocalName)
            {
                //case "SettleNotEarlier": // Исполнить не ранее, чем
                //    break;

                //case "SettleNotLater": // Исполнить не позднее, чем
                //    break;

                case "AccDoc":
                    ed.AccDocDate = e.Attribute("AccDocDate")!.Value; // required
                    ed.AccDocNo = e.Attribute("AccDocNo")!.Value; // required
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
                                ed.PayerName = p.Value.Trim().Replace("  ", " ");
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
                                ed.PayeeName = p.Value.Trim().Replace("  ", " ");
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
                    ed.Purpose = e.Value.Trim().Replace("  ", " ");
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

    public static void Load(this ED100 ed, ED100 e)
    {
        ed.EDType = e.EDType;

        ed.ChargeOffDate = e.ChargeOffDate;
        ed.CodePurpose = e.CodePurpose;
        ed.EDAuthor = e.EDAuthor;
        ed.EDDate = e.EDDate;
        ed.EDNo = e.EDNo;
        ed.EDReceiver = e.EDReceiver;
        ed.FileDate = e.FileDate;
        ed.OperationID = e.OperationID;
        ed.PaymentID = e.PaymentID;
        ed.PaymentPrecedence = e.PaymentPrecedence;
        ed.PaytKind = e.PaytKind;
        ed.Priority = e.Priority;
        ed.ReceiptDate = e.ReceiptDate;
        //ed.ReqSettlementDate = e.ReqSettlementDate;
        //ed.ResField = e.ResField;
        ed.Sum = e.Sum;
        ed.SystemCode = e.SystemCode;
        ed.TransKind = e.TransKind;
        ed.Xmlns = e.Xmlns;

        //ed.SettleNotEarlier = e.SettleNotEarlier;
        //ed.SettleNotLater = e.SettleNotLater;

        ed.AccDocDate = e.AccDocDate;
        ed.AccDocNo = e.AccDocNo;

        ed.PayerINN = e.PayerINN;
        ed.PayerKPP = e.PayerKPP;
        ed.PayerPersonalAcc = e.PayerPersonalAcc;
        ed.PayerName = e.PayerName;
        ed.PayerBIC = e.PayerBIC;
        ed.PayerCorrespAcc = e.PayerCorrespAcc;

        ed.PayeeINN = e.PayeeINN;
        ed.PayeeKPP = e.PayeeKPP;
        ed.PayeePersonalAcc = e.PayeePersonalAcc;
        ed.PayeeName = e.PayeeName;
        ed.PayeeBIC = e.PayeeBIC;
        ed.PayeeCorrespAcc = e.PayeeCorrespAcc;

        ed.Purpose = e.Purpose;

        if (e.Tax)
        {
            ed.CBC = e.CBC;
            ed.DocDate = e.DocDate;
            ed.DocNo = e.DocNo;
            ed.OKATO = e.OKATO;
            ed.PaytReason = e.PaytReason;
            ed.TaxPeriod = e.TaxPeriod;
            ed.TaxPaytKind = e.TaxPaytKind;
        }
    }

public static void WriteXML(this ED100 ed, XmlWriter writer) //TODO write not null only (except not required)
    {
        // ED101
        writer.WriteStartElement(ed.EDType, ed.Xmlns);

        writer.WriteAttributeString("ChargeOffDate", ed.ChargeOffDate); //71
        writer.WriteAttributeString("EDAuthor", ed.EDAuthor);
        writer.WriteAttributeString("EDDate", ed.EDDate);
        writer.WriteAttributeString("EDNo", ed.EDNo);
        writer.WriteAttributeString("FileDate", ed.FileDate); //63
        writer.WriteAttributeString("OperationID", ed.OperationID); //22
        writer.WriteAttributeString("PaymentID", ed.PaymentID); //22
        writer.WriteAttributeString("PaymentPrecedence", ed.PaymentPrecedence);
        writer.WriteAttributeString("PaytKind", ed.PaytKind); //5
        writer.WriteAttributeString("Priority", ed.Priority); //21
        writer.WriteAttributeString("ReceiptDate", ed.ReceiptDate); //62
        //writer.WriteAttributeString("ResField", ed.ResField); //23
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

            writer.WriteAttributeString("CBC", ed.CBC ?? "0"); //104
            writer.WriteAttributeString("DocDate", ed.DocDate ?? "0"); //109
            writer.WriteAttributeString("DocNo", ed.DocNo ?? "0"); //108
            writer.WriteAttributeString("DrawerStatus", ed.DrawerStatus); //101
            writer.WriteAttributeString("OKATO", ed.OKATO ?? "0"); //105
            writer.WriteAttributeString("PaytReason", ed.PaytReason ?? "0"); //106
            writer.WriteAttributeString("TaxPeriod", ed.TaxPeriod ?? "0"); //107

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
    public static string MT103(this ED100 ed)
    {
        string sum = ed.Sum; // save for BESP if over 100 000 000.00
        ed.Translit();

        string id = $"{ed.EDDate}{ed.EDNo.PadLeft(9, '0')}"; //15x

        // Block Structure

        var sb = new StringBuilder();
        sb
            // Basic header
            .Append("{1:F01") // Block 1 identifier : Application identifier
            .Append(Config.BankSWIFT) // Service identifier
            .Append("AXXX") // Logical terminal address
            .Append(id) // Session number
            .Append('}')

            // Application header
            .Append("{2:I103") // Block 2 identifier : In, MT103
            .Append(Config.CorrSWIFT) // Destination address
            .Append("XXXXXN}") // Logical terminal address, Message priority (Normal)

            // User header
            .Append("{3:{113:RUR6}{121:") // Block 3 identifier : Version
            .Append(Guid.NewGuid()) // uuid v4
            .Append("}}")

            // Text
            .AppendLine("{4:"); // Block 4 identifier : 

        #region SWIFT TEXT

        // Референс Отправителя (16x)
        //sb.AppendLine($":20:+{date}-{es.EDNo}F103"); //..F103 is over 16x!

        sb.AppendLine($":20:+{id}");

        // Код банковской операции

        sb.AppendLine(":23B:CRED");

        // Код типа операции

        sb.AppendLineIf(ed.Tax, $":26T:S{ed.DrawerStatus}");

        // Дата валютирования/Валюта/Сумма межбанковского расчета

        sb.AppendLine($":32A:{ed.ChargeOffDate}RUB{ed.Sum}");

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
            .AppendLineIf(ed.PayerINN != null, $"INN{ed.PayerINN}{ed.PayerKPP.AddNotEmptyNorZeros()}");

        // (3*35x!)
        // ООО "Название юрлица"
        // или при отсутствии ИНН у физлица:
        // Ф.И.О. полностью//адрес места жительства (регистрации) или места пребывания//

        if (ed.PayerName != null)
        {
            var s = ed.PayerName.Prepare35();

            for (int i = 0; i < 3; i++)
            {
                var s35 = s.Slice(i * 35, 35).TrimEnd();

                if (s35.Length == 0) break;

                sb.AppendLine(s35.ToString());
            }
        }

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

        // Бенефициар
        // (клиент, которому будут выплачены средства)

        sb.AppendLine($":59:/{ed.PayeePersonalAcc}")
            .AppendLineIf(ed.PayeeINN != null, $"INN{ed.PayeeINN}{ed.PayeeKPP.AddNotEmptyNorZeros()}");

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


        int purposeLine = 0;
        ReadOnlySpan<char> purpose = string.Empty;

        if (ed.Purpose != null)
        {
            purpose = ed.Purpose.Prepare35();

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

        sb.AppendLine($":71A:OUR");

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

        // Уникальный Идентификатор Платежа (УИН)
        if (ed.Tax)
        {
            sb.AppendLine($"/UIP/{ed.PaymentID ?? "0"}");
        }

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
                .AppendLine($"/N10/{ed.TaxPaytKind ?? "0"}/N4/{ed.CBC ?? "0"}")
                .AppendLine($"/N5/{ed.OKATO ?? "0"}/N6/{ed.PaytReason ?? "0"}/N7/{ed.TaxPeriod ?? "0"}")
                .AppendLine($"/N8/{ed.DocNo ?? "0"}/N9/{ed.DocDate ?? "0"}");
        }

        #endregion SWIFT TEXT

        sb.Append("-}")

            // Trailers
            .AppendLine("{5:}"); // Block 5 identifier : 

        return sb.ToString();
    }

    /// <summary>
    /// SWIFT-RUR 6: MT202 ("ОБЩИЙ МЕЖБАНКОВСКИЙ ПЕРЕВОД")
    /// </summary>
    /// <param name="ed"></param>
    /// <returns></returns>
    public static string MT202(this ED100 ed)
    {
        string sum = ed.Sum; // save for BESP if over 100 000 000.00
        ed.Translit();

        string id = $"{ed.EDDate}{ed.EDNo.PadLeft(9, '0')}"; //15x

        // Block Structure

        var sb = new StringBuilder();
        sb
            // Basic header
            .Append("{1:F01") // Block 1 identifier : Application identifier
            .Append(Config.BankSWIFT) // Service identifier
            .Append("AXXX") // Logical terminal address
            .Append(id) // Session number
            .Append('}')

            // Application header
            .Append("{2:I202") // Block 2 identifier : In, MT202
            .Append(Config.CorrSWIFT) // Destination address
            .Append("XXXXXN}") // Logical terminal address, Message priority (Normal)

            // User header
            .Append("{3:{113:RUR6}{121:") // Block 3 identifier : Version
            .Append(Guid.NewGuid()) // uuid v4
            .Append("}}")

            // Text
            .AppendLine("{4:"); // Block 4 identifier : 

        #region SWIFT TEXT

        // Референс Отправителя (16x)

        sb.AppendLine($":20:+{id}");

        // Связанный референс (16x)

        sb.AppendLine(":21:NONREF");

        // Дата валютирования/Валюта/Сумма межбанковского расчета

        sb.AppendLine($":32A:{ed.ChargeOffDate}RUB{ed.Sum}");

        // Банк-плательщик

        sb.AppendLine($":52D://RU{ed.PayerBIC}.{ed.PayerCorrespAcc}") // OurBIC.OurCorrACC
            .AppendLine(SwiftTranslit.Lat("АО Сити Инвест Банк"))
            .AppendLine(SwiftTranslit.Lat("г.Санкт-Петербург"));

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

        // Банк-Бенефициар

        sb.AppendLine($":58D:/{ed.PayeePersonalAcc}")
            .AppendLineIf(ed.PayeeINN != null, $"INN{ed.PayeeINN}{ed.PayeeKPP.AddNotEmpty()}");

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
            .AppendLine("{5:}"); // Block 5 identifier : 

        return sb.ToString();
    }

    public static string ESum(this string value)
    {
        if (value is null || value == "0")
        {
            return string.Empty;
        }

        // "0 123 456 789 012 345.67" (18d УФЭБС в целых копейках)

        //ReadOnlySpan<char> s = value.PadLeft(18);

        //return s.Length switch
        //{
        //    1 => $"0.0{value}",
        //    2 => $"0.{value}",
        //    _ => $"{s[..1]} {s.Slice(1, 3)} {s.Slice(4, 3)} {s.Slice(7, 3)} {s.Slice(10, 3)} {s.Slice(13, 3)}.{s.Slice(16, 2)}".Trim(),
        //};

        // "012345678901,34" 15d SWIFT
        // "01234567890123"  УФЭБС, PadLeft(14)
        // "012 345 678 901,23" (15d SWIFT вместе с запятой после 2 знаков)

        ReadOnlySpan<char> s = value.PadLeft(14);

        return value.Length switch
        {
            1 => $"0.0{value}",
            2 => $"0.{value}",
            _ => $"{s[..3]} {s.Slice(3, 3)} {s.Slice(6, 3)} {s.Slice(9, 3)}.{s.Slice(12, 2)}".Trim(),
        };
    }
}
