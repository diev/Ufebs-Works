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
            this.SwiftNameLimitChoice = new System.Windows.Forms.ComboBox();
            this.CorrSwiftEdit = new System.Windows.Forms.TextBox();
            this.CorrSWIFTLabel = new System.Windows.Forms.Label();
            this.BankSwiftEdit = new System.Windows.Forms.TextBox();
            this.BankSWIFTLabel = new System.Windows.Forms.Label();
            this.SwiftLimitLabel = new System.Windows.Forms.Label();
            this.PurposeTemplateEdit = new System.Windows.Forms.TextBox();
            this.CorrPurposeLabel = new System.Windows.Forms.Label();
            this.NameTemplateEdit = new System.Windows.Forms.TextBox();
            this.CorrPayerLabel = new System.Windows.Forms.Label();
            this.BankKppEdit = new System.Windows.Forms.TextBox();
            this.BankKPPLabel = new System.Windows.Forms.Label();
            this.BankInnEdit = new System.Windows.Forms.TextBox();
            this.BankINNLabel = new System.Windows.Forms.Label();
            this.CorrAccountEdit = new System.Windows.Forms.TextBox();
            this.CorrAccountLabel = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.AbortButton = new System.Windows.Forms.Button();
            this.ResetConfigButton = new System.Windows.Forms.Button();
            this.OpenBox.SuspendLayout();
            this.SaveBox.SuspendLayout();
            this.BankBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // OpenBox
            // 
            this.OpenBox.Controls.Add(this.OpenMaskEdit);
            this.OpenBox.Controls.Add(this.OpenMaskLabel);
            this.OpenBox.Controls.Add(this.OpenDirButton);
            this.OpenBox.Controls.Add(this.OpenDirEdit);
            this.OpenBox.Controls.Add(this.OpenDirLabel);
            this.OpenBox.Location = new System.Drawing.Point(9, 8);
            this.OpenBox.Name = "OpenBox";
            this.OpenBox.Size = new System.Drawing.Size(563, 82);
            this.OpenBox.TabIndex = 0;
            this.OpenBox.TabStop = false;
            this.OpenBox.Text = "Исходные файлы";
            // 
            // OpenMaskEdit
            // 
            this.OpenMaskEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.OpenMaskEdit.Location = new System.Drawing.Point(106, 48);
            this.OpenMaskEdit.Name = "OpenMaskEdit";
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
            this.OpenDirEdit.Size = new System.Drawing.Size(370, 22);
            this.OpenDirEdit.TabIndex = 1;
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
            this.SaveBox.Location = new System.Drawing.Point(9, 96);
            this.SaveBox.Name = "SaveBox";
            this.SaveBox.Size = new System.Drawing.Size(563, 82);
            this.SaveBox.TabIndex = 1;
            this.SaveBox.TabStop = false;
            this.SaveBox.Text = "Конечные файлы";
            // 
            // SaveFormatChoice
            // 
            this.SaveFormatChoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SaveFormatChoice.FormattingEnabled = true;
            this.SaveFormatChoice.Items.AddRange(new object[] {
            "УФЭБС",
            "SWIFT"});
            this.SaveFormatChoice.Location = new System.Drawing.Point(355, 48);
            this.SaveFormatChoice.Name = "SaveFormatChoice";
            this.SaveFormatChoice.Size = new System.Drawing.Size(121, 23);
            this.SaveFormatChoice.TabIndex = 6;
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
            this.SaveDirEdit.Size = new System.Drawing.Size(370, 22);
            this.SaveDirEdit.TabIndex = 1;
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
            this.BankBox.Controls.Add(this.SwiftNameLimitChoice);
            this.BankBox.Controls.Add(this.CorrSwiftEdit);
            this.BankBox.Controls.Add(this.CorrSWIFTLabel);
            this.BankBox.Controls.Add(this.BankSwiftEdit);
            this.BankBox.Controls.Add(this.BankSWIFTLabel);
            this.BankBox.Controls.Add(this.SwiftLimitLabel);
            this.BankBox.Controls.Add(this.PurposeTemplateEdit);
            this.BankBox.Controls.Add(this.CorrPurposeLabel);
            this.BankBox.Controls.Add(this.NameTemplateEdit);
            this.BankBox.Controls.Add(this.CorrPayerLabel);
            this.BankBox.Controls.Add(this.BankKppEdit);
            this.BankBox.Controls.Add(this.BankKPPLabel);
            this.BankBox.Controls.Add(this.BankInnEdit);
            this.BankBox.Controls.Add(this.BankINNLabel);
            this.BankBox.Controls.Add(this.CorrAccountEdit);
            this.BankBox.Controls.Add(this.CorrAccountLabel);
            this.BankBox.Location = new System.Drawing.Point(9, 184);
            this.BankBox.Name = "BankBox";
            this.BankBox.Size = new System.Drawing.Size(563, 198);
            this.BankBox.TabIndex = 2;
            this.BankBox.TabStop = false;
            this.BankBox.Text = "Реквизиты Банка и {шаблоны}";
            // 
            // SwiftNameLimitChoice
            // 
            this.SwiftNameLimitChoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SwiftNameLimitChoice.FormattingEnabled = true;
            this.SwiftNameLimitChoice.Items.AddRange(new object[] {
            "105",
            "160"});
            this.SwiftNameLimitChoice.Location = new System.Drawing.Point(480, 76);
            this.SwiftNameLimitChoice.Name = "SwiftNameLimitChoice";
            this.SwiftNameLimitChoice.Size = new System.Drawing.Size(75, 23);
            this.SwiftNameLimitChoice.TabIndex = 17;
            // 
            // CorrSwiftEdit
            // 
            this.CorrSwiftEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CorrSwiftEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CorrSwiftEdit.Location = new System.Drawing.Point(394, 163);
            this.CorrSwiftEdit.Name = "CorrSwiftEdit";
            this.CorrSwiftEdit.Size = new System.Drawing.Size(161, 22);
            this.CorrSwiftEdit.TabIndex = 16;
            // 
            // CorrSWIFTLabel
            // 
            this.CorrSWIFTLabel.AutoSize = true;
            this.CorrSWIFTLabel.Location = new System.Drawing.Point(299, 166);
            this.CorrSWIFTLabel.Name = "CorrSWIFTLabel";
            this.CorrSWIFTLabel.Size = new System.Drawing.Size(69, 15);
            this.CorrSWIFTLabel.TabIndex = 15;
            this.CorrSWIFTLabel.Text = "Кор. SWIFT:";
            // 
            // BankSwiftEdit
            // 
            this.BankSwiftEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BankSwiftEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BankSwiftEdit.Location = new System.Drawing.Point(106, 163);
            this.BankSwiftEdit.Name = "BankSwiftEdit";
            this.BankSwiftEdit.Size = new System.Drawing.Size(161, 22);
            this.BankSwiftEdit.TabIndex = 14;
            // 
            // BankSWIFTLabel
            // 
            this.BankSWIFTLabel.AutoSize = true;
            this.BankSWIFTLabel.Location = new System.Drawing.Point(11, 166);
            this.BankSWIFTLabel.Name = "BankSWIFTLabel";
            this.BankSWIFTLabel.Size = new System.Drawing.Size(42, 15);
            this.BankSWIFTLabel.TabIndex = 13;
            this.BankSWIFTLabel.Text = "SWIFT:";
            // 
            // SwiftLimitLabel
            // 
            this.SwiftLimitLabel.AutoSize = true;
            this.SwiftLimitLabel.Location = new System.Drawing.Point(299, 80);
            this.SwiftLimitLabel.Name = "SwiftLimitLabel";
            this.SwiftLimitLabel.Size = new System.Drawing.Size(135, 15);
            this.SwiftLimitLabel.TabIndex = 11;
            this.SwiftLimitLabel.Text = "Предел наименования:";
            // 
            // PurposeTemplateEdit
            // 
            this.PurposeTemplateEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PurposeTemplateEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PurposeTemplateEdit.Location = new System.Drawing.Point(106, 135);
            this.PurposeTemplateEdit.Name = "PurposeTemplateEdit";
            this.PurposeTemplateEdit.Size = new System.Drawing.Size(449, 22);
            this.PurposeTemplateEdit.TabIndex = 10;
            // 
            // CorrPurposeLabel
            // 
            this.CorrPurposeLabel.AutoSize = true;
            this.CorrPurposeLabel.Location = new System.Drawing.Point(11, 138);
            this.CorrPurposeLabel.Name = "CorrPurposeLabel";
            this.CorrPurposeLabel.Size = new System.Drawing.Size(84, 15);
            this.CorrPurposeLabel.TabIndex = 9;
            this.CorrPurposeLabel.Text = "{Назначение}:";
            // 
            // NameTemplateEdit
            // 
            this.NameTemplateEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NameTemplateEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NameTemplateEdit.Location = new System.Drawing.Point(106, 106);
            this.NameTemplateEdit.Name = "NameTemplateEdit";
            this.NameTemplateEdit.Size = new System.Drawing.Size(449, 22);
            this.NameTemplateEdit.TabIndex = 8;
            // 
            // CorrPayerLabel
            // 
            this.CorrPayerLabel.AutoSize = true;
            this.CorrPayerLabel.Location = new System.Drawing.Point(11, 109);
            this.CorrPayerLabel.Name = "CorrPayerLabel";
            this.CorrPayerLabel.Size = new System.Drawing.Size(88, 15);
            this.CorrPayerLabel.TabIndex = 7;
            this.CorrPayerLabel.Text = "{Плательщик}:";
            // 
            // BankKppEdit
            // 
            this.BankKppEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BankKppEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BankKppEdit.Location = new System.Drawing.Point(106, 77);
            this.BankKppEdit.Name = "BankKppEdit";
            this.BankKppEdit.Size = new System.Drawing.Size(137, 22);
            this.BankKppEdit.TabIndex = 6;
            // 
            // BankKPPLabel
            // 
            this.BankKPPLabel.AutoSize = true;
            this.BankKPPLabel.Location = new System.Drawing.Point(11, 80);
            this.BankKPPLabel.Name = "BankKPPLabel";
            this.BankKPPLabel.Size = new System.Drawing.Size(35, 15);
            this.BankKPPLabel.TabIndex = 5;
            this.BankKPPLabel.Text = "КПП:";
            // 
            // BankInnEdit
            // 
            this.BankInnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BankInnEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BankInnEdit.Location = new System.Drawing.Point(106, 48);
            this.BankInnEdit.Name = "BankInnEdit";
            this.BankInnEdit.Size = new System.Drawing.Size(161, 22);
            this.BankInnEdit.TabIndex = 4;
            // 
            // BankINNLabel
            // 
            this.BankINNLabel.AutoSize = true;
            this.BankINNLabel.Location = new System.Drawing.Point(11, 51);
            this.BankINNLabel.Name = "BankINNLabel";
            this.BankINNLabel.Size = new System.Drawing.Size(37, 15);
            this.BankINNLabel.TabIndex = 3;
            this.BankINNLabel.Text = "ИНН:";
            // 
            // CorrAccountEdit
            // 
            this.CorrAccountEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CorrAccountEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CorrAccountEdit.Location = new System.Drawing.Point(106, 19);
            this.CorrAccountEdit.Name = "CorrAccountEdit";
            this.CorrAccountEdit.Size = new System.Drawing.Size(274, 22);
            this.CorrAccountEdit.TabIndex = 1;
            // 
            // CorrAccountLabel
            // 
            this.CorrAccountLabel.AutoSize = true;
            this.CorrAccountLabel.Location = new System.Drawing.Point(11, 22);
            this.CorrAccountLabel.Name = "CorrAccountLabel";
            this.CorrAccountLabel.Size = new System.Drawing.Size(61, 15);
            this.CorrAccountLabel.TabIndex = 0;
            this.CorrAccountLabel.Text = "Кор. счет:";
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(408, 390);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 3;
            this.OKButton.Text = "Сохранить";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // AbortButton
            // 
            this.AbortButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AbortButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.AbortButton.Location = new System.Drawing.Point(489, 390);
            this.AbortButton.Name = "AbortButton";
            this.AbortButton.Size = new System.Drawing.Size(75, 23);
            this.AbortButton.TabIndex = 4;
            this.AbortButton.Text = "Отмена";
            this.AbortButton.UseVisualStyleBackColor = true;
            // 
            // ResetConfigButton
            // 
            this.ResetConfigButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ResetConfigButton.Location = new System.Drawing.Point(115, 390);
            this.ResetConfigButton.Name = "ResetConfigButton";
            this.ResetConfigButton.Size = new System.Drawing.Size(75, 23);
            this.ResetConfigButton.TabIndex = 5;
            this.ResetConfigButton.Text = "Сброс";
            this.ResetConfigButton.UseVisualStyleBackColor = true;
            this.ResetConfigButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 423);
            this.Controls.Add(this.ResetConfigButton);
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
            this.ResumeLayout(false);

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
        private TextBox PurposeTemplateEdit;
        private Label CorrPurposeLabel;
        private TextBox NameTemplateEdit;
        private Label CorrPayerLabel;
        private TextBox BankKppEdit;
        private Label BankKPPLabel;
        private TextBox BankInnEdit;
        private Label BankINNLabel;
        private TextBox CorrAccountEdit;
        private Label CorrAccountLabel;
        private Button OKButton;
        private Button AbortButton;
        private Button ResetConfigButton;
        private Label SwiftLimitLabel;
        private TextBox CorrSwiftEdit;
        private Label CorrSWIFTLabel;
        private TextBox BankSwiftEdit;
        private Label BankSWIFTLabel;
        private ComboBox SaveFormatChoice;
        private Label OutFormatLabel;
        private ComboBox SwiftNameLimitChoice;
    }
}