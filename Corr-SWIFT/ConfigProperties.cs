namespace Corr_SWIFT;

internal class ConfigProperties
{
    internal class OpenProperties
    {
        public string Dir { get; set; }
        public string Mask { get; set; }

        public OpenProperties()
        {
            Dir = AppContext.GetData("Open.Directory") as string
                ?? Directory.GetCurrentDirectory();

            Mask = AppContext.GetData("Open.Mask") as string
                ?? "*.xml";
        }
    }

    internal class SaveProperties
    {
        public string Dir { get; set; }
        public string Mask { get; set; }

        public SaveProperties()
        {
            Dir = AppContext.GetData("Save.Directory") as string
                ?? Directory.GetCurrentDirectory();

            Mask = AppContext.GetData("Save.Mask") as string
                ?? "*_.txt";
        }
    }

    internal class BankProperties
    {
        public string Account { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }
        public string PayerTemplate { get; set; }
        public string PurposeTemplate { get; set; }

        public BankProperties()
        {
            Account = AppContext.GetData("Bank.Account") as string
                ?? "30109810800010001378";

            INN = AppContext.GetData("Bank.INN") as string
                ?? "7831001422";

            KPP = AppContext.GetData("Bank.KPP") as string
                ?? "784101001";

            PayerTemplate = (AppContext.GetData("Bank.PayerTemplate") as string
                ?? "АО \"Сити Инвест Банк\" ИНН {inn} ({name} р/с {acc})")
                .Replace("{acc}", Account)
                .Replace("{inn}", Account)
                .Replace("{kpp}", Account);

            PurposeTemplate = (AppContext.GetData("Bank.PurposeTemplate") as string
                ?? "//{inn}//{kpp}//{name}//{purpose}")
                .Replace("{acc}", Account)
                .Replace("{inn}", Account)
                .Replace("{kpp}", Account);
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
}
