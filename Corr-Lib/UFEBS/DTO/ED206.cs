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

using static CorrLib.SWIFT.SwiftMT;

namespace CorrLib.UFEBS.DTO;

/// <summary>
/// Подтверждение дебета/кредита.
/// </summary>
public record ED206 : EDBase
{
    /// <summary>
    /// Номер расчетного документа.
    /// </summary>
    public string? Acc { get; set; }

    /// <summary>
    /// Уникальный идентификатор получателя ЭС - УИС.
    /// </summary>
    public string ActualReceiver { get; } = "4030702000";

    /// <summary>
    /// БИК банка корреспондента.
    /// </summary>
    public string BICCorr { get; set; } = null!; // required

    public string CorrAcc { get; set; } = null!; // required

    /// <summary>
    /// Признак дебета/кредита.
    /// </summary>
    public string DC { get; set; } = "1"; // required

    /// <summary>
    /// Сумма ЭПС.
    /// </summary>
    public string Sum { get; set; } = "0"; // required

    /// <summary>
    /// Вид операции.
    /// </summary>
    public string? TransDate { get; set; } // required

    public string? TransTime { get; set; } // required

    #region AccDoc

    public string? AccDocDate { get; set; }

    public string? AccDocNo { get; set; }

    #endregion AccDoc

    #region EDRefID
    //Идентификаторы исходного ЭПС.

    /// <summary>
    /// Уникальный идентификатор составителя ЭС - УИС.
    /// </summary>
    public string? EDRefAuthor { get; set; }

    /// <summary>
    /// Дата составления ЭС.
    /// </summary>
    public string? EDRefDate { get; set; }

    /// <summary>
    /// Номер ЭС в течение опердня.
    /// </summary>
    public string? EDRefNo { get; set; }

    #endregion EDRefID

    #region Extensions

    /// <summary>
    /// Транслитерирован ли документ для корсчета SWIFT.
    /// </summary>
    public bool Lat { get; set; } = false;

    /// <summary>
    /// Тип документа до корректировки для корсчета.
    /// </summary>
    public string? OriginalEDType { get; set; }

    /// <summary>
    /// Сохранен ли откорректированный документ для корсчета.
    /// </summary>
    public bool Saved { get; set; } = false;

    /// <summary>
    /// MT900 :20:
    /// </summary>
    public string? SwiftId { get; set; }

    /// <summary>
    /// MT900 :21:
    /// </summary>
    public string? RefSwiftId { get; set; }

    #endregion Extensions

    #region Constructors

    public ED206()
    {
        EDType = nameof(ED206);
    }

    public ED206(TransInfo ti)
        => this.Load(ti);

    public ED206(string[] lines)
    {
        this.Load(lines);
    }

    #endregion Constructors
}
