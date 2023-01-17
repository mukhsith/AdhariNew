using Data.Common;

namespace Data.Shop
{
    public partial class CartAttribute : BaseEntityDate
    {
        public int CustomerId { get; set; }
        public int? AddressId { get; set; }
        public int? CouponId { get; set; }
        public int? PaymentMethodId { get; set; }
        public bool UseWalletAmount { get; set; }
        public string Notes { get; set; }
        public int? OtherPaymentMethodId { get; set; }
    }
}
