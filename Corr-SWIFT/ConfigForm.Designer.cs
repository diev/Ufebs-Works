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
            this.SwiftNameLimitChoice = new System.Windows.Forms.ComboBox();
            this.TemplatesNameEdit = new System.Windows.Forms.TextBox();
            this.SwiftNameLimitLabel = new System.Windows.Forms.Label();
            this.TemplatesPurposeGroup = new System.Windows.Forms.GroupBox();
            this.SwiftPurposeFieldChoice = new System.Windows.Forms.ComboBox();
            this.SwiftPurposeFieldLabel = new System.Windows.Forms.Label();
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
            this.OpenBox.Location = new System.Drawing.Point(9, 8);
            this.OpenBox.Name = "OpenBox";
            this.OpenBox.Size = new System.Drawing.Size(563, 115);
            this.OpenBox.TabIndex = 0;
            this.OpenBox.TabStop = false;
            this.OpenBox.Text = "Исходные файлы и Справочник БИК";
            // 
            // SelectFileButton
            // 
            this.SelectFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectFileButton.Location = new System.Drawing.Point(482, 77);
            this.SelectFileButton.Name = "SelectFileButton";
            this.SelectFileButton.Size = new System.Drawing.Size(75, 23);
            this.SelectFileButton.TabIndex = 7;
            this.SelectFileButton.Text = "Выбор...";
            this.SelectFileButton.UseVisualStyleBackColor = true;
            this.SelectFileButton.Click += new System.EventHandler(this.SelectFileButton_Click);
            // 
            // SelectFileEdit
            // 
            this.SelectFileEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectFileEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SelectFileEdit.Location = new System.Drawing.Point(106, 78);
            this.SelectFileEdit.Name = "SelectFileEdit";
            this.SelectFileEdit.PlaceholderText = "C:\\TEMP\\ED807.xml";
            this.SelectFileEdit.Size = new System.Drawing.Size(370, 22);
            this.SelectFileEdit.TabIndex = 6;
            this.SelectFileEdit.TextChanged += new System.EventHandler(this.SelectFileEdit_TextChanged);
            // 
            // SelectFileLabel
            // 
            this.SelectFileLabel.AutoSize = true;
            this.SelectFileLabel.Location = new System.Drawing.Point(11, 81);
            this.SelectFileLabel.Name = "SelectFileLabel";
            this.SelectFileLabel.Size = new System.Drawing.Size(78, 15);
            this.SelectFileLabel.TabIndex = 5;
            this.SelectFileLabel.Text = "Справочник:";
            // 
            // OpenMaskEdit
            // 
            this.OpenMaskEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.OpenMaskEdit.Location = new System.Drawing.Point(106, 48);
            this.OpenMaskEdit.Name = "OpenMaskEdit";
            this.OpenMaskEdit.PlaceholderText = "*.xml";
            this.OpenMaskEdit.Size = new System.Drawing.Size(137, 22);
            this.OpenMaskEdit.TabIndex = 4;
            // 
            // OpenMaskLabel
            // 
            this.OpenMaskLabel.AutoSize = true;
            this.OpenMaskLabel.Location = new System.Drawing.Point(11, 51);
            this.OpenMaskLabel.Name = "OpenMaskLabel";
            this.OpenMaskLabel.Size = new System.Drawing.Size(45, 15);
            this.OpenMaskLabel.TabIndex = 3;
            this.OpenMaskLabel.Text = "Маска:";
            // 
            // OpenDirButton
            // 
            this.OpenDirButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenDirButton.Location = new System.Drawing.Point(482, 18);
            this.OpenDirButton.Name = "OpenDirButton";
            this.OpenDirButton.Size = new System.Drawing.Size(75, 23);
            this.OpenDirButton.TabIndex = 2;
            this.OpenDirButton.Text = "Выбор...";
            this.OpenDirButton.UseVisualStyleBackColor = true;
            this.OpenDirButton.Click += new System.EventHandler(this.OpenDirButton_Click);
            // 
            // OpenDirEdit
            // 
            this.OpenDirEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenDirEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.OpenDirEdit.Location = new System.Drawing.Point(106, 19);
            this.OpenDirEdit.Name = "OpenDirEdit";
            this.OpenDirEdit.PlaceholderText = "C:\\TEMP\\IN";
            this.OpenDirEdit.Size = new System.Drawing.Size(370, 22);
            this.OpenDirEdit.TabIndex = 1;
            this.OpenDirEdit.TextChanged += new System.EventHandler(this.OpenDirEdit_TextChanged);
            // 
            // OpenDirLabel
            // 
            this.OpenDirLabel.AutoSize = true;
            this.OpenDirLabel.Location = new System.Drawing.Point(11, 22);
            this.OpenDirLabel.Name = "OpenDirLabel";
            this.OpenDirLabel.Size = new System.Drawing.Size(76, 15);
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
            this.SaveBox.Location = new System.Drawing.Point(9, 129);
            this.SaveBox.Name = "SaveBox";
            this.SaveBox.Size = new System.Drawing.Size(563, 79);
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
            this.SaveFormatChoice.Location = new System.Drawing.Point(344, 47);
            this.SaveFormatChoice.Name = "SaveFormatChoice";
            this.SaveFormatChoice.Size = new System.Drawing.Size(75, 23);
            this.SaveFormatChoice.TabIndex = 6;
            this.SaveFormatChoice.SelectedValueChanged += new System.EventHandler(this.SaveFormatChoice_SelectedValueChanged);
            // 
            // OutFormatLabel
            // 
            this.OutFormatLabel.AutoSize = true;
            this.OutFormatLabel.Location = new System.Drawing.Point(264, 51);
            this.OutFormatLabel.Name = "OutFormatLabel";
            this.OutFormatLabel.Size = new System.Drawing.Size(53, 15);
            this.OutFormatLabel.TabIndex = 5;
            this.OutFormatLabel.Text = "Формат:";
            // 
            // SaveMaskEdit
            // 
            this.SaveMaskEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SaveMaskEdit.Location = new System.Drawing.Point(106, 48);
            this.SaveMaskEdit.Name = "SaveMaskEdit";
            this.SaveMaskEdit.PlaceholderText = "{id}.txt";
            this.SaveMaskEdit.Size = new System.Drawing.Size(137, 22);
            this.SaveMaskEdit.TabIndex = 4;
            // 
            // SaveMaskLabel
            // 
            this.SaveMaskLabel.AutoSize = true;
            this.SaveMaskLabel.Location = new System.Drawing.Point(11, 51);
            this.SaveMaskLabel.Name = "SaveMaskLabel";
            this.SaveMaskLabel.Size = new System.Drawing.Size(45, 15);
            this.SaveMaskLabel.TabIndex = 3;
            this.SaveMaskLabel.Text = "Маска:";
            // 
            // SaveDirButton
            // 
            this.SaveDirButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveDirButton.Location = new System.Drawing.Point(482, 18);
            this.SaveDirButton.Name = "SaveDirButton";
            this.SaveDirButton.Size = new System.Drawing.Size(75, 23);
            this.SaveDirButton.TabIndex = 2;
            this.SaveDirButton.Text = "Выбор...";
            this.SaveDirButton.UseVisualStyleBackColor = true;
            this.SaveDirButton.Click += new System.EventHandler(this.SaveDirButton_Click);
            // 
            // SaveDirEdit
            // 
            this.SaveDirEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveDirEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SaveDirEdit.Location = new System.Drawing.Point(106, 19);
            this.SaveDirEdit.Name = "SaveDirEdit";
            this.SaveDirEdit.PlaceholderText = "C:\\TEMP\\OUT";
            this.SaveDirEdit.Size = new System.Drawing.Size(370, 22);
            this.SaveDirEdit.TabIndex = 1;
            this.SaveDirEdit.TextChanged += new System.EventHandler(this.SaveDirEdit_TextChanged);
            // 
            // SaveDirLabel
            // 
            this.SaveDirLabel.AutoSize = true;
            this.SaveDirLabel.Location = new System.Drawing.Point(11, 22);
            this.SaveDirLabel.Name = "SaveDirLabel";
            this.SaveDirLabel.Size = new System.Drawing.Size(76, 15);
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
            this.BankBox.Location = new System.Drawing.Point(9, 214);
            this.BankBox.Name = "BankBox";
            this.BankBox.Size = new System.Drawing.Size(563, 108);
            this.BankBox.TabIndex = 2;
            this.BankBox.TabStop = false;
            this.BankBox.Text = "Реквизиты нашего Банка и счета у банка-посредника";
            // 
            // CorrSwiftEdit
            // 
            this.CorrSwiftEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CorrSwiftEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CorrSwiftEdit.Location = new System.Drawing.Point(344, 76);
            this.CorrSwiftEdit.Name = "CorrSwiftEdit";
            this.CorrSwiftEdit.PlaceholderText = "CITVRU2P";
            this.CorrSwiftEdit.Size = new System.Drawing.Size(137, 22);
            this.CorrSwiftEdit.TabIndex = 9;
            this.CorrSwiftEdit.TextChanged += new System.EventHandler(this.CorrSwiftEdit_TextChanged);
            // 
            // CorrSwiftLabel
            // 
            this.CorrSwiftLabel.AutoSize = true;
            this.CorrSwiftLabel.Location = new System.Drawing.Point(264, 79);
            this.CorrSwiftLabel.Name = "CorrSwiftLabel";
            this.CorrSwiftLabel.Size = new System.Drawing.Size(69, 15);
            this.CorrSwiftLabel.TabIndex = 8;
            this.CorrSwiftLabel.Text = "Кор. SWIFT:";
            // 
            // BankSwiftEdit
            // 
            this.BankSwiftEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BankSwiftEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BankSwiftEdit.Location = new System.Drawing.Point(106, 76);
            this.BankSwiftEdit.Name = "BankSwiftEdit";
            this.BankSwiftEdit.PlaceholderText = "CITVRU2P";
            this.BankSwiftEdit.Size = new System.Drawing.Size(137, 22);
            this.BankSwiftEdit.TabIndex = 5;
            this.BankSwiftEdit.TextChanged += new System.EventHandler(this.BankSwiftEdit_TextChanged);
            // 
            // BankSwiftLabel
            // 
            this.BankSwiftLabel.AutoSize = true;
            this.BankSwiftLabel.Location = new System.Drawing.Point(11, 79);
            this.BankSwiftLabel.Name = "BankSwiftLabel";
            this.BankSwiftLabel.Size = new System.Drawing.Size(42, 15);
            this.BankSwiftLabel.TabIndex = 4;
            this.BankSwiftLabel.Text = "SWIFT:";
            // 
            // BankKppEdit
            // 
            this.BankKppEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BankKppEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BankKppEdit.Location = new System.Drawing.Point(106, 48);
            this.BankKppEdit.Name = "BankKppEdit";
            this.BankKppEdit.PlaceholderText = "784101001";
            this.BankKppEdit.Size = new System.Drawing.Size(137, 22);
            this.BankKppEdit.TabIndex = 3;
            this.BankKppEdit.TextChanged += new System.EventHandler(this.BankKppEdit_TextChanged);
            // 
            // BankKPPLabel
            // 
            this.BankKPPLabel.AutoSize = true;
            this.BankKPPLabel.Location = new System.Drawing.Point(11, 51);
            this.BankKPPLabel.Name = "BankKPPLabel";
            this.BankKPPLabel.Size = new System.Drawing.Size(35, 15);
            this.BankKPPLabel.TabIndex = 2;
            this.BankKPPLabel.Text = "КПП:";
            // 
            // BankInnEdit
            // 
            this.BankInnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BankInnEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BankInnEdit.Location = new System.Drawing.Point(106, 19);
            this.BankInnEdit.Name = "BankInnEdit";
            this.BankInnEdit.PlaceholderText = "7831001422";
            this.BankInnEdit.Size = new System.Drawing.Size(137, 22);
            this.BankInnEdit.TabIndex = 1;
            this.BankInnEdit.TextChanged += new System.EventHandler(this.BankInnEdit_TextChanged);
            // 
            // BankINNLabel
            // 
            this.BankINNLabel.AutoSize = true;
            this.BankINNLabel.Location = new System.Drawing.Point(11, 22);
            this.BankINNLabel.Name = "BankINNLabel";
            this.BankINNLabel.Size = new System.Drawing.Size(37, 15);
            this.BankINNLabel.TabIndex = 0;
            this.BankINNLabel.Text = "ИНН:";
            // 
            // CorrAccountEdit
            // 
            this.CorrAccountEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CorrAccountEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CorrAccountEdit.Location = new System.Drawing.Point(344, 15);
            this.CorrAccountEdit.Name = "CorrAccountEdit";
            this.CorrAccountEdit.PlaceholderText = "30101810600000000702";
            this.CorrAccountEdit.Size = new System.Drawing.Size(211, 22);
            this.CorrAccountEdit.TabIndex = 7;
            this.CorrAccountEdit.TextChanged += new System.EventHandler(this.CorrAccountEdit_TextChanged);
            // 
            // CorrAccountLabel
            // 
            this.CorrAccountLabel.AutoSize = true;
            this.CorrAccountLabel.Location = new System.Drawing.Point(264, 22);
            this.CorrAccountLabel.Name = "CorrAccountLabel";
            this.CorrAccountLabel.Size = new System.Drawing.Size(61, 15);
            this.CorrAccountLabel.TabIndex = 6;
            this.CorrAccountLabel.Text = "Кор. счет:";
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(408, 516);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 7;
            this.OKButton.Text = "Сохранить";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // AbortButton
            // 
            this.AbortButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AbortButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.AbortButton.Location = new System.Drawing.Point(489, 516);
            this.AbortButton.Name = "AbortButton";
            this.AbortButton.Size = new System.Drawing.Size(75, 23);
            this.AbortButton.TabIndex = 8;
            this.AbortButton.Text = "Отмена";
            this.AbortButton.UseVisualStyleBackColor = true;
            // 
            // TemplatesNameGroup
            // 
            this.TemplatesNameGroup.Controls.Add(this.SwiftNameLimitChoice);
            this.TemplatesNameGroup.Controls.Add(this.TemplatesNameEdit);
            this.TemplatesNameGroup.Controls.Add(this.SwiftNameLimitLabel);
            this.TemplatesNameGroup.Location = new System.Drawing.Point(9, 328);
            this.TemplatesNameGroup.Name = "TemplatesNameGroup";
            this.TemplatesNameGroup.Size = new System.Drawing.Size(563, 88);
            this.TemplatesNameGroup.TabIndex = 3;
            this.TemplatesNameGroup.TabStop = false;
            this.TemplatesNameGroup.Text = "Шаблон Плательщика за 3 лицо с {name} и {acc} клиента";
            // 
            // SwiftNameLimitChoice
            // 
            this.SwiftNameLimitChoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SwiftNameLimitChoice.FormattingEnabled = true;
            this.SwiftNameLimitChoice.Items.AddRange(new object[] {
            "105",
            "160"});
            this.SwiftNameLimitChoice.Location = new System.Drawing.Point(401, 50);
            this.SwiftNameLimitChoice.Name = "SwiftNameLimitChoice";
            this.SwiftNameLimitChoice.Size = new System.Drawing.Size(75, 23);
            this.SwiftNameLimitChoice.Sorted = true;
            this.SwiftNameLimitChoice.TabIndex = 2;
            // 
            // TemplatesNameEdit
            // 
            this.TemplatesNameEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TemplatesNameEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TemplatesNameEdit.Location = new System.Drawing.Point(11, 22);
            this.TemplatesNameEdit.Name = "TemplatesNameEdit";
            this.TemplatesNameEdit.PlaceholderText = "АО \"Сити Инвест Банк\" ИНН 7831001422 ({name} р/с {acc})";
            this.TemplatesNameEdit.Size = new System.Drawing.Size(544, 22);
            this.TemplatesNameEdit.TabIndex = 0;
            // 
            // SwiftNameLimitLabel
            // 
            this.SwiftNameLimitLabel.AutoSize = true;
            this.SwiftNameLimitLabel.Location = new System.Drawing.Point(264, 55);
            this.SwiftNameLimitLabel.Name = "SwiftNameLimitLabel";
            this.SwiftNameLimitLabel.Size = new System.Drawing.Size(134, 15);
            this.SwiftNameLimitLabel.TabIndex = 1;
            this.SwiftNameLimitLabel.Text = "Предел длины в SWIFT:";
            // 
            // TemplatesPurposeGroup
            // 
            this.TemplatesPurposeGroup.Controls.Add(this.SwiftPurposeFieldChoice);
            this.TemplatesPurposeGroup.Controls.Add(this.SwiftPurposeFieldLabel);
            this.TemplatesPurposeGroup.Controls.Add(this.TemplatesPurposeEdit);
            this.TemplatesPurposeGroup.Location = new System.Drawing.Point(9, 422);
            this.TemplatesPurposeGroup.Name = "TemplatesPurposeGroup";
            this.TemplatesPurposeGroup.Size = new System.Drawing.Size(563, 88);
            this.TemplatesPurposeGroup.TabIndex = 4;
            this.TemplatesPurposeGroup.TabStop = false;
            this.TemplatesPurposeGroup.Text = "Шаблон Назначения за 3 лицо с ИНН и КПП Банка, c {name} и {purpose}";
            // 
            // SwiftPurposeFieldChoice
            // 
            this.SwiftPurposeFieldChoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SwiftPurposeFieldChoice.FormattingEnabled = true;
            this.SwiftPurposeFieldChoice.Items.AddRange(new object[] {
            "70",
            "72"});
            this.SwiftPurposeFieldChoice.Location = new System.Drawing.Point(401, 50);
            this.SwiftPurposeFieldChoice.Name = "SwiftPurposeFieldChoice";
            this.SwiftPurposeFieldChoice.Size = new System.Drawing.Size(75, 23);
            this.SwiftPurposeFieldChoice.Sorted = true;
            this.SwiftPurposeFieldChoice.TabIndex = 2;
            // 
            // SwiftPurposeFieldLabel
            // 
            this.SwiftPurposeFieldLabel.AutoSize = true;
            this.SwiftPurposeFieldLabel.Location = new System.Drawing.Point(264, 56);
            this.SwiftPurposeFieldLabel.Name = "SwiftPurposeFieldLabel";
            this.SwiftPurposeFieldLabel.Size = new System.Drawing.Size(83, 15);
            this.SwiftPurposeFieldLabel.TabIndex = 1;
            this.SwiftPurposeFieldLabel.Text = "Поле в SWIFT:";
            // 
            // TemplatesPurposeEdit
            // 
            this.TemplatesPurposeEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TemplatesPurposeEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TemplatesPurposeEdit.Location = new System.Drawing.Point(11, 22);
            this.TemplatesPurposeEdit.Name = "TemplatesPurposeEdit";
            this.TemplatesPurposeEdit.PlaceholderText = "//7831001422//784101001//{name}//{purpose}";
            this.TemplatesPurposeEdit.Size = new System.Drawing.Size(544, 22);
            this.TemplatesPurposeEdit.TabIndex = 0;
            // 
            // ProfileChoice
            // 
            this.ProfileChoice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ProfileChoice.FormattingEnabled = true;
            this.ProfileChoice.Location = new System.Drawing.Point(115, 517);
            this.ProfileChoice.Name = "ProfileChoice";
            this.ProfileChoice.Size = new System.Drawing.Size(137, 23);
            this.ProfileChoice.Sorted = true;
            this.ProfileChoice.TabIndex = 6;
            this.ProfileChoice.SelectedValueChanged += new System.EventHandler(this.ProfileChoice_SelectedValueChanged);
            // 
            // ProfileLabel
            // 
            this.ProfileLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ProfileLabel.AutoSize = true;
            this.ProfileLabel.Location = new System.Drawing.Point(20, 520);
            this.ProfileLabel.Name = "ProfileLabel";
            this.ProfileLabel.Size = new System.Drawing.Size(62, 15);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.AbortButton;
            this.ClientSize = new System.Drawing.Size(584, 549);
            this.Controls.Add(this.ProfileChoice);
            this.Controls.Add(this.ProfileLabel);
            this.Controls.Add(this.TemplatesPurposeGroup);
            this.Controls.Add(this.TemplatesNameGroup);
            this.Controls.Add(this.AbortButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.BankBox);
            this.Controls.Add(this.SaveBox);
            this.Controls.Add(this.OpenBox);
            this.Name = "ConfigForm";
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
        private ComboBox SwiftNameLimitChoice;
        private Label SwiftNameLimitLabel;
        private TextBox TemplatesNameEdit;
        private GroupBox TemplatesPurposeGroup;
        private ComboBox SwiftPurposeFieldChoice;
        private Label SwiftPurposeFieldLabel;
        private TextBox TemplatesPurposeEdit;
        private ComboBox ProfileChoice;
        private Label ProfileLabel;
        private OpenFileDialog SelectFileDialog;
        private Button SelectFileButton;
        private TextBox SelectFileEdit;
        private Label SelectFileLabel;
    }
}