using System;
using System.Threading.Tasks;
using Utility.Models.MasterCard;

namespace API.Helpers
{
    public interface IMasterCardHelper
    {
        Task<Tuple<string, string>> CreateRequest(decimal amount, string orderNumber, string requestType, int entityId, string description);
        Task<MasterCardTransaction> CreateApplepayRequest(decimal amount, string orderNumber, PaymentTokenModel paymentTokenModel);
        Task<bool> ValidateApplepayMerchant(string requestUrl);
        MasterCardRootModel GetResult(string orderId);
    }
}
