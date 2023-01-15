namespace Utility.Models.MyFatoorah
{
    public class ExecutePaymentModel
    {
        public int PaymentMethodId { get; set; }
        public string SessionId { get; set; }
        public string CustomerName { get; set; }
        public string DisplayCurrencyIso { get; set; }
        public string MobileCountryCode { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public decimal InvoiceValue { get; set; }
        public string Language { get; set; }
        public string CallBackUrl { get; set; }
        public string ErrorUrl { get; set; }
    }
}
