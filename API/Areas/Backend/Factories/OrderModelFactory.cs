
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Models.Admin.Sales;
using API.Helpers;
using Utility.API;
using Utility.ResponseMapper;
using Utility.Helpers;
using System.Linq;
using Data.Sales;
using Data.CouponPromotion;
using Utility.Enum;
using Data.ProductManagement;
using Data.CustomerManagement;
using Utility.Models.KNET;
using Utility;
using Data.Shop;
using Services.Backend.CustomerManagement;
using Services.Backend.Shop;
using Services.Backend.ProductManagement.Interface;
using Services.Backend.Locations;
using Services.Backend.CouponPromotion.Interface;
using Services.Backend.Sales;
using Services.Backend.Content.Interface;
using API.Areas.Backend.Helpers;
using Utility.Models.Admin.Delivery;
using Utility.Models.Frontend.Sales;
using Utility.Models.Frontend.CustomizedModel;
using Services.Backend.Template.Interface;

namespace API.Areas.Backend.Factories
{
    public class OrderModelFactory : IOrderModelFactory
    {
        private readonly ILogger _logger;
        private readonly AppSettingsModel _appSettings;
        private readonly IModelHelper _modelHelper;
        // private readonly ISystemUserService _systemUserService;
        private readonly ICustomerService _customerService;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly ICommonHelper _commonHelper;
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly ICouponService _couponService;
        private readonly IOrderService _orderService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IPromotionService _promotionService;
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly IQuickPaymentService _quickPaymentService;
        private readonly INotificationTemplateService _notificationTemplateService;
        private readonly IPaymentHelper _paymentHelper;
        private readonly IAPIHelper _apiHelper;
        public OrderModelFactory(ILoggerFactory logger,
            IOptions<AppSettingsModel> options,
            IModelHelper modelHelper,
            // ISystemUserService  systemUserService,
            ICustomerService customerService,
            ICartService cartService,
            IProductService productService,
            ICommonHelper commonHelper,
            ICountryService countryService,
            IMapper mapper,
            ICategoryService categoryService,
            ICouponService couponService,
            IOrderService orderService,
            ISubscriptionService  subscriptionService,
            IPromotionService promotionService,
            IPaymentMethodService paymentMethodService,
            IQuickPaymentService quickPaymentService,
            INotificationTemplateService notificationTemplateService,
        IPaymentHelper paymentHelper,
            IAPIHelper apiHelper)
        {
            _logger = logger.CreateLogger(typeof(OrderModelFactory).Name);
            _appSettings = options.Value;
            _modelHelper = modelHelper;
            // _systemUserService = systemUserService;
            _customerService = customerService;
            _cartService = cartService;
            _productService = productService;
            _commonHelper = commonHelper;
            _countryService = countryService;
            _mapper = mapper;
            _categoryService = categoryService;
            _couponService = couponService;
            _orderService = orderService;
            _subscriptionService = subscriptionService;
            _promotionService = promotionService;
            _paymentMethodService = paymentMethodService;
            _quickPaymentService = quickPaymentService;
            _notificationTemplateService = notificationTemplateService;
            _paymentHelper = paymentHelper;
            _apiHelper = apiHelper;
        }

        public async Task<OrderModel> PrepareOrder(bool isEnglish, int  id)
        {
            var orderModel = new OrderModel();
            var order = await _orderService.GetOrderById(id);

            if (order != null)
            {
                orderModel = await _modelHelper.PrepareOrderModel(order: order, isEnglish: isEnglish, loadDetails : true);

            }
            return orderModel;
        }

        public async Task<DailyOrderSummaryModel> GetTodaySales(bool isEnglish)
        {
            var model = await _orderService.GetTodaySales();
            if (model is not null)
            {
                model.FormattedItemsSoldToday = model.ItemsSoldToday.ToString();
                model.FormattedOrderReceivedToday = model.OrderReceivedToday.ToString();
                model.FormattedSalesAmountToday = await _commonHelper.ConvertDecimalToString(model.SalesAmountToday, isEnglish, 1, true);
            }
            return model;
        }

        public async Task<DailySubscriptionSummaryModel> GetTodaySubscriptionSales(bool isEnglish)
        {
            var model = await _subscriptionService.GetSubscriptionTodaySales();
            if (model is not null)
            { 
                model.FormattedSubscriptionOrdersReceivedToday = model.SubscriptionOrdersReceivedToday.ToString();
                model.FormattedSubscriptionSalesAmountToday = await _commonHelper.ConvertDecimalToString(model.SubscriptionSalesAmountToday, isEnglish, 1, true);
            }
            return model;
        }

