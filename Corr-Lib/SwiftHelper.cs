using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Corr_Lib;

public static class SwiftHelper
{
    private const string GETVALUE = @"(?<Value>(.|\n)*?)";
    private const string SETVALUE = @"(?<Value>)";

    public static string? GetSwiftDocument(string xml)
    {
        var xdoc = XDocument.Parse(xml);
        var ed = xdoc.Root;

        if (ed == null || ed.Name.LocalName != "ED503")
        {
            return null;
        }

        var ns = ed.GetDefaultNamespace();
        var container = ed.Element(ns + "SWIFTContainer");
        var document = container?.Element(ns + "SWIFTDocument");
        string? value = document?.Value;

        if (value == null)
        {
            return null;
        }

        byte[] bytes = Convert.FromBase64String(value);
        string text = Encoding.ASCII.GetString(bytes);

        return text;
    }

    public static bool HasTranslit(string input)
    {
        string pattern = @"^:20:\+";
        return Regex.IsMatch(input, pattern, RegexOptions.Multiline);
    }

    public static bool HasTax(string input)
    {
        //string pattern = @"^:26T:S\d{2}$";
        string pattern = @"^:26T:S";
        return Regex.IsMatch(input, pattern, RegexOptions.Multiline);
    }

    public static (string OuterText, string InnerText) GetSection(string input, string field)
    {
        string pattern = $"^:{field}:" + GETVALUE + @"\r?\n(:\d{2}\w?:|-})";
        Match m = Regex.Match(input, pattern, RegexOptions.Multiline);

        return (m.Value, m.Groups["Value"].Value);
    }

    public static string SetSection(string input, string field, string value)
    {
        string pattern = $"(?<=^:{field}:)" + SETVALUE + @"(?=\r?\n(:\d{2}\w?:|-}))";
        return Regex.Replace(input, pattern, value, RegexOptions.Multiline);
    }

    public static (string Acc, string INN, string KPP, string Name) GetPayerSection(string input)
    {
        string section = GetSection(input, "50K").InnerText;
        string pattern = @"/(?<Acc>\d*)\r?\nINN(?<INN>\d*)(|.KPP(?<KPP>\d*))\r?\n(?<Name>(.|\n)*)";
        Match m = Regex.Match(section, pattern, RegexOptions.Multiline);

        string acc = m.Groups["Acc"].Value;
        string inn = m.Groups["INN"].Value;
        string kpp = m.Groups["KPP"].Value;
        string name = m.Groups["Name"].Value.ReplaceLineEndings(string.Empty);

        if (inn == string.Empty)
        {
            pattern = @"/(?<Acc>\d*)\r?\n(?<Name>(.|\n)*)";
            m = Regex.Match(section, pattern, RegexOptions.Multiline);

            acc = m.Groups["Acc"].Value; //?
            name = m.Groups["Name"].Value.ReplaceLineEndings(string.Empty);
        }
        else if (inn.Length == 12)
        {
            kpp = string.Empty;
        }

        if (HasTranslit(input))
        {
            name = Swift.Cyr(name);
        }

        return (acc, inn, kpp, name);
    }

    public static (string Value, int Length) SetPayerSection(string input, string acc, string inn, string kpp, string name)
    {
        var s = new StringBuilder(256);

        s.AppendLine($"/{acc}")
            .Append($"INN{inn}");

        if (inn.Length < 12 && kpp.Length == 9)
        {
            s.Append($".KPP{kpp}");
        }

        s.AppendLine()
            .Append(Swift.Wrap35(Swift.Lat(name)));

        //return SetSection(input, "50K", s.ToString());
        string pattern = "(?<=^:50K:)" + SETVALUE + @"(?=\r?\n(:\d{2}\w?:|-}))";
        string value = s.ToString();
        int length = value.ReplaceLineEndings(string.Empty).Length;
        value = Regex.Replace(input, pattern, value, RegexOptions.Multiline);

        return (value, length);
    }

    public static string GetPurposeSection(string input)
    {
        //string pattern = "^:70:" + GETVALUE + @"\n:\d{2}\w?:";
        string section = GetSection(input, "70").InnerText;
        //Match m = Regex.Match(input, pattern, RegexOptions.Multiline);

        //return Swift.Cyr(m.Groups["Value"].Value.ReplaceLineEndings(string.Empty));
        string result = section.ReplaceLineEndings(string.Empty);

        if (HasTranslit(input))
        {
            result = Swift.Cyr(result);
        }

        return result;
    }

