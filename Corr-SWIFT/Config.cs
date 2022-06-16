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

using CorrLib;

using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Unicode;

namespace CorrSWIFT;

public static class Config
{
    private static string _profile; //for fast access

    // Save.Format
    public const string UfebsFormat = "УФЭБС";
    public const string SwiftFormat = "SWIFT";

    // AppContext.SetData(string name, object? data); // available from .NET 7+
    // See a lifehack at
    // https://www.strathweb.com/2019/12/runtime-host-configuration-options-and-appcontext-data-in-net-core/

    public static string Profile
    {
        get => G(nameof(Profile));
        set
        {
            S(nameof(Profile), value);

            if (string.IsNullOrEmpty(value))
            {
                _profile = string.Empty;
            }
            else
            {
                _profile = value + '.';
            }
        }
    }

    public static string[] Profiles
    {
        get => GArray(nameof(Profiles));
        set => S(nameof(Profiles), value);
    }

    public static string OpenDir
    {
        get => GP(nameof(OpenDir));
        set => SP(nameof(OpenDir), value);
    }

    public static string OpenMask
    {
        get => GP(nameof(OpenMask));
        set => SP(nameof(OpenMask), value);
    }

    public static string SaveDir
    {
        get => GP(nameof(SaveDir));
        set => SP(nameof(SaveDir), value);
    }

    public static string SaveMask
    {
        get => GP(nameof(SaveMask));
        set => SP(nameof(SaveMask), value);
    }

    public static string SaveFormat
    {
        get => GP(nameof(SaveFormat));
        set => SP(nameof(SaveFormat), value);
    }

    public static string BankINN
    {
        get => GP(nameof(BankINN));
        set => SP(nameof(BankINN), value);
    }

    public static string BankKPP
    {
        get => GP(nameof(BankKPP));
        set => SP(nameof(BankKPP), value);
    }

    public static string BankSWIFT
    {
        get => GP(nameof(BankSWIFT));
        set => SP(nameof(BankSWIFT), value);
    }

    public static string CorrSWIFT
    {
        get => GP(nameof(CorrSWIFT));
        set => SP(nameof(CorrSWIFT), value);
    }

    public static string CorrAccount
    {
        get => GP(nameof(CorrAccount));
        set => SP(nameof(CorrAccount), value);
    }

    public static string TemplatesName
    {
        get => GP(nameof(TemplatesName));
        set => SP(nameof(TemplatesName), value);
    }

    public static string TemplatesPurpose
    {
        get => GP(nameof(TemplatesPurpose));
        set => SP(nameof(TemplatesPurpose), value);
    }

    public static int SwiftNameLimit
    {
        get => GPInt(nameof(SwiftNameLimit));
        set => SP(nameof(SwiftNameLimit), value);
    }

    public static string SwiftPurposeField
    {
        get => GP(nameof(SwiftPurposeField));
        set => SP(nameof(SwiftPurposeField), value);
    }

    static Config()
    {
        _profile = AppContext.GetData(nameof(Profile)) as string ?? string.Empty;

        if (_profile.Length > 0)
        {
            _profile += '.';
        }

        InitCorrProperties();
    }

    public static void InitCorrProperties()
    {
        CorrProperties.BankINN = BankINN;
        CorrProperties.BankKPP = BankKPP;
        CorrProperties.BankSWIFT = BankSWIFT;

        CorrProperties.CorrAccount = CorrAccount;
        CorrProperties.CorrSWIFT = CorrSWIFT;

        CorrProperties.TemplatesName = TemplatesName;
        CorrProperties.TemplatesPurpose = TemplatesPurpose;

        CorrProperties.SwiftNameLimit = SwiftNameLimit;
        CorrProperties.SwiftPurposeField = SwiftPurposeField;
    }

    public static void Save()
    {
        const string runtimeOptions = nameof(runtimeOptions);
        const string configProperties = nameof(configProperties);

        string path = Path.ChangeExtension(Application.ExecutablePath, "runtimeconfig.json");
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
                    switch (p.PropertyType.Name)
                    {
                        case "String":
                            entry![_profile + p.Name] = p.GetValue(p) as string;
                            break;

                        case "Int32":
                            entry![_profile + p.Name] = p.GetValue(p) as int?;
                            break;
                    }
                    break;
            }
        }

        var options = new JsonSerializerOptions
        {
            //Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true
        };
        json = configNode.ToJsonString(options);

        File.WriteAllText(path, json);
        InitCorrProperties();
    }

    private static string G(string name, string defValue = "") =>
        AppContext.GetData(name) as string ?? defValue;

    private static string GP(string name, string defValue = "") =>
        G(_profile + name, defValue);

    private static int GInt(string name, int defValue = 0)
    {
        var value = AppContext.GetData(name) as int?;

        if (value is null)
        { 
            return defValue;
        }

        return (int)value;
    }

    private static int GPInt(string name, int defValue = 0) =>
        GInt(_profile + name, defValue);

    private static string[]? GArray(string name)
    {
        var value = AppContext.GetData(name);
        
        if (value is null)
        {
            return Array.Empty<string>();
        }

        if (value is String)
        {
            string[]? values = JsonSerializer.Deserialize<string[]>((string)value);
            S(name, values);

            return values;
        }

        return (string[]?)value;
    }

    private static void S(string name, string value = "") =>
        AppDomain.CurrentDomain.SetData(name, value);

    private static void SP(string name, string value = "") =>
        S(_profile + name, value);

    private static void S(string name, int value) =>
        AppDomain.CurrentDomain.SetData(name, value);

    private static void SP(string name, int value) =>
        S(_profile + name, value);

    private static void S(string name, string[]? value) =>
        AppDomain.CurrentDomain.SetData(name, value);

}
