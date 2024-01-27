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

public static class TransInfoEx
{
    public static Dictionary<string, TransInfo> D { get; set; } = [];
    public static Dictionary<string, TransInfo> C { get; set; } = [];

    public static TransInfo Load(this TransInfo ti, ED100 ed, string dc)
    {
        ti.AccDocNo = ed.AccDocNo;
        ti.BICCorr = dc == "1" ? ed.PayeeBIC! : ed.PayerBIC!;
        ti.EDRefDate = ed.EDDate;
        ti.EDRefNo = ed.EDNo;
        ti.DC = dc;
        ti.PayeePersonalAcc = ed.PayeePersonalAcc;
        ti.PayerPersonalAcc = ed.PayerPersonalAcc!;
        ti.Sum = ed.Sum;
        ti.TransKind = ed.TransKind;
        ti.TurnoverKind = "1";

        #region Extensions

        ti.AccDocDate = ed.AccDocDate;
        ti.CorrAcc = dc == "1" ? ed.PayeeCorrespAcc : ed.PayerCorrespAcc;

        #endregion Extensions

        return ti;
    }

    public static void WriteXML(this TransInfo ti, XmlWriter writer)
    {
        writer.WriteStartElement(nameof(TransInfo), "urn:cbr-ru:ed:v2.0");
        writer.WriteAttributeString("AccDocNo", ti.AccDocNo);
        writer.WriteAttributeString("BICCorr", ti.BICCorr);
        writer.WriteAttributeString("DC", ti.DC);
        writer.WriteAttributeString("PayeePersonalAcc", ti.PayeePersonalAcc);
        writer.WriteAttributeString("PayerPersonalAcc", ti.PayerPersonalAcc);
        writer.WriteAttributeString("Sum", ti.Sum);
        writer.WriteAttributeString("TransKind", ti.TransKind);
        writer.WriteAttributeString("TurnoverKind", ti.TurnoverKind);

        writer.WriteStartElement("EDRefID");
        writer.WriteAttributeString("EDAuthor", ti.EDRefAuthor);
        writer.WriteAttributeString("EDDate", ti.EDRefDate);
        writer.WriteAttributeString("EDNo", ti.EDRefNo);
        writer.WriteEndElement(); // EDRefID

        writer.WriteEndElement(); // TransInfo
        writer.Flush();
    }
}
