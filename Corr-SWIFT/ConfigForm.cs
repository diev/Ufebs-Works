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

        ProfileChoice.Items.AddRange(Config.Profiles);
        ProfileChoice.Items.Add(string.Empty);
        ProfileChoice.Text = Config.Profile;

        LoadConfig();
    }

    private void LoadConfig()
    {
        Config.Profile = ProfileChoice.Text;

        // string

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

        SwiftPurposeFieldChoice.Text = Config.SwiftPurposeField;

        // int

        SwiftNameLimitChoice.Text = Config.SwiftNameLimit == 0
            ? SwiftNameLimitChoice.Items[0].ToString()
            : Config.SwiftNameLimit.ToString();
    }

    private void SaveConfig()
    {
        string profile = ProfileChoice.Text;

        if (!ProfileChoice.Items.Contains(profile))
        {
            var list = new List<string>(Config.Profiles);
            list.Add(profile);
            Config.Profiles = list.ToArray();
        }

        Config.Profile = profile;

        // string TextBox

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

        // string ComboBox

        Config.SwiftPurposeField = SwiftPurposeFieldChoice.Text;

        // int ComboBox

        Config.SwiftNameLimit = int.Parse(SwiftNameLimitChoice.Text);

        Config.Save();

        MessageBox.Show($"Параметры профиля \"{profile}\" сохранены.", 
            Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
