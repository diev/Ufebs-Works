#region License
/*
Copyright 2022 Dmitrii Evdokimov
Open source software

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
#endregion

using CorrLib;

using System.Drawing.Printing;
using System.Text;

namespace CorrSWIFT;

public partial class MainForm : Form
{
    private const string VersionDate = "2022-06-14";

    // private const int MAX_NAME = 3 * 35; // 105 (SWIFT-RUR) или 160 (УФЭБС)?
    private const int MAX_NAME = 100; //TODO TEST ONLY!!!
    private const int MAX_PURPOSE = 100; //TODO TEST ONLY!!!

    private bool _isNameValid = false;
    private bool _isPurposeValid = false;

    private bool _saved = false;
    private string? _saveFileName;
    private string _saveMaskName = "*";

    private PacketEPD _packet;

    #region Init
    public MainForm()
    {
        InitializeComponent();
        InitForm();
    }

    private void InitForm()
    {
        int w = Screen.PrimaryScreen.WorkingArea.Width;
        int h = Screen.PrimaryScreen.WorkingArea.Height;

        SetBounds(
            (int)(w * 0.1), (int)(h * 0.15),
            (int)(w * 0.8), (int)(h * 0.75));

        // runtimeconfig.template.json > App.runtimeconfig.json

        // exe G:\BANK\TEST\OUT\r*.xml G:\BANK\TEST\CLI\*_.txt

        string[] args = Environment.GetCommandLineArgs(); // 0:exe 1:[Input|*] 2:[Output_|\]
        int argc = args.Length - 1;

        if (argc > 0) // 1:Input
        {
            string arg = Path.GetFullPath(args[1]);
            Config.OpenDir = Path.GetDirectoryName(arg) ?? @"C:\";
            Config.OpenMask = Path.GetFileName(arg);

            if (argc > 1) // 2:Output
            {
                arg = Path.GetFullPath(args[2]);
                Config.SaveDir = Path.GetDirectoryName(arg) ?? @"C:\";
                Config.SaveMask = Path.GetFileName(arg);
            }
        }

        ReInitForm();
    }

    private void ReInitForm()
    {
        string err = Config.Validate();

        if (err.Length > 0)
        {
            MessageBox.Show($"Проверьте настройки:\n\n{err}", Application.ProductName,
                MessageBoxButtons.OK, MessageBoxIcon.Error);

            ConfigMenuItem.PerformClick();
        }

        OpenFileDialog.InitialDirectory = Config.OpenDir;
        OpenFileDialog.Filter = $"УФЭБС|{Config.OpenMask}|{OpenFileDialog.Filter}";

        SaveAsFileDialog.InitialDirectory = Config.SaveDir;
        SaveAsFileDialog.Filter = $"SWIFT|{Config.SaveMask}|{SaveAsFileDialog.Filter}";
        SaveAsFileDialog.DefaultExt = Path.GetExtension(Config.SaveMask);

        _saveMaskName = Path.GetFileName(Config.SaveMask);

        FilesList.Items.Clear();
        //FilesListBox.Items.AddRange(Directory.GetFiles(ConfigProperties.OpenDir, ConfigProperties.OpenMask));

        foreach (var file in new SourceFileCollection(Config.OpenDir, Config.OpenMask))
        {
            FilesList.Items.Add(new ListViewItem(file));
        }

        var saved = Directory.GetFiles(Config.SaveDir, Config.SaveMask);

        if (saved.Length > 0)
        {
            var reply = MessageBox.Show(
                $"В выходной директории\n{Config.SaveDir}\nуже есть {saved.Length} файлов {Config.SaveMask}.\n\nОни будут перезаписаны при сохранениях!\nУдалить их?",
                Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            if (reply == DialogResult.Yes)
            {
                foreach (var file in saved)
                {
                    File.Delete(file);
                }
            }
        }
    }
    #endregion Init

    #region Dialogs
    private void OpenFilesOK()
    {
        FilesList.Items.Clear();
        //FilesListBox.Items.AddRange(OpenFileDialog.FileNames);

        foreach (var file in new SourceFileCollection(OpenFileDialog.FileNames))
        {
            FilesList.Items.Add(new ListViewItem(file));
        }
    }

    private void SaveFileOK()
    {
        //_saveFileName = SaveAsFileDialog.FileName;

        //switch (Tabs.SelectedIndex)
        //{
        //    case 0:
        //        SaveFile(string.Join(Environment.NewLine,
        //            FilesListBox.Items.OfType<string>().ToArray()));
        //        return;

        //    case 1:
        //        SaveFile(XmlTextBox.Text);
        //        break;

        //    case 2:
        //        SaveFile(SwiftTextBox.Text);
        //        break;

        //    case 3:
        //        SaveFile();
        //        break;
        //}

        ////FilesListBox.Items[FilesListBox.SelectedIndex] = _saveFileName;
        ////FilesListBox.SelectedItem = _saveFileName;
        //_saved = true;
        //SavedLabel.Text = _saved ? "Сохранен" : "Не сохранен";
    }

    private void FontOK()
    {
        var font = FontDialog.Font;

        //TODO remove?

        //SwiftText.Font = font;
    }
    #endregion Dialogs

    #region Actions

    private bool LoadFile(string path) //TODO remove
    {
        // Заголовок окна

        Text = $"{Application.ProductName} | {path}";

        _packet = new PacketEPD(path);
        var docs = new PacketEPDocs(_packet);
        DocsList.Items.Clear();

        int c = CorrColumn.Index;
        int p = PurposeColumn.Index;

        foreach (var ed in docs)
        {
            var item = DocsList.Items.Add(new ListViewItem(ed));
            
            string name = ed[c];
            string purpose = ed[p];

            if (name.Length > MAX_NAME)
            {
                item.UseItemStyleForSubItems = false;
                item.SubItems[c].BackColor = Color.LightPink;
            }

            if (purpose.Length > MAX_PURPOSE)
            {
                item.UseItemStyleForSubItems = false;
                item.SubItems[p].BackColor = Color.LightPink;
            }
        }

        return true; //???????????????
    }

    private void SaveFile(string? text = null)
    {
        if (_saveFileName != null)
        {
            //File.WriteAllText(_saveFileName, text ?? SwiftText.Text, Encoding.ASCII); ??????????????????????
            MarkSaved();
        }
    }

    private void MarkSaved()
    {
        _saved = File.Exists(_saveFileName);
        var item = FilesList.SelectedItem();

        if (_saved)
        {
            Status.Text = $"Сохранен в {_saveFileName}";
            item.SubItems[PackSavedColumn.Index].Text = _saveFileName;
            item.ForeColor = Color.DarkGreen;
        }
        else
        {
            Status.Text = $"Ошибка сохранения в {_saveFileName}";
            item.SubItems[PackSavedColumn.Index].Text = "<!>";
            item.ForeColor = Color.Red;
        }
    }

    private void PrintPage(PrintPageEventArgs e)
    {
        //string documentContents = SwiftText.Text; //????????????????????????????????????
        //string stringToPrint = documentContents;

        //if (e.Graphics != null)
        //{
        //    // Sets the value of charactersOnPage to the number of characters
        //    // of stringToPrint that will fit within the bounds of the page.
        //    e.Graphics.MeasureString(stringToPrint, Font,
        //        e.MarginBounds.Size, StringFormat.GenericTypographic,
        //        out int charactersOnPage, out int linesPerPage);

        //    // Draws the string within the bounds of the page.
        //    e.Graphics.DrawString(stringToPrint, Font, Brushes.Black,
        //    e.MarginBounds, StringFormat.GenericTypographic);

        //    // Remove the portion of the string that has been printed.
        //    stringToPrint = stringToPrint[charactersOnPage..];

        //    // Check to see if more pages are to be printed.
        //    e.HasMorePages = (stringToPrint.Length > 0);

        //    // If there are no more pages, reset the string to be printed.
        //    if (!e.HasMorePages)
        //    {
        //        stringToPrint = documentContents;
        //    }
        //}
    }

    private void TryClose(ref FormClosingEventArgs e)
    {
        if (!_saved && _saveFileName != null)
        {
            var reply = MessageBox.Show($"Сохранить файл \n{_saveFileName}\nперед выходом?",
                Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

            switch (reply)
            {
                case DialogResult.Yes:
                    SaveFile();
                    break;

                case DialogResult.No:
                    _saved = true;
                    break;

                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }
    }
    private static void About()
    {
        string config = Path.ChangeExtension(Application.ExecutablePath, "runtimeconfig.json");
        string text =
            $@"Программа дооформления документов из УФЭБС в SWIFT.

Версия {Application.ProductVersion} ({VersionDate})

Задайте параметры в меню Файл\Параметры...
Сохраняются они в файле
{config}

Также пути можно переопределить в командной строке:
    Input\[*.xml] [Output\[*_.txt]]";

        MessageBox.Show(text, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    #endregion Actions

    #region Buttons
    private void GoNext()
    {
        SaveFile();

        if (!FilesList.SelectNext() &&
            MessageBox.Show($"Сделано: {FilesList.Items.Count}.\nЗакрыть программу?", Application.ProductName,
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
        {
            Close();
        }
    }

    private void GoForward()
    {
        while (
            _isNameValid &&
            _isPurposeValid &&
            FilesList.NextEnabled())
        {
            //GoNext(); //зацикливается и не завершает программу!

            SaveFile();

            if (!FilesList.SelectNext() &&
                MessageBox.Show($"Сделано: {FilesList.Items.Count}.\nЗакрыть программу?", Application.ProductName,
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                Close();
                break;
            }
        }
    }
    #endregion Buttons

    #region UI
    private void OpenMenuItem_Click(object sender, EventArgs e)
    {
        OpenFileDialog.ShowDialog();
    }

    private void ExitMenuItem_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void SaveAsMenuItem_Click(object sender, EventArgs e)
    {
        SaveAsFileDialog.ShowDialog();
    }

    private void FontMenuItem_Click(object sender, EventArgs e)
    {
        //FontDialog.Font = SwiftText.Font;?????????????????????????????????????????????
        FontDialog.ShowDialog();
    }

    private void SaveMenuItem_Click(object sender, EventArgs e)
    {
        SaveFile();
    }

    private void OpenFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
    {
        OpenFilesOK();
    }

    private void SaveAsFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
    {
        SaveFileOK();
    }

    private void FontDialog_Apply(object sender, EventArgs e)
    {
        FontOK();
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        TryClose(ref e);
    }

    private void AboutMenuItem_Click(object sender, EventArgs e)
    {
        About();
    }

    private void PrintMenuItem_Click(object sender, EventArgs e)
    {
        PrintDocument.Print();
    }

    private void PrintPreviewMenuItem_Click(object sender, EventArgs e)
    {
        PrintDocument.DocumentName = _saveFileName ?? "Файл для печати";
        PrintPreviewDialog.ShowDialog();
    }

    private void NextMenuItem_Click(object sender, EventArgs e)
    {
        GoNext();
    }

    private void ForwardMenuItem_Click(object sender, EventArgs e)
    {
        GoForward();
    }

    private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {
        PrintPage(e);
    }

    private void ConfigMenuItem_Click(object sender, EventArgs e)
    {
        ConfigForm configForm = new();
        configForm.ShowDialog();

        ReInitForm();
    }

    #endregion UI

    private void FilesList_SelectedIndexChanged(object sender, EventArgs e)
    {
        var list = (ListView)sender;
        if (list.SelectedItems.Count != 1) return;
        var item = list.SelectedItems[0];

        string file = item.Text;
        LoadFile(file);
    }

    private void DocsList_SelectedIndexChanged(object sender, EventArgs e)
    {
        var list = (ListView)sender;
        if (list.SelectedItems.Count != 1) return;
        var item = list.SelectedItems[0];

        var ed = _packet.Docs[item.Index];
        string id = $"{SwiftTranslit.XDate(ed.EDDate)}{ed.EDNo.PadLeft(9, '0')}";
        string path = Path.Combine(Config.SaveDir, id + ".mt103");
        File.WriteAllText(path, ed.ToSWIFT(), Encoding.ASCII);
        item.SubItems[SavedColumn.Index].Text = path;
    }

    private void DocsList_DoubleClick(object sender, EventArgs e)
    {
        var list = (ListView)sender;
        if (list.SelectedItems.Count != 1) return;
        var item = list.SelectedItems[0];

        CorrectList(item);
    }

    private void CorrectList(ListViewItem item)
    {
        int i1 = CorrColumn.Index;
        int i2 = PurposeColumn.Index;

        string name = item.SubItems[i1].Text;
        string purpose = item.SubItems[i2].Text;

        var corrForm = new CorrForm(name, purpose)
        {
            Width = Width
        };

        if (corrForm.ShowDialog() == DialogResult.OK)
        {
            string name2 = corrForm.NameResult.Text;
            string purpose2 = corrForm.PurposeResult.Text;

            if (name == name2 && purpose == purpose2)
            {
                return;
            }

            var cursor = Cursor;
            Cursor = Cursors.WaitCursor;

            foreach (ListViewItem i in DocsList.Items)
            {
                if (i.SubItems[i1].Text == name)
                {
                    i.SubItems[i1].Text = name2;
                    i.SubItems[i1].ResetStyle();
                }

                if (i.SubItems[i2].Text == purpose)
                {
                    i.SubItems[i2].Text = purpose2;
                    i.SubItems[i2].ResetStyle();
                }
            }

            Cursor = cursor;
        }
    }
}
