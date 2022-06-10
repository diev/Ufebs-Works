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
using System.Xml.Linq;

namespace CorrSWIFT;

public partial class MainForm : Form
{
    private const string VersionDate = "2022-06-10";

    // private const int MAX_NAME = 3 * 35; // 105 (SWIFT-RUR) или 160 (УФЭБС)?
    private const int MAX_PURPOSE = 210;

    private bool _isNameValid = false;
    private bool _isPurposeValid = false;

    private bool _saved = false;
    private string? _saveFileName;
    private string _saveMaskName = "*";

    private List<ED100> _docs;

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
            ConfigProperties.OpenDir = Path.GetDirectoryName(arg) ?? @"C:\";
            ConfigProperties.OpenMask = Path.GetFileName(arg);

            if (argc > 1) // 2:Output
            {
                arg = Path.GetFullPath(args[2]);
                ConfigProperties.SaveDir = Path.GetDirectoryName(arg) ?? @"C:\";
                ConfigProperties.SaveMask = Path.GetFileName(arg);
            }
        }

        ReInitForm();
    }

    private void ReInitForm()
    {
        string err = ConfigProperties.Validate();

        if (err.Length > 0)
        {
            MessageBox.Show($"Проверьте настройки:\n\n{err}", Application.ProductName,
                MessageBoxButtons.OK, MessageBoxIcon.Error);

            ConfigMenuItem.PerformClick();
        }

        OpenFileDialog.InitialDirectory = ConfigProperties.OpenDir;
        OpenFileDialog.Filter = $"УФЭБС|{ConfigProperties.OpenMask}|{OpenFileDialog.Filter}";

        SaveAsFileDialog.InitialDirectory = ConfigProperties.SaveDir;
        SaveAsFileDialog.Filter = $"SWIFT|{ConfigProperties.SaveMask}|{SaveAsFileDialog.Filter}";
        SaveAsFileDialog.DefaultExt = Path.GetExtension(ConfigProperties.SaveMask);

        _saveMaskName = Path.GetFileName(ConfigProperties.SaveMask);

        FilesList.Items.Clear();
        //FilesListBox.Items.AddRange(Directory.GetFiles(ConfigProperties.OpenDir, ConfigProperties.OpenMask));

        foreach (var file in new SourceFileCollection(ConfigProperties.OpenDir, ConfigProperties.OpenMask))
        {
            FilesList.Items.Add(new ListViewItem(file));
        }

        var saved = Directory.GetFiles(ConfigProperties.SaveDir, ConfigProperties.SaveMask);

        if (saved.Length > 0)
        {
            var reply = MessageBox.Show(
                $"В выходной директории\n{ConfigProperties.SaveDir}\nуже есть {saved.Length} файлов {ConfigProperties.SaveMask}.\n\nОни будут перезаписаны при сохранениях!\nУдалить их?",
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

        //FilesList.Font = font;
        XmlText.Font = font;
        //DocsList.Font = font;
        SwiftText.Font = font;

        NameEdit.Font = font;
        PurposeEdit.Font = font;
    }
    #endregion Dialogs

    #region Actions

    private bool LoadFile(string path) //TODO remove
    {
        string filename = Path.GetFileNameWithoutExtension(path);
        string ext = Path.GetExtension(path);
        bool problems = false;

        SaveAsFileDialog.FileName = _saveMaskName.Replace("*", filename);
        _saveFileName = Path.Combine(SaveAsFileDialog.InitialDirectory, SaveAsFileDialog.FileName);
        MarkSaved();

        // Заголовок окна

        Text = $"{Application.ProductName} | {path}";

        if (ext.Equals(".txt", StringComparison.OrdinalIgnoreCase))
        {
            //string text = File.ReadAllText(path, Encoding.ASCII);
        }
        else if (ext.Equals(".xml", StringComparison.OrdinalIgnoreCase))
        {
            //XmlTextBox.Text = text; //no 1251!

            var xdoc = XDocument.Load(path);
            var root = xdoc.Root;

            if (root == null)
            {
                XmlText.Text = "Not XML file";
            }
            else
            {
                XmlText.Text = xdoc.ToString();

                //XNamespace ns = root.GetDefaultNamespace();
                //XNode? node;
                //XElement ed;

                switch (root.Name.LocalName)
                {
                    case "ED503": // SWIFT
                        {
                            //string text = File.ReadAllText(path, Encoding.ASCII);
                            //text = SwiftHelper.GetSwiftDocument(text) ?? "No SwiftDocument";


                            //// Исходный текст документа SWIFT

                            //SwiftTextBox.Text = text;
                            //_swift = new SwiftLines(SwiftTextBox.Lines);

                            //if (text is null)
                            //{
                            //    SwiftTextBox.Text = "<SWIFTDocument> не содержит текста.";
                            //}

                            //// Переключаемся на последнюю закладку "К отправке"

                            //Tabs.SelectedIndex = Tabs.TabCount - 1;

                            //// Читаем значения из текста формата документа SWIFT-RUR

                            //var acc = _swift.Account;
                            //var inn = _swift.INN;
                            ////var kpp = _swift.KPP;
                            //var name = _swift.Name;

                            //// Если ИНН плательщика наш (как в Параметрах), то это платеж от нашего Банка самого

                            //bool bank = inn == ConfigProperties.BankINN; // "7831001422";

                            //// Берем из Параметров номер корсчета

                            //string acc2 = ConfigProperties.CorrAccount; // "30109810800010001378";

                            //// Подставляем новые значения плательщика в шаблон, если не Банк сам за себя

                            //// $"АО \"Сити Инвест Банк\" ИНН 7831001422 ({name} р/с {acc})";
                            //string name2 = bank
                            //    ? name
                            //    : ConfigProperties.CorrPayerTemplate
                            //    .Replace("{name}", name)
                            //    .Replace("{acc}", acc);

                            //// Длина не превышает предельную?

                            //_isNameValid = name2.Length <= ConfigProperties.CorrPayerLimit; // MAX_NAME;

                            //// Присваиваем новые значения для генерации нового теста документа

                            //_swift.Account = acc2;
                            //_swift.Name = name2;

                            //// Берем назначение из документа

                            //string purpose = _swift.Purpose;

                            //// Если не Банк сам и есть признак платежа в бюджет

                            //if (!bank && _swift.Tax)
                            //{
                            //    // Подставляем новые значения назначения в шаблон платежа за третье лицо

                            //    // $"//7831001422//784101001//{name}//{purpose}";
                            //    purpose = ConfigProperties.CorrPurposeTemplate
                            //        .Replace("{name}", name)
                            //        .Replace("{purpose}", purpose);

                            //    _swift.Purpose = purpose;
                            //}

                            //// Заполняем текстбоксы

                            //SwiftText.Lines = _swift.Lines; // Новый текст документа SWIFT
                            //NameEdit.Text = name2; // Текст наименования плательщика
                            //PurposeEdit.Text = purpose; // Текст назначения платежа

                            //// Пишем в статусную строку тип платежа

                            //TaxValue.Text = _swift.Tax ? "Бюджет" : "Платеж";
                        }
                        break;

                    case "PacketEPD":
                        //var packet = new PacketEPD(root);

                        //var node = root.FirstNode;
                        //do
                        //{
                        //    var ed = new ED100(node);

                        //    node = node.NextNode;
                        //}
                        //while (node != null);

                        _docs = new();

                        foreach (var node in root.Elements())
                        {
                            var ed = new ED100(node);
                            //DocsList.AddItem(ed);

                            var corr = ed.CorrClone();
                            _docs.Add(corr);
                            DocsList.AddItem(corr);
                        }

                        int count = _docs.Count;
                        DocsPage.Text = $"PacketEPD {count}";
                        DocsDone.Text = count.ToString();
                        DocsDoneBar.Maximum = count;
                        break;

                    case "ED101":
                    case "ED103":
                    case "ED104":
                    case "ED108":
                        var sed = new ED100(root);
                        //DocsList.AddItem(sed);
                        _docs = new();
                        var scorr = sed.CorrClone();
                        _docs.Add(scorr);
                        DocsList.AddItem(scorr);
                        DocsPage.Text = $"PacketEPD 1";
                        DocsDone.Text = "1";
                        DocsDoneBar.Maximum = 1;
                        break;

                    default:
                        break;
                }
            }

            //text = SwiftHelper.GetSwiftDocument(text) ?? "No SwiftDocument";
        }
        else
        {
            XmlText.Text = "No XML file";
        }

        // Выставляем доступность пунктов меню и кнопок

        return CheckItemsEnabled();
    }

    private void SaveFile(string? text = null)
    {
        if (_saveFileName != null)
        {
            File.WriteAllText(_saveFileName, text ?? SwiftText.Text, Encoding.ASCII);
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
        string documentContents = SwiftText.Text;
        string stringToPrint = documentContents;

        if (e.Graphics != null)
        {
            // Sets the value of charactersOnPage to the number of characters
            // of stringToPrint that will fit within the bounds of the page.
            e.Graphics.MeasureString(stringToPrint, Font,
                e.MarginBounds.Size, StringFormat.GenericTypographic,
                out int charactersOnPage, out int linesPerPage);

            // Draws the string within the bounds of the page.
            e.Graphics.DrawString(stringToPrint, Font, Brushes.Black,
            e.MarginBounds, StringFormat.GenericTypographic);

            // Remove the portion of the string that has been printed.
            stringToPrint = stringToPrint[charactersOnPage..];

            // Check to see if more pages are to be printed.
            e.HasMorePages = (stringToPrint.Length > 0);

            // If there are no more pages, reset the string to be printed.
            if (!e.HasMorePages)
            {
                stringToPrint = documentContents;
            }
        }
    }

    private bool CheckItemsEnabled()
    {
        bool enabled = FilesList.PrevEnabled();

        enabled =
            _isNameValid &&
            _isPurposeValid &&
            FilesList.NextEnabled();

        NextMenuItem.Enabled = enabled;
        NextButton.Enabled = enabled;

        FastNextMenuItem.Enabled = enabled;
        FastNextButton.Enabled = enabled;

        return enabled;
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
        FontDialog.Font = SwiftText.Font;
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

    private void WrapMenuItem_Click(object sender, EventArgs e)
    {
        WrapMenuItem.Checked = !WrapMenuItem.Checked;
        XmlText.WordWrap = WrapMenuItem.Checked;
        SwiftText.WordWrap = WrapMenuItem.Checked;
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

    private void NameEdit_TextChanged(object sender, EventArgs e)
    {
        var edit = (TextBox)sender;

        NameSwiftText.Text = edit.Text.LatWrap35();

        int limit = ConfigProperties.CorrPayerLimit; // MAX_NAME;
        int length = edit.TextLength;
        _isNameValid = length <= limit;

        NameLabel.Text = $"Плательщик {length}/{limit}:";
        NameLabel.ForeColor = _isNameValid ? ForeColor : Color.Red;

        CheckItemsEnabled();
    }

    private void PurposeEdit_TextChanged(object sender, EventArgs e)
    {
        var edit = (TextBox)sender;

        PurposeSwiftText.Text = edit.Text.LatWrap35();

        int length = edit.TextLength;
        _isPurposeValid = length <= MAX_PURPOSE;

        PurposeLabel.Text = $"Назначение {length}/{MAX_PURPOSE}:";
        PurposeLabel.ForeColor = _isPurposeValid ? ForeColor : Color.Red;

        CheckItemsEnabled();
    }

    private void FilesList_SelectedIndexChanged(object sender, EventArgs e)
    {
        var list = (ListView)sender;
        if (list.SelectedItems.Count != 1) return;
        var item = list.SelectedItems[0];

        string file = item.Text;
        LoadFile(file);

        int index = item.Index + 1;
        int count = list.Items.Count;

        FilesDoneBar.Value = index;
        FilesDoneBar.Maximum = count;

        string done = $"{index}/{count}";
        FilesPage.Text = $"Файлы {done}";
        FilesDone.Text = done;
    }

    private void DocsList_SelectedIndexChanged(object sender, EventArgs e)
    {
        var list = (ListView)sender;
        if (list.SelectedItems.Count != 1) return;
        var item = list.SelectedItems[0];

        NameEdit.Text = item.SubItems[PayerColumn.Index].Text;
        PurposeEdit.Text = item.SubItems[PurposeColumn.Index].Text;

        var ed = _docs[item.Index];
        SwiftText.Text = ed.ToSWIFT();
        string id = $"{SwiftTranslit.XDate(ed.EDDate)}{ed.EDNo.PadLeft(9, '0')}";
        string path = Path.Combine(ConfigProperties.SaveDir, id + ".swt");
        File.WriteAllText(path, SwiftText.Text, Encoding.ASCII);
        //MarkSaved();
        item.SubItems[SavedColumn.Index].Text = path;

        int index = item.Index + 1;
        int count = list.Items.Count;

        DocsDoneBar.Value = index;
        DocsDoneBar.Maximum = count;

        string done = $"{index}/{count}";
        DocsPage.Text = $"PacketEPD {done}";
        DocsDone.Text = done;
    }
}
