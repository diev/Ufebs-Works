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
            this.PurposeEdit = new System.Windows.Forms.TextBox();
            this.PayeeEdit = new System.Windows.Forms.TextBox();
            this.PayerEdit = new System.Windows.Forms.TextBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.PayerLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PayerStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.PayeeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PayeeStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.PurposeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PurposeStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.PayerLen = new System.Windows.Forms.ToolStripStatusLabel();
            this.PayeeLen = new System.Windows.Forms.ToolStripStatusLabel();
            this.PurposeLen = new System.Windows.Forms.ToolStripStatusLabel();
            this.AbortButton = new System.Windows.Forms.Button();
            this.SamplePanel = new System.Windows.Forms.TableLayoutPanel();
            this.SamplePurpose = new System.Windows.Forms.TextBox();
            this.SamplePayee = new System.Windows.Forms.TextBox();
            this.SamplePayer = new System.Windows.Forms.TextBox();
            this.statusStrip1.SuspendLayout();
            this.SamplePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // PurposeEdit
            // 
            this.PurposeEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.PurposeEdit.Location = new System.Drawing.Point(0, 64);
            this.PurposeEdit.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.PurposeEdit.Name = "PurposeEdit";
            this.PurposeEdit.PlaceholderText = "Назначение платежа";
            this.PurposeEdit.Size = new System.Drawing.Size(1456, 32);
            this.PurposeEdit.TabIndex = 2;
            this.PurposeEdit.TextChanged += new System.EventHandler(this.PurposeEdit_TextChanged);
            // 
            // PayeeEdit
            // 
            this.PayeeEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.PayeeEdit.Location = new System.Drawing.Point(0, 32);
            this.PayeeEdit.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.PayeeEdit.Name = "PayeeEdit";
            this.PayeeEdit.PlaceholderText = "Получатель";
            this.PayeeEdit.Size = new System.Drawing.Size(1456, 32);
            this.PayeeEdit.TabIndex = 1;
            this.PayeeEdit.Visible = false;
            this.PayeeEdit.TextChanged += new System.EventHandler(this.PayeeEdit_TextChanged);
            // 
            // PayerEdit
            // 
            this.PayerEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.PayerEdit.Location = new System.Drawing.Point(0, 0);
            this.PayerEdit.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.PayerEdit.Name = "PayerEdit";
            this.PayerEdit.PlaceholderText = "Плательщик";
            this.PayerEdit.Size = new System.Drawing.Size(1456, 32);
            this.PayerEdit.TabIndex = 0;
            this.PayerEdit.TextChanged += new System.EventHandler(this.PayerEdit_TextChanged);
            // 
            // OKButton
            // 
            this.OKButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Enabled = false;
            this.OKButton.Location = new System.Drawing.Point(505, 123);
            this.OKButton.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(139, 40);
            this.OKButton.TabIndex = 3;
            this.OKButton.Text = "ОК";
            this.OKButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(656, 123);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(139, 40);
            this.CancelButton.TabIndex = 4;
            this.CancelButton.Text = "Отмена";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PayerLabel,
            this.PayerStatus,
            this.PayeeLabel,
            this.PayeeStatus,
            this.PurposeLabel,
            this.PurposeStatus,
            this.Status,
            this.PayerLen,
            this.PayeeLen,
            this.PurposeLen});
            this.statusStrip1.Location = new System.Drawing.Point(0, 186);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 26, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1456, 24);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // PayerLabel
            // 
            this.PayerLabel.Name = "PayerLabel";
            this.PayerLabel.Size = new System.Drawing.Size(80, 19);
            this.PayerLabel.Text = "Плательщик:";
            // 
            // PayerStatus
            // 
            this.PayerStatus.Name = "PayerStatus";
            this.PayerStatus.Size = new System.Drawing.Size(13, 19);
            this.PayerStatus.Text = "0";
            // 
            // PayeeLabel
            // 
            this.PayeeLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.PayeeLabel.Name = "PayeeLabel";
            this.PayeeLabel.Size = new System.Drawing.Size(80, 19);
            this.PayeeLabel.Text = "Получатель:";
            this.PayeeLabel.Visible = false;
            // 
            // PayeeStatus
            // 
            this.PayeeStatus.Name = "PayeeStatus";
            this.PayeeStatus.Size = new System.Drawing.Size(13, 19);
            this.PayeeStatus.Text = "0";
            this.PayeeStatus.Visible = false;
            // 
            // PurposeLabel
            // 
            this.PurposeLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.PurposeLabel.Name = "PurposeLabel";
            this.PurposeLabel.Size = new System.Drawing.Size(80, 19);
            this.PurposeLabel.Text = "Назначение:";
            // 
            // PurposeStatus
            // 
            this.PurposeStatus.Name = "PurposeStatus";
            this.PurposeStatus.Size = new System.Drawing.Size(13, 19);
            this.PurposeStatus.Text = "0";
            // 
            // Status
            // 
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(1062, 19);
            this.Status.Spring = true;
            this.Status.Text = "Format: УФЭБС";
            this.Status.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PayerLen
            // 
            this.PayerLen.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.PayerLen.Name = "PayerLen";
            this.PayerLen.Size = new System.Drawing.Size(17, 19);
            this.PayerLen.Text = "0";
            // 
            // PayeeLen
            // 
            this.PayeeLen.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.PayeeLen.Name = "PayeeLen";
            this.PayeeLen.Size = new System.Drawing.Size(17, 19);
            this.PayeeLen.Text = "0";
            this.PayeeLen.Visible = false;
            // 
            // PurposeLen
            // 
            this.PurposeLen.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.PurposeLen.Margin = new System.Windows.Forms.Padding(0, 3, 5, 2);
            this.PurposeLen.Name = "PurposeLen";
            this.PurposeLen.Size = new System.Drawing.Size(17, 19);
            this.PurposeLen.Text = "0";
            // 
            // AbortButton
            // 
            this.AbortButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.AbortButton.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.AbortButton.Location = new System.Drawing.Point(806, 123);
            this.AbortButton.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.AbortButton.Name = "AbortButton";
            this.AbortButton.Size = new System.Drawing.Size(139, 40);
            this.AbortButton.TabIndex = 5;
            this.AbortButton.Text = "Прервать";
            this.AbortButton.UseVisualStyleBackColor = true;
            // 
            // SamplePanel
            // 
            this.SamplePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SamplePanel.ColumnCount = 3;
            this.SamplePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.SamplePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.SamplePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.SamplePanel.Controls.Add(this.SamplePurpose, 0, 0);
            this.SamplePanel.Controls.Add(this.SamplePayee, 0, 0);
            this.SamplePanel.Controls.Add(this.SamplePayer, 0, 0);
            this.SamplePanel.Location = new System.Drawing.Point(149, 125);
            this.SamplePanel.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.SamplePanel.Name = "SamplePanel";
            this.SamplePanel.RowCount = 1;
            this.SamplePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.SamplePanel.Size = new System.Drawing.Size(1189, 64);
            this.SamplePanel.TabIndex = 10;
            // 
            // SamplePurpose
            // 
            this.SamplePurpose.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SamplePurpose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SamplePurpose.Location = new System.Drawing.Point(802, 5);
            this.SamplePurpose.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.SamplePurpose.Multiline = true;
            this.SamplePurpose.Name = "SamplePurpose";
            this.SamplePurpose.PlaceholderText = "SWIFT";
            this.SamplePurpose.ReadOnly = true;
            this.SamplePurpose.Size = new System.Drawing.Size(381, 54);
            this.SamplePurpose.TabIndex = 12;
            this.SamplePurpose.TabStop = false;
            this.SamplePurpose.Visible = false;
            this.SamplePurpose.WordWrap = false;
            // 
            // SamplePayee
            // 
            this.SamplePayee.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SamplePayee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SamplePayee.Location = new System.Drawing.Point(398, 5);
            this.SamplePayee.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.SamplePayee.Multiline = true;
            this.SamplePayee.Name = "SamplePayee";
            this.SamplePayee.PlaceholderText = "SWIFT";
            this.SamplePayee.ReadOnly = true;
            this.SamplePayee.Size = new System.Drawing.Size(392, 54);
            this.SamplePayee.TabIndex = 11;
            this.SamplePayee.TabStop = false;
            this.SamplePayee.Visible = false;
            this.SamplePayee.WordWrap = false;
            // 
            // SamplePayer
            // 
            this.SamplePayer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SamplePayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SamplePayer.Location = new System.Drawing.Point(6, 5);
            this.SamplePayer.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.SamplePayer.Multiline = true;
            this.SamplePayer.Name = "SamplePayer";
            this.SamplePayer.PlaceholderText = "SWIFT";
            this.SamplePayer.ReadOnly = true;
            this.SamplePayer.Size = new System.Drawing.Size(380, 54);
            this.SamplePayer.TabIndex = 10;
            this.SamplePayer.TabStop = false;
            this.SamplePayer.Visible = false;
            this.SamplePayer.WordWrap = false;
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1456, 210);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.AbortButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.SamplePanel);
            this.Controls.Add(this.PurposeEdit);
            this.Controls.Add(this.PayeeEdit);
            this.Controls.Add(this.PayerEdit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1101, 249);
            this.Name = "EditForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EditForm";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.SamplePanel.ResumeLayout(false);
            this.SamplePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal TextBox PurposeEdit;
        internal TextBox PayeeEdit;
        internal TextBox PayerEdit;
        private Button OKButton;
        private Button CancelButton;
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