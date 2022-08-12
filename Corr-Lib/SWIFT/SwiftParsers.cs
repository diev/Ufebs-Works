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

using System.Text.RegularExpressions;

namespace CorrLib.SWIFT;

public static class SwiftParsers
{
    /// <summary>
    /// Преобразование даты, если она есть, из формата УФЭБС XML в SWIFT-RUR
    /// </summary>
    /// <param name="value">ГГГГ-ММ-ДД или null</param>
    /// <returns>ГГММДД или null</returns>
    public static string? ToSwiftDate(this string? value)
    {
        if (value == null) return null;

        string date = value.Replace("-", "");

        return date.Length == 8 ? date[2..] : date;
    }

    /// <summary>
    /// Преобразование суммы из формата УФЭБС XML в SWIFT-RUR.
    /// Целая часть должна содержать, по крайней мере, одну цифру.
    /// Дробная часть может отсутствовать, но запятая между целой и дробной частью всегда должна присутствовать.
    /// </summary>
    /// <param name="value">Целочисленная сумма в копейках.</param>
    /// <returns>Рубли,[копейки]</returns>
    public static string ToSwiftSum(this string value)
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
    public static string ToUfebsDate(this string value)
        => $"20{value[..2]}-{value[2..4]}-{value[^2..]}";

    /// <summary>
    /// Преобразование суммы из формата SWIFT-RUR в УФЭБС XML.
    /// Целая часть должна содержать, по крайней мере, одну цифру.
    /// Дробная часть может отсутствовать, но запятая между целой и дробной частью всегда должна присутствовать.
    /// </summary>
    /// <param name="value">Рубли,[копейки]</param>
    /// <returns>Целочисленная сумма в копейках.</returns>
    public static string ToUfebsSum(this string value)
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
        string kop = (sum[0] + sum[1].PadRight(2, '0')).TrimStart('0');

        if (value[0] == '-')
        {
            kop = "-" + kop; //TODO nonsense!
        }

