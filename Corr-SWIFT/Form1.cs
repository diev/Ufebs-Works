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

namespace CorrSWIFT;

public partial class Form1 : Form
{
    private const int MAX_NAME = 3 * 35;
    private const int MAX_PURPOSE = 210;

    private bool _isNameValid = false;
    private bool _isPurposeValid = false;

    private bool _saved = false;
    private string? _saveFileName;
    private string _saveMaskName = "*";

    private SwiftLines? _swift;

    public Form1()
    {
        InitializeComponent();
        InitForm();
    }

    private static void About()
    {
        string config = Path.ChangeExtension(Application.ExecutablePath, "runtimeconfig.json");
        string text =
            $@"Программа дооформления документов из УФЭБС в SWIFT.

Версия {Application.ProductVersion}

Задайте параметры в меню Файл\Параметры...
Сохраняются они в файле
{config}

Также пути можно переопределить в командной строке:
    Input\[*.xml] [Output\[*_.txt]]";

        MessageBox.Show(text, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        FilesListBox.Items.AddRange(Directory.GetFiles(ConfigProperties.OpenDir, ConfigProperties.OpenMask));

        if (FilesListBox.Items.Count > 0)
        {
            FilesListBox.SelectedIndex = 0;
        }
    }

    private bool LoadFile(string path)
    {
        _saved = false;
        SavedLabel.Text = _saved ? "Сохранен" : "Не сохранен";

        string text = File.ReadAllText(path, Encoding.ASCII);
        string filename = Path.GetFileNameWithoutExtension(path);
        string ext = Path.GetExtension(path);

        SaveAsFileDialog.FileName = _saveMaskName.Replace("*", filename);
        _saveFileName = Path.Combine(SaveAsFileDialog.InitialDirectory, SaveAsFileDialog.FileName);

        Text = $"{Application.ProductName} | {path} > {_saveFileName}";

        if (ext.Equals(".xml", StringComparison.OrdinalIgnoreCase))
        {
            XmlTextBox.Text = text;
            text = SwiftHelper.GetSwiftDocument(text) ?? "No SwiftDocument";
        }
        else
        {
            XmlTextBox.Text = "No XML file";
        }

        SwiftTextBox.Text = text;
        _swift = new SwiftLines(SwiftTextBox.Lines);

        if (text is null)
        {
            SwiftTextBox.Text = "<SWIFTDocument> не содержит текста.";
        }

        Tabs.SelectedIndex = Tabs.TabCount - 1;

        var acc = _swift.Account;
        var inn = _swift.INN;
        //var kpp = _swift.KPP;
        var name = _swift.Name;

        bool bank = inn == ConfigProperties.BankINN; // "7831001422";
        string acc2 = ConfigProperties.BankAccount; // "30109810800010001378";

        // $"АО \"Сити Инвест Банк\" ИНН 7831001422 ({name} р/с {acc})";
        string name2 = bank
            ? name
            : ConfigProperties.BankPayerTemplate
            .Replace("{name}", name)
            .Replace("{acc}", acc);

        _isNameValid = name2.Length <= MAX_NAME;

        _swift.Account = acc2;
        _swift.Name = name2;

        string purpose = _swift.Purpose;

        if (!bank && _swift.Tax)
        {
            // $"//7831001422//784101001//{name}//{purpose}";
            purpose = ConfigProperties.BankPurposeTemplate
                .Replace("{name}", name)
                .Replace("{purpose}", purpose);

            _swift.Purpose = purpose;
        }

        OutTextBox.Lines = _swift.Lines;
        NameTextBox.Text = name2;
        PurposeTextBox.Text = purpose;

        TaxValue.Text = _swift.Tax ? "Бюджет" : "Платеж";

        return CheckNextEnabled();
    }

    private void GoNext()
    {
        SaveFile();

        if (FilesListBox.SelectedIndex + 1 < FilesListBox.Items.Count)
        {
            FilesListBox.SelectedIndex++;
        }
        else if (MessageBox.Show($"Сделано: {FilesListBox.Items.Count}.\nЗакрыть программу?", Application.ProductName,
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
            FilesListBox.SelectedIndex < FilesListBox.Items.Count)
        {
            GoNext();
        }
    }

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

        if (FilesListBox.SelectedIndex > 0)
        {
            FilesListBox.SelectedIndex--; //TODO почему не срабатывает событие смены?
        }
    }

    private void SaveFile(string? text = null)
    {
        if (_saveFileName != null)
        {
            File.WriteAllText(_saveFileName, text ?? OutTextBox.Text, Encoding.ASCII);
            _saved = File.Exists(_saveFileName);
            SavedLabel.Text = _saved ? "Сохранен" : "Не сохранен";

            var item = FilesListBox.SelectedItem.ToString();

            if (!item.Contains(" > "))
            {
                item += " > " + _saveFileName;
                //FilesListBox.SelectedItem = item; // not work
                FilesListBox.Items[FilesListBox.SelectedIndex] = item;
            }
        }
    }
    private void PrintPage(PrintPageEventArgs e)
    {
        string documentContents = OutTextBox.Text;
        string stringToPrint = documentContents;

        // Sets the value of charactersOnPage to the number of characters
        // of stringToPrint that will fit within the bounds of the page.
        e.Graphics.MeasureString(stringToPrint, this.Font,
            e.MarginBounds.Size, StringFormat.GenericTypographic,
            out int charactersOnPage, out int linesPerPage);

        // Draws the string within the bounds of the page.
        e.Graphics.DrawString(stringToPrint, this.Font, Brushes.Black,
        e.MarginBounds, StringFormat.GenericTypographic);

        // Remove the portion of the string that has been printed.
        stringToPrint = stringToPrint.Substring(charactersOnPage);

        // Check to see if more pages are to be printed.
        e.HasMorePages = (stringToPrint.Length > 0);

        // If there are no more pages, reset the string to be printed.
        if (!e.HasMorePages)
        {
            stringToPrint = documentContents;
        }
    }

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
        if (OutTextBox.Focused && OutEditCheck.Checked)
        {
            _swift.Lines = OutTextBox.Lines;
            //OutTextBox.Lines = _swift.Lines;

            NameTextBox.Text = _swift.Name;
            PurposeTextBox.Text = _swift.Purpose;
        }

        CheckNextEnabled();
    }

