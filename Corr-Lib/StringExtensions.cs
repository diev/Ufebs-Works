#region License
/*
Copyright 2022-2023 Dmitrii Evdokimov
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

namespace CorrLib;

public static class StringExtensions
{
    public static bool Empty(this string? value)
        => string.IsNullOrWhiteSpace(value);

    public static bool Exists(this string? value)
        => !string.IsNullOrWhiteSpace(value);

    public static string AddNotEmpty(this string? value)
        => value == null
        ? string.Empty
        : $".{value}";

    public static string AddKPPNotEmpty(this string? value)
        => value == null
        ? string.Empty
        : $".KPP{value}";

    public static string AddNotEmptyNorZeros(this string? value)
        => value == null || value.Length == 0 || value == "0" || value == "000000000"
        ? string.Empty
        : $".{value}";

    public static string AddKPPNotEmptyNorZeros(this string? value)
        => value == null || value.Length == 0 || value == "0" || value == "000000000"
        ? string.Empty
        : $".KPP{value}";

    public static StringBuilder AppendLineIf(this StringBuilder @this, bool condition, string value)
        => condition
        ? @this.AppendLine(value)
        : @this;

    public static StringBuilder AppendLineIf(this StringBuilder @this, string? conditional, string value)
        => conditional != null
        ? @this.AppendLine(value)
        : @this;

    public static StringBuilder AppendLineIf(this StringBuilder @this, string? value)
        => value != null
        ? @this.AppendLine(value)
        : @this;
}
