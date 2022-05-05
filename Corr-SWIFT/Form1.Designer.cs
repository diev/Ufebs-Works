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
            this.Swift50Label = new System.Windows.Forms.ToolStripStatusLabel();
            this.Swift72Label = new System.Windows.Forms.ToolStripStatusLabel();
            this.TaxLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.DoneLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReloadNameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReloadPurposeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FontMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.FilesPage = new System.Windows.Forms.TabPage();
            this.FilesListBox = new System.Windows.Forms.ListBox();
            this.XmlPage = new System.Windows.Forms.TabPage();
            this.XmlTextBox = new System.Windows.Forms.TextBox();
            this.SwiftPage = new System.Windows.Forms.TabPage();
            this.SwiftTextBox = new System.Windows.Forms.TextBox();
            this.OutPage = new System.Windows.Forms.TabPage();
            this.OutTextBox = new System.Windows.Forms.TextBox();
            this.ForwardButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.PurposeEditLabel = new System.Windows.Forms.Label();
            this.NameEditLabel = new System.Windows.Forms.Label();
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
            this.FilesPage.SuspendLayout();
            this.XmlPage.SuspendLayout();
            this.SwiftPage.SuspendLayout();
            this.OutPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Swift50Label,
            this.Swift72Label,
            this.TaxLabel,
            this.DoneLabel,
            this.ProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 426);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 24);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Swift50Label
            // 
            this.Swift50Label.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.Swift50Label.Name = "Swift50Label";
            this.Swift50Label.Size = new System.Drawing.Size(97, 19);
            this.Swift50Label.Text = "Выберите файл";
            // 
            // Swift72Label
            // 
            this.Swift72Label.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.Swift72Label.Name = "Swift72Label";
            this.Swift72Label.Size = new System.Drawing.Size(43, 19);
            this.Swift72Label.Text = "SWIFT";
            // 
            // TaxLabel
            // 
            this.TaxLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.TaxLabel.Name = "TaxLabel";
            this.TaxLabel.Size = new System.Drawing.Size(53, 19);
            this.TaxLabel.Text = "Платеж";
            // 
            // DoneLabel
            // 
            this.DoneLabel.Name = "DoneLabel";
            this.DoneLabel.Size = new System.Drawing.Size(66, 19);
            this.DoneLabel.Text = "Сделано: 0";
            // 
            // ProgressBar
            // 
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(100, 18);
            this.ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.EditMenuItem,
            this.ViewMenuItem,
            this.HelpMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenMenuItem,
            this.toolStripMenuItem2,
            this.SaveMenuItem,
            this.SaveAsMenuItem,
            this.toolStripSeparator1,
            this.ExitMenuItem});
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(48, 20);
            this.FileMenuItem.Text = "&Файл";
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
            // EditMenuItem
            // 
            this.EditMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ReloadNameMenuItem,
            this.ReloadPurposeMenuItem});
            this.EditMenuItem.Enabled = false;
            this.EditMenuItem.Name = "EditMenuItem";
            this.EditMenuItem.Size = new System.Drawing.Size(59, 20);
            this.EditMenuItem.Text = "&Правка";
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
            // ViewMenuItem
            // 
            this.ViewMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FontMenuItem});
            this.ViewMenuItem.Name = "ViewMenuItem";
            this.ViewMenuItem.Size = new System.Drawing.Size(39, 20);
            this.ViewMenuItem.Text = "&Вид";
            // 
            // FontMenuItem
            // 
            this.FontMenuItem.Name = "FontMenuItem";
            this.FontMenuItem.Size = new System.Drawing.Size(122, 22);
            this.FontMenuItem.Text = "Шрифт...";
            this.FontMenuItem.Click += new System.EventHandler(this.FontMenuItem_Click);
            // 
            // HelpMenuItem
            // 
            this.HelpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutMenuItem});
            this.HelpMenuItem.Name = "HelpMenuItem";
            this.HelpMenuItem.Size = new System.Drawing.Size(65, 20);
            this.HelpMenuItem.Text = "Справка";
            // 
            // AboutMenuItem
            // 
            this.AboutMenuItem.Name = "AboutMenuItem";
            this.AboutMenuItem.Size = new System.Drawing.Size(149, 22);
            this.AboutMenuItem.Text = "О программе";
            this.AboutMenuItem.Click += new System.EventHandler(this.AboutMenuItem_Click);
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
            this.splitContainer1.Panel2.Controls.Add(this.ForwardButton);
            this.splitContainer1.Panel2.Controls.Add(this.NextButton);
            this.splitContainer1.Panel2.Controls.Add(this.PurposeEditLabel);
            this.splitContainer1.Panel2.Controls.Add(this.NameEditLabel);
            this.splitContainer1.Panel2.Controls.Add(this.PurposeTextBox);
            this.splitContainer1.Panel2.Controls.Add(this.NameTextBox);
            this.splitContainer1.Size = new System.Drawing.Size(800, 402);
            this.splitContainer1.SplitterDistance = 530;
            this.splitContainer1.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.FilesPage);
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
            // FilesPage
            // 
            this.FilesPage.Controls.Add(this.FilesListBox);
            this.FilesPage.Location = new System.Drawing.Point(4, 24);
            this.FilesPage.Name = "FilesPage";
            this.FilesPage.Padding = new System.Windows.Forms.Padding(3);
            this.FilesPage.Size = new System.Drawing.Size(522, 374);
            this.FilesPage.TabIndex = 3;
            this.FilesPage.Text = "Файлы";
            this.FilesPage.UseVisualStyleBackColor = true;
            // 
            // FilesListBox
            // 
            this.FilesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilesListBox.FormattingEnabled = true;
            this.FilesListBox.ItemHeight = 15;
            this.FilesListBox.Location = new System.Drawing.Point(3, 3);
            this.FilesListBox.Name = "FilesListBox";
            this.FilesListBox.Size = new System.Drawing.Size(516, 368);
            this.FilesListBox.TabIndex = 0;
            this.FilesListBox.SelectedIndexChanged += new System.EventHandler(this.FilesListBox_SelectedIndexChanged);
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
            this.XmlTextBox.Text = "Исходные файлы XML не найдены.";
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
            // ForwardButton
            // 
            this.ForwardButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ForwardButton.Enabled = false;
            this.ForwardButton.Location = new System.Drawing.Point(220, 367);
            this.ForwardButton.Name = "ForwardButton";
            this.ForwardButton.Size = new System.Drawing.Size(34, 23);
            this.ForwardButton.TabIndex = 6;
            this.ForwardButton.Text = ">>";
            this.ForwardButton.UseVisualStyleBackColor = true;
            this.ForwardButton.Click += new System.EventHandler(this.ForwardButton_Click);
            // 
            // NextButton
            // 
            this.NextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NextButton.Enabled = false;
            this.NextButton.Location = new System.Drawing.Point(143, 367);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(75, 23);
            this.NextButton.TabIndex = 4;
            this.NextButton.Text = "Дальше";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // PurposeEditLabel
            // 
            this.PurposeEditLabel.AutoSize = true;
            this.PurposeEditLabel.Location = new System.Drawing.Point(6, 193);
            this.PurposeEditLabel.Name = "PurposeEditLabel";
            this.PurposeEditLabel.Size = new System.Drawing.Size(125, 15);
            this.PurposeEditLabel.TabIndex = 3;
            this.PurposeEditLabel.Text = "Назначение платежа:";
            // 
            // NameEditLabel
            // 
            this.NameEditLabel.AutoSize = true;
            this.NameEditLabel.Location = new System.Drawing.Point(6, 9);
            this.NameEditLabel.Name = "NameEditLabel";
            this.NameEditLabel.Size = new System.Drawing.Size(80, 15);
            this.NameEditLabel.TabIndex = 2;
            this.NameEditLabel.Text = "Плательщик:";
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
            this.OpenFileDialog.Multiselect = true;
            this.OpenFileDialog.ReadOnlyChecked = true;
            this.OpenFileDialog.SupportMultiDottedExtensions = true;
            this.OpenFileDialog.Title = "Открыть файл(ы)";
            this.OpenFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFileDialog_FileOk);
            // 
            // SaveAsFileDialog
            // 
            this.SaveAsFileDialog.CreatePrompt = true;
            this.SaveAsFileDialog.DefaultExt = "txt";
            this.SaveAsFileDialog.Filter = "TXT|*.txt|XML|*.xml|Все файлы|*.*";
            this.SaveAsFileDialog.Title = "Сохранить файл";
            this.SaveAsFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveAsFileDialog_FileOk);
            // 
            // FontDialog
            // 
            this.FontDialog.Apply += new System.EventHandler(this.FontDialog_Apply);
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
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Corr-SWIFT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
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
            this.FilesPage.ResumeLayout(false);
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
        private ToolStripMenuItem FileMenuItem;
        private ToolStripMenuItem OpenMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem SaveAsMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem ExitMenuItem;
        private ToolStripMenuItem EditMenuItem;
        private SplitContainer splitContainer1;
        private OpenFileDialog OpenFileDialog;
        private SaveFileDialog SaveAsFileDialog;
        private ToolStripMenuItem ViewMenuItem;
        private TabControl tabControl1;
        private TabPage XmlPage;
        private TextBox XmlTextBox;
        private TabPage SwiftPage;
        private TextBox SwiftTextBox;
        private TabPage OutPage;
        private TextBox OutTextBox;
        private ToolStripStatusLabel Swift50Label;
        private TextBox PurposeTextBox;
        private TextBox NameTextBox;
        private ToolStripMenuItem ReloadNameMenuItem;
        private ToolStripMenuItem ReloadPurposeMenuItem;
        private ToolStripMenuItem FontMenuItem;
        private FontDialog FontDialog;
        private Label PurposeEditLabel;
        private Label NameEditLabel;
        private ToolStripStatusLabel TaxLabel;
        private ToolStripMenuItem SaveMenuItem;
        private Button NextButton;
        private Button ForwardButton;
        private ToolStripMenuItem HelpMenuItem;
        private ToolStripMenuItem AboutMenuItem;
        private TabPage FilesPage;
        private ListBox FilesListBox;
        private ToolStripProgressBar ProgressBar;
        private ToolStripStatusLabel Swift72Label;
        private ToolStripStatusLabel DoneLabel;
    }
}