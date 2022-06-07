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

namespace CorrLib;

/// <summary>
/// SWIFT-RUR
/// </summary>
public static class SwiftTranslit
{
    private static readonly Dictionary<char, char> TRANSLAT = new()
    {
        { 'А', 'A' },
        { 'Б', 'B' },
        { 'В', 'V' },
        { 'Г', 'G' },
        { 'Д', 'D' },
        { 'Е', 'E' },
        { 'Ё', 'o' },
        { 'Ж', 'J' },
        { 'З', 'Z' },
        { 'И', 'I' },
        { 'Й', 'i' },
        { 'К', 'K' },
        { 'Л', 'L' },
        { 'М', 'M' },
        { 'Н', 'N' },
        { 'О', 'O' },
        { 'П', 'P' },
        { 'Р', 'R' },
        { 'С', 'S' },
        { 'Т', 'T' },
        { 'У', 'U' },
        { 'Ф', 'F' },
        { 'Х', 'H' },
        { 'Ц', 'C' },
        { 'Ч', 'c' },
        { 'Ш', 'Q' },
        { 'Щ', 'q' },
        { 'Ъ', 'x' },
        { 'Ы', 'Y' },
        { 'Ь', 'X' },
        { 'Э', 'e' },
        { 'Ю', 'u' },
        { 'Я', 'a' },
        { 'а', 'A' },
        { 'б', 'B' },
        { 'в', 'V' },
        { 'г', 'G' },
        { 'д', 'D' },
        { 'е', 'E' },
        { 'ё', 'o' },
        { 'ж', 'J' },
        { 'з', 'Z' },
        { 'и', 'I' },
        { 'й', 'i' },
        { 'к', 'K' },
        { 'л', 'L' },
        { 'м', 'M' },
        { 'н', 'N' },
        { 'о', 'O' },
        { 'п', 'P' },
        { 'р', 'R' },
        { 'с', 'S' },
        { 'т', 'T' },
        { 'у', 'U' },
        { 'ф', 'F' },
        { 'х', 'H' },
        { 'ц', 'C' },
        { 'ч', 'c' },
        { 'ш', 'Q' },
        { 'щ', 'q' },
        { 'ъ', 'x' },
        { 'ы', 'Y' },
        { 'ь', 'X' },
        { 'э', 'e' },
        { 'ю', 'u' },
        { 'я', 'a' },
        { '\'', 'j' },
        { '’', 'j' },
        { '‘', 'j' },
        { '`', 'j' },
        { '№', 'n' },
        { '#', 'n' },
        { '%', 'p' },
        { '&', 'd' },
        { '!', 'b' },
        { '$', 's' },
        { ';', 'v' },
        { '\\', '/' },
        { '|', '/' },
        { '_', 'z' },
        { '=', 'r' },
        { '<', '(' },
        { '>', ')' },
        { '[', '(' },
        { ']', ')' },
        { '{', '(' },
        { '}', ')' },
        { '"', 'm' },
        { '”', 'm' },
        { '“', 'm' },
        { '«', 'm' },
        { '»', 'm' },
        { '*', 'f' },
        { '@', 'f' },
        { '^', 'f' },
        { '~', 'f' }
    };

    private static readonly Dictionary<char, char> TRANSCYR = new()
    {
        { 'A', 'А' },
        { 'B', 'Б' },
        { 'V', 'В' },
        { 'G', 'Г' },
        { 'D', 'Д' },
        { 'E', 'Е' },
        { 'o', 'Ё' },
        { 'J', 'Ж' },
        { 'Z', 'З' },
        { 'I', 'И' },
        { 'i', 'Й' },
        { 'K', 'К' },
        { 'L', 'Л' },
        { 'M', 'М' },
        { 'N', 'Н' },
        { 'O', 'О' },
        { 'P', 'П' },
        { 'R', 'Р' },
        { 'S', 'С' },
        { 'T', 'Т' },
        { 'U', 'У' },
        { 'F', 'Ф' },
        { 'H', 'Х' },
        { 'C', 'Ц' },
        { 'c', 'Ч' },
        { 'Q', 'Ш' },
        { 'q', 'Щ' },
        { 'x', 'Ъ' },
        { 'Y', 'Ы' },
        { 'X', 'Ь' },
        { 'e', 'Э' },
        { 'u', 'Ю' },
        { 'a', 'Я' },
        { 'j', '\'' },
        { 'n', '№' },
        { 'p', '%' },
        { 'd', '&' },
        { 'b', '!' },
        { 's', '$' },
        { 'v', ';' },
        { 'z', '_' },
        { 'r', '=' },
        { 'm', '"' },
        { 'f', '*' }
    };

