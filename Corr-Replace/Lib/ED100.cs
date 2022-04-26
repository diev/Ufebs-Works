//Not used

using System.Xml;
using System.Xml.Linq;

namespace Lib;

/// <summary>
/// ED101 xmlns="urn:cbr-ru:ed:v2.0"
/// Платежное поручение
/// </summary>
public class ED100
{
    // Properties

    /// <summary>
    /// ED101 Платежное поручение.
    /// ED103 Платежное требование.
    /// ED104 Инкассовое поручение.
    /// ED105 Платежный ордер.
    /// ED107 Поручение банка.
    /// ED108 Платежное поручение на общую сумму с реестром.
    /// ED109 Банковский ордер.
    /// ED110 ЭПС сокращенного формата.
    /// ED111 Мемориальный ордер в электронном виде.
    /// </summary>
    public string EDType { get; set; } = "ED101";

    /// <summary>
    /// Списано со счета плательщика (поле 71). Дата списания денежных средств со счета плательщика.
    /// </summary>
    public string? ChargeOffDate { get; set; }

    /// <summary>
    /// Уникальный идентификатор составителя ЭС - УИС.
    /// </summary>
    public string EDAuthor { get; set; } = "4030702000";

    /// <summary>
    /// Дата составления ЭС.
    /// </summary>
    public string EDDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");

    /// <summary>
    /// Номер ЭС в течение опердня.
    /// </summary>
    public string EDNo { get; set; } = "1";

    /// <summary>
    /// Уникальный идентификатор платежа, присвоенный получателем средств (поле 22).
    /// </summary>
    public string? PaymentID { get; set; }

    /// <summary>
    /// Приоритет платежа.
    /// </summary>
    public string? PaymentPrecedence { get; set; } // required (\d{2})

    /// <summary>
    /// Вид платежа (поле 5).
    /// </summary>
    public string? PaytKind { get; set; }

    /// <summary>
    /// Очередность платежа (поле 21).
    /// </summary>
    public string Priority { get; set; } = "5"; // required

    /// <summary>
    /// Поступило в банк плательщика (поле 62).
    /// </summary>
    public string? ReceiptDate { get; set; }

    /// <summary>
    /// Сумма (поле 7).
    /// </summary>
    public string Sum { get; set; } = "0"; // required > 0

    /// <summary>
    /// Признак системы обработки.
    /// </summary>
    public string? SystemCode { get; set; }

    /// <summary>
    /// Вид операции (поле 18).
    /// </summary>
    public string TransKind { get; set; } = "01"; // required

    /// <summary>
    /// 
    /// </summary>
    public string Xmlns { get; set; } = "urn:cbr-ru:ed:v2.0";

    // AccDoc

    /// <summary>
    /// Дата составления распоряжения (поле 4).
    /// </summary>
    public string? AccDocDate { get; set; } // required

    /// <summary>
    /// Номер распоряжения (поле 3).
    /// </summary>
    public string? AccDocNo { get; set; } // required

    // PayerRU
    // Реквизиты плательщика в полноформатных электронных платежных сообщениях.

    /// <summary>
    /// ИНН плательщика (поле 60).
    /// </summary>
    public string? PayerINN { get; set; }

    /// <summary>
    /// Код постановки на учет (КПП) плательщика (поле 102).
    /// </summary>
    public string? PayerKPP { get; set; }

    /// <summary>
    /// Номер счета плательщика (поле 9).
    /// </summary>
    public string? PayerPersonalAcc { get; set; }

    /// <summary>
    /// Наименование плательщика (поле 8).
    /// </summary>
    public string? PayerName { get; set; }

    // BankRU
    // Реквизиты банка в электронных платежных сообщениях.

    /// <summary>
    /// БИК (поле 11).
    /// </summary>
    public string PayerBIC { get; set; } = "044030702";

    /// <summary>
    /// Номер счета банка плательщика (поле 12).
    /// </summary>
    public string PayerCorrespAcc { get; set; } = "30101810600000000702";

    // PayeeRU
    // Реквизиты получателя средств в полноформатных электронных платежных сообщениях.

    /// <summary>
    /// ИНН получателя средств (поле 61).
    /// </summary>
    public string? PayeeINN { get; set; }

    /// <summary>
    /// Код постановки на учет (КПП) получателя средств (поле 103).
    /// </summary>
    public string? PayeeKPP { get; set; }

