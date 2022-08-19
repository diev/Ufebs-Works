#region License
/*
Copyright 2022 Dmitrii Evdokimov
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

namespace CorrSWIFT;

public partial class EditForm : Form
{
    private static readonly int _maxSwiftName = 105;
    private static readonly int _maxUfebsName = 160;
    private static readonly int _maxPurpose = 210;

    private readonly bool _swiftMode;
    private readonly int _maxName = _maxUfebsName;

    private bool _payerValid, _payeeValid, _purposeValid;
    private bool _payerMinus, _payeeMinus, _purposeMinus;

    public EditForm(bool swiftMode)
    {
        InitializeComponent();
        int w = Screen.PrimaryScreen.WorkingArea.Width;
        //int h = Screen.PrimaryScreen.WorkingArea.Height;

        //SetBounds(
        //    (int)(w * 0.02), (int)(h * 0.5),
        //    (int)(w * 0.9), Height);
        Width = (int)(w * 0.9);

        _swiftMode = swiftMode;

        if (swiftMode)
        {
            PayeeEdit.Visible = true;
            _payeeValid = true; ;

            SamplePayer.Visible = true;
            SamplePayee.Visible = true;
            SamplePurpose.Visible = true;

            Height += PayeeEdit.Height + SamplePanel.Height +
                OKButton.Height + OKButton.Margin.Vertical;

            PayeeLabel.Visible = true;
            PayeeStatus.Visible = true;

            Status.Text = "Format: SWIFT";
            PayeeLen.Visible = true;

            _maxName = _maxSwiftName;
        }
    }

    private void PayerEdit_TextChanged(object sender, EventArgs e)
    {
        string text = _swiftMode ? PayerEdit.Text.Lat()! : PayerEdit.Text;
        int len = text.Length;

        PayerLen.Text = len.ToString();

        _payerValid = len > 0 && len <= _maxName;

        if (_swiftMode)
        {
            SamplePayer.Text = len > _maxSwiftName
                ? text[.._maxSwiftName].Div35(_maxSwiftName)
                : text.Div35(_maxSwiftName);


            if (_payerMinus)
            {
                _payerMinus = false;
                PayerLabel.Text = "Плательщик:";
                PayerLabel.ForeColor = ForeColor;
            }

            for (int i = 0; i < len; i += 35)
            {
                if (text[i] == '-')
                {
                    _payerValid = false;
                    _payerMinus = true;
                    PayerLabel.Text += $" '-' в позиции {i + 1}!";
                    PayerLabel.ForeColor = Color.Red;
                    break;
                }
            }
        }

        if (_payerValid)
        {
            PayerEdit.BackColor = BackColor;

            PayerStatus.Text = "OK";
            PayerStatus.ForeColor = Color.DarkGreen;
        }
        else
        {
            PayerEdit.BackColor = Color.LightPink;

            PayerStatus.Text = $"{_maxName - len}";
            PayerStatus.ForeColor = Color.Red;
        }

        OKButton.Enabled = OKEnabled();
    }

    private void PayeeEdit_TextChanged(object sender, EventArgs e)
    {
        string text = PayeeEdit.Text.Lat()!;
        int len = text.Length;

        PayeeLen.Text = len.ToString();

        _payeeValid = len > 0 && len <= _maxName;

        SamplePayee.Text = len > _maxSwiftName
            ? text[.._maxSwiftName].Div35(_maxSwiftName)
            : text.Div35(_maxSwiftName);

        if (_payeeMinus)
        {
            _payeeMinus = false;
            PayeeLabel.Text = "Получатель:";
            PayeeLabel.ForeColor = ForeColor;
        }

        for (int i = 0; i < len; i += 35)
        {
            if (text[i] == '-')
            {
                _payeeValid = false;
                _payeeMinus = true;
                PayeeLabel.Text += $" '-' в позиции {i + 1}!";
                PayeeLabel.ForeColor = Color.Red;
                break;
            }
        }

        if (_payeeValid)
        {
            PayeeEdit.BackColor = BackColor;

            PayeeStatus.Text = "OK";
            PayeeStatus.ForeColor = Color.DarkGreen;
        }
        else
        {
            PayeeEdit.BackColor = Color.LightPink;

            PayeeStatus.Text = $"{_maxName - len}";
            PayeeStatus.ForeColor = Color.Red;
        }

        OKButton.Enabled = OKEnabled();
    }

    private void PurposeEdit_TextChanged(object sender, EventArgs e)
    {
        const int rows = 4 * 35; // SWIFT field :70:

        string text = _swiftMode ? PurposeEdit.Text.Lat()! : PurposeEdit.Text;
        int len = text.Length;

        PurposeLen.Text = len.ToString();

        _purposeValid = len > 0 && len <= _maxPurpose;

        if (_swiftMode)
        {
            SamplePurpose.Text = len > _maxPurpose
                ? text[.._maxPurpose].Div35()
                : text.Div35();

            if (_purposeMinus)
            {
                _purposeMinus = false;
                PurposeLabel.Text = "Назначение:";
                PurposeLabel.ForeColor = ForeColor;
            }

            for (int i = 35; i < Math.Min(len, rows); i += 35)
            {
                if (text[i] == '-')
                {
                    _purposeValid = false;
                    _purposeMinus = true;
                    PurposeLabel.Text += $" '-' в позиции {i + 1}!";
                    PurposeLabel.ForeColor = Color.Red;
                    break;
                }
            }
        }

        if (_purposeValid)
        {
            PurposeEdit.BackColor = BackColor;

            PurposeStatus.Text = "OK";
            PurposeStatus.ForeColor = Color.DarkGreen;
        }
        else
        {
            PurposeEdit.BackColor = Color.LightPink;

            PurposeStatus.Text = $"{_maxName - len}";
            PurposeStatus.ForeColor = Color.Red;
        }

        OKButton.Enabled = OKEnabled();
    }

    private bool OKEnabled()
        => (PayerEdit.Modified || PayerEdit.Modified || PurposeEdit.Modified) &&
            _payerValid && _payeeValid && _purposeValid;

}
