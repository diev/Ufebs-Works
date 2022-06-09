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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.DoneLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.FilesDone = new System.Windows.Forms.ToolStripStatusLabel();
            this.FilesDoneBar = new System.Windows.Forms.ToolStripProgressBar();
            this.DocsDone = new System.Windows.Forms.ToolStripStatusLabel();
            this.DocsDoneBar = new System.Windows.Forms.ToolStripProgressBar();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
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
            this.GoMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.NextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FastNextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.FontMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WrapMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.FilesPage = new System.Windows.Forms.TabPage();
            this.FilesList = new System.Windows.Forms.ListView();
            this.FileColumn = new System.Windows.Forms.ColumnHeader();
            this.RootColumn = new System.Windows.Forms.ColumnHeader();
            this.TotalQtyColumn = new System.Windows.Forms.ColumnHeader();
            this.TotalSumColumn = new System.Windows.Forms.ColumnHeader();
            this.PackSavedColumn = new System.Windows.Forms.ColumnHeader();
            this.XmlPage = new System.Windows.Forms.TabPage();
            this.XmlText = new System.Windows.Forms.TextBox();
            this.PacketPage = new System.Windows.Forms.TabPage();
            this.DocsList = new System.Windows.Forms.ListView();
            this.NoColumn = new System.Windows.Forms.ColumnHeader();
            this.EDColumn = new System.Windows.Forms.ColumnHeader();
            this.SumColumn = new System.Windows.Forms.ColumnHeader();
            this.PayerColumn = new System.Windows.Forms.ColumnHeader();
            this.PayeeColumn = new System.Windows.Forms.ColumnHeader();
            this.PurposeColumn = new System.Windows.Forms.ColumnHeader();
            this.SavedColumn = new System.Windows.Forms.ColumnHeader();
            this.SwiftPage = new System.Windows.Forms.TabPage();
            this.SwiftText = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.NamePanel = new System.Windows.Forms.Panel();
            this.NameLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.NameSwiftText = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.NameEdit = new System.Windows.Forms.TextBox();
            this.PurposePanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.PurposeSwiftText = new System.Windows.Forms.TextBox();
            this.PurposeLabel = new System.Windows.Forms.Label();
            this.PurposeEdit = new System.Windows.Forms.TextBox();
            this.ButtonsPanel = new System.Windows.Forms.Panel();
            this.FastNextButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveAsFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.FontDialog = new System.Windows.Forms.FontDialog();
            this.PrintPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.PrintDocument = new System.Drawing.Printing.PrintDocument();
            this.PrintDialog = new System.Windows.Forms.PrintDialog();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.StatusBar.SuspendLayout();
            this.MainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.Tabs.SuspendLayout();
            this.FilesPage.SuspendLayout();
            this.XmlPage.SuspendLayout();
            this.PacketPage.SuspendLayout();
            this.SwiftPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.NamePanel.SuspendLayout();
            this.NameLayoutPanel.SuspendLayout();
            this.PurposePanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.ButtonsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // StatusBar
            // 
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status,
            this.DoneLabel,
            this.FilesDone,
            this.FilesDoneBar,
            this.DocsDone,
            this.DocsDoneBar});
            this.StatusBar.Location = new System.Drawing.Point(0, 424);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(930, 26);
            this.StatusBar.TabIndex = 0;
            this.StatusBar.Text = "statusStrip1";
            // 
            // Status
            // 
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(582, 21);
            this.Status.Spring = true;
            this.Status.Text = "Результат не сохранен";
            this.Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DoneLabel
            // 
            this.DoneLabel.Name = "DoneLabel";
            this.DoneLabel.Size = new System.Drawing.Size(57, 21);
            this.DoneLabel.Text = "Сделано:";
            this.DoneLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FilesDone
            // 
            this.FilesDone.Name = "FilesDone";
            this.FilesDone.Size = new System.Drawing.Size(13, 21);
            this.FilesDone.Text = "0";
            this.FilesDone.ToolTipText = "Файлы";
            // 
            // FilesDoneBar
            // 
            this.FilesDoneBar.Margin = new System.Windows.Forms.Padding(10, 7, 15, 7);
            this.FilesDoneBar.Name = "FilesDoneBar";
            this.FilesDoneBar.Size = new System.Drawing.Size(100, 12);
            this.FilesDoneBar.Step = 1;
            this.FilesDoneBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // DocsDone
            // 
            this.DocsDone.Name = "DocsDone";
            this.DocsDone.Size = new System.Drawing.Size(13, 21);
            this.DocsDone.Text = "0";
            this.DocsDone.ToolTipText = "Документы";
            // 
            // DocsDoneBar
            // 
            this.DocsDoneBar.Margin = new System.Windows.Forms.Padding(10, 7, 15, 7);
            this.DocsDoneBar.Name = "DocsDoneBar";
            this.DocsDoneBar.Size = new System.Drawing.Size(100, 12);
            this.DocsDoneBar.Step = 1;
            this.DocsDoneBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
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
            // GoMenu
            // 
            this.GoMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NextMenuItem,
            this.FastNextMenuItem});
            this.GoMenu.Name = "GoMenu";
            this.GoMenu.Size = new System.Drawing.Size(66, 20);
            this.GoMenu.Text = "П&ереход";
            // 
            // NextMenuItem
            // 
            this.NextMenuItem.Name = "NextMenuItem";
            this.NextMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.NextMenuItem.Size = new System.Drawing.Size(315, 22);
            this.NextMenuItem.Text = "&Сохранить и дальше";
            this.NextMenuItem.Click += new System.EventHandler(this.NextMenuItem_Click);
            // 
            // FastNextMenuItem
            // 
            this.FastNextMenuItem.Name = "FastNextMenuItem";
            this.FastNextMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.FastNextMenuItem.Size = new System.Drawing.Size(315, 22);
            this.FastNextMenuItem.Text = "С&охранить и до следующей ошибки";
            this.FastNextMenuItem.Click += new System.EventHandler(this.ForwardMenuItem_Click);
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
            this.Tabs.Controls.Add(this.PacketPage);
            this.Tabs.Controls.Add(this.SwiftPage);
            this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabs.Location = new System.Drawing.Point(0, 4);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(612, 392);
            this.Tabs.TabIndex = 0;
            // 
            // FilesPage
            // 
            this.FilesPage.Controls.Add(this.FilesList);
            this.FilesPage.Location = new System.Drawing.Point(4, 24);
            this.FilesPage.Name = "FilesPage";
            this.FilesPage.Padding = new System.Windows.Forms.Padding(3);
            this.FilesPage.Size = new System.Drawing.Size(604, 364);
            this.FilesPage.TabIndex = 3;
            this.FilesPage.Text = "Файлы";
            this.FilesPage.UseVisualStyleBackColor = true;
            // 
            // FilesList
            // 
            this.FilesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FileColumn,
            this.RootColumn,
            this.TotalQtyColumn,
            this.TotalSumColumn,
            this.PackSavedColumn});
            this.FilesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilesList.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FilesList.FullRowSelect = true;
            this.FilesList.GridLines = true;
            this.FilesList.Location = new System.Drawing.Point(3, 3);
            this.FilesList.MultiSelect = false;
            this.FilesList.Name = "FilesList";
            this.FilesList.Size = new System.Drawing.Size(598, 358);
            this.FilesList.TabIndex = 0;
            this.FilesList.UseCompatibleStateImageBehavior = false;
            this.FilesList.View = System.Windows.Forms.View.Details;
            this.FilesList.Click += new System.EventHandler(this.FilesListBox_Click);
            // 
            // FileColumn
            // 
            this.FileColumn.Text = "Файл";
            this.FileColumn.Width = 260;
            // 
            // RootColumn
            // 
            this.RootColumn.Text = "Тип";
            this.RootColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.RootColumn.Width = 100;
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
            this.PackSavedColumn.Text = "Сохранен";
            this.PackSavedColumn.Width = 260;
            // 
            // XmlPage
            // 
            this.XmlPage.Controls.Add(this.XmlText);
            this.XmlPage.Location = new System.Drawing.Point(4, 24);
            this.XmlPage.Name = "XmlPage";
            this.XmlPage.Padding = new System.Windows.Forms.Padding(3);
            this.XmlPage.Size = new System.Drawing.Size(604, 364);
            this.XmlPage.TabIndex = 0;
            this.XmlPage.Text = "XML";
            this.XmlPage.UseVisualStyleBackColor = true;
            // 
            // XmlText
            // 
            this.XmlText.BackColor = System.Drawing.SystemColors.Control;
            this.XmlText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.XmlText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.XmlText.Location = new System.Drawing.Point(3, 3);
            this.XmlText.Multiline = true;
            this.XmlText.Name = "XmlText";
            this.XmlText.PlaceholderText = "Исходные файлы XML не найдены.";
            this.XmlText.ReadOnly = true;
            this.XmlText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.XmlText.Size = new System.Drawing.Size(598, 358);
            this.XmlText.TabIndex = 2;
            this.XmlText.WordWrap = false;
            // 
            // PacketPage
            // 
            this.PacketPage.Controls.Add(this.DocsList);
            this.PacketPage.Location = new System.Drawing.Point(4, 24);
            this.PacketPage.Name = "PacketPage";
            this.PacketPage.Padding = new System.Windows.Forms.Padding(3);
            this.PacketPage.Size = new System.Drawing.Size(604, 364);
            this.PacketPage.TabIndex = 4;
            this.PacketPage.Text = "PacketEPD";
            this.PacketPage.UseVisualStyleBackColor = true;
            // 
            // DocsList
            // 
            this.DocsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NoColumn,
            this.EDColumn,
            this.SumColumn,
            this.PayerColumn,
            this.PayeeColumn,
            this.PurposeColumn,
            this.SavedColumn});
            this.DocsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocsList.FullRowSelect = true;
            this.DocsList.GridLines = true;
            this.DocsList.Location = new System.Drawing.Point(3, 3);
            this.DocsList.Name = "DocsList";
            this.DocsList.Size = new System.Drawing.Size(598, 358);
            this.DocsList.TabIndex = 0;
            this.DocsList.UseCompatibleStateImageBehavior = false;
            this.DocsList.View = System.Windows.Forms.View.Details;
            // 
            // NoColumn
            // 
            this.NoColumn.Text = "No";
            this.NoColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // EDColumn
            // 
            this.EDColumn.Text = "ED";
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
            this.PurposeColumn.Width = 320;
            // 
            // SavedColumn
            // 
            this.SavedColumn.Text = "Сохранен";
            this.SavedColumn.Width = 260;
            // 
            // SwiftPage
            // 
            this.SwiftPage.Controls.Add(this.SwiftText);
            this.SwiftPage.Location = new System.Drawing.Point(4, 24);
            this.SwiftPage.Name = "SwiftPage";
            this.SwiftPage.Padding = new System.Windows.Forms.Padding(3);
            this.SwiftPage.Size = new System.Drawing.Size(604, 364);
            this.SwiftPage.TabIndex = 2;
            this.SwiftPage.Text = "SWIFT";
            this.SwiftPage.UseVisualStyleBackColor = true;
            // 
            // SwiftText
            // 
            this.SwiftText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SwiftText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SwiftText.Location = new System.Drawing.Point(3, 3);
            this.SwiftText.Multiline = true;
            this.SwiftText.Name = "SwiftText";
            this.SwiftText.PlaceholderText = "Нечего отправлять.";
            this.SwiftText.ReadOnly = true;
            this.SwiftText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.SwiftText.Size = new System.Drawing.Size(598, 358);
            this.SwiftText.TabIndex = 2;
            this.SwiftText.WordWrap = false;
            this.SwiftText.TextChanged += new System.EventHandler(this.OutTextBox_TextChanged);
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
            this.splitContainer2.Panel1.Controls.Add(this.NamePanel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.PurposePanel);
            this.splitContainer2.Panel2.Controls.Add(this.ButtonsPanel);
            this.splitContainer2.Size = new System.Drawing.Size(311, 400);
            this.splitContainer2.SplitterDistance = 162;
            this.splitContainer2.SplitterWidth = 3;
            this.splitContainer2.TabIndex = 7;
            // 
            // NamePanel
            // 
            this.NamePanel.Controls.Add(this.NameLayoutPanel);
            this.NamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NamePanel.Location = new System.Drawing.Point(0, 0);
            this.NamePanel.Name = "NamePanel";
            this.NamePanel.Size = new System.Drawing.Size(307, 158);
            this.NamePanel.TabIndex = 6;
            // 
            // NameLayoutPanel
            // 
            this.NameLayoutPanel.ColumnCount = 1;
            this.NameLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.NameLayoutPanel.Controls.Add(this.NameSwiftText, 0, 2);
            this.NameLayoutPanel.Controls.Add(this.NameLabel, 0, 0);
            this.NameLayoutPanel.Controls.Add(this.NameEdit, 0, 1);
            this.NameLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NameLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.NameLayoutPanel.Name = "NameLayoutPanel";
            this.NameLayoutPanel.RowCount = 3;
            this.NameLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.NameLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.NameLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.NameLayoutPanel.Size = new System.Drawing.Size(307, 158);
            this.NameLayoutPanel.TabIndex = 6;
            // 
            // NameSwiftText
            // 
            this.NameSwiftText.BackColor = System.Drawing.SystemColors.Control;
            this.NameSwiftText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NameSwiftText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NameSwiftText.Location = new System.Drawing.Point(3, 97);
            this.NameSwiftText.Multiline = true;
            this.NameSwiftText.Name = "NameSwiftText";
            this.NameSwiftText.PlaceholderText = "3 строки по 35.";
            this.NameSwiftText.ReadOnly = true;
            this.NameSwiftText.Size = new System.Drawing.Size(301, 58);
            this.NameSwiftText.TabIndex = 5;
            this.NameSwiftText.WordWrap = false;
            // 
            // NameLabel
            // 
            this.NameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(3, 15);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(80, 15);
            this.NameLabel.TabIndex = 6;
            this.NameLabel.Text = "Плательщик:";
            // 
            // NameEdit
            // 
            this.NameEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NameEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NameEdit.Location = new System.Drawing.Point(3, 33);
            this.NameEdit.Multiline = true;
            this.NameEdit.Name = "NameEdit";
            this.NameEdit.PlaceholderText = "160 символов.";
            this.NameEdit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NameEdit.Size = new System.Drawing.Size(301, 58);
            this.NameEdit.TabIndex = 4;
            this.NameEdit.TextChanged += new System.EventHandler(this.NameTextBox_TextChanged);
            // 
            // PurposePanel
            // 
            this.PurposePanel.Controls.Add(this.tableLayoutPanel1);
            this.PurposePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PurposePanel.Location = new System.Drawing.Point(0, 0);
            this.PurposePanel.Name = "PurposePanel";
            this.PurposePanel.Size = new System.Drawing.Size(307, 191);
            this.PurposePanel.TabIndex = 13;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.PurposeSwiftText, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.PurposeLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.PurposeEdit, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(307, 191);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // PurposeSwiftText
            // 
            this.PurposeSwiftText.BackColor = System.Drawing.SystemColors.Control;
            this.PurposeSwiftText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PurposeSwiftText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PurposeSwiftText.Location = new System.Drawing.Point(3, 113);
            this.PurposeSwiftText.Multiline = true;
            this.PurposeSwiftText.Name = "PurposeSwiftText";
            this.PurposeSwiftText.PlaceholderText = "4+2 строки по 35.";
            this.PurposeSwiftText.ReadOnly = true;
            this.PurposeSwiftText.Size = new System.Drawing.Size(301, 75);
            this.PurposeSwiftText.TabIndex = 6;
            this.PurposeSwiftText.WordWrap = false;
            // 
            // PurposeLabel
            // 
            this.PurposeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PurposeLabel.AutoSize = true;
            this.PurposeLabel.Location = new System.Drawing.Point(3, 15);
            this.PurposeLabel.Name = "PurposeLabel";
            this.PurposeLabel.Size = new System.Drawing.Size(125, 15);
            this.PurposeLabel.TabIndex = 6;
            this.PurposeLabel.Text = "Назначение платежа:";
            // 
            // PurposeEdit
            // 
            this.PurposeEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PurposeEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PurposeEdit.Location = new System.Drawing.Point(3, 33);
            this.PurposeEdit.Multiline = true;
            this.PurposeEdit.Name = "PurposeEdit";
            this.PurposeEdit.PlaceholderText = "210 символов.";
            this.PurposeEdit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.PurposeEdit.Size = new System.Drawing.Size(301, 74);
            this.PurposeEdit.TabIndex = 5;
            this.PurposeEdit.TextChanged += new System.EventHandler(this.PurposeTextBox_TextChanged);
            // 
            // ButtonsPanel
            // 
            this.ButtonsPanel.Controls.Add(this.FastNextButton);
            this.ButtonsPanel.Controls.Add(this.NextButton);
            this.ButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ButtonsPanel.Location = new System.Drawing.Point(0, 191);
            this.ButtonsPanel.Name = "ButtonsPanel";
            this.ButtonsPanel.Size = new System.Drawing.Size(307, 40);
            this.ButtonsPanel.TabIndex = 11;
            // 
            // FastNextButton
            // 
            this.FastNextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FastNextButton.Enabled = false;
            this.FastNextButton.Location = new System.Drawing.Point(270, 6);
            this.FastNextButton.Name = "FastNextButton";
            this.FastNextButton.Size = new System.Drawing.Size(34, 23);
            this.FastNextButton.TabIndex = 13;
            this.FastNextButton.Text = ">>";
            this.FastNextButton.UseVisualStyleBackColor = true;
            this.FastNextButton.Click += new System.EventHandler(this.ForwardMenuItem_Click);
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
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(CorrLib.SourceFileCollection);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.MainMenu);
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
            this.Tabs.ResumeLayout(false);
            this.FilesPage.ResumeLayout(false);
            this.XmlPage.ResumeLayout(false);
            this.XmlPage.PerformLayout();
            this.PacketPage.ResumeLayout(false);
            this.SwiftPage.ResumeLayout(false);
            this.SwiftPage.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.NamePanel.ResumeLayout(false);
            this.NameLayoutPanel.ResumeLayout(false);
            this.NameLayoutPanel.PerformLayout();
            this.PurposePanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ButtonsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
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
    private TextBox XmlText;
    private TabPage SwiftPage;
    private TextBox SwiftText;
    private ToolStripMenuItem FontMenuItem;
    private FontDialog FontDialog;
    private TabPage FilesPage;
    private ToolStripProgressBar FilesDoneBar;
    private ToolStripStatusLabel DoneLabel;
    private ToolStripMenuItem FileMenu;
    private ToolStripMenuItem OpenFileMenuItem;
    private ToolStripSeparator toolStripSeparator;
    private ToolStripMenuItem SaveMenuItem;
    private ToolStripMenuItem SaveAsMenuItem;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripMenuItem PrintMenuItem;
    private ToolStripMenuItem PrintPreviewMenuItem;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripMenuItem ExitMenuItem;
    private ToolStripMenuItem GoMenu;
    private ToolStripMenuItem NextMenuItem;
    private ToolStripMenuItem FastNextMenuItem;
    private ToolStripMenuItem HelpMenu;
    private ToolStripMenuItem AboutMenuItem;
    private ToolStripMenuItem ConfigMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripStatusLabel FilesDone;
    private ToolStripMenuItem WrapMenuItem;
    private PrintPreviewDialog PrintPreviewDialog;
    private System.Drawing.Printing.PrintDocument PrintDocument;
    private PrintDialog PrintDialog;
    private ToolStripStatusLabel Status;
    private SplitContainer splitContainer2;
    private Panel ButtonsPanel;
    private Button FastNextButton;
    private Button NextButton;
    private Panel NamePanel;
    private TextBox NameEdit;
    private Panel PurposePanel;
    private TextBox PurposeEdit;
    private Label PurposeLabel;
    private TabPage PacketPage;
    private ListView DocsList;
    private ColumnHeader EDColumn;
    private ColumnHeader SumColumn;
    private ColumnHeader PayerColumn;
    private ColumnHeader PurposeColumn;
    private ToolStripStatusLabel DocsDone;
    private ToolStripProgressBar DocsDoneBar;
    private ListView FilesList;
    private ColumnHeader FileColumn;
    private ColumnHeader RootColumn;
    private ColumnHeader PayeeColumn;
    private TableLayoutPanel NameLayoutPanel;
    private TextBox NameSwiftText;
    private Label NameLabel;
    private TableLayoutPanel tableLayoutPanel1;
    private TextBox PurposeSwiftText;
    private ColumnHeader NoColumn;
    private ColumnHeader TotalQtyColumn;
    private ColumnHeader TotalSumColumn;
    private ColumnHeader PackSavedColumn;
    private ColumnHeader SavedColumn;
    private BindingSource bindingSource1;
}
