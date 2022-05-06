using System.Text.RegularExpressions;

namespace Corr_Lib;

public class SwiftLines
{
    private string[] _lines = Array.Empty<string>();

    /// <summary>
    /// В тексте документа применяется транслитерация по правилам SWIFT-RUR
    /// </summary>
    public bool Translit { get; set; } = false;

    /// <summary>
    /// Платеж осуществляется в бюджет - есть дополнительные поля и обработка
    /// </summary>
    public bool Tax { get; set; } = false;
    
    /// <summary>
    /// Счет плательщика / корсчет Банка
    /// </summary>
    public string Acc { get; set; } = string.Empty;

    /// <summary>
    /// ИНН плательщика
    /// </summary>
    public string INN { get; set; } = string.Empty;

    /// <summary>
    /// КПП плательщика
    /// </summary>
    public string KPP { get; set; } = string.Empty;

    /// <summary>
    /// Наименование плательщика
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Длина наименования плательщика на транслите
    /// </summary>
    public int NameLength { get; set; } = 0;

    /// <summary>
    /// Назначение платежа
    /// </summary>
    public string Purpose { get; set; } = string.Empty;

    /// <summary>
    /// Длина назначения платежа на транслите
    /// </summary>
    public int PurposeLength { get; set; } = 0;

    /// <summary>
    /// Массив строк документа
    /// </summary>
    public string[] Lines
    {
        get => GetLines();
        set => SetLines(value);
    }

    /// <summary>
    /// Текст документа
    /// </summary>
    public string Text
    {
        get => string.Join(Environment.NewLine, GetLines());
        set => SetLines(value.Split(Environment.NewLine));
    }

    /// <summary>
    /// Конструктор документа
    /// </summary>
    public SwiftLines()
    {
        //
    }

    /// <summary>
    /// Конструктор документа
    /// </summary>
    /// <param name="lines">Массив строк документа</param>
    public SwiftLines(string[] lines)
    {
        SetLines(lines);
    }

    /// <summary>
    /// Конструктор документа
    /// </summary>
    /// <param name="text">Текст документа</param>
    public SwiftLines(string text)
    {
        SetLines(text.Split(Environment.NewLine));
    }

    /// <summary>
    /// Чтение массива строк и разбор полей документа
    /// </summary>
    /// <param name="value">Массив строк</param>
    private void SetLines(string[] value)
    {
        _lines = value;

        bool field50 = false;
        bool field70 = false;
        bool field72 = false;

        bool innFound = false;
        bool nzpFound = false;

        foreach (string line in _lines)
        {
            // Проверка строки на начало раздела
            var m = Regex.Match(line, @"^(:\d{2}\w?:|-})", RegexOptions.Compiled);

            if (m.Success)
            {
                // Новый раздел

                field50 = false;
                field70 = false;
                field72 = false;

                switch (m.Value)
                {
                    case ":20:":
                        Translit = line.StartsWith(":20:+"); // Признак транслита
                        break;

                    case ":26T:":
                        Tax = true; // Признак платежа в бюджет
                        break;

                    case ":50K:":
                        field50 = true; // Начало раздела плательщика

                        if (line.StartsWith(":50K/"))
                        {
                            // Счет плательщика
                            Acc = line[6..]; // После ":50K:/"
                        }
                        break;

                    case ":70:":
                        field70 = true; // Начало раздела назначения платежа
                        Purpose = line[4..]; // Начало назначения платежа
                        break;

                    case ":72:":
                        field72 = true; // Начало раздела дополнительной информации
                        nzpFound = line.StartsWith(":72:/NZP/"); // Признак начала продолжения

                        if (nzpFound)
                        {
                            // Продолжение назначения платежа сверх 4 строк
                            Purpose += line[9..]; // После ":72:/NZP/"
                        }
                        break;

                    //case "-}": // eof
                    //    break;

                    default:
                        // Прочие разделы
                        break;
                }
            }
            else if (field50)
            {
                // Продолжение раздела плательщика
                if (innFound)
                {
                    // Продолжение наименования плательщика
                    Name += line;
                }
                else
                {
                    // Попытка прочесть строку с ИНН и КПП
                    innFound = true;
                    string pattern = @"^INN(?<INN>\d*)(|.KPP(?<KPP>\d*))$";
                    m = Regex.Match(line, pattern, RegexOptions.Compiled);

                    if (m.Success)
                    {
                        // ИНН
                        INN = m.Groups["INN"].Value;
                        // и опционально КПП
                        KPP = m.Groups["KPP"].Value;

                        // Следующей строкой пойдет наименование плательщика
                        Name = string.Empty;
                    }
                    else
                    {
                        // Иностранец без ИНН - первая строка наименования
                        Name = line;
                    }
                }
            }
            else if (field70)
            {
                // Продолжение раздела назначения платежа
                Purpose += line;
            }
            else if (field72)
            {
                // Продолжение раздела дополнительной информации
                if (nzpFound)
                {
                    if (line.StartsWith("//"))
                    {
                        // Продолжение назначения платежа
                        Purpose += line[2..]; // После "//"
                    }
                    else
                    {
                        // Конец чтения продолжений
                        nzpFound = false;
                    }
                }
                else if (line.StartsWith("/NZP/"))
                {
                    // Начало чтения продолжений
                    nzpFound = true;

                    // Продолжение назначения платежа
                    Purpose += line[5..]; // После "/NZP/"
                }
            }
        }

        // Длина символьных строк на латинице формата SWIFT
        NameLength = Name.Length;
        PurposeLength = Purpose.Length;

        if (Translit)
        {
            // Перевод на кирилицу
            Name = Swift.Cyr(Name);
            Purpose = Swift.Cyr(Purpose);
        }
    }

