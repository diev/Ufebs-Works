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

namespace CorrLib;

/// <summary>
/// ED101 xmlns="urn:cbr-ru:ed:v2.0"
/// Платежное поручение
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

    #endregion Properties

    #region Constructors

    public ED100()
    { }

    public ED100(XNode node)
    {
        this.Load((XElement)node);
    }

    public ED100(XElement element)
    {
        this.Load(element);
    }

    #endregion Constructors

    public ED100 CorrClone()
    {
        ED100 corr = (ED100)this.MemberwiseClone();

        corr.EDType = "ED101";
        corr.PayerName = this.CorrPayerName();

        // удалить КПП для ИП и нулевые
        corr.PayerKPP = this.CorrPayerKPP();
        corr.PayeeKPP = this.CorrPayeeKPP();

        if (this.Tax)
        {
            corr.Purpose = this.CorrPurpose();

            // add required "0" by default!
            corr.CBC = this.CBC ?? "0";
            corr.DocDate = this.DocDate ?? "0";
            corr.DocNo = this.DocNo ?? "0";
            corr.OKATO = this.OKATO ?? "0";
            corr.PaytReason = this.PaytReason ?? "0";
            corr.TaxPeriod = this.TaxPeriod ?? "0";
            corr.TaxPaytKind = this.TaxPaytKind ?? "0";
        }

        return corr;
    }
}
