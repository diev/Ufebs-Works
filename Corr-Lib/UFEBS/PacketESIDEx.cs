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

using CorrLib.UFEBS.DTO;

using System.Xml;

namespace CorrLib.UFEBS;

public static class PacketESIDEx
{
    public static void WriteXML(this PacketESID packet, XmlWriter writer, bool elements = true)
    {
        writer.WriteStartElement(packet.EDType ?? nameof(PacketESID), "urn:cbr-ru:ed:v2.0");
        writer.WriteAttributeString("EDAuthor", packet.EDAuthor);
        writer.WriteAttributeString("EDDate", packet.EDDate);
        writer.WriteAttributeString("EDNo", packet.EDNo);
        writer.WriteAttributeString("EDReceiver", packet.EDReceiver);
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
