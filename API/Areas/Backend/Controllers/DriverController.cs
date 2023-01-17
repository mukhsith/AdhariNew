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
//using Utility.Models.Frontend.Sales;
using API.Areas.Backend.Factories;
using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Utility.Models.Admin.Delivery;
using Utility.Models.Frontend.Sales;

namespace API.Areas.Backend.Controllers
{
    [Authorize]
    public class DriverController : BaseController
    {
        private readonly IOrderModelFactory _orderModelFactory;
        private readonly ICommonHelper _commonHelper;
        private readonly IOrderService _get;
        private readonly ILogger _logger;
        public DriverController(
            IOrderModelFactory orderModelFactory,
            ICommonHelper  commonHelper,
            IOptions<AppSettingsModel> options,
            ISystemUserService systemUserService,
            IOrderService get,
            ILoggerFactory logger) : 
            base(options, systemUserService, PermissionTypes.DriversDashboard)
        {
            _orderModelFactory = orderModelFactory;
            _commonHelper =  commonHelper;
            _get = get;
            _logger = logger.CreateLogger(typeof(DriverController).Name);
             
        }

        /// <summary>
        /// HTTP Status 405 - Method not allowed (Check Get or POST)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        [HttpGet, Route("api/driver/TodayDeliveries")]
        public async Task<IActionResult> TodayDeliveries()
        {
             DriverDeliverySummaryModel  response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                var item = await _get.GetDriverTodayDeliverySummary(this.UserId);
                return Ok(item);

            }
            catch (Exception ex)
            {
                //response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }
         
        [HttpPost, Route("api/driver/GetTodayDeliveryDataTable")]
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


        [HttpPost, Route("api/driver/GetDeliveryDataTable")]
        public async Task<IActionResult> GetDeliveryDataTable()
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



        /// <summary>
        /// To get orders
        /// </summary>
        /// <returns>Orders</returns>
        [HttpGet, Route("api/driver/orderDetails")]
        public async Task<APIResponseModel<OrderModel>> GetOrderDetails(int id, int customerId)
        {
            return await _orderModelFactory.GetOrder(isEnglish: IsEnglish,id: id, customerId: customerId);
        }
  
        [HttpPost, Route("api/driver/UpdateOrderStatus")]
        public async Task<IActionResult> UpdateOrderStatus([FromForm] AdminOrderCancel order)
        {
            ResponseMapper<bool> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var item = await _orderModelFactory.UpdateDriverOrderStatus(order.OrderId,order.OrderTypeId, order.OrderStatusId, order.RefundDeliveryFee,order.Notes);
                response.Update(item);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }
        [HttpPost, Route("api/driver/RescheduleOrder")]
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
                var item = await _orderModelFactory.RescheduleDelivery(order.OrderId, rescheduleDate);
                response.Update(item);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }
    }
}
