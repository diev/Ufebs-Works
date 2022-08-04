
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static CorrLib.SWIFT.SwiftTranslit;

namespace Corr_SWIFT.Tests;

[TestClass]
public class SwiftTranslitTest
{
    [TestMethod]
    public void LatTest1()
    {
        var test = "Этот текст DOLjen передаться";
        var expected = "eTOT TEKST 'DOLjen' PEREDATXSa";

        var result = Lat(test);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void LatTest2()
    {
        var test = "LTD PREMIERI AG G.BERLIN//Ул.Правды д.6 г.Москва//";
        var expected = "'LTD PREMIERI AG G.BERLIN'//UL.PRAVDY D.6 G.MOSKVA//";

        var result = Lat(test);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void LatTest3()
    {
        var test = "SENSAI LIMITED//O'NEAL MARKETING ASSOCIATES ROAD TOWN TORTOLA//";
        var expected = "'SENSAI LIMITED//OjNEAL MARKETING ASSOCIATES ROAD TOWN TORTOLA'//";

        var result = Lat(test);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void LatTest4()
    {
        var test = "{VO10010}Оплата за строительные материалы по Договору №82 от 01/05/2013. Без НДС";
        var expected = "'(VO10010)'OPLATA ZA STROITELXNYE MATERIALY PO DOGOVORU n82 OT 01/05/2013. BEZ NDS";

        var result = Lat(test);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void LatTest5()
    {
        var test = "{VO80050}PAYMENT FOR SERVICES INV 52 DD 30/06/2014. WITHOUT VAT";
        var expected = "'(VO80050)''PAYMENT FOR SERVICES INV 52 DD 30/06/2014. WITHOUT VAT'";

        var result = Lat(test);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void LatTest6()
    {
        var test = "//{VO80050PS1}PAYMENT FOR SERVICES INV 52 DD 30/06/2014. WITHOUT VAT";
        var expected = "//'(VO80050PS1)''PAYMENT FOR SERVICES INV 52 DD 30/06/2014. WITHOUT VAT'";

        var result = Lat(test);

        Assert.AreEqual(expected, result);
    }
}
