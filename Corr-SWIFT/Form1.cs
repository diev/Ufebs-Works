using Corr_SWIFT;

using System.Text;

namespace Corr_Lib;

public partial class Form1 : Form
{
    private const int MAX_NAME = 3 * 35;
    private const int MAX_PURPOSE = 210;

    private bool _isSwift50Valid = false;
    private bool _isSwift72Valid = false;
    private bool _isNameValid = false;
    private bool _isPurposeValid = false;

    private bool _saved = false;
    private string? _saveFileName;
    private string _saveMaskName = "*";

    private readonly ConfigProperties _config;
    private SwiftLines? _swift;

    public Form1()
    {
        InitializeComponent();

        int w = Screen.PrimaryScreen.WorkingArea.Width;
        int h = Screen.PrimaryScreen.WorkingArea.Height;

        SetBounds(
            (int)(w * 0.1), (int)(h * 0.15),
            (int)(w * 0.8), (int)(h * 0.75));

        // runtimeconfig.template.json > App.runtime.json

        _config = new ConfigProperties();

        // exe G:\BANK\TEST\OUT\r*.xml G:\BANK\TEST\CLI\*_.txt

        string[] args = Environment.GetCommandLineArgs(); // 0:exe 1:[Input|*] 2:[Output_|\]
        int argc = args.Length - 1;

        if (argc > 0) // 1:Input
        {
            string arg = Path.GetFullPath(args[1]);
            _config.Open.Dir = Path.GetDirectoryName(arg);
            _config.Open.Mask = Path.GetFileName(arg);

            if (argc > 1) // 2:Output
            {
                arg = Path.GetFullPath(args[2]);
                _config.Save.Dir = Path.GetDirectoryName(arg);
                _config.Save.Mask = Path.GetFileName(arg);
            }
        }

        StringBuilder err = new();
        
        if (string.IsNullOrEmpty(_config.Open.Dir) || !Directory.Exists(_config.Open.Dir))
        {
            err.AppendLine($"Папка Open.Dir не существует!");
            _config.Open.Dir = Directory.GetCurrentDirectory();
        }

        if (string.IsNullOrEmpty(_config.Open.Mask))
        {
            err.AppendLine($"Маска Open.Mask не указана!");
            _config.Open.Mask = "r*.xml";
        }

        if (string.IsNullOrEmpty(_config.Save.Dir) || !Directory.Exists(_config.Save.Dir))
        {
            err.AppendLine($"Папка Save.Dir не существует!");
            _config.Save.Dir = _config.Open.Dir;
        }

        if (string.IsNullOrEmpty(_config.Save.Mask))
        {
            err.AppendLine($"Маска Save.Mask не указана!");
            _config.Save.Mask = "*_.txt";
        }

        if (string.IsNullOrEmpty(_config.Bank.Account))
        {
            err.AppendLine($"Счет Банка не указан!");
            _config.Bank.Account = "12345678901234567890";
        }

        if (string.IsNullOrEmpty(_config.Bank.INN))
        {
            err.AppendLine($"ИНН Банка не указан!");
            _config.Bank.INN = "7831001422";
        }

        if (string.IsNullOrEmpty(_config.Bank.KPP))
        {
            err.AppendLine($"КПП Банка не указан!");
            _config.Bank.KPP = "783101001";
        }

        if (string.IsNullOrEmpty(_config.Bank.PayerTemplate))
        {
            err.AppendLine($"Шаблон за клиента Банка не указан!");
            _config.Bank.PayerTemplate = "АО \"Сити Инвест Банк\" ИНН 7831001422 ({name} р/с {acc})";
        }

        if (string.IsNullOrEmpty(_config.Bank.PurposeTemplate))
        {
            err.AppendLine($"Шаблон назначения за третье лицо не указан!");
            _config.Bank.PurposeTemplate = "//7831001422//783101001//{name}//{purpose}";
        }

        if (err.Length > 0)
        {
            MessageBox.Show($"Проверьте настройки:\n\n{err}\nПереход в демо-режим.", Application.ProductName,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        OpenFileDialog.InitialDirectory = _config.Open.Dir;
        OpenFileDialog.Filter = $"УФЭБС|{_config.Open.Mask}|{OpenFileDialog.Filter}";

        SaveAsFileDialog.InitialDirectory = _config.Save.Dir;
        SaveAsFileDialog.Filter = $"SWIFT|{_config.Save.Mask}|{SaveAsFileDialog.Filter}";
        SaveAsFileDialog.DefaultExt = Path.GetExtension(_config.Save.Mask);

        _saveMaskName = Path.GetFileName(_config.Save.Mask);

        FilesListBox.Items.Clear();
        FilesListBox.Items.AddRange(Directory.GetFiles(_config.Open.Dir, _config.Open.Mask));

        if (FilesListBox.Items.Count > 0)
        {
            FilesListBox.SelectedIndex = 0;
        }
    }

    private void OpenMenuItem_Click(object sender, EventArgs e)
    {
        OpenFileDialog.ShowDialog();
    }

    private bool LoadFile(string path)
    {
        _saved = false;

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

        if (text == null)
        {
            SwiftTextBox.Text = "<SWIFTDocument> не содержит текста.";
        }

        tabControl1.SelectedIndex = tabControl1.TabCount - 1;

        var acc = _swift.Acc;
        var inn = _swift.INN;
        var kpp = _swift.KPP;
        var name = _swift.Name;

        bool bank = inn == _config.Bank.INN; // "7831001422";
        string acc2 = _config.Bank.Account; // "30109810800010001378";

        // $"АО \"Сити Инвест Банк\" ИНН 7831001422 ({name} р/с {acc})";
        string name2 = bank
            ? name
            : _config.Bank.PayerTemplate
            .Replace("{name}", name)
            .Replace("{acc}", acc);

        _isNameValid = name2.Length <= MAX_NAME;

        _swift.Acc = acc2;
        _swift.Name = name2;

        string purpose = _swift.Purpose;

        if (!bank && _swift.Tax)
        {
            // $"//7831001422//784101001//{name}//{purpose}";
            purpose = _config.Bank.PurposeTemplate
                .Replace("{name}", name)
                .Replace("{purpose}", purpose);

            _swift.Purpose = purpose;
        }

        OutTextBox.Lines = _swift.Lines;
        NameTextBox.Text = name2;
        PurposeTextBox.Text = purpose;

        TaxLabel.Text = _swift.Tax ? "Бюджет" : "Платеж";

        return CheckNextEnabled();
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

        _isSwift50Valid = _swift.NameLength <= MAX_NAME;
        _isSwift72Valid = _swift.PurposeLength <= MAX_PURPOSE;
        
        Swift50Label.Text = $"SWIFT 50K: {_swift.NameLength}/{MAX_NAME}";
        Swift50Label.ForeColor = _isSwift50Valid ? ForeColor : Color.Red;

        Swift72Label.Text = $"SWIFT 70,72: {_swift.PurposeLength}/{MAX_PURPOSE}";
        Swift72Label.ForeColor = _isSwift72Valid ? ForeColor : Color.Red;

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
            _isSwift50Valid && 
            _isSwift72Valid &&
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
            _isSwift50Valid &&
            _isSwift72Valid &&
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
            var reply = MessageBox.Show($"Сохранить файл \"{_saveFileName}\"\nперед шагом назад?",
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
            FilesListBox.SelectedIndex--;
        }
    }

    private void SaveFile(string? text = null)
    {
        if (_saveFileName != null)
        {
            File.WriteAllText(_saveFileName, text ?? OutTextBox.Text, Encoding.ASCII);
            _saved = true;
        }
    }

    private void ForwardButton_Click(object sender, EventArgs e)
    {
        GoForward();
    }

    private void SaveAsFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
    {
        _saveFileName = ((SaveFileDialog)sender).FileName;

        switch (tabControl1.SelectedIndex)
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

        _saved = true;
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
            var reply = MessageBox.Show($"Сохранить файл \"{_saveFileName}\"\nперед выходом?", 
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
        string config = Path.ChangeExtension(Application.ExecutablePath, "runtime.json");
        string text =
            $@"Программа дооформления документов из УФЭБС в SWIFT.

Версия {Application.ProductVersion}

Задайте параметры в файле:
{config}

или укажите пути в командной строке:

Input\[*.xml] [Output\[*_.txt]]";

        MessageBox.Show(text, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            DoneLabel.Text = $"Сделано: {index}/{total}";

            FilesPage.Text = $"Файлы {index}/{total}";
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

    }

    private void PrintPreviewMenuItem_Click(object sender, EventArgs e)
    {

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
        var item = (ToolStripMenuItem)sender;
        item.Checked = !item.Checked;
        
        OutEditCheck.Checked = item.Checked;
    }

    private void SettingsMenuItem_Click(object sender, EventArgs e)
    {
        string config = Path.ChangeExtension(Application.ExecutablePath, "runtimeconfig.json");

        if (File.Exists(config))
        {
            System.Diagnostics.Process.Start("notepad.exe", config);
        }
    }
}
