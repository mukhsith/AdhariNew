using System;
using System.Threading.Tasks;
using Utility.Models.MasterCard;

namespace API.Helpers
{
    public interface IMasterCardHelper
    {
        Task<Tuple<string, string>> CreateRequest(decimal amount, string orderNumber, string requestType, int entityId, string description);
        Task<Tuple<string, string>> CreateRequest2(decimal amount, string orderNumber, string requestType, int entityId, string description);
        MasterCardRootModel GetResult(string orderId);
    }
}
