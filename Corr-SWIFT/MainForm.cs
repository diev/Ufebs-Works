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

using static CorrLib.SwiftTranslit;

namespace CorrSWIFT;

public partial class MainForm : Form
{
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

        splitContainer2.SplitterDistance = 
            splitContainer2.Height - NameEdit.Height - PurposeEdit.Height -
            splitContainer2.SplitterWidth * 3;

        // runtimeconfig.template.json > App.runtimeconfig.json

        //// exe G:\BANK\TEST\OUT\r*.xml G:\BANK\TEST\CLI\*_.txt

        //string[] args = Environment.GetCommandLineArgs(); // 0:exe 1:[Input|*] 2:[Output_|\]
        //int argc = args.Length - 1;

        //if (argc > 0) // 1:Input
        //{
        //    string arg = Path.GetFullPath(args[1]);
        //    Config.OpenDir = Path.GetDirectoryName(arg) ?? @"C:\";
        //    Config.OpenMask = Path.GetFileName(arg);

        //    if (argc > 1) // 2:Output
        //    {
        //        arg = Path.GetFullPath(args[2]);
        //        Config.SaveDir = Path.GetDirectoryName(arg) ?? @"C:\";
        //        Config.SaveMask = Path.GetFileName(arg);
        //    }
        //}

        string[] args = Environment.GetCommandLineArgs();
        int argc = args.Length - 1;

        if (argc == 1 && Config.Profiles.Contains(args[1]))
        {
            Config.Profile = args[1];
        }

        Show();

        if (Config.OpenDir.Length == 0)
        {
            MessageBox.Show($"Проверьте настройки!",
                Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            ConfigMenuItem.PerformClick();
        }

        ReInitForm();
    }

