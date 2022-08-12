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

using System.Text;
using System.Text.RegularExpressions;

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
        => Wrap35(value.Lat());

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
}
