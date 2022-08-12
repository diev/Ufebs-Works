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

public static class CorrBank
{
    public static string? Profile { get; set; } = 
        AppContext.GetData(nameof(Profile)) as string;

    //

    public static string? BIC
        => AppContext.GetData(nameof(BIC)) as string;

    public static string? CorrAcc
        => AppContext.GetData(nameof(CorrAcc)) as string;

    public static string? SWIFT
       => AppContext.GetData(nameof(SWIFT)) as string;

    public static string? UIC
        => AppContext.GetData(nameof(UIC)) as string;

    //

    public static string? ProfileBIC
        => AppContext.GetData(Profile + ".BIC") as string;

    public static string? ProfileCorrAcc
        => AppContext.GetData(Profile + ".CorrAcc") as string;

    public static string? ProfilePayAcc
        => AppContext.GetData(Profile + ".PayAcc") as string;

    public static string? ProfileSWIFT
        => AppContext.GetData(Profile + ".SWIFT") as string;

    public static string? ProfileUIC
        => AppContext.GetData(Profile + ".UIC") as string;
}