    private void ReInitForm()
    {
        Status.Text = $"Profile: {Config.Profile} | Format: {Config.SaveFormat}";

        string mask = Config.SaveMask.Replace("{id}", "*");

        OpenFileDialog.InitialDirectory = Config.OpenDir;
        OpenFileDialog.Filter = $"{Config.UfebsFormat}|{Config.OpenMask}|{OpenFileDialog.Filter}";

        SaveAsFileDialog.InitialDirectory = Config.SaveDir;
        SaveAsFileDialog.Filter = $"{Config.SaveFormat}|{mask}|{SaveAsFileDialog.Filter}";
        SaveAsFileDialog.DefaultExt = Path.GetExtension(mask);

        FilesList.Items.Clear();

        foreach (var file in new SourceFiles(Config.OpenDir, Config.OpenMask))
        {
            FilesList.Items.Add(new ListViewItem(file));
        }

        var saved = Directory.GetFiles(Config.SaveDir, mask);

        if (saved.Length > 0)
        {
            var reply = MessageBox.Show(
                $"В выходной директории\n{Config.SaveDir}\nуже есть {saved.Length} файлов {Config.SaveMask}.\n\nУдалить их?",
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

        foreach (var file in new SourceFiles(OpenFileDialog.FileNames))
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
        //var font = FontDialog.Font;

        //TODO remove?

        //SwiftText.Font = font;
    }
    #endregion Dialogs

    #region Actions

    //private void LoadFile(string path)
    private void LoadFile(ListViewItem item)
    {
        string path = item.Text;

        // Заголовок окна

        Text = $"{Application.ProductName} | {path} | Profile: {Config.Profile} | Format: {Config.SaveFormat}";

        _packet = new PacketEPD(path);
        var docs = new PacketEPDocs(_packet);
        DocsList.Items.Clear();

        //int c = CorrColumn.Index;
        //int p = PurposeColumn.Index;

        foreach (var ed in docs)
        {
            var doc = DocsList.Items.Add(new ListViewItem(ed));
            SaveDocItem(doc); //TODO Cancel* = Abort
            
            //string name = ed[c];
            //string purpose = ed[p];

            //if (name.Length > MAX_NAME) //TODO Test
            //{
            //    MarkItem(item, c);
            //}

            //if (purpose.Length > MAX_PURPOSE) //TODO Test
            //{
            //    MarkItem(item, p);
            //}
        }

        bool ok = true;

        foreach (var ed in _packet.Elements)
        {
            if (!ed.Saved)
            {
                ok = false;
                break;
            }
        }

        if (ok)
        {
            switch (Config.SaveFormat)
            {
                case Config.UfebsFormat:
                    string path2 = Path.Combine(Config.SaveDir, Config.SaveMask.Replace("{id}", _packet.Id));
                    File.WriteAllText(path2, _packet.Sum, Encoding.ASCII); //TODO write corrPacketEPD!!!
                    item.SubItems[PackSavedColumn.Index].Text = path2;
                    break;

                case Config.SwiftFormat:
                    item.SubItems[PackSavedColumn.Index].Text = "+";
                    break;
            }

        }
    }

    private void SaveFile(string? text = null)
    {
        //if (_saveFileName != null)
        //{
        //    //File.WriteAllText(_saveFileName, text ?? SwiftText.Text, Encoding.ASCII); ??????????????????????
        //    MarkFileSaved(); //TODO ????????????????????????????????????????????
        //}
    }

    private void MarkFileSaved()
    {
        //_fileSaved = File.Exists(_saveFileName);
        //var item = FilesList.SelectedItem();

        //if (_fileSaved)
        //{
        //    Status.Text = $"Сохранен в {_saveFileName}";
        //    item.SubItems[PackSavedColumn.Index].Text = _saveFileName;
        //    item.ForeColor = Color.DarkGreen;
        //}
        //else
        //{
        //    Status.Text = $"Ошибка сохранения в {_saveFileName}";
        //    item.SubItems[PackSavedColumn.Index].Text = "<!>";
        //    item.ForeColor = Color.Red;
        //}
    }

    private void MarkDocSaved()
    {
        //_docSaved = File.Exists(_saveDocName);
        //var item = DocsList.SelectedItem();

        //if (_docSaved)
        //{
        //    Status.Text = $"Сохранен в {_saveDocName}";
        //    item.SubItems[SavedColumn.Index].Text = _saveDocName;
        //    item.ForeColor = Color.DarkGreen;
        //}
        //else
        //{
        //    Status.Text = $"Ошибка сохранения в {_saveDocName}";
        //    item.SubItems[SavedColumn.Index].Text = "<!>";
        //    item.ForeColor = Color.Red;
        //}
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
        //switch (Config.SaveFormat)
        //{
        //    case Config.UfebsFormat:
        //        if (!_fileSaved && _saveFileName != null)
        //        {
        //            var reply = MessageBox.Show($"Сохранить файл \n{_saveFileName}\nперед выходом?",
        //                Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

        //            switch (reply)
        //            {
        //                case DialogResult.Yes:
        //                    SaveFile();
        //                    break;

        //                case DialogResult.No:
        //                    _fileSaved = true;
        //                    break;

        //                case DialogResult.Cancel:
        //                    e.Cancel = true;
        //                    break;
        //            }
        //        }
        //        break;

        //    case Config.SwiftFormat: //TODO
        //        if (!_docSaved && _saveDocName != null)
        //        {
        //            var reply = MessageBox.Show($"Сохранить файл \n{_saveDocName}\nперед выходом?",
        //                Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

        //            switch (reply)
        //            {
        //                case DialogResult.Yes:
        //                    SaveFile();
        //                    break;

        //                case DialogResult.No:
        //                    _docSaved = true;
        //                    break;

        //                case DialogResult.Cancel:
        //                    e.Cancel = true;
        //                    break;
        //            }
        //        }
        //        break;
        //}
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
            //_isNameValid &&
            //_isPurposeValid &&
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
        App.About();
    }

    private void PrintMenuItem_Click(object sender, EventArgs e)
    {
        PrintDocument.Print();
    }

    private void PrintPreviewMenuItem_Click(object sender, EventArgs e)
    {
        //PrintDocument.DocumentName = _saveFileName ?? "Файл для печати";
        PrintDocument.DocumentName = "Файл для печати";
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

        //string file = item.Text;
        //LoadFile(file);

        LoadFile(item);
    }

    private void DocsList_SelectedIndexChanged(object sender, EventArgs e)
    {
        var list = (ListView)sender;
        if (list.SelectedItems.Count != 1) return;
        var item = list.SelectedItems[0];

        SaveDocItem(item);
    }

    private void SaveDocItem(ListViewItem item)
    {
        var ed = _packet.Elements[item.Index];

        NameEdit.Text = ed.PayerName;
        PurposeEdit.Text = ed.Purpose;

        switch (Config.SaveFormat)
        {
            case Config.UfebsFormat:
                if (NameEdit.TextLength > 160 || PurposeEdit.TextLength > 210)
                {
                    //CorrectList(item);
                    return;
                }

                //item.SubItems[NoColumn.Index].Text += "+";
                item.SubItems[SavedColumn.Index].Text = "+";
                break;

            case Config.SwiftFormat:
                if (Lat(NameEdit.Text)?.Length > Config.SwiftNameLimit || Lat(PurposeEdit.Text)?.Length > 210)
                {
                    //CorrectList(item);
                    return;
                }

                string path = Path.Combine(Config.SaveDir, Config.SaveMask.Replace("{id}", ed.Id));
                File.WriteAllText(path, ed.ToStringMT103(), Encoding.ASCII);
                item.SubItems[SavedColumn.Index].Text = path;

                //item.SubItems[NoColumn.Index].Text += "+";
                break;
        }

        ed.Saved = true;
    }

    private void CorrectList(ListViewItem item)
    {
        //int i1 = CorrColumn.Index;
        //int i2 = PurposeColumn.Index;

        //string name = item.SubItems[i1].Text;
        //string purpose = item.SubItems[i2].Text;

        //var corrForm = new CorrForm(name, purpose)
        //{
        //    Width = Width
        //};

        //if (corrForm.ShowDialog() == DialogResult.OK)
        //{
        //    string name2 = corrForm.NameResult.Text;
        //    string purpose2 = corrForm.PurposeResult.Text;

        //    if (name == name2 && purpose == purpose2)
        //    {
        //        return;
        //    }

        //    var cursor = Cursor;
        //    Cursor = Cursors.WaitCursor;

        //    foreach (ListViewItem everyItem in DocsList.Items)
        //    {
        //        if (everyItem.SubItems[i1].Text == name)
        //        {
        //            ResetItem(everyItem, i1, name2);
        //            SaveDocItem(everyItem);
        //        }

        //        if (everyItem.SubItems[i2].Text == purpose)
        //        {
        //            ResetItem(everyItem, i2, purpose2);
        //            SaveDocItem(everyItem);
        //        }
        //    }

        //    Cursor = cursor;
        //}
    }

    private static void MarkItem(ListViewItem item, int column)
    {
        item.UseItemStyleForSubItems = false;
        item.SubItems[column].BackColor = Color.LightPink;
    }

    private static void ResetItem(ListViewItem item, int column, string value)
    {
        item.SubItems[column].Text = value;
        item.SubItems[column].ResetStyle();
    }

    private void NameEdit_TextChanged(object sender, EventArgs e)
    {
        EditChanged(sender as TextBox);
    }

    private void PurposeEdit_TextChanged(object sender, EventArgs e)
    {
        EditChanged(sender as TextBox);
    }

    private void EditChanged(TextBox? edit)
    {
        if (edit is null) return;

        bool name = edit.Name == "NameEdit";
        int len, max;

        switch (Config.SaveFormat)
        {
            case Config.UfebsFormat:
                len = edit.TextLength;
                max = name ? 160 : 210;
                edit.BackColor = len > max ? Color.LightPink : BackColor;
                Status.Text = len > max ? $"Надо сократить {len - max}" : $"Готово ({len}/{max})";
                break;

            case Config.SwiftFormat:
                len = Lat(edit.Text).Length;
                max = name ? Config.SwiftNameLimit : 210;
                edit.BackColor = len > max ? Color.LightPink : BackColor;
                Status.Text = len > max ? $"Надо сократить {len - max}" : $"Готово ({len}/{max})";
                break;
        }
    }
}
