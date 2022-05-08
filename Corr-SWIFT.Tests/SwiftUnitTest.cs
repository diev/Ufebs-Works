
#region License
/*
Copyright 2022 Dmitrii Evdokimov

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

using CorrLib;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Corr_Lib.Tests;

[TestClass]
public class SwiftUnitTest
{
    [TestMethod]
    public void BankTransLat()
    {
        string test = "�� \"���� ������ ����\"";
        string expected = "AO mSITI INVEST BANKm";

        string result = SwiftTranslit.Lat(test);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void BankTransCyr()
    {
        string test = "AO mSITI INVEST BANKm";
        string expected = "�� \"���� ������ ����\"";

        string result = SwiftTranslit.Cyr(test);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void OKeyTransLat()
    {
        string test = "O'Key";
        string expected = "'O'j'Key";

        string result = SwiftTranslit.Lat(test);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void OKeyTransCyr()
    {
        string test = "'O'j'Key";
        string expected = "O'Key";

        string result = SwiftTranslit.Cyr(test);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void TransLatMethod()
    {
        string test = "�������� �Alliance Men ������ (���) N1";
        string expected = "KOMPANIa m'Alliance Men 'VEKTORm (ZAO) 'N1";

        string result = SwiftTranslit.Lat(test);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void TransCyrMethod()
    {
        string test = "KOMPANIa m'Alliance Man'j's 'VEKTORm (ZAO) 'N1";
        string expected = "�������� \"Alliance Man's ������\" (���) N1";

        string result = SwiftTranslit.Cyr(test);

        Assert.AreEqual(expected, result);
    }
}
