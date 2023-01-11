namespace Utility.Models.Frontend.Sales
{
    public class SubscriptionOrderAdminModel
    {
        public int SubscriptionId { get; set; }
        public string FormattedDate { get; set; }
        public string PaymentMethodName { get; set; }
        public string PaymentMethodColor { get; set; }
        public string PaymentStatusName { get; set; }
        public string PaymentStatusColor { get; set; }
        public string PaymentId { get; set; }
        public string PaymentRefId { get; set; }
    }
}
