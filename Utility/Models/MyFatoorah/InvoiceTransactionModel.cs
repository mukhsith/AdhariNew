using System;

namespace Utility.Models.MyFatoorah
{
    public class InvoiceTransactionModel
    {
        public DateTime TransactionDate { get; set; }
        public string PaymentGateway { get; set; }
        public string ReferenceId { get; set; }
        public string TrackId { get; set; }
        public string TransactionId { get; set; }
        public string PaymentId { get; set; }
        public string AuthorizationId { get; set; }
        public string TransactionStatus { get; set; }
        public string TransationValue { get; set; }
        public string CustomerServiceCharge { get; set; }
        public string DueValue { get; set; }
        public string PaidCurrency { get; set; }
        public string PaidCurrencyValue { get; set; }
        public string IpAddress { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public object Error { get; set; }
        public object CardNumber { get; set; }
        public string ErrorCode { get; set; }
    }
}