    /// <summary>
    /// Транслитерация по правилам SWIFT-RUR
    /// </summary>
    /// <param name="c">Символ на кирилице</param>
    /// <returns>Символ на латинице</returns>
    public static char Lat(char c)
    {
        if (TRANSLAT.TryGetValue(c, out char result))
        {
            return result;
        }
        else
        {
            return c;
        }
    }

    /// <summary>
    /// Транслитерация по правилам SWIFT-RUR
    /// </summary>
    /// <param name="c">Символ на латинице</param>
    /// <returns>Символ на кирилице</returns>
    public static char Cyr(char c)
    {
        if (TRANSCYR.TryGetValue(c, out char result))
        {
            return result;
        }
        else
        {
            return c;
        }
    }

    /// <summary>
    /// Транслитерация по правилам SWIFT-RUR
    /// </summary>
    /// <param name="text">Строка на кирилице</param>
    /// <returns>Строка на латинице</returns>
    public static string Lat(string text)
    {
        bool rus = true; // default RU stream
        var result = new StringBuilder(text.Length);

        foreach (var c in text)
        {
            if (rus)
            {
                if ("ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(c, StringComparison.OrdinalIgnoreCase))
                {
                    rus = false;
                    result.Append('\'').Append(c);
                }
                else
                {
                    result.Append(Lat(c));
                }
            }
            else if (c == '\'')
            {
                rus = true;
                result.Append("\'j");
            }
            else if (!"ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789/-?:().,+ ".Contains(c, StringComparison.OrdinalIgnoreCase))
            {
                rus = true;
                result.Append('\'').Append(Lat(c));
            }
            else
            {
                result.Append(c);
            }
        }

        return result.ToString();
    }

    public static string L(this string value)
    {
        return Lat(value);
    }

    /// <summary>
    /// Транслитерация по правилам SWIFT-RUR
    /// </summary>
    /// <param name="text">Строка на латинице</param>
    /// <returns>Строка на кирилице</returns>
    public static string Cyr(string text)
    {
        if (!text.Contains('\'', StringComparison.OrdinalIgnoreCase))
        {
            return string.Concat(text.Select(Cyr));
        }

        bool rus = true; // default RU stream
        var result = new StringBuilder(text.Length);

        foreach (var c in text)
        {
            if (c == '\'')
            {
                rus = !rus;
            }
            else if (rus)
            {
                result.Append(Cyr(c));
            }
            else // !rus
            {
                result.Append(c);
            }
        }

        return result.ToString();
    }

    /// <summary>
    /// Разбиение строки на текст по 35 символов в строке
    /// </summary>
    /// <param name="src">Строка</param>
    /// <returns>Текст по 35 символов в строке</returns>
    public static string Wrap35(string src)
    {
        var result = new StringBuilder(220);

        while (src.Length > 35)
        {
            result.AppendLine(src[..35]);
            src = src.Remove(0, 35);
        }

        return result.Append(src).ToString();
    }

    /// <summary>
    /// Преобразование даты из формата УФЭБС XML в SWIFT-RUR
    /// </summary>
    /// <param name="src">ГГГГ-ММ-ДД</param>
    /// <returns>ГГММДД</returns>
    public static string XDate(string src)
    {
        return src[2..].Replace("-", "");
    }

    /// <summary>
    /// Преобразование суммы из формата УФЭБС XML в SWIFT-RUR.
    /// Целая часть должна содержать, по крайней мере, одну цифру.
    /// Дробная часть может отсутствовать, но запятая между целой и дробной частью всегда должна присутствовать.
    /// </summary>
    /// <param name="src">РКК</param>
    /// <returns>Р,КК</returns>
    public static string XSum(string src)
    {
        if (src.Length > 2)
        {
            if (src.EndsWith("00"))
            {
                return src[..^2] + ",";
            }

            return src.Insert(src.Length - 2, ",");
        }
        else if (src.Length == 2)
        {
            return "0," + src;
        }
        else
        {
            return "0,0" + src;
        }
    }
}
