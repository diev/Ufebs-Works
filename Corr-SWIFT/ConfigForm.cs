#region License
/*
Copyright 2022 Dmitrii Evdokimov

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

namespace CorrSWIFT;

public partial class ConfigForm : Form
{
    private ConfigProperties _config;

    public ConfigForm()
    {
        InitializeComponent();

        _config = new ConfigProperties();

        OpenDirText.Text = _config.Open.Dir;
        OpenMaskText.Text = _config.Open.Mask;

        SaveDirText.Text = _config.Save.Dir;
        SaveMaskText.Text = _config.Save.Mask;

        BankAccountText.Text = _config.Bank.Account;
        BankINNText.Text = _config.Bank.INN;
        BankKPPText.Text = _config.Bank.KPP;
        BankPayerText.Text = _config.Bank.PayerTemplate;
        BankPurposeText.Text = _config.Bank.PurposeTemplate;
    }

    private void OpenDirButton_Click(object sender, EventArgs e)
    {
        var result = OpenFolderDialog.ShowDialog();

        if (result == DialogResult.OK)
        {
            OpenDirText.Text = OpenFolderDialog.SelectedPath;
        }
    }

    private void SaveDirButton_Click(object sender, EventArgs e)
    {
        var result = SaveFolderDialog.ShowDialog();

        if (result == DialogResult.OK)
        {
            SaveDirText.Text = SaveFolderDialog.SelectedPath;
        }
    }

    private void ResetButton_Click(object sender, EventArgs e)
    {
        OpenDirText.Text = @"C:\TEMP";
        OpenMaskText.Text = "r*.xml";

        SaveDirText.Text = @"C:\TEMP";
        SaveMaskText.Text = "*_.txt";

        BankAccountText.Text = "30101810600000000702";
        BankINNText.Text = "7831001422";
        BankKPPText.Text = "784101001";
        BankPayerText.Text = "АО \"Сити Инвест Банк\" ИНН 7831001422 ({name} р/с {acc})";
        BankPurposeText.Text = "//7831001422//784101001//{name}//{purpose}";
    }

    private void AcceptButton_Click(object sender, EventArgs e)
    {
        _config.Open.Dir = OpenDirText.Text;
        _config.Open.Mask = OpenMaskText.Text;

        _config.Save.Dir = SaveDirText.Text;
        _config.Save.Mask = SaveMaskText.Text;

        _config.Bank.Account = BankAccountText.Text;
        _config.Bank.INN = BankINNText.Text;
        _config.Bank.KPP = BankKPPText.Text;
        _config.Bank.PayerTemplate = BankPayerText.Text;
        _config.Bank.PurposeTemplate = BankPurposeText.Text;

        _config.Flush(); //TODO Убрать требование перезапуска после переделки чтения JSON
        MessageBox.Show("Параметры сохранены.\nПерезапустите программу.", Application.ProductName,
            MessageBoxButtons.OK, MessageBoxIcon.Information);

        Close();
    }

    private void CancelButton_Click(object sender, EventArgs e)
    {
        Close();
    }
}
