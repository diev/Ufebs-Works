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

using CorrLib.UFEBS.DTO;

using System.Xml;

namespace CorrLib.UFEBS;

public static class ED206Ex
{
    public static ED206 Load(this ED206 e, ED100 ed, string dc)
    {
        e.AccDocDate = ed.AccDocDate;
        e.AccDocNo = ed.AccDocNo;
        e.BICCorr = ed.PayeeBIC!;
        e.CorrAcc = ed.PayeeCorrespAcc!;
        e.EDAuthor = ed.EDAuthor;
        e.EDDate = ed.EDDate;
        e.EDNo = ed.EDNo;
        e.DC = dc;
        e.Sum = ed.Sum;
        e.TransDate = ed.EDDate;
        //e.TransTime = "12:34:56"; //TODO ??

        return e;
    }

    public static ED206 Load(this ED206 e, TransInfo ti) //TODO !!!
    {
        e.AccDocDate = ti.AccDocDate ?? ti.EDRefDate; //TODO !!!
        e.AccDocNo = ti.AccDocNo;
        e.BICCorr = ti.BICCorr;
        e.CorrAcc = ti.CorrAcc!;
        e.EDAuthor = ti.EDRefAuthor; //TODO !!!
        e.EDDate = ti.EDRefDate; //TODO !!!
        e.EDNo = ti.EDRefNo; //TODO !!!
        e.DC = ti.DC;
        e.Sum = ti.Sum;
        e.TransDate = ti.EDRefDate; //TODO !!!
        e.EDRefDate = ti.EDRefDate;
        e.EDRefNo = ti.EDRefNo;

        return e;
    }

    public static void WriteXML(this ED206 e, XmlWriter writer)
    {
        writer.WriteStartElement(nameof(ED206), "urn:cbr-ru:ed:v2.0");
        writer.WriteAttributeString("Acc", e.Acc);
        writer.WriteAttributeString("ActualReceiver", e.ActualReceiver);
        writer.WriteAttributeString("BICCorr", e.BICCorr);
        writer.WriteAttributeString("CorrAcc", e.CorrAcc);
        writer.WriteAttributeString("DC", e.DC);
        writer.WriteAttributeString("EDAuthor", e.EDAuthor);
        writer.WriteAttributeString("EDDate", e.EDDate);
        writer.WriteAttributeString("EDNo", e.EDNo);
        writer.WriteAttributeString("Sum", e.Sum);
        writer.WriteAttributeString("TransDate", e.TransDate);
        writer.WriteAttributeString("TransTime", e.TransTime);

        writer.WriteStartElement("AccDoc");
        writer.WriteAttributeString("AccDocDate", e.AccDocDate);
        writer.WriteAttributeString("AccDocNo", e.AccDocNo);
        writer.WriteEndElement(); // AccDoc

        writer.WriteStartElement("EDRefID");
        writer.WriteAttributeString("EDAuthor", e.EDRefAuthor);
        writer.WriteAttributeString("EDDate", e.EDRefDate);
        writer.WriteAttributeString("EDNo", e.EDRefNo);
        writer.WriteEndElement(); // EDRefID
        writer.WriteEndElement(); // ED206
        writer.Flush();
    }
}
