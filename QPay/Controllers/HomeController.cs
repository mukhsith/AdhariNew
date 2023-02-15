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
using System.Globalization;
using System.Threading;
using Microsoft.AspNetCore.Localization;
using Utility.Models.Frontend.CustomerManagement;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Utility.Enum;
using Utility.Models.Frontend.Content;

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
        public async Task<IActionResult> Index()
        {
            QuickPaymentModel quickPaymentModel = new();
            try
            {
                var paymentMethodResponseModel = await _apiHelper.GetAsync<APIResponseModel<List<PaymentMethodModel>>>("webapi/common/paymentmethods?typeId=" + PaymentRequestType.QuickPay);
                if (paymentMethodResponseModel.Success && paymentMethodResponseModel.Data != null)
                {
                    quickPaymentModel.PaymentMethods = paymentMethodResponseModel.Data;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return View(quickPaymentModel);
        }
        public async Task<IActionResult> Pay(string quickPayNumber = "")
        {
            try
            {
                if (string.IsNullOrEmpty(quickPayNumber))
                {
                    return RedirectToAction("Index");
                }

                var responseModel = await _apiHelper.GetAsync<APIResponseModel<QuickPaymentModel>>("webapi/common/quickpay?quickPayNumber=" + quickPayNumber);
                if (responseModel.Success && responseModel.Data != null)
                {
                    var quickPaymentModel = responseModel.Data;
                    return View(quickPaymentModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public virtual async Task<JsonResult> Register(CustomerModel customerModel)
        {
            APIResponseModel<CustomerModel> response = new();
            try
            {
                response = await _apiHelper.PostAsync<APIResponseModel<CustomerModel>>("webapi/customer/register", customerModel);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Json(response);
        }

        [HttpGet]
        public virtual async Task<JsonResult> ResendOTP(int otpDetailId)
        {
            APIResponseModel<CustomerModel> response = new();
            try
            {
                response = await _apiHelper.GetAsync<APIResponseModel<CustomerModel>>("webapi/customer/resendotp?otpDetailId=" + otpDetailId);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Json(response);
        }

        [HttpPost]
        public virtual async Task<JsonResult> VerifyOTP(VerifyOTPModel verifyOTPModel)
        {
            APIResponseModel<CustomerModel> response = new();
            try
            {
                CustomerModel customerModel = new();
                customerModel.OTPDetailId = verifyOTPModel.RequestId;
                customerModel.OTP = verifyOTPModel.OTP;

                response = await _apiHelper.PostAsync<APIResponseModel<CustomerModel>>("webapi/customer/verifyotp", customerModel);
                if (response.Data != null && response.Success)
                {

                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Json(response);
        }

        [HttpPost]
        public virtual async Task<JsonResult> CreateQPay(QuickPaymentModel quickPaymentModel)
        {
            var responseModel = new APIResponseModel<CreatePaymentModel>();
            try
            {
                quickPaymentModel.CustomerIp = _apiHelper.GetUserIP();
                responseModel = await _apiHelper.PostAsync<APIResponseModel<CreatePaymentModel>>("webapi/common/createquickpay", quickPaymentModel);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return Json(responseModel);
        }

        [HttpPost]
        public virtual async Task<JsonResult> UpdateQPay(CreatePaymentModel createPaymentModel)
        {
            var responseModel = new APIResponseModel<CreatePaymentModel>();
            try
            {
                createPaymentModel.CustomerIp = _apiHelper.GetUserIP();
                responseModel = await _apiHelper.PostAsync<APIResponseModel<CreatePaymentModel>>("webapi/common/updatequickpay", createPaymentModel);
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
