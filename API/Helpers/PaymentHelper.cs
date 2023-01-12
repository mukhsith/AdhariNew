using API.Areas.Frontend.Helpers;
using Data.CustomerManagement;
using Data.EntityFramework;
using Microsoft.Extensions.Options;
using Services.Frontend.CouponPromotion.Interface;
using Services.Frontend.CustomerManagement;
using Services.Frontend.ProductManagement.Interface;
using Services.Frontend.Sales;
using Services.Frontend.Shop;
using System;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;
using Utility.Helpers;
using Utility.Models.Frontend.CustomizedModel;
using Utility.Models.Tabby;

namespace API.Helpers
{
    public class PaymentHelper : IPaymentHelper
    {
        private readonly AppSettingsModel _appSettings;
        private readonly ApplicationDbContext _dbcontext;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;
        private readonly ICustomerService _customerService;
        private readonly ICommonHelper _commonHelper;
        private readonly IPromotionService _promotionService;
        private readonly IModelHelper _modelHelper;
        private readonly IWalletPackageService _walletPackageService;
        private readonly IQuickPaymentService _quickPaymentService;
        public PaymentHelper(IOptions<AppSettingsModel> options,
            ApplicationDbContext dbcontext,
            IProductService productService,
            IOrderService orderService,
            ISubscriptionService subscriptionService,
            ICartService cartService,
            ICouponService couponService,
            ICustomerService customerService,
            ICommonHelper commonHelper,
            IPromotionService promotionService,
            IModelHelper modelHelper,
            IWalletPackageService walletPackageService,
            IQuickPaymentService quickPaymentService)
        {
            _appSettings = options.Value;
            _dbcontext = dbcontext;
            _productService = productService;
            _orderService = orderService;
            _subscriptionService = subscriptionService;
            _cartService = cartService;
            _couponService = couponService;
            _customerService = customerService;
            _commonHelper = commonHelper;
            _promotionService = promotionService;
            _modelHelper = modelHelper;
            _walletPackageService = walletPackageService;
            _quickPaymentService = quickPaymentService;
        }
        public async Task<string> UpdatePaymentEntity(PaymentResponseModel paymentResponseModel)
        {
            using (var dbContextTransaction = _dbcontext.Database.BeginTransaction())
            {
                try
                {
                    decimal.TryParse(paymentResponseModel.Amount, out decimal paymentAmount);
                    if (paymentResponseModel.RequestType == PaymentRequestType.Order.ToString())
                    {
                        int.TryParse(paymentResponseModel.EntityId, out int orderId);
                        if (orderId > 0)
                        {
                            var order = await _orderService.GetOrderById(orderId);
                            if (order != null)
                            {
                                order.PaymentId = paymentResponseModel.PaymentId;
                                if (string.IsNullOrEmpty(paymentResponseModel.Result))
                                {
                                    order.PaymentStatusId = PaymentStatus.NotCaptured;
                                    order.PaymentResult = "not+captured";
                                    order.OrderStatusId = OrderStatus.Cancelled;
                                }
                                else
                                {
                                    if (paymentResponseModel.Result.ToLower() == "captured" && order.Total == paymentAmount)
                                    {
                                        if (paymentResponseModel.BankServiceChargeInPercentage)
                                            order.BankServiceCharge = (paymentAmount * paymentResponseModel.BankServiceCharge) / 100;
                                        else
                                            order.BankServiceCharge = paymentResponseModel.BankServiceCharge;

                                        order.PaymentStatusId = order.PaymentMethodId == (int)PaymentMethod.Cash ? PaymentStatus.PendingCash :
                                            PaymentStatus.Captured;
                                        order.OrderStatusId = OrderStatus.Confirmed;
                                        order.PaymentResult = "captured";
                                    }
                                    else if (paymentResponseModel.Result.ToLower() == "not+captured")
                                    {
                                        order.PaymentStatusId = PaymentStatus.NotCaptured;
                                        order.OrderStatusId = OrderStatus.Cancelled;
                                        order.PaymentResult = "not+captured";
                                    }
                                    else if (paymentResponseModel.Result.ToLower() == "canceled")
                                    {
                                        order.PaymentStatusId = PaymentStatus.Canceled;
                                        order.OrderStatusId = OrderStatus.Cancelled;
                                        order.PaymentResult = "canceled";
                                    }
                                }

                                order.PaymentAuth = paymentResponseModel.Auth;
                                order.PaymentId = paymentResponseModel.PaymentId;
                                order.PaymentRefId = paymentResponseModel.RefId;
                                order.PaymentTrackId = paymentResponseModel.TrackId;
                                order.PaymentTransId = paymentResponseModel.TransId;
                                order.CreditCardType = paymentResponseModel.CreditCardType;
                                order.CreditCardNumber = paymentResponseModel.CreditCardNumber;
                                order.PaymentError = paymentResponseModel.IsExceptionError ? paymentResponseModel.ExceptionErrorText : paymentResponseModel.ErrorText;
                                order.PaymentDateTime = DateTime.Now;
                                order.ModifiedOn = DateTime.Now;
                                await _orderService.UpdateOrder(order);

                                string url;
                                if (order.DeviceTypeId == DeviceType.Web)
                                    url = _appSettings.WebsiteUrl + "ORD/" + order.OrderNumber;
                                else
                                    url = _appSettings.WebsiteUrl + "paymentresult";

                                if (order.PaymentStatusId == PaymentStatus.Captured || order.PaymentStatusId == PaymentStatus.PendingCash)
                                {
                                    foreach (var orderItem in order.OrderItems)
                                    {
                                        if (orderItem.Product.ProductType == ProductType.BaseProduct)
                                        {
                                            await _productService.AdjustStockQuantity(product: orderItem.Product, stock: orderItem.Quantity,
                                             relatedEntityType: RelatedEntityType.Order, relatedEntityId: orderItem.Id,
                                             productActionType: ProductActionType.RemoveFromStock);
                                        }
                                        else if (orderItem.Product.ProductType == ProductType.BundledProduct)
                                        {
                                            foreach (var orderItemDetail in orderItem.OrderItemDetails)
                                            {
                                                var childProduct = await _productService.GetById(orderItemDetail.ChildProductId);
                                                if (childProduct != null)
                                                {
                                                    await _productService.AdjustStockQuantity(product: childProduct,
                                                        stock: orderItem.Quantity * orderItemDetail.Quantity,
                                                       relatedEntityType: RelatedEntityType.Order, relatedEntityId: orderItem.Id,
                                                       productActionType: ProductActionType.RemoveFromStock);
                                                }
                                            }
                                        }
                                    }

                                    var coupon = order.Coupon;
                                    if (coupon != null)
                                    {
                                        coupon.QuantityUsed++;
                                        await _couponService.UpdateCoupon(coupon);
                                    }

                                    if (order.CashbackAmount > 0)
                                    {
                                        var walletTransaction = new WalletTransaction
                                        {
                                            TransactionNo = Common.GenerateRandomNo(10000000, 99999999),
                                            CreatedBy = order.CustomerId,
                                            CustomerId = order.CustomerId,
                                            WalletTypeId = WalletType.Cashback,
                                            RelatedEntityTypeId = RelatedEntityType.Order,
                                            RelatedEntityId = order.Id,
                                            Credit = 0,
                                            Debit = order.CashbackAmount,
                                            WalletTransactionTypeId = WalletTransactionType.CashbackRedeem
                                        };

                                        await _customerService.CreateWalletTransaction(walletTransaction);

                                        await _commonHelper.RedeemCashbackAmount(order.CustomerId, order.CashbackAmount);
                                    }

                                    if (order.WalletUsedAmount > 0)
                                    {
                                        var walletBalance = await _customerService.GetWalletBalanceByCustomerId(order.CustomerId, WalletType.Wallet);
                                        var walletTransaction = new WalletTransaction
                                        {
                                            TransactionNo = Common.GenerateRandomNo(10000000, 99999999),
                                            CreatedBy = order.CustomerId,
                                            CustomerId = order.CustomerId,
                                            WalletTypeId = WalletType.Wallet,
                                            RelatedEntityTypeId = RelatedEntityType.Order,
                                            RelatedEntityId = order.Id,
                                            Credit = 0,
                                            Debit = order.WalletUsedAmount,
                                            WalletTransactionTypeId = WalletTransactionType.UseWalletAmount,
                                            Balance = walletBalance - order.WalletUsedAmount
                                        };

                                        await _customerService.CreateWalletTransaction(walletTransaction);
                                    }

                                    var promotion = await _promotionService.GetDefault();
                                    if (promotion != null && promotion.PromotionEnabled(order.Total))
                                    {
                                        int noOfTimes = (int)(order.Total / promotion.CashbackOnPurchaseMinOrderAmount);
                                        var cashbackAmount = noOfTimes * promotion.CashbackOnPurchaseValue;

                                        var walletTransaction = new WalletTransaction
                                        {
                                            TransactionNo = Common.GenerateRandomNo(10000000, 99999999),
                                            CreatedBy = order.CustomerId,
                                            CustomerId = order.CustomerId,
                                            WalletTypeId = WalletType.Cashback,
                                            RelatedEntityTypeId = RelatedEntityType.Order,
                                            RelatedEntityId = order.Id,
                                            Credit = cashbackAmount,
                                            RemainingCredit = cashbackAmount,
                                            Debit = 0,
                                            WalletTransactionTypeId = WalletTransactionType.CashbackOnPurchase,
                                            ExpiryDate = promotion.CashbackOnPurchaseExpiryInNoOfDays > 0 ? DateTime.Now.AddDays(promotion.CashbackOnPurchaseExpiryInNoOfDays).Date : null
                                        };

                                        await _customerService.CreateWalletTransaction(walletTransaction);

                                        order.ReceivedCashbackAmount = cashbackAmount;
                                        await _orderService.UpdateOrder(order);
                                    }

                                    await _cartService.DeleteCartAttributeByCustomer(customerId: order.CustomerId);
                                    await _cartService.DeleteCartItemByCustomer(customerId: order.CustomerId);
                                }
                                else if (order.PaymentStatusId == PaymentStatus.NotCaptured)
                                {
                                    await _cartService.HoldAndReleaseCartItem(customerId: order.CustomerId, isHold: false);
                                }
                                else if (order.PaymentStatusId == PaymentStatus.Canceled)
                                {
                                    await _cartService.HoldAndReleaseCartItem(customerId: order.CustomerId, isHold: false);
                                }

                                await dbContextTransaction.CommitAsync();

                                try
                                {
                                    bool isEnglish = order.CustomerLanguageId == 1;
                                    var orderModel = await _modelHelper.PrepareOrderModel(order: order, isEnglish: isEnglish, loadDetails: true);
                                    await _commonHelper.SendOrderSMSNotification(orderModel: orderModel, isEnglish: isEnglish);
                                    if (!string.IsNullOrEmpty(orderModel.Customer.EmailAddress))
                                        await _commonHelper.SendOrderEmailNotification(orderModel: orderModel, isEnglish: isEnglish);
                                }
                                catch { }

                                return url;
                            }
                        }
                    }
                    else if (paymentResponseModel.RequestType == PaymentRequestType.SubscriptionOrder.ToString())
                    {
                        int.TryParse(paymentResponseModel.EntityId, out int subscriptionOrderId);
                        if (subscriptionOrderId > 0)
                        {
                            var subscriptionOrder = await _subscriptionService.GetSubscriptionOrderById(subscriptionOrderId);
                            if (subscriptionOrder != null)
                            {
                                var subscription = subscriptionOrder.Subscription;
                                subscriptionOrder.PaymentId = paymentResponseModel.PaymentId;
                                if (string.IsNullOrEmpty(paymentResponseModel.Result))
                                {
                                    subscriptionOrder.PaymentStatusId = PaymentStatus.NotCaptured;
                                    subscriptionOrder.PaymentResult = "not+captured";
                                }
                                else
                                {
                                    if (paymentResponseModel.Result.ToLower() == "captured" && subscriptionOrder.Subscription.Total == paymentAmount)
                                    {
                                        if (paymentResponseModel.BankServiceChargeInPercentage)
                                            subscriptionOrder.BankServiceCharge = (paymentAmount * paymentResponseModel.BankServiceCharge) / 100;
                                        else
                                            subscriptionOrder.BankServiceCharge = paymentResponseModel.BankServiceCharge;

                                        subscriptionOrder.PaymentStatusId = subscriptionOrder.PaymentMethodId == (int)PaymentMethod.Cash ? PaymentStatus.PendingCash :
                                            PaymentStatus.Captured;
                                        subscriptionOrder.Confirmed = true;

                                        subscriptionOrder.Subscription.PaidInitialPayment = true;
                                        subscriptionOrder.Subscription.SubscriptionStatusId = SubscriptionStatus.Confirmed;
                                        subscriptionOrder.PaymentResult = "captured";
                                    }
                                    else if (paymentResponseModel.Result.ToLower() == "not+captured")
                                    {
                                        subscriptionOrder.PaymentStatusId = PaymentStatus.NotCaptured;
                                        subscriptionOrder.PaymentResult = "not+captured";
                                    }
                                    else if (paymentResponseModel.Result.ToLower() == "canceled")
                                    {
                                        subscriptionOrder.PaymentStatusId = PaymentStatus.Canceled;
                                        subscriptionOrder.PaymentResult = "canceled";
                                    }
                                }
                                subscriptionOrder.PaymentAuth = paymentResponseModel.Auth;
                                subscriptionOrder.PaymentId = paymentResponseModel.PaymentId;
                                subscriptionOrder.PaymentRefId = paymentResponseModel.RefId;
                                subscriptionOrder.PaymentTrackId = paymentResponseModel.TrackId;
                                subscriptionOrder.PaymentTransId = paymentResponseModel.TransId;
                                subscriptionOrder.CreditCardType = paymentResponseModel.CreditCardType;
                                subscriptionOrder.CreditCardNumber = paymentResponseModel.CreditCardNumber;
                                subscriptionOrder.PaymentError = paymentResponseModel.IsExceptionError ? paymentResponseModel.ExceptionErrorText : paymentResponseModel.ErrorText;
                                subscriptionOrder.PaymentDateTime = DateTime.Now;
                                subscriptionOrder.ModifiedOn = DateTime.Now;
                                await _subscriptionService.UpdateSubscriptionOrder(subscriptionOrder);

                                string url;
                                if (subscription.DeviceTypeId == DeviceType.Web)
                                    url = _appSettings.WebsiteUrl + "SUB/" + subscription.SubscriptionNumber;
                                else
                                    url = _appSettings.WebsiteUrl + "paymentresult";

                                if (subscriptionOrder.Confirmed)
                                {
                                    var subscriptionItemDetails = subscription.SubscriptionItemDetails;
                                    foreach (var subscriptionItemDetail in subscriptionItemDetails)
                                    {
                                        var childProduct = await _productService.GetById(subscriptionItemDetail.ChildProductId);
                                        if (childProduct != null)
                                        {
                                            await _productService.AdjustStockQuantity(product: childProduct,
                                                stock: subscription.Quantity * subscriptionItemDetail.Quantity,
                                               relatedEntityType: RelatedEntityType.SubscriptionOrder, relatedEntityId: subscriptionOrder.Id,
                                               productActionType: ProductActionType.RemoveFromStock);
                                        }
                                    }

                                    var coupon = subscription.Coupon;
                                    if (coupon != null)
                                    {
                                        coupon.QuantityUsed++;
                                        await _couponService.UpdateCoupon(coupon);
                                    }

                                    if (subscription.CashbackAmount > 0)
                                    {
                                        var walletTransaction = new WalletTransaction
                                        {
                                            TransactionNo = Common.GenerateRandomNo(10000000, 99999999),
                                            CreatedBy = subscription.CustomerId,
                                            CustomerId = subscription.CustomerId,
                                            WalletTypeId = WalletType.Cashback,
                                            RelatedEntityTypeId = RelatedEntityType.Subscription,
                                            RelatedEntityId = subscription.Id,
                                            Credit = 0,
                                            Debit = subscription.CashbackAmount,
                                            WalletTransactionTypeId = WalletTransactionType.CashbackRedeem
                                        };

                                        await _customerService.CreateWalletTransaction(walletTransaction);

                                        await _commonHelper.RedeemCashbackAmount(subscription.CustomerId, subscription.CashbackAmount);
                                    }

                                    if (subscription.WalletUsedAmount > 0)
                                    {
                                        var walletBalance = await _customerService.GetWalletBalanceByCustomerId(subscription.CustomerId, WalletType.Wallet);
                                        var walletTransaction = new WalletTransaction
                                        {
                                            TransactionNo = Common.GenerateRandomNo(10000000, 99999999),
                                            CreatedBy = subscription.CustomerId,
                                            CustomerId = subscription.CustomerId,
                                            WalletTypeId = WalletType.Wallet,
                                            RelatedEntityTypeId = RelatedEntityType.Subscription,
                                            RelatedEntityId = subscription.Id,
                                            Credit = 0,
                                            Debit = subscription.WalletUsedAmount,
                                            WalletTransactionTypeId = WalletTransactionType.UseWalletAmount,
                                            Balance = walletBalance - subscription.WalletUsedAmount
                                        };

                                        await _customerService.CreateWalletTransaction(walletTransaction);
                                    }

                                    var promotion = await _promotionService.GetDefault();
                                    if (promotion != null && promotion.PromotionEnabled(subscription.Total))
                                    {
                                        int noOfTimes = (int)(subscription.Total / promotion.CashbackOnPurchaseMinOrderAmount);
                                        var cashbackAmount = noOfTimes * promotion.CashbackOnPurchaseValue;

                                        var walletTransaction = new WalletTransaction
                                        {
                                            TransactionNo = Common.GenerateRandomNo(10000000, 99999999),
                                            CreatedBy = subscription.CustomerId,
                                            CustomerId = subscription.CustomerId,
                                            WalletTypeId = WalletType.Cashback,
                                            RelatedEntityTypeId = RelatedEntityType.Subscription,
                                            RelatedEntityId = subscription.Id,
                                            Credit = cashbackAmount,
                                            RemainingCredit = cashbackAmount,
                                            Debit = 0,
                                            WalletTransactionTypeId = WalletTransactionType.CashbackOnPurchase,
                                            ExpiryDate = promotion.CashbackOnPurchaseExpiryInNoOfDays > 0 ? DateTime.Now.AddDays(promotion.CashbackOnPurchaseExpiryInNoOfDays).Date : null
                                        };

                                        await _customerService.CreateWalletTransaction(walletTransaction);

                                        subscription.ReceivedCashbackAmount = cashbackAmount;
                                        await _subscriptionService.UpdateSubscription(subscription);
                                    }

                                    await _cartService.DeleteSubscriptionAttributeByCustomer(customerId: subscription.CustomerId);
                                }

                                await _cartService.DeleteSubscriptionHoldingByCustomer(customerId: subscription.CustomerId);
                                await dbContextTransaction.CommitAsync();

                                if (subscriptionOrder.Confirmed)
                                {
                                    try
                                    {
                                        bool isEnglish = subscription.CustomerLanguageId == 1;
                                        var subscriptionModel = await _modelHelper.PrepareSubscriptionModel(subscription: subscription, isEnglish: isEnglish, loadDetails: true);
                                        await _commonHelper.SendSubscriptionSMSNotification(subscriptionModel: subscriptionModel, isEnglish: isEnglish);
                                        if (!string.IsNullOrEmpty(subscriptionModel.Customer.EmailAddress))
                                            await _commonHelper.SendSubscriptionEmailNotification(subscriptionModel: subscriptionModel, isEnglish: isEnglish);
                                    }
                                    catch { }
                                }

                                return url;
                            }
                        }
                    }
                    else if (paymentResponseModel.RequestType == PaymentRequestType.WalletPackageOrder.ToString())
                    {
                        int.TryParse(paymentResponseModel.EntityId, out int walletPackageOrderId);
                        if (walletPackageOrderId > 0)
                        {
                            var walletPackageOrder = await _walletPackageService.GetWalletPackageOrderById(walletPackageOrderId);
                            if (walletPackageOrder != null)
                            {
                                walletPackageOrder.PaymentId = paymentResponseModel.PaymentId;
                                if (string.IsNullOrEmpty(paymentResponseModel.Result))
                                {
                                    walletPackageOrder.PaymentStatusId = PaymentStatus.NotCaptured;
                                    walletPackageOrder.PaymentResult = "not+captured";
                                }
                                else
                                {
                                    if (paymentResponseModel.Result.ToLower() == "captured" && walletPackageOrder.Amount == paymentAmount)
                                    {
                                        if (paymentResponseModel.BankServiceChargeInPercentage)
                                            walletPackageOrder.BankServiceCharge = (paymentAmount * paymentResponseModel.BankServiceCharge) / 100;
                                        else
                                            walletPackageOrder.BankServiceCharge = paymentResponseModel.BankServiceCharge;

                                        walletPackageOrder.PaymentStatusId = PaymentStatus.Captured;
                                        walletPackageOrder.PaymentResult = "captured";
                                    }
                                    else if (paymentResponseModel.Result.ToLower() == "not+captured")
                                    {
                                        walletPackageOrder.PaymentStatusId = PaymentStatus.NotCaptured;
                                        walletPackageOrder.PaymentResult = "not+captured";
                                    }
                                    else if (paymentResponseModel.Result.ToLower() == "canceled")
                                    {
                                        walletPackageOrder.PaymentStatusId = PaymentStatus.Canceled;
                                        walletPackageOrder.PaymentResult = "canceled";
                                    }
                                }
                                walletPackageOrder.PaymentAuth = paymentResponseModel.Auth;
                                walletPackageOrder.PaymentId = paymentResponseModel.PaymentId;
                                walletPackageOrder.PaymentRefId = paymentResponseModel.RefId;
                                walletPackageOrder.PaymentTrackId = paymentResponseModel.TrackId;
                                walletPackageOrder.PaymentTransId = paymentResponseModel.TransId;
                                walletPackageOrder.CreditCardType = paymentResponseModel.CreditCardType;
                                walletPackageOrder.CreditCardNumber = paymentResponseModel.CreditCardNumber;
                                walletPackageOrder.PaymentError = paymentResponseModel.IsExceptionError ? paymentResponseModel.ExceptionErrorText : paymentResponseModel.ErrorText;
                                walletPackageOrder.PaymentDateTime = DateTime.Now;
                                walletPackageOrder.ModifiedOn = DateTime.Now;
                                await _walletPackageService.UpdateWalletPackageOrder(walletPackageOrder);

                                string url;
                                if (walletPackageOrder.DeviceTypeId == DeviceType.Web)
                                    url = _appSettings.WebsiteUrl + "WPP/" + walletPackageOrder.OrderNumber;
                                else
                                    url = _appSettings.WebsiteUrl + "paymentresult";

                                if (walletPackageOrder.PaymentStatusId == PaymentStatus.Captured)
                                {
                                    var walletBalance = await _customerService.GetWalletBalanceByCustomerId(walletPackageOrder.CustomerId, WalletType.Wallet);
                                    var walletTransaction = new WalletTransaction
                                    {
                                        TransactionNo = Common.GenerateRandomNo(10000000, 99999999),
                                        CreatedBy = walletPackageOrder.CustomerId,
                                        CustomerId = walletPackageOrder.CustomerId,
                                        WalletTypeId = WalletType.Wallet,
                                        RelatedEntityTypeId = RelatedEntityType.WalletPackageOrder,
                                        RelatedEntityId = walletPackageOrder.Id,
                                        Credit = walletPackageOrder.WalletAmount,
                                        Debit = 0,
                                        WalletTransactionTypeId = WalletTransactionType.TopUp,
                                        Balance = walletBalance + walletPackageOrder.WalletAmount
                                    };

                                    await _customerService.CreateWalletTransaction(walletTransaction);
                                }

                                await dbContextTransaction.CommitAsync();

                                return url;
                            }
                        }
                    }
                    else if (paymentResponseModel.RequestType == PaymentRequestType.QuickPay.ToString())
                    {
                        int.TryParse(paymentResponseModel.EntityId, out int quickPaymentId);
                        if (quickPaymentId > 0)
                        {
                            var quickPayment = await _quickPaymentService.GetQuickPaymentById(quickPaymentId);
                            if (quickPayment != null)
                            {
                                quickPayment.PaymentId = paymentResponseModel.PaymentId;
                                if (string.IsNullOrEmpty(paymentResponseModel.Result))
                                {
                                    quickPayment.PaymentStatusId = PaymentStatus.NotCaptured;
                                    quickPayment.PaymentResult = "not+captured";
                                }
                                else
                                {
                                    if (paymentResponseModel.Result.ToLower() == "captured" && quickPayment.Amount == paymentAmount)
                                    {
                                        if (paymentResponseModel.BankServiceChargeInPercentage)
                                            quickPayment.BankServiceCharge = (paymentAmount * paymentResponseModel.BankServiceCharge) / 100;
                                        else
                                            quickPayment.BankServiceCharge = paymentResponseModel.BankServiceCharge;

                                        quickPayment.PaymentStatusId = PaymentStatus.Captured;
                                        quickPayment.PaymentResult = "captured";
                                    }
                                    else if (paymentResponseModel.Result.ToLower() == "not+captured")
                                    {
                                        quickPayment.PaymentStatusId = PaymentStatus.NotCaptured;
                                        quickPayment.PaymentResult = "not+captured";
                                    }
                                    else if (paymentResponseModel.Result.ToLower() == "canceled")
                                    {
                                        quickPayment.PaymentStatusId = PaymentStatus.Canceled;
                                        quickPayment.PaymentResult = "canceled";
                                    }
                                }
                                quickPayment.PaymentAuth = paymentResponseModel.Auth;
                                quickPayment.PaymentId = paymentResponseModel.PaymentId;
                                quickPayment.PaymentRefId = paymentResponseModel.RefId;
                                quickPayment.PaymentTrackId = paymentResponseModel.TrackId;
                                quickPayment.PaymentTransId = paymentResponseModel.TransId;
                                quickPayment.CreditCardType = paymentResponseModel.CreditCardType;
                                quickPayment.CreditCardNumber = paymentResponseModel.CreditCardNumber;
                                quickPayment.PaymentError = paymentResponseModel.IsExceptionError ? paymentResponseModel.ExceptionErrorText : paymentResponseModel.ErrorText;
                                quickPayment.PaymentDateTime = DateTime.Now;
                                quickPayment.ModifiedOn = DateTime.Now;
                                quickPayment.Used = true;
                                await _quickPaymentService.UpdateQuickPayment(quickPayment);

                                if (quickPayment.PaymentStatusId == PaymentStatus.Captured)
                                {
                                    if (quickPayment.PaymentRequestTypeId == PaymentRequestType.Order)
                                    {
                                        var order = await _orderService.GetOrderById(quickPayment.EntityId);
                                        if (order != null)
                                        {
                                            if (paymentResponseModel.BankServiceChargeInPercentage)
                                                order.BankServiceCharge = (paymentAmount * paymentResponseModel.BankServiceCharge) / 100;
                                            else
                                                order.BankServiceCharge = paymentResponseModel.BankServiceCharge;

                                            order.PaymentId = paymentResponseModel.PaymentId;
                                            order.PaymentStatusId = PaymentStatus.Captured;
                                            order.PaymentResult = paymentResponseModel.Result;
                                            order.PaymentAuth = paymentResponseModel.Auth;
                                            order.PaymentId = paymentResponseModel.PaymentId;
                                            order.PaymentRefId = paymentResponseModel.RefId;
                                            order.PaymentTrackId = paymentResponseModel.TrackId;
                                            order.PaymentTransId = paymentResponseModel.TransId;
                                            order.CreditCardType = paymentResponseModel.CreditCardType;
                                            order.CreditCardNumber = paymentResponseModel.CreditCardNumber;
                                            order.PaymentError = paymentResponseModel.IsExceptionError ? paymentResponseModel.ExceptionErrorText : paymentResponseModel.ErrorText;
                                            order.PaymentDateTime = DateTime.Now;
                                            order.ModifiedOn = DateTime.Now;
                                            await _orderService.UpdateOrder(order);
                                        }
                                    }
                                    else if (quickPayment.PaymentRequestTypeId == PaymentRequestType.SubscriptionOrder)
                                    {
                                        var subscriptionOrder = await _subscriptionService.GetSubscriptionOrderById(quickPayment.EntityId);
                                        if (subscriptionOrder != null)
                                        {
                                            if (paymentResponseModel.BankServiceChargeInPercentage)
                                                subscriptionOrder.BankServiceCharge = (paymentAmount * paymentResponseModel.BankServiceCharge) / 100;
                                            else
                                                subscriptionOrder.BankServiceCharge = paymentResponseModel.BankServiceCharge;

                                            subscriptionOrder.PaymentStatusId = PaymentStatus.Captured;
                                            subscriptionOrder.Confirmed = true;
                                            subscriptionOrder.PaymentId = paymentResponseModel.PaymentId;
                                            subscriptionOrder.PaymentResult = paymentResponseModel.Result;
                                            subscriptionOrder.PaymentAuth = paymentResponseModel.Auth;
                                            subscriptionOrder.PaymentId = paymentResponseModel.PaymentId;
                                            subscriptionOrder.PaymentRefId = paymentResponseModel.RefId;
                                            subscriptionOrder.PaymentTrackId = paymentResponseModel.TrackId;
                                            subscriptionOrder.PaymentTransId = paymentResponseModel.TransId;
                                            subscriptionOrder.CreditCardType = paymentResponseModel.CreditCardType;
                                            subscriptionOrder.CreditCardNumber = paymentResponseModel.CreditCardNumber;
                                            subscriptionOrder.PaymentError = paymentResponseModel.IsExceptionError ? paymentResponseModel.ExceptionErrorText : paymentResponseModel.ErrorText;
                                            subscriptionOrder.PaymentDateTime = DateTime.Now;
                                            subscriptionOrder.ModifiedOn = DateTime.Now;
                                            await _subscriptionService.UpdateSubscriptionOrder(subscriptionOrder);

                                            if (!subscriptionOrder.FirstOrder)
                                            {
                                                var subscriptionItemDetails = await _subscriptionService.GetAllSubscriptionItemDetail(subscriptionOrder.SubscriptionId);
                                                foreach (var subscriptionItemDetail in subscriptionItemDetails)
                                                {
                                                    var childProduct = await _productService.GetById(subscriptionItemDetail.ChildProductId);
                                                    if (childProduct != null)
                                                    {
                                                        await _productService.AdjustStockQuantity(product: childProduct,
                                                            stock: subscriptionItemDetail.Subscription.Quantity * subscriptionItemDetail.Quantity,
                                                           relatedEntityType: RelatedEntityType.SubscriptionOrder, relatedEntityId: subscriptionOrder.Id,
                                                           productActionType: ProductActionType.RemoveFromStock);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                await dbContextTransaction.CommitAsync();

                                string url = _appSettings.WebsiteUrl + "CON/QPAY/" + quickPayment.PaymentNumber;
                                return url;
                            }
                        }
                    }
                }
                catch
                {
                    await dbContextTransaction.RollbackAsync();
                }
            }

            return string.Empty;
        }
    }
}
