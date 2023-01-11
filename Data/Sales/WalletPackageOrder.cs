using Data.Common;
using Data.CouponPromotion;
using Data.CustomerManagement;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Enum;
using Utility.Helpers;

namespace Data.Sales
{
    public partial class WalletPackageOrder : BaseEntityDate
    {
        public int CustomerId { get; set; }
        public int WalletPackageId { get; set; }

        [StringLength(Constants.SmallDataSize)]
        public string OrderNumber { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal Amount { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal WalletAmount { get; set; }
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
        public DeviceType DeviceTypeId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual WalletPackage WalletPackage { get; set; }
        public virtual Data.Content.PaymentMethod PaymentMethod { get; set; }
    }
}
