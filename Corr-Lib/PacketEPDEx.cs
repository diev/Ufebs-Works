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

using System.Xml;
using System.Xml.Linq;

namespace CorrLib;

public static class PacketEPDEx
{
    public static void Load(this PacketEPD packet, XElement xElement)
    {
        packet.EDType = xElement.Name.LocalName;

        packet.EDAuthor = xElement.Attribute("EDAuthor")?.Value;
        packet.EDDate = xElement.Attribute("EDDate")?.Value;
        packet.EDNo = xElement.Attribute("EDNo")?.Value;
        packet.EDQuantity = xElement.Attribute("EDQuantity")?.Value;
        packet.EDReceiver = xElement.Attribute("EDReceiver")?.Value;
        packet.Sum = xElement.Attribute("Sum")?.Value;
        packet.SystemCode = xElement.Attribute("SystemCode")?.Value;
        packet.Sum = xElement.Attribute("Sum")?.Value;
        packet.Xmlns = xElement.Attribute("xmlns")?.Value;
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
    }
}
