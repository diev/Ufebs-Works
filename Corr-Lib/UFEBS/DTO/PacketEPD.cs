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

using System.Xml.Linq;

namespace CorrLib.UFEBS.DTO;

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
    #region Properties

    /// <summary>
    /// PacketEPD
    /// </summary>
    public string EDType { get; set; } = "PacketEPD";

    /// <summary>
    /// Уникальный идентификатор составителя ЭС - УИС.
    /// </summary>
    public string EDAuthor { get; set; } = null!;

    /// <summary>
    /// Дата составления ЭС.
    /// </summary>
    public string EDDate { get; set; } = null!;

    /// <summary>
    /// Номер ЭС в течение опердня.
    /// </summary>
    public string EDNo { get; set; } = null!;

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
    public string SystemCode { get; set; } = "02"; // required

    #region Extensions

    /// <summary>
    /// Массив платежных документов.
    /// </summary>
    public ED100[] Elements { get; set; } = Array.Empty<ED100>();

    /// <summary>
    /// Иия файла, из которого загружен пакет.
    /// </summary>
    public string? Path { get; protected set; }

    /// <summary>
    /// Наш идентификатор документа в формате ГГММДД000000001 из EDDate и EDNo (15 цифр).
    /// </summary>
    public string Id
        => $"{EDDate[2..].Replace("-", "")}{EDNo.PadLeft(9, '0')}"; //15x

    #endregion Extensions
    #endregion Properties

    #region Constructors

    public PacketEPD()
    {
    }

    public PacketEPD(string path)
    {
        Path = path;
        this.Load(path);
    }

    public PacketEPD(XNode node)
        => this.Load((XElement)node);

    public PacketEPD(XElement element)
        => this.Load(element);

    #endregion Constructors
}
