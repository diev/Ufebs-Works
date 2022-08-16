
using CorrLib.SWIFT;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using static CorrLib.SWIFT.SwiftParsers;

namespace Corr_SWIFT.Tests;

[TestClass]
public class SwiftHelpersTest
{
    [TestMethod]
    public void SwiftDateTest()
    {
        var date = "2022-08-04";
        var expected = "220804";

        var result = date.ToSwiftDate();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void SwiftDateNullTest()
    {
        string? date = null;
        string? expected = null;

        var result = date.ToSwiftDate();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void SwiftSumDoneTest()
    {
        var sum = "130,";
        var expected = "130,";

        var result = sum.ToSwiftSum();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void SwiftSum0Test()
    {
        var sum = "0";
        var expected = "0,";

        var result = sum.ToSwiftSum();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void SwiftSum001Test()
    {
        var sum = "001";
        var expected = "0,01";

        var result = sum.ToSwiftSum();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void SwiftSum010Test()
    {
        var sum = "010";
        var expected = "0,1";

        var result = sum.ToSwiftSum();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void SwiftSum100Test()
    {
        var sum = "100";
        var expected = "1,";

        var result = sum.ToSwiftSum();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void SwiftSum120Test()
    {
        var sum = "120";
        var expected = "1,2";

        var result = sum.ToSwiftSum();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void SwiftSumTest()
    {
        var sum = "12000";
        var expected = "120,";

        var result = sum.ToSwiftSum();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void SwiftSum12050Test()
    {
        var sum = "12050";
        var expected = "120,5";

        var result = sum.ToSwiftSum();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void UfebstDateTest()
    {
        var date = "220804";
        var expected = "2022-08-04";

        var result = date.ToUfebsDate();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void UfebsSum0Test()
    {
        var sum = "0,";
        var expected = "0";

        var result = sum.ToUfebsSum();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void UfebsSum001Test()
    {
        var sum = "0,01";
        var expected = "1";

        var result = sum.ToUfebsSum();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void UfebsSum01Test()
    {
        var sum = "0,1";
        var expected = "10";

        var result = sum.ToUfebsSum();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void UfebsSum1Test()
    {
        var sum = "1,";
        var expected = "100";

        var result = sum.ToUfebsSum();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void UfebsSum15Test()
    {
        var sum = "1,5";
        var expected = "150";

        var result = sum.ToUfebsSum();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void UfebsSum130Test()
    {
        var sum = "130,";
        var expected = "13000";

        var result = sum.ToUfebsSum();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void UfebsSum13012Test()
    {
        var sum = "130,12";
        var expected = "13012";

        var result = sum.ToUfebsSum();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void ParseDrawerStatusTest()
    {
        string text = "S08";
        string expected = "08";

        var result = text.ParseDrawerStatus();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void ParseINNKPPTest()
    {
        string text = "INN17831001422.KPP784101001";
        string expectedINN = "17831001422";
        string expectedKPP = "784101001";

        var (INN, KPP) = text.ParseINNKPP();

        Assert.AreEqual(expectedINN, INN);
        Assert.AreEqual(expectedKPP, KPP);
    }

    [TestMethod]
    public void ParseINNKPP0Test()
    {
        string text = "INN17831001422.KPP0";
        string expectedINN = "17831001422";
        string expectedKPP = "0";

        var (INN, KPP) = text.ParseINNKPP();

        Assert.AreEqual(expectedINN, INN);
        Assert.AreEqual(expectedKPP, KPP);
    }

    [TestMethod]
    public void ParseINNTest()
    {
        string text = "INN17831001422";
        string expectedINN = "17831001422";
        string? expectedKPP = null;

        var (INN, KPP) = text.ParseINNKPP();

        Assert.AreEqual(expectedINN, INN);
        Assert.AreEqual(expectedKPP, KPP);
    }

    [TestMethod]
    public void ParseINN0Test()
    {
        string text = "INN0";
        string expectedINN = "0";
        string? expectedKPP = null;

        var (INN, KPP) = text.ParseINNKPP();

        Assert.AreEqual(expectedINN, INN);
        Assert.AreEqual(expectedKPP, KPP);
    }

    [TestMethod]
    public void ParseDateSumTest()
    {
        string text = "220808RUB130,";
        string expectedDate = "220808";
        string expectedSum = "130,";

        var (date, sum) = text.ParseDateSum();

        Assert.AreEqual(expectedDate, date);
        Assert.AreEqual(expectedSum, sum);
    }

    [TestMethod]
    public void UParseDateSumTest()
    {
        string text = "220808RUB130,";
        string expectedDate = "2022-08-08";
        string expectedSum = "13000";

        var (date, sum) = text.UParseDateSum();

        Assert.AreEqual(expectedDate, date);
        Assert.AreEqual(expectedSum, sum);
    }
    [TestMethod]

    public void ParseBICAccTest()
    {
        string text = "//RU044030702.30101810600000000702";
        string expectedBIC = "044030702";
        string expectedAcc = "30101810600000000702";

        var (BIC, Acc) = text.ParseBICAcc();

        Assert.AreEqual(expectedBIC, BIC);
        Assert.AreEqual(expectedAcc, Acc);
    }

    [TestMethod]
    public void ParseBICTest()
    {
        string text = "//RU044030702";
        string expectedBIC = "044030702";
        string? expectedAcc = null;

        var (BIC, Acc) = text.ParseBICAcc();

        Assert.AreEqual(expectedBIC, BIC);
        Assert.AreEqual(expectedAcc, Acc);
    }
}
