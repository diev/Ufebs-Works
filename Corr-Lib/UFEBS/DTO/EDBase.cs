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

public record EDBase
{
    /// <summary>
    /// Тип ЭС: Packet..., ED...
    /// </summary>
    public string EDType { get; set; } = null!; // required

    /// <summary>
    /// Уникальный идентификатор составителя ЭС - УИС.
    /// </summary>
    public string EDAuthor { get; set; } = null!; // required
    /// <summary>
    /// Дата составления ЭС.
    /// </summary>
    public string EDDate { get; set; } = null!; // required

    /// <summary>
    /// Уникальный номер ЭС в течение опердня.
    /// </summary>
    public string EDNo { get; set; } = null!; // required
}