        public async Task<APIResponseModel<AdminCreateOrderModel>> CreateOrder(bool isEnglish, int customerId, DeviceType deviceTypeId, AdminCreateOrderModel adminCreateOrderModel)
        {
            var response = new APIResponseModel<AdminCreateOrderModel>();

            try
            {
                var customer = await _customerService.GetCustomerById(customerId);
                if (customer == null)
                {
                    response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
                    return response;
                }

                if (customer.Deleted)
                {
                    response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
                    return response;
                }

                if (!customer.Active)
                {
                    response.Message = isEnglish ? Messages.InactiveCustomer : MessagesAr.InactiveCustomer;
                    return response;
                }

                var cartItems = await _cartService.GetAllCartItem(customerId: customerId);
                if (cartItems.Count == 0)
                {
                    response.Message = isEnglish ? Messages.NoItemsInCart : MessagesAr.NoItemsInCart;
                    return response;
                }

                var cartAttribute = (await _cartService.GetAllCartAttribute(customerId: customerId)).FirstOrDefault();
                if (cartAttribute == null)
                {
                    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    return response;
                }

                decimal deliveryFee = 0;
                Address address = null;
                if (!cartAttribute.AddressId.HasValue)
                {
                    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    return response;
                }
                else
                {
                    address = await _customerService.GetAddressById(cartAttribute.AddressId.Value);
                    if (address == null)
                    {
                        response.Message = isEnglish ? Messages.AddressNotExists : MessagesAr.AddressNotExists;
                        return response;
                    }

                    if (address.Deleted)
                    {
                        response.Message = isEnglish ? Messages.AddressNotExists : MessagesAr.AddressNotExists;
                        return response;
                    }

                    var area = address.Area;
                    if (area == null)
                    {
                        response.Message = isEnglish ? Messages.AreaNotExists : MessagesAr.AreaNotExists;
                        return response;
                    }

                    if (area.Deleted)
                    {
                        response.Message = isEnglish ? Messages.AreaNotExists : MessagesAr.AreaNotExists;
                        return response;
                    }

                    if (!area.Active)
                    {
                        response.Message = isEnglish ? Messages.AreaNotActive : MessagesAr.AreaNotActive;
                        return response;
                    }

                    var governorate = area.Governorate;
                    if (governorate == null)
                    {
                        response.Message = isEnglish ? Messages.GovernorateNotExists : MessagesAr.GovernorateNotExists;
                        return response;
                    }

                    if (governorate.Deleted)
                    {
                        response.Message = isEnglish ? Messages.GovernorateNotExists : MessagesAr.GovernorateNotExists;
                        return response;
                    }

                    if (!governorate.Active)
                    {
                        response.Message = isEnglish ? Messages.GovernorateNotActive : MessagesAr.GovernorateNotActive;
                        return response;
                    }

                    deliveryFee = area.DeliveryFee;
                }

                if (adminCreateOrderModel.PaymentMethodId==0)
                {
                    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    return response;
                }
                //else
                //{
                //    var paymentMethod = await _paymentMethodService.GetPaymentMethodById(adminCreateOrderModel.PaymentMethodId.Value);
                //    if (paymentMethod == null)
                //    {
                //        response.Message = isEnglish ? Messages.PaymentMethodNotExists : MessagesAr.PaymentMethodNotExists;
                //        return response;
                //    }

                //    if (paymentMethod.Deleted)
                //    {
                //        response.Message = isEnglish ? Messages.PaymentMethodNotExists : MessagesAr.PaymentMethodNotExists;
                //        return response;
                //    }

                //    if (!paymentMethod.Active)
                //    {
                //        response.Message = isEnglish ? Messages.PaymentMethodNotActive : MessagesAr.PaymentMethodNotActive;
                //        return response;
                //    }


                //    if (!paymentMethod.NormalCheckoutRegisteredCustomer)
                //    {
                //        response.Message = isEnglish ? Messages.PaymentMethodNotAvailable : MessagesAr.PaymentMethodNotAvailable;
                //        return response;
                //    }
                //}

                //if (cartAttribute.WalletUsedAmount > 0)
                //{
                //    var walletBalance = await _customerService.GetWalletBalanceByCustomerId(id: customerId, walletTypeId: WalletType.Wallet);
                //    if (walletBalance <= 0)
                //    {
                //        response.Message = isEnglish ? Messages.WalletBalanceLessThanActualAmount : MessagesAr.WalletBalanceLessThanActualAmount;
                //        return response;
                //    }

                //    if (cartAttribute.WalletUsedAmount > walletBalance)
                //    {
                //        response.Message = isEnglish ? Messages.WalletBalanceLessThanActualAmount : MessagesAr.WalletBalanceLessThanActualAmount;
                //        return response;
                //    }
                //}

                var orderItems = new List<OrderItem>();
                foreach (var cartItem in cartItems)
                {
                    var product = await _productService.GetById(cartItem.ProductId);
                    if (product == null)
                    {
                        response.Message = isEnglish ? Messages.ProductNotExists : MessagesAr.ProductNotExists;
                        return response;
                    }

                    var name = isEnglish ? product.NameEn : product.NameAr;

                    if (product.Deleted)
                    {
                        response.Message = isEnglish ? string.Format(Messages.ProductNotExistsWithName, name) : string.Format(MessagesAr.ProductNotExistsWithName, name);
                        return response;
                    }

                    if (!product.Active)
                    {
                        response.Message = isEnglish ? string.Format(Messages.ProductNotActiveWithName, name) : string.Format(MessagesAr.ProductNotActiveWithName, name);
                        return response;
                    }

                    if (product.ProductType == ProductType.SubscriptionProduct)
                    {
                        response.Message = isEnglish ? string.Format(Messages.ProductNotExistsWithName, name) : string.Format(MessagesAr.ProductNotExistsWithName, name);
                        return response;
                    }

                    Category category = product.Category;
                    if (category == null)
                    {
                        response.Message = isEnglish ? Messages.CategoryNotExists : MessagesAr.CategoryNotExists;
                        return response;
                    }

                    string categoryName = isEnglish ? category.NameEn : category.NameAr;

                    if (category.Deleted)
                    {
                        response.Message = isEnglish ? string.Format(Messages.CategoryNotExistsWithProduct, name, categoryName) : string.Format(MessagesAr.CategoryNotExistsWithProduct, name, categoryName);
                        return response;
                    }

                    if (!category.Active)
                    {
                        response.Message = isEnglish ? string.Format(Messages.CategoryNotExistsWithProduct, name, categoryName) : string.Format(MessagesAr.CategoryNotExistsWithProduct, name, categoryName);
                        return response;
                    }

                    if (cartItem.Quantity <= 0)
                    {
                        response.Message = isEnglish ? string.Format(Messages.CartZeroQuantityValidation, name) : string.Format(MessagesAr.CartZeroQuantityValidation, name);
                        return response;
                    }

                    var productStockQuantity = 0;
                    if (product.ProductType == ProductType.BaseProduct)
                    {
                        productStockQuantity = await _productService.GetAvailableStockQuantity(productId: product.Id, customerId: customer.Id);
                        if (productStockQuantity < 0)
                            productStockQuantity = 0;
                    }
                    else if (product.ProductType == ProductType.BundledProduct)
                    {
                        int lowStockProduct = 0;
                        var productDetails = product.ProductDetails.ToList();
                        foreach (var productDetail in productDetails)
                        {
                            var childProductStockQuantity = await _productService.GetAvailableStockQuantity(productId: productDetail.ChildProductId,
                        customerId: customer.Id);
                            if (childProductStockQuantity <= (cartItem.Quantity * productDetail.Quantity))
                                lowStockProduct++;
                        }

                        if (lowStockProduct > 0)
                            productStockQuantity = 0;
                        else
                            productStockQuantity = cartItem.Quantity;
                    }

                    if (cartItem.Quantity > productStockQuantity)
                    {
                        response.Message = isEnglish ? string.Format(Messages.ProductIsOutOfStock, productStockQuantity) : string.Format(MessagesAr.ProductIsOutOfStock, productStockQuantity);
                        return response;
                    }

                    bool b2bCustomer = customer != null && customer.B2B;
                    decimal price = product.GetPriceFrontend(b2bCustomer);
                    decimal discountedPrice = product.GetDiscountedPriceFrontend(b2bCustomer);

                    List<OrderItemDetail> orderItemDetails = new();
                    if (product.ProductType == ProductType.BundledProduct)
                    {
                        var productDetails = product.ProductDetails.ToList();
                        foreach (var productDetail in productDetails)
                        {
                            var orderItemDetail = new OrderItemDetail
                            {
                                ProductId = productDetail.ProductId,
                                ChildProductId = productDetail.ChildProductId,
                                Quantity = productDetail.Quantity
                            };

                            orderItemDetails.Add(orderItemDetail);
                        }
                    }

                    var orderItem = new OrderItem
                    {
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        UnitPrice = price,
                        B2BPrice = b2bCustomer,
                        DiscountType = discountedPrice > 0 ? DiscountType.Amount : DiscountType.NoDiscount,
                        DiscountValueApplied = price - discountedPrice,
                        DiscountAmount = price - discountedPrice,
                        OrderItemDetails = orderItemDetails
                    };

                    decimal itemTotal = (orderItem.UnitPrice - orderItem.DiscountAmount) * orderItem.Quantity;
                    orderItem.Total = itemTotal;
                    orderItems.Add(orderItem);
                }

                var dateAndSlot = await _commonHelper.GetAvailableDeliveryDateAndSlot();

                var order = new Order
                {
                    CustomerId = customer.Id,
                    OrderNumber = string.Empty,
                    OrderStatusId = OrderStatus.Discarded,
                    CustomerLanguageId = isEnglish ? 1 : 2,
                    CustomerIp = "1:0",
                    PaymentMethodId = adminCreateOrderModel.PaymentMethodId,
                    PaymentStatusId = PaymentStatus.Canceled,
                    CreatedOn = DateTime.Now,
                    DeviceTypeId = deviceTypeId,
                    DeliveryDate = dateAndSlot.Item1,
                    DeliveryTimeSlotId = dateAndSlot.Item2,
                    SubTotal = orderItems.Sum(a => a.Total),
                    DeliveryFee = deliveryFee,
                    OrderTypeId = OrderType.Offline,
                    Notes = ""
                };


                Coupon coupon = null;
                if (cartAttribute.CouponId.HasValue)
                {
                    coupon = await _couponService.GetById(cartAttribute.CouponId.Value);
                    if (coupon == null)
                    {
                        response.Message = isEnglish ? Messages.CouponNotValid : MessagesAr.CouponNotValid;
                        return response;
                    }

                    var couponValidation = _commonHelper.CouponValidation(coupon: coupon, isEnglish: isEnglish, total: order.SubTotal);
                    if (!string.IsNullOrEmpty(couponValidation))
                    {
                        response.Message = couponValidation;
                        return response;
                    }

                    order.CouponId = cartAttribute.CouponId;
                    order.CouponDiscountType = coupon.DiscountType;
                    order.CouponDiscountValueApplied = coupon.DiscountType == DiscountType.Percentage ? coupon.DiscountPercentage : coupon.DiscountAmount;
                    order.CouponDiscountAmount = coupon.ApplyCouponDiscount2(order.SubTotal);
                }

                var subTotalAfterCouponDiscount = order.SubTotal - order.CouponDiscountAmount;
                order.CashbackAmount = await _commonHelper.GetCashbackAmount(customerId: customerId, amount: subTotalAfterCouponDiscount);

                //if (cartAttribute.WalletUsedAmount > 0)
                //{
                //    decimal grossTotal = order.SubTotal - order.CouponDiscountAmount - order.CashbackAmount + order.DeliveryFee;

                //    if (order.PaymentMethodId == (int)PaymentMethod.Wallet)
                //    {
                //        if (cartAttribute.WalletUsedAmount != grossTotal)
                //        {
                //            response.Message = isEnglish ? Messages.PaymentMethodNotAvailable : MessagesAr.PaymentMethodNotAvailable;
                //            return response;
                //        }
                //    }

                //    if (cartAttribute.WalletUsedAmount > grossTotal)
                //    {
                //        response.Message = isEnglish ? Messages.WalletAmountSholuldLessThanActualAmount : MessagesAr.WalletAmountSholuldLessThanActualAmount;
                //        return response;
                //    }
                //}

                address = new Address
                {
                    AddressId = address.Id,
                    CustomerId = address.CustomerId,
                    TypeId = address.TypeId,
                    Name = address.Name,
                    MobileNumber = address.MobileNumber,
                    EmailAddress = address.EmailAddress,
                    AreaId = address.AreaId,
                    Block = address.Block,
                    Street = address.Street,
                    Avenue = address.Avenue,
                    HouseNumber = address.HouseNumber,
                    BuildingNumber = address.BuildingNumber,
                    FloorNumber = address.FloorNumber,
                    FlatNumber = address.FlatNumber,
                    SchoolName = address.SchoolName,
                    MosqueName = address.MosqueName,
                    GovernmentEntity = address.GovernmentEntity,
                    Notes = address.Notes
                };
                await _customerService.CreateAddress(address);
                order.AddressId = address.Id;

                order.OrderItems = orderItems;

                order.Total = order.SubTotal + order.DeliveryFee - order.WalletUsedAmount - order.CashbackAmount - order.CouponDiscountAmount;
                if (order.Total <= 0)
                {
                    response.Message = isEnglish ? Messages.OrderAmountValidation : MessagesAr.OrderAmountValidation;
                    return response;
                }

                order = await _orderService.CreateOrder(order);
                if (order != null)
                {
                    var orderNumber = string.Empty;
                    Order orderByOrderNumber = null;
                    do
                    {
                        orderNumber = "10" + Common.GenerateRandomNo();
                        orderByOrderNumber = await _orderService.GetOrderByOrderNumber(orderNumber);
                    }
                    while (orderByOrderNumber != null);

                    order.OrderNumber = orderNumber;
                    await _orderService.UpdateOrder(order);

                    await _cartService.HoldAndReleaseCartItem(customerId: customer.Id, isHold: true);


                    if (order.PaymentMethodId == (int)PaymentMethod.Cash)
                    {
                        var paymentResponseModel = new PaymentResponseModel()
                        {
                            Amount = order.Total.ToString("N3"),
                            EntityId = order.Id.ToString(),
                            RequestType = PaymentRequestType.Order.ToString(),
                            Result = "captured"
                        };

                        var url = await _paymentHelper.UpdatePaymentEntity(paymentResponseModel);
                    }
                    else if (order.PaymentMethodId == (int)PaymentMethod.QPay)
                    {
                        var qpayNumber = string.Empty;
                        QuickPayment qpayByNumber = null;
                        do
                        {
                            qpayNumber = "10" + Common.GenerateRandomNo();
                            qpayByNumber = await _quickPaymentService.GetqpayByNumber(qpayNumber);
                        }
                        while (qpayByNumber != null);

                        var qpayModel = new QuickPayment()
                        {
                            Amount = order.Total,
                            EntityId = order.Id,
                            CustomerId= customer.Id,
                            PaymentNumber= qpayNumber,
                            MobileNumber = customer.MobileNumber,
                            PaymentRequestTypeId = PaymentRequestType.Order

                        };

                        var _QpayInfo = await _quickPaymentService.Create(qpayModel);
                        var Message = string.Empty;
                        var notificationTemplate = await _notificationTemplateService.GetNotificationTemplateByTypeId(NotificationType.QPay);
                        if (notificationTemplate != null)
                        {
                            var Qlink = _appSettings.QuickPayUrl + qpayNumber;
                            if (customer.LanguageId == 1)
                            {
                                Message = notificationTemplate.SMSMessageEn.Replace("{link}", Qlink).Replace("{ordernumber}", orderNumber);
                            }
                            else
                            {
                                Message = notificationTemplate.SMSMessageAr.Replace("{link}", Qlink).Replace("{ordernumber}", orderNumber);

                            }

                           await _notificationTemplateService.CreateQpaySMSPush(Message, customer.MobileNumber, customer.LanguageId);

                        }


                       var paymentResponseModel = new PaymentResponseModel()
                        {
                            Amount = order.Total.ToString("N3"),
                            EntityId = order.Id.ToString(),
                            RequestType = PaymentRequestType.Order.ToString(),
                            Result = "captured"
                        };

                        var url = await _paymentHelper.UpdatePaymentEntity(paymentResponseModel);
                    }
                    //if (order.PaymentMethodId == (int)PaymentMethod.KNET)
                    //{
                    //    var paymentUrlRequestModel = new PaymentUrlRequestModel
                    //    {
                    //        LangId = order.CustomerLanguageId.ToString(),
                    //        Amount = order.Total.ToString("N3"),
                    //        TrackId = order.OrderNumber.ToString(),
                    //        EntityId = order.Id.ToString(),
                    //        CustomerId = order.CustomerId.ToString(),
                    //        RequestType = PaymentRequestType.Order.ToString()
                    //    };

                    //    var paymentUrl = await _apiHelper.PostAsync<string>("Home/GetPaymentUrl", paymentUrlRequestModel, baseUrl: _appSettings.PaymentAPIUrl);
                    //    if (!string.IsNullOrEmpty(paymentUrl))
                    //    {
                    //        createOrderModel.PaymentUrl = paymentUrl;

                    //        if (order.DeviceTypeId == DeviceType.Web)
                    //            createOrderModel.PaymentReturnUrl = _appSettings.WebsiteUrl + "ORD/" + order.OrderNumber;
                    //        else
                    //            createOrderModel.PaymentReturnUrl = _appSettings.WebsiteUrl + "paymentresult";

                    //        createOrderModel.OrderId = order.Id;
                    //    }
                    //}
                    //else if (order.PaymentMethodId == (int)PaymentMethod.VISAMASTER)
                    //{
                    //    var value = Cryptography.Encrypt(PaymentRequestType.Order.ToString() + "-" + order.Id);
                    //    createOrderModel.PaymentUrl = _appSettings.APIBaseUrl + "payment/requestgbmasterpayment?value=" + value;

                    //    if (order.DeviceTypeId == DeviceType.Web)
                    //        createOrderModel.PaymentReturnUrl = _appSettings.WebsiteUrl + "ORD/" + order.OrderNumber;
                    //    else
                    //        createOrderModel.PaymentReturnUrl = _appSettings.WebsiteUrl + "paymentresult";

                    //    createOrderModel.OrderId = order.Id;
                    //}
                }
               
                response.Data = adminCreateOrderModel;
                response.Message = isEnglish ? Messages.Success : MessagesAr.Success;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }
        
        public async Task<APIResponseModel<OrderModel>> GetOrder(bool isEnglish, int id, int customerId)
        {
            //var response = new APIResponseModel<AdminOrderModel>();
            var response = new APIResponseModel<OrderModel>();
            try
            {
                Order order = new();

                //var customer = await _customerService.GetCustomerById(customerId);
                //if (customer == null)
                //{
                //    response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
                //    return response;
                //}


                order = await _orderService.GetOrderById(id);
                if (order != null) //&& !order.Deleted && order.Customer != null
                {
                    //if (order.CustomerId != customerId)
                    //{
                    //    response.Message = isEnglish ? Messages.InvalidCustomer : MessagesAr.InvalidCustomer;
                    //    return response;
                    //}

                    response.DataRecordCount = 1;


                   // response.Data = await _modelHelper.PrepareOrderModelAdmin(order, isEnglish, true);
                    response.Data = await _modelHelper.PrepareOrderModel(order, isEnglish, true);


                }


                response.Message = isEnglish ? Messages.Success : MessagesAr.Success;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }

        public async Task<APIResponseModel<List<OrderModel>>> GetOrders(bool isEnglish, int customerId, int id = 0, string orderNumber = "",
            int limit = 0, int page = 0, OrderStatus? orderStatus = null)
        {
            var response = new APIResponseModel<List<OrderModel>>();
            try
            {
                var orders = new List<Order>();

                var customer = await _customerService.GetCustomerById(customerId);
                if (customer == null)
                {
                    response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
                    return response;
                }

                if (customer.Deleted)
                {
                    response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
                    return response;
                }

                if (!customer.Active)
                {
                    response.Message = isEnglish ? Messages.InactiveCustomer : MessagesAr.InactiveCustomer;
                    return response;
                }

                bool loadDetails = false;
                if (id > 0)
                {
                    loadDetails = true;
                    var order = await _orderService.GetOrderById(id);
                    if (order != null && !order.Deleted && order.Customer != null)
                    {
                        if (order.CustomerId != customerId)
                        {
                            response.Message = isEnglish ? Messages.InvalidCustomer : MessagesAr.InvalidCustomer;
                            return response;
                        }

                        orders.Add(order);
                    }
                }
                else if (!string.IsNullOrEmpty(orderNumber))
                {
                    loadDetails = true;
                    var order = await _orderService.GetOrderByOrderNumber(orderNumber);
                    if (order != null && !order.Deleted)
                        orders.Add(order);
                }
                else
                {
                    orders = await _orderService.GetAllOrder(customerId: customerId, orderStatus: orderStatus);
                }

                orders = orders.OrderByDescending(a => a.Id).ToList();

                response.DataRecordCount = orders.Count;

                if (limit > 0 && page > 0)
                {
                    orders = orders.Skip((page - 1) * limit).Take(limit).ToList();
                }

                var orderModels = new List<OrderModel>();
                foreach (var order in orders)
                {
                    //var orderModel = await _modelHelper.PrepareOrderModelAdmin(order, isEnglish, loadDetails: loadDetails);
                    var orderModel = await _modelHelper.PrepareOrderModel(order, isEnglish, loadDetails: loadDetails);
                    orderModels.Add(orderModel);
                }

                response.Data = orderModels;
                response.Message = isEnglish ? Messages.Success : MessagesAr.Success;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }
        public async Task<APIResponseModel<bool>> ReOrder(bool isEnglish, int customerId, int id)
        {
            var response = new APIResponseModel<bool>();

            try
            {
                var customer = await _customerService.GetCustomerById(customerId);
                if (customer == null)
                {
                    response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
                    return response;
                }

                if (customer.Deleted)
                {
                    response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
                    return response;
                }

                if (!customer.Active)
                {
                    response.Message = isEnglish ? Messages.InactiveCustomer : MessagesAr.InactiveCustomer;
                    return response;
                }

                var orderItems = await _orderService.GetAllOrderItem(orderId: id);
                if (orderItems.Count == 0)
                {
                    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    return response;
                }

                if (orderItems[0].Order.CustomerId != customer.Id)
                {
                    response.Message = isEnglish ? Messages.InvalidCustomer : MessagesAr.InvalidCustomer;
                    return response;
                }

                foreach (var orderItem in orderItems)
                {
                    var existingCart = (await _cartService.GetAllCartItem(productDetailId: orderItem.ProductId, customerId: customer.Id)).FirstOrDefault();
                    if (existingCart == null)
                    {
                        var cartItem = new CartItem
                        {
                            CustomerId = customer.Id,
                            ProductId = orderItem.ProductId,
                            Quantity = orderItem.Quantity
                        };

                        await _cartService.CreateCartItem(cartItem);
                    }
                    else
                    {
                        existingCart.Quantity = existingCart.Quantity + orderItem.Quantity;
                        await _cartService.UpdateCartItem(existingCart);
                    }
                }

                response.Data = true;
                response.Message = isEnglish ? Messages.AddToShoppingCartSuccess : MessagesAr.AddToShoppingCartSuccess;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }

        public async Task<bool> UpdateOrderStatus(int orderId, OrderStatus orderStatusId, bool refundDeliveryFee = false, string notes = "")
        {
            var order = await _orderService.GetOrderById(orderId);
            if (order is not null)
            {
                return await _commonHelper.UpdateOrderStatus(order, orderStatusId, refundDeliveryFee, notes);
            }
            return false;
        }

        public async Task<bool> UpdateDriverOrderStatus(int orderId, int orderType, OrderStatus orderStatusId, bool refundDeliveryFee = false, string notes = "")
        {
            if(orderStatusId== OrderStatus.Delivered)
            {
                if (orderType == 1)
                {
                    var order = await _orderService.GetOrderById(orderId);
                    if (order is not null)
                    {
                        return await _orderService.UpdateOrderPaymentStatus(order, (int)OrderStatus.Delivered, (int)PaymentStatus.Captured);
                    }
                }
                else if (orderType == 2)
                {
                    var order = await _subscriptionService.GetSubscriptionOrderById(orderId);
                    if (order is not null)
                    {
                        return await _subscriptionService.UpdateOrderPaymentStatus(order, true, (int)PaymentStatus.Captured);
                    }
                }

            }
            else if(orderStatusId == OrderStatus.Confirmed) // driver dashbord cancel
            {
                if (orderType == 1)
                {
                    var order = await _orderService.GetOrderById(orderId);
                    if (order is not null)
                    {
                        return await _orderService.UpdateDriverdetails(order, (int)OrderStatus.ReturnedByDriver, (int)order.PaymentStatusId);
                    }
                }
                else if (orderType == 2)
                {
                    var order = await _subscriptionService.GetSubscriptionOrderById(orderId);
                    if (order is not null)
                    {
                        return await _subscriptionService.UpdateOrderPaymentStatus(order, false, (int) order.PaymentStatusId);
                    }
                }
            }
           
            return false;
        }




        //public async Task<bool> AddDriver(int orderId, int driverId,int OrderTypeID)
        //{
        //    return await _orderService.AddDriver(orderId, driverId, OrderTypeID);
        //}

        public async Task<bool> RemoveDriver(int orderId)
        {
            return await _orderService.RemoveDriver(orderId);
        }

        public async Task<bool> RescheduleDelivery(int orderId, DateTime? dateTime = null)
        {
            var order = await _orderService.GetOrderById(orderId);
            if (order is not null)
            {
                 return await _commonHelper.RescheduleOrderDelivery(order,  dateTime);
            }
            return false;
        }

       

        public async Task<DataTableResult<List<DeliveriesDashboard>>> GetDeliveriesForDataTable(AdminOrderDeliveriesParam param)
        {
            DataTableResult<List<DeliveriesDashboard>> result = new() { Draw = param.DatatableParam.Draw };
            try
            {
                result = await _orderService.GetAllOrdersForDeliveries(param);
               foreach (var item in result.Data)
                {
                    item.FormattedDeliveryFee = await _commonHelper.ConvertDecimalToString(item.DeliveryFee, param.IsEnglish, 1, true);
                    item.FormattedTotal = await _commonHelper.ConvertDecimalToString(item.Total, param.IsEnglish, 1, true);
                    item.OrderStatus = _commonHelper.GetOrderStatusName(item.OrderStatusId, param.IsEnglish);
                }

                return result;
            }
            catch (Exception exp)
            {
                _logger.LogError("OrderModelFactor:", exp.Message);
            }

            return result;
        }



        public async Task<APIResponseModel<bool>> AddQPay(int CustomerId, int orderID, string OrderNumber,decimal Ordertotal,int OrderType)
        {
            var response = new APIResponseModel<bool>();
            try
            {


                var customer = await _customerService.GetCustomerById(CustomerId);
                if (customer == null)
                {
                    response.Message = Messages.CustomerNotExists;
                    return response;
                }
                var qpayNumber = string.Empty;
                QuickPayment qpayByNumber = null;
                do
                {
                    qpayNumber = "10" + Common.GenerateRandomNo();
                    qpayByNumber = await _quickPaymentService.GetqpayByNumber(qpayNumber);
                }
                while (qpayByNumber != null);

                var qpayModel = new QuickPayment()
                {
                    Amount = Ordertotal,
                    EntityId = orderID,
                    CustomerId = CustomerId,
                    PaymentNumber = qpayNumber,
                    MobileNumber = customer.MobileNumber,
                    PaymentRequestTypeId = (PaymentRequestType)OrderType

                };

                var _QpayInfo = await _quickPaymentService.Create(qpayModel);
                var Message = string.Empty;
                var notificationTemplate = await _notificationTemplateService.GetNotificationTemplateByTypeId(NotificationType.QPay);
                if (notificationTemplate != null)
                {
                    var Qlink = _appSettings.QuickPayUrl + qpayNumber;
                    if (customer.LanguageId == 1)
                    {
                        Message = notificationTemplate.SMSMessageEn.Replace("{link}", Qlink).Replace("{ordernumber}", qpayNumber);
                    }
                    else
                    {
                        Message = notificationTemplate.SMSMessageAr.Replace("{link}", Qlink).Replace("{ordernumber}", qpayNumber);

                    }

                    await _notificationTemplateService.CreateQpaySMSPush(Message, customer.MobileNumber, customer.LanguageId);

                }
            }
            catch (Exception exp)
            {
                _logger.LogError("OrderModelFactor:", exp.Message);
            }

            return response;
        }


        //public async Task<DataTableResult<List<DeliveriesDashboard>>> GetNotDispatchedDataTable(DataTableParam param,
        //                             bool isEnglish, string orderNumber = null,
        //                             DateTime? startDate = null, int? orderModeId = null, int? orderTypeId = null,
        //                             int? areaId = null, int? driverId = null)
        //{
        //    DataTableResult<List<DeliveriesDashboard>> result = new() { Draw = param.Draw };
        //    try
        //    {
        //        result = await _orderService.GetNotDispatchedOrdersDataTable(param, orderNumber, startDate, orderModeId, orderTypeId, areaId, driverId);
        //        foreach (var item in result.Data)
        //        {
        //            item.FormattedDeliveryFee = await _commonHelper.ConvertDecimalToString(item.DeliveryFee, isEnglish, 1, true);
        //            item.FormattedTotal = await _commonHelper.ConvertDecimalToString(item.Total, isEnglish, 1, true);
        //        }

        //        return result;
        //    }
        //    catch (Exception exp)
        //    {
        //        _logger.LogError("OrderModelFactor:", exp.Message);
        //    }

        //    return result;
        //}
        //public async Task<DataTableResult<List<DeliveriesDashboard>>> GetDispatchedDataTable(DataTableParam param,
        //                              bool isEnglish, string orderNumber = null,
        //                              DateTime? startDate = null, int? orderModeId = null, int? orderTypeId = null,
        //                              int? areaId = null, int? driverId = null)
        //{
        //    DataTableResult<List<DeliveriesDashboard>> result = new() { Draw = param.Draw };
        //    try
        //    {
        //        result = await _orderService.GetDispatchedOrdersDataTable(param, orderNumber, startDate, orderModeId, orderTypeId, areaId, driverId);
        //        foreach (var item in result.Data)
        //        {
        //            item.FormattedDeliveryFee = await _commonHelper.ConvertDecimalToString(item.DeliveryFee, isEnglish, 1, true);
        //            item.FormattedTotal = await _commonHelper.ConvertDecimalToString(item.Total, isEnglish, 1, true);
        //        }

        //        return result;
        //    }
        //    catch (Exception exp)
        //    {
        //        _logger.LogError("OrderModelFactor:", exp.Message);
        //    }

        //    return result;
        //}
    }
}