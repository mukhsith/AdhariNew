using Data.Content;
using Data.Locations;
using Data.ProductManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Backend.Content.Interface;
using Services.Backend.Locations.Interface;
using Services.Backend.ProductManagement.Interface;
using Services.Backend.Sales;
using Services.Backend.SystemUserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using Utility.API;
using Utility.Enum;
using Utility.Helpers;
using Utility.Models.Admin.Sales;
using Utility.ResponseMapper;

namespace API.Areas.Backend.Controllers
{
    [Authorize(Roles="Admin")]
    public class  PaymentLinksController : BaseController 
    {
        private readonly IQuickPaymentService _get;
        private readonly ILogger _logger;
        public PaymentLinksController(
            IOptions<AppSettingsModel> options,
            ISystemUserService systemUserService,
            IQuickPaymentService get,
            ILoggerFactory logger) : 
            base(options, systemUserService, PermissionTypes.QuickPaymentLinks)
        {
            _get = get;
            _logger = logger.CreateLogger(typeof(PaymentLinksController).Name);
             
        }
         
        [HttpPost, Route("api/PaymentLinks/GetAllForDataTable")]
        public async Task<IActionResult> GetAllForDataTable()
        {
            ResponseMapper<dynamic> response = new();
            try
            {

                if (!await Allowed()) { return Ok(accessResponse); }
                QuickPaymentParam param = new();
                param.DataTableParam = base.GetDataTableParameters;
                param.SelectedTab = Common.ConvertTextToInt(HttpContext.Request.Form["selectedTab"].FirstOrDefault());
                param.PaymentLinkId = Common.ConvertTextToIntOptional(HttpContext.Request.Form["paymentLinkId"].FirstOrDefault());
                param.StartDate = Common.ConvertYYYYMMDDTextToDate(HttpContext.Request.Form["startDate"].FirstOrDefault());
                param.EndDate = Common.ConvertYYYYMMDDTextToDate(HttpContext.Request.Form["endDate"].FirstOrDefault());
                param.PaymentMethodId = Common.ConvertTextToIntOptional(HttpContext.Request.Form["paymentMethodId"].FirstOrDefault());

                var items = await _get.GetAllForDataTable(param);
                 
                return Ok(items);
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
