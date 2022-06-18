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
        Text = $"{Application.ProductName}";
        Status.Text = "Загрузка......";
        FormatStatus.Text = $"Format: {Config.SaveFormat}";
        ProfileStatus.Text = $"Profile: {Config.Profile}";

        string mask = Config.SaveMask.Replace("{id}", "*");

        FilesList.Items.Clear();
        DocsList.Items.Clear();

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

        Status.Text = "Выберите файл";
    }
    #endregion Init

    #region Dialogs
    private void OpenFilesOK()
    {
        FilesList.Items.Clear();
        DocsList.Items.Clear();

        foreach (var file in new SourceFiles(OpenFileDialog.FileNames))
        {
            FilesList.Items.Add(new ListViewItem(file));
        }
    }

    private void FontOK()
    {
        var font = FontDialog.Font;
        NameEdit.Font = font;
        PurposeEdit.Font = font;

        splitContainer2.SplitterDistance =
            splitContainer2.Height - NameEdit.Height - PurposeEdit.Height -
            splitContainer2.SplitterWidth * 3;
    }
    
    #endregion Dialogs
    #region Actions

    //private void LoadFile(string path)
    private void LoadFile(ListViewItem item)
    {
        string path = item.Text;

        // Заголовок окна

        Text = $"{Application.ProductName} {path}";

        _packet = new PacketEPD(path);
        var docs = new PacketEPDocs(_packet);
        DocsList.Items.Clear();

        foreach (var ed in docs)
        {
            var doc = DocsList.Items.Add(new ListViewItem(ed));
            SaveDocItem(doc); //TODO Cancel* = Abort (foreach break)
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
                    string path2 = Path.Combine(Config.SaveDir, Config.SaveMask.Replace("{id}", _packet.Id)); //TODO add "*"
                    File.WriteAllText(path2, _packet.Sum, Encoding.ASCII); //TODO write corrPacketEPD!!! без ошибочных? MessageBox
                    item.SubItems[PackSavedColumn.Index].Text = path2;
                    break;

                case Config.SwiftFormat:
                    item.SubItems[PackSavedColumn.Index].Text = "+";
                    break;
            }

            item.ForeColor = Color.DarkGreen;
            Status.Text = "Готово. Выберите другой файл.";
        }
        else
        {
            item.ForeColor = Color.DarkRed;
            Status.Text = "Есть ошибки!";
        }

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

    #region UI
    private void OpenMenuItem_Click(object sender, EventArgs e)
    {
        OpenFileDialog.InitialDirectory = Config.OpenDir;
        OpenFileDialog.Filter = $"{Config.UfebsFormat}|{Config.OpenMask}|{OpenFileDialog.Filter}";
        OpenFileDialog.ShowDialog();
    }

    private void ExitMenuItem_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void FontMenuItem_Click(object sender, EventArgs e)
    {
        FontDialog.Font = NameEdit.Font;
        FontDialog.ShowDialog();
    }

    private void OpenFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
    {
        OpenFilesOK();
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
                    item.ForeColor = Color.DarkRed;
                    Status.Text = "Необходимо сократить текст!";
                    return;
                }

                item.SubItems[SavedColumn.Index].Text = "+";
                break;

            case Config.SwiftFormat:
                if (Lat(NameEdit.Text)?.Length > Config.SwiftNameLimit || Lat(PurposeEdit.Text)?.Length > 210)
                {
                    item.ForeColor = Color.DarkRed;
                    Status.Text = "Необходимо сократить текст!";
                    return;
                }

                string path = Path.Combine(Config.SaveDir, Config.SaveMask.Replace("{id}", ed.Id));
                File.WriteAllText(path, ed.ToStringMT103(), Encoding.ASCII);
                item.SubItems[SavedColumn.Index].Text = path;
                break;
        }

        ed.Saved = true;
        item.ForeColor = Color.DarkGreen;
        Status.Text = "Готово";
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

    private void NameEdit_Enter(object sender, EventArgs e)
    {
        EditChanged(sender as TextBox);
    }

    private void PurposeEdit_Enter(object sender, EventArgs e)
    {
        EditChanged(sender as TextBox);
    }

    private void NameEdit_KeyUp(object sender, KeyEventArgs e)
    {
        if (DocsList.SelectedItems.Count != 1) return;
        var item = DocsList.SelectedItems[0];
        var ed = _packet.Elements[item.Index];

        if (e.KeyCode == Keys.Enter)
        {
            string prev = ed.PayerName;
            string name = NameEdit.Text;
            ed.PayerName = name;
            item.SubItems[PayerColumn.Index].Text = name;
            SaveDocItem(item);

            foreach (ListViewItem doc in DocsList.Items)
            {
                ed = _packet.Elements[doc.Index];

                if (!ed.Saved && ed.PayerName == prev)
                {
                    ed.PayerName = name;
                    doc.SubItems[PayerColumn.Index].Text = name;
                    SaveDocItem(doc);
                }
            }

            //TODO mark ok in FilesList if all saved
        }
        else if (e.KeyCode == Keys.Escape)
        {
            NameEdit.Text = ed.PayerName;
        }
    }

    private void PurposeEdit_KeyUp(object sender, KeyEventArgs e)
    {
        if (DocsList.SelectedItems.Count != 1) return;
        var item = DocsList.SelectedItems[0];
        var ed = _packet.Elements[item.Index];

        if (e.KeyCode == Keys.Enter)
        {
            string purpose = PurposeEdit.Text;
            ed.Purpose = purpose;
            item.SubItems[PurposeColumn.Index].Text = purpose;
            SaveDocItem(item);
        }
        else if (e.KeyCode == Keys.Escape)
        {
            PurposeEdit.Text = ed.Purpose;
        }
    }
}
