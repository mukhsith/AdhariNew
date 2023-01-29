using Data.Common;

namespace Data.Shop
{
    public partial class SubscriptionAttribute : BaseEntityDate
    {
        public int? CustomerId { get; set; }
        public string CustomerGuidValue { get; set; }
        public int? ProductId { get; set; }
        public int? AddressId { get; set; }
        public int? CouponId { get; set; }
        public int? PaymentMethodId { get; set; }
        public int? DurationId { get; set; }
        public int? DeliveryDateId { get; set; }
        public int? Quantity { get; set; }
        public bool UseWalletAmount { get; set; }
        public string Notes { get; set; }
        public int? OtherPaymentMethodId { get; set; }
    }
}
