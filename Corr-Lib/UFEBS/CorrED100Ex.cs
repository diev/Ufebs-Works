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

namespace CorrLib.UFEBS;

public static class CorrED100Ex
{
    public static CorrED100 CorrLoad(this CorrED100 ed, ED100 e) //TODO Clone(ED100) 
    {
        ed.EDType = e.EDType;

        ed.ChargeOffDate = e.ChargeOffDate; //71
        ed.CodePurpose = e.CodePurpose; //20
        ed.EDAuthor = e.EDAuthor;
        ed.EDDate = e.EDDate;
        ed.EDNo = e.EDNo;
        ed.EDReceiver = e.EDReceiver;
        ed.FileDate = e.FileDate; //63
        ed.OperationID = e.OperationID;
        ed.PaymentID = e.PaymentID; //22
        ed.PaymentPrecedence = e.PaymentPrecedence;
        ed.PaytKind = e.PaytKind; //5
        ed.Priority = e.Priority; //21
        ed.ReceiptDate = e.ReceiptDate; //62
        ed.ReqSettlementDate = e.ReqSettlementDate;
        ed.ResField = e.ResField; //23
        ed.Sum = e.Sum; //7
        ed.SystemCode = e.SystemCode;
        ed.TransKind = e.TransKind; //18
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

        ed.CBC = e.CBC;
        ed.DocDate = e.DocDate;
        ed.DocNo = e.DocNo;
        ed.OKATO = e.OKATO;
        ed.PaytReason = e.PaytReason;
        ed.TaxPeriod = e.TaxPeriod;
        ed.TaxPaytKind = e.TaxPaytKind;

        ed.CorrSubst();

        return ed;
    }

    public static CorrED100 CorrSubst(this CorrED100 ed)
    {
        // Замена всех типов ЭС на Платежное поручение.

        ed.OriginalEDType = ed.EDType;
        ed.EDType = "ED101";

        ed.OriginalPayerName = ed.PayerName;

        // Требуется ли замена реквизитов на оплату за третье лицо (если ИНН Плательщика не собственно Банка).

        if (ed.PayerINN == null || ed.PayerINN == Config.BankINN)
        {
            return ed;
        }

        // Замена Наименования плательщика в случае оплаты за третье лицо.

        var name = ShortenName(ed.OriginalPayerName);

        ed.PayerName = Config.TemplatesName
            .Replace("{name}", name)
            .Replace("{acc}", ed.PayerPersonalAcc);

        // Замена Назначения платежа в случае оплаты в бюджет за третье лицо.

        if (ed.Tax)
        {
            ed.Purpose = Config.TemplatesPurpose
                .Replace("{name}", name)
                .Replace("{purpose}", ed.Purpose);
        }

        return ed;
    }

    /// <summary>
    /// Сокращение по возможности длинных строк в Наименовании плательщика.
    /// </summary>
    /// <returns>Текст покороче.</returns>
    private static string? ShortenName(string? name)
        => name?
        .Replace("Общество с ограниченной ответственностью", "ООО", StringComparison.OrdinalIgnoreCase)
        .Replace("Публичное акционерное общество", "ПАО", StringComparison.OrdinalIgnoreCase)
        .Replace("Акционерное общество", "АО", StringComparison.OrdinalIgnoreCase)
        .Replace("Индивидуальный предприниматель", "ИП", StringComparison.OrdinalIgnoreCase);
}
