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

        //ProfileChoice.DataSource = Config.Profiles.Split(';');
        var profiles = Config.Profiles.Split(';', StringSplitOptions.RemoveEmptyEntries);

        foreach (var item in profiles)
        {
            ProfileChoice.Items.Add(item);
        }

        ProfileChoice.Text = Config.Profile;

        LoadConfig();
    }

    private void LoadConfig()
    {
        Config.Profile = ProfileChoice.Text;

        OpenDirEdit.Text = Config.OpenDir;
        OpenMaskEdit.Text = Config.OpenMask;

        SaveDirEdit.Text = Config.SaveDir;
        SaveMaskEdit.Text = Config.SaveMask;
        SaveFormatChoice.Text = Config.SaveFormat;

        BankInnEdit.Text = Config.BankINN;
        BankKppEdit.Text = Config.BankKPP;
        BankSwiftEdit.Text = Config.BankSWIFT;

        CorrAccountEdit.Text = Config.CorrAccount;
        CorrSwiftEdit.Text = Config.CorrSWIFT;

        TemplatesNameEdit.Text = Config.TemplatesName;
        TemplatesPurposeEdit.Text = Config.TemplatesPurpose;

        SwiftNameLimitChoice.Text = Config.SwiftNameLimit.ToString();
        SwiftPurposeFieldChoice.Text = Config.SwiftPurposeField;
    }

    private void SaveConfig()
    {
        Config.Profile = ProfileChoice.Text;

        //Config.Profiles = string.Join(';', (string[])ProfileChoice.DataSource);

        string profiles = ProfileChoice.Text;

        foreach (var item in ProfileChoice.Items)
        {
            string profile = item.ToString();

            if (!profiles.Contains(profile))
            {
                profiles += $";{profile}";
            }
        }

        Config.Profiles = profiles;

        Config.OpenDir = OpenDirEdit.Text ?? OpenDirEdit.PlaceholderText;
        Config.OpenMask = OpenMaskEdit.Text ?? OpenDirEdit.PlaceholderText;

        Config.SaveDir = SaveDirEdit.Text ?? SaveDirEdit.PlaceholderText;
        Config.SaveMask = SaveMaskEdit.Text ?? SaveMaskEdit.PlaceholderText;
        Config.SaveFormat = SaveFormatChoice.Text;

        Config.BankINN = BankInnEdit.Text ?? BankInnEdit.PlaceholderText;
        Config.BankKPP = BankKppEdit.Text ?? BankKppEdit.PlaceholderText;
        Config.BankSWIFT = BankSwiftEdit.Text ?? BankSwiftEdit.PlaceholderText;

        Config.CorrAccount = CorrAccountEdit.Text ?? CorrAccountEdit.PlaceholderText;
        Config.CorrSWIFT = CorrSwiftEdit.Text ?? CorrSwiftEdit.PlaceholderText;

        Config.TemplatesName = TemplatesNameEdit.Text ?? TemplatesNameEdit.PlaceholderText;
        Config.TemplatesPurpose = TemplatesPurposeEdit.Text ?? TemplatesPurposeEdit.PlaceholderText;

        Config.SwiftNameLimit = int.Parse(SwiftNameLimitChoice.Text);
        Config.SwiftPurposeField = SwiftPurposeFieldChoice.Text;

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

    private void OKButton_Click(object sender, EventArgs e)
    {
        SaveConfig();
    }

    private void ProfileChoice_SelectedValueChanged(object sender, EventArgs e)
    {
        LoadConfig();
    }
}
