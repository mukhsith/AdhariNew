using Data.Common;
using Data.CouponPromotion;
using Data.CustomerManagement;
using Data.DeliveryManagement;
using Data.SystemUserManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Enum;
using Utility.Helpers;

namespace Data.Sales
{
    public partial class Order : BaseEntityDate
    {
        public int CustomerId { get; set; }

        [StringLength(Constants.SmallDataSize)]
        public string OrderNumber { get; set; }
        public int AddressId { get; set; }
        public OrderStatus OrderStatusId { get; set; }

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
        public int PaymentMethodId { get; set; }
        public PaymentStatus PaymentStatusId { get; set; }

        [StringLength(Constants.SmallDataSize)]
        public string PaymentResult { get; set; }

        [StringLength(Constants.SmallDataSize)]
        public string PaymentAuth { get; set; }

        [StringLength(Constants.SmallDataSize)]
        public string PaymentId { get; set; }

        [StringLength(Constants.SmallDataSize)]
        public string PaymentRefId { get; set; }

        [StringLength(Constants.SmallDataSize)]
        public string PaymentTrackId { get; set; }

        [StringLength(Constants.SmallDataSize)]
        public string PaymentTransId { get; set; }

        [StringLength(Constants.SmallDataSize)]
        public string CreditCardType { get; set; }

        [StringLength(Constants.SmallDataSize)]
        public string CreditCardNumber { get; set; }

        [StringLength(Constants.SmallDataSize)]
        public string PaymentError { get; set; }

        [StringLength(Constants.SmallDataSize)]
        public string PaidCurrencyValue { get; set; }
        public DateTime? PaymentDateTime { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal BankServiceCharge { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int DeliveryTimeSlotId { get; set; }
        public int? DriverId { get; set; }
        public string Notes { get; set; }
        public string TabbySessionId { get; set; }
        public string TabbyPaymentId { get; set; }
        public DeviceType DeviceTypeId { get; set; }
        public OrderType OrderTypeId { get; set; }
        public bool NotificationTunePlayed { get; set; }
        public bool DotMatrixPrinted { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Address Address { get; set; }
        public virtual Coupon Coupon { get; set; }
        public virtual Data.Content.PaymentMethod PaymentMethod { get; set; }
        public virtual DeliveryTimeSlot DeliveryTimeSlot { get; set; }
        public virtual SystemUser Driver { get; set; } 
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
