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

internal class ConfigProperties
{
    internal class OpenProperties
    {
        public string? Dir { get; set; }
        public string? Mask { get; set; }

        public OpenProperties()
        {
            Dir = (AppContext.GetData("Open.Dir") as string)?
                .Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            Mask = AppContext.GetData("Open.Mask") as string;
        }
    }

    internal class SaveProperties
    {
        public string? Dir { get; set; }
        public string? Mask { get; set; }

        public SaveProperties()
        {
            Dir = (AppContext.GetData("Save.Dir") as string)?
                .Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            Mask = AppContext.GetData("Save.Mask") as string;
        }
    }

    internal class BankProperties
    {
        public string? Account { get; set; }
        public string? INN { get; set; }
        public string? KPP { get; set; }
        public string? PayerTemplate { get; set; }
        public string? PurposeTemplate { get; set; }

        public BankProperties()
        {
            Account = AppContext.GetData("Bank.Account") as string;
            INN = AppContext.GetData("Bank.INN") as string;
            KPP = AppContext.GetData("Bank.KPP") as string;
            PayerTemplate = AppContext.GetData("Bank.PayerTemplate") as string;
            PurposeTemplate = AppContext.GetData("Bank.PurposeTemplate") as string;
        }
    }

    public OpenProperties Open { get; set; }
    public SaveProperties Save { get; set; }
    public BankProperties Bank { get; set; }

    public ConfigProperties()
    {
        Open = new OpenProperties();
        Save = new SaveProperties();
        Bank = new BankProperties();
    }

    public void Flush()
    {
        //AppContext.SetData(string name, object? data); //.NET 7+ only

        string config = Path.ChangeExtension(Application.ExecutablePath, "runtimeconfig.json");
        string json = File.ReadAllText(config);

        var configNode = JsonNode.Parse(json);
        var properties = configNode!["runtimeOptions"]!["configProperties"];

        properties!["Open.Dir"] = Open.Dir;
        properties!["Open.Mask"] = Open.Mask;

        properties!["Save.Dir"] = Save.Dir;
        properties!["Save.Mask"] = Save.Mask;

        properties!["Bank.Account"] = Bank.Account;
        properties!["Bank.INN"] = Bank.INN;
        properties!["Bank.KPP"] = Bank.KPP;
        properties!["Bank.PayerTemplate"] = Bank.PayerTemplate;
        properties!["Bank.PurposeTemplate"] = Bank.PurposeTemplate;

        var options = new JsonSerializerOptions { WriteIndented = true };
        json = configNode.ToJsonString(options);

        File.WriteAllText(config, json);
    }
}
