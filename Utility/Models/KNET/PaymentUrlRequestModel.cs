namespace Utility.Models.KNET
{
    public class PaymentUrlRequestModel
    {
        public string LangId { get; set; }
        public string Amount { get; set; }
        public string TrackId { get; set; }
        public string EntityId { get; set; }
        public string CustomerId { get; set; }
        public string RequestType { get; set; }
    }
}
