using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Logging;
using Utility.API;
using Utility.Models.Frontend.Sales;
using Utility.ResponseMapper;

namespace Web.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IAPIHelper _apiHelper;
        private readonly ILogger _logger;
        public OrderController(IAPIHelper apiHelper,
            ILoggerFactory logger,
            IRazorViewEngine razorViewEngine) : base(razorViewEngine)
        {
            _apiHelper = apiHelper;
            _logger = logger.CreateLogger(typeof(OrderController).Name);
        }

        /// <summary>
        /// Create order
        /// </summary>
        [HttpPost]
        public virtual async Task<JsonResult> CreateOrder(CreatePaymentModel createPaymentModel)
        {
            var responseModel = new APIResponseModel<CreatePaymentModel>();
            try
            {
                createPaymentModel.CustomerIp = _apiHelper.GetUserIP();
                responseModel = await _apiHelper.PostAsync<APIResponseModel<CreatePaymentModel>>("webapi/order/createorder", createPaymentModel);
                if (responseModel.MessageCode == 401)
                {
                    return Json(responseModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return Json(responseModel);
        }

        /// <summary>
        /// Get order result
        /// </summary>
        [Authorize]
        public async Task<IActionResult> OrderResult(string orderNumber)
        {
            var orderModel = new OrderModel();
            try
            {
                if (string.IsNullOrEmpty(orderNumber))
                {
                    return View(orderModel);
                }

                var responseModel = await _apiHelper.GetAsync<APIResponseModel<List<OrderModel>>>("webapi/order/orders?orderNumber=" + orderNumber);
                if (responseModel.Success && responseModel.Data != null && responseModel.Data.Count > 0)
                {
                    orderModel = responseModel.Data[0];
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return View(orderModel);
        }

        /// <summary>
        /// Get orders
        /// </summary>
        [Authorize]
        public async Task<IActionResult> Orders()
        {
            var orderModels = new List<OrderModel>();
            try
            {
                var responseModel = await _apiHelper.GetAsync<APIResponseModel<List<OrderModel>>>("webapi/order/orders");
                if (responseModel.MessageCode == 401)
                {
                    return RedirectToRoute("login");
                }

                if (responseModel.Success && responseModel.Data != null && responseModel.Data.Count > 0)
                {
                    orderModels = responseModel.Data;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return View(orderModels);
        }

        /// <summary>
        /// Get order details
        /// </summary>
        [Authorize]
        public async Task<IActionResult> OrderDetails(string orderNumber)
        {
            var orderModel = new OrderModel();
            try
            {
                if (string.IsNullOrEmpty(orderNumber))
                {
                    return View(orderModel);
                }

                var responseModel = await _apiHelper.GetAsync<APIResponseModel<List<OrderModel>>>("webapi/order/orders?orderNumber=" + orderNumber);
                if (responseModel.Success && responseModel.Data != null && responseModel.Data.Count > 0)
                {
                    orderModel = responseModel.Data[0];
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return View(orderModel);
        }

        /// <summary>
        /// Get orders
        /// </summary>
        public async Task<JsonResult> OrdersByAjax(int? limit = null, int? page = null)
        {
            var responseModel = new APIResponseModel<List<OrderModel>>();
            try
            {
                responseModel = await _apiHelper.GetAsync<APIResponseModel<List<OrderModel>>>("webapi/order/orders?limit=" + limit + "&page=" + page);
                if (responseModel.Success && responseModel.Data != null && responseModel.Data.Count > 0)
                {
                    return Json(new
                    {
                        html = await RenderPartialViewToStringAsync("_Orders", responseModel.Data),
                        TotalOrderCount = responseModel.DataRecordCount,
                        OrderCount = responseModel.Data.Count,
                        Success = true,
                        MessageCode = 0
                    });
                }
            }
            catch (Exception ex)
            {
                responseModel.Message = ex.Message;
            }

            var orderModels = new List<OrderModel>();
            return Json(new
            {
                html = await RenderPartialViewToStringAsync("_Orders", orderModels),
                TotalOrderCount = 0,
                OrderCount = 0,
                Success = true,
                MessageCode = 0
            });
        }

        /// <summary>
        /// Re order
        /// </summary>
        public async Task<JsonResult> ReOrder(int id)
        {
            var responseModel = new APIResponseModel<bool>();

            try
            {
                responseModel = await _apiHelper.GetAsync<APIResponseModel<bool>>("webapi/order/reorder?id=" + id);
                if (responseModel.MessageCode == 401)
                {
                    responseModel.MessageCode = 401;
                    return Json(responseModel);
                }
            }
            catch (Exception ex)
            {
                responseModel.Message = ex.Message;
            }

            return Json(responseModel);
        }

        /// <summary>
        /// print order
        /// </summary>
        public virtual async Task<JsonResult> PrintOrder(int id)
        {
            var responseModel = new APIResponseModel<object>();
            try
            {
                responseModel = await _apiHelper.GetAsync<APIResponseModel<object>>("webapi/order/getorderpdf?id=" + id);
                if (responseModel.MessageCode == 401)
                {
                    responseModel.MessageCode = 401;
                    return Json(responseModel);
                }
            }
            catch (Exception ex)
            {
                responseModel.Message = ex.Message;
            }

            return Json(responseModel);
        }

        /// <summary>
        /// Get order result by sms
        /// </summary>
        public async Task<IActionResult> OrderResultSMS(string orderNumber)
        {
            var orderModel = new OrderModel();
            try
            {
                if (string.IsNullOrEmpty(orderNumber))
                {
                    return View(orderModel);
                }

                var responseModel = await _apiHelper.GetAsync<APIResponseModel<List<OrderModel>>>("webapi/order/orderbyordernumber?orderNumber=" + orderNumber);
                if (responseModel.Success && responseModel.Data != null && responseModel.Data.Count > 0)
                {
                    orderModel = responseModel.Data[0];

                    var currentLanguage = string.Empty;
                    var customerLanguage = orderModel.CustomerLanguageId == 1 ? "en" : "ar";
                    if (!string.IsNullOrEmpty(CultureInfo.CurrentCulture.Name))
                    {
                        currentLanguage = CultureInfo.CurrentCulture.Name.ToLower();
                    }

                    if (currentLanguage != customerLanguage)
                    {
                        var cultureInfo = new CultureInfo(customerLanguage);
                        Thread.CurrentThread.CurrentUICulture = cultureInfo;
                        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);

                        Response.Cookies.Append(
                        CookieRequestCultureProvider.DefaultCookieName,
                        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(customerLanguage == "en" ? "en-US" : "ar-KW")),
                        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

                        return RedirectToRoute("orderresultsms", new { orderNumber = orderModel.OrderNumber });
                    }
                    else
                    {
                        return View(orderModel);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return View(orderModel);
        }
    }
}
