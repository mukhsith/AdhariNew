namespace Utility.Models.Tabby
{
    public class RootModel
    {
        public bool create_token { get; set; }
        public string id { get; set; }
        public ConfigurationModel configuration { get; set; }
        public PaymentModel payment { get; set; }
        public string status { get; set; }
        public string token { get; set; }
        public MerchantUrlsModel merchant_urls { get; set; }
        public string lang { get; set; }
        public MerchantModel merchant { get; set; }
        public string merchant_code { get; set; }
    }
}
