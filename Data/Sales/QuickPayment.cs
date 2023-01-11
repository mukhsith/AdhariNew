using Data.Common;
using Data.CustomerManagement;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Enum;
using Utility.Helpers;

namespace Data.Sales
{
    public partial class QuickPayment : BaseEntityDate
    {
        public PaymentRequestType PaymentRequestTypeId { get; set; }
        public int EntityId { get; set; }
        public int CustomerId { get; set; }
        public string MobileNumber { get; set; }
        public string PaymentNumber { get; set; }

        [Column(TypeName = Constants.AmountDataType)]
        public decimal Amount { get; set; }
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
        public bool Used { get; set; }
        public string CustomerIp { get; set; }
        public string Notes { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Data.Content.PaymentMethod PaymentMethod { get; set; }
    }
}
