namespace CorrSWIFT;

partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.FormatStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProfileStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.OpenStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.DirStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.SaveStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.ConfigMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.FontMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.FilesList = new System.Windows.Forms.ListView();
            this.CntFileColumn = new System.Windows.Forms.ColumnHeader();
            this.FileColumn = new System.Windows.Forms.ColumnHeader();
            this.RootColumn = new System.Windows.Forms.ColumnHeader();
            this.TotalQtyColumn = new System.Windows.Forms.ColumnHeader();
            this.TotalSumColumn = new System.Windows.Forms.ColumnHeader();
            this.PackSavedColumn = new System.Windows.Forms.ColumnHeader();
            this.DocsList = new System.Windows.Forms.ListView();
            this.CntDocColumn = new System.Windows.Forms.ColumnHeader();
            this.EDNoColumn = new System.Windows.Forms.ColumnHeader();
            this.EDColumn = new System.Windows.Forms.ColumnHeader();
            this.DocNoColumn = new System.Windows.Forms.ColumnHeader();
            this.SumColumn = new System.Windows.Forms.ColumnHeader();
            this.PayerColumn = new System.Windows.Forms.ColumnHeader();
            this.PayeeColumn = new System.Windows.Forms.ColumnHeader();
            this.PurposeColumn = new System.Windows.Forms.ColumnHeader();
            this.SavedColumn = new System.Windows.Forms.ColumnHeader();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.FontDialog = new System.Windows.Forms.FontDialog();
            this.StatusBar.SuspendLayout();
            this.MainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusBar
            // 
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status,
            this.FormatStatus,
            this.ProfileStatus,
            this.OpenStatus,
            this.DirStatus,
            this.SaveStatus});
            this.StatusBar.Location = new System.Drawing.Point(0, 426);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(930, 24);
            this.StatusBar.TabIndex = 0;
            this.StatusBar.Text = "statusStrip1";
            // 
            // Status
            // 
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(749, 19);
            this.Status.Spring = true;
            this.Status.Text = "Загрузка...";
            this.Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormatStatus
            // 
            this.FormatStatus.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.FormatStatus.Name = "FormatStatus";
            this.FormatStatus.Size = new System.Drawing.Size(49, 19);
            this.FormatStatus.Text = "Format";
            // 
            // ProfileStatus
            // 
            this.ProfileStatus.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.ProfileStatus.Name = "ProfileStatus";
            this.ProfileStatus.Size = new System.Drawing.Size(45, 19);
            this.ProfileStatus.Text = "Profile";
            // 
            // OpenStatus
            // 
            this.OpenStatus.Name = "OpenStatus";
            this.OpenStatus.Size = new System.Drawing.Size(17, 19);
            this.OpenStatus.Text = "In";
            // 
            // DirStatus
            // 
            this.DirStatus.Name = "DirStatus";
            this.DirStatus.Size = new System.Drawing.Size(23, 19);
            this.DirStatus.Text = ">>";
            // 
            // SaveStatus
            // 
            this.SaveStatus.Margin = new System.Windows.Forms.Padding(0, 3, 5, 2);
            this.SaveStatus.Name = "SaveStatus";
            this.SaveStatus.Size = new System.Drawing.Size(27, 19);
            this.SaveStatus.Text = "Out";
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.ViewMenu,
            this.HelpMenu});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(930, 24);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "menuStrip1";
            // 
            // FileMenu
            // 
            this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFileMenuItem,
            this.toolStripSeparator,
            this.ConfigMenuItem,
            this.toolStripSeparator1,
            this.ExitMenuItem});
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(48, 20);
            this.FileMenu.Text = "&Файл";
            // 
            // OpenFileMenuItem
            // 
            this.OpenFileMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("OpenFileMenuItem.Image")));
            this.OpenFileMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenFileMenuItem.Name = "OpenFileMenuItem";
            this.OpenFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OpenFileMenuItem.Size = new System.Drawing.Size(173, 22);
            this.OpenFileMenuItem.Text = "&Открыть...";
            this.OpenFileMenuItem.Click += new System.EventHandler(this.OpenMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(170, 6);
            // 
            // ConfigMenuItem
            // 
            this.ConfigMenuItem.Name = "ConfigMenuItem";
            this.ConfigMenuItem.Size = new System.Drawing.Size(173, 22);
            this.ConfigMenuItem.Text = "П&араметры...";
            this.ConfigMenuItem.Click += new System.EventHandler(this.ConfigMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.ExitMenuItem.Size = new System.Drawing.Size(173, 22);
            this.ExitMenuItem.Text = "Вы&ход";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // ViewMenu
            // 
            this.ViewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FontMenuItem});
            this.ViewMenu.Name = "ViewMenu";
            this.ViewMenu.Size = new System.Drawing.Size(39, 20);
            this.ViewMenu.Text = "&Вид";
            // 
            // FontMenuItem
            // 
            this.FontMenuItem.Name = "FontMenuItem";
            this.FontMenuItem.Size = new System.Drawing.Size(122, 22);
            this.FontMenuItem.Text = "&Шрифт...";
            this.FontMenuItem.Click += new System.EventHandler(this.FontMenuItem_Click);
            // 
            // HelpMenu
            // 
            this.HelpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutMenuItem});
            this.HelpMenu.Name = "HelpMenu";
            this.HelpMenu.Size = new System.Drawing.Size(65, 20);
            this.HelpMenu.Text = "&Справка";
            // 
            // AboutMenuItem
            // 
            this.AboutMenuItem.Name = "AboutMenuItem";
            this.AboutMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.AboutMenuItem.Size = new System.Drawing.Size(177, 22);
            this.AboutMenuItem.Text = "&О программе…";
            this.AboutMenuItem.Click += new System.EventHandler(this.AboutMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.FilesList);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.DocsList);
            this.splitContainer1.Size = new System.Drawing.Size(930, 402);
            this.splitContainer1.SplitterDistance = 137;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 2;
            // 
            // FilesList
            // 
            this.FilesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CntFileColumn,
            this.FileColumn,
            this.RootColumn,
            this.TotalQtyColumn,
            this.TotalSumColumn,
            this.PackSavedColumn});
            this.FilesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilesList.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FilesList.FullRowSelect = true;
            this.FilesList.GridLines = true;
            this.FilesList.Location = new System.Drawing.Point(0, 4);
            this.FilesList.MultiSelect = false;
            this.FilesList.Name = "FilesList";
            this.FilesList.Size = new System.Drawing.Size(926, 129);
            this.FilesList.TabIndex = 9;
            this.FilesList.UseCompatibleStateImageBehavior = false;
            this.FilesList.View = System.Windows.Forms.View.Details;
            this.FilesList.SelectedIndexChanged += new System.EventHandler(this.FilesList_SelectedIndexChanged);
            this.FilesList.DoubleClick += new System.EventHandler(this.FilesList_DoubleClick);
            // 
            // CntFileColumn
            // 
            this.CntFileColumn.Text = "N";
            this.CntFileColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.CntFileColumn.Width = 30;
            // 
            // FileColumn
            // 
            this.FileColumn.Text = "Входной файл";
            this.FileColumn.Width = 280;
            // 
            // RootColumn
            // 
            this.RootColumn.Text = "Тип";
            this.RootColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.RootColumn.Width = 80;
            // 
            // TotalQtyColumn
            // 
            this.TotalQtyColumn.Text = "Кол-во";
            this.TotalQtyColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TotalSumColumn
            // 
            this.TotalSumColumn.Text = "Сумма";
            this.TotalSumColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TotalSumColumn.Width = 100;
            // 
            // PackSavedColumn
            // 
            this.PackSavedColumn.Text = "Выходной файл УФЭБС";
            this.PackSavedColumn.Width = 280;
            // 
            // DocsList
            // 
            this.DocsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CntDocColumn,
            this.EDColumn,
            this.EDNoColumn,
            this.DocNoColumn,
            this.SumColumn,
            this.PayerColumn,
            this.PayeeColumn,
            this.PurposeColumn,
            this.SavedColumn});
            this.DocsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocsList.FullRowSelect = true;
            this.DocsList.GridLines = true;
            this.DocsList.Location = new System.Drawing.Point(0, 0);
            this.DocsList.Name = "DocsList";
            this.DocsList.Size = new System.Drawing.Size(926, 258);
            this.DocsList.TabIndex = 4;
            this.DocsList.UseCompatibleStateImageBehavior = false;
            this.DocsList.View = System.Windows.Forms.View.Details;
            this.DocsList.SelectedIndexChanged += new System.EventHandler(this.DocsList_SelectedIndexChanged);
            this.DocsList.DoubleClick += new System.EventHandler(this.DocsList_DoubleClick);
            // 
            // CntDocColumn
            // 
            this.CntDocColumn.Text = "N";
            this.CntDocColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.CntDocColumn.Width = 30;
            // 
            // EDNoColumn
            // 
            this.EDNoColumn.Text = "EDNo";
            this.EDNoColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // EDColumn
            // 
            this.EDColumn.Text = "Тип";
            this.EDColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DocNoColumn
            // 
            this.DocNoColumn.Text = "Номер";
            this.DocNoColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SumColumn
            // 
            this.SumColumn.Text = "Сумма";
            this.SumColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.SumColumn.Width = 80;
            // 
            // PayerColumn
            // 
            this.PayerColumn.Text = "Плательщик";
            this.PayerColumn.Width = 160;
            // 
            // PayeeColumn
            // 
            this.PayeeColumn.Text = "Получатель";
            this.PayeeColumn.Width = 160;
            // 
            // PurposeColumn
            // 
            this.PurposeColumn.Text = "Назначение";
            this.PurposeColumn.Width = 210;
            // 
            // SavedColumn
            // 
            this.SavedColumn.Text = "Выходной файл SWIFT";
            this.SavedColumn.Width = 280;
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
            // FontDialog
            // 
            this.FontDialog.Apply += new System.EventHandler(this.FontDialog_Apply);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.MainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Corr-SWIFT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private StatusStrip StatusBar;
    private MenuStrip MainMenu;
    private SplitContainer splitContainer1;
    private OpenFileDialog OpenFileDialog;
    private ToolStripMenuItem ViewMenu;
    private ToolStripMenuItem FontMenuItem;
    private FontDialog FontDialog;
    private ToolStripMenuItem FileMenu;
    private ToolStripMenuItem OpenFileMenuItem;
    private ToolStripSeparator toolStripSeparator;
    private ToolStripMenuItem ExitMenuItem;
    private ToolStripMenuItem HelpMenu;
    private ToolStripMenuItem AboutMenuItem;
    private ToolStripMenuItem ConfigMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    private ColumnHeader RootColumn;
    private ColumnHeader TotalQtyColumn;
    private ColumnHeader TotalSumColumn;
    private ToolStripStatusLabel FormatStatus;
    private ToolStripStatusLabel ProfileStatus;
    private ToolStripStatusLabel OpenStatus;
    private ToolStripStatusLabel DirStatus;
    private ToolStripStatusLabel SaveStatus;
    internal ListView FilesList;
    internal ToolStripStatusLabel Status;
    internal ListView DocsList;
    private ColumnHeader EDNoColumn;
    private ColumnHeader EDColumn;
    private ColumnHeader SumColumn;
    private ColumnHeader PayerColumn;
    private ColumnHeader PayeeColumn;
    private ColumnHeader PurposeColumn;
    internal ColumnHeader SavedColumn;
    private ColumnHeader DocNoColumn;
    internal ColumnHeader FileColumn;
    internal ColumnHeader PackSavedColumn;
    private ColumnHeader CntFileColumn;
    private ColumnHeader CntDocColumn;
}
