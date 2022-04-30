using Corr_Lib;

using System.Text;
using System.Xml.Linq;

namespace Corr_Lib;

public partial class Form1 : Form
{
    //public SwiftText SwText = new();

    public Form1()
    {
        InitializeComponent();

        string[] args = Environment.GetCommandLineArgs();
        if (args.Length > 1)
        {
            string path = args[1];
            if (File.Exists(path))
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
        //string[] lines = File.ReadAllLines(OpenFileDialog.FileName);
        //XmlTextBox.Lines = lines;

        string text = File.ReadAllText(path, Encoding.ASCII);
        string filename = Path.GetFileNameWithoutExtension(path);
        string ext = Path.GetExtension(path);

        SaveAsFileDialog.FileName = filename + "_";

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
        //SwText.Parse(SwiftTextBox.Lines);
        //OutTextBox.Lines = SwText.GetLines();
        OutTextBox.Text = text;

        //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); //enable Windows-1251
        //var xdoc = XDocument.Load(OpenFileDialog.FileName);
        //var xdoc = XDocument.Parse(xml);
        //var ed = xdoc.Root;

        //if (ed == null)
        //{
        //    return;
        //}

        //var ns = ed.GetDefaultNamespace();

        //if (ed.Name.LocalName != "ED503")
        //{
        //    return;
        //}

        ////try
        ////{
        //    var container = ed.Element(ns + "SWIFTContainer");
        //    var document = container?.Element(ns + "SWIFTDocument");
        //    string? value = document?.Value;

        //    if (value == null)
        //    {
        //        SwiftTextBox.Text = "<SWIFTDocument> не содержит текста.";
        //        return;
        //    }

        //    byte[] bytes = Convert.FromBase64String(value);
        //    string text = Encoding.ASCII.GetString(bytes);

        //string? text = SwiftHelper.GetSwiftDocument(xml);

        if (text == null)
        {
            SwiftTextBox.Text = "<SWIFTDocument> не содержит текста.";
            return;
        }

        tabControl1.SelectedIndex = tabControl1.TabCount - 1;



        //text = OutTextBox.Text;
        //NameTextBox.Text = SwiftHelper.GetPayerName(text); //Swift.Cyr(SwText.PayerName);
        //PurposeTextBox.Text = SwiftHelper.GetPurpose(text); //Swift.Cyr(SwText.Purpose);

        var (OuterText, InnerText) = SwiftHelper.GetSection(SwiftTextBox.Text, "70");

        NameTextBox.Text = InnerText; //Swift.Cyr(SwText.PayerName);
        PurposeTextBox.Text = OuterText; //Swift.Cyr(SwText.Purpose);

        //StatusLabel.Text = $"Плательщик: {SwText?.PayerName.Length}/{SwiftText.NameMaxLength}, " +
        //    $"назначение: {SwText?.Purpose.Length}/{SwiftText.PurposeMaxLength} символов";
        //}
        //catch (Exception ex)
        //{
        //    SwiftTextBox.Text = ex.Message;
        //}
    }

    private void ReloadNameMenuItem_Click(object sender, EventArgs e)
    {
        //SwText.Parse(OutTextBox.Lines);
        //NameTextBox.Text = Swift.Cyr(SwText.PayerName);
    }

    private void ReloadPurposeMenuItem_Click(object sender, EventArgs e)
    {
        //SwText.Parse(OutTextBox.Lines);
        //PurposeTextBox.Text = Swift.Cyr(SwText.Purpose);
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
        //SwText.Parse(OutTextBox.Lines);
        //StatusLabel.Text = $"Плательщик: {SwText?.PayerName.Length}/{SwiftText.NameMaxLength}, " +
        //    $"назначение: {SwText?.Purpose.Length}/{SwiftText.PurposeMaxLength} символов";
        //StatusLabel.Text = $"Плательщик: {SwText.PayerName.Length}/{SwiftText.NameMaxLength}, " +
        //        $"назначение: {SwText.Purpose.Length}/{SwiftText.PurposeMaxLength} символов";
        //StatusLabel.Text = $"Плательщик: {SwiftHelper.GetPayerName(OutTextBox.Text).Length}/{SwiftText.NameMaxLength}, " +
        //        $"назначение: {SwiftHelper.GetPurpose(OutTextBox.Text).Length}/{SwiftText.PurposeMaxLength} символов";
    }

    private void NameTextBox_TextChanged(object sender, EventArgs e)
    {
        //NameLabel.Text = $"Плательщик: {NameTextBox.TextLength}/{SwiftText.NameMaxLength}";

        //SwText.PayerName = Swift.Lat(NameTextBox.Text);
        //OutTextBox.Lines = SwText.GetLines();
    }

    private void PurposeTextBox_TextChanged(object sender, EventArgs e)
    {
        //PurposeLabel.Text = $"Назначение: {PurposeTextBox.TextLength}/{SwiftText.PurposeMaxLength}";

        //SwText.Purpose = Swift.Lat(PurposeTextBox.Text);
        //OutTextBox.Lines = SwText.GetLines();
        OutTextBox.Text = SwiftHelper.ReplacePurpose(OutTextBox.Text, PurposeTextBox.Text);
    }
}
