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
using CorrLib.UFEBS;
using CorrLib.UFEBS.DTO;

using System.Diagnostics;
using System.Text;
using System.Xml;

namespace CorrSWIFT;

public static class FilesModel
{
    private static string[] _fileNames;
    private static PacketEPD[] _packets;

    public static void AskIfCleanSavedFiles()
    {
        string mask = Config.SaveMask
            .Replace("{id}", "*")
            .Replace("{no}", "*");

        var saved = Directory.GetFiles(Config.SaveDir == string.Empty ? "." : Config.SaveDir, mask);

        if (saved.Length > 0 &&
            DialogResult.Yes == MessageBox.Show(
                $"В выходной директории\n\"{Config.SaveDir}\"\nуже есть {saved.Length} файлов {Config.SaveMask}.\n\nУдалить их?",
                Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
        {
            foreach (var file in saved)
            {
                File.Delete(file);
            }
        }
    }

    public static void LoadFiles(string directory, string mask, ref ListView list)
    {
        var fileNames = Directory.GetFiles(directory, mask);
        LoadFiles(fileNames, ref list);
    }
        
    public static void LoadFiles(string[] fileNames, ref ListView list)
    {
        _fileNames = fileNames;
        list.Items.Clear();
        int i = 0;

        foreach (var file in fileNames)
        {
            var packet = new PacketEPD(file);
            list.Items.Add(new ListViewItem(new string[]
            {
                (++i).ToString(),
                Path.GetFileName(file),
                packet.EDType,
                packet.EDQuantity,
                packet.Sum.DisplaySum(),
                string.Empty //TODO File.Exists?
            }));
        }
    }

    public static void SaveToFile(int index, string path)
    {
        var packet = _packets[index];

        var settings = new XmlWriterSettings()
        {
            Encoding = Encoding.GetEncoding("windows-1251"),
            Indent = true
        };

        using var writer = XmlWriter.Create(path, settings);

        if (packet.EDType == "PacketEPD")
        {
            packet.WriteXML(writer);
        }
        else if (packet.EDType.StartsWith("ED1"))
        {
            packet.Elements[0].WriteXML(writer);
        }
        //TODO ED503

        writer.Close();

        //item.SubItems[SavedColumn.Index].Text = path;
    }

    public static void Start(MainForm mainForm)
    {
        int index = mainForm.FilesList.SelectedIndices.Count == 1
            ? mainForm.FilesList.SelectedIndices[0] : -1;

        if (index == -1) return;

        string file = mainForm.FilesList.Items[index].SubItems[mainForm.FileColumn.Index].Text;
        string path = Path.Combine(Config.OpenDir, file);

        if (File.Exists(path))
        {
            Process.Start("notepad.exe", path);
        }

        file = mainForm.FilesList.Items[index].SubItems[mainForm.PackSavedColumn.Index].Text;
        path = Path.Combine(Config.SaveDir, file);

        if (File.Exists(path))
        {
            Process.Start("notepad.exe", path);
        }
    }
}