    /// <summary>
    /// Номер счета получателя средств (поле 17).
    /// </summary>
    public string? PayeePersonalAcc { get; set; }

    /// <summary>
    /// Наименование получателя средств (поле 16).
    /// </summary>
    public string? PayeeName { get; set; }

    // BankRU
    // Реквизиты банка, обслуживающего получателя средств.

    /// <summary>
    /// БИК (поле 14).
    /// </summary>
    public string? PayeeBIC { get; set; }

    /// <summary>
    /// Номер счета банка получателя средств (поле 15).
    /// </summary>
    public string? PayeeCorrespAcc { get; set; }

    // Purpose

    /// <summary>
    /// Назначение платежа (поле 24).
    /// </summary>
    public string? Purpose { get; set; }

    // DepartmentalInfo
    // Ведомственная информация (поля 101, 104-110).

    /// <summary>
    /// Поле 104.
    /// </summary>
    public string? CBC { get; set; }

    /// <summary>
    /// Поле 109.
    /// </summary>
    public string? DocDate { get; set; }

    /// <summary>
    /// Поле 108.
    /// </summary>
    public string? DocNo { get; set; }

    /// <summary>
    /// Поле 101.
    /// </summary>
    public string? DrawerStatus { get; set; }

    /// <summary>
    /// Поле 105.
    /// </summary>
    public string? OKATO { get; set; }

    /// <summary>
    /// Поле 106.
    /// </summary>
    public string? PaytReason { get; set; }

    /// <summary>
    /// Поле 107.
    /// </summary>
    public string? TaxPeriod { get; set; }

    /// <summary>
    /// Поле 110.
    /// </summary>
    public string? TaxPaytKind { get; set; }

    // Functions

    /// <summary>
    /// Присутствует ведомственная информация (поля 101, 104-110).
    /// </summary>
    public bool Tax => DrawerStatus != null;

    // Constructors

    public ED100() { }

    public ED100(XElement ed)
    {
        Load(ed);
    }

    // Methods

    public void Load(XElement ed)
    {
        EDType = ed.Name.LocalName;

        ChargeOffDate = ed.Attribute("ChargeOffDate")?.Value;
        EDAuthor = ed.Attribute("EDAuthor").Value;
        EDDate = ed.Attribute("EDDate").Value;
        EDNo = ed.Attribute("EDNo").Value;
        PaymentID = ed.Attribute("PaymentID")?.Value;
        PaymentPrecedence = ed.Attribute("PaymentPrecedence")?.Value;
        PaytKind = ed.Attribute("PaytKind")?.Value;
        Priority = ed.Attribute("Priority")?.Value;
        ReceiptDate = ed.Attribute("ReceiptDate")?.Value;
        Sum = ed.Attribute("Sum").Value;
        SystemCode = ed.Attribute("SystemCode")?.Value;
        TransKind = ed.Attribute("TransKind")?.Value;
        Xmlns = ed.Attribute("xmlns")?.Value;

        foreach (var e in ed.Elements())
        {
            switch (e.Name.LocalName)
            {
                case "AccDoc":
                    AccDocDate = e.Attribute("AccDocDate")?.Value;
                    AccDocNo = e.Attribute("AccDocNo")?.Value;
                    break;

                case "Payer":
                    PayerINN = e.Attribute("INN")?.Value;
                    PayerKPP = e.Attribute("KPP")?.Value;
                    PayerPersonalAcc = e.Attribute("PersonalAcc")?.Value;

                    foreach (var p in e.Elements())
                    {
                        switch (p.Name.LocalName)
                        {
                            case "Name":
                                PayerName = p.Value.Replace("  ", " ");
                                break;

                            case "Bank":
                                PayerBIC = p.Attribute("BIC")?.Value;
                                PayerCorrespAcc = p.Attribute("CorrespAcc")?.Value;
                                break;

                            default:
                                break;
                        }
                    }
                    break;

                case "Payee":
                    PayeeINN = e.Attribute("INN")?.Value;
                    PayeeKPP = e.Attribute("KPP")?.Value;
                    PayeePersonalAcc = e.Attribute("PersonalAcc")?.Value;

                    foreach (var p in e.Elements())
                    {
                        switch (p.Name.LocalName)
                        {
                            case "Name":
                                PayeeName = p.Value.Replace("  ", " ");
                                break;

                            case "Bank":
                                PayeeBIC = p.Attribute("BIC")?.Value;
                                PayeeCorrespAcc = p.Attribute("CorrespAcc")?.Value;
                                break;

                            default:
                                break;
                        }
                    }
                    break;

                case "Purpose":
                    Purpose = e.Value.Replace("  ", " ");
                    break;

                case "DepartmentalInfo":
                    CBC = e.Attribute("CBC")?.Value;
                    DocDate = e.Attribute("DocDate")?.Value;
                    DocNo = e.Attribute("DocNo")?.Value;
                    DrawerStatus = e.Attribute("DrawerStatus")?.Value;
                    OKATO = e.Attribute("OKATO")?.Value;
                    PaytReason = e.Attribute("PaytReason")?.Value;
                    TaxPeriod = e.Attribute("TaxPeriod")?.Value;
                    TaxPaytKind = e.Attribute("TaxPaytKind")?.Value;
                    break;

                default:
                    break;
            }
        }
    }

