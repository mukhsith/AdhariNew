using API.Areas.Frontend.Factories;
using Microsoft.AspNetCore.Authorization;
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
        private static readonly object controllerLock = new object();
        public OrderController(IOptions<AppSettingsModel> options,
            IOrderModelFactory orderModelFactory) : base(options)
        {
            _orderModelFactory = orderModelFactory;
        }

        /// <summary>
        /// Create order
        /// </summary>
        [HttpPost, Route("/webapi/order/createorder")]
        [Authorize]
        public APIResponseModel<CreatePaymentModel> CreateOrder([FromBody] CreatePaymentModel createPaymentModel)
        {
            APIResponseModel<CreatePaymentModel> response = new();
            lock (controllerLock)
            {
                response = _orderModelFactory.CreateOrder(isEnglish: isEnglish, customerId: LoggedInCustomerId, deviceTypeId: HeaderDeviceTypeId, createPaymentModel: createPaymentModel).Result;
            }
            return response;
        }

        /// <summary>
        /// To get orders
        /// </summary>
        /// <returns>Orders</returns>
        [HttpGet, Route("/webapi/order/orders")]
        [Authorize]
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
        [Authorize]
        public async Task<APIResponseModel<bool>> ReOrder(int id = 0)
        {
            return await _orderModelFactory.ReOrder(isEnglish: isEnglish, customerId: LoggedInCustomerId, id: id);
        }

        /// <summary>
        /// To get order in pdf
        /// </summary>
        /// <returns>Order pdf</returns>
        [HttpGet, Route("/webapi/order/getorderpdf")]
        [Authorize]
        public async Task<APIResponseModel<object>> GetOrderPdf(int id)
        {
            return await _orderModelFactory.GetOrderPdf(isEnglish: isEnglish, customerId: LoggedInCustomerId, id: id);
        }

        /// <summary>
        /// To get order by order number
        /// </summary>
        /// <returns>Orders</returns>
        [HttpGet, Route("/webapi/order/orderbyordernumber")]
        public async Task<APIResponseModel<List<OrderModel>>> GetOrderByOrderNumber(string orderNumber = "")
        {
            return await _orderModelFactory.GetOrderByOrderNumber(isEnglish: isEnglish, orderNumber: orderNumber);
        }

        /// <summary>
        /// To get order in pdf for admin
        /// </summary>
        /// <returns>Order pdf for admin</returns>
        [HttpGet, Route("/webapi/order/getorderpdfadmin")]
        public async Task<APIResponseModel<object>> GetOrderPdfAdmin(int id, int customerId, bool isEnglish = false)
        {
            return await _orderModelFactory.GetOrderPdf(isEnglish: isEnglish, customerId: customerId, id: id);
        }
    }
}
