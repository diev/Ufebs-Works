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
using System.Xml.Linq;

namespace CorrLib.UFEBS;

public static class ED100Ex
{
    public static void Load(this ED100 ed, string path)
    {
        var xdoc = XDocument.Load(path);
        var root = xdoc.Root;

        if (root is not null)
        {
            if (root.Name.LocalName == "PacketEPD")
            {
                if (root.Attribute("EDQuantity")!.Value != "1")
                {
                    throw new NotSupportedException("Попытка загрузки PacketEPD c EDQuantity более 1.");
                }

                ed.Load(root.FirstNode!); //TODO EDQuantity > 1
            }
            else if (root.Name.LocalName.StartsWith("ED1"))
            {
                ed.Load(root);
            }
            else
            {
                Console.WriteLine($"Неплатежный файл {path}");
            }
        }
    }

    public static ED100 Load(this ED100 ed, XNode node)
    {
        return ed.Load((XElement)node);
    }

    public static ED100 Load(this ED100 ed, XElement x)
    {
        ed.EDType = x.Name.LocalName; // required

        ed.ChargeOffDate = x.Attribute("ChargeOffDate")?.Value; // Нет в ED105
        ed.EDAuthor = x.Attribute("EDAuthor")!.Value; // required
        ed.EDDate = x.Attribute("EDDate")!.Value; // required
        ed.EDNo = x.Attribute("EDNo")!.Value; // required
        ed.FileDate = x.Attribute("FileDate")?.Value;
        ed.OperationID = x.Attribute("OperationID")?.Value;
        ed.PaymentID = x.Attribute("PaymentID")?.Value;
        ed.PaymentPrecedence = x.Attribute("PaymentPrecedence")!.Value; // required
        ed.PaytKind = x.Attribute("PaytKind")?.Value;
        ed.Priority = x.Attribute("Priority")!.Value; // required
        ed.ReceiptDate = x.Attribute("ReceiptDate")?.Value; // Нет в ED105
        ed.ReqSettlementDate = x.Attribute("ReqSettlementDate")?.Value;
        ed.ResField = x.Attribute("ResField")?.Value;
        ed.Sum = x.Attribute("Sum")!.Value; // required
        ed.SystemCode = x.Attribute("SystemCode")!.Value; // required
        ed.TransKind = x.Attribute("TransKind")!.Value; // required

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

        return ed;
    }

    private static string? FixKPP(string? inn, string? kpp)
        => inn != null && inn.Length != 12 &&
        kpp != null && kpp.Length > 0 && kpp != "0" && kpp != "000000000"
        ? kpp
        : null;

    public static void WriteXML(this ED100 ed, XmlWriter writer)
    {
        // ED101

        writer.WriteStartElement(ed.EDType ?? "ED101", "urn:cbr-ru:ed:v2.0");
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
