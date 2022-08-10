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

using CorrLib.UFEBS.DTO;

namespace CorrLib.SWIFT;

/// <summary>
/// Идентификаторы связи УФЭБС и SWIFT.
/// </summary>
public static class SwiftID
{
    /// <summary>
    /// Получение номера сессии для идентификации в системе СПФС.
    /// </summary>
    /// <param name="ed">Электронный документ формата ED100+.</param>
    /// <returns>Идентификатор сообщения TerminalSessionNum (субполя (e),(f) блока {1:}) в формате ММДД000001 из EDDate и EDNo (10 цифр).</returns>
    public static string Num(ED100 ed)
        => Num(ed.EDDate, ed.EDNo);

    /// <summary>
    /// Получение номера сессии для идентификации в системе СПФС.
    /// </summary>
    /// <param name="info">Дата составления и номер ЭС в течение опердня.</param>
    /// <returns>Идентификатор сообщения TerminalSessionNum (субполя (e),(f) блока {1:}) в формате ММДД000001 из EDDate и EDNo (10 цифр).</returns>
    public static string Num(EDInfo info)
        => Num(info.EDDate, info.EDNo);

    /// <summary>
    /// Получение номера сессии для идентификации в системе СПФС.
    /// </summary>
    /// <param name="edDate">Дата составления ЭС.</param>
    /// <param name="edNo">Номер ЭС в течение опердня.</param>
    /// <returns>Идентификатор сообщения TerminalSessionNum (субполя (e),(f) блока {1:}) в формате ММДД000001 из EDDate и EDNo (10 цифр).</returns>
    public static string Num(string edDate, string edNo)
        => edDate.Replace("-", "")[^4..] + edNo.PadLeft(6, '0')[^6..]; //10x

    /// <summary>
    /// Получение нашего идентификатора документа в системе SWIFT.
    /// </summary>
    /// <param name="ed">Электронный документ формата ED100+.</param>
    /// <returns>Идентификатор документа в формате +ГГММДД000000001 из EDDate и EDNo (15 цифр, 16 знаков).</returns>
    public static string Id(ED100 ed)
        => Id(ed.EDDate, ed.EDNo);

    /// <summary>
    /// Получение нашего идентификатора документа в системе SWIFT.
    /// </summary>
    /// <param name="info">Дата составления и номер ЭС в течение опердня.</param>
    /// <returns>Идентификатор документа в формате +ГГММДД000000001 из EDDate и EDNo (15 цифр, 16 знаков).</returns>
    public static string Id(EDInfo info)
        => Id(info.EDDate, info.EDNo);

    /// <summary>
    /// Получение нашего идентификатора документа в системе SWIFT.
    /// </summary>
    /// <param name="edDate">Дата составления ЭС.</param>
    /// <param name="edNo">Номер ЭС в течение опердня.</param>
    /// <returns>Идентификатор документа в формате +ГГММДД000000001 из EDDate и EDNo (15 цифр, 16 знаков).</returns>
    public static string Id(string edDate, string edNo)
        => "+" + edDate.Replace("-", "")[^6..] + edNo.PadLeft(9, '0')[^9..]; //16x (+15x)

    /// <summary>
    /// Получение номера сессии для идентификации в системе СПФС и нашего идентификатора документа в системе SWIFT.
    /// </summary>
    /// <param name="ed">Электронный документ формата ED100+.</param>
    /// <returns>Номер сессии и идентификатор документа.</returns>
    public static (string Num, string Id) ID(ED100 ed)
        => (Num(ed), Id(ed));

    /// <summary>
    /// Получение даты-номера УФЭБС из номера сессии идентификации в системе СПФС.
    /// </summary>
    /// <param name="num">Идентификатор сообщения TerminalSessionNum (субполя (e),(f) блока {1:}) в формате ММДД000001 из EDDate и EDNo (10 цифр).</param>
    /// <returns>Дата составления и номер ЭС в течение опердня.</returns>
    public static EDInfo Num(string num)
        => new(
            $"{DateTime.Today:yyyy}-{num[..2]}-{num[2..4]}",
            num[4..].TrimStart('0'));

    /// <summary>
    /// Получение даты-номера УФЭБС из нашего идентификатора документа в системе SWIFT.
    /// </summary>
    /// <param name="id">Идентификатор документа в формате ГГММДД000000001 из EDDate и EDNo (15 цифр).</param>
    /// <returns>Дата составления и номер ЭС в течение опердня.</returns>
    public static EDInfo Id(string id)
        => new(
            $"20{id[1..3]}-{id[3..5]}-{id[5..7]}",
            id[7..].TrimStart('0'));
}
