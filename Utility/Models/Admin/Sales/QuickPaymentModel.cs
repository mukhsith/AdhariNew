 
using System; 
using Utility.Enum; 

namespace Utility.Models.Admin.Sales
{
    public class QuickPaymentModel    
    {
        public PaymentRequestType PaymentRequestTypeId { get; set; }
        public int EntityId { get; set; }
        public int CustomerId { get; set; }
        public string MobileNumber { get; set; }
        public string PaymentNumber { get; set; } 
        public decimal Amount { get; set; }
        public string FormattedAmount { get; set; }
        public int? PaymentMethodId { get; set; }
        public PaymentStatus? PaymentStatusId { get; set; } 
        public string PaymentResult { get; set; } 
        public string PaymentAuth { get; set; } 
        public string PaymentId { get; set; } 
        public string PaymentRefId { get; set; } 
        public string PaymentTrackId { get; set; } 
        public string PaymentTransId { get; set; } 
        public string CreditCardType { get; set; } 
        public string CreditCardNumber { get; set; } 
        public string PaymentError { get; set; } 
        public string PaidCurrencyValue { get; set; }
        public DateTime? PaymentDateTime { get; set; } 
        public decimal BankServiceCharge { get; set; }
        public bool Used { get; set; }
        public string CustomerIp { get; set; }
        public string Notes { get; set; } 
        public PaymentMethod PaymentMethod { get; set; }
    }
}
