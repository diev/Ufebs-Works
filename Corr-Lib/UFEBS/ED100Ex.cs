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

namespace CorrLib.UFEBS;

public static class ED100Ex
{
    public static void Load(this ED100 ed, XElement x)
    {
        ed.EDType = x.Name.LocalName; // required

        ed.ChargeOffDate = x.Attribute("ChargeOffDate")!.Value;
        ed.EDAuthor = x.Attribute("EDAuthor")!.Value; // required
        ed.EDDate = x.Attribute("EDDate")!.Value; // required
        ed.EDNo = x.Attribute("EDNo")!.Value; // required
        ed.FileDate = x.Attribute("FileDate")?.Value;
        ed.OperationID = x.Attribute("OperationID")?.Value;
        ed.PaymentID = x.Attribute("PaymentID")?.Value;
        ed.PaymentPrecedence = x.Attribute("PaymentPrecedence")!.Value; // required
        ed.PaytKind = x.Attribute("PaytKind")?.Value;
        ed.Priority = x.Attribute("Priority")!.Value; // required
        ed.ReceiptDate = x.Attribute("ReceiptDate")!.Value;
        ed.ReqSettlementDate = x.Attribute("ReqSettlementDate")?.Value;
        ed.ResField = x.Attribute("ResField")?.Value;
        ed.Sum = x.Attribute("Sum")!.Value; // required
        ed.SystemCode = x.Attribute("SystemCode")!.Value; // required
        ed.TransKind = x.Attribute("TransKind")!.Value; // required
        ed.Xmlns = x.Attribute("xmlns")?.Value;

        foreach (var e in x.Elements())
        {
            switch (e.Name.LocalName)
            {
                //case "SettleNotEarlier": // Исполнить не ранее, чем
                //    break;

                //case "SettleNotLater": // Исполнить не позднее, чем
                //    break;

                case "AccDoc":
                    ed.AccDocDate = e.Attribute("AccDocDate")!.Value; // required
                    ed.AccDocNo = e.Attribute("AccDocNo")!.Value; // required
                    break;

                case "Payer":
                    ed.PayerINN = e.Attribute("INN")?.Value;
                    ed.PayerKPP = FixKPP(ed.PayerINN, e.Attribute("KPP")?.Value);
                    ed.PayerPersonalAcc = e.Attribute("PersonalAcc")?.Value;

                    foreach (var p in e.Elements())
                    {
                        switch (p.Name.LocalName)
                        {
                            case "Name":
                                ed.PayerName = p.Value.Trim().Replace("  ", " ");
                                break;

                            case "Bank":
                                ed.PayerBIC = p.Attribute("BIC")?.Value;
                                ed.PayerCorrespAcc = p.Attribute("CorrespAcc")?.Value;
                                break;

                            default:
                                break;
                        }
                    }
                    break;

                case "Payee":
                    ed.PayeeINN = e.Attribute("INN")?.Value;
                    ed.PayeeKPP = FixKPP(ed.PayeeINN, e.Attribute("KPP")?.Value);
                    ed.PayeePersonalAcc = e.Attribute("PersonalAcc")?.Value;

                    foreach (var p in e.Elements())
                    {
                        switch (p.Name.LocalName)
                        {
                            case "Name":
                                ed.PayeeName = p.Value.Trim().Replace("  ", " ");
                                break;

                            case "Bank":
                                ed.PayeeBIC = p.Attribute("BIC")?.Value;
                                ed.PayeeCorrespAcc = p.Attribute("CorrespAcc")?.Value;
                                break;

                            default:
                                break;
                        }
                    }
                    break;

                case "Purpose":
                    ed.Purpose = e.Value.Trim().Replace("  ", " ");
                    break;

                case "DepartmentalInfo":
                    ed.CBC = e.Attribute("CBC")?.Value;
                    ed.DocDate = e.Attribute("DocDate")?.Value;
                    ed.DocNo = e.Attribute("DocNo")?.Value;
                    ed.DrawerStatus = e.Attribute("DrawerStatus")?.Value;
                    ed.OKATO = e.Attribute("OKATO")?.Value;
                    ed.PaytReason = e.Attribute("PaytReason")?.Value;
                    ed.TaxPeriod = e.Attribute("TaxPeriod")?.Value;
                    ed.TaxPaytKind = e.Attribute("TaxPaytKind")?.Value;
                    break;

                default:
                    break;
            }
        }
    }

