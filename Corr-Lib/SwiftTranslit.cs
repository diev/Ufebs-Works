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
    /* Набор разрешенных символов:
a b c d e f g h i j k l m n o p q r s t u v w x y z
A B C D E F G H I J K L M N O P Q R S T U V W X Y Z
0 1 2 3 4 5 6 7 8 9
/ - ? : ( ) . , ' +
CR LF Space (Пробел)
     */

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
        { '{', '(' }, //TODO Применяется только для поля 70 в сообщениях SWIFT МТ103 и для поля 72 с кодом /NZP/ МТ202
        { '}', ')' }, //TODO Применяется только для поля 70 в сообщениях SWIFT МТ103 и для поля 72 с кодом /NZP/ МТ202
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
    /// <param name="value">Символ на кирилице</param>
    /// <returns>Символ на латинице</returns>
    public static char Lat(char value)
    {
        if (TRANSLAT.TryGetValue(value, out char result))
        {
            return result;
        }

        return value;
    }

    /// <summary>
    /// Транслитерация по правилам SWIFT-RUR
    /// </summary>
    /// <param name="value">Символ на латинице</param>
    /// <returns>Символ на кирилице</returns>
    public static char Cyr(char value)
    {
        if (TRANSCYR.TryGetValue(value, out char result))
        {
            return result;
        }

        return value;
    }

    /// <summary>
    /// Транслитерация по правилам SWIFT-RUR
    /// </summary>
    /// <param name="value">Строка на кирилице</param>
    /// <returns>Строка на латинице</returns>
    public static string? Lat(string? value)
    {
        if (value is null)
        {
            return value;
        }

        var result = new StringBuilder(value.Length * 2);

        // "{VO12345[PS...]}..."

        int n = value.IndexOf("{VO");

        if (n > -1)
        {
            int m = value.IndexOf("}", n + 3) + 1;

            if (m > -1)
            {
                if (n > 0)
                {
                    result.Append(Lat(value[..n]));
                }

                result.Append("'(VO")
                    .Append(value.AsSpan(n + 3, m - n - 4))
                    .Append(")'");

                if (m < value.Length)
                {
                    result.Append(Lat(value[m..]));
                }

                return result.ToString();
            }
        }

        bool rus = true; // default RU stream
        bool unknown = false;
        var buff = new StringBuilder();

        foreach (var c in value)
        { 
            if (rus)
            {
                if ("ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(c, StringComparison.OrdinalIgnoreCase))
                {
                    rus = false;
                    result.Append('\'').Append(c);
                }
                else if ("0123456789/-?:().,+ ".Contains(c, StringComparison.Ordinal))
                {
                    result.Append(c);
                }
                else if (c == '\'')
                {
                    result.Append('j');
                }
                else
                {
                    result.Append(Lat(c));
                }
            }

            // lat

            else if (c == '\'')
            {
                result.Append('j');
            }

            else if ("0123456789/-?:().,+ ".Contains(c, StringComparison.Ordinal))
            {
                unknown = true;
                buff.Append(c);
            }

            else if ("ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(c, StringComparison.OrdinalIgnoreCase))
            {
                if (unknown)
                {
                    result.Append(buff);
                    unknown = false;
                    buff.Clear();
                }

                result.Append(c);
            }

            else
            {
                rus = true;
                result.Append('\'');

                if (unknown)
                {
                    result.Append(buff);
                    unknown = false;
                    buff.Clear();
                }

                result.Append(Lat(c));
            }
        }

        if (!rus)
        {
            result.Append('\'');

            if (unknown)
            {
                result.Append(buff);
            }
        }

        return result.ToString();
    }

    /// <summary>
    /// Транслитерация по правилам SWIFT-RUR
    /// </summary>
    /// <param name="value">Строка на латинице</param>
    /// <returns>Строка на кирилице</returns>
    public static string? Cyr(string? value) //TODO Purpose "'(VO12345...)'..." -> "{VO12345...}..."
    {
        if (value is null)
        {
            return value;
        }

        /*
        В соответствии с Инструкцией Банка России N181-И от 16.08.2017г. при составлении платежных инструкций для осуществления 
        расчетов с участием нерезидентов в валюте Российской Федерации в поле 70 должен быть указан код вида валютной операции.
        Перед значением кода вида валютной операции проставляется разделительный символ VO.

        Эта информация должна быть заключена в скобки и помещена в начале поля «Назначение платежа» в следующем виде: '(VO<код>)'. 
        Пробелы внутри скобок не допускаются.

        '(VO5!n)'24х (Структурированный формат – Свободный текст)
        3*35x (Свободный текст)

        где VO - код вида валютной операции в соответствии с Приложением № 1 к Инструкции Банка России N181-И от 16.08.2017г.
        
        Примеры:
        
        :70:'(VO10010)'OPLATA ZA STROITELXNYE MATERIALY PO DOGOVORU n82 OT 01/05/2013. BEZ NDS
        :70:'(VO80050)''PAYMENT FOR SERVICES INV 52 DD 30/06/2014. WITHOUT VAT'
         */

        if (!value.Contains('\'', StringComparison.OrdinalIgnoreCase))
        {
            return string.Concat(value.Select(Cyr));
        }

        bool rus = true; // default RU stream
        var result = new StringBuilder(value.Length);

        foreach (var c in value)
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
}
