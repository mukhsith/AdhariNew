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
    public class DeliveryController : BaseController
    {
        private readonly IOrderModelFactory _orderModelFactory;
        private readonly ICommonHelper _commonHelper;
        private readonly IOrderService _get;
        private readonly ILogger _logger;
        public DeliveryController(
            IOrderModelFactory orderModelFactory,
            ICommonHelper commonHelper,
            IOptions<AppSettingsModel> options,
            ISystemUserService systemUserService,
            IOrderService get,
            ILoggerFactory logger) :
            base(options, systemUserService, PermissionTypes.DeliveriesDashboard)
        {
            _orderModelFactory = orderModelFactory;
            _commonHelper = commonHelper;
            _get = get;
            _logger = logger.CreateLogger(typeof(DriverController).Name);

        }


        /// <summary>
        /// HTTP Status 405 - Method not allowed (Check Get or POST)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        [HttpGet, Route("api/delivery/TodayDeliveries")]
        public async Task<IActionResult> TodayDeliveries()
        {
            AdminDeliverySummaryModel response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                var item = await _get.GetTodayDeliverySummary();
                return Ok(item);

            }
            catch (Exception ex)
            {
                //response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

    }
}
