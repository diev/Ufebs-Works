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
    private readonly int _maxName = 105;
    private readonly int _maxPurpose = 210;
    private bool _payerValid, _payeeValid, _purposeValid;

    public EditForm()
    {
        InitializeComponent();
        int w = Screen.PrimaryScreen.WorkingArea.Width;
        //int h = Screen.PrimaryScreen.WorkingArea.Height;

        //SetBounds(
        //    (int)(w * 0.02), (int)(h * 0.5),
        //    (int)(w * 0.9), Height);
        Width = (int)(w * 0.9);
    }

    private void PayerEdit_TextChanged(object sender, EventArgs e)
    {
        int len = PayerEdit.Text.Lat()!.Length;
        _payerValid = len > 0 && len <= _maxName;

        if (_payerValid)
        {
            PayerEdit.BackColor = BackColor;

            PayerStatus.Text = "OK";
            PayerStatus.ForeColor = Color.DarkGreen;
        }
        else
        {
            PayerEdit.BackColor = Color.LightPink;

            PayerStatus.Text = $"{len - _maxName}";
            PayerStatus.ForeColor = Color.Red;
        }

        OKButton.Enabled = _payerValid && _payeeValid && _purposeValid;
    }

    private void PayeeEdit_TextChanged(object sender, EventArgs e)
    {
        int len = PayeeEdit.Text.Lat()!.Length;
        _payeeValid = len > 0 && len <= _maxName;

        if (_payeeValid)
        {
            PayeeEdit.BackColor = BackColor;

            PayeeStatus.Text = "OK";
            PayeeStatus.ForeColor = Color.DarkGreen;
        }
        else
        {
            PayeeEdit.BackColor = Color.LightPink;

            PayeeStatus.Text = $"{len - _maxName}";
            PayeeStatus.ForeColor = Color.Red;
        }

        OKButton.Enabled = _payerValid && _payeeValid && _purposeValid;
    }

    private void PurposeEdit_TextChanged(object sender, EventArgs e)
    {
        int len = PurposeEdit.Text.Lat()!.Length;
        _purposeValid = len > 0 && len <= _maxPurpose;

        if (_purposeValid)
        {
            PurposeEdit.BackColor = BackColor;

            PurposeStatus.Text = "OK";
            PurposeStatus.ForeColor = Color.DarkGreen;
        }
        else
        {
            PurposeEdit.BackColor = Color.LightPink;

            PurposeStatus.Text = $"{len - _maxName}";
            PurposeStatus.ForeColor = Color.Red;
        }

        OKButton.Enabled = _payerValid && _payeeValid && _purposeValid;
    }
}
