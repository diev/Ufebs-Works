using Corr_SWIFT;

using System.Linq;
using System.Text;

namespace Corr_Lib;

public partial class Form1 : Form
{
    private const int MAX_UFEBS_NAME = 160;
    private const int MAX_UFEBS_PURPOSE = 210;

    private const int MAX_SWIFT_NAME = 3 * 35;
    private const int MAX_SWIFT_PURPOSE = 210;

    private string[] _files = Array.Empty<string>();
    private int _fileIndex = 0;

    private bool _isSwift50Valid = false;
    private bool _isSwift72Valid = false;
    private bool _isNameValid = false;
    private bool _isPurposeValid = false;

    private bool _saved = false;
    private string? _saveFileName;
    private string _saveMaskName = "*";

    private ConfigProperties _config;

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

            var x = Path.GetDirectoryName(arg);
            if (!string.IsNullOrEmpty(x))
                _config.Open.Dir = x;

            x = Path.GetFileName(arg);
            if (!string.IsNullOrEmpty(x))
                _config.Open.Mask = x;

            if (argc > 1) // 2:Output
            {
                arg = Path.GetFullPath(args[2]);

                x = Path.GetDirectoryName(arg);
                if (!string.IsNullOrEmpty(x))
                    _config.Save.Dir = x;

                x = Path.GetFileName(arg);
                if (!string.IsNullOrEmpty(x))
                    _config.Save.Mask = x;
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
        _files = Directory.GetFiles(_config.Open.Dir, _config.Open.Mask);

        FilesListBox.Items.Clear();
        FilesListBox.Items.AddRange(_files);

        if (_files.Length > 0)
        {
            LoadFile(_files[_fileIndex]);
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

        if (text == null)
        {
            SwiftTextBox.Text = "<SWIFTDocument> не содержит текста.";
        }

        tabControl1.SelectedIndex = tabControl1.TabCount - 1;

        var (acc, inn, kpp, name) = SwiftHelper.GetPayerSection(text);

        bool bank = inn == _config.Bank.INN; // "7831001422";
        string acc2 = _config.Bank.Account; // "30109810800010001378";

        // $"АО \"Сити Инвест Банк\" ИНН 7831001422 ({name} р/с {acc})";
        string name2 = bank
            ? name
            : _config.Bank.PayerTemplate
            .Replace("{name}", name)
            .Replace("{acc}", acc);

        _isNameValid = name2.Length <= MAX_UFEBS_NAME;

        text = SwiftHelper.SetPayerSection(text, acc2, inn, kpp, name2).Value;

        string purpose = SwiftHelper.GetPurpose(text).Value;

        if (!bank && SwiftHelper.HasTax(text))
        {
            // $"//7831001422//784101001//{name}//{purpose}";
            purpose = _config.Bank.PurposeTemplate
                .Replace("{name}", name)
                .Replace("{purpose}", purpose);

            text = SwiftHelper.SetPurpose(text, purpose).Text;
        }

        OutTextBox.Text = text;
        NameTextBox.Text = name2;
        PurposeTextBox.Text = purpose;

        TaxLabel.Text = SwiftHelper.HasTax(text)
            ? "Бюджет" : "Платеж";

        ProgressBar.Value = _fileIndex + 1;
        ProgressBar.Maximum = _files.Length;
        DoneLabel.Text = $"Сделано: {ProgressBar.Value}/{ProgressBar.Maximum}";

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
        string text = OutTextBox.Text;

        var (NameValue, NameLength) = SwiftHelper.GetPayerName(text);
        var (PurposeValue, PurposeLength) = SwiftHelper.GetPurpose(text);

        if (OutTextBox.Focused)
        {
            NameTextBox.Text = NameValue;
            PurposeTextBox.Text = PurposeValue;
        }

        _isSwift50Valid = NameLength <= MAX_SWIFT_NAME;
        _isSwift72Valid = PurposeLength <= MAX_SWIFT_PURPOSE;
        
        Swift50Label.Text = $"SWIFT 50K: {NameLength}/{MAX_SWIFT_NAME}";
        Swift50Label.ForeColor = _isSwift50Valid
            ? ForeColor : Color.Red;

        Swift72Label.Text = $"SWIFT 70,72: {PurposeLength}/{MAX_SWIFT_PURPOSE}";
        Swift72Label.ForeColor = _isSwift72Valid
            ? ForeColor : Color.Red;

        CheckNextEnabled();
    }

    private void NameTextBox_TextChanged(object sender, EventArgs e)
    {
        string text = NameTextBox.Text;
        int length = text.Length;
        
        _isNameValid = length <= MAX_UFEBS_NAME;

        NameEditLabel.Text = $"Плательщик {length}/{MAX_UFEBS_NAME}:";
        NameEditLabel.ForeColor = _isNameValid
            ? ForeColor : Color.Red;

        CheckNextEnabled();

        if (NameTextBox.Focused)
        {
            OutTextBox.Text = SwiftHelper.SetPayerName(OutTextBox.Text, text).Text;
        }
    }

    private void PurposeTextBox_TextChanged(object sender, EventArgs e)
    {
        string text = PurposeTextBox.Text;
        int length = text.Length;
        
        _isPurposeValid = length <= MAX_UFEBS_PURPOSE;

        PurposeEditLabel.Text = $"Назначение {length}/{MAX_SWIFT_PURPOSE}:";
        PurposeEditLabel.ForeColor = _isPurposeValid
            ? ForeColor : Color.Red;

        CheckNextEnabled();

        if (PurposeTextBox.Focused)
        {
            OutTextBox.Text = SwiftHelper.SetPurpose(OutTextBox.Text, text).Text;
        }
    }

    private void SaveMenuItem_Click(object sender, EventArgs e)
    {
        SaveFile();
    }

    private void OpenFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
    {
        _files = OpenFileDialog.FileNames;
        _fileIndex = 0;

        FilesListBox.Items.Clear();
        FilesListBox.Items.AddRange(_files);

        LoadFile(_files[_fileIndex]);
    }

    private bool CheckNextEnabled()
    {
        bool enabled = 
            _isSwift50Valid && 
            _isSwift72Valid &&
            _isNameValid &&
            _isPurposeValid &&
            _fileIndex < _files.Length;

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

        if (++_fileIndex < _files.Length)
        {
            LoadFile(_files[_fileIndex]);
        }
        else if (MessageBox.Show($"Сделано: {_files.Length}.\nЗакрыть программу?", Application.ProductName,
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
        {
            Close();
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
        while (
            _isSwift50Valid &&
            _isSwift72Valid &&
            _isNameValid &&
            _isPurposeValid &&
            _fileIndex < _files.Length)
        {
            GoNext();
        }
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
            var reply = MessageBox.Show($"Сохранить файл \"{_saveFileName}\" перед выходом?", 
                Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

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

или укажите в командной строке:

Input\[*.xml] [Output\[*_.txt]]";

        MessageBox.Show(text, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void FilesListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        var list = (ListBox)sender;

        if (list.SelectedItem is string file && File.Exists(file))
        {
            _fileIndex = list.SelectedIndex;
            LoadFile(file);
        }
    }
}
