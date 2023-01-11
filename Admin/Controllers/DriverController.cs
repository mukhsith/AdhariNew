using Admin.Models;
using Data.Common;
using Data.CustomerManagement;
using Data.SystemUserManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using Utility.Models.Admin.Sales;
using Utility.ResponseMapper;

namespace Admin.Controllers
{
    
    public class DriverController : BaseController
    {
        private readonly IAPIHelper _apiHelper; 
        public DriverController(IAPIHelper apiHelper, IHttpContextAccessor httpContextAccessor, IOptions<AppSettingsModel> options, ILoggerFactory logger) :
            base(httpContextAccessor, options,   logger.CreateLogger(typeof(DriverController).Name))
        {
            _apiHelper = apiHelper;
        }
        
        public async Task<IActionResult> DriverDashboard()
        {
            DriverDeliverySummaryModel responseModel = await _apiHelper.GetAsync<DriverDeliverySummaryModel>("driver/TodayDeliveries", "");

            if (responseModel != null)
            {
                return View(responseModel);
            }
            else
            { return View(new DriverDeliverySummaryModel());
            }

             
        }
        public async Task<IActionResult> OrderDetails(int id, int customerId)
        { 
            APIResponseModel<Utility.Models.Frontend.Sales.OrderModel> responseModel = await _apiHelper.GetAsync<APIResponseModel<Utility.Models.Frontend.Sales.OrderModel>>("order/orderDetails?id=" + id + "&customerId=" + customerId, "");

            if (responseModel.Success && responseModel.Data != null)
            {
                return View(responseModel);
            }

            return View(responseModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
