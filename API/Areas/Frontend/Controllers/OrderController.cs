using API.Areas.Frontend.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;
using Utility.Models.Frontend.Sales;
using Utility.ResponseMapper;

namespace API.Areas.Frontend.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderModelFactory _orderModelFactory;

        public OrderController(IOptions<AppSettingsModel> options,
            IOrderModelFactory orderModelFactory) : base(options)
        {
            _orderModelFactory = orderModelFactory;
        }

        /// <summary>
        /// Create order
        /// </summary>
        [HttpPost, Route("/webapi/order/createorder")]
        public async Task<APIResponseModel<CreatePaymentModel>> CreateOrder([FromBody] CreatePaymentModel createPaymentModel)
        {
            return await _orderModelFactory.CreateOrder(isEnglish: isEnglish, customerId: LoggedInCustomerId, deviceTypeId: HeaderDeviceTypeId, createPaymentModel: createPaymentModel);
        }

        /// <summary>
        /// To get orders
        /// </summary>
        /// <returns>Orders</returns>
        [HttpGet, Route("/webapi/order/orders")]
        public async Task<APIResponseModel<List<OrderModel>>> GetOrders(int id = 0, string orderNumber = "", int limit = 0, int page = 0,
            OrderStatus? orderStatus = null)
        {
            return await _orderModelFactory.GetOrders(isEnglish: isEnglish, customerId: LoggedInCustomerId, id: id, orderNumber: orderNumber,
                limit: limit, page: page, orderStatus: orderStatus);
        }

        /// <summary>
        /// To re order
        /// </summary>
        /// <returns>Re order</returns>
        [HttpGet, Route("/webapi/order/reorder")]
        public async Task<APIResponseModel<bool>> ReOrder(int id = 0)
        {
            return await _orderModelFactory.ReOrder(isEnglish: isEnglish, customerId: LoggedInCustomerId, id: id);
        }
    }
}
