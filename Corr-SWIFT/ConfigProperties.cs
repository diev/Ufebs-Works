namespace Corr_SWIFT;

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
}