    public static void Load(this ED100 ed, ED100 e)
    {
        ed.EDType = e.EDType;

        ed.ChargeOffDate = e.ChargeOffDate; //71
        ed.CodePurpose = e.CodePurpose; //20
        ed.EDAuthor = e.EDAuthor;
        ed.EDDate = e.EDDate;
        ed.EDNo = e.EDNo;
        ed.EDReceiver = e.EDReceiver;
        ed.FileDate = e.FileDate; //63
        ed.OperationID = e.OperationID;
        ed.PaymentID = e.PaymentID; //22
        ed.PaymentPrecedence = e.PaymentPrecedence;
        ed.PaytKind = e.PaytKind; //5
        ed.Priority = e.Priority; //21
        ed.ReceiptDate = e.ReceiptDate; //62
        ed.ReqSettlementDate = e.ReqSettlementDate;
        ed.ResField = e.ResField; //23
        ed.Sum = e.Sum; //7
        ed.SystemCode = e.SystemCode;
        ed.TransKind = e.TransKind; //18
        ed.Xmlns = e.Xmlns;

        //ed.SettleNotEarlier = e.SettleNotEarlier;
        //ed.SettleNotLater = e.SettleNotLater;

        ed.AccDocDate = e.AccDocDate;
        ed.AccDocNo = e.AccDocNo;

        ed.PayerINN = e.PayerINN;
        ed.PayerKPP = FixKPP(e.PayerINN, e.PayerKPP);
        ed.PayerPersonalAcc = e.PayerPersonalAcc;
        ed.PayerName = e.PayerName;
        ed.PayerBIC = e.PayerBIC;
        ed.PayerCorrespAcc = e.PayerCorrespAcc;

        ed.PayeeINN = e.PayeeINN;
        ed.PayeeKPP = FixKPP(e.PayeeINN, e.PayeeKPP);
        ed.PayeePersonalAcc = e.PayeePersonalAcc;
        ed.PayeeName = e.PayeeName;
        ed.PayeeBIC = e.PayeeBIC;
        ed.PayeeCorrespAcc = e.PayeeCorrespAcc;

        ed.Purpose = e.Purpose;

        if (e.Tax)
        {
            ed.CBC = e.CBC;
            ed.DocDate = e.DocDate;
            ed.DocNo = e.DocNo;
            ed.OKATO = e.OKATO;
            ed.PaytReason = e.PaytReason;
            ed.TaxPeriod = e.TaxPeriod;
            ed.TaxPaytKind = e.TaxPaytKind;
        }
    }

    private static string? FixKPP(string? inn, string? kpp)
        => inn != null && inn.Length != 12 &&
        kpp != null && kpp.Length > 0 && kpp != "0" && kpp != "000000000"
        ? kpp
        : null;

