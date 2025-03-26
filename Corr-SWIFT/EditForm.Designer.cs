namespace CorrSWIFT
{
    partial class EditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditForm));
            PurposeEdit = new TextBox();
            PayeeEdit = new TextBox();
            PayerEdit = new TextBox();
            OKButton = new Button();
            BreakButton = new Button();
            statusStrip1 = new StatusStrip();
            PayerLabel = new ToolStripStatusLabel();
            PayerStatus = new ToolStripStatusLabel();
            PayeeLabel = new ToolStripStatusLabel();
            PayeeStatus = new ToolStripStatusLabel();
            PurposeLabel = new ToolStripStatusLabel();
            PurposeStatus = new ToolStripStatusLabel();
            Status = new ToolStripStatusLabel();
            PayerLen = new ToolStripStatusLabel();
            PayeeLen = new ToolStripStatusLabel();
            PurposeLen = new ToolStripStatusLabel();
            AbortButton = new Button();
            SamplePanel = new TableLayoutPanel();
            SamplePurpose = new TextBox();
            SamplePayee = new TextBox();
            SamplePayer = new TextBox();
            statusStrip1.SuspendLayout();
            SamplePanel.SuspendLayout();
            SuspendLayout();
            // 
            // PurposeEdit
            // 
            PurposeEdit.Dock = DockStyle.Top;
            PurposeEdit.Location = new Point(0, 46);
            PurposeEdit.Name = "PurposeEdit";
            PurposeEdit.PlaceholderText = "Назначение платежа";
            PurposeEdit.Size = new Size(784, 23);
            PurposeEdit.TabIndex = 2;
            PurposeEdit.TextChanged += PurposeEdit_TextChanged;
            // 
            // PayeeEdit
            // 
            PayeeEdit.Dock = DockStyle.Top;
            PayeeEdit.Location = new Point(0, 23);
            PayeeEdit.Name = "PayeeEdit";
            PayeeEdit.PlaceholderText = "Получатель";
            PayeeEdit.Size = new Size(784, 23);
            PayeeEdit.TabIndex = 1;
            PayeeEdit.Visible = false;
            PayeeEdit.TextChanged += PayeeEdit_TextChanged;
            // 
            // PayerEdit
            // 
            PayerEdit.Dock = DockStyle.Top;
            PayerEdit.Location = new Point(0, 0);
            PayerEdit.Name = "PayerEdit";
            PayerEdit.PlaceholderText = "Плательщик";
            PayerEdit.Size = new Size(784, 23);
            PayerEdit.TabIndex = 0;
            PayerEdit.TextChanged += PayerEdit_TextChanged;
            // 
            // OKButton
            // 
            OKButton.Anchor = AnchorStyles.Bottom;
            OKButton.DialogResult = DialogResult.OK;
            OKButton.Enabled = false;
            OKButton.Location = new Point(272, 71);
            OKButton.Name = "OKButton";
            OKButton.Size = new Size(75, 23);
            OKButton.TabIndex = 3;
            OKButton.Text = "ОК";
            OKButton.UseVisualStyleBackColor = true;
            // 
            // BreakButton
            // 
            BreakButton.Anchor = AnchorStyles.Bottom;
            BreakButton.DialogResult = DialogResult.Cancel;
            BreakButton.Location = new Point(353, 71);
            BreakButton.Name = "BreakButton";
            BreakButton.Size = new Size(75, 23);
            BreakButton.TabIndex = 4;
            BreakButton.Text = "Отмена";
            BreakButton.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { PayerLabel, PayerStatus, PayeeLabel, PayeeStatus, PurposeLabel, PurposeStatus, Status, PayerLen, PayeeLen, PurposeLen });
            statusStrip1.Location = new Point(0, 97);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(784, 24);
            statusStrip1.TabIndex = 8;
            statusStrip1.Text = "statusStrip1";
            // 
            // PayerLabel
            // 
            PayerLabel.Name = "PayerLabel";
            PayerLabel.Size = new Size(80, 19);
            PayerLabel.Text = "Плательщик:";
            // 
            // PayerStatus
            // 
            PayerStatus.Name = "PayerStatus";
            PayerStatus.Size = new Size(13, 19);
            PayerStatus.Text = "0";
            // 
            // PayeeLabel
            // 
            PayeeLabel.BorderSides = ToolStripStatusLabelBorderSides.Left;
            PayeeLabel.Name = "PayeeLabel";
            PayeeLabel.Size = new Size(80, 19);
            PayeeLabel.Text = "Получатель:";
            PayeeLabel.Visible = false;
            // 
            // PayeeStatus
            // 
            PayeeStatus.Name = "PayeeStatus";
            PayeeStatus.Size = new Size(13, 19);
            PayeeStatus.Text = "0";
            PayeeStatus.Visible = false;
            // 
            // PurposeLabel
            // 
            PurposeLabel.BorderSides = ToolStripStatusLabelBorderSides.Left;
            PurposeLabel.Name = "PurposeLabel";
            PurposeLabel.Size = new Size(80, 19);
            PurposeLabel.Text = "Назначение:";
            // 
            // PurposeStatus
            // 
            PurposeStatus.Name = "PurposeStatus";
            PurposeStatus.Size = new Size(13, 19);
            PurposeStatus.Text = "0";
            // 
            // Status
            // 
            Status.Name = "Status";
            Status.Size = new Size(544, 19);
            Status.Spring = true;
            Status.Text = "Format: УФЭБС";
            Status.TextAlign = ContentAlignment.MiddleRight;
            // 
            // PayerLen
            // 
            PayerLen.BorderSides = ToolStripStatusLabelBorderSides.Left;
            PayerLen.Name = "PayerLen";
            PayerLen.Size = new Size(17, 19);
            PayerLen.Text = "0";
            // 
            // PayeeLen
            // 
            PayeeLen.BorderSides = ToolStripStatusLabelBorderSides.Left;
            PayeeLen.Name = "PayeeLen";
            PayeeLen.Size = new Size(17, 19);
            PayeeLen.Text = "0";
            PayeeLen.Visible = false;
            // 
            // PurposeLen
            // 
            PurposeLen.BorderSides = ToolStripStatusLabelBorderSides.Left;
            PurposeLen.Margin = new Padding(0, 3, 5, 2);
            PurposeLen.Name = "PurposeLen";
            PurposeLen.Size = new Size(17, 19);
            PurposeLen.Text = "0";
            // 
            // AbortButton
            // 
            AbortButton.Anchor = AnchorStyles.Bottom;
            AbortButton.DialogResult = DialogResult.Abort;
            AbortButton.Location = new Point(434, 71);
            AbortButton.Name = "AbortButton";
            AbortButton.Size = new Size(75, 23);
            AbortButton.TabIndex = 5;
            AbortButton.Text = "Прервать";
            AbortButton.UseVisualStyleBackColor = true;
            // 
            // SamplePanel
            // 
            SamplePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SamplePanel.ColumnCount = 3;
            SamplePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            SamplePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            SamplePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            SamplePanel.Controls.Add(SamplePurpose, 0, 0);
            SamplePanel.Controls.Add(SamplePayee, 0, 0);
            SamplePanel.Controls.Add(SamplePayer, 0, 0);
            SamplePanel.Location = new Point(80, 72);
            SamplePanel.Name = "SamplePanel";
            SamplePanel.RowCount = 1;
            SamplePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            SamplePanel.Size = new Size(640, 37);
            SamplePanel.TabIndex = 10;
            // 
            // SamplePurpose
            // 
            SamplePurpose.BorderStyle = BorderStyle.None;
            SamplePurpose.Dock = DockStyle.Fill;
            SamplePurpose.Location = new Point(431, 3);
            SamplePurpose.Multiline = true;
            SamplePurpose.Name = "SamplePurpose";
            SamplePurpose.PlaceholderText = "SWIFT";
            SamplePurpose.ReadOnly = true;
            SamplePurpose.Size = new Size(206, 31);
            SamplePurpose.TabIndex = 12;
            SamplePurpose.TabStop = false;
            SamplePurpose.Visible = false;
            SamplePurpose.WordWrap = false;
            // 
            // SamplePayee
            // 
            SamplePayee.BorderStyle = BorderStyle.None;
            SamplePayee.Dock = DockStyle.Fill;
            SamplePayee.Location = new Point(214, 3);
            SamplePayee.Multiline = true;
            SamplePayee.Name = "SamplePayee";
            SamplePayee.PlaceholderText = "SWIFT";
            SamplePayee.ReadOnly = true;
            SamplePayee.Size = new Size(211, 31);
            SamplePayee.TabIndex = 11;
            SamplePayee.TabStop = false;
            SamplePayee.Visible = false;
            SamplePayee.WordWrap = false;
            // 
            // SamplePayer
            // 
            SamplePayer.BorderStyle = BorderStyle.None;
            SamplePayer.Dock = DockStyle.Fill;
            SamplePayer.Location = new Point(3, 3);
            SamplePayer.Multiline = true;
            SamplePayer.Name = "SamplePayer";
            SamplePayer.PlaceholderText = "SWIFT";
            SamplePayer.ReadOnly = true;
            SamplePayer.Size = new Size(205, 31);
            SamplePayer.TabIndex = 10;
            SamplePayer.TabStop = false;
            SamplePayer.Visible = false;
            SamplePayer.WordWrap = false;
            // 
            // EditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 121);
            Controls.Add(statusStrip1);
            Controls.Add(AbortButton);
            Controls.Add(BreakButton);
            Controls.Add(OKButton);
            Controls.Add(SamplePanel);
            Controls.Add(PurposeEdit);
            Controls.Add(PayeeEdit);
            Controls.Add(PayerEdit);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(600, 160);
            Name = "EditForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "EditForm";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            SamplePanel.ResumeLayout(false);
            SamplePanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        internal TextBox PurposeEdit;
        internal TextBox PayeeEdit;
        internal TextBox PayerEdit;
        private Button OKButton;
        private Button BreakButton;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel PayerLabel;
        private ToolStripStatusLabel PayerStatus;
        private ToolStripStatusLabel PayeeLabel;
        private ToolStripStatusLabel PayeeStatus;
        private ToolStripStatusLabel PurposeLabel;
        private ToolStripStatusLabel PurposeStatus;
        private Button AbortButton;
        private ToolStripStatusLabel Status;
        private TableLayoutPanel SamplePanel;
        private TextBox SamplePurpose;
        private TextBox SamplePayee;
        private TextBox SamplePayer;
        private ToolStripStatusLabel PayerLen;
        private ToolStripStatusLabel PayeeLen;
        private ToolStripStatusLabel PurposeLen;
    }
}