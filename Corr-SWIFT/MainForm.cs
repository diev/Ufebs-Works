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
using CorrLib.SWIFT;
using CorrLib.UFEBS;
using CorrLib.UFEBS.DTO;

using System.Diagnostics;
using System.Text;
using System.Xml;

using static CorrLib.SWIFT.SwiftTranslit;

namespace CorrSWIFT;

public partial class MainForm : Form
{
    private PacketEPD _packet;
    private int _selectedFileIndex = -1;

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
            splitContainer2.Height - PayerEdit.Height - PayeeEdit.Height - PurposeEdit.Height -
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
        ReInitForm();
    }

    private void ReInitForm()
    {
        Text = $"{Application.ProductName}";
        Status.Text = "Инициализация...";
        FormatStatus.Text = $"Format: {Config.SaveFormat}";
        ProfileStatus.Text = $"Profile: {Config.Profile}";
        OpenStatus.Text = $"In: {Config.OpenDir}";
        SaveStatus.Text = $"Out: {Config.SaveDir}";

        string mask = Config.SaveMask
            .Replace("{id}", "*")
            .Replace("{no}", "*");

        FilesList.Items.Clear();
        DocsList.Items.Clear();
        PayerEdit.Text = string.Empty;
        PayeeEdit.Text = string.Empty;
        PurposeEdit.Text = string.Empty;

        _selectedFileIndex = -1;

        if (!Directory.Exists(Config.OpenDir) || !Directory.Exists(Config.SaveDir))
        {
            Status.Text = "Требуется проверить Параметры в меню Файл!";
            return;
        }

        foreach (var file in new SourceFiles(Config.OpenDir, Config.OpenMask))
        {
            FilesList.Items.Add(new ListViewItem(file));
        }

        var saved = Directory.GetFiles(Config.SaveDir == string.Empty
            ? "."
            : Config.SaveDir,
            mask);

        if (saved.Length > 0)
        {
            if (DialogResult.Yes == MessageBox.Show(
                $"В выходной директории\n\"{Config.SaveDir}\"\nуже есть {saved.Length} файлов {Config.SaveMask}.\n\nУдалить их?",
                Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
            {
                foreach (var file in saved)
                {
                    File.Delete(file);
                }
            }
        }

        Status.Text = "Выберите файл.";
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
        PayerEdit.Font = font;
        PayeeEdit.Font = font;
        PurposeEdit.Font = font;

        splitContainer2.SplitterDistance =
            splitContainer2.Height - PayerEdit.Height - PayeeEdit.Height - PurposeEdit.Height -
            splitContainer2.SplitterWidth * 3;
    }
    
    #endregion Dialogs
    #region Actions

    //private void LoadFile(string path)
    private void LoadFile(ListViewItem item)
    {
        string path = item.Text;

        if (!File.Exists(path))
        {
            MessageBox.Show($"Файл \"{path}\" уже отсутствует на диске!",
                Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            return;
        }

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

        SaveFileItem();
    }

    private void TryClose(ref FormClosingEventArgs e)
    {
        if (Status.Text != "Всё готово.")
        {
            var reply = MessageBox.Show("Есть несохраненные файлы!\nЗакрыть программу?",
                Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            switch (reply)
            {
                case DialogResult.OK:
                    break;

                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }

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
        FontDialog.Font = PayerEdit.Font;
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
        var about = new AboutBox();
        about.Show();
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

        if (list.SelectedItems.Count == 1)
        {
            _selectedFileIndex = list.SelectedItems[0].Index;
            var item = list.SelectedItems[0];

            LoadFile(item);
        }
    }

    private void DocsList_SelectedIndexChanged(object sender, EventArgs e)
    {
        var list = (ListView)sender;

        if (list.SelectedItems.Count == 1)
        {
            //_selectedDocIndex = list.SelectedItems[0].Index;
            var item = list.SelectedItems[0];

            SaveDocItem(item);
        }
    }

    private void SaveFileItem()
    {
        var item = FilesList.Items[_selectedFileIndex];

        foreach (var ed in _packet.Elements)
        {
            if (!ed.Saved)
            {
                item.ForeColor = Color.DarkRed;
                Status.Text = "В пакете есть ошибки!";

                return;
            }
        }

        _ = Config.SaveFormat switch
        {
            Config.UfebsFormat => item.SubItems[PackSavedColumn.Index].Text = GetUfebsFileName(),
            Config.SwiftFormat => item.SubItems[PackSavedColumn.Index].Text = "+",
            _ => throw new NotImplementedException()
        };

        item.ForeColor = Color.DarkGreen;
        Status.Text = "Готово. Выберите другой файл.";

        foreach (ListViewItem i in FilesList.Items)
        {
            //if (i.ForeColor != Color.DarkGreen) //TODO see in collection, not in listview!
            if (i.SubItems[PackSavedColumn.Index].Text.Length == 0) // not '+'
            {
                Status.Text = "Выберите следующий необработанный файл.";
                return;
            }
        }

        Status.Text = "Всё готово.";

        string GetUfebsFileName()
        {
            string path = Path.Combine(Config.SaveDir, Config.SaveMask
                .Replace("*", Path.GetFileNameWithoutExtension(_packet.Path))
                .Replace("{id}", _packet.Id)
                .Replace("{no}", _packet.EDNo));

            var settings = new XmlWriterSettings()
            {
                Encoding = Encoding.GetEncoding("windows-1251"),
                Indent = true
            };

            using (var writer = XmlWriter.Create(path, settings))
            {
                if (_packet.EDType == "PacketEPD")
                {
                    _packet.WriteXML(writer);
                }
                else if (_packet.EDType.StartsWith("ED1"))
                {
                    _packet.Elements[0].WriteXML(writer);
                }
                //TODO ED503

                writer.Close();
            }

            return path;
        }
    }

    private void SaveDocItem(ListViewItem item)
    {
        //var item = DocsList.Items[_selectedDocIndex];
        //var ed = _packet.Elements[_selectedDocIndex];

        var ed = _packet.Elements[item.Index];

        PayerEdit.Text = ed.PayerName;
        PayeeEdit.Text = ed.PayeeName;
        PurposeEdit.Text = ed.Purpose;

        switch (Config.SaveFormat)
        {
            case Config.UfebsFormat:
                if (PayerEdit.TextLength > 160 || PurposeEdit.TextLength > 210)
                {
                    item.ForeColor = Color.DarkRed;
                    Status.Text = "Необходимо сократить текст!";
                    return;
                }

                item.SubItems[SavedColumn.Index].Text = "+";
                break;

            case Config.SwiftFormat:
                if (PayerEdit.Text.Lat()?.Length > Config.SwiftNameLimit ||
                    PayeeEdit.Text.Lat()?.Length > Config.SwiftNameLimit ||
                    PurposeEdit.Text.Lat()?.Length > 210)
                {
                    item.ForeColor = Color.DarkRed;
                    Status.Text = "Необходимо сократить текст!";
                    return;
                }

                string path = Path.Combine(Config.SaveDir, Config.SaveMask
                    .Replace("*", Path.GetFileNameWithoutExtension(_packet.Path))
                    .Replace("{id}", SwiftID.Id(ed))
                    .Replace("{no}", ed.EDNo));

                File.WriteAllText(path, ed.ToStringMT103(Config.BankSWIFT, Config.CorrSWIFT, Config.CorrAccount), Encoding.ASCII);
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

        bool name = edit.Name != nameof(PurposeEdit);
        int len, max;

        switch (Config.SaveFormat)
        {
            case Config.UfebsFormat:
                len = edit.TextLength;
                max = name
                    ? 160
                    : 210;
                edit.BackColor = len > max 
                    ? Color.LightPink 
                    : BackColor;
                Status.Text = len > max 
                    ? $"Надо сократить {len - max}" 
                    : $"Готово ({len}/{max})";
                break;

            case Config.SwiftFormat:
                string lat = edit.Text.Lat() ?? string.Empty;
                len = lat.Length;
                max = name
                    ? Config.SwiftNameLimit
                    : 210;
                edit.BackColor = len > max
                    ? Color.LightPink
                    : BackColor;
                Status.Text = len > max 
                    ? $"Надо сократить {len - max}" 
                    : $"Готово ({len}/{max})";
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
        if (DocsList.SelectedItems.Count == 1)
        {
            var item = DocsList.SelectedItems[0];
            var ed = _packet.Elements[item.Index];

            var edit = sender as TextBox;

            if (e.KeyCode == Keys.Enter)
            {
                string name = edit!.Text;

                if (edit.Name == nameof(PayerEdit))
                {
                    string? prev = ed.PayerName;
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
                }
                else if (edit.Name == nameof(PayeeEdit))
                {
                    string? prev = ed.PayeeName;
                    ed.PayeeName = name;
                    item.SubItems[PayeeColumn.Index].Text = name;

                    SaveDocItem(item);

                    foreach (ListViewItem doc in DocsList.Items)
                    {
                        ed = _packet.Elements[doc.Index];

                        if (!ed.Saved && ed.PayeeName == prev)
                        {
                            ed.PayeeName = name;
                            doc.SubItems[PayeeColumn.Index].Text = name;

                            SaveDocItem(doc);
                        }
                    }
                }

                SaveFileItem();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                edit!.Text = edit.Name == nameof(PayerEdit)
                    ? ed.PayerName
                    : ed.PayeeName;
            }
        }
    }

    private void PurposeEdit_KeyUp(object sender, KeyEventArgs e)
    {
        if (DocsList.SelectedItems.Count == 1)
        {
            var item = DocsList.SelectedItems[0];
            var ed = _packet.Elements[item.Index];

            if (e.KeyCode == Keys.Enter)
            {
                string purpose = PurposeEdit.Text;
                ed.Purpose = purpose;
                item.SubItems[PurposeColumn.Index].Text = purpose;

                SaveDocItem(item);
                SaveFileItem();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                PurposeEdit.Text = ed.Purpose;
            }
        }
    }

    private void FilesList_DoubleClick(object sender, EventArgs e)
    {
        var list = (ListView)sender;

        if (list.SelectedItems.Count == 1)
        {
            //_selectedDocIndex = list.SelectedItems[0].Index;
            var item = list.SelectedItems[0];
            string path = item.SubItems[FileColumn.Index].Text;
            path = Path.Combine(Config.OpenDir, path);

            if (File.Exists(path))
            {
                Process.Start("notepad.exe", path);
            }
        }
    }

    private void DocsList_DoubleClick(object sender, EventArgs e)
    {
        var list = (ListView)sender;

        if (list.SelectedItems.Count == 1)
        {
            //_selectedDocIndex = list.SelectedItems[0].Index;
            var item = list.SelectedItems[0];
            string path = item.SubItems[SavedColumn.Index].Text;
            path = Path.Combine(Config.SaveDir, path);

            if (File.Exists(path))
            {
                Process.Start("notepad.exe", path);
            }
        }
    }
}
