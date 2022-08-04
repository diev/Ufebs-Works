using CorrLib.SWIFT;

namespace SwiftTranslator;

public partial class Form1 : Form
{
    int _maxLength = 0;

    public Form1()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void RusSourceText_TextChanged(object sender, EventArgs e)
    {
        ColorizeLength();

        string s = SwiftTranslit.Lat(RusSourceText.Text);
        SwiftDestText35.Text = string.Empty;
        
        while (s.Length > 35)
        {
            SwiftDestText35.Text += s[..34] + "\n";
            s = s.Remove(0, 34);
        }

        SwiftDestText35.Text += s + "\n";
    }

    private void SwiftSourceText35_TextChanged(object sender, EventArgs e)
    {
        RusDestText.Text = SwiftTranslit.Cyr(SwiftSourceText35.Text.ReplaceLineEndings(string.Empty));
    }

    private void ColorizeLength()
    {
        int length = RusSourceText.TextLength;

        if (_maxLength == 0 || length <= _maxLength)
        {
            RusSourceTextLength.ForeColor = Control.DefaultForeColor;
            RusSourceTextLength.Text = $"Фактическая длина: {length}";
        }
        else
        {
            RusSourceTextLength.ForeColor = Color.Red;
            RusSourceTextLength.Text = $"Фактическая длина: {length} (превышение на {length - _maxLength}!)";
        }
    }

    private void LimitNone_CheckedChanged(object sender, EventArgs e)
    {
        _maxLength = 0;
        ColorizeLength();
    }

    private void Limit160_CheckedChanged(object sender, EventArgs e)
    {
        _maxLength = 160;
        ColorizeLength();
    }

    private void Limit210_CheckedChanged(object sender, EventArgs e)
    {
        _maxLength = 210;
        ColorizeLength();
    }

    private void SwiftSrcText_TextChanged(object sender, EventArgs e)
    {
        RusDstText.Text = SwiftTranslit.Cyr(SwiftSrcText.Text);
    }
}
