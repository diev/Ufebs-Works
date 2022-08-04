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

namespace CorrSWIFT;

public partial class ConfigForm : Form
{
    public ConfigForm()
    {
        InitializeComponent();

        ProfileChoice.Items.AddRange(Config.Profiles);
        ProfileChoice.Items.Add(string.Empty);
        ProfileChoice.Text = Config.Profile;

        string pwd = Directory.GetCurrentDirectory();
        OpenDirEdit.PlaceholderText = pwd;
        SaveDirEdit.PlaceholderText = pwd;
        SelectFileEdit.PlaceholderText = Path.Combine(pwd, "ED807.xml");

        //OpenFolderDialog.InitialDirectory = pwd;
        //SaveFolderDialog.InitialDirectory = pwd;
        //SelectFileDialog.InitialDirectory = pwd;
        //SelectFileDialog.FileName = SelectFileEdit.PlaceholderText;

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
        SelectFileEdit.Text = Config.ED807;

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
        string profile = ProfileChoice.Text.Trim();

        if (!ProfileChoice.Items.Contains(profile))
        {
            var list = new List<string>(Config.Profiles)
            {
                profile
            };
            Config.Profiles = list.ToArray();
        }

        Config.Profile = profile;

        // string TextBox

        Config.OpenDir = OpenDirEdit.Text.Trim(); // ?? OpenDirEdit.PlaceholderText;
        Config.OpenMask = OpenMaskEdit.Text.Trim(); // ?? OpenDirEdit.PlaceholderText;

        Config.SaveDir = SaveDirEdit.Text.Trim(); // ?? SaveDirEdit.PlaceholderText;
        Config.SaveMask = SaveMaskEdit.Text.Trim(); // ?? SaveMaskEdit.PlaceholderText;
        Config.SaveFormat = SaveFormatChoice.Text;
        Config.ED807 = SelectFileEdit.Text.Trim(); // ?? SelectFileEdit.PlaceholderText;

        Config.BankINN = BankInnEdit.Text.Trim(); // ?? BankInnEdit.PlaceholderText;
        Config.BankKPP = BankKppEdit.Text.Trim(); // ?? BankKppEdit.PlaceholderText;
        Config.BankSWIFT = BankSwiftEdit.Text.Trim(); // ?? BankSwiftEdit.PlaceholderText;

        Config.CorrAccount = CorrAccountEdit.Text.Trim(); // ?? CorrAccountEdit.PlaceholderText;
        Config.CorrSWIFT = CorrSwiftEdit.Text.Trim(); // ?? CorrSwiftEdit.PlaceholderText;

        Config.TemplatesName = TemplatesNameEdit.Text.Trim(); // ?? TemplatesNameEdit.PlaceholderText;
        Config.TemplatesPurpose = TemplatesPurposeEdit.Text.Trim(); // ?? TemplatesPurposeEdit.PlaceholderText;

        // string ComboBox

        Config.SwiftPurposeField = SwiftPurposeFieldChoice.Text;

        // int ComboBox

        Config.SwiftNameLimit = int.Parse(SwiftNameLimitChoice.Text);

        Config.Save(Application.ExecutablePath);

        string msg = profile.Length == 0
            ? "Параметры сохранены."
            : $"Параметры профиля \"{profile}\" сохранены.";

        MessageBox.Show(msg, Application.ProductName, 
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void OpenDirButton_Click(object sender, EventArgs e)
    {
        try
        {
            string path = Path.GetFullPath(OpenDirEdit.Text);

            OpenFolderDialog.InitialDirectory = path;
            OpenFolderDialog.SelectedPath = path;
        }
        catch { }

        if (OpenFolderDialog.ShowDialog() == DialogResult.OK)
        {
            OpenDirEdit.Text = OpenFolderDialog.SelectedPath;
        }
    }

    private void SaveDirButton_Click(object sender, EventArgs e)
    {
        try
        {
            string path = Path.GetFullPath(SaveDirEdit.Text);

            SaveFolderDialog.InitialDirectory = path;
            SaveFolderDialog.SelectedPath = path;
        }
        catch { }

        if (SaveFolderDialog.ShowDialog() == DialogResult.OK)
        {
            SaveDirEdit.Text = SaveFolderDialog.SelectedPath;
        }
    }

    private void SelectFileButton_Click(object sender, EventArgs e)
    {
        try
        {
            string file = Path.GetFullPath(SelectFileEdit.Text);
            string path = Path.GetDirectoryName(file);

            SelectFileDialog.InitialDirectory = path;
            SelectFileDialog.FileName = Path.GetFileName(file);
        }
        catch { }

        if (SelectFileDialog.ShowDialog() == DialogResult.OK)
        {
            SelectFileEdit.Text = SelectFileDialog.FileName;
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

    private void SaveFormatChoice_SelectedValueChanged(object sender, EventArgs e)
    {
        bool on = (sender as ComboBox)?.Text == Config.SwiftFormat;

        //SelectFileLabel.Visible = on;
        BankSwiftLabel.Visible = on;
        CorrSwiftLabel.Visible = on;
        SwiftPurposeFieldLabel.Visible = on;
        SwiftNameLimitLabel.Visible = on;

        //SelectFileEdit.Visible = on;
        BankSwiftEdit.Visible = on;
        CorrSwiftEdit.Visible = on;
        SwiftPurposeFieldChoice.Visible = on;
        SwiftNameLimitChoice.Visible = on;

        //SelectFileButton.Visible = on;

        SaveMaskEdit.PlaceholderText = on
            ? "{id}.txt"
            : "*_.xml";
    }

    private void OpenDirEdit_TextChanged(object sender, EventArgs e)
    {
        var edit = sender as TextBox;

        if (edit != null)
        {
            edit.BackColor = Directory.Exists(edit.Text)
                ? BackColor
                : Color.LightPink;
        }
    }

    private void SaveDirEdit_TextChanged(object sender, EventArgs e)
    {
        var edit = sender as TextBox;

        if (edit != null)
        {
            edit.BackColor = Directory.Exists(edit.Text)
                ? BackColor
                : Color.LightPink;
        }
    }

    private void SelectFileEdit_TextChanged(object sender, EventArgs e)
    {
        var edit = sender as TextBox;

        if (edit != null)
        {
            edit.BackColor = File.Exists(edit.Text)
                ? BackColor
                : Color.LightPink;
        }
    }

    private void BankInnEdit_TextChanged(object sender, EventArgs e)
    {
        if (sender is TextBox edit)
        {
            edit.BackColor = edit.TextLength == 10
                ? BackColor
                : Color.LightPink;
        }
    }

    private void BankKppEdit_TextChanged(object sender, EventArgs e)
    {
        if (sender is TextBox edit)
        {
            edit.BackColor = edit.TextLength == 9
                ? BackColor
                : Color.LightPink;
        }
    }

    private void BankSwiftEdit_TextChanged(object sender, EventArgs e)
    {
        if (sender is TextBox edit)
        {
            edit.BackColor = edit.TextLength == 8
                ? BackColor
                : Color.LightPink;
        }
    }

    private void CorrSwiftEdit_TextChanged(object sender, EventArgs e)
    {
        if (sender is TextBox edit)
        {
            edit.BackColor = edit.TextLength == 8
                ? BackColor
                : Color.LightPink;
        }
    }

    private void CorrAccountEdit_TextChanged(object sender, EventArgs e)
    {
        if (sender is TextBox edit)
        {
            edit.BackColor = edit.TextLength == 20
                ? BackColor
                : Color.LightPink;
        }
    }
}
