using System.Text;

namespace Corr_Lib;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        string[] args = Environment.GetCommandLineArgs();
        
        if (args.Length > 1)
        {
            string path = args[1];

            if (path.Contains('*') || path.Contains('?'))
            {
                foreach (var file in Directory.GetFiles(path))
                {
                    LoadFile(file);
                }
            }
            else if (File.Exists(path))
            {
                LoadFile(path);
            }
            else
            {
                MessageBox.Show($"File \"{path}\" not found!");
            }
        }
    }

    private void OpenMenuItem_Click(object sender, EventArgs e)
    {
        if (OpenFileDialog.ShowDialog() == DialogResult.OK)
        {
            LoadFile(OpenFileDialog.FileName);
        }
    }

    private void LoadFile(string path)
    {
        string text = File.ReadAllText(path, Encoding.ASCII);
        string filename = Path.GetFileNameWithoutExtension(path);
        string ext = Path.GetExtension(path);

        SaveAsFileDialog.FileName = $"{filename}_{SaveAsFileDialog.DefaultExt}";

        Text = $"Corr-SWIFT {path} - {SaveAsFileDialog.FileName}";

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
            return;
        }

        tabControl1.SelectedIndex = tabControl1.TabCount - 1;

        var (acc, inn, kpp, name) = SwiftHelper.GetPayerSection(text);

        bool bank = inn == "7831001422";
        string acc2 = "30109810800010001378";
        string name2 = bank
            ? name
            : $"АО \"Сити Инвест Банк\" ИНН 7831001422 ({name} р/с {acc})";

        text = SwiftHelper.SetPayerSection(text, acc2, inn, kpp, name2);

        string purpose = SwiftHelper.GetPurpose(text).Value;

        if (!bank && SwiftHelper.HasTax(text))
        {
            purpose = $"//7831001422//784101001//{name}//{purpose}";
            text = SwiftHelper.SetPurpose(text, purpose);
        }

        OutTextBox.Text = text;
        NameTextBox.Text = name2;
        PurposeTextBox.Text = purpose;

        TaxLabel.Text = SwiftHelper.HasTax(text)
            ? "Бюджет" : "Платеж";
    }

    private void ExitMenuItem_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void SaveAsMenuItem_Click(object sender, EventArgs e)
    {
        if (SaveAsFileDialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        switch (tabControl1.SelectedIndex)
        {
            case 0:
                File.WriteAllLines(SaveAsFileDialog.FileName, XmlTextBox.Lines, Encoding.ASCII);
                break;

            case 1:
                File.WriteAllLines(SaveAsFileDialog.FileName, SwiftTextBox.Lines, Encoding.ASCII);
                break;

            case 2:
                File.WriteAllLines(SaveAsFileDialog.FileName, OutTextBox.Lines, Encoding.ASCII);
                break;
        }

    }

    private void FontMenuItem_Click(object sender, EventArgs e)
    {
        FontDialog.Font = OutTextBox.Font;

        if (FontDialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        XmlTextBox.Font = FontDialog.Font;
        SwiftTextBox.Font = FontDialog.Font;
        OutTextBox.Font = FontDialog.Font;
        NameTextBox.Font = FontDialog.Font;
        PurposeTextBox.Font = FontDialog.Font;
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

        StatusLabel.Text = $"SWIFT: {NameLength}/160, {PurposeLength}/210";
        StatusLabel.ForeColor = NameLength > 160 || PurposeLength > 210
            ? Color.Red : ForeColor;
    }

    private void NameTextBox_TextChanged(object sender, EventArgs e)
    {
        string text = NameTextBox.Text;
        int length = text.Length;

        NameLabel.Text = $"Плательщик: {length}/160";
        NameLabel.ForeColor = length > 160
            ? Color.Red : ForeColor;

        if (NameTextBox.Focused)
        {
            OutTextBox.Text = SwiftHelper.SetPayerName(OutTextBox.Text, text);
        }
    }

    private void PurposeTextBox_TextChanged(object sender, EventArgs e)
    {
        string text = PurposeTextBox.Text;
        int length = text.Length;

        PurposeLabel.Text = $"Назначение: {length}/210";
        PurposeLabel.ForeColor = length > 210
            ? Color.Red : ForeColor;

        if (PurposeTextBox.Focused)
        {
            OutTextBox.Text = SwiftHelper.SetPurpose(OutTextBox.Text, text);
        }
    }

    private void SaveMenuItem_Click(object sender, EventArgs e)
    {
        File.WriteAllLines(SaveAsFileDialog.FileName, OutTextBox.Lines, Encoding.ASCII);
    }
}
