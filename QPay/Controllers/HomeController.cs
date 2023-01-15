using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Utility.API;
using Utility.Models.Frontend.CustomizedModel;
using Utility.Models.Frontend.Sales;
using Utility.ResponseMapper;
using QPay.Models;

namespace QPay.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAPIHelper _apiHelper;
        private readonly ILogger _logger;
        private readonly AppSettingsModel _appSettings;
        private IStringLocalizer<SharedResource> _sharedLocalizer;
        public HomeController(IAPIHelper apiHelper,
            ILoggerFactory logger,
            IOptions<AppSettingsModel> options,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _apiHelper = apiHelper;
            _logger = logger.CreateLogger(typeof(HomeController).Name);
            _appSettings = options.Value;
            _sharedLocalizer = sharedLocalizer;
        }
        public IActionResult WebsiteUrl()
        {
            return Redirect(_appSettings.WebsiteUrl);
        }
        public async Task<IActionResult> Index(string quickPayNumber)
        {
            if (string.IsNullOrEmpty(quickPayNumber))
            {
                return Redirect(_appSettings.WebsiteUrl);
            }

            try
            {
                var responseModel = await _apiHelper.GetAsync<APIResponseModel<QuickPaymentModel>>("webapi/common/quickpay?quickPayNumber=" + quickPayNumber);
                if (responseModel.Success && responseModel.Data != null)
                {
                    return View(responseModel.Data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return Redirect(_appSettings.WebsiteUrl);
        }

        [HttpPost]
        public virtual async Task<JsonResult> CreateQPay(CreatePaymentModel createPaymentModel)
        {
            var responseModel = new APIResponseModel<CreatePaymentModel>();
            try
            {
                createPaymentModel.CustomerIp = _apiHelper.GetUserIP();
                responseModel = await _apiHelper.PostAsync<APIResponseModel<CreatePaymentModel>>("webapi/common/createquickpay", createPaymentModel);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return Json(responseModel);
        }
        public async Task<IActionResult> QPayResult(string quickPayNumber)
        {
            var quickPaymentModel = new QuickPaymentModel();
            try
            {
                if (string.IsNullOrEmpty(quickPayNumber))
                {
                    return View(quickPaymentModel);
                }

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