        return kop;
    }

    /// <summary>
    /// MT* {2:T...}
    /// </summary>
    /// <param name="text">{2:T...}</param>
    /// <returns></returns>
    public static string? ParseMT(this string text)
    {
        string pattern = @"{2:([IO]\d{3})";
        var match = Regex.Match(text, pattern);

        return match.Success
            ? match.Groups[1].Value
            : null;
    }

    /// <summary>
    /// MT* {2:...ГГММДДЧЧММ.}
    /// </summary>
    /// <param name="text">{2:...ГГММДДЧЧММ.}</param>
    /// <returns>HH:MM:00</returns>
    public static (string date, string time) UParseDateTime(this string text)
    {
        string pattern = @"{2:[^}]+(\d{6})(\d{2})(\d{2})N}";
        var match = Regex.Match(text, pattern);

        string date = match.Groups[1].Value.ToUfebsDate();
        string time = match.Success
            ? $"{match.Groups[2].Value}:{match.Groups[3].Value}:00"
            : "00:00:00";

        return (date, time);
    }

    /// <summary>
    /// MT950 :60F:, :62F:, :64:
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static (string date, string sum) UParseBal(this string text)
    {
        string pattern = @"([CD])(\d{6})RUB(\d+,\d{0,2})";
        var match = Regex.Match(text, pattern);

        string dc = match.Groups[1].Value; //Debit|Credit
        string date = match.Groups[2].Value.ToUfebsDate();
        string sum = match.Groups[3].Value.ToUfebsSum();

        if (dc == "D")
        {
            sum = "-" + sum; //TODO nonsense!
        }

        return (date, sum);
    }

    /// <summary>
    /// MT950 :61:
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static (string dc, string id) ParseTrans(this string text)
    {
        string pattern = @"(\d{6})([CD])(\d+,\d{0,2})([^/]+)//(.+)";
        var match = Regex.Match(text, pattern);

        //chargeOffDate = match.Groups[1].Value;
        string dc = match.Groups[2].Value == "D" ? "1" : "2";
        //string sum = match.Groups[3].Value;
        string ourId = match.Groups[4].Value[4..]; //NONREF | +220804000012157
        string corrId = match.Groups[5].Value;

        return (dc, dc == "1" ? ourId : corrId);
    }

    /// <summary>
    /// MT103 :26T:
    /// </summary>
    /// <param name="text">S08</param>
    /// <returns></returns>
    public static string ParseDrawerStatus(this string text)
    {
        //string pattern = @"S(\d{2})";
        //var match = Regex.Match(text, pattern);

        //return match.Groups[1].Value; // Tax

        return text[^2..];
    }

    /// <summary>
    /// MT103 :50K:, :59:
    /// </summary>
    /// <param name="text">INN17831001422[.KPP784101001]</param>
    /// <returns></returns>
    public static (string inn, string? kpp) ParseINNKPP(this string text)
    {
        string pattern = @"INN(\d*)(\.KPP(\d*)){0,1}";
        var match = Regex.Match(text, pattern);

        string inn = match.Groups[1].Value;
        string? kpp = match.Groups[2].Success
            ? match.Groups[3].Value
            : null;

        return (inn, kpp);
    }

    /// <summary>
    /// MT103 :32A:
    /// </summary>
    /// <param name="text">220808RUB130,</param>
    /// <returns></returns>
    public static (string date, string sum) ParseDateSum(this string text)
    {
        string pattern = @"(\d{6})RUB(\d+,\d{0,2})";
        var match = Regex.Match(text, pattern);

        string date = match.Groups[1].Value;
        string sum = match.Groups[2].Value;

        return (date, sum);
    }

    /// <summary>
    /// MT103 :32A:
    /// </summary>
    /// <param name="text">220808RUB130,</param>
    /// <returns></returns>
    public static (string date, string sum) UParseDateSum(this string text)
    {
        string pattern = @"(\d{6})RUB(\d+,\d{0,2})";
        var match = Regex.Match(text, pattern);

        string date = match.Groups[1].Value.ToUfebsDate();
        string sum = match.Groups[2].Value.ToUfebsSum();

        return (date, sum);
    }

    /// <summary>
    /// MT103 :52D:, :57D:
    /// </summary>
    /// <param name="text">//RU044030702.30101810600000000702</param>
    /// <returns></returns>
    public static (string bic, string? acc) ParseBICAcc(this string text)
    {
        string pattern = @"//RU(\d{9})(\.(\d{20})){0,1}";
        var match = Regex.Match(text, pattern);

        string bic = match.Groups[1].Value;
        string? acc = match.Groups[2].Success
            ? match.Groups[3].Value
            : null;

        return (bic, acc);
    }

    public static (string accDocNo, string accDocDate, string priority, bool besp, string transKind) UParseRPP(this string text)
    {
        string pattern = @"/RPP/(\d*)\.(\d{6})\.(\d)\.(\w{4})(\.\d*)?";
        var match = Regex.Match(text, pattern);

        string accDocNo = match.Groups[1].Value;
        string accDocDate = match.Groups[2].Value.ToUfebsDate();
        string priority = match.Groups[3].Value;
        bool besp = match.Groups[4].Value == "BESP";
        string transKind = match.Groups[5].Success
            ? match.Groups[5].Value[1..]
            : "01";

        return (accDocNo, accDocDate, priority, besp, transKind);
    }

    public static string UParseDAS(this string text)
    {
        string pattern = @"/DAS/\d{6}\.(\d{6})";
        var match = Regex.Match(text, pattern);

        //ed.ChargeOffDate = UfebsDate(match.Groups[1].Value);
        //ed.ReceiptDate = match.Groups[2].Value);

        return match.Groups[1].Value.ToUfebsDate();
    }

    public static bool ParseTax(this string text, string num, out string value)
    {
        string pattern = $"/{num}/([^/]*)";
        var match = Regex.Match(text, pattern);

        if (match.Success)
        {
            value = match.Groups[1].Value;
            return true;
        }

        value = string.Empty;
        return false;
    }
}
