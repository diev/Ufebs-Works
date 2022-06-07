namespace CorrSWIFT;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.TaxValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.SavedLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.DoneLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.FilesDoneValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.FilesDoneBar = new System.Windows.Forms.ToolStripProgressBar();
            this.DocsDoneValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.DocsDoneBar = new System.Windows.Forms.ToolStripProgressBar();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.NewFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.SaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.PrintMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PrintPreviewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ConfigMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.UndoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RedoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.CutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.SelectAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GoMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.PrevMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.NextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ForwardMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.FontMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WrapMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.FilesPage = new System.Windows.Forms.TabPage();
            this.FilesListBox = new System.Windows.Forms.ListView();
            this.InColumn = new System.Windows.Forms.ColumnHeader();
            this.OutColumn = new System.Windows.Forms.ColumnHeader();
            this.XmlPage = new System.Windows.Forms.TabPage();
            this.XmlTextBox = new System.Windows.Forms.TextBox();
            this.EdPage = new System.Windows.Forms.TabPage();
            this.DocsListBox = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.OutEdPage = new System.Windows.Forms.TabPage();
            this.OutDocsListBox = new System.Windows.Forms.ListView();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.SwiftPage = new System.Windows.Forms.TabPage();
            this.SwiftTextBox = new System.Windows.Forms.TextBox();
            this.OutSwiftPage = new System.Windows.Forms.TabPage();
            this.OutSwiftTextBox = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.NameEditLabel = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.PurposeTextBox = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.PurposeEditLabel = new System.Windows.Forms.Label();
            this.ButtonsPanel = new System.Windows.Forms.Panel();
            this.PrevButton = new System.Windows.Forms.Button();
            this.ForwardButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.OutEditCheck = new System.Windows.Forms.CheckBox();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveAsFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.FontDialog = new System.Windows.Forms.FontDialog();
            this.PrintPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.PrintDocument = new System.Drawing.Printing.PrintDocument();
            this.PrintDialog = new System.Windows.Forms.PrintDialog();
            this.StatusBar.SuspendLayout();
            this.MainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.Tabs.SuspendLayout();
            this.FilesPage.SuspendLayout();
            this.XmlPage.SuspendLayout();
            this.EdPage.SuspendLayout();
            this.OutEdPage.SuspendLayout();
            this.SwiftPage.SuspendLayout();
            this.OutSwiftPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.ButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusBar
            // 
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TaxValue,
            this.SavedLabel,
            this.DoneLabel,
            this.FilesDoneValue,
            this.FilesDoneBar,
            this.DocsDoneValue,
            this.DocsDoneBar});
            this.StatusBar.Location = new System.Drawing.Point(0, 424);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(930, 26);
            this.StatusBar.TabIndex = 0;
            this.StatusBar.Text = "statusStrip1";
            // 
            // TaxValue
            // 
            this.TaxValue.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.TaxValue.Name = "TaxValue";
            this.TaxValue.Size = new System.Drawing.Size(251, 21);
            this.TaxValue.Text = "Выберите файл(ы) в меню Файл\\Открыть...";
            // 
            // SavedLabel
            // 
            this.SavedLabel.Name = "SavedLabel";
            this.SavedLabel.Size = new System.Drawing.Size(131, 21);
            this.SavedLabel.Text = "Результат не сохранен";
            // 
            // DoneLabel
            // 
            this.DoneLabel.Name = "DoneLabel";
            this.DoneLabel.Size = new System.Drawing.Size(257, 21);
            this.DoneLabel.Spring = true;
            this.DoneLabel.Text = "Сделано:";
            this.DoneLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FilesDoneValue
            // 
            this.FilesDoneValue.Name = "FilesDoneValue";
            this.FilesDoneValue.Size = new System.Drawing.Size(13, 21);
            this.FilesDoneValue.Text = "0";
            this.FilesDoneValue.ToolTipText = "Файлы";
            // 
            // FilesDoneBar
            // 
            this.FilesDoneBar.Margin = new System.Windows.Forms.Padding(10, 7, 15, 7);
            this.FilesDoneBar.Name = "FilesDoneBar";
            this.FilesDoneBar.Size = new System.Drawing.Size(100, 12);
            this.FilesDoneBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.FilesDoneBar.ToolTipText = "Файлы";
            // 
            // DocsDoneValue
            // 
            this.DocsDoneValue.Name = "DocsDoneValue";
            this.DocsDoneValue.Size = new System.Drawing.Size(13, 21);
            this.DocsDoneValue.Text = "0";
            this.DocsDoneValue.ToolTipText = "Документы";
            // 
            // DocsDoneBar
            // 
            this.DocsDoneBar.Margin = new System.Windows.Forms.Padding(10, 7, 15, 7);
            this.DocsDoneBar.Name = "DocsDoneBar";
            this.DocsDoneBar.Size = new System.Drawing.Size(100, 12);
            this.DocsDoneBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.DocsDoneBar.ToolTipText = "Документы";
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.EditMenu,
            this.GoMenu,
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
            this.NewFileMenuItem,
            this.OpenFileMenuItem,
            this.toolStripSeparator,
            this.SaveMenuItem,
            this.SaveAsMenuItem,
            this.toolStripSeparator2,
            this.PrintMenuItem,
            this.PrintPreviewMenuItem,
            this.toolStripSeparator3,
            this.ConfigMenuItem,
            this.toolStripSeparator1,
            this.ExitMenuItem});
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(48, 20);
            this.FileMenu.Text = "&Файл";
            // 
            // NewFileMenuItem
            // 
            this.NewFileMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("NewFileMenuItem.Image")));
            this.NewFileMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewFileMenuItem.Name = "NewFileMenuItem";
            this.NewFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.NewFileMenuItem.Size = new System.Drawing.Size(242, 22);
            this.NewFileMenuItem.Text = "&Создать";
            this.NewFileMenuItem.Visible = false;
            this.NewFileMenuItem.Click += new System.EventHandler(this.NewFileMenuItem_Click);
            // 
            // OpenFileMenuItem
            // 
            this.OpenFileMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("OpenFileMenuItem.Image")));
            this.OpenFileMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenFileMenuItem.Name = "OpenFileMenuItem";
            this.OpenFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OpenFileMenuItem.Size = new System.Drawing.Size(242, 22);
            this.OpenFileMenuItem.Text = "&Открыть...";
            this.OpenFileMenuItem.Click += new System.EventHandler(this.OpenMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(239, 6);
            // 
            // SaveMenuItem
            // 
            this.SaveMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("SaveMenuItem.Image")));
            this.SaveMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveMenuItem.Name = "SaveMenuItem";
            this.SaveMenuItem.Size = new System.Drawing.Size(242, 22);
            this.SaveMenuItem.Text = "&Сохранить";
            this.SaveMenuItem.Click += new System.EventHandler(this.SaveMenuItem_Click);
            // 
            // SaveAsMenuItem
            // 
            this.SaveAsMenuItem.Name = "SaveAsMenuItem";
            this.SaveAsMenuItem.Size = new System.Drawing.Size(242, 22);
            this.SaveAsMenuItem.Text = "Сохранить &как...";
            this.SaveAsMenuItem.Click += new System.EventHandler(this.SaveAsMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(239, 6);
            // 
            // PrintMenuItem
            // 
            this.PrintMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("PrintMenuItem.Image")));
            this.PrintMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PrintMenuItem.Name = "PrintMenuItem";
            this.PrintMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.PrintMenuItem.Size = new System.Drawing.Size(242, 22);
            this.PrintMenuItem.Text = "&Печать";
            this.PrintMenuItem.Click += new System.EventHandler(this.PrintMenuItem_Click);
            // 
            // PrintPreviewMenuItem
            // 
            this.PrintPreviewMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("PrintPreviewMenuItem.Image")));
            this.PrintPreviewMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PrintPreviewMenuItem.Name = "PrintPreviewMenuItem";
            this.PrintPreviewMenuItem.Size = new System.Drawing.Size(242, 22);
            this.PrintPreviewMenuItem.Text = "Предварительный про&смотр...";
            this.PrintPreviewMenuItem.Click += new System.EventHandler(this.PrintPreviewMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(239, 6);
            // 
            // ConfigMenuItem
            // 
            this.ConfigMenuItem.Name = "ConfigMenuItem";
            this.ConfigMenuItem.Size = new System.Drawing.Size(242, 22);
            this.ConfigMenuItem.Text = "П&араметры...";
            this.ConfigMenuItem.Click += new System.EventHandler(this.ConfigMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(239, 6);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.ExitMenuItem.Size = new System.Drawing.Size(242, 22);
            this.ExitMenuItem.Text = "Вы&ход";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // EditMenu
            // 
            this.EditMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ChangeMenuItem,
            this.toolStripSeparator6,
            this.UndoMenuItem,
            this.RedoMenuItem,
            this.toolStripSeparator4,
            this.CutMenuItem,
            this.CopyMenuItem,
            this.PasteMenuItem,
            this.toolStripSeparator5,
            this.SelectAllMenuItem});
            this.EditMenu.Name = "EditMenu";
            this.EditMenu.Size = new System.Drawing.Size(59, 20);
            this.EditMenu.Text = "&Правка";
            // 
            // ChangeMenuItem
            // 
            this.ChangeMenuItem.Name = "ChangeMenuItem";
            this.ChangeMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.ChangeMenuItem.Size = new System.Drawing.Size(184, 22);
            this.ChangeMenuItem.Text = "&Изменить";
            this.ChangeMenuItem.CheckedChanged += new System.EventHandler(this.ChangeMenuItem_CheckedChanged);
            this.ChangeMenuItem.Click += new System.EventHandler(this.ChangeMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(181, 6);
            this.toolStripSeparator6.Visible = false;
            // 
            // UndoMenuItem
            // 
            this.UndoMenuItem.Name = "UndoMenuItem";
            this.UndoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.UndoMenuItem.Size = new System.Drawing.Size(184, 22);
            this.UndoMenuItem.Text = "&Отменить";
            this.UndoMenuItem.Visible = false;
            this.UndoMenuItem.Click += new System.EventHandler(this.UndoMenuItem_Click);
            // 
            // RedoMenuItem
            // 
            this.RedoMenuItem.Name = "RedoMenuItem";
            this.RedoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.RedoMenuItem.Size = new System.Drawing.Size(184, 22);
            this.RedoMenuItem.Text = "&Повторить";
            this.RedoMenuItem.Visible = false;
            this.RedoMenuItem.Click += new System.EventHandler(this.RedoMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(181, 6);
            this.toolStripSeparator4.Visible = false;
            // 
            // CutMenuItem
            // 
            this.CutMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("CutMenuItem.Image")));
            this.CutMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CutMenuItem.Name = "CutMenuItem";
            this.CutMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.CutMenuItem.Size = new System.Drawing.Size(184, 22);
            this.CutMenuItem.Text = "В&ырезать";
            this.CutMenuItem.Visible = false;
            this.CutMenuItem.Click += new System.EventHandler(this.CutMenuItem_Click);
            // 
            // CopyMenuItem
            // 
            this.CopyMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("CopyMenuItem.Image")));
            this.CopyMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CopyMenuItem.Name = "CopyMenuItem";
            this.CopyMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.CopyMenuItem.Size = new System.Drawing.Size(184, 22);
            this.CopyMenuItem.Text = "&Копировать";
            this.CopyMenuItem.Visible = false;
            this.CopyMenuItem.Click += new System.EventHandler(this.CopyMenuItem_Click);
            // 
            // PasteMenuItem
            // 
            this.PasteMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("PasteMenuItem.Image")));
            this.PasteMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PasteMenuItem.Name = "PasteMenuItem";
            this.PasteMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.PasteMenuItem.Size = new System.Drawing.Size(184, 22);
            this.PasteMenuItem.Text = "&Вставить";
            this.PasteMenuItem.Visible = false;
            this.PasteMenuItem.Click += new System.EventHandler(this.PasteMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(181, 6);
            this.toolStripSeparator5.Visible = false;
            // 
            // SelectAllMenuItem
            // 
            this.SelectAllMenuItem.Name = "SelectAllMenuItem";
            this.SelectAllMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.SelectAllMenuItem.Size = new System.Drawing.Size(184, 22);
            this.SelectAllMenuItem.Text = "Выбрать &все";
            this.SelectAllMenuItem.Visible = false;
            this.SelectAllMenuItem.Click += new System.EventHandler(this.SelectAllMenuItem_Click);
            // 
            // GoMenu
            // 
            this.GoMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PrevMenuItem,
            this.toolStripMenuItem1,
            this.NextMenuItem,
            this.ForwardMenuItem});
            this.GoMenu.Name = "GoMenu";
            this.GoMenu.Size = new System.Drawing.Size(66, 20);
            this.GoMenu.Text = "П&ереход";
            // 
            // PrevMenuItem
            // 
            this.PrevMenuItem.Name = "PrevMenuItem";
            this.PrevMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.PrevMenuItem.Size = new System.Drawing.Size(315, 22);
            this.PrevMenuItem.Text = "&Назад";
            this.PrevMenuItem.Click += new System.EventHandler(this.PrevMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(312, 6);
            // 
            // NextMenuItem
            // 
            this.NextMenuItem.Name = "NextMenuItem";
            this.NextMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.NextMenuItem.Size = new System.Drawing.Size(315, 22);
            this.NextMenuItem.Text = "&Сохранить и дальше";
            this.NextMenuItem.Click += new System.EventHandler(this.NextMenuItem_Click);
            // 
            // ForwardMenuItem
            // 
            this.ForwardMenuItem.Name = "ForwardMenuItem";
            this.ForwardMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.ForwardMenuItem.Size = new System.Drawing.Size(315, 22);
            this.ForwardMenuItem.Text = "С&охранить и до следующей ошибки";
            this.ForwardMenuItem.Click += new System.EventHandler(this.ForwardMenuItem_Click);
            // 
            // ViewMenu
            // 
            this.ViewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FontMenuItem,
            this.WrapMenuItem});
            this.ViewMenu.Name = "ViewMenu";
            this.ViewMenu.Size = new System.Drawing.Size(39, 20);
            this.ViewMenu.Text = "&Вид";
            // 
            // FontMenuItem
            // 
            this.FontMenuItem.Name = "FontMenuItem";
            this.FontMenuItem.Size = new System.Drawing.Size(183, 22);
            this.FontMenuItem.Text = "&Шрифт...";
            this.FontMenuItem.Click += new System.EventHandler(this.FontMenuItem_Click);
            // 
            // WrapMenuItem
            // 
            this.WrapMenuItem.Name = "WrapMenuItem";
            this.WrapMenuItem.Size = new System.Drawing.Size(183, 22);
            this.WrapMenuItem.Text = "Перенос по словам";
            this.WrapMenuItem.Click += new System.EventHandler(this.WrapMenuItem_Click);
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
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Tabs);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(930, 400);
            this.splitContainer1.SplitterDistance = 616;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 2;
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.FilesPage);
            this.Tabs.Controls.Add(this.XmlPage);
            this.Tabs.Controls.Add(this.EdPage);
            this.Tabs.Controls.Add(this.OutEdPage);
            this.Tabs.Controls.Add(this.SwiftPage);
            this.Tabs.Controls.Add(this.OutSwiftPage);
            this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabs.Location = new System.Drawing.Point(0, 4);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(612, 392);
            this.Tabs.TabIndex = 0;
            this.Tabs.Selected += new System.Windows.Forms.TabControlEventHandler(this.Tabs_Selected);
            // 
            // FilesPage
            // 
            this.FilesPage.Controls.Add(this.FilesListBox);
            this.FilesPage.Location = new System.Drawing.Point(4, 24);
            this.FilesPage.Name = "FilesPage";
            this.FilesPage.Padding = new System.Windows.Forms.Padding(3);
            this.FilesPage.Size = new System.Drawing.Size(604, 364);
            this.FilesPage.TabIndex = 3;
            this.FilesPage.Text = "Файлы";
            this.FilesPage.UseVisualStyleBackColor = true;
            // 
            // FilesListBox
            // 
            this.FilesListBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.InColumn,
            this.OutColumn});
            this.FilesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilesListBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FilesListBox.FullRowSelect = true;
            this.FilesListBox.GridLines = true;
            this.FilesListBox.Location = new System.Drawing.Point(3, 3);
            this.FilesListBox.MultiSelect = false;
            this.FilesListBox.Name = "FilesListBox";
            this.FilesListBox.Size = new System.Drawing.Size(598, 358);
            this.FilesListBox.TabIndex = 0;
            this.FilesListBox.UseCompatibleStateImageBehavior = false;
            this.FilesListBox.View = System.Windows.Forms.View.Details;
            this.FilesListBox.SizeChanged += new System.EventHandler(this.FilesListBox_SizeChanged);
            this.FilesListBox.Click += new System.EventHandler(this.FilesListBox_Click);
            // 
            // InColumn
            // 
            this.InColumn.Text = "Входной файл";
            // 
            // OutColumn
            // 
            this.OutColumn.Text = "Выходной файл";
            // 
            // XmlPage
            // 
            this.XmlPage.Controls.Add(this.XmlTextBox);
            this.XmlPage.Location = new System.Drawing.Point(4, 24);
            this.XmlPage.Name = "XmlPage";
            this.XmlPage.Padding = new System.Windows.Forms.Padding(3);
            this.XmlPage.Size = new System.Drawing.Size(604, 364);
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
            this.XmlTextBox.PlaceholderText = "Исходные файлы XML не найдены.";
            this.XmlTextBox.ReadOnly = true;
            this.XmlTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.XmlTextBox.Size = new System.Drawing.Size(598, 358);
            this.XmlTextBox.TabIndex = 2;
            this.XmlTextBox.WordWrap = false;
            // 
            // EdPage
            // 
            this.EdPage.Controls.Add(this.DocsListBox);
            this.EdPage.Location = new System.Drawing.Point(4, 24);
            this.EdPage.Name = "EdPage";
            this.EdPage.Padding = new System.Windows.Forms.Padding(3);
            this.EdPage.Size = new System.Drawing.Size(604, 364);
            this.EdPage.TabIndex = 4;
            this.EdPage.Text = "ED";
            this.EdPage.UseVisualStyleBackColor = true;
            // 
            // DocsListBox
            // 
            this.DocsListBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader4});
            this.DocsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocsListBox.FullRowSelect = true;
            this.DocsListBox.GridLines = true;
            this.DocsListBox.Location = new System.Drawing.Point(3, 3);
            this.DocsListBox.Name = "DocsListBox";
            this.DocsListBox.Size = new System.Drawing.Size(598, 358);
            this.DocsListBox.TabIndex = 0;
            this.DocsListBox.UseCompatibleStateImageBehavior = false;
            this.DocsListBox.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ED";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Сумма";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Плательщик";
            this.columnHeader3.Width = 160;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Получатель";
            this.columnHeader5.Width = 160;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Назначение";
            this.columnHeader4.Width = 320;
            // 
            // OutEdPage
            // 
            this.OutEdPage.Controls.Add(this.OutDocsListBox);
            this.OutEdPage.Location = new System.Drawing.Point(4, 24);
            this.OutEdPage.Name = "OutEdPage";
            this.OutEdPage.Padding = new System.Windows.Forms.Padding(3);
            this.OutEdPage.Size = new System.Drawing.Size(604, 364);
            this.OutEdPage.TabIndex = 5;
            this.OutEdPage.Text = "К отправке ED";
            this.OutEdPage.UseVisualStyleBackColor = true;
            // 
            // OutDocsListBox
            // 
            this.OutDocsListBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10});
            this.OutDocsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutDocsListBox.FullRowSelect = true;
            this.OutDocsListBox.GridLines = true;
            this.OutDocsListBox.Location = new System.Drawing.Point(3, 3);
            this.OutDocsListBox.Name = "OutDocsListBox";
            this.OutDocsListBox.Size = new System.Drawing.Size(598, 358);
            this.OutDocsListBox.TabIndex = 1;
            this.OutDocsListBox.UseCompatibleStateImageBehavior = false;
            this.OutDocsListBox.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "ED";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Сумма";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader7.Width = 80;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Плательщик";
            this.columnHeader8.Width = 260;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Получатель";
            this.columnHeader9.Width = 160;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Назначение";
            this.columnHeader10.Width = 400;
            // 
            // SwiftPage
            // 
            this.SwiftPage.Controls.Add(this.SwiftTextBox);
            this.SwiftPage.Location = new System.Drawing.Point(4, 24);
            this.SwiftPage.Name = "SwiftPage";
            this.SwiftPage.Padding = new System.Windows.Forms.Padding(3);
            this.SwiftPage.Size = new System.Drawing.Size(604, 364);
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
            this.SwiftTextBox.PlaceholderText = "Документ не имеет тип ED503.";
            this.SwiftTextBox.ReadOnly = true;
            this.SwiftTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.SwiftTextBox.Size = new System.Drawing.Size(598, 358);
            this.SwiftTextBox.TabIndex = 1;
            this.SwiftTextBox.WordWrap = false;
            // 
            // OutSwiftPage
            // 
            this.OutSwiftPage.Controls.Add(this.OutSwiftTextBox);
            this.OutSwiftPage.Location = new System.Drawing.Point(4, 24);
            this.OutSwiftPage.Name = "OutSwiftPage";
            this.OutSwiftPage.Padding = new System.Windows.Forms.Padding(3);
            this.OutSwiftPage.Size = new System.Drawing.Size(604, 364);
            this.OutSwiftPage.TabIndex = 2;
            this.OutSwiftPage.Text = "К отправке SWIFT";
            this.OutSwiftPage.UseVisualStyleBackColor = true;
            // 
            // OutSwiftTextBox
            // 
            this.OutSwiftTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutSwiftTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.OutSwiftTextBox.Location = new System.Drawing.Point(3, 3);
            this.OutSwiftTextBox.Multiline = true;
            this.OutSwiftTextBox.Name = "OutSwiftTextBox";
            this.OutSwiftTextBox.PlaceholderText = "Нечего отправлять.";
            this.OutSwiftTextBox.ReadOnly = true;
            this.OutSwiftTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.OutSwiftTextBox.Size = new System.Drawing.Size(598, 358);
            this.OutSwiftTextBox.TabIndex = 2;
            this.OutSwiftTextBox.WordWrap = false;
            this.OutSwiftTextBox.TextChanged += new System.EventHandler(this.OutTextBox_TextChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel3);
            this.splitContainer2.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel4);
            this.splitContainer2.Panel2.Controls.Add(this.panel2);
            this.splitContainer2.Panel2.Controls.Add(this.ButtonsPanel);
            this.splitContainer2.Size = new System.Drawing.Size(311, 400);
            this.splitContainer2.SplitterDistance = 162;
            this.splitContainer2.SplitterWidth = 3;
            this.splitContainer2.TabIndex = 7;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.NameTextBox);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 30);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(307, 128);
            this.panel3.TabIndex = 6;
            // 
            // NameTextBox
            // 
            this.NameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NameTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NameTextBox.Location = new System.Drawing.Point(0, 0);
            this.NameTextBox.Multiline = true;
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.PlaceholderText = "3 строки по 35.";
            this.NameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.NameTextBox.Size = new System.Drawing.Size(307, 128);
            this.NameTextBox.TabIndex = 4;
            this.NameTextBox.TextChanged += new System.EventHandler(this.NameTextBox_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.NameEditLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(307, 30);
            this.panel1.TabIndex = 5;
            // 
            // NameEditLabel
            // 
            this.NameEditLabel.AutoSize = true;
            this.NameEditLabel.Location = new System.Drawing.Point(6, 9);
            this.NameEditLabel.Name = "NameEditLabel";
            this.NameEditLabel.Size = new System.Drawing.Size(80, 15);
            this.NameEditLabel.TabIndex = 5;
            this.NameEditLabel.Text = "Плательщик:";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.PurposeTextBox);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 30);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(307, 161);
            this.panel4.TabIndex = 13;
            // 
            // PurposeTextBox
            // 
            this.PurposeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PurposeTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PurposeTextBox.Location = new System.Drawing.Point(0, 0);
            this.PurposeTextBox.Multiline = true;
            this.PurposeTextBox.Name = "PurposeTextBox";
            this.PurposeTextBox.PlaceholderText = "4+2 строки по 35.";
            this.PurposeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.PurposeTextBox.Size = new System.Drawing.Size(307, 161);
            this.PurposeTextBox.TabIndex = 5;
            this.PurposeTextBox.TextChanged += new System.EventHandler(this.PurposeTextBox_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.PurposeEditLabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(307, 30);
            this.panel2.TabIndex = 12;
            // 
            // PurposeEditLabel
            // 
            this.PurposeEditLabel.AutoSize = true;
            this.PurposeEditLabel.Location = new System.Drawing.Point(6, 9);
            this.PurposeEditLabel.Name = "PurposeEditLabel";
            this.PurposeEditLabel.Size = new System.Drawing.Size(125, 15);
            this.PurposeEditLabel.TabIndex = 6;
            this.PurposeEditLabel.Text = "Назначение платежа:";
            // 
            // ButtonsPanel
            // 
            this.ButtonsPanel.Controls.Add(this.PrevButton);
            this.ButtonsPanel.Controls.Add(this.ForwardButton);
            this.ButtonsPanel.Controls.Add(this.NextButton);
            this.ButtonsPanel.Controls.Add(this.OutEditCheck);
            this.ButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ButtonsPanel.Location = new System.Drawing.Point(0, 191);
            this.ButtonsPanel.Name = "ButtonsPanel";
            this.ButtonsPanel.Size = new System.Drawing.Size(307, 40);
            this.ButtonsPanel.TabIndex = 11;
            // 
            // PrevButton
            // 
            this.PrevButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PrevButton.Enabled = false;
            this.PrevButton.Location = new System.Drawing.Point(108, 6);
            this.PrevButton.Name = "PrevButton";
            this.PrevButton.Size = new System.Drawing.Size(75, 23);
            this.PrevButton.TabIndex = 11;
            this.PrevButton.Text = "Назад";
            this.PrevButton.UseVisualStyleBackColor = true;
            this.PrevButton.Click += new System.EventHandler(this.PrevMenuItem_Click);
            // 
            // ForwardButton
            // 
            this.ForwardButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ForwardButton.Enabled = false;
            this.ForwardButton.Location = new System.Drawing.Point(270, 6);
            this.ForwardButton.Name = "ForwardButton";
            this.ForwardButton.Size = new System.Drawing.Size(34, 23);
            this.ForwardButton.TabIndex = 13;
            this.ForwardButton.Text = ">>";
            this.ForwardButton.UseVisualStyleBackColor = true;
            this.ForwardButton.Click += new System.EventHandler(this.ForwardMenuItem_Click);
            // 
            // NextButton
            // 
            this.NextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NextButton.Enabled = false;
            this.NextButton.Location = new System.Drawing.Point(189, 6);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(75, 23);
            this.NextButton.TabIndex = 12;
            this.NextButton.Text = "Дальше";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextMenuItem_Click);
            // 
            // OutEditCheck
            // 
            this.OutEditCheck.AutoSize = true;
            this.OutEditCheck.Location = new System.Drawing.Point(6, 9);
            this.OutEditCheck.Name = "OutEditCheck";
            this.OutEditCheck.Size = new System.Drawing.Size(80, 19);
            this.OutEditCheck.TabIndex = 14;
            this.OutEditCheck.Text = "Изменить";
            this.OutEditCheck.UseVisualStyleBackColor = true;
            this.OutEditCheck.CheckedChanged += new System.EventHandler(this.ChangeMenuItem_Click);
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
            // PrintPreviewDialog
            // 
            this.PrintPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.PrintPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.PrintPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.PrintPreviewDialog.Document = this.PrintDocument;
            this.PrintPreviewDialog.Enabled = true;
            this.PrintPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("PrintPreviewDialog.Icon")));
            this.PrintPreviewDialog.Name = "PrintPreviewDialog";
            this.PrintPreviewDialog.Visible = false;
            // 
            // PrintDocument
            // 
            this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocument_PrintPage);
            // 
            // PrintDialog
            // 
            this.PrintDialog.UseEXDialog = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "Form1";
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
            this.Tabs.ResumeLayout(false);
            this.FilesPage.ResumeLayout(false);
            this.XmlPage.ResumeLayout(false);
            this.XmlPage.PerformLayout();
            this.EdPage.ResumeLayout(false);
            this.OutEdPage.ResumeLayout(false);
            this.SwiftPage.ResumeLayout(false);
            this.SwiftPage.PerformLayout();
            this.OutSwiftPage.ResumeLayout(false);
            this.OutSwiftPage.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ButtonsPanel.ResumeLayout(false);
            this.ButtonsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private StatusStrip StatusBar;
    private MenuStrip MainMenu;
    private SplitContainer splitContainer1;
    private OpenFileDialog OpenFileDialog;
    private SaveFileDialog SaveAsFileDialog;
    private ToolStripMenuItem ViewMenu;
    private TabControl Tabs;
    private TabPage XmlPage;
    private TextBox XmlTextBox;
    private TabPage SwiftPage;
    private TextBox SwiftTextBox;
    private TabPage OutSwiftPage;
    private TextBox OutSwiftTextBox;
    private ToolStripMenuItem FontMenuItem;
    private FontDialog FontDialog;
    private ToolStripStatusLabel TaxValue;
    private TabPage FilesPage;
    private ToolStripProgressBar FilesDoneBar;
    private ToolStripStatusLabel DoneLabel;
    private ToolStripMenuItem FileMenu;
    private ToolStripMenuItem NewFileMenuItem;
    private ToolStripMenuItem OpenFileMenuItem;
    private ToolStripSeparator toolStripSeparator;
    private ToolStripMenuItem SaveMenuItem;
    private ToolStripMenuItem SaveAsMenuItem;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripMenuItem PrintMenuItem;
    private ToolStripMenuItem PrintPreviewMenuItem;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripMenuItem ExitMenuItem;
    private ToolStripMenuItem EditMenu;
    private ToolStripMenuItem UndoMenuItem;
    private ToolStripMenuItem RedoMenuItem;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripMenuItem CutMenuItem;
    private ToolStripMenuItem CopyMenuItem;
    private ToolStripMenuItem PasteMenuItem;
    private ToolStripSeparator toolStripSeparator5;
    private ToolStripMenuItem SelectAllMenuItem;
    private ToolStripMenuItem GoMenu;
    private ToolStripMenuItem PrevMenuItem;
    private ToolStripSeparator toolStripMenuItem1;
    private ToolStripMenuItem NextMenuItem;
    private ToolStripMenuItem ForwardMenuItem;
    private ToolStripMenuItem HelpMenu;
    private ToolStripMenuItem AboutMenuItem;
    private ToolStripMenuItem ConfigMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem ChangeMenuItem;
    private ToolStripSeparator toolStripSeparator6;
    private ToolStripStatusLabel FilesDoneValue;
    private ToolStripMenuItem WrapMenuItem;
    private PrintPreviewDialog PrintPreviewDialog;
    private System.Drawing.Printing.PrintDocument PrintDocument;
    private PrintDialog PrintDialog;
    private ToolStripStatusLabel SavedLabel;
    private SplitContainer splitContainer2;
    private Panel ButtonsPanel;
    private Button PrevButton;
    private Button ForwardButton;
    private Button NextButton;
    private CheckBox OutEditCheck;
    private Panel panel3;
    private TextBox NameTextBox;
    private Panel panel1;
    private Label NameEditLabel;
    private Panel panel4;
    private TextBox PurposeTextBox;
    private Panel panel2;
    private Label PurposeEditLabel;
    private TabPage EdPage;
    private ListView DocsListBox;
    private ColumnHeader columnHeader1;
    private ColumnHeader columnHeader2;
    private ColumnHeader columnHeader3;
    private ColumnHeader columnHeader4;
    private ToolStripStatusLabel DocsDoneValue;
    private ToolStripProgressBar DocsDoneBar;
    private ListView FilesListBox;
    private ColumnHeader InColumn;
    private ColumnHeader OutColumn;
    private ColumnHeader columnHeader5;
    private TabPage OutEdPage;
    private ListView OutDocsListBox;
    private ColumnHeader columnHeader6;
    private ColumnHeader columnHeader7;
    private ColumnHeader columnHeader8;
    private ColumnHeader columnHeader9;
    private ColumnHeader columnHeader10;
}
