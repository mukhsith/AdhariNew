using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Enum;
using Utility.Models.Frontend.Sales;
using Utility.ResponseMapper;

namespace API.Areas.Frontend.Factories
{
    public interface IOrderModelFactory
    {
        Task<APIResponseModel<CreatePaymentModel>> CreateOrder(bool isEnglish, int customerId, DeviceType deviceTypeId, CreatePaymentModel createPaymentModel);
        Task<APIResponseModel<List<OrderModel>>> GetOrders(bool isEnglish, int customerId, int id = 0, string orderNumber = "",
            int limit = 0, int page = 0, OrderStatus? orderStatus = null);
        Task<APIResponseModel<bool>> ReOrder(bool isEnglish, int customerId, int id);
        Task<APIResponseModel<object>> GetOrderPdf(bool isEnglish, int customerId, int id);
    }
}
