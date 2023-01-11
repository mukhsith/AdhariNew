using Utility.Enum;

namespace Utility.Models.Frontend.Sales
{
    public class CreatePaymentModel
    {        
        public string CustomerIp { get; set; }
        public string PaymentUrl { get; set; }
        public string PaymentReturnUrl { get; set; }
        public string QuickPayNumber { get; set; }
        public int OrderId { get; set; }
        public int EntityId { get; set; }
        public string EntityNumber { get; set; }
        public PaymentRequestType PaymentRequestTypeId { get; set; }
        public int WalletPackageId { get; set; }
        public int PaymentMethodId { get; set; }
        public bool RedirectToPaymentPage { get; set; }
        public int CustomerLanguageId { get; set; }
    }
}
