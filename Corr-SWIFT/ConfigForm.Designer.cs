namespace CorrSWIFT
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.OpenBox = new System.Windows.Forms.GroupBox();
            this.SelectFileButton = new System.Windows.Forms.Button();
            this.SelectFileEdit = new System.Windows.Forms.TextBox();
            this.SelectFileLabel = new System.Windows.Forms.Label();
            this.OpenMaskEdit = new System.Windows.Forms.TextBox();
            this.OpenMaskLabel = new System.Windows.Forms.Label();
            this.OpenDirButton = new System.Windows.Forms.Button();
            this.OpenDirEdit = new System.Windows.Forms.TextBox();
            this.OpenDirLabel = new System.Windows.Forms.Label();
            this.OpenFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SaveFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SaveBox = new System.Windows.Forms.GroupBox();
            this.SaveFormatChoice = new System.Windows.Forms.ComboBox();
            this.OutFormatLabel = new System.Windows.Forms.Label();
            this.SaveMaskEdit = new System.Windows.Forms.TextBox();
            this.SaveMaskLabel = new System.Windows.Forms.Label();
            this.SaveDirButton = new System.Windows.Forms.Button();
            this.SaveDirEdit = new System.Windows.Forms.TextBox();
            this.SaveDirLabel = new System.Windows.Forms.Label();
            this.BankBox = new System.Windows.Forms.GroupBox();
            this.CorrSwiftEdit = new System.Windows.Forms.TextBox();
            this.CorrSwiftLabel = new System.Windows.Forms.Label();
            this.BankSwiftEdit = new System.Windows.Forms.TextBox();
            this.BankSwiftLabel = new System.Windows.Forms.Label();
            this.BankKppEdit = new System.Windows.Forms.TextBox();
            this.BankKPPLabel = new System.Windows.Forms.Label();
            this.BankInnEdit = new System.Windows.Forms.TextBox();
            this.BankINNLabel = new System.Windows.Forms.Label();
            this.CorrAccountEdit = new System.Windows.Forms.TextBox();
            this.CorrAccountLabel = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.AbortButton = new System.Windows.Forms.Button();
            this.TemplatesNameGroup = new System.Windows.Forms.GroupBox();
            this.TemplatesNameEdit = new System.Windows.Forms.TextBox();
            this.TemplatesPurposeGroup = new System.Windows.Forms.GroupBox();
            this.TemplatesPurposeEdit = new System.Windows.Forms.TextBox();
            this.ProfileChoice = new System.Windows.Forms.ComboBox();
            this.ProfileLabel = new System.Windows.Forms.Label();
            this.SelectFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.OpenBox.SuspendLayout();
            this.SaveBox.SuspendLayout();
            this.BankBox.SuspendLayout();
            this.TemplatesNameGroup.SuspendLayout();
            this.TemplatesPurposeGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // OpenBox
            // 
            this.OpenBox.Controls.Add(this.SelectFileButton);
            this.OpenBox.Controls.Add(this.SelectFileEdit);
            this.OpenBox.Controls.Add(this.SelectFileLabel);
            this.OpenBox.Controls.Add(this.OpenMaskEdit);
            this.OpenBox.Controls.Add(this.OpenMaskLabel);
            this.OpenBox.Controls.Add(this.OpenDirButton);
            this.OpenBox.Controls.Add(this.OpenDirEdit);
            this.OpenBox.Controls.Add(this.OpenDirLabel);
            this.OpenBox.Location = new System.Drawing.Point(17, 14);
            this.OpenBox.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.OpenBox.Name = "OpenBox";
            this.OpenBox.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.OpenBox.Size = new System.Drawing.Size(1046, 199);
            this.OpenBox.TabIndex = 0;
            this.OpenBox.TabStop = false;
            this.OpenBox.Text = "Исходные файлы и Справочник БИК";
            // 
            // SelectFileButton
            // 
            this.SelectFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectFileButton.Location = new System.Drawing.Point(895, 133);
            this.SelectFileButton.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.SelectFileButton.Name = "SelectFileButton";
            this.SelectFileButton.Size = new System.Drawing.Size(139, 40);
            this.SelectFileButton.TabIndex = 7;
            this.SelectFileButton.Text = "Выбор...";
            this.SelectFileButton.UseVisualStyleBackColor = true;
            this.SelectFileButton.Click += new System.EventHandler(this.SelectFileButton_Click);
            // 
            // SelectFileEdit
            // 
            this.SelectFileEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectFileEdit.Location = new System.Drawing.Point(197, 135);
            this.SelectFileEdit.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.SelectFileEdit.Name = "SelectFileEdit";
            this.SelectFileEdit.PlaceholderText = "C:\\TEMP\\ED807.xml";
            this.SelectFileEdit.Size = new System.Drawing.Size(684, 32);
            this.SelectFileEdit.TabIndex = 6;
            this.SelectFileEdit.TextChanged += new System.EventHandler(this.SelectFileEdit_TextChanged);
            // 
            // SelectFileLabel
            // 
            this.SelectFileLabel.AutoSize = true;
            this.SelectFileLabel.Location = new System.Drawing.Point(20, 140);
            this.SelectFileLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.SelectFileLabel.Name = "SelectFileLabel";
            this.SelectFileLabel.Size = new System.Drawing.Size(141, 26);
            this.SelectFileLabel.TabIndex = 5;
            this.SelectFileLabel.Text = "Справочник:";
            // 
            // OpenMaskEdit
            // 
            this.OpenMaskEdit.Location = new System.Drawing.Point(197, 83);
            this.OpenMaskEdit.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.OpenMaskEdit.Name = "OpenMaskEdit";
            this.OpenMaskEdit.PlaceholderText = "*.xml";
            this.OpenMaskEdit.Size = new System.Drawing.Size(251, 32);
            this.OpenMaskEdit.TabIndex = 4;
            // 
            // OpenMaskLabel
            // 
            this.OpenMaskLabel.AutoSize = true;
            this.OpenMaskLabel.Location = new System.Drawing.Point(20, 88);
            this.OpenMaskLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.OpenMaskLabel.Name = "OpenMaskLabel";
            this.OpenMaskLabel.Size = new System.Drawing.Size(82, 26);
            this.OpenMaskLabel.TabIndex = 3;
            this.OpenMaskLabel.Text = "Маска:";
            // 
            // OpenDirButton
            // 
            this.OpenDirButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenDirButton.Location = new System.Drawing.Point(895, 31);
            this.OpenDirButton.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.OpenDirButton.Name = "OpenDirButton";
            this.OpenDirButton.Size = new System.Drawing.Size(139, 40);
            this.OpenDirButton.TabIndex = 2;
            this.OpenDirButton.Text = "Выбор...";
            this.OpenDirButton.UseVisualStyleBackColor = true;
            this.OpenDirButton.Click += new System.EventHandler(this.OpenDirButton_Click);
            // 
            // OpenDirEdit
            // 
            this.OpenDirEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenDirEdit.Location = new System.Drawing.Point(197, 33);
            this.OpenDirEdit.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.OpenDirEdit.Name = "OpenDirEdit";
            this.OpenDirEdit.PlaceholderText = "C:\\TEMP\\IN";
            this.OpenDirEdit.Size = new System.Drawing.Size(684, 32);
            this.OpenDirEdit.TabIndex = 1;
            this.OpenDirEdit.TextChanged += new System.EventHandler(this.OpenDirEdit_TextChanged);
            // 
            // OpenDirLabel
            // 
            this.OpenDirLabel.AutoSize = true;
            this.OpenDirLabel.Location = new System.Drawing.Point(20, 38);
            this.OpenDirLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.OpenDirLabel.Name = "OpenDirLabel";
            this.OpenDirLabel.Size = new System.Drawing.Size(140, 26);
            this.OpenDirLabel.TabIndex = 0;
            this.OpenDirLabel.Text = "Директория:";
            // 
            // OpenFolderDialog
            // 
            this.OpenFolderDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // SaveFolderDialog
            // 
            this.SaveFolderDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // SaveBox
            // 
            this.SaveBox.Controls.Add(this.SaveFormatChoice);
            this.SaveBox.Controls.Add(this.OutFormatLabel);
            this.SaveBox.Controls.Add(this.SaveMaskEdit);
            this.SaveBox.Controls.Add(this.SaveMaskLabel);
            this.SaveBox.Controls.Add(this.SaveDirButton);
            this.SaveBox.Controls.Add(this.SaveDirEdit);
            this.SaveBox.Controls.Add(this.SaveDirLabel);
            this.SaveBox.Location = new System.Drawing.Point(17, 224);
            this.SaveBox.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.SaveBox.Name = "SaveBox";
            this.SaveBox.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.SaveBox.Size = new System.Drawing.Size(1046, 137);
            this.SaveBox.TabIndex = 1;
            this.SaveBox.TabStop = false;
            this.SaveBox.Text = "Конечные файлы с {id}, {no} или * в маске";
            // 
            // SaveFormatChoice
            // 
            this.SaveFormatChoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SaveFormatChoice.FormattingEnabled = true;
            this.SaveFormatChoice.Items.AddRange(new object[] {
            "УФЭБС",
            "SWIFT"});
            this.SaveFormatChoice.Location = new System.Drawing.Point(639, 81);
            this.SaveFormatChoice.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.SaveFormatChoice.Name = "SaveFormatChoice";
            this.SaveFormatChoice.Size = new System.Drawing.Size(136, 34);
            this.SaveFormatChoice.TabIndex = 6;
            this.SaveFormatChoice.SelectedValueChanged += new System.EventHandler(this.SaveFormatChoice_SelectedValueChanged);
            // 
            // OutFormatLabel
            // 
            this.OutFormatLabel.AutoSize = true;
            this.OutFormatLabel.Location = new System.Drawing.Point(490, 88);
            this.OutFormatLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.OutFormatLabel.Name = "OutFormatLabel";
            this.OutFormatLabel.Size = new System.Drawing.Size(99, 26);
            this.OutFormatLabel.TabIndex = 5;
            this.OutFormatLabel.Text = "Формат:";
            // 
            // SaveMaskEdit
            // 
            this.SaveMaskEdit.Location = new System.Drawing.Point(197, 83);
            this.SaveMaskEdit.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.SaveMaskEdit.Name = "SaveMaskEdit";
            this.SaveMaskEdit.PlaceholderText = "{id}.txt";
            this.SaveMaskEdit.Size = new System.Drawing.Size(251, 32);
            this.SaveMaskEdit.TabIndex = 4;
            // 
            // SaveMaskLabel
            // 
            this.SaveMaskLabel.AutoSize = true;
            this.SaveMaskLabel.Location = new System.Drawing.Point(20, 88);
            this.SaveMaskLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.SaveMaskLabel.Name = "SaveMaskLabel";
            this.SaveMaskLabel.Size = new System.Drawing.Size(82, 26);
            this.SaveMaskLabel.TabIndex = 3;
            this.SaveMaskLabel.Text = "Маска:";
            // 
            // SaveDirButton
            // 
            this.SaveDirButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveDirButton.Location = new System.Drawing.Point(895, 31);
            this.SaveDirButton.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.SaveDirButton.Name = "SaveDirButton";
            this.SaveDirButton.Size = new System.Drawing.Size(139, 40);
            this.SaveDirButton.TabIndex = 2;
            this.SaveDirButton.Text = "Выбор...";
            this.SaveDirButton.UseVisualStyleBackColor = true;
            this.SaveDirButton.Click += new System.EventHandler(this.SaveDirButton_Click);
            // 
            // SaveDirEdit
            // 
            this.SaveDirEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveDirEdit.Location = new System.Drawing.Point(197, 33);
            this.SaveDirEdit.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.SaveDirEdit.Name = "SaveDirEdit";
            this.SaveDirEdit.PlaceholderText = "C:\\TEMP\\OUT";
            this.SaveDirEdit.Size = new System.Drawing.Size(684, 32);
            this.SaveDirEdit.TabIndex = 1;
            this.SaveDirEdit.TextChanged += new System.EventHandler(this.SaveDirEdit_TextChanged);
            // 
            // SaveDirLabel
            // 
            this.SaveDirLabel.AutoSize = true;
            this.SaveDirLabel.Location = new System.Drawing.Point(20, 38);
            this.SaveDirLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.SaveDirLabel.Name = "SaveDirLabel";
            this.SaveDirLabel.Size = new System.Drawing.Size(140, 26);
            this.SaveDirLabel.TabIndex = 0;
            this.SaveDirLabel.Text = "Директория:";
            // 
            // BankBox
            // 
            this.BankBox.Controls.Add(this.CorrSwiftEdit);
            this.BankBox.Controls.Add(this.CorrSwiftLabel);
            this.BankBox.Controls.Add(this.BankSwiftEdit);
            this.BankBox.Controls.Add(this.BankSwiftLabel);
            this.BankBox.Controls.Add(this.BankKppEdit);
            this.BankBox.Controls.Add(this.BankKPPLabel);
            this.BankBox.Controls.Add(this.BankInnEdit);
            this.BankBox.Controls.Add(this.BankINNLabel);
            this.BankBox.Controls.Add(this.CorrAccountEdit);
            this.BankBox.Controls.Add(this.CorrAccountLabel);
            this.BankBox.Location = new System.Drawing.Point(17, 371);
            this.BankBox.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.BankBox.Name = "BankBox";
            this.BankBox.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.BankBox.Size = new System.Drawing.Size(1046, 187);
            this.BankBox.TabIndex = 2;
            this.BankBox.TabStop = false;
            this.BankBox.Text = "Реквизиты нашего Банка и счета у банка-посредника";
            // 
            // CorrSwiftEdit
            // 
            this.CorrSwiftEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CorrSwiftEdit.Location = new System.Drawing.Point(639, 132);
            this.CorrSwiftEdit.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.CorrSwiftEdit.Name = "CorrSwiftEdit";
            this.CorrSwiftEdit.PlaceholderText = "CITVRU2P";
            this.CorrSwiftEdit.Size = new System.Drawing.Size(251, 32);
            this.CorrSwiftEdit.TabIndex = 9;
            this.CorrSwiftEdit.TextChanged += new System.EventHandler(this.CorrSwiftEdit_TextChanged);
            // 
            // CorrSwiftLabel
            // 
            this.CorrSwiftLabel.AutoSize = true;
            this.CorrSwiftLabel.Location = new System.Drawing.Point(490, 137);
            this.CorrSwiftLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.CorrSwiftLabel.Name = "CorrSwiftLabel";
            this.CorrSwiftLabel.Size = new System.Drawing.Size(136, 26);
            this.CorrSwiftLabel.TabIndex = 8;
            this.CorrSwiftLabel.Text = "Кор. SWIFT:";
            // 
            // BankSwiftEdit
            // 
            this.BankSwiftEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BankSwiftEdit.Location = new System.Drawing.Point(197, 132);
            this.BankSwiftEdit.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.BankSwiftEdit.Name = "BankSwiftEdit";
            this.BankSwiftEdit.PlaceholderText = "CITVRU2P";
            this.BankSwiftEdit.Size = new System.Drawing.Size(251, 32);
            this.BankSwiftEdit.TabIndex = 5;
            this.BankSwiftEdit.TextChanged += new System.EventHandler(this.BankSwiftEdit_TextChanged);
            // 
            // BankSwiftLabel
            // 
            this.BankSwiftLabel.AutoSize = true;
            this.BankSwiftLabel.Location = new System.Drawing.Point(20, 137);
            this.BankSwiftLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.BankSwiftLabel.Name = "BankSwiftLabel";
            this.BankSwiftLabel.Size = new System.Drawing.Size(85, 26);
            this.BankSwiftLabel.TabIndex = 4;
            this.BankSwiftLabel.Text = "SWIFT:";
            // 
            // BankKppEdit
            // 
            this.BankKppEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BankKppEdit.Location = new System.Drawing.Point(197, 83);
            this.BankKppEdit.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.BankKppEdit.Name = "BankKppEdit";
            this.BankKppEdit.PlaceholderText = "784101001";
            this.BankKppEdit.Size = new System.Drawing.Size(251, 32);
            this.BankKppEdit.TabIndex = 3;
            this.BankKppEdit.TextChanged += new System.EventHandler(this.BankKppEdit_TextChanged);
            // 
            // BankKPPLabel
            // 
            this.BankKPPLabel.AutoSize = true;
            this.BankKPPLabel.Location = new System.Drawing.Point(20, 88);
            this.BankKPPLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.BankKPPLabel.Name = "BankKPPLabel";
            this.BankKPPLabel.Size = new System.Drawing.Size(65, 26);
            this.BankKPPLabel.TabIndex = 2;
            this.BankKPPLabel.Text = "КПП:";
            // 
            // BankInnEdit
            // 
            this.BankInnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BankInnEdit.Location = new System.Drawing.Point(197, 33);
            this.BankInnEdit.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.BankInnEdit.Name = "BankInnEdit";
            this.BankInnEdit.PlaceholderText = "7831001422";
            this.BankInnEdit.Size = new System.Drawing.Size(251, 32);
            this.BankInnEdit.TabIndex = 1;
            this.BankInnEdit.TextChanged += new System.EventHandler(this.BankInnEdit_TextChanged);
            // 
            // BankINNLabel
            // 
            this.BankINNLabel.AutoSize = true;
            this.BankINNLabel.Location = new System.Drawing.Point(20, 38);
            this.BankINNLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.BankINNLabel.Name = "BankINNLabel";
            this.BankINNLabel.Size = new System.Drawing.Size(66, 26);
            this.BankINNLabel.TabIndex = 0;
            this.BankINNLabel.Text = "ИНН:";
            // 
            // CorrAccountEdit
            // 
            this.CorrAccountEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CorrAccountEdit.Location = new System.Drawing.Point(639, 26);
            this.CorrAccountEdit.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.CorrAccountEdit.Name = "CorrAccountEdit";
            this.CorrAccountEdit.PlaceholderText = "30101810600000000702";
            this.CorrAccountEdit.Size = new System.Drawing.Size(388, 32);
            this.CorrAccountEdit.TabIndex = 7;
            this.CorrAccountEdit.TextChanged += new System.EventHandler(this.CorrAccountEdit_TextChanged);
            // 
            // CorrAccountLabel
            // 
            this.CorrAccountLabel.AutoSize = true;
            this.CorrAccountLabel.Location = new System.Drawing.Point(490, 38);
            this.CorrAccountLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.CorrAccountLabel.Name = "CorrAccountLabel";
            this.CorrAccountLabel.Size = new System.Drawing.Size(114, 26);
            this.CorrAccountLabel.TabIndex = 6;
            this.CorrAccountLabel.Text = "Кор. счет:";
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(758, 790);
            this.OKButton.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(139, 40);
            this.OKButton.TabIndex = 7;
            this.OKButton.Text = "Сохранить";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // AbortButton
            // 
            this.AbortButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AbortButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.AbortButton.Location = new System.Drawing.Point(908, 790);
            this.AbortButton.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.AbortButton.Name = "AbortButton";
            this.AbortButton.Size = new System.Drawing.Size(139, 40);
            this.AbortButton.TabIndex = 8;
            this.AbortButton.Text = "Отмена";
            this.AbortButton.UseVisualStyleBackColor = true;
            // 
            // TemplatesNameGroup
            // 
            this.TemplatesNameGroup.Controls.Add(this.TemplatesNameEdit);
            this.TemplatesNameGroup.Location = new System.Drawing.Point(17, 569);
            this.TemplatesNameGroup.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.TemplatesNameGroup.Name = "TemplatesNameGroup";
            this.TemplatesNameGroup.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.TemplatesNameGroup.Size = new System.Drawing.Size(1046, 97);
            this.TemplatesNameGroup.TabIndex = 3;
            this.TemplatesNameGroup.TabStop = false;
            this.TemplatesNameGroup.Text = "Шаблон Плательщика за 3 лицо с {name} и {acc} клиента";
            // 
            // TemplatesNameEdit
            // 
            this.TemplatesNameEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TemplatesNameEdit.Location = new System.Drawing.Point(20, 38);
            this.TemplatesNameEdit.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.TemplatesNameEdit.Name = "TemplatesNameEdit";
            this.TemplatesNameEdit.PlaceholderText = "АО \"Сити Инвест Банк\" ИНН 7831001422 ({name} р/с {acc})";
            this.TemplatesNameEdit.Size = new System.Drawing.Size(1007, 32);
            this.TemplatesNameEdit.TabIndex = 0;
            // 
            // TemplatesPurposeGroup
            // 
            this.TemplatesPurposeGroup.Controls.Add(this.TemplatesPurposeEdit);
            this.TemplatesPurposeGroup.Location = new System.Drawing.Point(17, 676);
            this.TemplatesPurposeGroup.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.TemplatesPurposeGroup.Name = "TemplatesPurposeGroup";
            this.TemplatesPurposeGroup.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.TemplatesPurposeGroup.Size = new System.Drawing.Size(1046, 97);
            this.TemplatesPurposeGroup.TabIndex = 4;
            this.TemplatesPurposeGroup.TabStop = false;
            this.TemplatesPurposeGroup.Text = "Шаблон Назначения за 3 лицо с ИНН и КПП Банка, c {name} и {purpose}";
            // 
            // TemplatesPurposeEdit
            // 
            this.TemplatesPurposeEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TemplatesPurposeEdit.Location = new System.Drawing.Point(20, 38);
            this.TemplatesPurposeEdit.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.TemplatesPurposeEdit.Name = "TemplatesPurposeEdit";
            this.TemplatesPurposeEdit.PlaceholderText = "//7831001422//784101001//{name}//{purpose}";
            this.TemplatesPurposeEdit.Size = new System.Drawing.Size(1007, 32);
            this.TemplatesPurposeEdit.TabIndex = 0;
            // 
            // ProfileChoice
            // 
            this.ProfileChoice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ProfileChoice.FormattingEnabled = true;
            this.ProfileChoice.Location = new System.Drawing.Point(214, 792);
            this.ProfileChoice.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.ProfileChoice.Name = "ProfileChoice";
            this.ProfileChoice.Size = new System.Drawing.Size(251, 34);
            this.ProfileChoice.Sorted = true;
            this.ProfileChoice.TabIndex = 6;
            this.ProfileChoice.SelectedValueChanged += new System.EventHandler(this.ProfileChoice_SelectedValueChanged);
            // 
            // ProfileLabel
            // 
            this.ProfileLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ProfileLabel.AutoSize = true;
            this.ProfileLabel.Location = new System.Drawing.Point(37, 797);
            this.ProfileLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.ProfileLabel.Name = "ProfileLabel";
            this.ProfileLabel.Size = new System.Drawing.Size(114, 26);
            this.ProfileLabel.TabIndex = 5;
            this.ProfileLabel.Text = "Профиль:";
            // 
            // SelectFileDialog
            // 
            this.SelectFileDialog.DefaultExt = "xml";
            this.SelectFileDialog.FileName = "ED807.xml";
            this.SelectFileDialog.SupportMultiDottedExtensions = true;
            this.SelectFileDialog.Title = "Укажите Справочник БИК";
            // 
            // ConfigForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.AbortButton;
            this.ClientSize = new System.Drawing.Size(1085, 848);
            this.Controls.Add(this.ProfileChoice);
            this.Controls.Add(this.ProfileLabel);
            this.Controls.Add(this.TemplatesPurposeGroup);
            this.Controls.Add(this.TemplatesNameGroup);
            this.Controls.Add(this.AbortButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.BankBox);
            this.Controls.Add(this.SaveBox);
            this.Controls.Add(this.OpenBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "ConfigForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Параметры";
            this.OpenBox.ResumeLayout(false);
            this.OpenBox.PerformLayout();
            this.SaveBox.ResumeLayout(false);
            this.SaveBox.PerformLayout();
            this.BankBox.ResumeLayout(false);
            this.BankBox.PerformLayout();
            this.TemplatesNameGroup.ResumeLayout(false);
            this.TemplatesNameGroup.PerformLayout();
            this.TemplatesPurposeGroup.ResumeLayout(false);
            this.TemplatesPurposeGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox OpenBox;
        private TextBox OpenMaskEdit;
        private Label OpenMaskLabel;
        private Button OpenDirButton;
        private TextBox OpenDirEdit;
        private Label OpenDirLabel;
        private FolderBrowserDialog OpenFolderDialog;
        private FolderBrowserDialog SaveFolderDialog;
        private GroupBox SaveBox;
        private TextBox SaveMaskEdit;
        private Label SaveMaskLabel;
        private Button SaveDirButton;
        private TextBox SaveDirEdit;
        private Label SaveDirLabel;
        private GroupBox BankBox;
        private TextBox BankKppEdit;
        private Label BankKPPLabel;
        private TextBox BankInnEdit;
        private Label BankINNLabel;
        private TextBox CorrAccountEdit;
        private Label CorrAccountLabel;
        private Button OKButton;
        private Button AbortButton;
        private TextBox CorrSwiftEdit;
        private Label CorrSwiftLabel;
        private TextBox BankSwiftEdit;
        private Label BankSwiftLabel;
        private ComboBox SaveFormatChoice;
        private Label OutFormatLabel;
        private GroupBox TemplatesNameGroup;
        private TextBox TemplatesNameEdit;
        private GroupBox TemplatesPurposeGroup;
        private TextBox TemplatesPurposeEdit;
        private ComboBox ProfileChoice;
        private Label ProfileLabel;
        private OpenFileDialog SelectFileDialog;
        private Button SelectFileButton;
        private TextBox SelectFileEdit;
        private Label SelectFileLabel;
    }
}