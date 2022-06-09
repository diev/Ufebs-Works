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

namespace CorrLib;

public static class CorrProperties
{
    public static string CorrAccount { get; set; }
    public static string BankINN { get; set; }
    public static string BankKPP { get; set; }
    public static string CorrPayerTemplate { get; set; }
    public static string CorrPurposeTemplate { get; set; }
    public static int CorrPayerLimit { get; set; }
    public static string BankSWIFT { get; set; }
    public static string CorrSWIFT { get; set; }
}
