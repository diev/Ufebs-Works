#region License
/*
Copyright 2022-2024 Dmitrii Evdokimov
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

namespace CorrLib.SWIFT;

/// <summary>
/// SWIFT-RUR 6.
/// </summary>
public static class SwiftKvit
{
    /// <summary>
    /// Kvit.
    /// </summary>
    /// <param name="lines"></param>
    /// <returns></returns>
    public static (string swiftid, string date, string time) ToTransTime(string[] lines)
    {
        /*
{1:F21CITVRU2PXXXX0804012157}
{4:{177:2208041611}
{451:0}}

{1:F21CITVRU2PXXXX0125010279}
{4:{177:2401252315}
{451:0}}
        */

        int n = 0;
        string line = lines[n++];
        string swiftid = line.ParseHeaderSwiftId();

        line = lines[n++];
        (string date, string time) = line.ParseHeaderTransTime();

        return (swiftid, date, time);
    }
}
