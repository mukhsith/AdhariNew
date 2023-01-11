using Utility.Enum;

namespace Utility.Models.Frontend.Shop
{
    public class CartAttributeModel
    {
        public int? AddressId { get; set; }
        public int? CouponId { get; set; }
        public string CouponCode { get; set; }
        public int? PaymentMethodId { get; set; }
        public string Notes { get; set; }
        /// <summary>
        /// For Admin only and its used in Backend CartController, default value is zero which nothing 
        /// </summary>
        public int CustomerId { get; set; } = 0;//for admin only
        public AttributeType AttributeTypeId { get; set; }
    }
}
