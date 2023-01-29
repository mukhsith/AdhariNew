using Utility.Enum;

namespace Utility.Models.Frontend.Shop
{
    public class SubscriptionAttributeModel
    {
        public int? CustomerId { get; set; }
        public string CustomerGuidValue { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public int? DurationId { get; set; }
        public int? DeliveryDateId { get; set; }
        public int? AddressId { get; set; }
        public int? CouponId { get; set; }
        public string CouponCode { get; set; }
        public int? PaymentMethodId { get; set; }
        public string Notes { get; set; }
        public AttributeType AttributeTypeId { get; set; }
    }
}
