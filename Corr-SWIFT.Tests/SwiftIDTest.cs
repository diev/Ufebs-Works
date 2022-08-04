
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static CorrLib.SWIFT.SwiftID;

namespace Corr_SWIFT.Tests;

[TestClass]
public class SwiftIDTest
{
    [TestMethod]
    public void UfebsNumTest()
    {
        var date = "2022-08-04";
        var no = "12345678";
        var expected = "0804345678"; //10x

        var result = Num(date, no);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void SwiftNumTest()
    {
        var date = "220804";
        var no = "12345678";
        var expected = "0804345678"; //10x

        var result = Num(date, no);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void UfebsIdTest()
    {
        var date = "2022-08-04";
        var no = "12345678";
        var expected = "220804012345678"; //15x

        var result = Id(date, no);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void SwiftIdTest()
    {
        var date = "220804";
        var no = "12345678";
        var expected = "220804012345678"; //15x

        var result = Id(date, no);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void NumUfebsTest()
    {
        var date = "2022-08-04";
        var no = "345678";
        var test = "0804345678"; //10x

        var (Date, No) = Num(test);

        Assert.AreEqual(date, Date);
        Assert.AreEqual(no, No);
    }

    [TestMethod]
    public void IdUfebsTest()
    {
        var date = "2022-08-04";
        var no = "12345678";
        var test = "220804012345678"; //15x

        var (Date, No) = Id(test);

        Assert.AreEqual(date, Date);
        Assert.AreEqual(no, No);
    }
}
