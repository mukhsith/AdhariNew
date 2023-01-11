using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Logging;
using Utility.API;
using Utility.Enum;
using Utility.Models.Frontend.Content;
using Utility.Models.Frontend.CustomerManagement;
using Utility.Models.Frontend.Sales;
using Utility.Models.Frontend.Shop;
using Utility.ResponseMapper;

namespace Web.Controllers
{
    public class SubscriptionController : BaseController
    {
        private readonly IAPIHelper _apiHelper;
        private readonly ILogger _logger;
        public SubscriptionController(IAPIHelper apiHelper,
            ILoggerFactory logger,
            IRazorViewEngine razorViewEngine) : base(razorViewEngine)
        {
            _apiHelper = apiHelper;
            _logger = logger.CreateLogger(typeof(OrderController).Name);
        }

        [HttpPost]
        public virtual async Task<JsonResult> SaveSubscriptionAttributes(SubscriptionAttributeModel subscriptionAttributeModel)
        {
            var responseModel = new APIResponseModel<SubscriptionSummaryModel>();
            try
            {
                responseModel = await _apiHelper.PostAsync<APIResponseModel<SubscriptionSummaryModel>>("webapi/subscription/savesubscriptionattributes?app=false", subscriptionAttributeModel);
                if (responseModel.Success && responseModel.Data != null)
                {
                    responseModel.Data.FormattedSubscriptionSummary = await RenderPartialViewToStringAsync("_SubscriptionSummary", responseModel.Data);
                }
            }
            catch (Exception ex)
            {
                responseModel.Message = ex.Message;
                _logger.LogInformation(ex.Message);
            }

            return Json(responseModel);
        }
        public virtual IActionResult Checkout()
        {
            try
            {
                var authenticationToken = Convert.ToString(Request.Cookies["AuthenticationToken"]);
                if (!string.IsNullOrEmpty(authenticationToken))
                {
                    return RedirectToRoute("subscriptioncheckoutaddress");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return View();
        }
        public virtual async Task<IActionResult> CheckoutAddress()
        {
            var addressModels = new List<AddressModel>();
            try
            {
                var responseModel = await _apiHelper.GetAsync<APIResponseModel<List<AddressModel>>>("webapi/customer/getaddress?typeId=" + RelatedEntityType.Subscription);
                if (responseModel.Success && responseModel.Data != null && responseModel.Data.Count > 0)
                {
                    addressModels = responseModel.Data;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return View(addressModels);
        }
        public virtual async Task<IActionResult> CheckoutSummary(int addressId)
        {
            var subscriptionCheckOutModel = new SubscriptionCheckOutModel();
            try
            {
                var responseModel = await _apiHelper.GetAsync<APIResponseModel<SubscriptionCheckOutModel>>("webapi/subscription/getcheckoutsummary?app=false");
                if (responseModel.Success && responseModel.Data != null)
                {
                    var responsePaymentModel = await _apiHelper.GetAsync<APIResponseModel<List<PaymentMethodModel>>>("webapi/common/paymentmethods?typeId=" + PaymentRequestType.SubscriptionOrder);
                    if (responsePaymentModel.Success && responsePaymentModel.Data != null && responsePaymentModel.Data.Count > 0)
                    {
                        responseModel.Data.PaymentMethods = responsePaymentModel.Data;
                    }

                    subscriptionCheckOutModel = responseModel.Data;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return View(subscriptionCheckOutModel);
        }

        [HttpGet]
        public virtual async Task<JsonResult> CreateSubscription()
        {
            var responseModel = new APIResponseModel<CreatePaymentModel>();
            try
            {
                CreatePaymentModel createPaymentModel = new()
                {
                    CustomerIp = _apiHelper.GetUserIP()
                };

                responseModel = await _apiHelper.PostAsync<APIResponseModel<CreatePaymentModel>>("webapi/subscription/createsubscription", createPaymentModel);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return Json(responseModel);
        }
        public async Task<IActionResult> SubscriptionResult(string subscriptionNumber)
        {
            var subscriptionModel = new SubscriptionModel();
            try
            {
                if (string.IsNullOrEmpty(subscriptionNumber))
                {
                    return View(subscriptionModel);
                }

                var responseModel = await _apiHelper.GetAsync<APIResponseModel<List<SubscriptionModel>>>("webapi/subscription/subscriptions?subscriptionNumber=" + subscriptionNumber);
                if (responseModel.Success && responseModel.Data != null && responseModel.Data.Count > 0)
                {
                    subscriptionModel = responseModel.Data[0];
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return View(subscriptionModel);
        }
        public async Task<IActionResult> SubscriptionDetails(string subscriptionNumber)
        {
            var subscriptionModel = new SubscriptionModel();
            try
            {
                if (string.IsNullOrEmpty(subscriptionNumber))
                {
                    return View(subscriptionModel);
                }

                var responseModel = await _apiHelper.GetAsync<APIResponseModel<List<SubscriptionModel>>>("webapi/subscription/subscriptions?subscriptionNumber=" + subscriptionNumber);
                if (responseModel.Success && responseModel.Data != null && responseModel.Data.Count > 0)
                {
                    subscriptionModel = responseModel.Data[0];
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return View(subscriptionModel);
        }
    }
}
