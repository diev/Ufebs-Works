using CorrLib;
using CorrLib.UFEBS;
using CorrLib.UFEBS.DTO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CorrSWIFT;

public static class FilesModel
{
    private static string[] _fileNames;
    private static PacketEPD[] _packets;

    public static void LoadFiles(string[] fileNames)
    {
        _fileNames = fileNames;
    }

    public static ListViewItem GetViewItem(int index)
    {
        //return new ListViewItem(_fileNames[index]);

        var file = _fileNames[index];
        var packet = new PacketEPD(file);

        return new ListViewItem(new string[]
        {
            file,
            packet.EDType,
            packet.EDQuantity,
            packet.Sum.DisplaySum(),
            string.Empty //TODO File.Exists?
        });
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
}
