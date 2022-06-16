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

using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace CorrSWIFT;

public static class Config
{
    private const string _openDir = "Open.Dir";
    private const string _openMask = "Open.Mask";

    private const string _saveDir = "Save.Dir";
    private const string _saveMask = "Save.Mask";
    private const string _saveFormat = "Save.Format";

    private const string _bankINN = "Bank.INN";
    private const string _bankKPP = "Bank.KPP";
    private const string _bankSWIFT = "Bank.SWIFT";

    private const string _corrAccount = "Corr.Account";
    private const string _corrSWIFT = "Corr.SWIFT";

    private const string _templatesName = "Templates.Name";
    private const string _templatesPurpose = "Templates.Purpose";

    private const string _swiftNameLimit = "Swift.NameLimit";
    private const string _swiftPurposeField = "Swift.PurposeField";

    // Save.Format
    public const string UfebsFormat = "УФЭБС";
    public const string SwiftFormat = "SWIFT";

    // AppContext.SetData(string name, object? data); // available from .NET 7+
    // See a lifehack at
    // https://www.strathweb.com/2019/12/runtime-host-configuration-options-and-appcontext-data-in-net-core/

    public static string Profile
    {
        get => G(nameof(Profile));
        set => S(nameof(Profile), value);
    }

    public static string[] Profiles
    {
        get => Garray(nameof(Profiles));
        set => S(nameof(Profiles), value);
    }

    public static string OpenDir
    {
        get => GP(_openDir);
        set => SP(_openDir, value);
    }

    public static string OpenMask
    {
        get => GP(_openMask);
        set => SP(_openMask, value);
    }

    public static string SaveDir
    {
        get => GP(_saveDir);
        set => SP(_saveDir, value);
    }

    public static string SaveMask
    {
        get => GP(_saveMask);
        set => SP(_saveMask, value);
    }

    public static string SaveFormat
    {
        get => GP(_saveFormat);
        set => SP(_saveFormat, value);
    }

    public static string CorrAccount
    {
        get => GP(_corrAccount);
        set => SP(_corrAccount, value);
    }

    public static string BankINN
    {
        get => GP(_bankINN);
        set => SP(_bankINN, value);
    }

    public static string BankKPP
    {
        get => GP(_bankKPP);
        set => SP(_bankKPP, value);
    }

    public static string TemplatesName
    {
        get => GP(_templatesName);
        set => SP(_templatesName, value);
    }

    public static string TemplatesPurpose
    {
        get => GP(_templatesPurpose);
        set => SP(_templatesPurpose, value);
    }

    public static int SwiftNameLimit
    {
        get => GPint(_swiftNameLimit, 160);
        set => SP(_swiftNameLimit, value);
    }

    public static string SwiftPurposeField
    {
        get => GP(_swiftPurposeField, "70");
        set => SP(_swiftPurposeField, value);
    }

    public static string BankSWIFT
    {
        get => GP(_bankSWIFT);
        set => SP(_bankSWIFT, value);
    }

    public static string CorrSWIFT
    {
        get => GP(_corrSWIFT);
        set => SP(_corrSWIFT, value);
    }

