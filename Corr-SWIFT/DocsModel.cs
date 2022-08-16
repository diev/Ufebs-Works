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

using System;
using System.Diagnostics;
using System.Text;

namespace CorrSWIFT;

public static class DocsModel
{
    private static string _fileName;
    private static PacketEPD _packet;
    private static PacketEPD _result;
    private static int _index = -1;

    private static int _ok = 0;
    private static int _count = 0;

    public static void LoadFile(MainForm mainForm, string fileName = null)
    {
        if (fileName is null)
        {
            int index = mainForm.FilesList.SelectedIndices.Count == 1
                ? mainForm.FilesList.SelectedIndices[0] : -1;

            if (index == -1)
                return;

            fileName = Path.Combine(Config.OpenDir, mainForm.FilesList.Items[index].Text);
        }

        _fileName = fileName;
        _packet = new(fileName);
        _result = _packet with { };
        _index = -1;
        _ok = 0;
        _count = _result.Elements.Length;

        mainForm.DocsList.Items.Clear();
        mainForm.Status.Text = $"Готово {_ok} из {_count}";

        for (int i = 0; i < _count; i++)
        {
            var ed = _packet.Elements[i];
            ed.PayerName ??= string.Empty;
            ed.PayeeName ??= string.Empty;
            ed.Purpose ??= string.Empty;

            var ced = ed with { };
            _result.Elements[i] = ced.CorrSubst();

            mainForm.DocsList.Items.Add(new ListViewItem(new String[]
            {
                ed.EDNo,
                ed.EDType,
                ed.AccDocNo,
                ed.Sum.DisplaySum(),
                ed.PayerName,
                ed.PayeeName,
                ed.Purpose,
                string.Empty, //TODO File.Exists?

                ced.PayerName,
                ced.PayeeName,
                ced.Purpose,

                ced.PayerName.Lat()!,
                ced.PayeeName.Lat()!,
                ced.Purpose.Lat()!
            }));

            if (ced.PayerName.Lat()!.Length <= 105 &&
                ced.PayeeName.Lat()!.Length <= 105 &&
                ced.Purpose.Lat()!.Length <= 210)
            {
                string text = ced.ToStringMT103(
                    Config.BankSWIFT,
                    Config.CorrSWIFT,
                    Config.CorrAccount);

                string file = Config.SaveMask
                    .Replace("*", Path.GetFileNameWithoutExtension(_packet.Path))
                    .Replace("{id}", SwiftID.Id(ced))
                    .Replace("{no}", ced.EDNo);

                string path = Path.Combine(Config.SaveDir, file);

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

        if (_ok >= _count)
        {
            int index = mainForm.FilesList.SelectedIndices.Count == 1
                ? mainForm.FilesList.SelectedIndices[0] : -1;

            if (index > -1)
            {
                mainForm.FilesList.Items[index].SubItems[mainForm.PackSavedColumn.Index].Text = "+";
                mainForm.FilesList.Items[index].ForeColor = Color.DarkGreen;
            }
        }
    }

    public static void GetToUpdate(MainForm mainForm)
    {
        int index = mainForm.DocsList.SelectedIndices.Count == 1
            ? mainForm.DocsList.SelectedIndices[0] : -1;
        _index = index;

        if (index == -1)
            return;

        var ced = _result.Elements[index];

        EditForm editForm = new()
        {
            Text = $"EDNo=\"{ced.EDNo}\" (Номер {ced.AccDocNo} на {ced.Sum.DisplaySum()})"
        };

        editForm.PayerEdit.Text = ced.PayerName;
        editForm.PayeeEdit.Text = ced.PayeeName;
        editForm.PurposeEdit.Text = ced.Purpose;

        var result = editForm.ShowDialog(mainForm);

        if (result == DialogResult.OK)
        {
            var pred = ced with { };
            var uped = ced with
            {
                PayerName = editForm.PayerEdit.Text,
                PayeeName = editForm.PayeeEdit.Text,
                Purpose = editForm.PurposeEdit.Text
            };

            for (int i = 0; i < _count; i++)
            {
                if (mainForm.DocsList.Items[i].SubItems[mainForm.SavedColumn.Index].Text != string.Empty)
                    continue;

                if (_result.Elements[i].PayerName == pred.PayerName)
                    _result.Elements[i].PayerName = uped.PayerName;

                if (_result.Elements[i].PayeeName == pred.PayeeName)
                    _result.Elements[i].PayeeName = uped.PayeeName;

                if (_result.Elements[i].Purpose == pred.Purpose)
                    _result.Elements[i].Purpose = uped.Purpose;

                if (_result.Elements[i].PayerName.Lat()!.Length <= 105 &&
                    _result.Elements[i].PayeeName.Lat()!.Length <= 105 &&
                    _result.Elements[i].Purpose.Lat()!.Length <= 210)
                {
                    string text = _result.Elements[i].ToStringMT103(
                        Config.BankSWIFT,
                        Config.CorrSWIFT,
                        Config.CorrAccount);

                    string file = Config.SaveMask
                        .Replace("*", Path.GetFileNameWithoutExtension(_packet.Path))
                        .Replace("{id}", SwiftID.Id(ced))
                        .Replace("{no}", ced.EDNo);

                    string path = Path.Combine(Config.SaveDir, file);

                    File.WriteAllText(path, text, Encoding.ASCII);
                    mainForm.DocsList.Items[i].SubItems[mainForm.SavedColumn.Index].Text = file;
                    mainForm.DocsList.Items[i].BackColor = mainForm.BackColor;
                    mainForm.DocsList.Items[i].ForeColor = Color.DarkGreen;
                    mainForm.Status.Text = $"Готово {++_ok} из {_count}";

                    if (_ok >= _count)
                    {
                        index = mainForm.FilesList.SelectedIndices.Count == 1
                            ? mainForm.FilesList.SelectedIndices[0] : -1;

                        if (index > -1)
                        {
                            mainForm.FilesList.Items[index].SubItems[mainForm.PackSavedColumn.Index].Text = "+";
                            mainForm.FilesList.Items[index].ForeColor = Color.DarkGreen;
                        }
                    }
                }
            }
        }
        else if (result == DialogResult.Cancel)
        {
            //mainForm.Status.Text = "Отмена редактирования";
        }
        else
        {
            mainForm.Status.Text = "Закрываемся...";
            mainForm.Close(); //TODO - Exception on closing App
        }
    }

    public static void Start(MainForm mainForm)
    {
        int index = mainForm.DocsList.SelectedIndices.Count == 1
            ? mainForm.DocsList.SelectedIndices[0] : -1;
        _index = index;

        if (index > -1)
        {
            string path = mainForm.DocsList.Items[index].SubItems[mainForm.SavedColumn.Index].Text;
            path = Path.Combine(Config.SaveDir, path);

            if (File.Exists(path))
            {
                Process.Start("notepad.exe", path);
            }
        }
    }
}
