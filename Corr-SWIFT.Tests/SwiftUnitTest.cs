using Corr_Lib;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Corr_Lib.Tests;

[TestClass]
public class SwiftUnitTest
{
    [TestMethod]
    public void BankTransLat()
    {
        string test = "ÀÎ \"Ñèòè Èíâåñò Áàíê\"";
        string expected = "AO mSITI INVEST BANKm";

        string result = Swift.Lat(test);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void BankTransCyr()
    {
        string test = "AO mSITI INVEST BANKm";
        string expected = "ÀÎ \"ÑÈÒÈ ÈÍÂÅÑÒ ÁÀÍÊ\"";

        string result = Swift.Cyr(test);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void OKeyTransLat()
    {
        string test = "O'Key";
        string expected = "'O'j'Key";

        string result = Swift.Lat(test);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void OKeyTransCyr()
    {
        string test = "'O'j'Key";
        string expected = "O'Key";

        string result = Swift.Cyr(test);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void TransLatMethod()
    {
        string test = "Êîìïàíèÿ «Alliance Men Âåêòîğ» (ÇÀÎ) N1";
        string expected = "KOMPANIa m'Alliance Men 'VEKTORm (ZAO) 'N1";

        string result = Swift.Lat(test);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void TransCyrMethod()
    {
        string test = "KOMPANIa m'Alliance Man'j's 'VEKTORm (ZAO) 'N1";
        string expected = "ÊÎÌÏÀÍÈß \"Alliance Man's ÂÅÊÒÎĞ\" (ÇÀÎ) N1";

        string result = Swift.Cyr(test);

        Assert.AreEqual(expected, result);
    }
}
