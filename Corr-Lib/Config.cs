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

using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Unicode;

namespace CorrLib;

public static class Config
{
    private static string _profile = 
        (AppContext.GetData(nameof(Profile)) as string ?? string.Empty) + '.'; //for fast access

    // Save.Format
    public const string UfebsFormat = "УФЭБС";
    public const string SwiftFormat = "SWIFT";

    // AppContext.SetData(string name, object? data); // available from .NET 7+
    // See a lifehack at
    // https://www.strathweb.com/2019/12/runtime-host-configuration-options-and-appcontext-data-in-net-core/

    public static string ED807
    {
        get => G(nameof(ED807));
        set => S(nameof(ED807), value);
    }

    public static string Profile
    {
        get => G(nameof(Profile));
        set
        {
            S(nameof(Profile), value);
            _profile = value + '.';
        }
    }

    public static string[] Profiles
    {
        get => GArray(nameof(Profiles));
        set => S(nameof(Profiles), value);
    }

    [Profile]
    public static string OpenDir
    {
        get => GP(nameof(OpenDir));
        set => SP(nameof(OpenDir), value);
    }

    [Profile]
    public static string OpenMask
    {
        get => GP(nameof(OpenMask));
        set => SP(nameof(OpenMask), value);
    }

    [Profile]
    public static string SaveDir
    {
        get => GP(nameof(SaveDir));
        set => SP(nameof(SaveDir), value);
    }

    [Profile]
    public static string SaveMask
    {
        get => GP(nameof(SaveMask));
        set => SP(nameof(SaveMask), value);
    }

    [Profile]
    public static string SaveFormat
    {
        get => GP(nameof(SaveFormat));
        set => SP(nameof(SaveFormat), value);
    }

    [Profile]
    public static string BankINN
    {
        get => GP(nameof(BankINN));
        set => SP(nameof(BankINN), value);
    }

    [Profile]
    public static string BankKPP
    {
        get => GP(nameof(BankKPP));
        set => SP(nameof(BankKPP), value);
    }

    [Profile]
    public static string BankSWIFT
    {
        get => GP(nameof(BankSWIFT));
        set => SP(nameof(BankSWIFT), value);
    }

    [Profile]
    public static string CorrSWIFT
    {
        get => GP(nameof(CorrSWIFT));
        set => SP(nameof(CorrSWIFT), value);
    }

    [Profile]
    public static string CorrAccount
    {
        get => GP(nameof(CorrAccount));
        set => SP(nameof(CorrAccount), value);
    }

    [Profile]
    public static string TemplatesName
    {
        get => GP(nameof(TemplatesName));
        set => SP(nameof(TemplatesName), value);
    }

    [Profile]
    public static string TemplatesPurpose
    {
        get => GP(nameof(TemplatesPurpose));
        set => SP(nameof(TemplatesPurpose), value);
    }

    //static Config()
    //    => _profile = (AppContext.GetData(nameof(Profile)) as string ?? string.Empty) + '.';

    public static void Save(string appPath)
    {
        const string runtimeOptions = nameof(runtimeOptions);
        const string configProperties = nameof(configProperties);

        string path = Path.ChangeExtension(appPath, "runtimeconfig.json");
        string json = File.ReadAllText(path);

        var configNode = JsonNode.Parse(json);
        var entry = configNode![runtimeOptions]![configProperties];

        Type t = typeof(Config);
        var properties = t.GetProperties(/*BindingFlags.Public | BindingFlags.Instance*/);
        
        foreach (var p in properties)
        {
            switch (p.Name)
            {
                case nameof(Profile):
                    entry![nameof(Profile)] = Profile;
                    break;

                case nameof(Profiles): //TODO https://blog.okyrylchuk.dev/system-text-json-features-in-the-dotnet-6#heading-serialization-order-of-properties
                    var arr = new JsonArray();

                    foreach (var profile in Profiles)
                    {
                        arr.Add(profile); //TODO new JsonArray() { Profiles }
                    }

                    entry![nameof(Profiles)] = arr;
                    break;

                default: // with Profile
                    var info = t.GetProperty(p.Name);

                    // with [Profile] or not
                    var profiled = Attribute.IsDefined(info!, typeof(ProfileAttribute));
                    string name = profiled ? _profile + p.Name : p.Name;

                    switch (p.PropertyType.Name)
                    {
                        case "String":
                            entry![name] = p.GetValue(p) as string;
                            break;

                        case "Int32":
                            entry![name] = p.GetValue(p) as int?;
                            break;
                    }
                    break;
            }
        }

        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            //Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true
        };
        json = configNode.ToJsonString(options); //TODO JsonSerializer.Serialize() ?

        File.WriteAllText(path, json);
    }

    #region Getters

    private static string G(string name, string defValue = "")
        => AppContext.GetData(name) as string ?? defValue;

    private static string GP(string name, string defValue = "")
        => G(_profile + name, defValue);

    private static int GInt(string name, int defValue = 0)
    {
        var o = AppContext.GetData(name);

        if (o is null) return defValue;
        if (o is int i) return i;

        return int.Parse((string)o);
    }

    private static int GPInt(string name, int defValue = 0)
        => GInt(_profile + name, defValue);

    private static string[] GArray(string name)
    {
        var value = AppContext.GetData(name);
        
        if (value is null)
        {
            return Array.Empty<string>();
        }

        if (value is String)
        {
            var values = JsonSerializer.Deserialize<string[]>((string)value);
            S(name, values);

            if (values is null)
            {
                return Array.Empty<string>();
            }

            return values;
        }

        return (string[])value;
    }

    #endregion Getters
    #region Setters

    private static void S(string name, string value = "")
        => AppDomain.CurrentDomain.SetData(name, value);

    private static void SP(string name, string value = "")
        => S(_profile + name, value);

    private static void S(string name, int value)
        => AppDomain.CurrentDomain.SetData(name, value);

    private static void SP(string name, int value)
        => S(_profile + name, value);

    private static void S(string name, string[]? value)
        => AppDomain.CurrentDomain.SetData(name, value);

    #endregion Setters
}

[AttributeUsage(AttributeTargets.Property)]
internal class ProfileAttribute : Attribute
{
    // [Profile]
}
