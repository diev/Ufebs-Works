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

using CorrLib;

namespace CorrSWIFT
{
    public partial class CorrForm : Form
    {
        private bool _nameOk;
        private bool _purposeOk;

        public CorrForm(string name, string purpose)
        {
            InitializeComponent();
            NameResult.Text = name;
            PurposeResult.Text = purpose;
            SetButtons();
        }

        private void NameResult_TextChanged(object sender, EventArgs e)
        {
            NameSwift.Text = NameResult.Text.LatWrap35();
            int n = NameResult.TextLength - 160; //TODO
            _nameOk = n <= 0;

            if (_nameOk)
            {
                NameLabel.Text = "Наименование плательщика:";
                NameResult.BackColor = BackColor;
            }
            else
            {
                NameLabel.Text = $"Наименование плательщика - надо удалить {n} символов:";
                NameResult.BackColor = Color.LightPink;
            }

            SetButtons();
        }

        private void PurposeResult_TextChanged(object sender, EventArgs e)
        {
            PurposeSwift.Text = PurposeResult.Text.LatWrap35();
            int n = PurposeResult.TextLength - 210; //TODO
            _purposeOk = n <= 0;

            if (_purposeOk)
            {
                PurposeLabel.Text = "Назначение платежа:";
                PurposeResult.BackColor = BackColor;
            }
            else
            {
                PurposeLabel.Text = $"Назначение платежа - надо удалить {n} символов:";
                PurposeResult.BackColor = Color.LightPink;
            }

            SetButtons();
        }

        private void SetButtons()
        {
            OKButton.Enabled = _nameOk && _purposeOk;
        }
    }
}
