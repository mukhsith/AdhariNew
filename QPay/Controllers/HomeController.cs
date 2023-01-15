using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using Utility.Models.Frontend.CustomizedModel;
using Utility.Models.Frontend.Sales;
using Utility.ResponseMapper;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAPIHelper _apiHelper;
        private readonly ILogger _logger;
        private IStringLocalizer<SharedResource> _sharedLocalizer;
        public HomeController(IAPIHelper apiHelper,ILoggerFactory logger, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _apiHelper = apiHelper;
            _logger = logger.CreateLogger(typeof(HomeController).Name);
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task<IActionResult> Index(string quickPayNumber)
        {
            var quickPaymentModel = new QuickPaymentModel();
            try
            {
                var responseModel = await _apiHelper.GetAsync<APIResponseModel<QuickPaymentModel>>("webapi/common/quickpay?quickPayNumber=" + quickPayNumber);
                
                if (responseModel.Success && responseModel.Data != null)
                {
                    quickPaymentModel = responseModel.Data;
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return View(quickPaymentModel);
        }

        [HttpPost]
        public virtual async Task<JsonResult> CreateQPay(CreatePaymentModel createPaymentModel)
        {
            var responseModel = new APIResponseModel<CreatePaymentModel>();
            try
            {
                createPaymentModel.CustomerIp = _apiHelper.GetUserIP();
                responseModel = await _apiHelper.PostAsync<APIResponseModel<CreatePaymentModel>>("webapi/customer/createquickpay", createPaymentModel);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return Json(responseModel);
        }

        public async Task<IActionResult> QPayResult(string quickPayNumber)
        {
            var quickPaymentModel = new WalletPackageOrderModel();
            try
            {
                if (string.IsNullOrEmpty(quickPayNumber))
                {
                    return View(quickPaymentModel);
                }

                var responseModel = await _apiHelper.GetAsync<APIResponseModel<WalletPackageOrderModel>>("webapi/common/quickpayresult?quickPayNumber=" + quickPayNumber);
                if (responseModel.Success && responseModel.Data != null)
                {
                    quickPaymentModel = responseModel.Data;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return View(quickPaymentModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
