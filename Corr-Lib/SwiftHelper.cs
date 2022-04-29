﻿using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Corr_Lib;

public static class SwiftHelper
{
    private const string _payerPattern = @"^:50K:/(\d*)\nINN(\d*)(|.KPP(\d*))\n((.|\n)*?)\n(:\d{2}\w?:)";
    private const string _payerNamePattern = @"^:50K:/\d*\r\nINN\d*(|.KPP\d*)\r\n((.|\n)*?)\r\n(:\d{2}\w?:)";
    //private const string _purposePattern = @"^:70:((.|\n)*?)\n:\d{2}\w?:(.|\n)*\n/NZP/((.|\n)*?)\n(/DAS/|/RPO/|/RPP/|/UIP/|:\d{2}\w?:|-})";
    private const string _purpose1Pattern = @"^:70:((.|\n)*?)\n(:\d{2}\w?:)";
    private const string _purpose2Pattern = @"^:72:((.|\n)*?)/NZP/((.|\n)*?)\n(/DAS/|/RPO/|/RPP/|/UIP/|:\d{2}\w?:|-})";

    private static readonly RegexOptions _regexOptions = RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Compiled;

    private static string? _payerEndCode;
    private static string? _purpose1EndCode;
    private static string? _purpose2StartCode;
    private static string? _purpose2EndCode;

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

    public static string GetPayerName(string text)
    {
        var match = Regex.Match(text, _payerNamePattern, _regexOptions);
        _payerEndCode = match.Groups[4].Value;

        return Swift.Cyr(match.Groups[2].Value.ReplaceLineEndings(string.Empty));
    }

    public static PayerInfo GetPayerInfo(string text)
    {
        var match = Regex.Match(text, _payerPattern, _regexOptions);
        _payerEndCode = match.Groups[5].Value;

        return new PayerInfo
        {
            Acc = match.Groups[1].Value,
            INN = match.Groups[2].Value,
            KPP = match.Groups[3].Value, // null if absent
            Name = Swift.Cyr(match.Groups[4].Value.ReplaceLineEndings(string.Empty))
        };
    }

    public static string GetPurpose(string text)
    {
        var match1 = Regex.Match(text, _purpose1Pattern, _regexOptions);
        string purpose1 = match1.Groups[1].Value.ReplaceLineEndings(string.Empty);
        _purpose1EndCode = match1.Groups[3].Value;

        var match2 = Regex.Match(text, _purpose2Pattern, _regexOptions);

        if (match2 == null)
        {
            return Swift.Cyr(purpose1);
        }

        _purpose2StartCode = match2.Groups[1].Value;
        string purpose2 = match2.Groups[3].Value.Replace("\n//", string.Empty, StringComparison.Ordinal);
        _purpose2EndCode = match2.Groups[5].Value;

        return Swift.Cyr(purpose1 + purpose2);
    }

    public static string ReplacePayer(string text, PayerInfo payer)
    {
        string kpp = payer.INN != null && payer.INN.Length == 12
            ? string.Empty
            : $".KPP{payer.KPP}";

        string tran = Swift.Lat(payer.Name ?? string.Empty);
        string name = Swift.Wrap35(tran);

        string replace = $"^:50K:/{payer.Acc}\nINN{payer.INN}{kpp}\n{name}\n{_payerEndCode}";

        return new Regex(_payerPattern, _regexOptions).Replace(text, replace, 1);
    }

    public static string ReplacePurpose(string text, string purpose)
    {
        string tran = Swift.Lat(purpose);

        if (tran.Length > 140) // 4x35
        {
            string replace1 = ":70:" + Swift.Wrap35(tran[..139]) + _purpose1EndCode;
            string replace2 = ":72:" + _purpose2StartCode + "/NZP/" + Swift.Wrap35(tran[140..]).Replace("\n", "\n//") + _purpose2EndCode;

            text = new Regex(_purpose1Pattern, _regexOptions).Replace(text, replace1, 1);
            text = new Regex(_purpose2Pattern, _regexOptions).Replace(text, replace2, 1);
        }
        else
        {
            string replace1 = ":70:" + Swift.Wrap35(tran) + _purpose1EndCode;
            text = new Regex(_purpose1Pattern, _regexOptions).Replace(text, replace1, 1);
        }

        return text;
    }
}