    private void NameTextBox_TextChanged(object sender, EventArgs e)
    {
        if (NameTextBox.Focused && !OutEditCheck.Checked)
        {
            _swift.Name = NameTextBox.Text;
            OutTextBox.Lines = _swift.Lines;
        }

        int length = NameTextBox.TextLength;
        _isNameValid = length <= MAX_NAME;

        NameEditLabel.Text = $"Плательщик {length}/{MAX_NAME}:";
        NameEditLabel.ForeColor = _isNameValid ? ForeColor : Color.Red;

        CheckNextEnabled();
    }

    private void PurposeTextBox_TextChanged(object sender, EventArgs e)
    {
        if (PurposeTextBox.Focused && !OutEditCheck.Checked)
        {
            _swift.Purpose = PurposeTextBox.Text;
            OutTextBox.Lines = _swift.Lines;
        }

        int length = PurposeTextBox.TextLength;
        _isPurposeValid = length <= MAX_PURPOSE;

        PurposeEditLabel.Text = $"Назначение {length}/{MAX_PURPOSE}:";
        PurposeEditLabel.ForeColor = _isPurposeValid ? ForeColor : Color.Red;

        CheckNextEnabled();
    }

    private void SaveMenuItem_Click(object sender, EventArgs e)
    {
        SaveFile();
    }

    private void OpenFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
    {
        FilesListBox.Items.Clear();
        FilesListBox.Items.AddRange(OpenFileDialog.FileNames);
        FilesListBox.SelectedIndex = 0;
    }

    private bool CheckNextEnabled()
    {
        bool enabled = FilesListBox.SelectedIndex > 0;

        PrevMenuItem.Enabled = enabled;
        PrevButton.Enabled = enabled;

        enabled = 
            _isNameValid &&
            _isPurposeValid &&
            FilesListBox.SelectedIndex < FilesListBox.Items.Count;

        NextMenuItem.Enabled = enabled;
        ForwardMenuItem.Enabled = enabled;

        NextButton.Enabled = enabled;
        ForwardButton.Enabled = enabled;

        return enabled;
    }

    private void NextButton_Click(object sender, EventArgs e)
    {
        GoNext();
    }

    private void ForwardButton_Click(object sender, EventArgs e)
    {
        GoForward();
    }

    private void SaveAsFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
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

    private void FontDialog_Apply(object sender, EventArgs e)
    {
        var font = ((FontDialog)sender).Font;

        FilesListBox.Font = font;
        XmlTextBox.Font = font;
        SwiftTextBox.Font = font;
        OutTextBox.Font = font;
        NameTextBox.Font = font;
        PurposeTextBox.Font = font;
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
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

    private void AboutMenuItem_Click(object sender, EventArgs e)
    {
        About();
    }

    private void FilesListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        var list = (ListBox)sender;

        if (list.SelectedItem is string file && File.Exists(file))
        {
            LoadFile(file);

            int index = list.SelectedIndex + 1;
            int total = list.Items.Count;

            ProgressBar.Value = index;
            ProgressBar.Maximum = total;

            string s = $"{index}/{total}";
            DoneValue.Text = s;

            FilesPage.Text = $"Файлы {s}";
        }
    }

    private void PrevButton_Click(object sender, EventArgs e)
    {
        GoPrev();
    }

    private void OutEditCheck_CheckedChanged(object sender, EventArgs e)
    {
        bool check = ((CheckBox)sender).Checked;

        ChangeMenuItem.Checked = check;

        OutTextBox.ReadOnly = !check;

        NameTextBox.ReadOnly = check;
        PurposeTextBox.ReadOnly = check;
    }

    private void NewFileMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void PrintMenuItem_Click(object sender, EventArgs e)
    {
        PrintDocument.Print();
    }

    private void PrintPreviewMenuItem_Click(object sender, EventArgs e)
    {
        PrintDocument.DocumentName = _saveFileName;
        PrintPreviewDialog.ShowDialog();
    }

    private void UndoMenuItem_Click(object sender, EventArgs e)
    {
        OutTextBox.Undo();
    }

    private void RedoMenuItem_Click(object sender, EventArgs e)
    {
        //OutTextBox.
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
}
