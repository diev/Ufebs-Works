#region License
/*
Copyright 2022-2024 Dmitrii Evdokimov
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

using CorrLib.SWIFT;
using CorrLib.UFEBS.DTO;

using System.ComponentModel.Design;
using System.Xml.Linq;

using static CorrLib.SWIFT.SwiftTranslit;

namespace CorrLib.UFEBS;

/// <summary>
/// Изменения, внесенные в Справочник БИК (Полный Справочник БИК).
/// Сверено с форматом УФЭБС по файлу cbr_ed807_v2022.4.0.xsd
/// </summary>
public static class ED807Finder
{
    /*
<BICDirectoryEntry BIC="044525593">
  <ParticipantInfo NameP="АО "АЛЬФА-БАНК"" EnglName="ALFA-BANK" RegN="1326" 
    CntrCd="RU" Rgn="45" Ind="107078" Tnp="г" Nnp="Москва" 
    Adr="ул Каланчёвская, 27" DateIn="1994-01-20" PtType="20" Srvcs="5" 
    XchType="1" UID="4525593000" ParticipantStatus="PSAC"/> 
  <SWBICS SWBIC="ALFARUMMXXX" DefaultSWBIC="1"/> 
  <Accounts Account="30101810200000000593" RegulationAccountType="CRSA" 
    CK="53" AccountCBRBIC="044525000" DateIn="1998-03-25" 
    AccountStatus="ACAC"/> 
</BICDirectoryEntry>
    */

    private static XElement? _ed807 = null;
    private static readonly Dictionary<string, BankInfo> _cbrCache = [];
    private static readonly Dictionary<string, SwiftBicInfo> _swiftCache = [];

    public static string? ED807File { get; set; }

    /// <summary>
    /// Поиск наименования и населенного пункта банка по его БИК.
    /// </summary>
    /// <param name="bic">БИК банка.</param>
    /// <param name="translit">Надо ли транслитерировать в SWIFT-RUR сразу.</param>
    /// <returns>Наименование и населенный пункт банка по Справочнику БИК.</returns>
    public static BankInfo? Find(string bic, bool translit = false)
    {
        if (_ed807 == null)
        {
            if (ED807File == null)
            {
                return null;
            }

            if (!File.Exists(ED807File))
            {
                return null;
            }

            try
            {
                var root = XDocument.Load(ED807File);
                _ed807 = root.Root;
            }
            catch
            {
                return null;
            }

            if (_ed807 == null)
            {
                return null;
            }
        }

        if (_cbrCache.TryGetValue(bic, out BankInfo? bankInfo))
        {
            return bankInfo;
        }

        //var entry = from e in _ed807.Root.Elements()
        //            where e.Attribute("BIC")!.Value == bic
        //            select e;

        //var info = entry.Elements().First();

        //return new(
        //    info.Attribute("NameP")!.Value, 
        //    info.Attribute("Tnp")!.Value + " " + 
        //    info.Attribute("Nnp")!.Value);

        foreach (var item in _ed807.Elements())
        {
            if (item.Attribute("BIC")!.Value == bic)
            {
                var info = item.Elements().First();

                string name = info.Attribute("NameP")?.Value ?? "Банк";
                string tnp = info.Attribute("Tnp")?.Value ?? "г";
                string nnp = info.Attribute("Nnp")?.Value ?? string.Empty;
                string place = $"{tnp} {nnp}".Trim();

                bankInfo = translit
                    ? new BankInfo(name.Lat(), place.Lat())
                    : new BankInfo(name, place);

                _cbrCache.Add(bic, bankInfo);

                return bankInfo;
            }
        }

        return null;
    }

    /// <summary>
    /// Поиск БИК и кор.счета банка по его SWIFT BIC.
    /// </summary>
    /// <param name="swiftbic">SWIFT BIC банка.</param>
    /// <returns>БИК и кор.счета банка по Справочнику БИК.</returns>
    public static SwiftBicInfo? FindSwift(string swiftbic)
    {
        if (_ed807 == null)
        {
            if (ED807File == null)
            {
                return null;
            }

            if (!File.Exists(ED807File))
            {
                return null;
            }

            try
            {
                var root = XDocument.Load(ED807File);
                _ed807 = root.Root;
            }
            catch
            {
                return null;
            }

            if (_ed807 == null)
            {
                return null;
            }
        }

        if (_swiftCache.TryGetValue(swiftbic, out SwiftBicInfo? bicInfo))
        {
            return bicInfo;
        }

        var ns = _ed807.GetDefaultNamespace();

        foreach (var item in _ed807.Elements())
        {
            foreach (var swbics in item.Elements(ns + "SWBICS"))
            {
                if (swbics.Attribute("SWBIC")!.Value.StartsWith(swiftbic))
                {
                    foreach (var accounts in item.Elements(ns + "Accounts"))
                    {
                        string bic = item.Attribute("BIC")!.Value;
                        string acc = accounts.Attribute("Account")!.Value;

                        bicInfo = new SwiftBicInfo(bic, acc);
                        _swiftCache.Add(swiftbic, bicInfo);

                        return bicInfo;
                    }
                }
            }
        }

        return null;
    }
}
