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
            this.OpenMaskText = new System.Windows.Forms.TextBox();
            this.OpenMaskLabel = new System.Windows.Forms.Label();
            this.OpenDirButton = new System.Windows.Forms.Button();
            this.OpenDirText = new System.Windows.Forms.TextBox();
            this.OpenDirLabel = new System.Windows.Forms.Label();
            this.OpenFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SaveFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SaveBox = new System.Windows.Forms.GroupBox();
            this.OutFormatBox = new System.Windows.Forms.ComboBox();
            this.OutFormatLabel = new System.Windows.Forms.Label();
            this.SaveMaskText = new System.Windows.Forms.TextBox();
            this.SaveMaskLabel = new System.Windows.Forms.Label();
            this.SaveDirButton = new System.Windows.Forms.Button();
            this.SaveDirText = new System.Windows.Forms.TextBox();
            this.SaveDirLabel = new System.Windows.Forms.Label();
            this.BankBox = new System.Windows.Forms.GroupBox();
            this.CorrSWIFTText = new System.Windows.Forms.TextBox();
            this.CorrSWIFTLabel = new System.Windows.Forms.Label();
            this.BankSWIFTText = new System.Windows.Forms.TextBox();
            this.BankSWIFTLabel = new System.Windows.Forms.Label();
            this.CorrPayerLimitText = new System.Windows.Forms.TextBox();
            this.CorrPayerLimitLabel = new System.Windows.Forms.Label();
            this.CorrPurposeText = new System.Windows.Forms.TextBox();
            this.CorrPurposeLabel = new System.Windows.Forms.Label();
            this.CorrPayerText = new System.Windows.Forms.TextBox();
            this.CorrPayerLabel = new System.Windows.Forms.Label();
            this.BankKPPText = new System.Windows.Forms.TextBox();
            this.BankKPPLabel = new System.Windows.Forms.Label();
            this.BankINNText = new System.Windows.Forms.TextBox();
            this.BankINNLabel = new System.Windows.Forms.Label();
            this.CorrAccountText = new System.Windows.Forms.TextBox();
            this.CorrAccountLabel = new System.Windows.Forms.Label();
            this.AcceptConfigButton = new System.Windows.Forms.Button();
            this.CancelConfigButton = new System.Windows.Forms.Button();
            this.ResetConfigButton = new System.Windows.Forms.Button();
            this.OpenBox.SuspendLayout();
            this.SaveBox.SuspendLayout();
            this.BankBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // OpenBox
            // 
            this.OpenBox.Controls.Add(this.OpenMaskText);
            this.OpenBox.Controls.Add(this.OpenMaskLabel);
            this.OpenBox.Controls.Add(this.OpenDirButton);
            this.OpenBox.Controls.Add(this.OpenDirText);
            this.OpenBox.Controls.Add(this.OpenDirLabel);
            this.OpenBox.Location = new System.Drawing.Point(9, 8);
            this.OpenBox.Name = "OpenBox";
            this.OpenBox.Size = new System.Drawing.Size(563, 82);
            this.OpenBox.TabIndex = 0;
            this.OpenBox.TabStop = false;
            this.OpenBox.Text = "Исходные файлы";
            // 
            // OpenMaskText
            // 
            this.OpenMaskText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.OpenMaskText.Location = new System.Drawing.Point(106, 48);
            this.OpenMaskText.Name = "OpenMaskText";
            this.OpenMaskText.Size = new System.Drawing.Size(137, 22);
            this.OpenMaskText.TabIndex = 4;
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
            // OpenDirText
            // 
            this.OpenDirText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenDirText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.OpenDirText.Location = new System.Drawing.Point(106, 19);
            this.OpenDirText.Name = "OpenDirText";
            this.OpenDirText.Size = new System.Drawing.Size(370, 22);
            this.OpenDirText.TabIndex = 1;
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
            this.SaveBox.Controls.Add(this.OutFormatBox);
            this.SaveBox.Controls.Add(this.OutFormatLabel);
            this.SaveBox.Controls.Add(this.SaveMaskText);
            this.SaveBox.Controls.Add(this.SaveMaskLabel);
            this.SaveBox.Controls.Add(this.SaveDirButton);
            this.SaveBox.Controls.Add(this.SaveDirText);
            this.SaveBox.Controls.Add(this.SaveDirLabel);
            this.SaveBox.Location = new System.Drawing.Point(9, 96);
            this.SaveBox.Name = "SaveBox";
            this.SaveBox.Size = new System.Drawing.Size(563, 82);
            this.SaveBox.TabIndex = 1;
            this.SaveBox.TabStop = false;
            this.SaveBox.Text = "Конечные файлы";
            // 
            // OutFormatBox
            // 
            this.OutFormatBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OutFormatBox.FormattingEnabled = true;
            this.OutFormatBox.Items.AddRange(new object[] {
            "УФЭБС",
            "SWIFT"});
            this.OutFormatBox.Location = new System.Drawing.Point(355, 48);
            this.OutFormatBox.Name = "OutFormatBox";
            this.OutFormatBox.Size = new System.Drawing.Size(121, 23);
            this.OutFormatBox.TabIndex = 6;
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
            // SaveMaskText
            // 
            this.SaveMaskText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SaveMaskText.Location = new System.Drawing.Point(106, 48);
            this.SaveMaskText.Name = "SaveMaskText";
            this.SaveMaskText.Size = new System.Drawing.Size(137, 22);
            this.SaveMaskText.TabIndex = 4;
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
            // SaveDirText
            // 
            this.SaveDirText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveDirText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SaveDirText.Location = new System.Drawing.Point(106, 19);
            this.SaveDirText.Name = "SaveDirText";
            this.SaveDirText.Size = new System.Drawing.Size(370, 22);
            this.SaveDirText.TabIndex = 1;
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
            this.BankBox.Controls.Add(this.CorrSWIFTText);
            this.BankBox.Controls.Add(this.CorrSWIFTLabel);
            this.BankBox.Controls.Add(this.BankSWIFTText);
            this.BankBox.Controls.Add(this.BankSWIFTLabel);
            this.BankBox.Controls.Add(this.CorrPayerLimitText);
            this.BankBox.Controls.Add(this.CorrPayerLimitLabel);
            this.BankBox.Controls.Add(this.CorrPurposeText);
            this.BankBox.Controls.Add(this.CorrPurposeLabel);
            this.BankBox.Controls.Add(this.CorrPayerText);
            this.BankBox.Controls.Add(this.CorrPayerLabel);
            this.BankBox.Controls.Add(this.BankKPPText);
            this.BankBox.Controls.Add(this.BankKPPLabel);
            this.BankBox.Controls.Add(this.BankINNText);
            this.BankBox.Controls.Add(this.BankINNLabel);
            this.BankBox.Controls.Add(this.CorrAccountText);
            this.BankBox.Controls.Add(this.CorrAccountLabel);
            this.BankBox.Location = new System.Drawing.Point(9, 184);
            this.BankBox.Name = "BankBox";
            this.BankBox.Size = new System.Drawing.Size(563, 198);
            this.BankBox.TabIndex = 2;
            this.BankBox.TabStop = false;
            this.BankBox.Text = "Реквизиты Банка и {шаблоны}";
            // 
            // CorrSWIFTText
            // 
            this.CorrSWIFTText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CorrSWIFTText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CorrSWIFTText.Location = new System.Drawing.Point(394, 163);
            this.CorrSWIFTText.Name = "CorrSWIFTText";
            this.CorrSWIFTText.Size = new System.Drawing.Size(161, 22);
            this.CorrSWIFTText.TabIndex = 16;
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
            // BankSWIFTText
            // 
            this.BankSWIFTText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BankSWIFTText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BankSWIFTText.Location = new System.Drawing.Point(106, 163);
            this.BankSWIFTText.Name = "BankSWIFTText";
            this.BankSWIFTText.Size = new System.Drawing.Size(161, 22);
            this.BankSWIFTText.TabIndex = 14;
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
            // CorrPayerLimitText
            // 
            this.CorrPayerLimitText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CorrPayerLimitText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CorrPayerLimitText.Location = new System.Drawing.Point(482, 77);
            this.CorrPayerLimitText.Name = "CorrPayerLimitText";
            this.CorrPayerLimitText.Size = new System.Drawing.Size(73, 22);
            this.CorrPayerLimitText.TabIndex = 12;
            // 
            // CorrPayerLimitLabel
            // 
            this.CorrPayerLimitLabel.AutoSize = true;
            this.CorrPayerLimitLabel.Location = new System.Drawing.Point(264, 80);
            this.CorrPayerLimitLabel.Name = "CorrPayerLimitLabel";
            this.CorrPayerLimitLabel.Size = new System.Drawing.Size(209, 15);
            this.CorrPayerLimitLabel.TabIndex = 11;
            this.CorrPayerLimitLabel.Text = "Предел наименования (105 или 160):";
            // 
            // CorrPurposeText
            // 
            this.CorrPurposeText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CorrPurposeText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CorrPurposeText.Location = new System.Drawing.Point(106, 135);
            this.CorrPurposeText.Name = "CorrPurposeText";
            this.CorrPurposeText.Size = new System.Drawing.Size(449, 22);
            this.CorrPurposeText.TabIndex = 10;
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
            // CorrPayerText
            // 
            this.CorrPayerText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CorrPayerText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CorrPayerText.Location = new System.Drawing.Point(106, 106);
            this.CorrPayerText.Name = "CorrPayerText";
            this.CorrPayerText.Size = new System.Drawing.Size(449, 22);
            this.CorrPayerText.TabIndex = 8;
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
            // BankKPPText
            // 
            this.BankKPPText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BankKPPText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BankKPPText.Location = new System.Drawing.Point(106, 77);
            this.BankKPPText.Name = "BankKPPText";
            this.BankKPPText.Size = new System.Drawing.Size(137, 22);
            this.BankKPPText.TabIndex = 6;
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
            // BankINNText
            // 
            this.BankINNText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BankINNText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BankINNText.Location = new System.Drawing.Point(106, 48);
            this.BankINNText.Name = "BankINNText";
            this.BankINNText.Size = new System.Drawing.Size(161, 22);
            this.BankINNText.TabIndex = 4;
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
            // CorrAccountText
            // 
            this.CorrAccountText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CorrAccountText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CorrAccountText.Location = new System.Drawing.Point(106, 19);
            this.CorrAccountText.Name = "CorrAccountText";
            this.CorrAccountText.Size = new System.Drawing.Size(274, 22);
            this.CorrAccountText.TabIndex = 1;
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
            // AcceptConfigButton
            // 
            this.AcceptConfigButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AcceptConfigButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.AcceptConfigButton.Location = new System.Drawing.Point(408, 390);
            this.AcceptConfigButton.Name = "AcceptConfigButton";
            this.AcceptConfigButton.Size = new System.Drawing.Size(75, 23);
            this.AcceptConfigButton.TabIndex = 3;
            this.AcceptConfigButton.Text = "Сохранить";
            this.AcceptConfigButton.UseVisualStyleBackColor = true;
            this.AcceptConfigButton.Click += new System.EventHandler(this.AcceptButton_Click);
            // 
            // CancelConfigButton
            // 
            this.CancelConfigButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelConfigButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelConfigButton.Location = new System.Drawing.Point(489, 390);
            this.CancelConfigButton.Name = "CancelConfigButton";
            this.CancelConfigButton.Size = new System.Drawing.Size(75, 23);
            this.CancelConfigButton.TabIndex = 4;
            this.CancelConfigButton.Text = "Отмена";
            this.CancelConfigButton.UseVisualStyleBackColor = true;
            this.CancelConfigButton.Click += new System.EventHandler(this.CancelButton_Click);
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
            this.Controls.Add(this.CancelConfigButton);
            this.Controls.Add(this.AcceptConfigButton);
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
        private TextBox OpenMaskText;
        private Label OpenMaskLabel;
        private Button OpenDirButton;
        private TextBox OpenDirText;
        private Label OpenDirLabel;
        private FolderBrowserDialog OpenFolderDialog;
        private FolderBrowserDialog SaveFolderDialog;
        private GroupBox SaveBox;
        private TextBox SaveMaskText;
        private Label SaveMaskLabel;
        private Button SaveDirButton;
        private TextBox SaveDirText;
        private Label SaveDirLabel;
        private GroupBox BankBox;
        private TextBox CorrPurposeText;
        private Label CorrPurposeLabel;
        private TextBox CorrPayerText;
        private Label CorrPayerLabel;
        private TextBox BankKPPText;
        private Label BankKPPLabel;
        private TextBox BankINNText;
        private Label BankINNLabel;
        private TextBox CorrAccountText;
        private Label CorrAccountLabel;
        private Button AcceptConfigButton;
        private Button CancelConfigButton;
        private Button ResetConfigButton;
        private TextBox CorrPayerLimitText;
        private Label CorrPayerLimitLabel;
        private TextBox CorrSWIFTText;
        private Label CorrSWIFTLabel;
        private TextBox BankSWIFTText;
        private Label BankSWIFTLabel;
        private ComboBox OutFormatBox;
        private Label OutFormatLabel;
    }
}