    public void WriteXML(XmlWriter writer)
    {
        // ED101
        writer.WriteStartElement(EDType, Xmlns);

        writer.WriteAttributeString("ChargeOffDate", ChargeOffDate);
        writer.WriteAttributeString("EDAuthor", EDAuthor);
        writer.WriteAttributeString("EDDate", EDDate);
        writer.WriteAttributeString("EDNo", EDNo);
        writer.WriteAttributeString("PaymentID", PaymentID);
        writer.WriteAttributeString("PaymentPrecedence", PaymentPrecedence);
        writer.WriteAttributeString("PaytKind", PaytKind);
        writer.WriteAttributeString("Priority", Priority);
        writer.WriteAttributeString("ReceiptDate", ReceiptDate);
        writer.WriteAttributeString("Sum", Sum);
        writer.WriteAttributeString("SystemCode", SystemCode);
        writer.WriteAttributeString("TransKind", TransKind);

        // AccDoc
        writer.WriteStartElement("AccDoc");
        writer.WriteAttributeString("AccDocDate", AccDocDate);
        writer.WriteAttributeString("AccDocNo", AccDocNo);
        writer.WriteEndElement(); // AccDoc

        // Payer
        writer.WriteStartElement("Payer");
        writer.WriteAttributeString("INN", PayerINN);

        if (PayerINN.Length != 12)
        {
            writer.WriteAttributeString("KPP", PayerKPP);
        }

        writer.WriteAttributeString("PersonalAcc", PayerPersonalAcc);
        
        writer.WriteElementString("Name", PayerName);
        
        writer.WriteStartElement("Bank");
        writer.WriteAttributeString("BIC", PayerBIC);
        writer.WriteAttributeString("CorrespAcc", PayerCorrespAcc);
        writer.WriteEndElement(); // Bank
        writer.WriteEndElement(); // Payer

        // Payee
        writer.WriteStartElement("Payee");
        writer.WriteAttributeString("INN", PayeeINN);
        writer.WriteAttributeString("KPP", PayeeKPP);
        writer.WriteAttributeString("PersonalAcc", PayeePersonalAcc);
        
        writer.WriteElementString("Name", PayeeName);
        
        writer.WriteStartElement("Bank");
        writer.WriteAttributeString("BIC", PayeeBIC);
        writer.WriteAttributeString("CorrespAcc", PayeeCorrespAcc);
        writer.WriteEndElement(); // Bank
        writer.WriteEndElement(); // Payee

        // Purpose
        writer.WriteElementString("Purpose", Purpose);

        if (Tax)
        {
            // DepartmentalInfo
            writer.WriteStartElement("DepartmentalInfo");
            
            writer.WriteAttributeString("CBC", CBC);
            writer.WriteAttributeString("DocDate", DocDate);
            writer.WriteAttributeString("DocNo", DocNo);
            writer.WriteAttributeString("DrawerStatus", DrawerStatus);
            writer.WriteAttributeString("OKATO", OKATO);
            writer.WriteAttributeString("PaytReason", PaytReason);
            writer.WriteAttributeString("TaxPeriod", TaxPeriod);

            if (TaxPaytKind != null && TaxPaytKind != "0") //fix
            {
                writer.WriteAttributeString("TaxPaytKind", TaxPaytKind);
            }

            writer.WriteEndElement(); // DepartmentalInfo
        }

        writer.WriteEndElement(); // ED101
        writer.Flush();
    }
}
