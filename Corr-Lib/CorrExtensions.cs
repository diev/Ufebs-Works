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

namespace CorrLib;

public static class CorrExtensions
{
    const string OurINN = "7831001422";
    const string OurKPP = "784101001";
    const string OurName = "АО \"Сити Инвест Банк\"";

    /// <summary>
    /// Замена Наименования плательщика в случае оплаты за третье лицо.
    /// </summary>
    /// <param name="ed">Документ в формате ED100.</param>
    /// <returns>Наименование плательщика с заменой или без.</returns>
    public static string CorrPayerName(this ED100 ed)
    {
        if (ed.CorrSubstRequired())
        {
            return $"{OurName} ИНН {OurINN} ({ed.CorrShortenName()} Р/С {ed.PayerPersonalAcc})";
        }

        return ed.PayerName;
    }

    /// <summary>
    /// Замена Назначения платежа в случае оплаты в бюджет за третье лицо.
    /// </summary>
    /// <param name="ed">Документ в формате ED100.</param>
    /// <returns>Назначение платежа с заменой или без.</returns>
    public static string CorrPurpose(this ED100 ed)
    {
        if (ed.Tax && ed.CorrSubstRequired())
        {
            return $"{OurINN}//{OurKPP}//{ed.CorrShortenName()}//{ed.Purpose}"
                .Replace("////", "//");
        }

        return ed.Purpose;
    }

    /// <summary>
    /// Требуется ли замена реквизитов на оплату за третье лицо (если ИНН не банка).
    /// </summary>
    /// <param name="ed">Документ в формате ED100.</param>
    /// <returns></returns>
    public static bool CorrSubstRequired(this ED100 ed)
    {
        return ed.PayerINN != null && ed.PayerINN != OurINN;
    }

    /// <summary>
    /// Сокращение по возможности длинных строк в Наименовании плательщика.
    /// </summary>
    /// <param name="ed">Документ в формате ED100.</param>
    /// <returns>Сокращенное Наименование плательщика.</returns>
    public static string CorrShortenName(this ED100 ed)
    {
        return ed.PayerName
            .Replace("Общество с ограниченной ответственностью", "ООО", StringComparison.OrdinalIgnoreCase)
            .Replace("Акционерное общество", "АО", StringComparison.OrdinalIgnoreCase)
            .Replace("Индивидуальный предприниматель", "ИП", StringComparison.OrdinalIgnoreCase);
    }

    public static string? CorrPayerKPP(this ED100 ed)
    {
        string? inn = ed.PayerINN;
        string? kpp = ed.PayerKPP;

        return inn != null && inn.Length != 12 &&
            kpp != null && kpp.Length > 0 && kpp != "0" && kpp != "000000000"
            ? kpp
            : null;
    }

    public static string? CorrPayeeKPP(this ED100 ed)
    {
        string? inn = ed.PayeeINN;
        string? kpp = ed.PayeeKPP;

        return inn != null && inn.Length != 12 &&
            kpp != null && kpp.Length > 0 && kpp != "0" && kpp != "000000000"
            ? kpp
            : null;
    }
}
