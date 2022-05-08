#region License
/*
Copyright 2022 Dmitrii Evdokimov

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

using System.Text.Json;
using System.Text.Json.Nodes;

namespace CorrSWIFT;

internal static class ConfigProperties
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

    public static string BankAccount
    {
        get => AppContext.GetData(nameof(BankAccount)) as string ?? string.Empty;
        set => AppDomain.CurrentDomain.SetData(nameof(BankAccount), value);
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

    public static string BankPayerTemplate
    {
        get => AppContext.GetData(nameof(BankPayerTemplate)) as string ?? string.Empty;
        set => AppDomain.CurrentDomain.SetData(nameof(BankPayerTemplate), value);
    }

    public static string BankPurposeTemplate
    {
        get => AppContext.GetData(nameof(BankPurposeTemplate)) as string ?? string.Empty;
        set => AppDomain.CurrentDomain.SetData(nameof(BankPurposeTemplate), value);
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

        properties![nameof(BankAccount)] = BankAccount;
        properties![nameof(BankINN)] = BankINN;
        properties![nameof(BankKPP)] = BankKPP;
        properties![nameof(BankPayerTemplate)] = BankPayerTemplate;
        properties![nameof(BankPurposeTemplate)] = BankPurposeTemplate;

        var options = new JsonSerializerOptions { WriteIndented = true };
        json = configNode.ToJsonString(options);

        File.WriteAllText(config, json);
    }
}
