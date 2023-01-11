using Data.Content;
using Data.CouponPromotion;
using Data.Locations;
using Data.ProductManagement;
using Data.Sales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Backend.Content.Interface;
using Services.Backend.CouponPromotion.Interface;
using Services.Backend.Locations.Interface;
 
using Services.Backend.SystemUserManagement; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using Utility.API;
using Utility.Enum;
using Utility.Models.Admin.Sales;
using Utility.Models.Admin.WalletPackage;
using Utility.ResponseMapper;
namespace API.Areas.Backend.Controllers
{
    [Authorize(Roles="Root,Admin")]
    public class WalletPackageOrderController : BaseController 
    {
        private readonly IWalletPackageService _get;
        private readonly ILogger _logger;
        public WalletPackageOrderController(
            IOptions<AppSettingsModel> options,
            ISystemUserService systemUserService,
            IWalletPackageService get,
            ILoggerFactory logger) : 
            base(options, systemUserService, PermissionTypes.WalletPackageOrders)
        {
            _get = get;
            _logger = logger.CreateLogger(typeof(WalletPackageOrderController).Name);
             
        }

        /// <summary>
        /// HTTP Status 405 - Method not allowed (Check Get or POST)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        [HttpGet, Route("api/WalletPackageOrder/ById")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseMapper<WalletPackageOrderModel> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                if (id > 0)
                {
                    var item = await _get.GetWalletPackageOrderById(id);
                    response.GetById(item);
                }


            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }
        //[HttpPost, Route("api/WalletPackageOrder/GetTopUpSummary")]
        //public async Task<IActionResult> GetTopUpSummary()
        //{
        //    if (!await Allowed()) { return Ok(accessResponse); }
        //    return Ok(new { Name = "Top up 1 sale", count = 10 });
        //}

            [HttpPost, Route("api/WalletPackageOrder/GetAllForDataTable")]
        public async Task<IActionResult> GetAllForDataTable()
        {
            DataTableResult<dynamic> response = new();
            try
            {
                AdminWalletPackageOrderParam param = new();

                if (!await Allowed()) { return Ok(accessResponse); }
                param.IsEnglish = IsEnglish;
                param.DatatableParam = base.GetDataTableParameters;
                param.SelectedTab = Utility.Helpers.Common.ConvertTextToInt(HttpContext.Request.Form["selectedTab"].FirstOrDefault());
                param.PaymentId = HttpContext.Request.Form["PaymentId"].FirstOrDefault();
                var startDate = HttpContext.Request.Form["startDate"].FirstOrDefault();
                var endDate = HttpContext.Request.Form["endDate"].FirstOrDefault(); 
                param.CustomerName = HttpContext.Request.Form["customerName"].FirstOrDefault();
                param.MobileNumber = HttpContext.Request.Form["mobileNumber"].FirstOrDefault();
                param.CustomerEmail = HttpContext.Request.Form["customerEmail"].FirstOrDefault();
                var paymentMethodId = HttpContext.Request.Form["paymentMethodId"].FirstOrDefault();
                var prepaidCardId = HttpContext.Request.Form["PrepaidCardId"].FirstOrDefault();

                param.StartDate = Utility.Helpers.Common.ConvertYYYYMMDDTextToDate(startDate);
                param.EndDate = Utility.Helpers.Common.ConvertYYYYMMDDTextToDate(endDate);
                param.PrepaidCardId = Utility.Helpers.Common.ConvertTextToIntOptional(prepaidCardId);
                param.PaymentMethodId = Utility.Helpers.Common.ConvertTextToIntOptional(paymentMethodId);
                
                // response = await _orderModelFactory.GetDeliveriesForDataTable(param);


                var items = await _get.GetAllWalletPackageOrder(param);
                 
                return Ok(items);
            }
            catch (Exception ex)
            { 
                _logger.LogError(ex.Message);
            }
            return Ok(response);
        }




    }
}
