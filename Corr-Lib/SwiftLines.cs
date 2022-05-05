using System.Text.RegularExpressions;

namespace Corr_Lib;

internal class SwiftLines
{
    private const string NZP = "/NZP/";
    private const string NZZ = "//";

    public bool Translit { get; set; } = false;
    public bool Tax { get; set; } = false;
    public string Acc { get; set; } = string.Empty;
    public string INN { get; set; } = string.Empty;
    public string KPP { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Purpose { get; set; } = string.Empty;

    public string[] Lines
    {
        get => GetLines();
        set => SetLines(value); 
    }

    public void SetLines(string[] value)
    {
        bool field50 = false;
        bool field70 = false;
        bool field72 = false;
        bool nzpFound = false;

        foreach (var line in value)
        {
            var m = Regex.Match(line, @"^(:\d{2}\w?:|-})", RegexOptions.Compiled);
            if (m.Success)
            {
                field50 = false;
                field70 = false;
                field72 = false;

                string field = m.Value;
                switch (field)
                {
                    case ":20:":
                        Translit = line.StartsWith(field + "+");
                        break;

                    case ":26T:":
                        Tax = true;
                        break;

                    case ":50K:":
                        field50 = true;
                        Acc = line[6..];
                        break;

                    case ":70:":
                        field70 = true;
                        Purpose = line[4..];
                        break;

                    case ":72:":
                        field72 = true;
                        nzpFound = line[4..].StartsWith(NZP);
                        if (nzpFound)
                        {
                            Purpose += line[9..]; // :72:/NZP/...
                        }
                        break;

                    default:
                        break;
                }
            }
            else if (field50)
            {
                string pattern = @"^INN(?<INN>\d*)(|.KPP(?<KPP>\d*))$";
                m = Regex.Match(line, pattern);
                if (m.Success)
                {
                    INN = m.Groups["INN"].Value;
                    KPP = m.Groups["KPP"].Value;
                }
                else
                {
                    Name += line;
                }
            }
            else if (field70)
            {
                Purpose += line;
            }
            else if (field72)
            {
                if (nzpFound)
                {
                    if (line.StartsWith(NZZ))
                    {
                        Purpose += line[2..]; // //...
                    }
                    else
                    {
                        nzpFound = false;
                    }
                }
                else if (line.StartsWith(NZP))
                {
                    nzpFound = true;
                    Purpose += line[5..]; // /NZP/...
                }
            }
        }

        if (Translit)
        {
            Name = Swift.Cyr(Name);
            Purpose = Swift.Cyr(Purpose);
        }
    }

    public string[] GetLines()
    {
        List<string> list = new();

        bool skipUntilNextField = false;
        bool field72 = false;
        bool nzpFound = false;
        string text = string.Empty;

        foreach (string line in Lines)
        {
            var m = Regex.Match(line, @"^(:\d{2}\w?:|-})", RegexOptions.Compiled);
            if (m.Success)
            {
                if (field72 && !nzpFound)  // вариант /NZP/ не было
                {
                    if (text.Length > 35)
                    {
                        list.Add(NZP + text[..35]);
                        text = text[35..];
                        while (text.Length > 35)
                        {
                            list.Add(NZZ + text[..35]);
                            text = text[35..];
                        }
                        if (text.Length > 0)
                        {
                            list.Add(NZZ + text);
                            text = string.Empty;
                        }
                    }
                    else
                    {
                        list.Add(NZP + text);
                        text = string.Empty;
                    }

                    field72 = false;
                }

                skipUntilNextField = false;
                string field = m.Value;
                switch (field)
                {
                    case ":50K:":
                        list.Add($"{field}/{Acc}");
                        if (INN != null)
                        {
                            if (INN.Length < 12 && KPP != null)
                            {
                                list.Add($"INN{INN}.KPP{KPP}");
                            }
                            else
                            {
                                list.Add($"INN{INN}");
                            }
                        }
                        text = Translit ? Swift.Lat(Name) : Name;
                        for (int i = 0; i < 3; i++)
                        {
                            if (text.Length > 35)
                            {
                                list.Add(text[..35]);
                                text = text[35..];
                            }
                            else
                            {
                                list.Add(text);
                                break;
                            }
                        }
                        if (text.Length > 0)
                        {
                            //TODO alarm!
                        }
                        skipUntilNextField = true;
                        break;

                    case ":70:":
                        text = Translit ? Swift.Lat(Purpose) : Purpose;
                        if (text.Length > 210)
                        {
                            text = text[..210]; //TODO alarm!
                        }
                        if (text.Length > 35)
                        {
                            list.Add(field + text[..35]);
                            text = text[35..];
                            for (int i = 0; i < 3; i++)
                            {
                                if (text.Length > 35)
                                {
                                    list.Add(text[..35]);
                                    text = text[35..];
                                }
                                else
                                {
                                    list.Add(text);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            list.Add(field + text);
                        }
                        skipUntilNextField = true;
                        break;

                    case ":72:":
                        field72 = true;
                        if (line.StartsWith(field + NZP)) // вариант /NZP/ впереди других
                        {
                            nzpFound = true;
                            if (text.Length > 35)
                            {
                                list.Add(field + NZP + text[..35]);
                                text = text[35..];
                                while (text.Length > 35)
                                {
                                    list.Add(NZZ + text[..35]);
                                    text = text[35..];
                                }
                                if (text.Length > 0)
                                {
                                    list.Add(NZZ + text);
                                    text = string.Empty;
                                }
                            }
                            else
                            {
                                list.Add(field + NZP + text);
                                text = string.Empty;
                            }
                        }
                        break;

                    default:
                        list.Add(line);
                        break;
                }
            }
            else if (!skipUntilNextField)
            {
                if (field72)
                {
                    if (line.StartsWith(NZP)) // вариант /NZP/ среди других
                    {
                        nzpFound = true;
                        if (text.Length > 35)
                        {
                            list.Add(NZP + text[..35]);
                            text = text[35..];
                            while (text.Length > 35)
                            {
                                list.Add(NZZ + text[..35]);
                                text = text[35..];
                            }
                            if (text.Length > 0)
                            {
                                list.Add(NZZ + text);
                                text = string.Empty;
                            }
                        }
                        else
                        {
                            list.Add(NZP + text);
                            text = string.Empty;
                        }
                    }
                    else if (!nzpFound || (nzpFound && !line.StartsWith(NZZ))) // вариант других
                    {
                        list.Add(line);
                    }
                }
                else // не в поле 72
                {
                    list.Add(line);
                }
            }
        }

        return list.ToArray();
    }
}
