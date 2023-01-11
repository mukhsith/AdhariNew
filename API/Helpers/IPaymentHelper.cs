using System.Threading.Tasks;
using Utility.Models.Frontend.CustomizedModel;

namespace API.Helpers
{
    public interface IPaymentHelper
    {
        Task<string> UpdatePaymentEntity(PaymentResponseModel paymentResponseModel);
    }
}
