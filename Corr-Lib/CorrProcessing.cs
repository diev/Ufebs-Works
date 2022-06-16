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

using static CorrLib.SwiftTranslit;

namespace CorrLib;

public static class CorrProcessing
{
    /// <summary>
    /// Замена Наименования плательщика в случае оплаты за третье лицо.
    /// </summary>
    /// <param name="ed">Документ в формате ED100.</param>
    /// <returns>Наименование плательщика с заменой или без.</returns>
    public static string? CorrName(this ED100 ed)
    {
        if (ed.CorrSubstRequired() && Config.TemplatesName != null)
        {
            //return $"{OurName} ИНН {OurINN} ({ed.CorrShortenName()} Р/С {ed.PayerPersonalAcc})";
            return Config.TemplatesName
                .Replace("{name}", ed.CorrShortenName())
                .Replace("{acc}", ed.PayerPersonalAcc);
        }

        return ed.PayerName;
    }

    /// <summary>
    /// Замена Назначения платежа в случае оплаты в бюджет за третье лицо.
    /// </summary>
    /// <param name="ed">Документ в формате ED100.</param>
    /// <returns>Назначение платежа с заменой или без.</returns>
    public static string? CorrPurpose(this ED100 ed)
    {
        if (ed.Tax && ed.CorrSubstRequired() && Config.TemplatesPurpose != null)
        {
            return Config.TemplatesPurpose
                .Replace("{name}", ed.CorrShortenName())
                .Replace("{purpose}", ed.Purpose);
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
        return ed.PayerINN != null && ed.PayerINN != Config.BankINN;
    }

    /// <summary>
    /// Сокращение по возможности длинных строк в Наименовании плательщика.
    /// </summary>
    /// <param name="ed">Документ в формате ED100.</param>
    /// <returns>Сокращенное Наименование плательщика.</returns>
    public static string? CorrShortenName(this ED100 ed)
    {
        if (ed.PayerName != null)
        {
            return ed.PayerName
                .Replace("Общество с ограниченной ответственностью", "ООО",
                StringComparison.OrdinalIgnoreCase)
                .Replace("Акционерное общество", "АО",
                StringComparison.OrdinalIgnoreCase)
                .Replace("Индивидуальный предприниматель", "ИП",
                StringComparison.OrdinalIgnoreCase)
                .Replace("..", ".",
                StringComparison.OrdinalIgnoreCase);
        }
        else
        {
            return ed.PayerName;
        }
    }

    public static string? CorrKPP(this ED100 ed, int person)
    {
        string? inn = person == 1 ? ed.PayerINN : ed.PayeeINN;
        string? kpp = person == 1 ? ed.PayerKPP : ed.PayeeKPP;

        return inn != null && inn.Length != 12 &&
            kpp != null && kpp.Length > 0 && kpp != "0" && kpp != "000000000"
            ? kpp
            : null;
    }

    public static ED100 Corr(this ED100 ed)
    {
        ed.EDType = "ED101";
        ed.PayerName = ed.CorrName();

        // удалить КПП для ИП и нулевые
        ed.PayerKPP = ed.CorrKPP(1);
        ed.PayeeKPP = ed.CorrKPP(2);

        if (ed.Tax)
        {
            ed.Purpose = ed.CorrPurpose();
        }

        return ed;
    }

    public static ED100 Translit(this ED100 ed)
    {
        ed.PayerName = Lat(ed.PayerName);
        ed.PayeeName = Lat(ed.PayeeName);

        ed.Sum = XSum(ed.Sum);

        ed.ChargeOffDate = XDate(ed.ChargeOffDate);
        ed.EDDate = XDateX(ed.EDDate);
        ed.FileDate = XDate(ed.FileDate);
        ed.ReceiptDate = XDate(ed.ReceiptDate);
        ed.AccDocDate = XDateX(ed.AccDocDate);
        ed.Purpose = Lat(ed.Purpose);

        if (ed.Tax)
        {
            ed.CBC = Lat(ed.CBC);
            ed.DocDate = Lat(ed.DocDate);
            ed.DocNo = Lat(ed.DocNo);
            ed.OKATO = Lat(ed.OKATO);
            ed.PaytReason = Lat(ed.PaytReason);
            ed.TaxPeriod = Lat(ed.TaxPeriod);
            ed.TaxPaytKind = Lat(ed.TaxPaytKind);
        }

        return ed;
    }

    public static bool CorrValid(this ED100 ed)
    {
        switch (Config.SaveFormat)
        {
            case Config.UfebsFormat:
                if (ed.PayerName?.Length > 160) return false;
                if (ed.Purpose?.Length > 210) return false;
                break;

            case Config.SwiftFormat:
                if (Lat(ed.PayerName)?.Length > Config.SwiftNameLimit) return false;
                if (Lat(ed.Purpose)?.Length > 210) return false;
                break;
        }

        return true;
    }

    public static bool CorrMT103Valid(this ED100 ed)
    {
        if (ed.PayerName?.Length > Config.SwiftNameLimit) return false;
        if (ed.Purpose?.Length > 210) return false;

        return true;
    }

    public static string CorrId(this ED100 @this) =>
        $"{SwiftTranslit.XDate(@this.EDDate)}{@this.EDNo.PadLeft(9, '0')}";

    public static string CorrFileName(this ED100 @this) =>
        Path.Combine(Config.SaveDir, Config.SaveMask.Replace("{id}", @this.CorrId()));

}
