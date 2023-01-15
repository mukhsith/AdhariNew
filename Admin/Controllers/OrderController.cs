using Admin.Models;
using Data.Common;
using Data.CouponPromotion;
using Data.CustomerManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using Utility.Models.Admin.Sales;
using Utility.Models.Admin.WalletPackage;
using Utility.Models.Frontend.Sales;
using Utility.ResponseMapper;

namespace Admin.Controllers
{

    public class OrderController : BaseController
    {
        private readonly IAPIHelper _apiHelper;
        public OrderController(IAPIHelper apiHelper, IHttpContextAccessor httpContextAccessor, IOptions<AppSettingsModel> options, ILoggerFactory logger) :
            base(httpContextAccessor, options, logger.CreateLogger(typeof(CustomerController).Name))
        {
            _apiHelper = apiHelper;
        }

        //[HttpPost]
        //public async Task<IActionResult> SalesOrdersByCustomer(int customerId)
        //{
        //    //today sales statistics
        //    DailyOrderSummaryModel responseModel = new();

        //    responseModel = await _apiHelper.GetAsync<DailyOrderSummaryModel>("Order/TodaySales", "");
        //    responseModel.customerId = customerId;
        //    return View("SalesOrders",responseModel);
        //}

        public async Task<IActionResult> SalesOrders(int? customerId = null)
        {
            //today sales statistics
            DailyOrderSummaryModel responseModel = new();

            responseModel = await _apiHelper.GetAsync<DailyOrderSummaryModel>("Order/TodaySales", "");
            responseModel.CustomerId = customerId;
            return View(responseModel);
        }



        public async Task<IActionResult> OrderDetails(int id, int customerId)
        {
            // return  View(new OrderParamModel (){ Id = id, CustomerId=customerId });
            //APIResponseModel<AdminOrderModel> responseModel = await _apiHelper.GetAsync<APIResponseModel<AdminOrderModel>>("order/orderDetails?id=" + id + "&customerId=" + customerId, "");
            APIResponseModel<Utility.Models.Frontend.Sales.OrderModel> responseModel = await _apiHelper.GetAsync<APIResponseModel<Utility.Models.Frontend.Sales.OrderModel>>("order/orderDetails?id=" + id + "&customerId=" + customerId, "");
            //if (responseModel.Success && responseModel.Data != null)
            //{
            //    return View(responseModel);
            //}

            return View(responseModel);
        }


        [HttpGet, Route("order/salesSubscriptions")]
        public async Task<IActionResult> SalesSubscriptions(int? customerId = null)
        {
            //today sales statistics
            DailySubscriptionSummaryModel responseModel = new();

            responseModel = await _apiHelper.GetAsync<DailySubscriptionSummaryModel>("Order/SubscriptionTodaySales", "");
            responseModel.CustomerId = customerId;
            return View(responseModel);
        }

        [HttpGet, Route("order/sales-subscriptionDetails")]
        public async Task<IActionResult> SubscriptionDetails(int id, int customerId, string subscriptionNumber)
        {
            //today sales statistics
            APIResponseModel<List<SubscriptionAdminModel>> responseModel = new();

            responseModel = await _apiHelper.GetAsync<APIResponseModel<List<SubscriptionAdminModel>>>("webapi/subscription/subscriptionsadmin?id=" + id, _appSettings.APIBaseUrl2);
            //if (responseModel.Data)
            // {
            return View(responseModel.Data[0]);
            //}
            // return View(responseModel.Data);
        }
        public IActionResult Subscriptions()
        {
            return View();
        }
        public IActionResult CreateOrder()
        {
            return View();
        }

        public async Task<IActionResult> PrepaidCardSales()
        {
            WalletPackageHeader responseModel = await _apiHelper.GetAsync<WalletPackageHeader>("WalletPackage/GetWalletPackageOrderHeader", "");


            return View(responseModel);

        }
        public IActionResult PrepaidCardSaleDetials(int id)
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