    /// <summary>
    /// Создание текста документа
    /// </summary>
    /// <returns>Массив строк</returns>
    private string[] GetLines()
    {
        List<string> list = new();

        bool skipUntilNextField = false;
        bool field72 = false;
        bool field72found = false;
        bool field72required = false;
        bool nzpFound = false;

        string? textPurpose = string.Empty;

        foreach (string line in _lines)
        {
            // Проверка строки на начало раздела
            var m = Regex.Match(line, @"^(:\d{2}\w?:|-})", RegexOptions.Compiled);

            if (m.Success)
            {
                // Новый раздел

                if (field72 && !nzpFound)
                {
                    // Вставка перед новым разделом ненайденного в предыдущем поля /NZP/
                    if (textPurpose.Length > 35)
                    {
                        // Первая строка продолжения
                        list.Add("/NZP/" + textPurpose[..35]);
                        textPurpose = textPurpose[35..];

                        if (textPurpose.Length > 0)
                        {
                            // Вторая строка продолжения
                            list.Add("//" + textPurpose);
                        }
                    }
                    else
                    {
                        // Продолжение влезает на одну строку
                        list.Add("/NZP/" + textPurpose);
                    }

                    // Текст весь размещен
                    textPurpose = string.Empty;
                    field72 = false;
                }

                skipUntilNextField = false;

                switch (m.Value)
                {
                    case ":50K:":
                        // Строка счета
                        list.Add(":50K:/" + Acc);

                        // Строка ИНН и КПП, если они есть
                        if (INN != null)
                        {
                            string inn = "INN" + INN;

                            // КПП только у юрлиц
                            if (INN.Length == 10 && KPP != null)
                            {
                                inn += ".KPP" + KPP;
                            }

                            list.Add(inn);
                        }

                        // Перекодировка наименования плательщика
                        string textName = Translit ? Swift.Lat(Name) : Name;

                        // До трех строк наименования плательщика
                        for (int i = 0; i < 3; i++)
                        {
                            if (textName.Length > 35)
                            {
                                list.Add(textName[..35]);
                                textName = textName[35..];
                            }
                            else
                            {
                                list.Add(textName);
                                break;
                            }
                        }

                        // Если наименование плательщика не влезло в три строки
                        if (textName.Length > 0)
                        {
                            //TODO alarm!
                            textName = string.Empty;
                        }

                        skipUntilNextField = true;
                        break;

                    case ":70:":
                        // Перекодировка назначения платежа
                        textPurpose = Translit ? Swift.Lat(Purpose) : Purpose;

                        // Если назначение платежа обрезается по лимиту
                        if (textPurpose.Length > 210)
                        {
                            textPurpose = textPurpose[..210]; //TODO alarm!
                        }

                        // До четырех строк в разделе назначения платежа
                        if (textPurpose.Length > 35)
                        {
                            // Первая строка
                            list.Add(":70:" + textPurpose[..35]);
                            textPurpose = textPurpose[35..];

                            // Следующие три строки
                            for (int i = 0; i < 3; i++)
                            {
                                if (textPurpose.Length > 35)
                                {
                                    // Продолжение на новую строку
                                    list.Add(textPurpose[..35]);
                                    textPurpose = textPurpose[35..];
                                }
                                else
                                {
                                    // Остаток текста
                                    list.Add(textPurpose);
                                    textPurpose = string.Empty;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            // Назначение платежа влезает на одну строку без переносов
                            list.Add(":70:" + textPurpose);
                            textPurpose = string.Empty;
                        }

                        skipUntilNextField = true;
                        break;

                    case ":72:":
                        field72 = true;
                        field72found = true;

                        if (line.StartsWith(":72:/NZP/")) // вариант, если /NZP/ впереди других
                        {
                            // Найдено и больше не искать
                            nzpFound = true;

                            if (textPurpose.Length == 0)
                            {
                                // Первое место пустого /NZP/ должен занять другой
                                field72required = true;
                            }
                            else if (textPurpose.Length > 35)
                            {
                                // Первая строка продолжения
                                list.Add(":72:/NZP/" + textPurpose[..35]);
                                textPurpose = textPurpose[35..];

                                if (textPurpose.Length > 0)
                                {
                                    // Вторая строка продолжения
                                    list.Add("//" + textPurpose);
                                }
                            }
                            else
                            {
                                // Продолжение влезает на одну строку
                                list.Add(":72:/NZP/" + textPurpose);
                            }

                            // Текст весь размещен
                            textPurpose = string.Empty;
                        }
                        else
                        {
                            // Прочие строки
                            list.Add(line);
                        }
                        break;

                    case "-}": // eof
                        if (!field72found && textPurpose.Length > 0)
                        {
                            // Конец файла - вставка несуществовавшего в нем раздела
                            if (textPurpose.Length > 35)
                            {
                                // Первая строка продолжения
                                list.Add(":72:/NZP/" + textPurpose[..35]);
                                textPurpose = textPurpose[35..];

                                if (textPurpose.Length > 0)
                                {
                                    // Вторая строка продолжения
                                    list.Add("//" + textPurpose);
                                }
                            }
                            else
                            {
                                // Продолжение влезает на одну строку
                                list.Add(":72:/NZP/" + textPurpose);
                            }

                            // Текст весь размещен
                            textPurpose = string.Empty;
                            field72found = true;
                        }

                        // Конец файла
                        list.Add(line);
                        break;

                    default:
                        // Прочие разделы
                        list.Add(line);
                        break;
                }
            }
            else if (!skipUntilNextField)
            {
                if (field72)
                {
                    if (line.StartsWith("/NZP/")) // вариант, что /NZP/ среди других
                    {
                        // Найдено и больше не искать
                        nzpFound = true;

                        if (textPurpose.Length == 0)
                        {
                            // Пустой /NZP/ больше не нужен
                        }
                        else if (textPurpose.Length > 35)
                        {
                            // Первая строка продолжения
                            list.Add("/NZP/" + textPurpose[..35]);
                            textPurpose = textPurpose[35..];

                            if (textPurpose.Length > 0)
                            {
                                // Вторая строка продолжения
                                list.Add("//" + textPurpose);
                            }
                        }
                        else
                        {
                            // Продолжение влезает на одну строку
                            list.Add("/NZP/" + textPurpose);
                        }

                        // Текст весь размещен
                        textPurpose = string.Empty;
                    }
                    else if (!nzpFound || (nzpFound && !line.StartsWith("//"))) // вариант других
                    {
                        // Прочие строки
                        if (field72required)
                        {
                            // Вместо /NZP/ на первом месте надо разместить другую строку
                            field72required = false;
                            list.Add(":72:" + line);
                        }
                        else
                        {
                            list.Add(line);
                        }
                    }
                }
                else // не в поле 72
                {
                    // Прочие строки
                    list.Add(line);
                }
            }
        }

        return list.ToArray();
    }
}
