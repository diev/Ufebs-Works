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

using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace CorrLib;

public static class PacketEPDEx
{
    public static void Load(this PacketEPD packet, string path)
    {
        var xdoc = XDocument.Load(path);
        var root = xdoc.Root;

        if (root is not null)
        {
            packet.Load(root);
        }
    }

    public static void Load(this PacketEPD packet, XElement root)
    {
        packet.EDType = root.Name.LocalName;
        var ns = root.GetDefaultNamespace();

        if (packet.EDType.StartsWith("ED1"))
        {
            packet.EDAuthor = root.Attribute("EDAuthor")!.Value;
            packet.EDDate = root.Attribute("EDDate")!.Value;
            packet.EDNo = root.Attribute("EDNo")!.Value;
            packet.EDQuantity = "1";
            packet.EDReceiver = root.Attribute("EDReceiver")?.Value;
            packet.Sum = root.Attribute("Sum")!.Value;
            packet.SystemCode = root.Attribute("SystemCode")!.Value;
            packet.Xmlns = root.Attribute("xmlns")?.Value;

            packet.Docs = new ED100[1];
            packet.Docs[0] = new ED100(root);
        }
        else
        {
            switch (packet.EDType)
            {
                case "PacketEPD":
                    packet.EDAuthor = root.Attribute("EDAuthor")!.Value;
                    packet.EDDate = root.Attribute("EDDate")!.Value;
                    packet.EDNo = root.Attribute("EDNo")!.Value;
                    packet.EDQuantity = root.Attribute("EDQuantity")!.Value;
                    packet.EDReceiver = root.Attribute("EDReceiver")?.Value;
                    packet.Sum = root.Attribute("Sum")!.Value;
                    packet.SystemCode = root.Attribute("SystemCode")!.Value;
                    packet.Xmlns = root.Attribute("xmlns")?.Value;

                    int qty = int.Parse(packet.EDQuantity);
                    packet.Docs = new ED100[qty];
                    var node = root.FirstNode;

                    for (int i = 0; i < qty; i++)
                    {
                        packet.Docs[i] = new ED100(node);
                        node = node?.NextNode;
                    }

                    break;

                case "ED503":
                    packet.EDAuthor = root.Attribute("EDAuthor")!.Value;
                    packet.EDDate = root.Attribute("EDDate")!.Value;
                    packet.EDNo = root.Attribute("EDNo")!.Value;
                    packet.EDQuantity = "1";
                    packet.EDReceiver = root.Attribute("ActualReceiver")?.Value;
                    //packet.Sum = root.Attribute("Sum")?.Value;
                    //packet.SystemCode = root.Attribute("SystemCode")?.Value;
                    packet.Xmlns = root.Attribute("xmlns")?.Value;

                    packet.Docs = Array.Empty<ED100>();
                    //packet.Docs[0] = new ED100(root.FirstNode);

                    var container = root.Element(ns + "SWIFTContainer");
                    var document = container?.Element(ns + "SWIFTDocument");
                    string? value = document?.Value;

                    if (value != null)
                    {
                        byte[] bytes = Convert.FromBase64String(value);
                        string swiftText = Encoding.ASCII.GetString(bytes);

                        //TODO swiftText
                    }

                    break;

                default:
                    break;
            }
        }
    }

    public static string[] RowData(this PacketEPD packet, int index)
    {
        var item = packet.Docs[index];
        var corr = new ED100(item).Corr();

        return new string[]
        {
            item.EDNo,
            item.EDType,
            item.Sum.ESum(),
            item.PayerName ?? string.Empty,
            corr.PayerName ?? string.Empty, //TODO
            item.PayeeName ?? string.Empty,
            corr.Purpose ?? string.Empty,
            string.Empty //TODO File.Exists?
        };
    }

    public static void WriteStartXML(this PacketEPD packet, XmlWriter writer)
    {
        // PacketEPD
        writer.WriteStartElement(packet.EDType, packet.Xmlns);

        writer.WriteAttributeString("EDAuthor", packet.EDAuthor);
        writer.WriteAttributeString("EDDate", packet.EDDate);
        writer.WriteAttributeString("EDNo", packet.EDNo);
        writer.WriteAttributeString("EDQuantity", packet.EDQuantity);
        writer.WriteAttributeString("EDReceiver", packet.EDReceiver);
        writer.WriteAttributeString("Sum", packet.Sum);
        writer.WriteAttributeString("SystemCode", packet.SystemCode);
        writer.Flush();

        //TODO Write Docs[]
    }
}
