#region License
/*
Copyright 2022-2025 Dmitrii Evdokimov
Open source software

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

        string s = SwiftTranslit.Lat(RusSourceText.Text) ?? string.Empty;
        SwiftDestText35.Text = string.Empty;
        
        while (s.Length > 35)
        {
            SwiftDestText35.Text += s[..34] + "\n";
            s = s[34..];
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

    private void RusSrcText_TextChanged(object sender, EventArgs e)
    {
        SwiftDstText35.Text = SwiftHelpers.LatWrapText35(RusSrcText.Text);
    }
}
