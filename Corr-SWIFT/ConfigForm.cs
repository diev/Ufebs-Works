﻿#region License
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
    public ConfigForm()
    {
        InitializeComponent();

        LoadConfig();
    }

    private void LoadConfig()
    {
        OpenDirText.Text = ConfigProperties.OpenDir;
        OpenMaskText.Text = ConfigProperties.OpenMask;

        SaveDirText.Text = ConfigProperties.SaveDir;
        SaveMaskText.Text = ConfigProperties.SaveMask;

        BankAccountText.Text = ConfigProperties.BankAccount;
        BankINNText.Text = ConfigProperties.BankINN;
        BankKPPText.Text = ConfigProperties.BankKPP;
        BankPayerText.Text = ConfigProperties.BankPayerTemplate;
        BankPurposeText.Text = ConfigProperties.BankPurposeTemplate;
    }

    private void ResetConfig()
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

    private void SaveConfig()
    {
        ConfigProperties.OpenDir = OpenDirText.Text;
        ConfigProperties.OpenMask = OpenMaskText.Text;

        ConfigProperties.SaveDir = SaveDirText.Text;
        ConfigProperties.SaveMask = SaveMaskText.Text;

        ConfigProperties.BankAccount = BankAccountText.Text;
        ConfigProperties.BankINN = BankINNText.Text;
        ConfigProperties.BankKPP = BankKPPText.Text;
        ConfigProperties.BankPayerTemplate = BankPayerText.Text;
        ConfigProperties.BankPurposeTemplate = BankPurposeText.Text;

        ConfigProperties.Save();

        MessageBox.Show("Параметры сохранены.", Application.ProductName,
            MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        ResetConfig();
    }

    private void AcceptButton_Click(object sender, EventArgs e)
    {
        SaveConfig();
        Close();
    }

    private void CancelButton_Click(object sender, EventArgs e)
    {
        Close();
    }
}
