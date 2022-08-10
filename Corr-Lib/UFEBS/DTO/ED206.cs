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


namespace CorrLib.UFEBS.DTO;

/// <summary>
/// Подтверждение дебета/кредита.
/// </summary>
public record ED206
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
    /// Сумма ЭПС.
    /// </summary>
    public string Sum { get; set; } = "0"; // required

    /// <summary>
    /// Вид операции.
    /// </summary>
    public string TransDate { get; set; } // required

    public string TransTime { get; } = "00:00:00"; // required

    #region AccDoc

    public string AccDocDate { get; set; }

    public string AccDocNo { get; set; }

    #endregion AccDoc

    #region EDRefID
    //Идентификаторы исходного ЭПС.

    /// <summary>
    /// Уникальный идентификатор составителя ЭС - УИС.
    /// </summary>
    public string EDRefAuthor { get; } = "4030702000";

    /// <summary>
    /// Дата составления ЭС.
    /// </summary>
    public string EDRefDate { get; set; }

    /// <summary>
    /// Номер ЭС в течение опердня.
    /// </summary>
    public string EDRefNo { get; set; }

    #endregion EDRefID

    #region Constructors

    public ED206()
    { }

    public ED206(TransInfo ti)
        => this.Load(ti);

    #endregion Constructors
}
