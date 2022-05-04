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
    private bool _isValid = false;
    private bool _saved = false;
    private string? _saveFileName;
    private string _saveMaskName = "*";

    public Form1()
    {
        InitializeComponent();
        Size = new Size((int)(Screen.PrimaryScreen.WorkingArea.Width * 0.8), 
            (int)(Screen.PrimaryScreen.WorkingArea.Height * 0.8));

        // default

        string openDirectory = Directory.GetCurrentDirectory();
        string openMask = "r*.xml";
        string saveDirectory = openDirectory;
        string saveMask = "*_.txt";

        // runtimeconfig.template.json > App.runtime.json

        if (AppContext.GetData(nameof(openDirectory)) is string od)
            openDirectory = od.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

        if (AppContext.GetData(nameof(saveDirectory)) is string sd)
            saveDirectory = sd.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

        if (AppContext.GetData(nameof(openMask)) is string om)
            openMask = om;

        if (AppContext.GetData(nameof(saveMask)) is string sm)
            saveMask = sm;

        // exe G:\BANK\TEST\OUT\r*.xml G:\BANK\TEST\CLI\*_.txt

        string[] args = Environment.GetCommandLineArgs(); // 0:exe 1:[Input|*] 2:[Output_|\]
        int argc = args.Length - 1;

        if (argc > 0) // 1:Input
        {
            string arg = Path.GetFullPath(args[1]);

            var x = Path.GetDirectoryName(arg);
            if (!string.IsNullOrEmpty(x))
                openDirectory = x;

            x = Path.GetFileName(arg);
            if (!string.IsNullOrEmpty(x))
                openMask = x;

            if (argc > 1) // 2:Output
            {
                arg = Path.GetFullPath(args[2]);

                x = Path.GetDirectoryName(arg);
                if (!string.IsNullOrEmpty(x))
                    saveDirectory = x;

                x = Path.GetFileName(arg);
                if (!string.IsNullOrEmpty(x))
                    saveMask = x;
            }
        }

        OpenFileDialog.InitialDirectory = openDirectory;
        OpenFileDialog.Filter = $"�����|{openMask}|{OpenFileDialog.Filter}";

        SaveAsFileDialog.InitialDirectory = saveDirectory;
        SaveAsFileDialog.Filter = $"SWIFT|{saveMask}|{SaveAsFileDialog.Filter}";
        SaveAsFileDialog.DefaultExt = Path.GetExtension(saveMask);

        _saveMaskName = Path.GetFileName(saveMask);
        _files = Directory.GetFiles(openDirectory, openMask);

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
        _isValid = false;
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
            SwiftTextBox.Text = "<SWIFTDocument> �� �������� ������.";
            return _isValid;
        }

        tabControl1.SelectedIndex = tabControl1.TabCount - 1;
        _isValid = true;

        var (acc, inn, kpp, name) = SwiftHelper.GetPayerSection(text);

        bool bank = inn == "7831001422";
        string acc2 = "30109810800010001378";
        string name2 = bank
            ? name
            : $"�� \"���� ������ ����\" ��� 7831001422 ({name} �/� {acc})";

        _isValid = _isValid && name2.Length <= MAX_UFEBS_NAME;

        text = SwiftHelper.SetPayerSection(text, acc2, inn, kpp, name2).Value;

        string purpose = SwiftHelper.GetPurpose(text).Value;

        if (!bank && SwiftHelper.HasTax(text))
        {
            purpose = $"//7831001422//784101001//{name}//{purpose}";
            text = SwiftHelper.SetPurpose(text, purpose).Text;
        }

        OutTextBox.Text = text;
        NameTextBox.Text = name2;
        PurposeTextBox.Text = purpose;

        TaxLabel.Text = $"{_fileIndex + 1}/{_files.Length} " + (SwiftHelper.HasTax(text)
            ? "������" : "������");

        CheckNextEnabled();

        return _isValid;
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

        bool valid = NameLength <= MAX_SWIFT_NAME && PurposeLength <= MAX_SWIFT_PURPOSE;
        StatusLabel.Text = $"SWIFT: {NameLength}/{MAX_SWIFT_NAME}, {PurposeLength}/{MAX_SWIFT_PURPOSE}";
        StatusLabel.ForeColor = valid
            ? ForeColor : Color.Red;

        _isValid = _isValid && valid;
        CheckNextEnabled();
    }

    private void NameTextBox_TextChanged(object sender, EventArgs e)
    {
        string text = NameTextBox.Text;
        int length = text.Length;
        bool valid = length <= MAX_UFEBS_NAME;

        NameLabel.Text = $"����������: {length}/{MAX_UFEBS_NAME}";
        NameLabel.ForeColor = valid
            ? ForeColor : Color.Red;

        _isValid = _isValid && valid;
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
        bool valid = length <= MAX_UFEBS_PURPOSE;

        PurposeLabel.Text = $"����������: {length}/{MAX_SWIFT_PURPOSE}";
        PurposeLabel.ForeColor = valid
            ? ForeColor : Color.Red;

        _isValid = _isValid && valid;
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

        LoadFile(_files[_fileIndex]);
    }

    private void CheckNextEnabled()
    {
        bool enabled = _isValid && _fileIndex + 1 < _files.Length;

        NextButton.Enabled = enabled;
        ForwardButton.Enabled = enabled;
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
        else if (MessageBox.Show($"�������: {_files.Length}.\n������� ���������?", Application.ProductName,
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
        {
            Close();
        }
    }

    private void SaveFile(string? text = null)
    {
        File.WriteAllText(_saveFileName, text ?? OutTextBox.Text, Encoding.ASCII);
        _saved = true;
    }

    private void ForwardButton_Click(object sender, EventArgs e)
    {
        while (_isValid && _fileIndex < _files.Length)
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
                SaveFile(XmlTextBox.Text);
                break;

            case 1:
                SaveFile(SwiftTextBox.Text);
                break;

            case 2:
                SaveFile();
                break;
        }

        _saved = true;
    }

    private void FontDialog_Apply(object sender, EventArgs e)
    {
        var font = ((FontDialog)sender).Font;

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
            var reply = MessageBox.Show($"��������� ���� \"{_saveFileName}\" ����� �������?", 
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
            $@"��������� ������������ ���������� �� ����� � SWIFT.

������ {Application.ProductVersion}

������� ��������� � �����:
{config}

��� ������� � ��������� ������:

Input\[*.xml] [Output\[*_.txt]]";

        MessageBox.Show(text, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
