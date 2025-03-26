//Not used

using System.Xml;
using System.Xml.Linq;

namespace Lib;

/// <summary>
/// PacketEPD xmlns="urn:cbr-ru:ed:v2.0"
/// Может включать:
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
public class PacketEPD
{
    // Properties

    /// <summary>
    /// PacketEPD
    /// </summary>
    public string EDType { get; set; } = "PacketEPD";

    /// <summary>
    /// Уникальный идентификатор составителя ЭС - УИС.
    /// </summary>
    public string? EDAuthor { get; set; }

    /// <summary>
    /// Дата составления ЭС.
    /// </summary>
    public string? EDDate { get; set; }

    /// <summary>
    /// Номер ЭС в течение опердня.
    /// </summary>
    public string? EDNo { get; set; }

    /// <summary>
    /// Количество ЭПС в пакете.
    /// </summary>
    public string EDQuantity { get; set; } = "0"; // required

    /// <summary>
    /// Уникальный идентификатор получателя ЭС.
    /// </summary>
    public string? EDReceiver { get; set; }

    /// <summary>
    /// Общая сумма ЭПС в пакете.
    /// </summary>
    public string Sum { get; set; } = "0"; // required > 0

    /// <summary>
    /// Признак системы обработки.
    /// </summary>
    public string SystemCode { get; set; } = string.Empty; // required

    /// <summary>
    /// 
    /// </summary>
    public string? Xmlns { get; set; } = "urn:cbr-ru:ed:v2.0";

    // Constructors

    public PacketEPD() { }

    public PacketEPD(XElement ed)
    {
        Load(ed);
    }

    // Methods

    public void Load(XElement ed)
    {
        EDType = ed.Name.LocalName;

        EDAuthor = ed.Attribute("EDAuthor")?.Value;
        EDDate = ed.Attribute("EDDate")?.Value;
        EDNo = ed.Attribute("EDNo")?.Value;
        EDQuantity = ed.Attribute("EDQuantity")!.Value;
        EDReceiver = ed.Attribute("EDReceiver")?.Value;
        Sum = ed.Attribute("Sum")!.Value;
        SystemCode = ed.Attribute("SystemCode")!.Value;
        Sum = ed.Attribute("Sum")!.Value;
        Xmlns = ed.Attribute("xmlns")?.Value;
    }

    public void WriteStartXML(XmlWriter writer)
    {
        // PacketEPD
        writer.WriteStartElement(EDType, Xmlns);
        
        writer.WriteAttributeString("EDAuthor", EDAuthor);
        writer.WriteAttributeString("EDDate", EDDate);
        writer.WriteAttributeString("EDNo", EDNo);
        writer.WriteAttributeString("EDQuantity", EDQuantity);
        writer.WriteAttributeString("EDReceiver", EDReceiver);
        writer.WriteAttributeString("Sum", Sum);
        writer.WriteAttributeString("SystemCode", SystemCode);
        writer.Flush();
    }
}
