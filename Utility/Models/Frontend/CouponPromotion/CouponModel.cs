using System;
using Utility.Enum;
using Utility.Models.Base;

namespace Utility.Models.Frontend.CouponPromotion
{
    public class CouponModel : BaseModel
    {
        public string CouponCode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public bool LimitUsageEnabled { get; set; }
        public int Quantity { get; set; }
        public int QuantityUsed { get; set; }  //quantity - quantityUsed = Remaining
        public bool Validity { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Expired()
        {
            return EndDate <= DateTime.Now;
        }
        public int RemainingQuantity()
        {
            return (Quantity >= QuantityUsed ? 0 : Quantity - QuantityUsed);
        }
    }
}
