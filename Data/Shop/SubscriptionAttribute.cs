using Data.Common;
using System.ComponentModel.DataAnnotations;
using Utility.Helpers;

namespace Data.Shop
{
    public partial class SubscriptionAttribute : BaseEntityDate
    {
        public int CustomerId { get; set; }
        public int? ProductId { get; set; }
        public int? AddressId { get; set; }
        public int? CouponId { get; set; }
        public int? PaymentMethodId { get; set; }
        public int? DurationId { get; set; }
        public int? DeliveryDateId { get; set; }
        public int? Quantity { get; set; }
        public bool UseWalletAmount { get; set; }
        public string Notes { get; set; }
    }
}
