namespace SwiftTranslator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Limit210 = new System.Windows.Forms.RadioButton();
            this.Limit160 = new System.Windows.Forms.RadioButton();
            this.LimitNone = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.RusSourceTextLength = new System.Windows.Forms.Label();
            this.SwiftDestText35 = new System.Windows.Forms.TextBox();
            this.RusSourceText = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SwiftSourceText35 = new System.Windows.Forms.TextBox();
            this.RusDestText = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(584, 361);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Limit210);
            this.tabPage1.Controls.Add(this.Limit160);
            this.tabPage1.Controls.Add(this.LimitNone);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.RusSourceTextLength);
            this.tabPage1.Controls.Add(this.SwiftDestText35);
            this.tabPage1.Controls.Add(this.RusSourceText);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(576, 333);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Rus > Swift";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Limit210
            // 
            this.Limit210.AutoSize = true;
            this.Limit210.Location = new System.Drawing.Point(146, 127);
            this.Limit210.Name = "Limit210";
            this.Limit210.Size = new System.Drawing.Size(43, 19);
            this.Limit210.TabIndex = 12;
            this.Limit210.Text = "210";
            this.Limit210.UseVisualStyleBackColor = true;
            this.Limit210.CheckedChanged += new System.EventHandler(this.Limit210_CheckedChanged);
            // 
            // Limit160
            // 
            this.Limit160.AutoSize = true;
            this.Limit160.Location = new System.Drawing.Point(146, 102);
            this.Limit160.Name = "Limit160";
            this.Limit160.Size = new System.Drawing.Size(43, 19);
            this.Limit160.TabIndex = 11;
            this.Limit160.Text = "160";
            this.Limit160.UseVisualStyleBackColor = true;
            this.Limit160.CheckedChanged += new System.EventHandler(this.Limit160_CheckedChanged);
            // 
            // LimitNone
            // 
            this.LimitNone.AutoSize = true;
            this.LimitNone.Checked = true;
            this.LimitNone.Location = new System.Drawing.Point(146, 77);
            this.LimitNone.Name = "LimitNone";
            this.LimitNone.Size = new System.Drawing.Size(45, 19);
            this.LimitNone.TabIndex = 10;
            this.LimitNone.TabStop = true;
            this.LimitNone.Text = "Нет";
            this.LimitNone.UseVisualStyleBackColor = true;
            this.LimitNone.CheckedChanged += new System.EventHandler(this.LimitNone_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Строки SWIFT по 35:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Ограничение длины:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Введите текст:";
            // 
            // RusSourceTextLength
            // 
            this.RusSourceTextLength.AutoSize = true;
            this.RusSourceTextLength.Location = new System.Drawing.Point(232, 81);
            this.RusSourceTextLength.Name = "RusSourceTextLength";
            this.RusSourceTextLength.Size = new System.Drawing.Size(125, 15);
            this.RusSourceTextLength.TabIndex = 5;
            this.RusSourceTextLength.Text = "Фактическая длина: 0";
            this.RusSourceTextLength.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // SwiftDestText35
            // 
            this.SwiftDestText35.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SwiftDestText35.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SwiftDestText35.Location = new System.Drawing.Point(6, 190);
            this.SwiftDestText35.Multiline = true;
            this.SwiftDestText35.Name = "SwiftDestText35";
            this.SwiftDestText35.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.SwiftDestText35.Size = new System.Drawing.Size(562, 137);
            this.SwiftDestText35.TabIndex = 4;
            // 
            // RusSourceText
            // 
            this.RusSourceText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RusSourceText.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RusSourceText.Location = new System.Drawing.Point(6, 35);
            this.RusSourceText.Name = "RusSourceText";
            this.RusSourceText.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.RusSourceText.Size = new System.Drawing.Size(564, 21);
            this.RusSourceText.TabIndex = 0;
            this.RusSourceText.TextChanged += new System.EventHandler(this.RusSourceText_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.SwiftSourceText35);
            this.tabPage2.Controls.Add(this.RusDestText);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(426, 283);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Swift > Rus";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 15);
            this.label5.TabIndex = 14;
            this.label5.Text = "Строки SWIFT по 35:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 225);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "Русский текст:";
            // 
            // SwiftSourceText35
            // 
            this.SwiftSourceText35.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SwiftSourceText35.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SwiftSourceText35.Location = new System.Drawing.Point(6, 35);
            this.SwiftSourceText35.Multiline = true;
            this.SwiftSourceText35.Name = "SwiftSourceText35";
            this.SwiftSourceText35.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.SwiftSourceText35.Size = new System.Drawing.Size(412, 174);
            this.SwiftSourceText35.TabIndex = 11;
            this.SwiftSourceText35.TextChanged += new System.EventHandler(this.SwiftSourceText35_TextChanged);
            // 
            // RusDestText
            // 
            this.RusDestText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RusDestText.Location = new System.Drawing.Point(8, 243);
            this.RusDestText.Name = "RusDestText";
            this.RusDestText.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.RusDestText.Size = new System.Drawing.Size(410, 23);
            this.RusDestText.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(450, 350);
            this.Name = "Form1";
            this.Text = "SwiftTranslator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TextBox SwiftDestText35;
        private TextBox RusSourceText;
        private TabPage tabPage2;
        private Label RusSourceTextLength;
        private Label label4;
        private Label label3;
        private Label label1;
        private Label label5;
        private Label label6;
        private TextBox SwiftSourceText35;
        private TextBox RusDestText;
        private RadioButton Limit210;
        private RadioButton Limit160;
        private RadioButton LimitNone;
    }
}