using Data.Common;
using Data.CouponPromotion;
using Data.CustomerManagement;
using Data.ProductManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Enum;
using Utility.Helpers;

namespace Data.Sales
{
    public partial class Subscription : BaseEntityDate
    {
        public int CustomerId { get; set; }

        [StringLength(Constants.SmallDataSize)]
        public string SubscriptionNumber { get; set; }
        public int AddressId { get; set; }
        public SubscriptionStatus SubscriptionStatusId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int DurationId { get; set; }
        public int NumberOfMonths { get; set; }
        public int DeliveryDateId { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal UnitPrice { get; set; }
        public bool B2BPrice { get; set; }
        public DiscountType DiscountType { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal DiscountValueApplied { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal DiscountAmount { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal CompleteSubscriptionPrice { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal SubTotal { get; set; }
        public DiscountType CouponDiscountType { get; set; }

        [Column(TypeName = Constants.PercentageDataType)]
        public decimal CouponDiscountValueApplied { get; set; }
        public int? CouponId { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal CouponDiscountAmount { get; set; }
        public bool FreeDelivery { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal DeliveryFee { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal WalletUsedAmount { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal CashbackAmount { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal Total { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal GrandTotal { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal ReceivedCashbackAmount { get; set; }
        public int CustomerLanguageId { get; set; }
        public string CustomerIp { get; set; }
        public bool PaidInitialPayment { get; set; }
        public DeviceType DeviceTypeId { get; set; }
        public DateTime NextExpectedDelivery { get; set; }
        public string Notes { get; set; }
        public bool SpecialPackage { get; set; }
        public bool FullPayment { get; set; }
        public string TabbySessionId { get; set; }
        public string TabbyPaymentId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Address Address { get; set; }
        public virtual Product Product { get; set; }
        public virtual Coupon Coupon { get; set; }
        public ICollection<SubscriptionItemDetail> SubscriptionItemDetails { get; set; }
        public ICollection<SubscriptionOrder> SubscriptionOrders { get; set; }
    }
}
