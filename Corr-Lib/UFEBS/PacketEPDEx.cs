﻿#region License
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

using CorrLib.UFEBS.DTO;

using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace CorrLib.UFEBS;

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

            packet.Elements = new ED100[1];
            packet.Elements[0] = new ED100(root); //.CorrSubst();
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

                    int qty = int.Parse(packet.EDQuantity);
                    packet.Elements = new ED100[qty];
                    var node = root.FirstNode;

                    for (int i = 0; i < qty; i++)
                    {
                        packet.Elements[i] = new ED100(node!); //.CorrSubst();
                        node = node?.NextNode;
                    }

                    break;

                case "ED503":
                    packet.EDAuthor = root.Attribute("EDAuthor")!.Value;
                    packet.EDDate = root.Attribute("EDDate")!.Value;
                    packet.EDNo = root.Attribute("EDNo")!.Value;
                    packet.EDQuantity = "1";
                    packet.EDReceiver = root.Attribute("ActualReceiver")!.Value;
                    //packet.Sum = root.Attribute("Sum")?.Value;
                    //packet.SystemCode = root.Attribute("SystemCode")?.Value;

                    packet.Elements = Array.Empty<ED100>();
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

    public static string[] DataRow(this PacketEPD packet, int index)
    {
        var item = packet.Elements[index];

        return new string[]
        {
            item.EDNo,
            item.EDType,
            item.Sum.DisplaySum(),
            //item.OriginalPayerName ?? string.Empty,
            item.PayerName ?? string.Empty, //TODO
            //item.OriginalPayeeName ?? string.Empty,
            item.PayeeName ?? string.Empty,
            item.Purpose ?? string.Empty,
            string.Empty //TODO File.Exists?
        };
    }

    public static void WriteXML(this PacketEPD packet, XmlWriter writer, bool elements = true)
    {
        // PacketEPD
        writer.WriteStartElement(packet.EDType ?? nameof(PacketEPD), "urn:cbr-ru:ed:v2.0");

        writer.WriteAttributeString("EDAuthor", packet.EDAuthor);
        writer.WriteAttributeString("EDDate", packet.EDDate);
        writer.WriteAttributeString("EDNo", packet.EDNo);
        writer.WriteAttributeString("EDQuantity", packet.EDQuantity);
        writer.WriteAttributeString("EDReceiver", packet.EDReceiver);
        writer.WriteAttributeString("Sum", packet.Sum);
        writer.WriteAttributeString("SystemCode", packet.SystemCode);
        writer.Flush();

        if (elements)
        {
            foreach (var item in packet.Elements)
            {
                item.WriteXML(writer);
            }
        }
    }
}
