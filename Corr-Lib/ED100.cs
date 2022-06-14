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

using System.Xml.Linq;

namespace CorrLib;

/// <summary>
/// Базовый комплексный тип для всех электронных платежных сообщений. Содержит реквизиты, общие для всех типов ЭПС.
/// Сверено с форматом УФЭБС по файлу cbr_ed101_v2022.3.0.xsd
/// </summary>
public class ED100
{
    #region Properties

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
    public string EDType { get; set; } = null!;

    #region Attributes

    /// <summary>
    /// Списано со счета плательщика (поле 71). Дата списания денежных средств со счета плательщика.
    /// </summary>
    public string? ChargeOffDate { get; set; }

    /// <summary>
    /// Назначение платежа кодовое (поле 20). До 35 символов.
    /// </summary>
    public string? CodePurpose { get; set; }

    /// <summary>
    /// Уникальный идентификатор составителя ЭС - УИС.
    /// </summary>
    public string EDAuthor { get; set; } = null!; // required "4030702000";

    /// <summary>
    /// Дата составления ЭС.
    /// </summary>
    public string EDDate { get; set; } = null!; // required

    /// <summary>
    /// Номер ЭС в течение опердня.
    /// </summary>
    public string EDNo { get; set; } = null!; // required

    /// <summary>
    /// Уникальный идентификатор получателя ЭС - УИС.
    /// </summary>
    public string? EDReceiver { get; set; }

    /// <summary>
    /// Дата помещения в картотеку (поле 63).
    /// </summary>
    public string? FileDate { get; set; }

    /// <summary>
    /// Уникальный присваиваемый номер операции.
    /// </summary>
    public string? OperationID { get; set; }

    /// <summary>
    /// Уникальный идентификатор платежа (УИП), присвоенный получателем средств (поле 22).
    /// </summary>
    public string? PaymentID { get; set; } // required for Tax (default 0)

    /// <summary>
    /// Приоритет платежа. Две цифры.
    /// </summary>
    public string PaymentPrecedence { get; set; } = null!; // required (default 79)

    /// <summary>
    /// Вид платежа (поле 5). Одна цифра.
    /// </summary>
    public string? PaytKind { get; set; }

    /// <summary>
    /// Очередность платежа (поле 21).
    /// </summary>
    public string Priority { get; set; } = null!; // required (default 5)

    /// <summary>
    /// Поступило в банк плательщика (поле 62).
    /// </summary>
    public string? ReceiptDate { get; set; }

    /// <summary>
    /// Запрошенная (требуемая) дата исполнения распоряжения.
    /// </summary>
    //public string? ReqSettlementDate { get; set; }

    /// <summary>
    /// Резервное поле (поле 23). Текст до 35 символов.
    /// </summary>
    //public string? ResField { get; set; }

    /// <summary>
    /// Сумма (поле 7).
    /// </summary>
    public string Sum { get; set; } = null!; // required > 0

    /// <summary>
    /// Признак системы обработки.
    /// </summary>
    public string SystemCode { get; set; } = null!; // required (default 02)

    /// <summary>
    /// Вид операции (поле 18).
    /// 01 – платежное поручение (ED101)
    /// 02 – платежное требование (ED103)
    /// 06 – инкассовое поручение (ED104)
    /// 16 – платежный ордер (ED105)
    /// </summary>
    public string TransKind { get; set; } = null!; // required (default 01)

    /// <summary>
    /// Схема имен УФЭБС XML.
    /// </summary>
    public string? Xmlns { get; set; } // "urn:cbr-ru:ed:v2.0"

    #endregion Attributes

    #region Elements

    #region SetleNot
    /// <summary>
    /// Исполнить не ранее, чем
    /// </summary>
    //public string? SettleNotEarlier { get; set; }

    /// <summary>
    /// Исполнить не позднее, чем
    /// </summary>
    //public string? SettleNotLater { get; set; }

    #endregion SetleNot

    #region AccDoc
    // Реквизиты исходного распоряжения о переводе денежных средств (поля 3 и 4).

    /// <summary>
    /// Дата составления распоряжения (поле 4).
    /// </summary>
    public string AccDocDate { get; set; } = null!; // required

    /// <summary>
    /// Номер распоряжения (поле 3).
    /// </summary>
    public string AccDocNo { get; set; } = null!; // required

    #endregion AccDoc

    #region PayerRU
    // Реквизиты плательщика.

    /// <summary>
    /// ИНН (или КИО) плательщика (поле 60).
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
    /// Наименование плательщика (поле 8). Текст до 160 символов.
    /// </summary>
    public string? PayerName { get; set; }

    // BankRU
    // Реквизиты банка в электронных платежных сообщениях.

    /// <summary>
    /// БИК (поле 11).
    /// </summary>
    public string? PayerBIC { get; set; } // "044030702"

    /// <summary>
    /// Номер счета банка плательщика (поле 12).
    /// </summary>
    public string? PayerCorrespAcc { get; set; } // "30101810600000000702"

    #endregion PayerRU

    #region PayeeRU
    // Реквизиты получателя средств.

    /// <summary>
    /// ИНН (или КИО) получателя средств (поле 61).
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
    /// Наименование получателя средств (поле 16). Текст до 160 символов.
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

    #endregion PayeeRU

    // Purpose

    /// <summary>
    /// Назначение платежа (поле 24). Текст до 210 символов.
    /// </summary>
    public string? Purpose { get; set; }

    #endregion Elements

    #region DepartmentalInfo
    // Ведомственная информация (поля 101, 104-110). Опционально.

    /// <summary>
    /// Код бюджетной классификации (поле 104).
    /// </summary>
    public string? CBC { get; set; }

    /// <summary>
    /// Дата документа / Показатель даты документа (поле 109).
    /// </summary>
    public string? DocDate { get; set; }

    /// <summary>
    /// Номер документа / Идентификатор сведений о физическом лице в соответствии с указаниями Минфина РФ (поле 108).
    /// </summary>
    public string? DocNo { get; set; }

    /// <summary>
    /// Статус налогоплательщика (поле 101).
    /// </summary>
    public string? DrawerStatus { get; set; } // null if no DepartmentalInfo

    /// <summary>
    /// Код ОКТМО (поле 105).
    /// </summary>
    public string? OKATO { get; set; }

    /// <summary>
    /// Основание платежа (поле 106).
    /// </summary>
    public string? PaytReason { get; set; }

    /// <summary>
    /// Основание налогового периода / Код таможенного органа (поле 107).
    /// </summary>
    public string? TaxPeriod { get; set; }

    /// <summary>
    /// Поле 110.
    /// </summary>
    public string? TaxPaytKind { get; set; }

    #endregion DepartmentalInfo

    #region Functions

    /// <summary>
    /// Присутствует ведомственная информация (поля 101, 104-110).
    /// </summary>
    public bool Tax => DrawerStatus != null;

    #endregion Functions
    #endregion Properties

    #region Constructors

    public ED100(XNode? node)
    {
        if (node != null)
        {
            this.Load((XElement)node);
        }
    }

    public ED100(XElement element)
    {
        this.Load(element);
    }

    public ED100(ED100 ed)
    {
        this.Load(ed);
    }

    #endregion Constructors
}
