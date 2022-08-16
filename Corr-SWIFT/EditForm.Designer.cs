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
            this.PurposeEdit = new System.Windows.Forms.TextBox();
            this.PayeeEdit = new System.Windows.Forms.TextBox();
            this.PayerEdit = new System.Windows.Forms.TextBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.PayerStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.PayeeStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.PurposeStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.AbortButton = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PurposeEdit
            // 
            this.PurposeEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.PurposeEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PurposeEdit.Location = new System.Drawing.Point(0, 44);
            this.PurposeEdit.Name = "PurposeEdit";
            this.PurposeEdit.PlaceholderText = "Назначение платежа";
            this.PurposeEdit.Size = new System.Drawing.Size(800, 22);
            this.PurposeEdit.TabIndex = 5;
            this.PurposeEdit.TextChanged += new System.EventHandler(this.PurposeEdit_TextChanged);
            // 
            // PayeeEdit
            // 
            this.PayeeEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.PayeeEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PayeeEdit.Location = new System.Drawing.Point(0, 22);
            this.PayeeEdit.Name = "PayeeEdit";
            this.PayeeEdit.PlaceholderText = "Получатель";
            this.PayeeEdit.Size = new System.Drawing.Size(800, 22);
            this.PayeeEdit.TabIndex = 4;
            this.PayeeEdit.TextChanged += new System.EventHandler(this.PayeeEdit_TextChanged);
            // 
            // PayerEdit
            // 
            this.PayerEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.PayerEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PayerEdit.Location = new System.Drawing.Point(0, 0);
            this.PayerEdit.Name = "PayerEdit";
            this.PayerEdit.PlaceholderText = "Плательщик";
            this.PayerEdit.Size = new System.Drawing.Size(800, 22);
            this.PayerEdit.TabIndex = 3;
            this.PayerEdit.TextChanged += new System.EventHandler(this.PayerEdit_TextChanged);
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Enabled = false;
            this.OKButton.Location = new System.Drawing.Point(551, 72);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 6;
            this.OKButton.Text = "ОК";
            this.OKButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(632, 72);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 7;
            this.CancelButton.Text = "Отмена";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.PayerStatus,
            this.toolStripStatusLabel3,
            this.PayeeStatus,
            this.toolStripStatusLabel5,
            this.PurposeStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 108);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 24);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(80, 19);
            this.toolStripStatusLabel1.Text = "Плательщик:";
            // 
            // PayerStatus
            // 
            this.PayerStatus.Name = "PayerStatus";
            this.PayerStatus.Size = new System.Drawing.Size(13, 19);
            this.PayerStatus.Text = "0";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(80, 19);
            this.toolStripStatusLabel3.Text = "Получатель:";
            // 
            // PayeeStatus
            // 
            this.PayeeStatus.Name = "PayeeStatus";
            this.PayeeStatus.Size = new System.Drawing.Size(13, 19);
            this.PayeeStatus.Text = "0";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(80, 19);
            this.toolStripStatusLabel5.Text = "Назначение:";
            // 
            // PurposeStatus
            // 
            this.PurposeStatus.Name = "PurposeStatus";
            this.PurposeStatus.Size = new System.Drawing.Size(13, 19);
            this.PurposeStatus.Text = "0";
            // 
            // AbortButton
            // 
            this.AbortButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AbortButton.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.AbortButton.Location = new System.Drawing.Point(713, 72);
            this.AbortButton.Name = "AbortButton";
            this.AbortButton.Size = new System.Drawing.Size(75, 23);
            this.AbortButton.TabIndex = 9;
            this.AbortButton.Text = "Прервать";
            this.AbortButton.UseVisualStyleBackColor = true;
            // 
            // EditForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 132);
            this.Controls.Add(this.AbortButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.PurposeEdit);
            this.Controls.Add(this.PayeeEdit);
            this.Controls.Add(this.PayerEdit);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditForm";
            this.Text = "EditForm";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel PayerStatus;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private ToolStripStatusLabel PayeeStatus;
        private ToolStripStatusLabel toolStripStatusLabel5;
        private ToolStripStatusLabel PurposeStatus;
        private Button AbortButton;
    }
}