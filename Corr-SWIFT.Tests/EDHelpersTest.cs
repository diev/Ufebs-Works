using CorrLib.UFEBS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Corr_SWIFT.Tests;

[TestClass]
public class EDHelpersTest
{
    [TestMethod]
    public void ESumTest385()
    {
        string test = "385";
        string expected = "3.85";

        string result = test.DisplaySum();

        Assert.AreEqual(expected, result);
    }

    public void ESumTest385K()
    {
        string test = "38500000";
        string expected = "385 000.00";

        string result = test.DisplaySum();

        Assert.AreEqual(expected, result);
    }

    public void ESumTest385M()
    {
        string test = "38500000000";
        string expected = "385 000 000.00";

        string result = test.DisplaySum();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void ESumTest1()
    {
        string test = "1";
        string expected = "0.01";

        string result = test.DisplaySum();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void ESumTest10()
    {
        string test = "10";
        string expected = "0.10";

        string result = test.DisplaySum();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void ESumTest100()
    {
        string test = "100";
        string expected = "1.00";

        string result = test.DisplaySum();

        Assert.AreEqual(expected, result);
    }
}
