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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.SamplePurpose = new System.Windows.Forms.TextBox();
            this.SamplePayee = new System.Windows.Forms.TextBox();
            this.SamplePayer = new System.Windows.Forms.TextBox();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PurposeEdit
            // 
            this.PurposeEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.PurposeEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PurposeEdit.Location = new System.Drawing.Point(0, 44);
            this.PurposeEdit.Name = "PurposeEdit";
            this.PurposeEdit.PlaceholderText = "Назначение платежа";
            this.PurposeEdit.Size = new System.Drawing.Size(784, 22);
            this.PurposeEdit.TabIndex = 2;
            this.PurposeEdit.TextChanged += new System.EventHandler(this.PurposeEdit_TextChanged);
            // 
            // PayeeEdit
            // 
            this.PayeeEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.PayeeEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PayeeEdit.Location = new System.Drawing.Point(0, 22);
            this.PayeeEdit.Name = "PayeeEdit";
            this.PayeeEdit.PlaceholderText = "Получатель";
            this.PayeeEdit.Size = new System.Drawing.Size(784, 22);
            this.PayeeEdit.TabIndex = 1;
            this.PayeeEdit.Visible = false;
            this.PayeeEdit.TextChanged += new System.EventHandler(this.PayeeEdit_TextChanged);
            // 
            // PayerEdit
            // 
            this.PayerEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.PayerEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PayerEdit.Location = new System.Drawing.Point(0, 0);
            this.PayerEdit.Name = "PayerEdit";
            this.PayerEdit.PlaceholderText = "Плательщик";
            this.PayerEdit.Size = new System.Drawing.Size(784, 22);
            this.PayerEdit.TabIndex = 0;
            this.PayerEdit.TextChanged += new System.EventHandler(this.PayerEdit_TextChanged);
            // 
            // OKButton
            // 
            this.OKButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Enabled = false;
            this.OKButton.Location = new System.Drawing.Point(272, 71);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 3;
            this.OKButton.Text = "ОК";
            this.OKButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(353, 71);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 97);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 24);
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
            this.Status.Size = new System.Drawing.Size(544, 19);
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
            this.AbortButton.Location = new System.Drawing.Point(434, 71);
            this.AbortButton.Name = "AbortButton";
            this.AbortButton.Size = new System.Drawing.Size(75, 23);
            this.AbortButton.TabIndex = 5;
            this.AbortButton.Text = "Прервать";
            this.AbortButton.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.Controls.Add(this.SamplePurpose, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.SamplePayee, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.SamplePayer, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(80, 72);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(640, 37);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // SamplePurpose
            // 
            this.SamplePurpose.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SamplePurpose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SamplePurpose.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SamplePurpose.Location = new System.Drawing.Point(431, 3);
            this.SamplePurpose.Multiline = true;
            this.SamplePurpose.Name = "SamplePurpose";
            this.SamplePurpose.PlaceholderText = "SWIFT";
            this.SamplePurpose.ReadOnly = true;
            this.SamplePurpose.Size = new System.Drawing.Size(206, 31);
            this.SamplePurpose.TabIndex = 12;
            this.SamplePurpose.TabStop = false;
            this.SamplePurpose.Visible = false;
            this.SamplePurpose.WordWrap = false;
            // 
            // SamplePayee
            // 
            this.SamplePayee.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SamplePayee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SamplePayee.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SamplePayee.Location = new System.Drawing.Point(214, 3);
            this.SamplePayee.Multiline = true;
            this.SamplePayee.Name = "SamplePayee";
            this.SamplePayee.PlaceholderText = "SWIFT";
            this.SamplePayee.ReadOnly = true;
            this.SamplePayee.Size = new System.Drawing.Size(211, 31);
            this.SamplePayee.TabIndex = 11;
            this.SamplePayee.TabStop = false;
            this.SamplePayee.Visible = false;
            this.SamplePayee.WordWrap = false;
            // 
            // SamplePayer
            // 
            this.SamplePayer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SamplePayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SamplePayer.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SamplePayer.Location = new System.Drawing.Point(3, 3);
            this.SamplePayer.Multiline = true;
            this.SamplePayer.Name = "SamplePayer";
            this.SamplePayer.PlaceholderText = "SWIFT";
            this.SamplePayer.ReadOnly = true;
            this.SamplePayer.Size = new System.Drawing.Size(205, 31);
            this.SamplePayer.TabIndex = 10;
            this.SamplePayer.TabStop = false;
            this.SamplePayer.Visible = false;
            this.SamplePayer.WordWrap = false;
            // 
            // EditForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 121);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.AbortButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.PurposeEdit);
            this.Controls.Add(this.PayeeEdit);
            this.Controls.Add(this.PayerEdit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 160);
            this.Name = "EditForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EditForm";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
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
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox SamplePurpose;
        private TextBox SamplePayee;
        private TextBox SamplePayer;
        private ToolStripStatusLabel PayerLen;
        private ToolStripStatusLabel PayeeLen;
        private ToolStripStatusLabel PurposeLen;
    }
}