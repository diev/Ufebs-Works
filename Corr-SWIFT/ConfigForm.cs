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
        OpenDirEdit.Text = Config.OpenDir;
        OpenMaskEdit.Text = Config.OpenMask;

        SaveDirEdit.Text = Config.SaveDir;
        SaveMaskEdit.Text = Config.SaveMask;
        SaveFormatChoice.Text = Config.SaveFormat;

        CorrAccountEdit.Text = Config.CorrAccount;
        BankInnEdit.Text = Config.BankINN;
        BankKppEdit.Text = Config.BankKPP;
        NameTemplateEdit.Text = Config.NameTemplate;
        PurposeTemplateEdit.Text = Config.PurposeTemplate;

        SwiftNameLimitChoice.Text = Config.SwiftNameLimit.ToString();

        BankSwiftEdit.Text = Config.BankSWIFT;
        CorrSwiftEdit.Text = Config.CorrSWIFT;
    }

    private void ResetConfig()
    {
        OpenDirEdit.Text = @"C:\TEMP";
        OpenMaskEdit.Text = "*.xml";

        SaveDirEdit.Text = @"C:\TEMP";
        SaveMaskEdit.Text = "*.mt103";
        SaveFormatChoice.Text = "SWIFT";

        CorrAccountEdit.Text = "30101810600000000702";
        BankInnEdit.Text = "7831001422";
        BankKppEdit.Text = "784101001";
        NameTemplateEdit.Text = "АО \"Сити Инвест Банк\" ИНН 7831001422 ({name} р/с {acc})";
        PurposeTemplateEdit.Text = "//7831001422//784101001//{name}//{purpose}";

        SwiftNameLimitChoice.Text = "160"; // 105 = три строки по стандарту SWIFT-RUR или 160 (= 4.5 строки) по стандару УФЭБС

        BankSwiftEdit.Text = "CITVRU2P";
        CorrSwiftEdit.Text = "CITVRU2P";
    }

    private void SaveConfig()
    {
        Config.OpenDir = OpenDirEdit.Text;
        Config.OpenMask = OpenMaskEdit.Text;

        Config.SaveDir = SaveDirEdit.Text;
        Config.SaveMask = SaveMaskEdit.Text;
        Config.SaveFormat = SaveFormatChoice.Text;

        Config.CorrAccount = CorrAccountEdit.Text;
        Config.BankINN = BankInnEdit.Text;
        Config.BankKPP = BankKppEdit.Text;
        Config.NameTemplate = NameTemplateEdit.Text;
        Config.PurposeTemplate = PurposeTemplateEdit.Text;

        Config.SwiftNameLimit = int.Parse(SwiftNameLimitChoice.Text);

        Config.BankSWIFT = BankSwiftEdit.Text;
        Config.CorrSWIFT = CorrSwiftEdit.Text;

        Config.Save();

        MessageBox.Show("Параметры сохранены.", Application.ProductName,
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void OpenDirButton_Click(object sender, EventArgs e)
    {
        if (OpenFolderDialog.ShowDialog() == DialogResult.OK)
        {
            OpenDirEdit.Text = OpenFolderDialog.SelectedPath;
        }
    }

    private void SaveDirButton_Click(object sender, EventArgs e)
    {
        if (SaveFolderDialog.ShowDialog() == DialogResult.OK)
        {
            SaveDirEdit.Text = SaveFolderDialog.SelectedPath;
        }
    }

    private void ResetButton_Click(object sender, EventArgs e)
    {
        ResetConfig();
    }

    private void OKButton_Click(object sender, EventArgs e)
    {
        SaveConfig();
    }
}
