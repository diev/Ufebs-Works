namespace Corr_Lib
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.NameLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PurposeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.TaxLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.правкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReloadNameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReloadPurposeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.видToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FontMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.XmlPage = new System.Windows.Forms.TabPage();
            this.XmlTextBox = new System.Windows.Forms.TextBox();
            this.SwiftPage = new System.Windows.Forms.TabPage();
            this.SwiftTextBox = new System.Windows.Forms.TextBox();
            this.OutPage = new System.Windows.Forms.TabPage();
            this.OutTextBox = new System.Windows.Forms.TextBox();
            this.NextButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PurposeTextBox = new System.Windows.Forms.TextBox();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveAsFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.FontDialog = new System.Windows.Forms.FontDialog();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.XmlPage.SuspendLayout();
            this.SwiftPage.SuspendLayout();
            this.OutPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.NameLabel,
            this.PurposeLabel,
            this.TaxLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 426);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 24);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(97, 19);
            this.StatusLabel.Text = "Выберите файл";
            // 
            // NameLabel
            // 
            this.NameLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(81, 19);
            this.NameLabel.Text = "Плательщик";
            // 
            // PurposeLabel
            // 
            this.PurposeLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.PurposeLabel.Name = "PurposeLabel";
            this.PurposeLabel.Size = new System.Drawing.Size(77, 19);
            this.PurposeLabel.Text = "Назначение";
            // 
            // TaxLabel
            // 
            this.TaxLabel.Name = "TaxLabel";
            this.TaxLabel.Size = new System.Drawing.Size(49, 19);
            this.TaxLabel.Text = "Платеж";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.правкаToolStripMenuItem,
            this.видToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenMenuItem,
            this.toolStripMenuItem2,
            this.SaveMenuItem,
            this.SaveAsMenuItem,
            this.toolStripSeparator1,
            this.ExitMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(48, 20);
            this.toolStripMenuItem1.Text = "&Файл";
            // 
            // OpenMenuItem
            // 
            this.OpenMenuItem.Name = "OpenMenuItem";
            this.OpenMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OpenMenuItem.Size = new System.Drawing.Size(173, 22);
            this.OpenMenuItem.Text = "Открыть...";
            this.OpenMenuItem.Click += new System.EventHandler(this.OpenMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(170, 6);
            // 
            // SaveMenuItem
            // 
            this.SaveMenuItem.Name = "SaveMenuItem";
            this.SaveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveMenuItem.Size = new System.Drawing.Size(173, 22);
            this.SaveMenuItem.Text = "Сохранить";
            this.SaveMenuItem.Click += new System.EventHandler(this.SaveMenuItem_Click);
            // 
            // SaveAsMenuItem
            // 
            this.SaveAsMenuItem.Name = "SaveAsMenuItem";
            this.SaveAsMenuItem.Size = new System.Drawing.Size(173, 22);
            this.SaveAsMenuItem.Text = "Сохранить как...";
            this.SaveAsMenuItem.Click += new System.EventHandler(this.SaveAsMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(173, 22);
            this.ExitMenuItem.Text = "Выход";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // правкаToolStripMenuItem
            // 
            this.правкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ReloadNameMenuItem,
            this.ReloadPurposeMenuItem});
            this.правкаToolStripMenuItem.Enabled = false;
            this.правкаToolStripMenuItem.Name = "правкаToolStripMenuItem";
            this.правкаToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.правкаToolStripMenuItem.Text = "&Правка";
            // 
            // ReloadNameMenuItem
            // 
            this.ReloadNameMenuItem.Name = "ReloadNameMenuItem";
            this.ReloadNameMenuItem.Size = new System.Drawing.Size(207, 22);
            this.ReloadNameMenuItem.Text = "Перечитать Получателя";
            // 
            // ReloadPurposeMenuItem
            // 
            this.ReloadPurposeMenuItem.Name = "ReloadPurposeMenuItem";
            this.ReloadPurposeMenuItem.Size = new System.Drawing.Size(207, 22);
            this.ReloadPurposeMenuItem.Text = "Перечитать Назначение";
            // 
            // видToolStripMenuItem
            // 
            this.видToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FontMenuItem});
            this.видToolStripMenuItem.Name = "видToolStripMenuItem";
            this.видToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.видToolStripMenuItem.Text = "&Вид";
            // 
            // FontMenuItem
            // 
            this.FontMenuItem.Name = "FontMenuItem";
            this.FontMenuItem.Size = new System.Drawing.Size(122, 22);
            this.FontMenuItem.Text = "Шрифт...";
            this.FontMenuItem.Click += new System.EventHandler(this.FontMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.NextButton);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.PurposeTextBox);
            this.splitContainer1.Panel2.Controls.Add(this.NameTextBox);
            this.splitContainer1.Size = new System.Drawing.Size(800, 402);
            this.splitContainer1.SplitterDistance = 530;
            this.splitContainer1.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.XmlPage);
            this.tabControl1.Controls.Add(this.SwiftPage);
            this.tabControl1.Controls.Add(this.OutPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(530, 402);
            this.tabControl1.TabIndex = 0;
            // 
            // XmlPage
            // 
            this.XmlPage.Controls.Add(this.XmlTextBox);
            this.XmlPage.Location = new System.Drawing.Point(4, 24);
            this.XmlPage.Name = "XmlPage";
            this.XmlPage.Padding = new System.Windows.Forms.Padding(3);
            this.XmlPage.Size = new System.Drawing.Size(522, 374);
            this.XmlPage.TabIndex = 0;
            this.XmlPage.Text = "XML";
            this.XmlPage.UseVisualStyleBackColor = true;
            // 
            // XmlTextBox
            // 
            this.XmlTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.XmlTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.XmlTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.XmlTextBox.Location = new System.Drawing.Point(3, 3);
            this.XmlTextBox.Multiline = true;
            this.XmlTextBox.Name = "XmlTextBox";
            this.XmlTextBox.ReadOnly = true;
            this.XmlTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.XmlTextBox.Size = new System.Drawing.Size(516, 368);
            this.XmlTextBox.TabIndex = 2;
            this.XmlTextBox.Text = "Исходный файл XML не указан.";
            this.XmlTextBox.WordWrap = false;
            // 
            // SwiftPage
            // 
            this.SwiftPage.Controls.Add(this.SwiftTextBox);
            this.SwiftPage.Location = new System.Drawing.Point(4, 24);
            this.SwiftPage.Name = "SwiftPage";
            this.SwiftPage.Padding = new System.Windows.Forms.Padding(3);
            this.SwiftPage.Size = new System.Drawing.Size(522, 374);
            this.SwiftPage.TabIndex = 1;
            this.SwiftPage.Text = "SWIFT";
            this.SwiftPage.UseVisualStyleBackColor = true;
            // 
            // SwiftTextBox
            // 
            this.SwiftTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.SwiftTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SwiftTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SwiftTextBox.Location = new System.Drawing.Point(3, 3);
            this.SwiftTextBox.Multiline = true;
            this.SwiftTextBox.Name = "SwiftTextBox";
            this.SwiftTextBox.ReadOnly = true;
            this.SwiftTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.SwiftTextBox.Size = new System.Drawing.Size(516, 368);
            this.SwiftTextBox.TabIndex = 1;
            this.SwiftTextBox.Text = "Документ не имеет тип ED503.";
            this.SwiftTextBox.WordWrap = false;
            // 
            // OutPage
            // 
            this.OutPage.Controls.Add(this.OutTextBox);
            this.OutPage.Location = new System.Drawing.Point(4, 24);
            this.OutPage.Name = "OutPage";
            this.OutPage.Padding = new System.Windows.Forms.Padding(3);
            this.OutPage.Size = new System.Drawing.Size(522, 374);
            this.OutPage.TabIndex = 2;
            this.OutPage.Text = "К отправке";
            this.OutPage.UseVisualStyleBackColor = true;
            // 
            // OutTextBox
            // 
            this.OutTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.OutTextBox.Location = new System.Drawing.Point(3, 3);
            this.OutTextBox.Multiline = true;
            this.OutTextBox.Name = "OutTextBox";
            this.OutTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.OutTextBox.Size = new System.Drawing.Size(516, 368);
            this.OutTextBox.TabIndex = 2;
            this.OutTextBox.Text = "Нечего отправлять.";
            this.OutTextBox.WordWrap = false;
            this.OutTextBox.TextChanged += new System.EventHandler(this.OutTextBox_TextChanged);
            // 
            // NextButton
            // 
            this.NextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NextButton.Location = new System.Drawing.Point(143, 367);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(75, 23);
            this.NextButton.TabIndex = 4;
            this.NextButton.Text = "Дальше >>";
            this.NextButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Назначение платежа:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Плательщик:";
            // 
            // PurposeTextBox
            // 
            this.PurposeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PurposeTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PurposeTextBox.Location = new System.Drawing.Point(6, 211);
            this.PurposeTextBox.Multiline = true;
            this.PurposeTextBox.Name = "PurposeTextBox";
            this.PurposeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.PurposeTextBox.Size = new System.Drawing.Size(254, 144);
            this.PurposeTextBox.TabIndex = 1;
            this.PurposeTextBox.TextChanged += new System.EventHandler(this.PurposeTextBox_TextChanged);
            // 
            // NameTextBox
            // 
            this.NameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NameTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NameTextBox.Location = new System.Drawing.Point(6, 27);
            this.NameTextBox.Multiline = true;
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.NameTextBox.Size = new System.Drawing.Size(254, 154);
            this.NameTextBox.TabIndex = 0;
            this.NameTextBox.TextChanged += new System.EventHandler(this.NameTextBox_TextChanged);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.DefaultExt = "xml";
            this.OpenFileDialog.Filter = "XML|*.xml|TXT|*.txt|Все файлы|*.*";
            this.OpenFileDialog.InitialDirectory = "G:\\BANK\\TEST\\OUT";
            this.OpenFileDialog.ReadOnlyChecked = true;
            this.OpenFileDialog.Title = "Открыть файл";
            // 
            // SaveAsFileDialog
            // 
            this.SaveAsFileDialog.CreatePrompt = true;
            this.SaveAsFileDialog.DefaultExt = "txt";
            this.SaveAsFileDialog.Filter = "XML|*.xml|TXT|*.txt|Все файлы|*.*";
            this.SaveAsFileDialog.FilterIndex = 2;
            this.SaveAsFileDialog.Title = "Сохранить файл";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Corr-SWIFT";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.XmlPage.ResumeLayout(false);
            this.XmlPage.PerformLayout();
            this.SwiftPage.ResumeLayout(false);
            this.SwiftPage.PerformLayout();
            this.OutPage.ResumeLayout(false);
            this.OutPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StatusStrip statusStrip1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem OpenMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem SaveAsMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem ExitMenuItem;
        private ToolStripMenuItem правкаToolStripMenuItem;
        private SplitContainer splitContainer1;
        private OpenFileDialog OpenFileDialog;
        private SaveFileDialog SaveAsFileDialog;
        private ToolStripMenuItem видToolStripMenuItem;
        private TabControl tabControl1;
        private TabPage XmlPage;
        private TextBox XmlTextBox;
        private TabPage SwiftPage;
        private TextBox SwiftTextBox;
        private TabPage OutPage;
        private TextBox OutTextBox;
        private ToolStripStatusLabel StatusLabel;
        private TextBox PurposeTextBox;
        private TextBox NameTextBox;
        private ToolStripStatusLabel NameLabel;
        private ToolStripStatusLabel PurposeLabel;
        private ToolStripMenuItem ReloadNameMenuItem;
        private ToolStripMenuItem ReloadPurposeMenuItem;
        private ToolStripMenuItem FontMenuItem;
        private FontDialog FontDialog;
        private Label label2;
        private Label label1;
        private ToolStripStatusLabel TaxLabel;
        private ToolStripMenuItem SaveMenuItem;
        private Button NextButton;
    }
}