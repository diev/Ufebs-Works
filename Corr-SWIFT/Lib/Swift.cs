using System.Text;

namespace Lib;

/// <summary>
/// SWIFT-RUR
/// </summary>
public static class Swift
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

    public static string Lat(string src)
    {
        return Wrap35(string.Concat(src.ToUpper().Select(Lat)));
    }

    public static string Cyr(string src)
    {
        string s = src; //.ToUpper();

        if (!s.Contains('\''))
        {
            return string.Concat(s.Select(Cyr));
        }

        bool asis = false;
        var result = new StringBuilder(s.Length);

        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '\'')
            {
                asis = !asis;
            }
            else if (asis)
            {
                result.Append(s[i]);
            }
            else
            {
                result.Append(Cyr(s[i]));
            }
        }

        return result.ToString();
    }

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
    /// Преобразование суммы из формата УФЭБС XML в SWIFT-RUR
    /// </summary>
    /// <param name="src">РКК</param>
    /// <returns>Р,КК</returns>
    public static string XSum(string src)
    {
        if (src.Length > 2)
        {
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
