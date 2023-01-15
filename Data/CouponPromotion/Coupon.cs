using Data.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Enum;
using Utility.Helpers;

namespace Data.CouponPromotion
{
    public class Coupon : BaseEntityCommon
    {

        [StringLength(Constants.ShortDataSize)]
        public string CouponCode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DiscountType DiscountType { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal DiscountPercentage { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal DiscountAmount { get; set; }
        public bool LimitUsageEnabled { get; set; }
        public int Quantity { get; set; }
        public int QuantityUsed { get; set; }
        public bool Expired()
        {
            return EndDate <= DateTime.Now;
        }
        public int RemainingQuantity()
        {
            return (Quantity >= QuantityUsed ? 0 : Quantity - QuantityUsed);
        }
        public decimal ApplyCouponDiscount(decimal subTotal)
        {
            decimal discountAmount = 0;

            if (Active)
            {
                if (LimitUsageEnabled && QuantityUsed > Quantity)
                {
                    return discountAmount;
                }

                if (StartDate.HasValue && EndDate.HasValue)
                {
                    if (DateTime.Now.Date >= StartDate.Value.Date && DateTime.Now.Date <= EndDate.Value.Date)
                    {
                        if (DiscountType == DiscountType.Percentage)
                        {
                            discountAmount = (subTotal * this.DiscountPercentage) / 100;
                        }
                        else if (DiscountType == DiscountType.Amount)
                        {
                            discountAmount = this.DiscountAmount;
                            if (discountAmount > subTotal)
                            {
                                discountAmount = subTotal;
                            }
                        }
                    }
                }
                else
                {
                    if (DiscountType == DiscountType.Percentage)
                    {
                        discountAmount = (subTotal * this.DiscountPercentage) / 100;
                    }
                    else if (DiscountType == DiscountType.Amount)
                    {
                        discountAmount = this.DiscountAmount;
                        if (discountAmount > subTotal)
                        {
                            discountAmount = subTotal;
                        }
                    }
                }
            }

            return discountAmount;
        }
        public decimal ApplyCouponDiscount2(decimal subTotal)
        {
            decimal discountAmount = 0;

            if (DiscountType == DiscountType.Percentage)
            {
                discountAmount = (subTotal * this.DiscountPercentage) / 100;
            }
            else if (DiscountType == DiscountType.Amount)
            {
                discountAmount = this.DiscountAmount;
            }

            return discountAmount;
        }
    }
}
