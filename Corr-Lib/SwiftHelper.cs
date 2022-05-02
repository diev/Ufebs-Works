using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Corr_Lib;

public static class SwiftHelper
{
    private const string VALUE = @"(?<Value>(.|\n)*?)";

    //private const string _payerPattern = @"^:50K:/(\d*)\r\nINN(\d*)(|.KPP(\d*))\r\n((.|\r\n)*?)\r\n(:\d{2}\w?:)";
    //private const string _payerNamePattern = @"^:50K:/\d*\r\nINN\d*(|.KPP\d*)\r\n((.|\r\n)*?)\r\n(:\d{2}\w?:)";
    ////private const string _purposePattern = @"^:70:((.|\n)*?)\n:\d{2}\w?:(.|\n)*\n/NZP/((.|\n)*?)\n(/DAS/|/RPO/|/RPP/|/UIP/|:\d{2}\w?:|-})";
    //private const string _purpose1Pattern = @"^:70:((.|\r\n)*?)\r\n(:\d{2}\w?:)";
    //private const string _purpose2Pattern = @"^:72:((.|\r\n)*?)/NZP/((.|\r\n)*?)\n(/DAS/|/RPO/|/RPP/|/UIP/|:\d{2}\w?:|-})";
    //private const string _purpose3Pattern = @"^:72:(.|\r\n)*?\r\n(:\d{2}\w?:|-})";

    //private static readonly RegexOptions _regexOptions = RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Compiled;

    //private static string? _payerEndCode;
    //private static string? _purpose1EndCode;
    //private static string? _purpose2StartCode;
    //private static string? _purpose2EndCode;
    //private static bool _purposeNzp;

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

    //public static string GetSection(string text, string field)
    //{
    //    string pattern = $":{field}:" + @"(.|\n)*?\n(:\d{2}\w?:|-})";
    //    RegexOptions options = RegexOptions.Multiline | RegexOptions.ExplicitCapture;

    //    Match m = Regex.Match(text, pattern, options);
    //    Console.WriteLine("'{0}' found at index {1}", m.Value, m.Index);

    //    return m.Value;
    //}

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

    public static string SetPayerSection(string input, string acc, string inn, string kpp, string name)
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

        return Regex.Replace(input, pattern, value, RegexOptions.Multiline);

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

    public static string SetPayerName(string input, string value)
    {
        string pattern = @"(?<=^:50K:/\d*\r?\nINN\d*(|.KPP\d*)\r?\n)" + VALUE + @"(?=\r?\n:\d{2}\w?:)";
        string replacement = Swift.Wrap35(Swift.Lat(value));

        return Regex.Replace(input, pattern, replacement, RegexOptions.Multiline);
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

    public static string SetPurpose(string input, string value)
    {
        string pattern = "(?<=^:70:)" + VALUE + @"(?=\r?\n:\d{2}\w?:)";
        string replacement;
        value = Swift.Lat(value);

        if (value.Length <= 140)
        {
            replacement = Swift.Wrap35(value);
            return Regex.Replace(input, pattern, replacement, RegexOptions.Multiline);
        }

        replacement = Swift.Wrap35(value[..140]);
        string result = Regex.Replace(input, pattern, replacement, RegexOptions.Multiline);

        pattern = @"(?<=^:72:(|(.|\n)*?\n)/NZP/)" + VALUE + @"(?=\r?\n(/DAS/|/RPO/|/RPP/|/UIP/|:\d{2}\w?:|-}))";
        replacement = Swift.Wrap35(value[140..]).Replace("\n", "\n//");

        return Regex.Replace(result, pattern, replacement, RegexOptions.Multiline);
    }

    //------------------------------------------------------------------------------------------

    //public static string GetPayerName(string text)
    //{
    //    var match = Regex.Match(text, _payerNamePattern, _regexOptions);
    //    _payerEndCode = match.Groups[4].Value;

    //    return Swift.Cyr(match.Groups[2].Value.ReplaceLineEndings(string.Empty));
    //}

    //public static PayerInfo GetPayerInfo(string text)
    //{
    //    var match = Regex.Match(text, _payerPattern, _regexOptions);
    //    _payerEndCode = match.Groups[5].Value;

    //    return new PayerInfo
    //    {
    //        Acc = match.Groups[1].Value,
    //        INN = match.Groups[2].Value,
    //        KPP = match.Groups[3].Value, // null if absent
    //        Name = Swift.Cyr(match.Groups[4].Value.ReplaceLineEndings(string.Empty))
    //    };
    //}

    //public static string GetPurpose(string text)
    //{
    //    var match1 = Regex.Match(text, _purpose1Pattern, _regexOptions);
    //    string purpose1 = match1.Groups[1].Value.ReplaceLineEndings(string.Empty);
    //    _purpose1EndCode = match1.Groups[3].Value;

    //    var match2 = Regex.Match(text, _purpose2Pattern, _regexOptions);
    //    _purposeNzp = match2.Success;

    //    if (!_purposeNzp)
    //    {
    //        return Swift.Cyr(purpose1);
    //    }

    //    _purpose2StartCode = match2.Groups[1].Value;
    //    string purpose2 = match2.Groups[3].Value.Replace("\n//", string.Empty, StringComparison.Ordinal);
    //    _purpose2EndCode = match2.Groups[5].Value;

    //    return Swift.Cyr(purpose1 + purpose2);
    //}

    //public static string ReplacePayer(string text, PayerInfo payer)
    //{
    //    string kpp = payer.INN != null && payer.INN.Length == 12
    //        ? string.Empty
    //        : $".KPP{payer.KPP}";

    //    string tran = Swift.Lat(payer.Name ?? string.Empty);
    //    string name = Swift.Wrap35(tran);

    //    string replace = $"^:50K:/{payer.Acc}\r\nINN{payer.INN}{kpp}\r\n{name}\r\n{_payerEndCode}";

    //    return new Regex(_payerPattern, _regexOptions).Replace(text, replace, 1);
    //}

    //public static string ReplacePurpose(string text, string purpose)
    //{
    //    string tran = Swift.Lat(purpose);

    //    if (tran.Length > 140) // 4x35
    //    {
    //        string replace1 = ":70:" + Swift.Wrap35(tran[..140]) + Environment.NewLine + _purpose1EndCode;
    //        text = new Regex(_purpose1Pattern, _regexOptions).Replace(text, replace1, 1);
    //        string replace2 = ":72:" + _purpose2StartCode + "/NZP/" + Swift.Wrap35(tran[140..]).Replace("\n", "\n//") + Environment.NewLine + _purpose2EndCode;

    //        if (_purposeNzp)
    //        {
    //            text = new Regex(_purpose2Pattern, _regexOptions).Replace(text, replace2, 1);
    //        }
    //        else
    //        {
    //            text = new Regex(_purpose3Pattern, _regexOptions).Replace(text, replace2, 1);
    //        }
    //    }
    //    else
    //    {
    //        string replace = ":70:" + Swift.Wrap35(tran) + Environment.NewLine + _purpose1EndCode;
    //        text = new Regex(_purpose1Pattern, _regexOptions).Replace(text, replace, 1);
    //    }

    //    return text;
    //}
}
