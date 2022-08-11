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
    const string _class = nameof(CorrBank) + ".";

    public static string? BIC
        => AppContext.GetData(_class + nameof(BIC)) as string;

    public static string? CorrAcc
        => AppContext.GetData(_class + nameof(CorrAcc)) as string;

    public static string? EDAuthor
        => AppContext.GetData(_class + nameof(EDAuthor)) as string;

    public static string? EDReceiver
        => AppContext.GetData(_class + nameof(EDReceiver)) as string;

    public static string? OurAcc
        => AppContext.GetData(_class + nameof(OurAcc)) as string;
}
