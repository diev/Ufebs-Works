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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CorrLib.SWIFT;

namespace CorrLib;

public static class Repository
{
    public static string BasePath { get; set; } = ".";
    public static string InPath => Path.Combine(BasePath, "IN");
    public static string OutPath => Path.Combine(BasePath, "OUT");

    public static string InStorePath(string date) => Path.Combine(BasePath, "IN", date);
    public static string OutStorePath(string date) => Path.Combine(BasePath, "OUT", date);

    public static string? GetOutSwiftFileBySwiftId(string id)
    {
        string date = SwiftID.Id(id).EDDate;
        string path = OutStorePath(date);

        if (!Directory.Exists(path))
            return null;

        foreach (var file in Directory.GetFiles(path, $"*{id}.txt"))
        {
            return file;
        }

        return null;
    }

    public static string? GetOutUfebsFileBySwiftId(string id)
    {
        var swift = GetOutSwiftFileBySwiftId(id);

        if (swift == null)
            return null;

        string path = swift.Replace($"{id}.txt", ".xml");
        return path;
    }


    //public static string MakePath(string path, string date, string ext = "")
    //{
    //    string file = ext == ""
    //        ? Path.GetFileName(path)
    //        : Path.GetFileNameWithoutExtension(path) + ext; // not Extension!

    //    string dir = Path.Combine(Path.GetDirectoryName(path)!, date);

    //    if (!Directory.Exists(dir))
    //    {
    //        Directory.CreateDirectory(dir);
    //    }

    //    string store = Path.Combine(dir, file);
    //    return store;
    //}

    public static string GetInStoreFile(string path, string date, string ext = "")
    {
        string file = ext == ""
            ? Path.GetFileName(path)
            : Path.GetFileNameWithoutExtension(path) + ext;

        string dir = InStorePath(date);

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        string store = Path.Combine(dir, file);
        return store;
    }

    public static string GetOutStoreFile(string path, string date, string ext = "")
    {
        string file = ext == ""
            ? Path.GetFileName(path)
            : Path.GetFileNameWithoutExtension(path) + ext;

        string dir = OutStorePath(date);

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        string store = Path.Combine(dir, file);
        return store;
    }
}
