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
        //e.TransTime = "12:34:56"; //TODO ??

        e.EDRefAuthor = ti.EDRefAuthor;
        e.EDRefDate = ti.EDRefDate;
        e.EDRefNo = ti.EDRefNo;

        return e;
    }

    public static void WriteXML(this ED206 ti, XmlWriter writer)
    {
        writer.WriteStartElement(nameof(ED206), "urn:cbr-ru:ed:v2.0");
        writer.WriteAttributeString("Acc", ti.Acc);
        writer.WriteAttributeString("ActualReceiver", ti.ActualReceiver);
        writer.WriteAttributeString("BICCorr", ti.BICCorr);
        writer.WriteAttributeString("CorrAcc", ti.CorrAcc);
        writer.WriteAttributeString("DC", ti.DC);
        writer.WriteAttributeString("EDAuthor", ti.EDAuthor);
        writer.WriteAttributeString("EDDate", ti.EDDate);
        writer.WriteAttributeString("EDNo", ti.EDNo);
        writer.WriteAttributeString("Sum", ti.Sum);
        writer.WriteAttributeString("TransDate", ti.TransDate);
        writer.WriteAttributeString("TransTime", ti.TransTime);

        writer.WriteStartElement("AccDoc");
        writer.WriteAttributeString("AccDocDate", ti.AccDocDate);
        writer.WriteAttributeString("AccDocNo", ti.AccDocNo);
        writer.WriteEndElement(); // AccDoc

        writer.WriteStartElement("EDRefID");
        writer.WriteAttributeString("EDAuthor", ti.EDRefAuthor);
        writer.WriteAttributeString("EDDate", ti.EDRefDate);
        writer.WriteAttributeString("EDNo", ti.EDRefNo);
        writer.WriteEndElement(); // EDRefID
        writer.WriteEndElement(); // ED206
        writer.Flush();
    }
}