    public static string GetNzpSection(string input)
    {
        string section = GetSection(input, "72").InnerText;

        if (section.Contains("/NZP/", StringComparison.Ordinal))
        {
            string pattern = @"/NZP/" + GETVALUE + @"\n(|/DAS/|/RPO/|/RPP/|/UIP/)";
            Match m = Regex.Match(section, pattern, RegexOptions.Multiline);

            return Swift.Cyr(m.Groups["Value"].Value
                .Replace("\n//", string.Empty)
                .ReplaceLineEndings(string.Empty));
        }

        return string.Empty;
    }

    public static (string Value, int Length) GetPayerName(string input)
    {
        string section = GetSection(input, "50K").InnerText;
        //string pattern = @"^:50K:/\d*\r?\nINN\d*(|.KPP\d*)\r?\n" + GETVALUE + @"\n:\d{2}\w?:";
        string pattern = @"^INN\d*(|.KPP\d*)\r?\n" + GETVALUE;
        Match m = Regex.Match(section, pattern, RegexOptions.Multiline);

        //return Swift.Cyr(m.Groups["Name"].Value.ReplaceLineEndings(string.Empty));
        string result = m.Groups["Value"].Value.ReplaceLineEndings(string.Empty);
        string value = Swift.Cyr(result);
        int length = result.Length;

        return (value, length);
    }

    public static (string Text, int Length) SetPayerName(string input, string value)
    {
        string pattern = @"(?<=^:50K:/\d*\r?\nINN\d*(|.KPP\d*)\r?\n)" + SETVALUE + @"(?=\r?\n:\d{2}\w?:)";
        string replacement = Swift.Wrap35(Swift.Lat(value));
        int length = replacement.ReplaceLineEndings(string.Empty).Length;
        string text = Regex.Replace(input, pattern, replacement, RegexOptions.Multiline);

        return (text, length);
    }

    public static (string Value, int Length) GetPurpose(string input)
    {
        //return GetPurposeSection(input) + GetNzpSection(input);

        string section = GetSection(input, "70").InnerText;
        //string pattern = "^:70:" + GETVALUE + @"\n:\d{2}\w?:";
        //Match m = Regex.Match(input, pattern, RegexOptions.Multiline);
        //string result = m.Groups["Value"].Value
        string result = section.ReplaceLineEndings(string.Empty);

        section = GetSection(input, "72").InnerText;

        if (section.Contains("/NZP/", StringComparison.Ordinal))
        {
            //pattern = @"^:72:(|(.|\n)*?\n)/NZP/" + GETVALUE + @"\n(/DAS/|/RPO/|/RPP/|/UIP/|:\d{2}\w?:|-})";
            string pattern = @"/NZP/" + GETVALUE + @"\n(|/DAS/|/RPO/|/RPP/|/UIP/)";
            Match m = Regex.Match(section, pattern, RegexOptions.Multiline);
            result += m.Groups["Value"].Value
                .Replace("\n//", string.Empty)
                .ReplaceLineEndings(string.Empty);
        }

        string value = Swift.Cyr(result);
        int length = result.Length;

        //return Swift.Cyr(result);
        return (value, length);
    }

    public static (string Text, int Length) SetPurpose(string input, string value)
    {
        string pattern = "(?<=^:70:)" + SETVALUE + @"(?=\r?\n:\d{2}\w?:)";
        string replacement, text;
        value = Swift.Lat(value);
        int length = value.ReplaceLineEndings(string.Empty).Length;

        if (length <= 140) //3x35
        {
            replacement = Swift.Wrap35(value);
            text = Regex.Replace(input, pattern, replacement, RegexOptions.Multiline);

            return (text, length);
        }

        replacement = Swift.Wrap35(value[..140]);
        length = 140; // replacement.ReplaceLineEndings(string.Empty).Length;
        text = Regex.Replace(input, pattern, replacement, RegexOptions.Multiline);

        if (text.Contains("/NZP/", StringComparison.Ordinal))
        {
            pattern = @"(?<=^:72:(|(.|\n)*?\n)/NZP/)" + SETVALUE + @"(?=\r?\n(/DAS/|/RPO/|/RPP/|/UIP/|:\d{2}\w?:|-}))";
            replacement = Swift.Wrap35(value[140..]).Replace("\n", "\n//");
        }
        else
        {
            pattern = @"(?<=^:72:(.|\n)*?)" + SETVALUE + @"(?=\r?\n(:\d{2}\w?:|-}))";
            replacement = Swift.Wrap35("/NZP/" + value[140..]).Replace("\n", "\n//") + "\n";
        }

        length += replacement.ReplaceLineEndings(string.Empty).Length;
        text = Regex.Replace(text, pattern, replacement, RegexOptions.Multiline);

        return (text, length);
    }
}
