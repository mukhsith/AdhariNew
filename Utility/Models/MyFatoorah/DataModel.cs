using System;
using System.Collections.Generic;

namespace Utility.Models.MyFatoorah
{
    public class DataModel
    {
        public int InvoiceId { get; set; }
        public bool IsDirectPayment { get; set; }
        public string PaymentURL { get; set; }
        public object CustomerReference { get; set; }
        public object UserDefinedField { get; set; }
        public string RecurringId { get; set; }
        public List<PaymentMethodModel> PaymentMethods { get; set; }
        public string InvoiceStatus { get; set; }
        public string InvoiceReference { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ExpiryDate { get; set; }
        public string ExpiryTime { get; set; }
        public double InvoiceValue { get; set; }
        public object Comments { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public string InvoiceDisplayValue { get; set; }
        public List<object> InvoiceItems { get; set; }
        public List<InvoiceTransactionModel> InvoiceTransactions { get; set; }
        public List<object> Suppliers { get; set; }
    }
}
