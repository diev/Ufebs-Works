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
            this.SaveMaskText = new System.Windows.Forms.TextBox();
            this.SaveMaskLabel = new System.Windows.Forms.Label();
            this.SaveDirButton = new System.Windows.Forms.Button();
            this.SaveDirText = new System.Windows.Forms.TextBox();
            this.SaveDirLabel = new System.Windows.Forms.Label();
            this.BankBox = new System.Windows.Forms.GroupBox();
            this.BankPurposeText = new System.Windows.Forms.TextBox();
            this.BankPurposeLabel = new System.Windows.Forms.Label();
            this.BankPayerText = new System.Windows.Forms.TextBox();
            this.BankPayerLabel = new System.Windows.Forms.Label();
            this.BankKPPText = new System.Windows.Forms.TextBox();
            this.BankKPPLabel = new System.Windows.Forms.Label();
            this.BankINNText = new System.Windows.Forms.TextBox();
            this.BankINNLabel = new System.Windows.Forms.Label();
            this.BankAccountText = new System.Windows.Forms.TextBox();
            this.BankAccountLabel = new System.Windows.Forms.Label();
            this.AcceptConfigButton = new System.Windows.Forms.Button();
            this.CancelConfigButton = new System.Windows.Forms.Button();
            this.ResetConfigButton = new System.Windows.Forms.Button();
            this.BankPayerLimitText = new System.Windows.Forms.TextBox();
            this.BankPayerLimitLabel = new System.Windows.Forms.Label();
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
            this.BankBox.Controls.Add(this.BankPayerLimitText);
            this.BankBox.Controls.Add(this.BankPayerLimitLabel);
            this.BankBox.Controls.Add(this.BankPurposeText);
            this.BankBox.Controls.Add(this.BankPurposeLabel);
            this.BankBox.Controls.Add(this.BankPayerText);
            this.BankBox.Controls.Add(this.BankPayerLabel);
            this.BankBox.Controls.Add(this.BankKPPText);
            this.BankBox.Controls.Add(this.BankKPPLabel);
            this.BankBox.Controls.Add(this.BankINNText);
            this.BankBox.Controls.Add(this.BankINNLabel);
            this.BankBox.Controls.Add(this.BankAccountText);
            this.BankBox.Controls.Add(this.BankAccountLabel);
            this.BankBox.Location = new System.Drawing.Point(9, 184);
            this.BankBox.Name = "BankBox";
            this.BankBox.Size = new System.Drawing.Size(563, 171);
            this.BankBox.TabIndex = 2;
            this.BankBox.TabStop = false;
            this.BankBox.Text = "Реквизиты Банка";
            // 
            // BankPurposeText
            // 
            this.BankPurposeText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BankPurposeText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BankPurposeText.Location = new System.Drawing.Point(106, 135);
            this.BankPurposeText.Name = "BankPurposeText";
            this.BankPurposeText.Size = new System.Drawing.Size(449, 22);
            this.BankPurposeText.TabIndex = 10;
            // 
            // BankPurposeLabel
            // 
            this.BankPurposeLabel.AutoSize = true;
            this.BankPurposeLabel.Location = new System.Drawing.Point(11, 138);
            this.BankPurposeLabel.Name = "BankPurposeLabel";
            this.BankPurposeLabel.Size = new System.Drawing.Size(76, 15);
            this.BankPurposeLabel.TabIndex = 9;
            this.BankPurposeLabel.Text = "Назначение:";
            // 
            // BankPayerText
            // 
            this.BankPayerText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BankPayerText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BankPayerText.Location = new System.Drawing.Point(106, 106);
            this.BankPayerText.Name = "BankPayerText";
            this.BankPayerText.Size = new System.Drawing.Size(449, 22);
            this.BankPayerText.TabIndex = 8;
            // 
            // BankPayerLabel
            // 
            this.BankPayerLabel.AutoSize = true;
            this.BankPayerLabel.Location = new System.Drawing.Point(11, 109);
            this.BankPayerLabel.Name = "BankPayerLabel";
            this.BankPayerLabel.Size = new System.Drawing.Size(80, 15);
            this.BankPayerLabel.TabIndex = 7;
            this.BankPayerLabel.Text = "Плательщик:";
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
            // BankAccountText
            // 
            this.BankAccountText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BankAccountText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BankAccountText.Location = new System.Drawing.Point(106, 19);
            this.BankAccountText.Name = "BankAccountText";
            this.BankAccountText.Size = new System.Drawing.Size(274, 22);
            this.BankAccountText.TabIndex = 1;
            // 
            // BankAccountLabel
            // 
            this.BankAccountLabel.AutoSize = true;
            this.BankAccountLabel.Location = new System.Drawing.Point(11, 22);
            this.BankAccountLabel.Name = "BankAccountLabel";
            this.BankAccountLabel.Size = new System.Drawing.Size(36, 15);
            this.BankAccountLabel.TabIndex = 0;
            this.BankAccountLabel.Text = "Счет:";
            // 
            // AcceptConfigButton
            // 
            this.AcceptConfigButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AcceptConfigButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.AcceptConfigButton.Location = new System.Drawing.Point(408, 366);
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
            this.CancelConfigButton.Location = new System.Drawing.Point(489, 366);
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
            this.ResetConfigButton.Location = new System.Drawing.Point(115, 366);
            this.ResetConfigButton.Name = "ResetConfigButton";
            this.ResetConfigButton.Size = new System.Drawing.Size(75, 23);
            this.ResetConfigButton.TabIndex = 5;
            this.ResetConfigButton.Text = "Сброс";
            this.ResetConfigButton.UseVisualStyleBackColor = true;
            this.ResetConfigButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // BankPayerLimitText
            // 
            this.BankPayerLimitText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BankPayerLimitText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BankPayerLimitText.Location = new System.Drawing.Point(482, 77);
            this.BankPayerLimitText.Name = "BankPayerLimitText";
            this.BankPayerLimitText.Size = new System.Drawing.Size(73, 22);
            this.BankPayerLimitText.TabIndex = 12;
            // 
            // BankPayerLimitLabel
            // 
            this.BankPayerLimitLabel.AutoSize = true;
            this.BankPayerLimitLabel.Location = new System.Drawing.Point(264, 80);
            this.BankPayerLimitLabel.Name = "BankPayerLimitLabel";
            this.BankPayerLimitLabel.Size = new System.Drawing.Size(209, 15);
            this.BankPayerLimitLabel.TabIndex = 11;
            this.BankPayerLimitLabel.Text = "Предел наименования (105 или 160):";
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 399);
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
        private TextBox BankPurposeText;
        private Label BankPurposeLabel;
        private TextBox BankPayerText;
        private Label BankPayerLabel;
        private TextBox BankKPPText;
        private Label BankKPPLabel;
        private TextBox BankINNText;
        private Label BankINNLabel;
        private TextBox BankAccountText;
        private Label BankAccountLabel;
        private Button AcceptConfigButton;
        private Button CancelConfigButton;
        private Button ResetConfigButton;
        private TextBox BankPayerLimitText;
        private Label BankPayerLimitLabel;
    }
}