    public static void WriteXML(this ED100 ed, XmlWriter writer)
    {
        // ED101

        writer.WriteStartElement(ed.EDType, ed.Xmlns);
        writer.WriteAttributeString("ChargeOffDate", ed.ChargeOffDate); //71

        if (ed.CodePurpose != null)
            writer.WriteAttributeString("CodePurpose", ed.CodePurpose); //20

        writer.WriteAttributeString("EDAuthor", ed.EDAuthor);
        writer.WriteAttributeString("EDDate", ed.EDDate);
        writer.WriteAttributeString("EDNo", ed.EDNo);

        if (ed.FileDate != null)
            writer.WriteAttributeString("FileDate", ed.FileDate); //63

        if (ed.OperationID != null)
            writer.WriteAttributeString("OperationID", ed.OperationID);

        if (ed.PaymentID != null)
            writer.WriteAttributeString("PaymentID", ed.PaymentID); //22

        writer.WriteAttributeString("PaymentPrecedence", ed.PaymentPrecedence);

        if (ed.PaytKind != null)
            writer.WriteAttributeString("PaytKind", ed.PaytKind); //5

        writer.WriteAttributeString("Priority", ed.Priority); //21
        writer.WriteAttributeString("ReceiptDate", ed.ReceiptDate); //62

        if (ed.ReqSettlementDate != null)
            writer.WriteAttributeString("ReqSettlementDate", ed.ReqSettlementDate); //23

        if (ed.ResField != null)
            writer.WriteAttributeString("ResField", ed.ResField); //23

        writer.WriteAttributeString("Sum", ed.Sum); //7
        writer.WriteAttributeString("SystemCode", ed.SystemCode);
        writer.WriteAttributeString("TransKind", ed.TransKind); //18

        // AccDoc

        writer.WriteStartElement("AccDoc");
        writer.WriteAttributeString("AccDocDate", ed.AccDocDate); //4
        writer.WriteAttributeString("AccDocNo", ed.AccDocNo); //3
        writer.WriteEndElement(); // AccDoc

        // Payer

        writer.WriteStartElement("Payer");
        writer.WriteAttributeString("INN", ed.PayerINN); //60

        if (ed.PayerINN != null && ed.PayerINN.Length != 12)
        {
            writer.WriteAttributeString("KPP", ed.PayerKPP); //102
        }

        writer.WriteAttributeString("PersonalAcc", ed.PayerPersonalAcc);  //9
        writer.WriteElementString("Name", ed.PayerName); //8

        // Bank
        writer.WriteStartElement("Bank");
        writer.WriteAttributeString("BIC", ed.PayerBIC); //11
        writer.WriteAttributeString("CorrespAcc", ed.PayerCorrespAcc); //12
        writer.WriteEndElement(); // Bank
        writer.WriteEndElement(); // Payer

        // Payee

        writer.WriteStartElement("Payee");
        writer.WriteAttributeString("INN", ed.PayeeINN); //61
        writer.WriteAttributeString("KPP", ed.PayeeKPP); //103
        writer.WriteAttributeString("PersonalAcc", ed.PayeePersonalAcc); //17
        writer.WriteElementString("Name", ed.PayeeName); //16

        // Bank
        writer.WriteStartElement("Bank");
        writer.WriteAttributeString("BIC", ed.PayeeBIC); //14
        writer.WriteAttributeString("CorrespAcc", ed.PayeeCorrespAcc); //15
        writer.WriteEndElement(); // Bank
        writer.WriteEndElement(); // Payee

        // Purpose

        writer.WriteElementString("Purpose", ed.Purpose); //24

        if (ed.Tax)
        {
            // DepartmentalInfo
            writer.WriteStartElement("DepartmentalInfo");

            writer.WriteAttributeString("CBC", ed.CBC ?? "0"); //104
            writer.WriteAttributeString("DocDate", ed.DocDate ?? "0"); //109
            writer.WriteAttributeString("DocNo", ed.DocNo ?? "0"); //108
            writer.WriteAttributeString("DrawerStatus", ed.DrawerStatus); //101
            writer.WriteAttributeString("OKATO", ed.OKATO ?? "0"); //105
            writer.WriteAttributeString("PaytReason", ed.PaytReason ?? "0"); //106
            writer.WriteAttributeString("TaxPeriod", ed.TaxPeriod ?? "0"); //107

            if (ed.TaxPaytKind != null && ed.TaxPaytKind != "0") //fix
            {
                writer.WriteAttributeString("TaxPaytKind", ed.TaxPaytKind); //110
            }

            writer.WriteEndElement(); // DepartmentalInfo
        }

        writer.WriteEndElement(); // ED101
        writer.Flush();
    }
}
