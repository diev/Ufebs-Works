#region License
/*
Copyright 2022-2023 Dmitrii Evdokimov
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
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Xml;

namespace CorrSWIFT;

public static class DocsModel
{
    private static readonly Dictionary<string, string> _payers = new();
    private static readonly Dictionary<string, string> _payees = new();
    private static readonly Dictionary<string, string> _purposes = new();

    private static bool _swiftMode;

    private static string? _fileName;
    private static PacketEPD? _packet;
    private static PacketEPD? _result;

    private static int _ok = 0;
    private static int _count = 0;

    public static void LoadFile(MainForm mainForm, string? fileName = null)
    {
        int index = mainForm.FilesList.SelectedIndices.Count == 1
            ? mainForm.FilesList.SelectedIndices[0] : -1;

        if (fileName is null)
        {
            if (index == -1) return;

            fileName = Path.Combine(Config.OpenDir, mainForm.FilesList.Items[index].SubItems[mainForm.FileColumn.Index].Text);
        }

        _swiftMode = Config.SaveFormat == "SWIFT";

        _fileName = fileName;
        _packet = new(fileName);
        _result = _packet with { };
        _ok = 0;
        _count = _result.Elements.Length;

        mainForm.DocsList.Items.Clear();
        mainForm.Status.Text = $"Готово {_ok} из {_count}";

        for (int i = 0; i < _count; i++)
        {
            //_packet.Elements[i].PayerName ??= string.Empty;
            //_packet.Elements[i].PayeeName ??= string.Empty;
            //_packet.Elements[i].Purpose ??= string.Empty;

            var ced = _packet.Elements[i] with { };

            string payer = ced.PayerName!;
            string payee = ced.PayeeName!;
            string purpose = ced.Purpose!;

            if (_swiftMode)
            {
                if (!payer.LatNameValid() && _payers.ContainsKey(payer))
                    ced.PayerName = _payers[payer];

                if (!payee.LatNameValid() && _payees.ContainsKey(payee))
                    ced.PayeeName = _payees[payee];

                if (!purpose.LatNameValid() && _purposes.ContainsKey(purpose))
                    ced.Purpose = _purposes[purpose];

                _result.Elements[i] = ced.CorrSubst();
                var (path, file) = ResultFile(_packet.Elements[i]);

                mainForm.DocsList.Items.Add(new ListViewItem(new String[]
                {
                    (i + 1).ToString(),
                    _packet.Elements[i].EDType,
                    _packet.Elements[i].EDNo,
                    _packet.Elements[i].AccDocNo,
                    _packet.Elements[i].Sum.DisplaySum(),
                    _packet.Elements[i].PayerName!,
                    _packet.Elements[i].PayeeName!,
                    _packet.Elements[i].Purpose!,
                    File.Exists(path) ? file : string.Empty

                    //ced.PayerName,
                    //ced.PayeeName,
                    //ced.Purpose,

                    //ced.PayerName.Lat()!,
                    //ced.PayeeName.Lat()!,
                    //ced.Purpose.Lat()!
                }));

                if (ced.PayerName!.LatNameValid() &&
                    ced.PayeeName!.LatNameValid() &&
                    ced.Purpose!.LatPurposeValid())
                {
                    string text = ced.ToStringMT103(
                        Config.BankSWIFT,
                        Config.CorrSWIFT,
                        Config.CorrAccount);

                    (path, file) = ResultFile(ced);

                    File.WriteAllText(path, text, Encoding.ASCII);
                    mainForm.DocsList.Items[i].SubItems[mainForm.SavedColumn.Index].Text = file;
                    mainForm.DocsList.Items[i].ForeColor = Color.DarkGreen;
                    mainForm.Status.Text = $"Готово {++_ok} из {_count}";
                }
                else
                {
                    mainForm.DocsList.Items[i].BackColor = Color.LightPink;
                }
            }
            else // УФЭБС
            {
                if (payer.Length > 160 && _payers.ContainsKey(payer))
                    ced.PayerName = _payers[payer];

                if (purpose.Length > 210 && _purposes.ContainsKey(purpose))
                    ced.Purpose = _purposes[purpose];

                _result.Elements[i] = ced.CorrSubst();
                bool ok = ced.PayerName!.Length <= 160 && ced.Purpose!.Length <= 210;

                var item = new ListViewItem(new String[]
                {
                    (i + 1).ToString(),
                    _packet.Elements[i].EDType,
                    _packet.Elements[i].EDNo,
                    _packet.Elements[i].AccDocNo,
                    _packet.Elements[i].Sum.DisplaySum(),
                    _packet.Elements[i].PayerName!,
                    _packet.Elements[i].PayeeName!,
                    _packet.Elements[i].Purpose!,
                    ok ? "+" : string.Empty
                });

                if (ok)
                {
                    item.ForeColor = Color.DarkGreen;
                    mainForm.Status.Text = $"Готово {++_ok} из {_count}";
                }
                else
                {
                    item.BackColor = Color.LightPink;
                }

                mainForm.DocsList.Items.Add(item);
            }
        }

        if (_ok < _count) return;

        SavePacketFile(mainForm);
    }

    public static void GetToUpdate(MainForm mainForm)
    {
        int index = mainForm.DocsList.SelectedIndices.Count == 1
            ? mainForm.DocsList.SelectedIndices[0] : -1;

        if (index == -1) return;

        var ced = _result!.Elements[index] with { };

        EditForm editForm = new(_swiftMode)
        {
            Text = $"Файл {Path.GetFileName(_fileName)}, элемент {index + 1} - EDNo=\"{ced.EDNo}\" (Документ номер {ced.AccDocNo} на {ced.Sum.DisplaySum()})"
        };

        editForm.PayerEdit.Text = ced.PayerName;
        editForm.PayeeEdit.Text = ced.PayeeName;
        editForm.PurposeEdit.Text = ced.Purpose;

        var result = editForm.ShowDialog(mainForm);

        if (result == DialogResult.OK)
        {
            string badPayer = ced.PayerName!; // probably bad
            string badPayee = ced.PayeeName!; // probably bad
            string badPurpose = ced.Purpose!; // probably bad

            string updPayer = editForm.PayerEdit.Text!;
            string updPayee = editForm.PayeeEdit.Text!;
            string updPurpose = editForm.PurposeEdit.Text!;

            if (_swiftMode)
            {
                if (!badPayer.LatNameValid() && !_payers.ContainsKey(badPayer)) // exactly bad
                    _payers.Add(badPayer, updPayer);

                if (!badPayee.LatNameValid() && !_payees.ContainsKey(badPayee)) // exactly bad
                    _payees.Add(badPayee, updPayee);

                if (!badPurpose.LatPurposeValid() && !_purposes.ContainsKey(badPurpose)) // exactly bad
                    _purposes.Add(badPurpose, updPurpose);

                for (int i = 0; i < _count; i++)
                {
                    var item = mainForm.DocsList.Items[i];

                    if (item.SubItems[mainForm.SavedColumn.Index].Text != string.Empty)
                        continue; //TODO overwrite optionally

                    ced = _result.Elements[i] with { };

                    if (!ced.PayerName!.LatNameValid() && ced.PayerName == badPayer)
                        _result.Elements[i].PayerName = updPayer;

                    if (!ced.PayeeName!.LatNameValid() && ced.PayeeName == badPayee)
                        _result.Elements[i].PayeeName = updPayee;

                    if (!ced.Purpose!.LatPurposeValid() && ced.Purpose == badPurpose)
                        _result.Elements[i].Purpose = updPurpose;

                    ced = _result.Elements[i] with { };

                    if (ced.PayerName!.LatNameValid() &&
                        ced.PayeeName!.LatNameValid() &&
                        ced.Purpose!.LatPurposeValid())
                    {
                        string text = ced.ToStringMT103(
                            Config.BankSWIFT,
                            Config.CorrSWIFT,
                            Config.CorrAccount);

                        var (path, file) = ResultFile(ced);

                        File.WriteAllText(path, text, Encoding.ASCII);

                        item.SubItems[mainForm.SavedColumn.Index].Text = file;
                        item.BackColor = mainForm.BackColor;
                        item.ForeColor = Color.DarkGreen;
                        //mainForm.DocsList.Refresh();

                        mainForm.Status.Text = $"Готово {++_ok} из {_count}";

                        if (_ok < _count) continue;

                        index = mainForm.FilesList.SelectedIndices.Count == 1
                            ? mainForm.FilesList.SelectedIndices[0] : -1;

                        if (index == -1) continue;

                        item = mainForm.FilesList.Items[index];
                        item.SubItems[mainForm.PackSavedColumn.Index].Text = "+";
                        item.ForeColor = Color.DarkGreen;
                        //mainForm.FilesList.Refresh();
                    }
                }
            }
            else // УФЭБС
            {
                if (badPayer.Length > 160 && !_payers.ContainsKey(badPayer)) // exactly bad
                    _payers.Add(badPayer, updPayer);

                if (badPurpose.Length > 210 && !_purposes.ContainsKey(badPurpose)) // exactly bad
                    _purposes.Add(badPurpose, updPurpose);

                for (int i = 0; i < _count; i++)
                {
                    var item = mainForm.DocsList.Items[i];

                    if (item.SubItems[mainForm.SavedColumn.Index].Text != string.Empty)
                        continue; //TODO overwrite optionally

                    ced = _result.Elements[i] with { };

                    if (ced.PayerName!.Length > 160 && ced.PayerName == badPayer)
                        _result.Elements[i].PayerName = updPayer;

                    if (ced.Purpose!.Length > 210 && ced.Purpose == badPurpose)
                        _result.Elements[i].Purpose = updPurpose;

                    ced = _result.Elements[i] with { };

                    bool ok = ced.PayerName!.Length <= 160 && ced.Purpose!.Length <= 210;

                    if (ok)
                    {
                        item.SubItems[mainForm.SavedColumn.Index].Text = "+";
                        item.BackColor = mainForm.BackColor;
                        item.ForeColor = Color.DarkGreen;
                        //mainForm.DocsList.Refresh();

                        mainForm.Status.Text = $"Готово {++_ok} из {_count}";
                    }
                }

                SavePacketFile(mainForm);
            }
        }
        else if (result == DialogResult.Cancel)
        {
            //mainForm.Status.Text = "Отмена редактирования";
        }
        else
        {
            mainForm.Status.Text = "Закрываемся...";
            Application.Exit();
        }
    }

    private static void SavePacketFile(MainForm mainForm)
    {
        int index = mainForm.FilesList.SelectedIndices.Count == 1
            ? mainForm.FilesList.SelectedIndices[0] : -1;

        if (index == -1) return;

        var item = mainForm.FilesList.Items[index];

        //foreach (var ced in _result.Elements)
        //{
        //    if (!ced.Saved)
        //    {
        //        item.BackColor = Color.LightPink;
        //        mainForm.Status.Text = "В пакете есть ошибки!";

        //        return;
        //    }
        //}

        item.SubItems[mainForm.PackSavedColumn.Index].Text = _swiftMode ? "+" : GetUfebsFileName();
        item.ForeColor = Color.DarkGreen;
        mainForm.Status.Text = "Готово.";

        //foreach (ListViewItem i in mainForm.FilesList.Items)
        //{
        //    //if (i.ForeColor != Color.DarkGreen) //TODO see in collection, not in listview!
        //    if (i.SubItems[mainForm.PackSavedColumn.Index].Text.Length == 0) // not '+'
        //    {
        //        mainForm.Status.Text = "Выберите следующий необработанный файл.";
        //        return;
        //    }
        //}

        mainForm.Status.Text = "Всё готово.";

        static string GetUfebsFileName()
        {
            string file = Config.SaveMask
                .Replace("*", Path.GetFileNameWithoutExtension(_result!.Path)) // _fileName
                .Replace("{id}", _result!.Id)
                .Replace("{no}", _result.EDNo);

            string path = Path.Combine(Config.SaveDir, file);

            var settings = new XmlWriterSettings()
            {
                Encoding = Encoding.GetEncoding("windows-1251"),
                Indent = true
            };

            using (var writer = XmlWriter.Create(path, settings))
            {
                if (_result.EDType == "PacketEPD")
                {
                    _result.WriteXML(writer);
                }
                else if (_result.EDType.StartsWith("ED1"))
                {
                    _result.Elements[0].WriteXML(writer);
                }
                //TODO ED503

                writer.Close();
            }

            return file;
        }
    }

    public static void Start(MainForm mainForm)
    {
        int index = mainForm.DocsList.SelectedIndices.Count == 1
            ? mainForm.DocsList.SelectedIndices[0] : -1;

        if (index == -1) return;

        string path = mainForm.DocsList.Items[index].SubItems[mainForm.SavedColumn.Index].Text;
        path = Path.Combine(Config.SaveDir, path);

        if (!File.Exists(path)) return;

        Process.Start("notepad.exe", path);
    }

    public static (string path, string file) ResultFile(ED100 ced)
    {
        string file = Config.SaveMask
            .Replace("*", Path.GetFileNameWithoutExtension(_packet!.Path))
            .Replace("{id}", SwiftID.Id(ced))
            .Replace("{no}", ced.EDNo);

        string path = Path.Combine(Config.SaveDir, file);

        return (path, file);
    }

    public static void SaveDictionaries()
    {
        string path = Path.GetDirectoryName(Application.ExecutablePath) ?? ".";

        var options = new JsonSerializerOptions {
            //Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true 
        };

        string file = Path.Combine(path, nameof(_payers) + ".json");
        var json = JsonSerializer.SerializeToUtf8Bytes(_payers, options);
        File.WriteAllBytes(file, json);

        file = Path.Combine(path, nameof(_payees) + ".json");
        json = JsonSerializer.SerializeToUtf8Bytes(_payees, options);
        File.WriteAllBytes(file, json);

        file = Path.Combine(path, nameof(_purposes) + ".json");
        json = JsonSerializer.SerializeToUtf8Bytes(_purposes, options);
        File.WriteAllBytes(file, json);
    }
}
