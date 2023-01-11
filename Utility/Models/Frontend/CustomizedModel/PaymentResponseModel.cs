namespace Utility.Models.Frontend.CustomizedModel
{
    public class PaymentResponseModel
    {
        public string Trandata { get; set; }
        public string Amount { get; set; }
        public string PaymentId { get; set; }
        public string TrackId { get; set; }
        public string TransId { get; set; }
        public string RefId { get; set; }
        public string EntityId { get; set; }
        public string RequestType { get; set; }
        public string Auth { get; set; }
        public string Result { get; set; }
        public string ErrorText { get; set; }
        public bool IsExceptionError { get; set; }
        public string ExceptionErrorText { get; set; }
        public string CreditCardType { get; set; }
        public string CreditCardNumber { get; set; }
        public decimal BankServiceCharge { get; set; }
        public bool BankServiceChargeInPercentage { get; set; }
    }
}
