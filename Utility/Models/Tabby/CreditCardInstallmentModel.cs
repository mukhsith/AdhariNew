namespace Utility.Models.Tabby
{
    public class CreditCardInstallmentModel
    {
        public string web_url { get; set; }
        public string type { get; set; }
        public bool is_available { get; set; }
        public string rejection_reason { get; set; }
    }
}
