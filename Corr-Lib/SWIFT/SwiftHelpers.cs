#region License
/*
Copyright 2022-2023 Dmitrii Evdokimov
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

namespace CorrLib.SWIFT;

public static partial class SwiftHelpers
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

    /// <summary>
    /// Разбиение строк на текст по 35 символов в строке
    /// </summary>
    /// <param name="value">Строка</param>
    /// <returns>Текст по 35 символов в строке</returns>
    public static string? WrapText35(string? value)
    {
        if (value is null)
            return null;

        var n = value.Length + 100;
        var s = value.Prepare35(n);
        var sb = new StringBuilder(n);
        var i = 0;

        while (i < n)
        {
            var s35 = s.Slice(i * 35, 35).TrimEnd();

            if (s35.Length == 0) break;

            sb.AppendLine(s35.ToString());
            i++;
        }

        return sb.ToString();
    }

    public static string? LatWrap35(this string? value)
        => Wrap35(value.Lat());

    public static string? LatWrapText35(this string? value)
        => WrapText35(value.Lat());

    public static ReadOnlySpan<char> Prepare35(this string value, int width = 210)
        => value.PadRight(width, ' ');

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

        var s = value.Prepare35(1024); //TODO test
        StringBuilder sb = new(260);

        for (int i = 0; i < maxLines; i++)
        {
            var s35 = s.Slice(i * 35, 35).TrimEnd();

            if (s35.Length == 0) break;

            sb.AppendLine(s35.ToString());
        }

        return sb.ToString().TrimEnd();
    }

    public static bool LatNameValid(this string value)
    {
        const int rows = 3 * 35;
        //const string pattern = @"^:\d{2}\D{0,1}:";

        string text = value.Lat()!;
        int len = text.Length;

        if (len > rows) return false;

        for (int i = 0; i < len; i += 35)
        {
            if (text[i] == '-')
                return false;

            if (text[i] == ':' &&
                RegexLatName().Match(text[i..]).Success)
                return false;
        }

        return true;
    }

    public static bool LatPurposeValid(this string value)
    {
        const int rows = 4 * 35; // SWIFT field :70:
        //const string pattern = @"^:\d{2}\D{0,1}:";

        string text = value.Lat()!;
        int len = text.Length;

        if (len > 210) return false;

        for (int i = 35; i < Math.Min(len, rows); i += 35)
        {
            if (text[i] == '-')
                return false;

            if (text[i] == ':' &&
                RegexLatPurpose().Match(text[i..]).Success)
                return false;
        }

        return true;
    }

    [GeneratedRegex(@"^:\d{2}\D{0,1}:")]
    private static partial Regex RegexLatName();

    [GeneratedRegex(@"^:\d{2}\D{0,1}:")]
    private static partial Regex RegexLatPurpose();
}
