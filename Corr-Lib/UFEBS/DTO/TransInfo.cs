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


namespace CorrLib.UFEBS.DTO;

/// <summary>
/// Информация об одной операции по лицевому счету в извещении об операциях по счету.
/// </summary>
public record TransInfo
{
    /// <summary>
    /// Номер расчетного документа.
    /// </summary>
    public string? AccDocNo { get; set; }

    /// <summary>
    /// БИК банка корреспондента.
    /// </summary>
    public string BICCorr { get; set; } = null!; // required

    /// <summary>
    /// Признак дебета/кредита.
    /// </summary>
    public string DC { get; set; } = "1"; // required

    /// <summary>
    /// Счет получателя.
    /// </summary>
    public string? PayeePersonalAcc { get; set; }

    /// <summary>
    /// Счет отправителя/плательщика.
    /// </summary>
    public string PayerPersonalAcc { get; set; } = null!; // required

    /// <summary>
    /// Сумма ЭПС.
    /// </summary>
    public string Sum { get; set; } = "0"; // required

    /// <summary>
    /// Вид операции.
    /// </summary>
    public string TransKind { get; set; } = "01"; // required

    /// <summary>
    /// Вид оборотов.
    /// </summary>
    public string TurnoverKind { get; set; } = "1"; // required

    #region EDRefID
    //Идентификаторы исходного ЭПС.

    /// <summary>
    /// Уникальный идентификатор составителя ЭС - УИС.
    /// </summary>
    public string EDRefAuthor { get; set; }

    /// <summary>
    /// Дата составления ЭС.
    /// </summary>
    public string EDRefDate { get; set; }

    /// <summary>
    /// Номер ЭС в течение опердня.
    /// </summary>
    public string EDRefNo { get; set; }

    #endregion EDRefID

    #region Extensions

    public string? AccDocDate { get; set; }
    public string? CorrAcc { get; set; }

    #endregion Extensions

    #region Constructors

    public TransInfo()
    { }

    public TransInfo(ED100 ed, string dc)
        => this.Load(ed, dc);

    #endregion Constructors
}
