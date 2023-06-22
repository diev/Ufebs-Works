#region License
/*
Copyright 2022-2023 Dmitrii Evdokimov
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

using CorrLib.UFEBS.DTO;

namespace CorrLib.UFEBS;

public static class CorrED100Ex
{
    public static ED100 CorrSubst(this ED100 ed)
    {
        // Замена всех типов ЭС на Платежное поручение.

        //ed.OriginalEDType = ed.EDType;
        ed.EDType = "ED101";
        ed.ChargeOffDate ??= ed.EDDate;
        ed.TransKind = "01";

        //ed.OriginalPayerName = ed.PayerName;
        //ed.OriginalPayeeName = ed.PayeeName;
        var payerName = ed.PayerName;
        //var payeeName = ed.PayeeName;

        // Требуется ли замена реквизитов на оплату за третье лицо (если ИНН Плательщика не собственно Банка).

        if (ed.PayerINN == null || ed.PayerINN == Config.BankINN)
        {
            return ed;
        }

        // Замена Наименования плательщика в случае оплаты за третье лицо.

        var name = ShortenName(payerName);

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
