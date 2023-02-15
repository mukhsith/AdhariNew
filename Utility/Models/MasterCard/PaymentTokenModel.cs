namespace Utility.Models.MasterCard
{
    public class PaymentTokenModel
    {
        public string version { get; set; }
        public string data { get; set; }
        public string signature { get; set; }
        public PaymentTokenHeaderModel header { get; set; }
    }
    public class PaymentTokenHeaderModel
    {
        public string ephemeralPublicKey { get; set; }
        public string publicKeyHash { get; set; }
        public string transactionId { get; set; }
    }
}
