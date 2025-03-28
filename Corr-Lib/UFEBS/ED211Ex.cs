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

using System.Xml;

namespace CorrLib.UFEBS;

public static class ED211Ex
{
    public static void WriteXML(this ED211 packet, XmlWriter writer, bool elements = true)
    {
        writer.WriteStartElement(packet.EDType ?? nameof(ED211), "urn:cbr-ru:ed:v2.0");

        writer.WriteAttributeString("AbstractDate", packet.AbstractDate);
        writer.WriteAttributeString("AbstractKind", packet.AbstractKind);
        writer.WriteAttributeString("Acc", packet.Acc);
        writer.WriteAttributeString("BIC", packet.BIC);

        if (packet.CreditSum != null && packet.CreditSum != "0")
            writer.WriteAttributeString("CreditSum", packet.CreditSum);

        if (packet.DebetSum != null && packet.DebetSum != "0")
            writer.WriteAttributeString("DebetSum", packet.DebetSum);

        writer.WriteAttributeString("EDAuthor", packet.EDAuthor);
        writer.WriteAttributeString("EDDate", packet.EDDate);
        writer.WriteAttributeString("EDNo", packet.EDNo);

        if (packet.EDReceiver != null)
            writer.WriteAttributeString("EDReceiver", packet.EDReceiver);

        writer.WriteAttributeString("EndTime", packet.EndTime);

        if (packet.EnterBal != null)
            writer.WriteAttributeString("EnterBal", packet.EnterBal);

        if (packet.LastMovetDate != null)
            writer.WriteAttributeString("LastMovetDate", packet.LastMovetDate);

        writer.WriteAttributeString("OutBal", packet.OutBal);

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
