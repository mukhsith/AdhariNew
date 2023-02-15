using API.Helpers;
using Data.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Services.Frontend.CouponPromotion;
using Services.Frontend.CustomerManagement;
using Services.Frontend.ProductManagement;
using Services.Frontend.Sales;
using Services.Frontend.Shop;
using System;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using Utility.API;
using Utility.Enum;
using Utility.Helpers;
using Utility.Models.Frontend.CustomizedModel;
using Utility.Models.Tabby;
using Utility.ResponseMapper;

namespace API.Areas.Frontend.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IAPIHelper _apiHelper;
        private readonly AppSettingsModel _appSettings;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;
        private readonly ApplicationDbContext _dbcontext;
        private readonly IPromotionService _promotionService;
        private readonly ICustomerService _customerService;
        private readonly IMasterCardHelper _masterCardHelper;
        private readonly IPaymentHelper _paymentHelper;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IWalletPackageService _walletPackageService;
        private readonly IQuickPaymentService _quickPaymentService;
        private readonly ITabbyHelper _tabbyHelper;
        public PaymentController(IAPIHelper apiHelper,
            IOptions<AppSettingsModel> options,
            IOrderService orderService,
            IProductService productService,
            ICartService cartService,
            ICouponService couponService,
            ApplicationDbContext dbcontext,
            IPromotionService promotionService,
            ICustomerService customerService,
            IMasterCardHelper masterCardHelper,
            ICommonHelper commonHelper,
            IPaymentHelper paymentHelper,
            ISubscriptionService subscriptionService,
            IWalletPackageService walletPackageService,
            IQuickPaymentService quickPaymentService,
            ITabbyHelper tabbyHelper)
        {
            _apiHelper = apiHelper;
            _appSettings = options.Value;
            _orderService = orderService;
            _productService = productService;
            _cartService = cartService;
            _couponService = couponService;
            _dbcontext = dbcontext;
            _promotionService = promotionService;
            _customerService = customerService;
            _masterCardHelper = masterCardHelper;
            _paymentHelper = paymentHelper;
            _subscriptionService = subscriptionService;
            _walletPackageService = walletPackageService;
            _quickPaymentService = quickPaymentService;
            _tabbyHelper = tabbyHelper;
        }

        #region Common

        [HttpGet, Route("/Payment/Error")]
        public ActionResult PaymentError()
        {
            try
            {
                return Redirect(_appSettings.WebsiteUrl + "error");
            }
            catch (Exception ex)
            {
                Common.SaveExceptionError(ex);
            }

            return View();
        }
        #endregion

        #region KNET

        /// <summary>
        /// get payment result
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("/Payment/Reciept")]
        public async Task<ActionResult> Reciept(PaymentResponseModel paymentResponseModel)
        {
            try
            {
                string trandata = paymentResponseModel.Trandata;
                string errorText = paymentResponseModel.ErrorText;

                if (!string.IsNullOrEmpty(trandata))
                {
                    paymentResponseModel = await _apiHelper.PostAsync<PaymentResponseModel>("Home/GetPaymentResult", paymentResponseModel,
                        baseUrl: _appSettings.PaymentAPIUrl);
                    if (paymentResponseModel != null)
                    {
                        if (paymentResponseModel.IsExceptionError)
                        {
                            Common.SaveExceptionError(paymentResponseModel.ExceptionErrorText);
                        }

                        paymentResponseModel.BankServiceCharge = _appSettings.KNETFee;
                        var url = await _paymentHelper.UpdatePaymentEntity(paymentResponseModel);
                        if (!string.IsNullOrEmpty(url))
                            return Redirect(url);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.SaveExceptionError(ex);
            }

            return RedirectToAction("PaymentError");
        }

        /// <summary>
        /// get payment error
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/Payment/Reciept")]
        public async Task<ActionResult> Reciept(string values, string errorText)
        {
            try
            {
                if (!string.IsNullOrEmpty(values))
                {
                    string decEntityId = Cryptography.Decrypt(values);
                    var arr = decEntityId.Split('~');
                    if (arr.Length == 2)
                    {
                        string requestType = arr[0];

                        var paymentResponseModel = new PaymentResponseModel();
                        paymentResponseModel.RequestType = requestType;
                        paymentResponseModel.EntityId = arr[1];
                        paymentResponseModel.ErrorText = errorText;
                        var url = await _paymentHelper.UpdatePaymentEntity(paymentResponseModel);
                        if (!string.IsNullOrEmpty(url))
                            return Redirect(url);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.SaveExceptionError(ex);
            }

            return RedirectToAction("PaymentError");
        }
        #endregion

        #region  Master

        //[HttpGet, Route("/payment/createapplepayrequest")]
        //public async Task<APIResponseModel<object>> CreateApplePayRequest(PaymentRequestType requestType, int entityId, string token)
        //{
        //    if (requestType == PaymentRequestType.Order)
        //    {
        //        var order = await _orderService.GetOrderById(entityId);
        //        if (order != null)
        //        {
        //            decimal minimumLimit = Convert.ToDecimal(0.100);
        //            if (order.Total < minimumLimit)
        //            {
        //                return RedirectToAction("PaymentError");
        //            }

        //            request = await _masterCardHelper.CreateRequest(order.Total, order.OrderNumber,
        //                PaymentRequestType.Order.ToString(), entityId, "Order for product sale payment");

        //            referenceNo = order.OrderNumber;
        //        }
        //    }
        //    else if (requestType == PaymentRequestType.SubscriptionOrder)
        //    {
        //        var subscriptionOrder = await _subscriptionService.GetSubscriptionOrderById(entityId);
        //        if (subscriptionOrder != null)
        //        {
        //            decimal minimumLimit = Convert.ToDecimal(0.100);
        //            if (subscriptionOrder.Total < minimumLimit)
        //            {
        //                return RedirectToAction("PaymentError");
        //            }

        //            request = await _masterCardHelper.CreateRequest(subscriptionOrder.Total, subscriptionOrder.OrderNumber,
        //                PaymentRequestType.SubscriptionOrder.ToString(), entityId, "Order for subscription payment");

        //            referenceNo = subscriptionOrder.OrderNumber;
        //        }
        //    }
        //    else if (requestType == PaymentRequestType.WalletPackageOrder)
        //    {
        //        var walletPackageOrder = await _walletPackageService.GetWalletPackageOrderById(entityId);
        //        if (walletPackageOrder != null)
        //        {
        //            decimal minimumLimit = Convert.ToDecimal(0.100);
        //            if (walletPackageOrder.Amount < minimumLimit)
        //            {
        //                return RedirectToAction("PaymentError");
        //            }

        //            request = await _masterCardHelper.CreateRequest(walletPackageOrder.Amount, walletPackageOrder.OrderNumber,
        //                PaymentRequestType.WalletPackageOrder.ToString(), entityId, "Order for wallet package payment");

        //            referenceNo = walletPackageOrder.OrderNumber;
        //        }
        //    }
        //    else if (requestType == PaymentRequestType.QuickPay)
        //    {
        //        var quickPayment = await _quickPaymentService.GetQuickPaymentById(entityId);
        //        if (quickPayment != null)
        //        {
        //            decimal minimumLimit = Convert.ToDecimal(0.100);
        //            if (quickPayment.Amount < minimumLimit)
        //            {
        //                return RedirectToAction("PaymentError");
        //            }

        //            request = await _masterCardHelper.CreateRequest(quickPayment.Amount, quickPayment.PaymentNumber,
        //                PaymentRequestType.QuickPay.ToString(), entityId, "Order for quick payment");

        //            referenceNo = quickPayment.PaymentNumber;
        //        }
        //    }

        //    var request = await _masterCardHelper.CreateApplepayRequest(45, "34324234",
        //                       PaymentRequestType.Order.ToString(), 654645, "Order for product sale payment");
        //    return View();
        //}

        /// <summary>
        /// Create gulf bank master card payment request
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/payment/requestmastercardpayment")]
        public async Task<IActionResult> RequestMasterCardPayment(string value)
        {
            try
            {
                string referenceNo = string.Empty;
                string requestType = string.Empty;
                int entityId = 0;

                var values = Cryptography.Decrypt(value);
                var arrValues = values.Split("-");
                if (arrValues.Length == 2)
                {
                    requestType = Convert.ToString(arrValues[0]);
                    int.TryParse(arrValues[1], out entityId);

                    Tuple<string, string> request = null;

                    if (requestType == PaymentRequestType.Order.ToString())
                    {
                        var order = await _orderService.GetOrderById(entityId);
                        if (order != null)
                        {
                            decimal minimumLimit = Convert.ToDecimal(0.100);
                            if (order.Total < minimumLimit)
                            {
                                return RedirectToAction("PaymentError");
                            }

                            request = await _masterCardHelper.CreateRequest(order.Total, order.OrderNumber,
                                PaymentRequestType.Order.ToString(), entityId, "Order for product sale payment");

                            referenceNo = order.OrderNumber;
                        }
                    }
                    else if (requestType == PaymentRequestType.SubscriptionOrder.ToString())
                    {
                        var subscriptionOrder = await _subscriptionService.GetSubscriptionOrderById(entityId);
                        if (subscriptionOrder != null)
                        {
                            decimal minimumLimit = Convert.ToDecimal(0.100);
                            if (subscriptionOrder.Total < minimumLimit)
                            {
                                return RedirectToAction("PaymentError");
                            }

                            request = await _masterCardHelper.CreateRequest(subscriptionOrder.Total, subscriptionOrder.OrderNumber,
                                PaymentRequestType.SubscriptionOrder.ToString(), entityId, "Order for subscription payment");

                            referenceNo = subscriptionOrder.OrderNumber;
                        }
                    }
                    else if (requestType == PaymentRequestType.WalletPackageOrder.ToString())
                    {
                        var walletPackageOrder = await _walletPackageService.GetWalletPackageOrderById(entityId);
                        if (walletPackageOrder != null)
                        {
                            decimal minimumLimit = Convert.ToDecimal(0.100);
                            if (walletPackageOrder.Amount < minimumLimit)
                            {
                                return RedirectToAction("PaymentError");
                            }

                            request = await _masterCardHelper.CreateRequest(walletPackageOrder.Amount, walletPackageOrder.OrderNumber,
                                PaymentRequestType.WalletPackageOrder.ToString(), entityId, "Order for wallet package payment");

                            referenceNo = walletPackageOrder.OrderNumber;
                        }
                    }
                    else if (requestType == PaymentRequestType.QuickPay.ToString())
                    {
                        var quickPayment = await _quickPaymentService.GetQuickPaymentById(entityId);
                        if (quickPayment != null)
                        {
                            decimal minimumLimit = Convert.ToDecimal(0.100);
                            if (quickPayment.Amount < minimumLimit)
                            {
                                return RedirectToAction("PaymentError");
                            }

                            request = await _masterCardHelper.CreateRequest(quickPayment.Amount, quickPayment.PaymentNumber,
                                PaymentRequestType.QuickPay.ToString(), entityId, "Order for quick payment");

                            referenceNo = quickPayment.PaymentNumber;
                        }
                    }

                    if (request.Item1 == null || request.Item2 == null)
                    {
                        return RedirectToAction("PaymentError");
                    }

                    ViewBag.SessionId = request.Item1;
                    ViewBag.SuccessIndicator = request.Item2;
                    ViewBag.OrderId = referenceNo;
                    ViewBag.MerchantId = _appSettings.MasterCardMerchant;
                    ViewBag.CheckOutUrl = _appSettings.MasterCardUrl + "static/checkout/checkout.min.js";
                    ViewBag.MasterCardInteractionReturnUrl = _appSettings.MasterCardInteractionReturnUrl + referenceNo + "/" + requestType + "/" + entityId;
                }
            }
            catch (Exception ex)
            {
                Common.SaveExceptionError(ex);
                return RedirectToAction("PaymentError");
            }

            return View();
        }

        /// <summary>
        /// Get gulf bank master card payment receipt
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/payment/getmastercardpaymentreceipt/{orderId?}/{type?}/{entityId?}")]
        public async Task<IActionResult> GetMasterCardPaymentReceipt(string orderId = "", string type = "", int entityId = 0)
        {
            try
            {
                var masterCardRootModel = _masterCardHelper.GetResult(orderId);
                if (masterCardRootModel != null)
                {
                    PaymentResponseModel paymentResponseModel = new();
                    paymentResponseModel.Amount = masterCardRootModel.amount.ToString();

                    if (masterCardRootModel.sourceOfFunds != null && masterCardRootModel.sourceOfFunds.provided != null &&
                        masterCardRootModel.sourceOfFunds.provided.card != null)
                    {
                        paymentResponseModel.PaymentId = masterCardRootModel.sourceOfFunds.provided.card.number;
                        paymentResponseModel.CreditCardType = masterCardRootModel.sourceOfFunds.provided.card.scheme.ToLower() == "visa" ? "001" : "002";
                        paymentResponseModel.CreditCardNumber = masterCardRootModel.sourceOfFunds.provided.card.number;
                    }

                    if (masterCardRootModel.transaction != null && masterCardRootModel.transaction.Count > 0)
                    {
                        var paymentTransaction = masterCardRootModel.transaction.Where(a => a.transaction != null && a.transaction.type.ToLower() == "payment").FirstOrDefault();
                        if (paymentTransaction != null)
                        {
                            paymentResponseModel.Auth = paymentTransaction.transaction.authorizationCode;
                            paymentResponseModel.TransId = paymentTransaction.transaction.receipt;
                        }
                    }

                    paymentResponseModel.TrackId = masterCardRootModel.reference;
                    paymentResponseModel.RefId = masterCardRootModel.reference;
                    paymentResponseModel.EntityId = entityId.ToString();
                    paymentResponseModel.RequestType = type;
                    paymentResponseModel.BankServiceCharge = _appSettings.MasterCardFee;
                    paymentResponseModel.BankServiceChargeInPercentage = true;
                    paymentResponseModel.Result = "not+captured";
                    if (!string.IsNullOrEmpty(masterCardRootModel.status) && !string.IsNullOrEmpty(masterCardRootModel.result))
                    {
                        if (masterCardRootModel.status.ToLower() == "captured" && masterCardRootModel.result.ToLower() == "success")
                        {
                            paymentResponseModel.Result = "captured";
                        }
                    }

                    var url = await _paymentHelper.UpdatePaymentEntity(paymentResponseModel);
                    if (!string.IsNullOrEmpty(url))
                        return Redirect(url);
                }
            }
            catch (Exception ex)
            {
                Common.SaveExceptionError(ex);
            }

            return RedirectToAction("PaymentError");
        }
        #endregion

        #region  Tabby

        [HttpGet, Route("/payment/tabby/success")]
        public async Task<IActionResult> TabbySuccessResponse(string payment_id)
        {
            try
            {
                string requestType = string.Empty;
                int entityId = 0;
                var paymentModel = await _tabbyHelper.GetPayment(payment_id);
                if (paymentModel != null)
                {
                    PaymentResponseModel paymentResponseModel = new();
                    paymentResponseModel.Amount = paymentModel.amount.ToString();

                    var arrValues = paymentModel.order.reference_id.Split("-");
                    if (arrValues.Length == 2)
                    {
                        requestType = Convert.ToString(arrValues[0]);
                        int.TryParse(arrValues[1], out entityId);
                    }

                    if (!string.IsNullOrEmpty(requestType) && entityId > 0)
                    {
                        if (paymentModel.status.ToLower() == "authorized")
                        {
                            var paymentCaptureModel = new PaymentCaptureModel
                            {
                                amount = paymentModel.amount.ToString()
                            };

                            paymentModel = await _tabbyHelper.CapturePayment(paymentCaptureModel, payment_id);
                            if (paymentModel.status.ToLower() == "closed")
                            {
                                paymentResponseModel.Result = "captured";
                            }
                        }
                        else
                        {
                            paymentResponseModel.Result = "not+captured";
                        }

                        paymentResponseModel.RequestType = requestType;
                        paymentResponseModel.EntityId = entityId.ToString();

                        var url = await _paymentHelper.UpdatePaymentEntity(paymentResponseModel);
                        if (!string.IsNullOrEmpty(url))
                            return Redirect(url);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.SaveExceptionError(ex);
            }

            return RedirectToAction("PaymentError");
        }

        [HttpGet, Route("/payment/tabby/cancel")]
        public async Task<IActionResult> TabbyCancelResponse(string payment_id)
        {
            try
            {
                string requestType = string.Empty;
                int entityId = 0;
                var paymentModel = await _tabbyHelper.GetPayment(payment_id);
                if (paymentModel != null)
                {
                    PaymentResponseModel paymentResponseModel = new();
                    paymentResponseModel.Amount = paymentModel.amount.ToString();

                    var arrValues = paymentModel.order.reference_id.Split("-");
                    if (arrValues.Length == 2)
                    {
                        requestType = Convert.ToString(arrValues[0]);
                        int.TryParse(arrValues[1], out entityId);
                    }

                    if (!string.IsNullOrEmpty(requestType) && entityId > 0)
                    {
                        paymentResponseModel.Result = "not+captured";
                        paymentResponseModel.RequestType = requestType;
                        paymentResponseModel.EntityId = entityId.ToString();

                        var url = await _paymentHelper.UpdatePaymentEntity(paymentResponseModel);
                        if (!string.IsNullOrEmpty(url))
                            return Redirect(url);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.SaveExceptionError(ex);
            }

            return RedirectToAction("PaymentError");
        }

        [HttpGet, Route("/payment/tabby/failure")]
        public async Task<IActionResult> TabbyFailureResponse(string payment_id)
        {
            try
            {
                string requestType = string.Empty;
                int entityId = 0;
                var paymentModel = await _tabbyHelper.GetPayment(payment_id);
                if (paymentModel != null)
                {
                    PaymentResponseModel paymentResponseModel = new();
                    paymentResponseModel.Amount = paymentModel.amount.ToString();

                    var arrValues = paymentModel.order.reference_id.Split("-");
                    if (arrValues.Length == 2)
                    {
                        requestType = Convert.ToString(arrValues[0]);
                        int.TryParse(arrValues[1], out entityId);
                    }

                    if (!string.IsNullOrEmpty(requestType) && entityId > 0)
                    {
                        paymentResponseModel.Result = "not+captured";
                        paymentResponseModel.RequestType = requestType;
                        paymentResponseModel.EntityId = entityId.ToString();

                        var url = await _paymentHelper.UpdatePaymentEntity(paymentResponseModel);
                        if (!string.IsNullOrEmpty(url))
                            return Redirect(url);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.SaveExceptionError(ex);
            }

            return RedirectToAction("PaymentError");
        }
        #endregion      
    }
}
