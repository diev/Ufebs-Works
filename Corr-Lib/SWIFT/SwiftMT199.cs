﻿#region License
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

using System.Text;

namespace CorrLib.SWIFT;

/// <summary>
/// SWIFT-RUR 6: MT196, 199, 299.
/// </summary>
public static class SwiftMT199
{
    /// <summary>
    /// MT196, MT199, MT299
    /// </summary>
    /// <param name="lines"></param>
    /// <returns></returns>
    public static string ToString(string[] lines)
    {
        // Text dump

        int n = 0;
        string line = lines[n++];
        StringBuilder sb = new();

        while (!line.StartsWith(":76:") && !line.StartsWith(":79:"))
        {
            sb.AppendLine(line);
            line = lines[n++];
        }

        sb.Append(line[..4]);

        StringBuilder sc = new();
        sc.AppendLine(line[4..]);
        line = lines[n++];

        while (!line.StartsWith("-}"))
        {
            sc.AppendLine(line);
            line = lines[n++];
        }

        sb.Append(sc.Cyr());
        sb.AppendLine(line);

        return sb.ToString();
    }
}
