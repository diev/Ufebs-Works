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

using System.Text;

using static CorrLib.SWIFT.SwiftTranslit;

namespace CorrLib.SWIFT;

public static class SwiftHelpers
{
    /// <summary>
    /// Разбиение строки на текст по 35 символов в строке
    /// </summary>
    /// <param name="value">Строка</param>
    /// <returns>Текст по 35 символов в строке</returns>
    public static string? Wrap35(string? value)
    {
        if (value is null)
            return null;

        var s = value.Prepare35();
        var sb = new StringBuilder(210);

        for (int i = 0; i < 6; i++)
        {
            var s35 = s.Slice(i * 35, 35).TrimEnd();

            if (s35.Length == 0) break;

            sb.AppendLine(s35.ToString());
        }

        return sb.ToString();
    }

    public static string? LatWrap35(this string? value)
        => Wrap35(Lat(value));

    public static ReadOnlySpan<char> Prepare35(this string value)
        => value.PadRight(210, ' ');

    //for (int i = 0; i < 4; i++) //TODO '-' для первых 4 строк
    //{
    //    int pos = i * 35;

    //    if (result[pos] == '-') // prohibited char at beginning of any line
    //    {
    //        result = result.Insert(pos, " ");
    //    }
    //}

    public static string Div35(this string value, int limit = 0)
    {
        int maxLines = limit switch
        {
            105 => 3,
            _ => 260
        };

        var s = value.Prepare35();
        StringBuilder sb = new(260);

        for (int i = 0; i < maxLines; i++)
        {
            var s35 = s.Slice(i * 35, 35).TrimEnd();

            if (s35.Length == 0) break;

            sb.AppendLine(s35.ToString());
        }

        return sb.ToString().TrimEnd();
    }

    /// <summary>
    /// Преобразование даты, если она есть, из формата УФЭБС XML в SWIFT-RUR
    /// </summary>
    /// <param name="value">ГГГГ-ММ-ДД или null</param>
    /// <returns>ГГММДД или null</returns>
    public static string? SwiftDate(string? value)
    {
        if (value == null) return null;

        string date = value.Replace("-", "");

        return date.Length == 8
            ? date[2..]
            : date;
    }

    /// <summary>
    /// Преобразование суммы из формата УФЭБС XML в SWIFT-RUR.
    /// Целая часть должна содержать, по крайней мере, одну цифру.
    /// Дробная часть может отсутствовать, но запятая между целой и дробной частью всегда должна присутствовать.
    /// </summary>
    /// <param name="value">Целочисленная сумма в копейках.</param>
    /// <returns>Рубли,[копейки]</returns>
    public static string SwiftSum(string value)
    {
        if (value.Contains(','))
        {
            return value;
        }

        if (value == "0")
        {
            return "0,";
        }

        return value.Length switch
        {
            1 => "0,0" + value,
            2 => "0," + value.TrimEnd('0'),
            _ => value[..^2] + ',' + value[^2..].TrimEnd('0')
        };
    }

    /// <summary>
    /// Преобразование даты из формата SWIFT-RUR в УФЭБС XML.
    /// </summary>
    /// <param name="value">ГГММДД</param>
    /// <returns>ГГГГ-ММ-ДД</returns>
    public static string UfebsDate(string value)
        => $"20{value[..2]}-{value[2..4]}-{value[^2..]}";

    /// <summary>
    /// Преобразование суммы из формата SWIFT-RUR в УФЭБС XML.
    /// Целая часть должна содержать, по крайней мере, одну цифру.
    /// Дробная часть может отсутствовать, но запятая между целой и дробной частью всегда должна присутствовать.
    /// </summary>
    /// <param name="value">Рубли,[копейки]</param>
    /// <returns>Целочисленная сумма в копейках.</returns>
    public static string UfebsSum(string value)
    {
        if (!value.Contains(','))
        {
            return value;
        }

        if (value == "0,")
        {
            return "0";
        }

        var sum = value.Split(',');

        return (sum[0] + sum[1].PadRight(2, '0')).TrimStart('0');
    }
}
