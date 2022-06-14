namespace CorrSWIFT
{
    partial class CorrForm
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
            this.SwiftGroup = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.PurposeSwift = new System.Windows.Forms.TextBox();
            this.NameSwift = new System.Windows.Forms.TextBox();
            this.NameResult = new System.Windows.Forms.TextBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.AbortButton = new System.Windows.Forms.Button();
            this.PurposeResult = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.PurposeLabel = new System.Windows.Forms.Label();
            this.SwiftGroup.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SwiftGroup
            // 
            this.SwiftGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SwiftGroup.Controls.Add(this.tableLayoutPanel1);
            this.SwiftGroup.Location = new System.Drawing.Point(12, 158);
            this.SwiftGroup.Name = "SwiftGroup";
            this.SwiftGroup.Size = new System.Drawing.Size(613, 135);
            this.SwiftGroup.TabIndex = 4;
            this.SwiftGroup.TabStop = false;
            this.SwiftGroup.Text = "SWIFT:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.PurposeSwift, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.NameSwift, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(607, 113);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // PurposeSwift
            // 
            this.PurposeSwift.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PurposeSwift.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PurposeSwift.Location = new System.Drawing.Point(306, 3);
            this.PurposeSwift.Multiline = true;
            this.PurposeSwift.Name = "PurposeSwift";
            this.PurposeSwift.PlaceholderText = "TEXT";
            this.PurposeSwift.ReadOnly = true;
            this.PurposeSwift.Size = new System.Drawing.Size(298, 107);
            this.PurposeSwift.TabIndex = 6;
            this.PurposeSwift.TabStop = false;
            this.PurposeSwift.WordWrap = false;
            // 
            // NameSwift
            // 
            this.NameSwift.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NameSwift.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NameSwift.Location = new System.Drawing.Point(3, 3);
            this.NameSwift.Multiline = true;
            this.NameSwift.Name = "NameSwift";
            this.NameSwift.PlaceholderText = "TEXT";
            this.NameSwift.ReadOnly = true;
            this.NameSwift.Size = new System.Drawing.Size(297, 107);
            this.NameSwift.TabIndex = 5;
            this.NameSwift.TabStop = false;
            this.NameSwift.WordWrap = false;
            // 
            // NameResult
            // 
            this.NameResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NameResult.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NameResult.Location = new System.Drawing.Point(12, 31);
            this.NameResult.MaxLength = 500;
            this.NameResult.Name = "NameResult";
            this.NameResult.PlaceholderText = "Плательщик";
            this.NameResult.Size = new System.Drawing.Size(776, 22);
            this.NameResult.TabIndex = 1;
            this.NameResult.TextChanged += new System.EventHandler(this.NameResult_TextChanged);
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(631, 123);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 7;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            // 
            // AbortButton
            // 
            this.AbortButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AbortButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.AbortButton.Location = new System.Drawing.Point(712, 123);
            this.AbortButton.Name = "AbortButton";
            this.AbortButton.Size = new System.Drawing.Size(75, 23);
            this.AbortButton.TabIndex = 8;
            this.AbortButton.Text = "Cancel";
            this.AbortButton.UseVisualStyleBackColor = true;
            // 
            // PurposeResult
            // 
            this.PurposeResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PurposeResult.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PurposeResult.Location = new System.Drawing.Point(12, 83);
            this.PurposeResult.MaxLength = 500;
            this.PurposeResult.Name = "PurposeResult";
            this.PurposeResult.PlaceholderText = "Назначение";
            this.PurposeResult.Size = new System.Drawing.Size(776, 22);
            this.PurposeResult.TabIndex = 3;
            this.PurposeResult.TextChanged += new System.EventHandler(this.PurposeResult_TextChanged);
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(12, 13);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(38, 15);
            this.NameLabel.TabIndex = 0;
            this.NameLabel.Text = "label1";
            // 
            // PurposeLabel
            // 
            this.PurposeLabel.AutoSize = true;
            this.PurposeLabel.Location = new System.Drawing.Point(12, 65);
            this.PurposeLabel.Name = "PurposeLabel";
            this.PurposeLabel.Size = new System.Drawing.Size(38, 15);
            this.PurposeLabel.TabIndex = 2;
            this.PurposeLabel.Text = "label2";
            // 
            // CorrForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.AbortButton;
            this.ClientSize = new System.Drawing.Size(800, 305);
            this.Controls.Add(this.PurposeLabel);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.PurposeResult);
            this.Controls.Add(this.AbortButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.NameResult);
            this.Controls.Add(this.SwiftGroup);
            this.Name = "CorrForm";
            this.Text = "Сокращение текста";
            this.Load += new System.EventHandler(this.CorrForm_Load);
            this.SwiftGroup.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private GroupBox SwiftGroup;
        private Button OKButton;
        private Button AbortButton;
        public TextBox NameResult;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox PurposeSwift;
        private TextBox NameSwift;
        public TextBox PurposeResult;
        private Label NameLabel;
        private Label PurposeLabel;
    }
}