    static Config()
    {
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

    public static string Validate()
    {
        StringBuilder err = new();
        err
            .AppendLineIf(OpenDir.Empty() || !Directory.Exists(OpenDir),
            $"Папка OpenDir не существует!")

            .AppendLineIf(OpenMask.Empty(),
            $"Маска OpenMask не указана!")

            .AppendLineIf(SaveDir.Empty() || !Directory.Exists(SaveDir),
            $"Папка SaveDir не существует!")

            .AppendLineIf(SaveMask.Empty(),
            $"Маска SaveMask не указана!")

            .AppendLineIf(CorrAccount.Empty(),
            $"Счет Банка не указан!")

            .AppendLineIf(BankINN.Empty(),
            $"ИНН Банка не указан!")

            .AppendLineIf(BankKPP.Empty(),
            $"КПП Банка не указан!");

            //.AppendLineIf(TemplatesName.Empty(),
            //$"Шаблон за клиента Банка не указан!")

            //.AppendLineIf(TemplatesPurpose.Empty(),
            //$"Шаблон назначения за третье лицо не указан!");

        return err.ToString();
    }

    public static void Save()
    {
        const string runtimeOptions = nameof(runtimeOptions);
        const string configProperties = nameof(configProperties);

        string config = Path.ChangeExtension(Application.ExecutablePath, "runtimeconfig.json");
        string json = File.ReadAllText(config);

        var configNode = JsonNode.Parse(json);
        var entry = configNode![runtimeOptions]![configProperties];

        entry![nameof(Profile)] = Profile;
        //entry![nameof(Profiles)] = (string[])Profiles;

        entry![P(_openDir)] = OpenDir;
        entry![P(_openMask)] = OpenMask;

        entry![P(_saveDir)] = SaveDir;
        entry![P(_saveMask)] = SaveMask;
        entry![P(_saveFormat)] = SaveFormat;

        entry![P(_bankINN)] = BankINN;
        entry![P(_bankKPP)] = BankKPP;
        entry![P(_bankSWIFT)] = BankSWIFT;

        entry![P(_corrAccount)] = CorrAccount;
        entry![P(_corrSWIFT)] = CorrSWIFT;

        entry![P(_templatesName)] = TemplatesName;
        entry![P(_templatesPurpose)] = TemplatesPurpose;

        entry![P(_swiftNameLimit)] = SwiftNameLimit;
        entry![P(_swiftPurposeField)] = SwiftPurposeField;

        var options = new JsonSerializerOptions { WriteIndented = true };
        json = configNode.ToJsonString(options);

        File.WriteAllText(config, json);
        InitCorrProperties();
    }

    public static void Save2()
    {
        const string runtimeOptions = nameof(runtimeOptions);
        const string configProperties = nameof(configProperties);

        string path = Path.ChangeExtension(Application.ExecutablePath, "runtimeconfig.json");
        string json = File.ReadAllText(path);

        var configNode = JsonNode.Parse(json);
        var entry = configNode![runtimeOptions]![configProperties];

        Type t = typeof(Config);
        var properties = t.GetProperties(/*BindingFlags.Public | BindingFlags.Instance*/);
        //var sb = new StringBuilder();
        
        foreach (var p in properties)
        {
            //sb.Append(p.Name).Append('/').Append(p.PropertyType.Name).Append(" = ");
            //if (p.PropertyType.Name == "String")
            //    sb.AppendLine(p.GetValue(p) as string);
            //else
            //    sb.AppendLine((p.GetValue(p) as int?).ToString());

            //AppDomain.CurrentDomain.SetData(p.Name, p.GetValue(p) as string);

            switch (p.Name)
            {
                case nameof(Profile):
                    entry![nameof(Profile)] = Profile;
                    break;

                case nameof(Profiles): //TODO https://blog.okyrylchuk.dev/system-text-json-features-in-the-dotnet-6#heading-serialization-order-of-properties
                    entry![nameof(Profiles)] = string.Join(';', Profiles);
                    break;

                default:
                    switch (p.PropertyType.Name)
                    {
                        case "String":
                            entry![P(p.Name)] = p.GetValue(p) as string;
                            break;

                        case "Int32":
                            entry![P(p.Name)] = p.GetValue(p) as int?;
                            break;
                    }
                    break;
            }
        }

        //File.WriteAllText(path + "!", sb.ToString());
        /*
        Profile/String = 
        Profiles/String = 
        OpenDir/String = G:\BANK\TEST\OUT
        OpenMask/String = *.xml
        SaveDir/String = G:\BANK\TEST\CLI
        SaveMask/String = {id}.txt
        SaveFormat/String = УФЭБС
        CorrAccount/String = 30101810600000000702
        BankINN/String = 7831001422
        BankKPP/String = 784101001
        TemplatesName/String = АО "Сити Инвест Банк" ИНН 7831001422 ({name} р/с {acc})
        TemplatesPurpose/String = //7831001422//784101001//{name}//{purpose}
        SwiftNameLimit/Int32 = 105
        SwiftPurposeField/String = 70
        BankSWIFT/String = CITVRU2P
        CorrSWIFT/String = CITVRU2P
         */

        var options = new JsonSerializerOptions { WriteIndented = true };
        json = configNode.ToJsonString(options);

        File.WriteAllText(path + "2", json);
        InitCorrProperties();
    }

    private static string P(string name)
    {
        return Profile == string.Empty
            ? name
            : Profile + "." + name;
    }

    private static string G(string name, string defValue = "")
    {
        return AppContext.GetData(name) as string ?? defValue;
    }

    private static string GP(string name, string defValue = "")
    {
        return G(P(name), defValue);
    }

    private static string[]? Garray(string name)
    {
        return AppContext.GetData(name) as string[];
    }

    private static void S(string name, string[] value)
    {
        AppDomain.CurrentDomain.SetData(name, value);
    }

    private static int Gint(string name, int defValue = 0)
    {
        return int.Parse(G(name, defValue.ToString()));
    }

    private static int GPint(string name, int defValue = 0)
    {
        return Gint(P(name), defValue);
    }

    private static void S(string name, string value)
    {
        AppDomain.CurrentDomain.SetData(name, value);
    }

    private static void SP(string name, string value)
    {
        S(P(name), value);
    }

    private static void S(string name, int value)
    {
        AppDomain.CurrentDomain.SetData(P(name), value.ToString());
    }

    private static void SP(string name, int value)
    {
        S(P(name), value.ToString());
    }
}
