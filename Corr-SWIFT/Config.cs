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

using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace CorrSWIFT;

public static class Config
{
    private const string runtimeOptions = nameof(runtimeOptions);
    private const string configProperties = nameof(configProperties);

    // AppContext.SetData(string name, object? data); // available from .NET 7+
    // See a lifehack at
    // https://www.strathweb.com/2019/12/runtime-host-configuration-options-and-appcontext-data-in-net-core/

    public static string OpenDir
    {
        get => AppContext.GetData(nameof(OpenDir)) as string ?? string.Empty;
        set => AppDomain.CurrentDomain.SetData(nameof(OpenDir), value);
    }

    public static string OpenMask
    {
        get => AppContext.GetData(nameof(OpenMask)) as string ?? string.Empty;
        set => AppDomain.CurrentDomain.SetData(nameof(OpenMask), value);
    }

    public static string SaveDir
    {
        get => AppContext.GetData(nameof(SaveDir)) as string ?? string.Empty;
        set => AppDomain.CurrentDomain.SetData(nameof(SaveDir), value);
    }

    public static string SaveMask
    {
        get => AppContext.GetData(nameof(SaveMask)) as string ?? string.Empty;
        set => AppDomain.CurrentDomain.SetData(nameof(SaveMask), value);
    }

    public static string SaveFormat
    {
        get => AppContext.GetData(nameof(SaveFormat)) as string ?? string.Empty;
        set => AppDomain.CurrentDomain.SetData(nameof(SaveFormat), value);
    }

    public static string CorrAccount
    {
        get => AppContext.GetData(nameof(CorrAccount)) as string ?? string.Empty;
        set => AppDomain.CurrentDomain.SetData(nameof(CorrAccount), value);
    }

    public static string BankINN
    {
        get => AppContext.GetData(nameof(BankINN)) as string ?? string.Empty;
        set => AppDomain.CurrentDomain.SetData(nameof(BankINN), value);
    }

    public static string BankKPP
    {
        get => AppContext.GetData(nameof(BankKPP)) as string ?? string.Empty;
        set => AppDomain.CurrentDomain.SetData(nameof(BankKPP), value);
    }

    public static string NameTemplate
    {
        get => AppContext.GetData(nameof(NameTemplate)) as string ?? string.Empty;
        set => AppDomain.CurrentDomain.SetData(nameof(NameTemplate), value);
    }

    public static string PurposeTemplate
    {
        get => AppContext.GetData(nameof(PurposeTemplate)) as string ?? string.Empty;
        set => AppDomain.CurrentDomain.SetData(nameof(PurposeTemplate), value);
    }

    public static int SwiftNameLimit
    {
        get => int.Parse(AppContext.GetData(nameof(SwiftNameLimit)) as string ?? "105");
        set => AppDomain.CurrentDomain.SetData(nameof(SwiftNameLimit), value.ToString());
    }

    public static string BankSWIFT
    {
        get => AppContext.GetData(nameof(BankSWIFT)) as string ?? string.Empty;
        set => AppDomain.CurrentDomain.SetData(nameof(BankSWIFT), value);
    }

    public static string CorrSWIFT
    {
        get => AppContext.GetData(nameof(CorrSWIFT)) as string ?? string.Empty;
        set => AppDomain.CurrentDomain.SetData(nameof(CorrSWIFT), value);
    }

    static Config()
    {
        InitCorrProperties();
    }

    public static void InitCorrProperties()
    {
        CorrProperties.CorrAccount = CorrAccount;
        CorrProperties.BankINN = BankINN;
        CorrProperties.BankKPP = BankKPP;
        CorrProperties.NameTemplate = NameTemplate;
        CorrProperties.PurposeTemplate = PurposeTemplate;

        CorrProperties.SwiftNameLimit = SwiftNameLimit;

        CorrProperties.BankSWIFT = BankSWIFT;
        CorrProperties.CorrSWIFT = CorrSWIFT;
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
            $"КПП Банка не указан!")

            .AppendLineIf(NameTemplate.Empty(),
            $"Шаблон за клиента Банка не указан!")

            .AppendLineIf(PurposeTemplate.Empty(),
            $"Шаблон назначения за третье лицо не указан!");

        return err.ToString();
    }

    public static void Save()
    {
        string config = Path.ChangeExtension(Application.ExecutablePath, "runtimeconfig.json");
        string json = File.ReadAllText(config);

        var configNode = JsonNode.Parse(json);
        var properties = configNode![runtimeOptions]![configProperties];

        properties![nameof(OpenDir)] = OpenDir;
        properties![nameof(OpenMask)] = OpenMask;

        properties![nameof(SaveDir)] = SaveDir;
        properties![nameof(SaveMask)] = SaveMask;
        properties![nameof(SaveFormat)] = SaveFormat;

        properties![nameof(CorrAccount)] = CorrAccount;
        properties![nameof(BankINN)] = BankINN;
        properties![nameof(BankKPP)] = BankKPP;
        properties![nameof(NameTemplate)] = NameTemplate;
        properties![nameof(PurposeTemplate)] = PurposeTemplate;

        properties![nameof(SwiftNameLimit)] = SwiftNameLimit;

        properties![nameof(BankSWIFT)] = BankSWIFT;
        properties![nameof(CorrSWIFT)] = CorrSWIFT;

        var options = new JsonSerializerOptions { WriteIndented = true };
        json = configNode.ToJsonString(options);

        File.WriteAllText(config, json);
        InitCorrProperties();
    }
}
