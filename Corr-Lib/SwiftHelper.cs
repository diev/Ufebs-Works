using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Corr_Lib;

public static class SwiftHelper
{
    private const string VALUE = @"(?<Value>(.|\n)*?)";

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

    public static bool HasTax(string input)
    {
        //string pattern = @"^:26T:S\d{2}$";
        string pattern = @"^:26T:";
        return Regex.IsMatch(input, pattern, RegexOptions.Multiline);
    }

    public static (string OuterText, string InnerText) GetSection(string input, string field)
    {
        string pattern = $"^:{field}:" + VALUE + @"\n(:\d{2}\w?:|-})";
        Match m = Regex.Match(input, pattern, RegexOptions.Multiline);

        return (m.Value, m.Groups["Value"].Value);
    }

    public static string SetSection(string input, string field, string value)
    {
        string pattern = $"(?<=^:{field}:)" + VALUE + @"(?=\r?\n(:\d{2}\w?:|-}))";
        return Regex.Replace(input, pattern, value, RegexOptions.Multiline);
    }

    public static (string Acc, string INN, string KPP, string Name) GetPayerSection(string input)
    {
        const string pattern = @"^:50K:/(?<Acc>\d*)\r?\nINN(?<INN>\d*)(|.KPP(?<KPP>\d*))\r?\n(?<Name>(.|\n)*?)\n:\d{2}\w?:";
        Match m = Regex.Match(input, pattern, RegexOptions.Multiline);

        return (
            m.Groups["Acc"].Value,
            m.Groups["INN"].Value,
            m.Groups["KPP"].Value,
            Swift.Cyr(m.Groups["Name"].Value.ReplaceLineEndings(string.Empty)));
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
        string pattern = "(?<=^:50K:)" + VALUE + @"(?=\r?\n(:\d{2}\w?:|-}))";
        string value = s.ToString();
        int length = value.ReplaceLineEndings(string.Empty).Length;
        value = Regex.Replace(input, pattern, value, RegexOptions.Multiline);

        return (value, length);
    }

    public static string GetPurposeSection(string input)
    {
        string pattern = "^:70:" + VALUE + @"\n:\d{2}\w?:";
        Match m = Regex.Match(input, pattern, RegexOptions.Multiline);

        return Swift.Cyr(m.Groups["Value"].Value.ReplaceLineEndings(string.Empty));
    }

    public static string GetNzpSection(string input)
    {
        if (input.Contains("/NZP/", StringComparison.Ordinal))
        {
            string pattern = @"^:72:(|(.|\n)*?\n)/NZP/" + VALUE + @"\n(/DAS/|/RPO/|/RPP/|/UIP/|:\d{2}\w?:|-})";
            Match m = Regex.Match(input, pattern, RegexOptions.Multiline);

            return Swift.Cyr(m.Groups["Value"].Value
                .Replace("\n//", string.Empty)
                .ReplaceLineEndings(string.Empty));
        }

        return string.Empty;
    }

    public static (string Value, int Length) GetPayerName(string input)
    {
        string pattern = @"^:50K:/\d*\r?\nINN\d*(|.KPP\d*)\r?\n" + VALUE + @"\n:\d{2}\w?:";
        Match m = Regex.Match(input, pattern, RegexOptions.Multiline);

        //return Swift.Cyr(m.Groups["Name"].Value.ReplaceLineEndings(string.Empty));
        string result = m.Groups["Value"].Value.ReplaceLineEndings(string.Empty);
        string value = Swift.Cyr(result);
        int length = result.Length;

        return (value, length);
    }

    public static (string Text, int Length) SetPayerName(string input, string value)
    {
        string pattern = @"(?<=^:50K:/\d*\r?\nINN\d*(|.KPP\d*)\r?\n)" + VALUE + @"(?=\r?\n:\d{2}\w?:)";
        string replacement = Swift.Wrap35(Swift.Lat(value));
        int length = replacement.ReplaceLineEndings(string.Empty).Length;
        string text = Regex.Replace(input, pattern, replacement, RegexOptions.Multiline);

        return (text, length);
    }

    public static (string Value, int Length) GetPurpose(string input)
    {
        //return GetPurposeSection(input) + GetNzpSection(input);

        string pattern = "^:70:" + VALUE + @"\n:\d{2}\w?:";
        Match m = Regex.Match(input, pattern, RegexOptions.Multiline);
        string result = m.Groups["Value"].Value.ReplaceLineEndings(string.Empty);

        if (input.Contains("/NZP/", StringComparison.Ordinal))
        {
            pattern = @"^:72:(|(.|\n)*?\n)/NZP/" + VALUE + @"\n(/DAS/|/RPO/|/RPP/|/UIP/|:\d{2}\w?:|-})";
            m = Regex.Match(input, pattern, RegexOptions.Multiline);
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
        string pattern = "(?<=^:70:)" + VALUE + @"(?=\r?\n:\d{2}\w?:)";
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
        length = replacement.ReplaceLineEndings(string.Empty).Length;
        text = Regex.Replace(input, pattern, replacement, RegexOptions.Multiline);

        pattern = @"(?<=^:72:(|(.|\n)*?\n)/NZP/)" + VALUE + @"(?=\r?\n(/DAS/|/RPO/|/RPP/|/UIP/|:\d{2}\w?:|-}))";
        replacement = Swift.Wrap35(value[140..]).Replace("\n", "\n//");
        length += replacement.ReplaceLineEndings(string.Empty).Length;
        text = Regex.Replace(text, pattern, replacement, RegexOptions.Multiline);

        return (text, length);
    }
}
