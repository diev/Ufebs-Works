using System.Text.RegularExpressions;

namespace Corr_Lib;

public class SwiftText
{
    private const int _startPayer = 4;
    private const int _startPurpose = 10;
    
    private static readonly Regex _regex = new(@"^:\d{2}\w{0,1}:", RegexOptions.Compiled);

    public const string CORR = "30109810800010001378";
    public const string INN = "7831001422";
    public const string KPP = "784101001";

    //                         "1                                35"!
    public const string BANK = "AO mSITI INVEST BANKm INN 7831001422";

    public const int NameMaxLength = 160;
    public const int PurposeMaxLength = 210;

    public List<string> Lines { get; set; } = new();
    public string PayerName { get => GetPayerName(); set => SetPayerName(value); }
    public string Purpose { get => GetPurpose(); set => SetPurpose(value); }
    public bool Tax => IfExists(":77B:");


    public SwiftText()
    { }

    public SwiftText(string[] lines)
    {
        Parse(lines);
    }

    public void Parse(string[] lines)
    {
        Lines = new List<string>(lines);
        MakeOurSwiftText();
    }

    public string[] GetLines() =>
        Lines.ToArray();

    public bool IfExists(string find) =>
        Lines.Exists(s => s.StartsWith(find, StringComparison.Ordinal));

    public int FindLine(int start, string find) =>
        Lines.FindIndex(start + 1, s => s.StartsWith(find, StringComparison.Ordinal));

    public int CountLines(int start, int count) =>
        Lines.FindIndex(start + 1, /*count + 10,*/ s => _regex.IsMatch(s)) - start; //??

    public string Read35(int start, int count) =>
        string.Join(string.Empty, Lines.GetRange(start, count));
            //.Replace("  ", " ");

    public static List<string> Wrap35(string text)
    {
        var list = new List<string>(PurposeMaxLength);

        while (text.Length > 35)
        {
            list.Add(text[..35]);
            text = text[35..];
        }

        if (text.Length > 0)
        {
            list.Add(text);
        }

        return list;
    }

    public void Replace(int start, int count, List<string> list)
    {
        Lines.RemoveRange(start, count);
        Lines.InsertRange(start, list);
    }

    public void MakeOurSwiftText()
    {
        // Ищем номер строки с Плательщиком
        int n = FindLine(_startPayer, ":50K:");

        // Получаем номер счета
        string pattern = @"^:50K:/(\d*)";
        var match = Regex.Match(Lines[n], pattern);
        string acc = match.Groups[1].Value;

        // Меняем номер счета на Корсчет
        Lines[n] = $":50K:/{CORR}";

        // Убираем КПП у ИП и физиков (где ИНН 12 цифр) в следующей строке
        string inn = INN; // ИНН Банка по умолчанию
        if (Lines[++n].Contains(".KPP"))
        {
            pattern = @"^INN(\d*).KPP(\d*)$";
            match = Regex.Match(Lines[n], pattern);
            inn = match.Groups[1].Value;

            if (inn.Length == 12)
            {
                Lines[n] = $"INN{inn}";
            }
        }

        // Если ИНН не Банка (или платеж зарубеж...)
        if (inn != INN)
        {
            // Получаем прежнее Наименование плательщика (1-3 строки по 35, плюс может быть ниже пара строк)
            string payer = PayerName;

            // Вставляем в Наименование плательщика наш Банк с ИНН
            // Заключаем в скобки наименование Клиента вместе с его номером счета
            string name = $"{BANK} ({payer} R/S {acc})";

            if (name.Length > NameMaxLength)
            {
                //?? требуется творческая работа
            }

            // Заменяем Наименование
            PayerName = name;

            // Если у Клиента платеж в бюджет
            if (Tax)
            {
                // Получаем прежнее Назначение (1-4 строки по 35, плюс может быть ниже пара строк)
                string purpose = Purpose;

                // Вставляем признак оплаты за третье лицо (ИНН и КПП Банка, плюс наименование лица)
                purpose = $"//{INN}//{KPP}//{payer}//{purpose}"; // первые два "//" спорны в стандартах

                if (purpose.Length > PurposeMaxLength)
                {
                    //?? требуется творческая работа
                }

                // Заменяем Назначение
                Purpose = purpose;
            }
        }
    }

    public string GetPayerName()
    {
        int n = FindLine(_startPayer, ":50K:");

        if (Lines[++n].StartsWith("INN"))
        {
            ++n;
        }

        int count = CountLines(n, 3);
        string value = Read35(n, count); // а если есть продолжение ниже??

        return value;
    }

    public void SetPayerName(string value)
    {
        int n = FindLine(_startPayer, ":50K:");

        if (Lines[++n].StartsWith("INN"))
        {
            ++n;
        }

        int count = CountLines(n,3);
        var list = Wrap35(value);

        Replace(n, count, list); // а если есть продолжение ниже??
    }

    public string GetPurpose()
    {
        int n = FindLine(_startPurpose, ":70:");
        int count = CountLines(n, 4);
        string value = Read35(n, count); // а если есть продолжение ниже??

        return value[4..]; // strip ":70:"
    }

    public void SetPurpose(string value)
    {
        int n = FindLine(_startPurpose, ":70:");
        int count = CountLines(n, 4); //??
        var list = Wrap35(value);
        list[0] = $":70:{list[0]}";

        Replace(n, count, list); // а если есть продолжение ниже??
    }
}
