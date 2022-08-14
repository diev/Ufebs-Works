using CorrLib.SWIFT;
using CorrLib;
using CorrLib.UFEBS;
using CorrLib.UFEBS.DTO;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CorrSWIFT;

public static class DocsModel
{
    private static PacketEPD _packet;

    public static void LoadFile(string fileName)
    {
        _packet = new(fileName);
    }

    public static ListViewItem GetViewItem(int index)
    {
        var ed = _packet.Elements[index];
        return new ListViewItem(new String[]
        {
            ed.EDNo,
            ed.EDType,
            ed.Sum.DisplaySum(),
            //ed.OriginalPayerName ?? string.Empty,
            ed.PayerName ?? string.Empty, //TODO
            //ed.OriginalPayeeName ?? string.Empty,
            ed.PayeeName ?? string.Empty,
            ed.Purpose ?? string.Empty,
            string.Empty //TODO File.Exists?
        });
    }

    public static (string payer, string payee, string purpose) GetToUpdate(int index)
    {
        var ed = _packet.Elements[index];
        return (
            ed.PayerName ?? string.Empty,
            ed.PayeeName ?? string.Empty,
            ed.Purpose ?? string.Empty);
    }

    public static void SetUpdated(int index, string payer, string payee, string purpose)
    {
        var ed = _packet.Elements[index];
        ed.PayerName = payer;
        ed.PayeeName = payee;
        ed.Purpose = purpose;
    }

    public static void SaveToFile(int index, string path)
    {
        var ed = _packet.Elements[index];
        string text = ed.ToStringMT103(
            Config.BankSWIFT,
            Config.CorrSWIFT,
            Config.CorrAccount);
        File.WriteAllText(path, text, Encoding.ASCII);
        //item.SubItems[SavedColumn.Index].Text = path;
    }
}
