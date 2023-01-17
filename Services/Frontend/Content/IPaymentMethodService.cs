using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Enum;

namespace Services.Frontend.Content
{
    public interface IPaymentMethodService
    {
        Task<IList<Data.Content.PaymentMethod>> GetAllPaymentMethod(PaymentRequestType paymentRequestType);
        Task<Data.Content.PaymentMethod> GetPaymentMethodById(int id);
    }
}
