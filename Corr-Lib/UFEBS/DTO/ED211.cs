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
/// Извещение об операциях по счету.
/// </summary>
public record ED211 : EDBase
{
    /// <summary>
    /// Дата, на которую формируется извещение об операциях по счету.
    /// </summary>
    public string AbstractDate { get; set; } = null!; // required

    /// <summary>
    /// Тип извещения об операциях по счету.
    /// </summary>
    public string AbstractKind { get; set; } = "0"; // required

    /// <summary>
    /// Номер счета, по которому формируется ЭСИС.
    /// </summary>
    public string Acc { get; set; } = null!; // required

    /// <summary>
    /// БИК подразделения Банка России, в котором открыт счет.
    /// </summary>
    public string BIC { get; set; } = null!; // required

    /// <summary>
    /// Общая сумма документов по кредиту счета участников (если больше нуля).
    /// </summary>
    public string? CreditSum { get; set; }

    /// <summary>
    /// Общая сумма документов по дебету счета участников (если больше нуля).
    /// </summary>
    public string? DebetSum { get; set; }

    /// <summary>
    /// Уникальный идентификатор получателя ЭС.
    /// </summary>
    public string? EDReceiver { get; set; }

    /// <summary>
    /// Конец периода формирования извещения об операциях по счету.
    /// </summary>
    public string EndTime { get; set; } // required

    /// <summary>
    /// Входящий остаток на счете участника (дебетовый остаток - отрицательный, кредитовый - положительный).
    /// </summary>
    public string? EnterBal { get; set; }

    /// <summary>
    /// Дата предыдущего движения по счету.
    /// </summary>
    public string? LastMovetDate { get; set; }

    /// <summary>
    /// Исходящий остаток на счете участника (дебетовый остаток - отрицательный, кредитовый - положительный).
    /// </summary>
    public string OutBal { get; set; } = "0"; // required

    #region Extensions

    /// <summary>
    /// Массив, где каждый элемент это:
    /// Информация об одной операции по лицевому счету в извещении об операциях по счету.
    /// </summary>
    public TransInfo[] Elements { get; set; } = Array.Empty<TransInfo>();

    #endregion Extensions

    #region Constructors

    public ED211()
    {
        EDType = nameof(ED211);
    }

    #endregion Constructors
}
