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
/// PacketESID xmlns="urn:cbr-ru:ed:v2.0"
/// </summary>
public record PacketESID
{
    #region Properties

    /// <summary>
    /// PacketESID
    /// </summary>
    public string EDType { get; } = "PacketESID";

    /// <summary>
    /// Уникальный идентификатор составителя ЭС - УИС.
    /// </summary>
    public string EDAuthor { get; set; } = null!;

    /// <summary>
    /// Дата составления ЭС.
    /// </summary>
    public string EDDate { get; set; } = null!;

    /// <summary>
    /// Номер ЭС в течение опердня.
    /// </summary>
    public string EDNo { get; set; } = null!;

    /// <summary>
    /// Уникальный идентификатор получателя ЭС.
    /// </summary>
    public string EDReceiver { get; } = "4030702000";

    #region Extensions

    /// <summary>
    /// Массив платежных документов.
    /// </summary>
    public ED206[] Elements { get; set; } = Array.Empty<ED206>();

    #endregion Extensions
    #endregion Properties

    #region Constructors

    public PacketESID()
    {
    }

    #endregion Constructors
}
