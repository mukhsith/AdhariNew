using Utility.Enum;

namespace Utility.Models.MasterCard
{
    public class CreateApplePayRequestModel
    {
        public PaymentRequestType PaymentRequestTypeId { get; set; }
        public int EntityId { get; set; }
        //public string PaymentToken { get; set; }
        public PaymentTokenModel PaymentToken { get; set; }
    }
}
