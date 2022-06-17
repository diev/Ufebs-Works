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
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.SaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.FilesList = new System.Windows.Forms.ListView();
            this.FileColumn = new System.Windows.Forms.ColumnHeader();
            this.RootColumn = new System.Windows.Forms.ColumnHeader();
            this.TotalQtyColumn = new System.Windows.Forms.ColumnHeader();
            this.TotalSumColumn = new System.Windows.Forms.ColumnHeader();
            this.PackSavedColumn = new System.Windows.Forms.ColumnHeader();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.DocsList = new System.Windows.Forms.ListView();
            this.NoColumn = new System.Windows.Forms.ColumnHeader();
            this.EDColumn = new System.Windows.Forms.ColumnHeader();
            this.SumColumn = new System.Windows.Forms.ColumnHeader();
            this.PayerColumn = new System.Windows.Forms.ColumnHeader();
            this.CorrColumn = new System.Windows.Forms.ColumnHeader();
            this.PayeeColumn = new System.Windows.Forms.ColumnHeader();
            this.PurposeColumn = new System.Windows.Forms.ColumnHeader();
            this.SavedColumn = new System.Windows.Forms.ColumnHeader();
            this.PurposeEdit = new System.Windows.Forms.TextBox();
            this.NameEdit = new System.Windows.Forms.TextBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusBar
            // 
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status});
            this.StatusBar.Location = new System.Drawing.Point(0, 428);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(930, 22);
            this.StatusBar.TabIndex = 0;
            this.StatusBar.Text = "statusStrip1";
            // 
            // Status
            // 
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(915, 17);
            this.Status.Spring = true;
            this.Status.Text = "Готово";
            this.Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.SaveAllMenuItem,
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
            // SaveAllMenuItem
            // 
            this.SaveAllMenuItem.Name = "SaveAllMenuItem";
            this.SaveAllMenuItem.Size = new System.Drawing.Size(242, 22);
            this.SaveAllMenuItem.Text = "Сохранить все";
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
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(930, 404);
            this.splitContainer1.SplitterDistance = 138;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 2;
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
            this.FilesList.Location = new System.Drawing.Point(0, 4);
            this.FilesList.MultiSelect = false;
            this.FilesList.Name = "FilesList";
            this.FilesList.Size = new System.Drawing.Size(926, 130);
            this.FilesList.TabIndex = 9;
            this.FilesList.UseCompatibleStateImageBehavior = false;
            this.FilesList.View = System.Windows.Forms.View.Details;
            this.FilesList.SelectedIndexChanged += new System.EventHandler(this.FilesList_SelectedIndexChanged);
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
            // splitContainer2
            // 
            this.splitContainer2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.DocsList);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel2.Controls.Add(this.PurposeEdit);
            this.splitContainer2.Panel2.Controls.Add(this.NameEdit);
            this.splitContainer2.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.splitContainer2.Size = new System.Drawing.Size(926, 259);
            this.splitContainer2.SplitterDistance = 193;
            this.splitContainer2.TabIndex = 3;
            // 
            // DocsList
            // 
            this.DocsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NoColumn,
            this.EDColumn,
            this.SumColumn,
            this.PayerColumn,
            this.CorrColumn,
            this.PayeeColumn,
            this.PurposeColumn,
            this.SavedColumn});
            this.DocsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocsList.FullRowSelect = true;
            this.DocsList.GridLines = true;
            this.DocsList.Location = new System.Drawing.Point(0, 0);
            this.DocsList.Name = "DocsList";
            this.DocsList.Size = new System.Drawing.Size(926, 193);
            this.DocsList.TabIndex = 3;
            this.DocsList.UseCompatibleStateImageBehavior = false;
            this.DocsList.View = System.Windows.Forms.View.Details;
            this.DocsList.SelectedIndexChanged += new System.EventHandler(this.DocsList_SelectedIndexChanged);
            // 
            // NoColumn
            // 
            this.NoColumn.Text = "No";
            this.NoColumn.Width = 80;
            // 
            // EDColumn
            // 
            this.EDColumn.Text = "Тип";
            this.EDColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            // CorrColumn
            // 
            this.CorrColumn.Text = "Подстава";
            this.CorrColumn.Width = 80;
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
            this.SavedColumn.Text = "Выходной файл SWIFT";
            this.SavedColumn.Width = 280;
            // 
            // PurposeEdit
            // 
            this.PurposeEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.PurposeEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PurposeEdit.Location = new System.Drawing.Point(3, 25);
            this.PurposeEdit.Name = "PurposeEdit";
            this.PurposeEdit.Size = new System.Drawing.Size(920, 22);
            this.PurposeEdit.TabIndex = 1;
            this.PurposeEdit.TextChanged += new System.EventHandler(this.PurposeEdit_TextChanged);
            // 
            // NameEdit
            // 
            this.NameEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.NameEdit.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NameEdit.Location = new System.Drawing.Point(3, 3);
            this.NameEdit.Name = "NameEdit";
            this.NameEdit.Size = new System.Drawing.Size(920, 22);
            this.NameEdit.TabIndex = 0;
            this.NameEdit.TextChanged += new System.EventHandler(this.NameEdit_TextChanged);
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
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
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
    private ToolStripMenuItem FontMenuItem;
    private FontDialog FontDialog;
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
    private PrintPreviewDialog PrintPreviewDialog;
    private System.Drawing.Printing.PrintDocument PrintDocument;
    private PrintDialog PrintDialog;
    private ToolStripStatusLabel Status;
    private ListView FilesList;
    private ColumnHeader FileColumn;
    private ColumnHeader RootColumn;
    private ColumnHeader TotalQtyColumn;
    private ColumnHeader TotalSumColumn;
    private ColumnHeader PackSavedColumn;
    private ToolStripMenuItem SaveAllMenuItem;
    private SplitContainer splitContainer2;
    private ListView DocsList;
    private ColumnHeader NoColumn;
    private ColumnHeader EDColumn;
    private ColumnHeader SumColumn;
    private ColumnHeader PayerColumn;
    private ColumnHeader CorrColumn;
    private ColumnHeader PayeeColumn;
    private ColumnHeader PurposeColumn;
    private ColumnHeader SavedColumn;
    private TextBox PurposeEdit;
    private TextBox NameEdit;
}
