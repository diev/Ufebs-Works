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

using System.Linq;
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

    // AppContext.SetData(string name, object? data); // available from .NET 7+
    // See a lifehack at
    // https://www.strathweb.com/2019/12/runtime-host-configuration-options-and-appcontext-data-in-net-core/

    public static string Profile
    {
        get => AppContext.GetData(".Profile") as string ?? string.Empty;
        set => AppDomain.CurrentDomain.SetData(".Profile", value);
    }

    public static string Profiles
    {
        get => AppContext.GetData(".Profiles") as string ?? string.Empty;
        set => AppDomain.CurrentDomain.SetData(".Profiles", value);
    }

    public static string OpenDir
    {
        get => G(_openDir);
        set => S(_openDir, value);
    }

    public static string OpenMask
    {
        get => G(_openMask);
        set => S(_openMask, value);
    }

    public static string SaveDir
    {
        get => G(_saveDir);
        set => S(_saveDir, value);
    }

    public static string SaveMask
    {
        get => G(_saveMask);
        set => S(_saveMask, value);
    }

    public static string SaveFormat
    {
        get => G(_saveFormat);
        set => S(_saveFormat, value);
    }

    public static string CorrAccount
    {
        get => G(_corrAccount);
        set => S(_corrAccount, value);
    }

    public static string BankINN
    {
        get => G(_bankINN);
        set => S(_bankINN, value);
    }

    public static string BankKPP
    {
        get => G(_bankKPP);
        set => S(_bankKPP, value);
    }

    public static string TemplatesName
    {
        get => G(_templatesName);
        set => S(_templatesName, value);
    }

    public static string TemplatesPurpose
    {
        get => G(_templatesPurpose);
        set => S(_templatesPurpose, value);
    }

    public static int SwiftNameLimit
    {
        get => Gint(_swiftNameLimit);
        set => S(_swiftNameLimit, value);
    }

    public static string SwiftPurposeField
    {
        get => G(_swiftPurposeField);
        set => S(_swiftPurposeField, value);
    }

    public static string BankSWIFT
    {
        get => G(_bankSWIFT);
        set => S(_bankSWIFT, value);
    }

    public static string CorrSWIFT
    {
        get => G(_corrSWIFT);
        set => S(_corrSWIFT, value);
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
        var properties = configNode![runtimeOptions]![configProperties];

        properties![".Profile"] = Profile;
        properties![".Profiles"] = Profiles;

        properties![P(_openDir)] = OpenDir;
        properties![P(_openMask)] = OpenMask;

        properties![P(_saveDir)] = SaveDir;
        properties![P(_saveMask)] = SaveMask;
        properties![P(_saveFormat)] = SaveFormat;

        properties![P(_bankINN)] = BankINN;
        properties![P(_bankKPP)] = BankKPP;
        properties![P(_bankSWIFT)] = BankSWIFT;

        properties![P(_corrAccount)] = CorrAccount;
        properties![P(_corrSWIFT)] = CorrSWIFT;

        properties![P(_templatesName)] = TemplatesName;
        properties![P(_templatesPurpose)] = TemplatesPurpose;

        properties![P(_swiftNameLimit)] = SwiftNameLimit;
        properties![P(_swiftPurposeField)] = SwiftPurposeField;

        var options = new JsonSerializerOptions { WriteIndented = true };
        json = configNode.ToJsonString(options);

        File.WriteAllText(config, json);
        InitCorrProperties();
    }

    private static string P(string value)
    {
        return Profile + "." + value;
    }

    private static string G(string name, string defValue = "")
    {
        return AppContext.GetData(P(name)) as string ?? defValue;
    }

    private static int Gint(string name, int defValue = 0)
    {
        return int.Parse(G(name, defValue.ToString()));
    }

    private static void S(string name, string value)
    {
        AppDomain.CurrentDomain.SetData(P(name), value);
    }

    private static void S(string name, int value)
    {
        AppDomain.CurrentDomain.SetData(P(name), value.ToString());
    }
}
