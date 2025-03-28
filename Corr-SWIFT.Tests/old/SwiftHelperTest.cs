﻿//using static CorrLib.SwiftHelper;

//using Microsoft.VisualStudio.TestTools.UnitTesting;

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Corr_SWIFT.Tests.old;

//[TestClass]
//internal class SwiftHelperTest
//{
//    private string _text =
//@"{1:F01CITVRU2PAXXX0000036558}{2:I103AVTBRUMMXXXXN}{3:{113:RUR6}{121:de302129-4181-49b0-8053-0546a8c0af58}}{4:
//:20:+220428-0853F103
//:23B:CRED
//:32A:220428RUB2000,
//:50K:/40703810000000001470
//INN7803018248.KPP784101001
//ASSOCIACIa VEKTOR ROGA I KOPYTA TOV
//ARIqESTVO SOBSTVENNIKOV JILXa I eKS
//PLUATANTOV PODZEMNYH I VODNYH ARTER
//:52A:CITVRU2P
//:53B:/D/30109810800010001378
//:57D://RU044030653.30101810500000000653
//SEVERO-ZAPADNYi BANK PAO SBERBANK
//G. SANKT-PETERBURG
//:59:/40817810555004092061
//INN780105207066.KPP000000000
//MEJOV ALEKSEi ALEKSANDROVIc PREDSTA
//VITELX RAZUMNOi JIZNI NA PLANETE ZE
//MLa I SPUINIKAH uPITERA I DRUGIH NA
//:70:DLa ZAcISLENIa NA ScET MEJOVA ALEKS
//Ea ALEKSANDROVIcA I ZARABOTNAa PLAT
//A ZA MAi 2022 G. ZA VYPOLNENNUu RAB
//OTU PO PEREKLADKE TRUB I VYKAPYVANI
//:71A:OUR
//:72:/RPP/31.220428.3
///DAS/220428.220427.000000.000000
///NZP/u I ZAKAPYVANIu KANAV V GRUNT
////E NA GORE. SUMMA 2000-00 BEZ NALO
////GA (NDS)
//-}{5:}";

//    private string _text2 =
//@"{1:F01CITVRU2PAXXX0000036558}{2:I103AVTBRUMMXXXXN}{3:{113:RUR6}{121:de302129-4181-49b0-8053-0546a8c0af58}}{4:
//:20:+220428-0853F103
//:23B:CRED
//:32A:220428RUB2000,
//:50K:/40703810000000001470
//INN7803018248
//ASSOCIACIa VEKTOR ROGA I KOPYTA TOV
//ARIqESTVO SOBSTVENNIKOV JILXa I eKS
//PLUATANTOV PODZEMNYH I VODNYH ARTER
//:52A:CITVRU2P";

//    [TestMethod]
//    internal void GetSectionTest()
//    {
//        string expected =
//            @"/40703810000000001470
//INN7803018248.KPP784101001
//ASSOCIACIa VEKTOR ROGA I KOPYTA TOV
//ARIqESTVO SOBSTVENNIKOV JILXa I eKS
//PLUATANTOV PODZEMNYH I VODNYH ARTER";

//        string test = GetSection(_text, "50K").InnerText;
//        Assert.AreEqual(expected, test);
//    }

//    [TestMethod]
//    internal void GetPayerSectionTest()
//    {
//        var test = GetPayerSection(_text);
//        Assert.AreEqual("40703810000000001470", test.Acc);
//        Assert.AreEqual("780105207066", test.INN);
//        Assert.AreEqual("784101001", test.KPP);
//        Assert.AreEqual(@"ASSOCIACIa VEKTOR ROGA I KOPYTA TOV
//ARIqESTVO SOBSTVENNIKOV JILXa I eKS
//PLUATANTOV PODZEMNYH I VODNYH ARTER", test.Name);
//    }

//    [TestMethod]
//    internal void GetPayerSectionNoKPPTest()
//    {
//        var test = GetPayerSection(_text);
//        Assert.AreEqual("40703810000000001470", test.Acc);
//        Assert.AreEqual("780105207066", test.INN);
//        Assert.AreEqual(null, test.KPP);
//        Assert.AreEqual(@"ASSOCIACIa VEKTOR ROGA I KOPYTA TOV
//ARIqESTVO SOBSTVENNIKOV JILXa I eKS
//PLUATANTOV PODZEMNYH I VODNYH ARTER", test.Name);
//    }
//}
