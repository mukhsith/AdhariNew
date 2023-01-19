using Data.Content;
using Data.Sales;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Backend.Content.Interface;
using Services.Backend.SystemUserManagement;
using Services.Backend.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using Utility.API;
using Utility.Enum;
using Utility.ResponseMapper;
using Utility.Models.Admin.Sales; 
using API.Areas.Backend.Factories;
using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Utility.Models.Admin.Delivery;
using Utility.Models.Frontend.Sales;

namespace API.Areas.Backend.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderModelFactory _orderModelFactory;
        private readonly ICommonHelper _commonHelper;
        private readonly IOrderService _get;
        private readonly ILogger _logger;
        public OrderController(
            IOrderModelFactory orderModelFactory,
            ICommonHelper  commonHelper,
            IOptions<AppSettingsModel> options,
            ISystemUserService systemUserService,
            IOrderService get,
            ILoggerFactory logger) : 
            base(options, systemUserService, PermissionTypes.Orders)
        {
            _orderModelFactory = orderModelFactory;
            _commonHelper =  commonHelper;
            _get = get;
            _logger = logger.CreateLogger(typeof(SystemUserController).Name);
             
        }

        /// <summary>
        /// HTTP Status 405 - Method not allowed (Check Get or POST)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        [HttpGet, Route("api/Order/TodaySales")]
        public async Task<IActionResult> TodaySales()
        {
             DailyOrderSummaryModel  response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                response  = await _orderModelFactory.GetTodaySales(IsEnglish);
                return Ok(response);

            }
            catch (Exception ex)
            {
                //response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

        [HttpGet, Route("api/Order/SubscriptionTodaySales")]
        public async Task<IActionResult> SubscriptionTodaySales()
        {
            DailySubscriptionSummaryModel response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                response = await _orderModelFactory.GetTodaySubscriptionSales(IsEnglish);
                return Ok(response);

            }
            catch (Exception ex)
            {
                //response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

        
        [HttpPost, Route("api/Order/GetDeliveriesForDataTable")]
        public async Task<IActionResult> GetDeliveriesDataTable()
        {
            DataTableResult<List<DeliveriesDashboard>> response = new();
            try
            {
                AdminOrderDeliveriesParam param = new();

                if (!await Allowed()) { return Ok(accessResponse); }
                 param.IsEnglish = IsEnglish;
                 param.DatatableParam = base.GetDataTableParameters;
                 param.SelectedTab = Utility.Helpers.Common.ConvertTextToInt(HttpContext.Request.Form["selectedTab"].FirstOrDefault());
                 param.OrderNumber = HttpContext.Request.Form["orderNumber"].FirstOrDefault();
                 var deliveryDate = HttpContext.Request.Form["deliveryDate"].FirstOrDefault(); 
                var orderModeId = HttpContext.Request.Form["orderModeId"].FirstOrDefault();
                var orderTypeId = HttpContext.Request.Form["orderTypeId"].FirstOrDefault();
                var areaId = HttpContext.Request.Form["areaId"].FirstOrDefault();
                var driverId = HttpContext.Request.Form["driverId"].FirstOrDefault();

                param.DeliveryDate = Utility.Helpers.Common.ConvertYYYYMMDDTextToDate(deliveryDate);
                param.OrderModeId = Utility.Helpers.Common.ConvertTextToIntOptional(orderModeId);
                param.OrderTypeId = Utility.Helpers.Common.ConvertTextToIntOptional(orderTypeId);
                param.AreaId = Utility.Helpers.Common.ConvertTextToIntOptional(areaId);
                param.DriverId = Utility.Helpers.Common.ConvertTextToIntOptional(driverId);

                response = await _orderModelFactory.GetDeliveriesForDataTable(param);

                return Ok(response);
            }
            catch (Exception ex)
            {
                //response.CacheException(ex);
                _logger.LogError(ex.Message);
            }
            return Ok(response);
        }


        [HttpPost, Route("api/Order/GetDeliveriesForDataTableReturned")]
        public async Task<IActionResult> GetDeliveriesReturnedDataTable()
        {
            DataTableResult<List<DeliveriesDashboard>> response = new();
            try
            {
                AdminOrderDeliveriesParam param = new();

                if (!await Allowed()) { return Ok(accessResponse); }
                param.IsEnglish = IsEnglish;
                param.DatatableParam = base.GetDataTableParameters;
                param.SelectedTab = Utility.Helpers.Common.ConvertTextToInt(HttpContext.Request.Form["selectedTab"].FirstOrDefault());
                param.OrderNumber = HttpContext.Request.Form["orderNumber"].FirstOrDefault();
                var deliveryDate = HttpContext.Request.Form["deliveryDate"].FirstOrDefault();
                var orderModeId = HttpContext.Request.Form["orderModeId"].FirstOrDefault();
                var orderTypeId = HttpContext.Request.Form["orderTypeId"].FirstOrDefault();
                var areaId = HttpContext.Request.Form["areaId"].FirstOrDefault();
                var driverId = HttpContext.Request.Form["driverId"].FirstOrDefault();

                param.DeliveryDate = Utility.Helpers.Common.ConvertYYYYMMDDTextToDate(deliveryDate);
                param.OrderModeId = Utility.Helpers.Common.ConvertTextToIntOptional(orderModeId);
                param.OrderTypeId = Utility.Helpers.Common.ConvertTextToIntOptional(orderTypeId);
                param.AreaId = Utility.Helpers.Common.ConvertTextToIntOptional(areaId);
                param.DriverId = Utility.Helpers.Common.ConvertTextToIntOptional(driverId);
                param.OrderStatusID = OrderStatus.ReturnedByDriver;
                response = await _orderModelFactory.GetDeliveriesForDataTable(param);

                return Ok(response);
            }
            catch (Exception ex)
            {
                //response.CacheException(ex);
                _logger.LogError(ex.Message);
            }
            return Ok(response);
        }

        [HttpPost, Route("api/Order/GetSalesOrderForDataTable")]
        public async Task<IActionResult> GetSalesOrderForDataTable()
        { 
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                AdminSalesOrderParam param = new(); 
  
                param.SelectedTab = Utility.Helpers.Common.ConvertTextToInt(HttpContext.Request.Form["selectedTab"].FirstOrDefault());
                param.DatatableParam = base.GetDataTableParameters;
                param.CustomerId = Utility.Helpers.Common.ConvertTextToIntOptional(HttpContext.Request.Form["customerId"].FirstOrDefault());
                var orderNumber = HttpContext.Request.Form["orderNumber"].FirstOrDefault();
                var startDate = HttpContext.Request.Form["startDate"].FirstOrDefault();
                var endDate = HttpContext.Request.Form["endDate"].FirstOrDefault(); 
                var paymentMethodId = HttpContext.Request.Form["paymentMethodId"].FirstOrDefault();
                var orderTypeId = HttpContext.Request.Form["orderTypeId"].FirstOrDefault();
                var orderStatusId = HttpContext.Request.Form["orderStatusId"].FirstOrDefault();

                param.OrderNumber = HttpContext.Request.Form["orderNumber"].FirstOrDefault();
                param.CustomerName = HttpContext.Request.Form["customerName"].FirstOrDefault();
                param.MobileNumber = HttpContext.Request.Form["customerMobile"].FirstOrDefault();
                param.CustomerEmail = HttpContext.Request.Form["customerEmail"].FirstOrDefault();
                param.StartDate = Utility.Helpers.Common.ConvertYYYYMMDDTextToDate(startDate);
                param.EndDate = Utility.Helpers.Common.ConvertYYYYMMDDTextToDate(endDate); 
                param.PaymentMethodId = Utility.Helpers.Common.ConvertTextToIntOptional(paymentMethodId);
                param.OrderTypeId = Utility.Helpers.Common.ConvertTextToIntOptional(orderTypeId);
                param.OrderStatusId = Utility.Helpers.Common.ConvertTextToIntOptional(orderStatusId);

                var items = await _get.GetAllSalesOrders(param);
                
                // response.GetAll(items);
                return Ok(items);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }
            return Ok(response);
        }

         
        [HttpPost, Route("api/Order/GetTodayDeliveryDataTable")]
        public async Task<IActionResult> GetTodayDeliveryDataTable()
        {
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }


                //driver id will be taken login userid                
                var items = await _get.GetTodayDeliveriesDataTable(base.GetDataTableParameters, this.UserId);
                foreach (var item in items.Data)
                {
                    item.FormattedDeliveryFee = await _commonHelper.ConvertDecimalToString(item.DeliveryFee, IsEnglish, 1, true);
                    item.FormattedTotal = await _commonHelper.ConvertDecimalToString(item.Total, IsEnglish, 1, true);
                }
                //var items = await _get.GetAllForDeliveriesDataTable(base.GetDataTableParameters, IsEnglish, orderNumber, _orderDate, _orderModeId, _orderTypeId, _areaId, _driverId);

                // response.GetAll(items);
                return Ok(items);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }
            return Ok(response);
        }


        [HttpPost, Route("api/Order/GetAllForDeliveriesDataTable")]
        public async Task<IActionResult> GetAllForDeliveriesDataTable()
        {
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }


                var orderNumber = HttpContext.Request.Form["orderNumber"].FirstOrDefault();
                var orderDate = HttpContext.Request.Form["orderDate"].FirstOrDefault(); 
                var orderModeId = HttpContext.Request.Form["orderModeId"].FirstOrDefault();
                var orderTypeId = HttpContext.Request.Form["orderTypeId"].FirstOrDefault();
                var areaId = HttpContext.Request.Form["areaId"].FirstOrDefault();
                var driverId = HttpContext.Request.Form["driverId"].FirstOrDefault();

                DateTime? _orderDate = Utility.Helpers.Common.ConvertTextToDate(orderDate);
                var _orderModeId = Utility.Helpers.Common.ConvertTextToIntOptional(orderModeId);
                var _orderTypeId = Utility.Helpers.Common.ConvertTextToIntOptional(orderTypeId);
                var _areaId = Utility.Helpers.Common.ConvertTextToIntOptional(areaId);
                var _driverId = Utility.Helpers.Common.ConvertTextToIntOptional(driverId);

                var items = await _get.GetAllForDeliveriesDataTable(base.GetDataTableParameters,   orderNumber, _orderDate, _orderModeId, _orderTypeId,_areaId,_driverId);
                foreach (var item in items.Data)
                {
                    item.FormattedDeliveryFee = await _commonHelper.ConvertDecimalToString(item.DeliveryFee, IsEnglish, 1, true);
                    item.FormattedTotal = await _commonHelper.ConvertDecimalToString(item.Total, IsEnglish, 1, true);
                    item.OrderStatus = _commonHelper.GetOrderStatusName(item.OrderStatusId, IsEnglish);
                }
                //var items = await _get.GetAllForDeliveriesDataTable(base.GetDataTableParameters, IsEnglish, orderNumber, _orderDate, _orderModeId, _orderTypeId, _areaId, _driverId);

                // response.GetAll(items);
                return Ok(items);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }
            return Ok(response);
        }
        /// <summary>
        /// To get orders
        /// </summary>
        /// <returns>Orders</returns>
        [HttpGet, Route("api/order/orderDetails")]
        public async Task<APIResponseModel<OrderModel>> GetOrderDetails(int id, int customerId)
        {
            return await _orderModelFactory.GetOrder(isEnglish: IsEnglish,id: id, customerId: customerId);
        }

        [HttpGet, Route("api/order/orders")]
        public async Task<APIResponseModel<List<OrderModel>>> GetOrders(int id, int customerId)
        {
            return await _orderModelFactory.GetOrders(isEnglish: IsEnglish, customerId: customerId, id: id, orderNumber: "",
                limit: 0, page: 0, orderStatus: null);
        }

        [HttpPost, Route("api/order/RescheduleOrder")]
        public async Task<IActionResult> RescheduleOrder([FromForm] OrderRequestParam order)
        {
            ResponseMapper<bool> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                DateTime? rescheduleDate = null;
                if (!string.IsNullOrEmpty(order.RescheduleDeliveryDate))
                {
                    rescheduleDate = Utility.Helpers.Common.ConvertTextToDate(order.RescheduleDeliveryDate);
                }
                var item = await _orderModelFactory.RescheduleDelivery(order.OrderId,rescheduleDate);
                response.Update(item);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }


        [HttpPost, Route("api/order/UpdateOrderStatus")]
        public async Task<IActionResult> UpdateOrderStatus([FromForm] AdminOrderCancel order)
        {
            ResponseMapper<bool> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var item = await _orderModelFactory.UpdateOrderStatus(order.OrderId, order.OrderStatusId, order.RefundDeliveryFee,order.Notes);
                response.Update(item);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }
        [HttpPost, Route("api/order/AssignDriver")]
        public async Task<IActionResult> UpdateOrderStatus([FromForm] AdminDriverModel order)
        {
            ResponseMapper<bool> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                //var item = await _orderModelFactory.AddDriver(order.OrderId, order.DriverId, order.OrderTypeId);
                //response.Update(item);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }
        [HttpPost, Route("api/order/RemoveDriver")]
        public async Task<IActionResult> RemoveDriver([FromForm] AdminDriverModel order)
        {
            ResponseMapper<bool> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                //var item = await _orderModelFactory.AddDriver(order.OrderId, order.DriverId, order.OrderTypeId);
                //response.Update(item);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

        /// <summary>
        /// To get order in pdf
        /// </summary>
        /// <returns>Order pdf</returns>
        [HttpGet, Route("api/order/GetOrderPDF")]
        public async Task<IActionResult> GetOrderPDF(int? id)
        {

            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                if (id.HasValue)
                {
                    var orderItem = await _orderModelFactory.PrepareOrder(true, id.Value); 
                    var url = _commonHelper.GetOrderPdfUrl(orderItem, base.AppSettings.APIBaseUrl, true);
                    response.GetById(url);
                }
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogInformation(ex.Message);
            }
            return Ok(response);

        }


        /// <summary>
        /// Create order
        /// </summary>
        [HttpPost, Route("api/order/createorder")]
        // public async Task<APIResponseModel<CreatePaymentModel>> CreateOrder([FromBody] CreatePaymentModel createPaymentModel)
        public async Task<APIResponseModel<AdminCreateOrderModel>> CreateOrder([FromBody] AdminCreateOrderModel _adminCreateOrderModel)
        {


            var offlineOrder= await _orderModelFactory.CreateOrder(isEnglish: true, customerId: _adminCreateOrderModel.CustomerId, deviceTypeId: DeviceType.Web, adminCreateOrderModel: _adminCreateOrderModel);


            return offlineOrder;
        }


        /// <summary>
        /// Create order
        /// </summary>
        [HttpPost, Route("api/order/SendQpay")]
        // public async Task<APIResponseModel<CreatePaymentModel>> CreateOrder([FromBody] CreatePaymentModel createPaymentModel)
        public async Task<IActionResult> SendQpay(int CustomerID, int OrderID, string OrderNumber, string Ordertotal, int OrderType)
        {
            ResponseMapper<bool> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                 var item = await _orderModelFactory.AddQPay(CustomerID,OrderID, OrderNumber,Convert.ToDecimal(Ordertotal), OrderType);
                

                return Ok(item);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);

            //var offlineOrder = await _orderModelFactory.CreateOrder(isEnglish: true, customerId: _adminCreateOrderModel.CustomerId, deviceTypeId: DeviceType.Web, adminCreateOrderModel: _adminCreateOrderModel);


            //return offlineOrder;
        }




        //[HttpPost, Route("api/Order/UpdateStatus")]
        //public async Task<IActionResult> UpdateStatus(int Id, OrderStatus status = OrderStatus.Failed)
        //{
        //    ResponseMapper<Order> response = new();
        //    try
        //    {
        //        if (!await Allowed()) { return Ok(accessResponse); }

        //        var item = await _get.UpdateOrderStatus(Id, status);
        //        response.DisplayOrder(item);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.CacheException(ex);
        //        _logger.LogError(ex.Message);
        //    }

        //    return Ok(response);
        //}
    }
}
