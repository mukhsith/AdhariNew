using Data.Common;
using Data.DeliveryManagement;
using Data.SystemUserManagement;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Enum;
using Utility.Helpers;

namespace Data.Sales
{
    public partial class SubscriptionOrder : BaseEntityDate
    {
        public int SubscriptionId { get; set; }
        public string OrderNumber { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal SubTotal { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal DeliveryFee { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal Total { get; set; }
        public int? PaymentMethodId { get; set; }
        public PaymentStatus? PaymentStatusId { get; set; }

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
        public bool Confirmed { get; set; }
        public bool Delivered { get; set; }
        public bool FirstOrder { get; set; }
        public virtual Subscription Subscription { get; set; }
        public virtual Data.Content.PaymentMethod PaymentMethod { get; set; }
        public virtual DeliveryTimeSlot DeliveryTimeSlot { get; set; }
        public virtual SystemUser Driver { get; set; }
    }
}
