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

public class CorrED100 : ED100
{
    public string OriginalEDType { get; internal set; } = null!;
    public string? OriginalPayerName { get; internal set; }
    public bool Saved { get; set; } = false;

    public CorrED100(XNode? node) : base(node)
        => CorrSubst(this);

    public CorrED100(XElement element) : base(element)
        => CorrSubst(this);

    public CorrED100(ED100 ed) : base(ed)
        => CorrSubst(this);

    private static void CorrSubst(CorrED100 ed)
    {
        // Замена всех типов ЭС на Платежное поручение.

        ed.OriginalEDType = ed.EDType;
        ed.EDType = "ED101";

        ed.OriginalPayerName = ed.PayerName;

        // Требуется ли замена реквизитов на оплату за третье лицо (если ИНН Плательщика не собственно Банка).

        if (ed.PayerINN == null || ed.PayerINN == Config.BankINN)
        {
            return;
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
