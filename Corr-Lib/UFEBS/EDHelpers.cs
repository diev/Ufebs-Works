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

namespace CorrLib.UFEBS;

public static class EDHelpers
{
    private static int _EDNo = 1;
    private static string _TimedEDNo = DateTime.Now.ToString("Hmmss");
    private static string _EDDate = DateTime.Today.ToString("yyyy-MM-dd");

    public static string NextEDNo(string? value = null)
    {
        if (value != null)
        {
            _EDNo = int.Parse(value);
        }

        return _EDNo++.ToString();
    }

    public static string NextTimedEDNo()
    {
        while (DateTime.Now.ToString("Hmmss") == _TimedEDNo)
        {
            Thread.Sleep(100);
        }

        _TimedEDNo = DateTime.Now.ToString("Hmmss");

        return _TimedEDNo;
    }

    public static string EDToday(string? value = null)
    {
        if (value != null)
        {
            _EDDate = value;
        }

        return _EDDate;
    }

    public static string DisplaySum(this string value)
    {
        if (value is null || value == "0")
        {
            return string.Empty;
        }

        // "0 123 456 789 012 345.67" (18d УФЭБС в целых копейках)

        //ReadOnlySpan<char> s = value.PadLeft(18);

        //return s.Length switch
        //{
        //    1 => $"0.0{value}",
        //    2 => $"0.{value}",
        //    _ => $"{s[..1]} {s.Slice(1, 3)} {s.Slice(4, 3)} {s.Slice(7, 3)} {s.Slice(10, 3)} {s.Slice(13, 3)}.{s.Slice(16, 2)}".Trim(),
        //};

        // "012345678901,34" 15d SWIFT
        // "01234567890123"  УФЭБС, PadLeft(14)
        // "012 345 678 901,23" (15d SWIFT вместе с запятой после 2 знаков)

        ReadOnlySpan<char> s = value.PadLeft(14);

        return value.Length switch
        {
            1 => $"0.0{value}",
            2 => $"0.{value}",
            _ => $"{s[..3]} {s.Slice(3, 3)} {s.Slice(6, 3)} {s.Slice(9, 3)}.{s.Slice(12, 2)}".Trim(),
        };
    }
}
