#region License
/*
Copyright 2022 Dmitrii Evdokimov

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

using CorrLib;

using System.Drawing.Printing;
using System.Text;
using System.Xml.Linq;

namespace CorrSWIFT;

public partial class Form1 : Form
{
    private const string VersionDate = "2022-06-07";

    // private const int MAX_NAME = 3 * 35; // 105 (SWIFT-RUR) или 160 (УФЭБС)?
    private const int MAX_PURPOSE = 210;

    private bool _isNameValid = false;
    private bool _isPurposeValid = false;

    private bool _saved = false;
    private string? _saveFileName;
    private string _saveMaskName = "*";

    private SwiftLines _swift = new();

    #region Init
    public Form1()
    {
        InitializeComponent();
        InitForm();
    }

    private void InitForm()
    {
        int w = Screen.PrimaryScreen.WorkingArea.Width;
        int h = Screen.PrimaryScreen.WorkingArea.Height;

        SetBounds(
            (int)(w * 0.1), (int)(h * 0.15),
            (int)(w * 0.8), (int)(h * 0.75));

        // runtimeconfig.template.json > App.runtimeconfig.json

        // exe G:\BANK\TEST\OUT\r*.xml G:\BANK\TEST\CLI\*_.txt

        string[] args = Environment.GetCommandLineArgs(); // 0:exe 1:[Input|*] 2:[Output_|\]
        int argc = args.Length - 1;

        if (argc > 0) // 1:Input
        {
            string arg = Path.GetFullPath(args[1]);
            ConfigProperties.OpenDir = Path.GetDirectoryName(arg) ?? @"C:\";
            ConfigProperties.OpenMask = Path.GetFileName(arg);

            if (argc > 1) // 2:Output
            {
                arg = Path.GetFullPath(args[2]);
                ConfigProperties.SaveDir = Path.GetDirectoryName(arg) ?? @"C:\";
                ConfigProperties.SaveMask = Path.GetFileName(arg);
            }
        }

        ReInitForm();
    }

    private void ReInitForm()
    {
        StringBuilder err = new();

        if (string.IsNullOrEmpty(ConfigProperties.OpenDir) || !Directory.Exists(ConfigProperties.OpenDir))
        {
            err.AppendLine($"Папка OpenDir не существует!");
            //ConfigProperties.OpenDir = Directory.GetCurrentDirectory();
        }

        if (string.IsNullOrEmpty(ConfigProperties.OpenMask))
        {
            err.AppendLine($"Маска OpenMask не указана!");
            //ConfigProperties.OpenMask = "r*.xml";
        }

        if (string.IsNullOrEmpty(ConfigProperties.SaveDir) || !Directory.Exists(ConfigProperties.SaveDir))
        {
            err.AppendLine($"Папка SaveDir не существует!");
            //ConfigProperties.SaveDir = ConfigProperties.OpenDir;
        }

        if (string.IsNullOrEmpty(ConfigProperties.SaveMask))
        {
            err.AppendLine($"Маска SaveMask не указана!");
            //ConfigProperties.SaveMask = "*_.txt";
        }

        if (string.IsNullOrEmpty(ConfigProperties.BankAccount))
        {
            err.AppendLine($"Счет Банка не указан!");
            //ConfigProperties.BankAccount = "12345678901234567890";
        }

        if (string.IsNullOrEmpty(ConfigProperties.BankINN))
        {
            err.AppendLine($"ИНН Банка не указан!");
            //ConfigProperties.BankINN = "7831001422";
        }

        if (string.IsNullOrEmpty(ConfigProperties.BankKPP))
        {
            err.AppendLine($"КПП Банка не указан!");
            //ConfigProperties.BankKPP = "783101001";
        }

        if (string.IsNullOrEmpty(ConfigProperties.BankPayerTemplate))
        {
            err.AppendLine($"Шаблон за клиента Банка не указан!");
            //ConfigProperties.BankPayerTemplate = "АО \"Сити Инвест Банк\" ИНН 7831001422 ({name} р/с {acc})";
        }

        if (string.IsNullOrEmpty(ConfigProperties.BankPurposeTemplate))
        {
            err.AppendLine($"Шаблон назначения за третье лицо не указан!");
            //ConfigProperties.BankPurposeTemplate = "//7831001422//783101001//{name}//{purpose}";
        }

        if (err.Length > 0)
        {
            MessageBox.Show($"Проверьте настройки:\n\n{err}", Application.ProductName,
                MessageBoxButtons.OK, MessageBoxIcon.Error);

            ConfigMenuItem.PerformClick();
        }

        OpenFileDialog.InitialDirectory = ConfigProperties.OpenDir;
        OpenFileDialog.Filter = $"УФЭБС|{ConfigProperties.OpenMask}|{OpenFileDialog.Filter}";

        SaveAsFileDialog.InitialDirectory = ConfigProperties.SaveDir;
        SaveAsFileDialog.Filter = $"SWIFT|{ConfigProperties.SaveMask}|{SaveAsFileDialog.Filter}";
        SaveAsFileDialog.DefaultExt = Path.GetExtension(ConfigProperties.SaveMask);

        _saveMaskName = Path.GetFileName(ConfigProperties.SaveMask);

        FilesListBox.Items.Clear();
        //FilesListBox.Items.AddRange(Directory.GetFiles(ConfigProperties.OpenDir, ConfigProperties.OpenMask));

        foreach (string file in Directory.GetFiles(ConfigProperties.OpenDir, ConfigProperties.OpenMask))
        {
            var item = new ListViewItem(file);
            item.SubItems.Add("-");
            FilesListBox.Items.Add(item);
        }

        FilesListBox.SelectFirst();

        var saved = Directory.GetFiles(ConfigProperties.SaveDir, ConfigProperties.SaveMask);

        if (saved.Length > 0)
        {
            var reply = MessageBox.Show($"В выходной директории\n{ConfigProperties.SaveDir}\nуже есть {saved.Length} файлов {ConfigProperties.SaveMask}.\n\nОни будут перезаписаны при сохранениях!\nУдалить их?",
                Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            if (reply == DialogResult.Yes)
            {
                foreach (var file in saved)
                {
                    File.Delete(file);
                }
            }
        }
    }
    #endregion Init

    #region Dialogs
    private void OpenFilesOK()
    {
        FilesListBox.Items.Clear();
        //FilesListBox.Items.AddRange(OpenFileDialog.FileNames);

        foreach (string file in OpenFileDialog.FileNames)
        {
            var item = new ListViewItem(file);
            item.SubItems.Add("-");
            FilesListBox.Items.Add(item);
        }

        FilesListBox.SelectFirst();
    }

    private void SaveFileOK()
    {
        _saveFileName = SaveAsFileDialog.FileName;

        switch (Tabs.SelectedIndex)
        {
            case 0:
                SaveFile(string.Join(Environment.NewLine,
                    FilesListBox.Items.OfType<string>().ToArray()));
                return;

            case 1:
                SaveFile(XmlTextBox.Text);
                break;

            case 2:
                SaveFile(SwiftTextBox.Text);
                break;

            case 3:
                SaveFile();
                break;
        }

        //FilesListBox.Items[FilesListBox.SelectedIndex] = _saveFileName;
        //FilesListBox.SelectedItem = _saveFileName;
        _saved = true;
        SavedLabel.Text = _saved ? "Сохранен" : "Не сохранен";
    }

    private void FontOK()
    {
        var font = FontDialog.Font;

        FilesListBox.Font = font;
        XmlTextBox.Font = font;
        SwiftTextBox.Font = font;
        OutTextBox.Font = font;
        NameTextBox.Font = font;
        PurposeTextBox.Font = font;
    }
    #endregion Dialogs

    #region Actions
    private void FileSelected()
    {
        var list = FilesListBox;
        var selected = list.SelectedItem();

        if (selected is null)
        {
            return;
        }

        string? file = selected.Text;

        if (file is null)
        {
            return;
        }

        //if (file.Contains('>'))
        //{
        //    file = file.Split('>')[0].Trim();
        //}

        if (File.Exists(file))
        {
            LoadFile(file);

            int index = list.SelectedIndex() + 1;
            int total = list.Items.Count;

            FilesDoneBar.Value = index;
            FilesDoneBar.Maximum = total;

            string s = $"{index}/{total}";
            FilesDoneValue.Text = s;

            FilesPage.Text = $"Файлы {s}";
        }
    }

    private bool LoadFile(string path)
    {
        string text = File.ReadAllText(path, Encoding.ASCII);
        string filename = Path.GetFileNameWithoutExtension(path);
        string ext = Path.GetExtension(path);

        SaveAsFileDialog.FileName = _saveMaskName.Replace("*", filename);
        _saveFileName = Path.Combine(SaveAsFileDialog.InitialDirectory, SaveAsFileDialog.FileName);
        MarkSaved();

        // Заголовок окна

        Text = $"{Application.ProductName} | {path}";

        // Исходный текст XML

        if (ext.Equals(".xml", StringComparison.OrdinalIgnoreCase))
        {
            //XmlTextBox.Text = text; //no 1251!

            var xdoc = XDocument.Load(path);
            var root = xdoc.Root;

            if (root == null)
            {
                XmlTextBox.Text = "Not XML file";
            }
            else
            {
                XNamespace ns = root.GetDefaultNamespace();
                XmlTextBox.Text = xdoc.ToString();

                foreach (var node in root.Elements())
                {
                    #region Get Values
                    // <ED101 ... EDDate="2022-04-13" EDNo="10442" Sum="164839" TransKind="01" xmlns="urn:cbr-ru:ed:v2.0">
                    var ED = (XElement)node;
                    XAttribute? EDNo = ED.Attribute(nameof(EDNo));
                    XAttribute? Sum = ED.Attribute(nameof(Sum));
                    XAttribute? TransKind = ED.Attribute(nameof(TransKind));

                    // <AccDoc AccDocDate="2022-04-12" AccDocNo="1517"/>
                    XElement? AccDoc = ED.Element(ns + nameof(AccDoc));
                    XAttribute? AccDocNo = AccDoc?.Attribute(nameof(AccDocNo));

                    // <Payer INN="470608634408" KPP="470301001" PersonalAcc="30109810300000000063">
                    XElement? Payer = ED.Element(ns + nameof(Payer));
                    XAttribute? INN = Payer?.Attribute(nameof(INN));
                    XAttribute? KPP = Payer?.Attribute(nameof(KPP));
                    XAttribute? PersonalAcc = Payer?.Attribute(nameof(PersonalAcc));

                    // <Name>Ван ден Бринк Виллеке</Name>
                    XElement? Name = Payer?.Element(ns + nameof(Name));

                    // <Bank BIC="044525769" CorrespAcc="30109810300000000063"/>
                    XElement? Bank = Payer?.Element(ns + nameof(Bank));

                    XAttribute? BIC = Bank?.Attribute(nameof(BIC));
                    XAttribute? CorrespAcc = Bank?.Attribute(nameof(CorrespAcc));

                    // <Payee INN="470608634408" KPP="470301001" PersonalAcc="30109810300000000063">
                    XElement? Payee = ED.Element(ns + nameof(Payee));
                    XAttribute? PayeeINN = Payee?.Attribute(nameof(INN));
                    XAttribute? PayeeKPP = Payee?.Attribute(nameof(KPP));
                    //XAttribute? PayeePersonalAcc = Payee?.Attribute(nameof(PersonalAcc));
                    
                    XElement? PayeeName = Payee?.Element(ns + nameof(Name));

                    // <Purpose>Взносы по страхованию...</Purpose>
                    XElement? Purpose = ED.Element(ns + nameof(Purpose));

                    // <DepartmentalInfo CBC="39310202050071000160" DocDate="0" DocNo="0" DrawerStatus="13" OKATO="41625156" PaytReason="0" TaxPaytKind="0" TaxPeriod="0"/>
                    XElement? DepartmentalInfo = ED.Element(ns + nameof(DepartmentalInfo));
                    XAttribute? TaxPaytKind = DepartmentalInfo?.Attribute(nameof(TaxPaytKind));
                    #endregion Get Values

                    var item = new ListViewItem(ED.Name.LocalName);
                    
                    item.SubItems.Add(Sum.Value);
                    item.SubItems.Add(Name.Value);
                    item.SubItems.Add(PayeeName.Value);
                    item.SubItems.Add(Purpose.Value);

                    DocsListBox.Items.Add(item);
                    OutDocsListBox.Items.Add((ListViewItem)item.Clone());
                }
            }

            text = SwiftHelper.GetSwiftDocument(text) ?? "No SwiftDocument";
        }
        else
        {
            XmlTextBox.Text = "No XML file";
        }

        // Исходный текст документа SWIFT

        SwiftTextBox.Text = text;
        _swift = new SwiftLines(SwiftTextBox.Lines);

        if (text is null)
        {
            SwiftTextBox.Text = "<SWIFTDocument> не содержит текста.";
        }

        // Переключаемся на последнюю закладку "К отправке"

        Tabs.SelectedIndex = Tabs.TabCount - 1;

        // Читаем значения из текста формата документа SWIFT-RUR

        var acc = _swift.Account;
        var inn = _swift.INN;
        //var kpp = _swift.KPP;
        var name = _swift.Name;

        // Если ИНН плательщика наш (как в Параметрах), то это платеж от нашего Банка самого

        bool bank = inn == ConfigProperties.BankINN; // "7831001422";

        // Берем из Параметров номер корсчета

        string acc2 = ConfigProperties.BankAccount; // "30109810800010001378";

        // Подставляем новые значения плательщика в шаблон, если не Банк сам за себя

        // $"АО \"Сити Инвест Банк\" ИНН 7831001422 ({name} р/с {acc})";
        string name2 = bank
            ? name
            : ConfigProperties.BankPayerTemplate
            .Replace("{name}", name)
            .Replace("{acc}", acc);

        // Длина не превышает предельную?

        _isNameValid = name2.Length <= ConfigProperties.BankPayerLimit; // MAX_NAME;

        // Присваиваем новые значения для генерации нового теста документа

        _swift.Account = acc2;
        _swift.Name = name2;

        // Берем назначение из документа

        string purpose = _swift.Purpose;

        // Если не Банк сам и есть признак платежа в бюджет

        if (!bank && _swift.Tax)
        {
            // Подставляем новые значения назначения в шаблон платежа за третье лицо

            // $"//7831001422//784101001//{name}//{purpose}";
            purpose = ConfigProperties.BankPurposeTemplate
                .Replace("{name}", name)
                .Replace("{purpose}", purpose);

            _swift.Purpose = purpose;
        }

        // Заполняем текстбоксы

        OutTextBox.Lines = _swift.Lines; // Новый текст документа SWIFT
        NameTextBox.Text = name2; // Текст наименования плательщика
        PurposeTextBox.Text = purpose; // Текст назначения платежа

        // Пишем в статусную строку тип платежа

        TaxValue.Text = _swift.Tax ? "Бюджет" : "Платеж";

        // Выставляем доступность пунктов меню и кнопок

        return CheckItemsEnabled();
    }

    private void SaveFile(string? text = null)
    {
        if (_saveFileName != null)
        {
            File.WriteAllText(_saveFileName, text ?? OutTextBox.Text, Encoding.ASCII);
            MarkSaved();
        }
    }

    private void MarkSaved()
    {
        _saved = File.Exists(_saveFileName);

        SavedLabel.Text = _saved
            ? $"Сохранен в {_saveFileName}"
            : $"Не сохранен (в {_saveFileName})";

        var item = FilesListBox.SelectedItem();
        item.SubItems[1].Text = _saveFileName;
    }

    private void PrintPage(PrintPageEventArgs e)
    {
        string documentContents = OutTextBox.Text;
        string stringToPrint = documentContents;

        if (e.Graphics != null)
        {
            // Sets the value of charactersOnPage to the number of characters
            // of stringToPrint that will fit within the bounds of the page.
            e.Graphics.MeasureString(stringToPrint, Font,
                e.MarginBounds.Size, StringFormat.GenericTypographic,
                out int charactersOnPage, out int linesPerPage);

            // Draws the string within the bounds of the page.
            e.Graphics.DrawString(stringToPrint, Font, Brushes.Black,
            e.MarginBounds, StringFormat.GenericTypographic);

            // Remove the portion of the string that has been printed.
            stringToPrint = stringToPrint[charactersOnPage..];

            // Check to see if more pages are to be printed.
            e.HasMorePages = (stringToPrint.Length > 0);

            // If there are no more pages, reset the string to be printed.
            if (!e.HasMorePages)
            {
                stringToPrint = documentContents;
            }
        }
    }

    private bool CheckItemsEnabled()
    {
        bool enabled = FilesListBox.PrevEnabled();

        PrevMenuItem.Enabled = enabled;
        PrevButton.Enabled = enabled;

        enabled =
            _isNameValid &&
            _isPurposeValid &&
            FilesListBox.NextEnabled();

        NextMenuItem.Enabled = enabled;
        NextButton.Enabled = enabled;

        ForwardMenuItem.Enabled = enabled;
        ForwardButton.Enabled = enabled;

        return enabled;
    }

    private void TryClose(ref FormClosingEventArgs e)
    {
        if (!_saved && _saveFileName != null)
        {
            var reply = MessageBox.Show($"Сохранить файл \n{_saveFileName}\nперед выходом?",
                Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

            switch (reply)
            {
                case DialogResult.Yes:
                    SaveFile();
                    break;

                case DialogResult.No:
                    _saved = true;
                    break;

                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }
    }
    private static void About()
    {
        string config = Path.ChangeExtension(Application.ExecutablePath, "runtimeconfig.json");
        string text =
            $@"Программа дооформления документов из УФЭБС в SWIFT.

Версия {Application.ProductVersion} ({VersionDate})

Задайте параметры в меню Файл\Параметры...
Сохраняются они в файле
{config}

Также пути можно переопределить в командной строке:
    Input\[*.xml] [Output\[*_.txt]]";

        MessageBox.Show(text, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    #endregion Actions

    #region TextEdits
    private void OutputChanged()
    {
        int limit = ConfigProperties.BankPayerLimit; // MAX_NAME;

        //if (OutTextBox.Focused && OutEditCheck.Checked)
        if (ChangeMenuItem.Checked)
        {
            _swift.Lines = OutTextBox.Lines;
            _swift.NameLimit = limit;
            //OutTextBox.Lines = _swift.Lines;

            NameTextBox.Text = _swift.Name;
            PurposeTextBox.Text = _swift.Purpose;
        }

        CheckItemsEnabled();
    }

    private void NameChanged()
    {
        int limit = ConfigProperties.BankPayerLimit; // MAX_NAME;

        //if (NameTextBox.Focused && !OutEditCheck.Checked)
        if (!ChangeMenuItem.Checked)
        {
            _swift.Name = NameTextBox.Text;
            _swift.NameLimit = limit;
            OutTextBox.Lines = _swift.Lines;
        }

        int length = NameTextBox.TextLength;
        _isNameValid = length <= limit;

        NameEditLabel.Text = $"Плательщик {length}/{limit}:";
        NameEditLabel.ForeColor = _isNameValid ? ForeColor : Color.Red;

        CheckItemsEnabled();
    }

    private void PurposeChanged()
    {
        //if (PurposeTextBox.Focused && !ChangeMenuItem.Checked)
        if (!ChangeMenuItem.Checked)
        {
            _swift.Purpose = PurposeTextBox.Text;
            OutTextBox.Lines = _swift.Lines;
        }

        int length = PurposeTextBox.TextLength;
        _isPurposeValid = length <= MAX_PURPOSE;

        PurposeEditLabel.Text = $"Назначение {length}/{MAX_PURPOSE}:";
        PurposeEditLabel.ForeColor = _isPurposeValid ? ForeColor : Color.Red;

        CheckItemsEnabled();
    }
    #endregion TextEdits

    #region Buttons
    private void GoPrev()
    {
        if (!_saved)
        {
            var reply = MessageBox.Show($"Сохранить файл\n{_saveFileName}\nперед шагом назад?",
                Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

            if (reply == DialogResult.Yes)
            {
                SaveFile();
            }
            else if (reply == DialogResult.Cancel)
            {
                return;
            }
        }

        FilesListBox.SelectPrev();
    }

    private void GoNext()
    {
        SaveFile();

        if (!FilesListBox.SelectNext() &&
            MessageBox.Show($"Сделано: {FilesListBox.Items.Count}.\nЗакрыть программу?", Application.ProductName,
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
        {
            Close();
        }
    }

    private void GoForward()
    {
        while (
            _isNameValid &&
            _isPurposeValid &&
            FilesListBox.NextEnabled())
        {
            //GoNext(); //зацикливается и не завершает программу!

            SaveFile();

            if (!FilesListBox.SelectNext() &&
                MessageBox.Show($"Сделано: {FilesListBox.Items.Count}.\nЗакрыть программу?", Application.ProductName,
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                Close();
                break;
            }
        }
    }
    #endregion Buttons

    #region UI
    private void OpenMenuItem_Click(object sender, EventArgs e)
    {
        OpenFileDialog.ShowDialog();
    }

    private void ExitMenuItem_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void SaveAsMenuItem_Click(object sender, EventArgs e)
    {
        SaveAsFileDialog.ShowDialog();
    }

    private void FontMenuItem_Click(object sender, EventArgs e)
    {
        FontDialog.Font = OutTextBox.Font;
        FontDialog.ShowDialog();
    }

    private void OutTextBox_TextChanged(object sender, EventArgs e)
    {
        OutputChanged();
    }

    private void NameTextBox_TextChanged(object sender, EventArgs e)
    {
        NameChanged();
    }

    private void PurposeTextBox_TextChanged(object sender, EventArgs e)
    {
        PurposeChanged();
    }

    private void SaveMenuItem_Click(object sender, EventArgs e)
    {
        SaveFile();
    }

    private void OpenFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
    {
        OpenFilesOK();
    }

    private void SaveAsFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
    {
        SaveFileOK();
    }

    private void FontDialog_Apply(object sender, EventArgs e)
    {
        FontOK();
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        TryClose(ref e);
    }

    private void AboutMenuItem_Click(object sender, EventArgs e)
    {
        About();
    }

    private void FilesListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        FileSelected();
    }

    private void NewFileMenuItem_Click(object sender, EventArgs e)
    {
        //TODO
    }

    private void PrintMenuItem_Click(object sender, EventArgs e)
    {
        PrintDocument.Print();
    }

    private void PrintPreviewMenuItem_Click(object sender, EventArgs e)
    {
        PrintDocument.DocumentName = _saveFileName ?? "Файл для печати";
        PrintPreviewDialog.ShowDialog();
    }

    private void UndoMenuItem_Click(object sender, EventArgs e)
    {
        OutTextBox.Undo();
    }

    private void RedoMenuItem_Click(object sender, EventArgs e)
    {
        //TODO
    }

    private void CutMenuItem_Click(object sender, EventArgs e)
    {
        OutTextBox.Cut();
    }

    private void CopyMenuItem_Click(object sender, EventArgs e)
    {
        OutTextBox.Copy();
    }

    private void PasteMenuItem_Click(object sender, EventArgs e)
    {
        OutTextBox.Paste();
    }

    private void SelectAllMenuItem_Click(object sender, EventArgs e)
    {
        OutTextBox.SelectAll();
    }

    private void PrevMenuItem_Click(object sender, EventArgs e)
    {
        GoPrev();
    }

    private void NextMenuItem_Click(object sender, EventArgs e)
    {
        GoNext();
    }

    private void ForwardMenuItem_Click(object sender, EventArgs e)
    {
        GoForward();
    }

    private void ChangeMenuItem_Click(object sender, EventArgs e)
    {
        ChangeMenuItem.Checked = !ChangeMenuItem.Checked;
        OutEditCheck.Checked = ChangeMenuItem.Checked;
    }

    private void WrapMenuItem_Click(object sender, EventArgs e)
    {
        WrapMenuItem.Checked = !WrapMenuItem.Checked;
        XmlTextBox.WordWrap = WrapMenuItem.Checked;
        OutTextBox.WordWrap = WrapMenuItem.Checked;
    }

    private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {
        PrintPage(e);
    }

    private void ConfigMenuItem_Click(object sender, EventArgs e)
    {
        ConfigForm configForm = new();
        configForm.ShowDialog();

        ReInitForm();
    }

    private void ChangeMenuItem_CheckedChanged(object sender, EventArgs e)
    {
        bool check = ChangeMenuItem.Checked;
        OutTextBox.ReadOnly = !check;
        NameTextBox.ReadOnly = check;
        PurposeTextBox.ReadOnly = check;
    }
    #endregion UI

    private void FilesListBox_SizeChanged(object sender, EventArgs e)
    {
        var width = FilesListBox.ClientSize.Width / 2;
        InColumn.Width = width;
        OutColumn.Width = width;
    }

    private void Tabs_Selected(object sender, TabControlEventArgs e)
    {

    }

    private void FilesListBox_Click(object sender, EventArgs e)
    {
        FileSelected();
    }
}
