using Data.Sales;
using System.Threading.Tasks;
using Utility.Models.Tabby;

namespace API.Helpers
{
    public interface ITabbyHelper
    {
        Task<RootModel> PrepareOrderRootModel(Order order);
        Task<RootModel> PrepareSubscriptionRootModel(Subscription subscription);
        Task<RootModel> CreateSession(RootModel rootModel);
        Task<PaymentModel> GetPayment(string id);
        Task<PaymentModel> CapturePayment(PaymentCaptureModel paymentCaptureModel, string id);
    }
}
