using API.Areas.Frontend.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Frontend.Shop;
using Services.Frontend.CustomerManagement;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Models.Frontend.Sales;
using API.Helpers;
using Utility.API;
using Services.Frontend.ProductManagement.Interface;
using Services.Frontend.CouponPromotion.Interface;
using Services.Frontend.Sales;
using Utility.ResponseMapper;
using Utility.Helpers;
using System.Linq;
using Data.Sales;
using Data.CouponPromotion;
using Utility.Enum;
using Data.ProductManagement;
using Data.CustomerManagement;
using Services.Frontend.Content.Interface;
using Utility.Models.KNET;
using Utility;
using Data.Shop;
using Utility.Models.Frontend.CustomizedModel;

namespace API.Areas.Frontend.Factories
{
    public class OrderModelFactory : IOrderModelFactory
    {
        private readonly ILogger _logger;
        private readonly AppSettingsModel _appSettings;
        private readonly IModelHelper _modelHelper;
        private readonly ICustomerService _customerService;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly ICommonHelper _commonHelper;
        private readonly ICouponService _couponService;
        private readonly IOrderService _orderService;
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly IAPIHelper _apiHelper;
        private readonly IPaymentHelper _paymentHelper;
        private readonly ITabbyHelper _tabbyHelper;
        public OrderModelFactory(ILoggerFactory logger,
            IOptions<AppSettingsModel> options,
            IModelHelper modelHelper,
            ICustomerService customerService,
            ICartService cartService,
            IProductService productService,
            ICommonHelper commonHelper,
            ICouponService couponService,
            IOrderService orderService,
            IPaymentMethodService paymentMethodService,
            IAPIHelper apiHelper,
            IPaymentHelper paymentHelper,
            ITabbyHelper tabbyHelper)
        {
            _logger = logger.CreateLogger(typeof(OrderModelFactory).Name);
            _appSettings = options.Value;
            _modelHelper = modelHelper;
            _customerService = customerService;
            _cartService = cartService;
            _productService = productService;
            _commonHelper = commonHelper;
            _couponService = couponService;
            _orderService = orderService;
            _paymentMethodService = paymentMethodService;
            _apiHelper = apiHelper;
            _paymentHelper = paymentHelper;
            _tabbyHelper = tabbyHelper;
        }
        public async Task<APIResponseModel<CreatePaymentModel>> CreateOrder(bool isEnglish, int customerId, DeviceType deviceTypeId, CreatePaymentModel createPaymentModel)
        {
            var response = new APIResponseModel<CreatePaymentModel>();

            try
            {
                var customer = await _customerService.GetCustomerById(customerId);
                if (customer == null || customer.Deleted)
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

                if (!cartAttribute.AddressId.HasValue || !cartAttribute.PaymentMethodId.HasValue)
                {
                    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    return response;
                }

                Address address = await _customerService.GetAddressById(cartAttribute.AddressId.Value);
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

                if (address.CustomerId != customer.Id)
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

                decimal deliveryFee = area.DeliveryFee;

                var paymentMethod = await _paymentMethodService.GetPaymentMethodById(cartAttribute.PaymentMethodId.Value);
                if (paymentMethod == null)
                {
                    response.Message = isEnglish ? Messages.PaymentMethodNotExists : MessagesAr.PaymentMethodNotExists;
                    return response;
                }

                if (paymentMethod.Deleted)
                {
                    response.Message = isEnglish ? Messages.PaymentMethodNotExists : MessagesAr.PaymentMethodNotExists;
                    return response;
                }

                if (!paymentMethod.Active)
                {
                    response.Message = isEnglish ? Messages.PaymentMethodNotActive : MessagesAr.PaymentMethodNotActive;
                    return response;
                }

                if (!paymentMethod.NormalCheckoutRegisteredCustomer)
                {
                    response.Message = isEnglish ? Messages.PaymentMethodNotAvailable : MessagesAr.PaymentMethodNotAvailable;
                    return response;
                }

                decimal walletBalance = 0;
                if (cartAttribute.UseWalletAmount)
                {
                    walletBalance = await _customerService.GetWalletBalanceByCustomerId(id: customerId, walletTypeId: WalletType.Wallet);
                    if (walletBalance <= 0)
                    {
                        response.Message = isEnglish ? Messages.WalletBalanceLessThanActualAmount : MessagesAr.WalletBalanceLessThanActualAmount;
                        return response;
                    }
                }

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

                    decimal discountAmount = discountedPrice > 0 ? price - discountedPrice : 0;
                    var orderItem = new OrderItem
                    {
                        Product = product,
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        UnitPrice = price,
                        B2BPrice = b2bCustomer,
                        DiscountType = discountAmount > 0 ? DiscountType.Amount : DiscountType.NoDiscount,
                        DiscountValueApplied = discountAmount,
                        DiscountAmount = discountAmount,
                        OrderItemDetails = orderItemDetails
                    };

                    decimal itemTotal = (orderItem.UnitPrice - orderItem.DiscountAmount) * orderItem.Quantity;
                    orderItem.Total = itemTotal;
                    orderItems.Add(orderItem);
                }

                var dateAndSlot = await _commonHelper.GetAvailableDeliveryDateAndSlot();

                string notes = string.Empty;
                if (deviceTypeId == DeviceType.Web)
                    notes = createPaymentModel.Notes;
                else
                    notes = cartAttribute.Notes;

                var order = new Order
                {
                    CustomerId = customer.Id,
                    OrderNumber = string.Empty,
                    OrderStatusId = OrderStatus.Discarded,
                    CustomerLanguageId = isEnglish ? 1 : 2,
                    CustomerIp = createPaymentModel.CustomerIp,
                    PaymentMethodId = cartAttribute.PaymentMethodId.Value,
                    PaymentStatusId = PaymentStatus.Canceled,
                    CreatedOn = DateTime.Now,
                    DeviceTypeId = deviceTypeId,
                    DeliveryDate = dateAndSlot.Item1,
                    DeliveryTimeSlotId = dateAndSlot.Item2,
                    SubTotal = orderItems.Sum(a => a.Total),
                    DeliveryFee = deliveryFee,
                    OrderTypeId = OrderType.Online,
                    Notes = notes
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

                order.CashbackAmount = await _commonHelper.GetCashbackAmount(customerId: customerId, amount: order.SubTotal - order.CouponDiscountAmount);

                if (cartAttribute.UseWalletAmount)
                {
                    order.WalletUsedAmount = walletBalance;
                    decimal grossTotal = order.SubTotal - order.CouponDiscountAmount + order.DeliveryFee - order.CashbackAmount;

                    if (order.WalletUsedAmount > grossTotal)
                    {
                        order.WalletUsedAmount = grossTotal;

                        if (order.PaymentMethodId != (int)PaymentMethod.Wallet)
                        {
                            response.Message = isEnglish ? Messages.PaymentMethodNotAvailable : MessagesAr.PaymentMethodNotAvailable;
                            return response;
                        }
                    }
                    else
                    {
                        if (order.PaymentMethodId == (int)PaymentMethod.Wallet)
                        {
                            response.Message = isEnglish ? Messages.PaymentMethodNotAvailable : MessagesAr.PaymentMethodNotAvailable;
                            return response;
                        }
                    }
                }

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
                order.Address = address;

                order.OrderItems = orderItems;

                order.Total = order.SubTotal + order.DeliveryFee - order.CouponDiscountAmount - order.CashbackAmount - order.WalletUsedAmount;
                if (order.PaymentMethodId == (int)PaymentMethod.KNET || order.PaymentMethodId == (int)PaymentMethod.VISAMASTER &&
                    order.PaymentMethodId == (int)PaymentMethod.Tabby)
                {
                    if (order.Total <= 0)
                    {
                        response.Message = isEnglish ? Messages.OrderAmountValidation : MessagesAr.OrderAmountValidation;
                        return response;
                    }
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

                    if (order.PaymentMethodId == (int)PaymentMethod.KNET)
                    {
                        var paymentUrlRequestModel = new PaymentUrlRequestModel
                        {
                            LangId = order.CustomerLanguageId.ToString(),
                            Amount = order.Total.ToString("N3"),
                            TrackId = order.OrderNumber.ToString(),
                            EntityId = order.Id.ToString(),
                            CustomerId = order.CustomerId.ToString(),
                            RequestType = PaymentRequestType.Order.ToString()
                        };

                        var paymentUrl = await _apiHelper.PostAsync<string>("Home/GetPaymentUrl", paymentUrlRequestModel, baseUrl: _appSettings.PaymentAPIUrl);
                        if (!string.IsNullOrEmpty(paymentUrl))
                        {
                            createPaymentModel.PaymentUrl = paymentUrl;
                            createPaymentModel.RedirectToPaymentPage = true;
                        }
                        else
                        {
                            response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
                            return response;
                        }
                    }
                    else if (order.PaymentMethodId == (int)PaymentMethod.VISAMASTER)
                    {
                        var value = Cryptography.Encrypt(PaymentRequestType.Order.ToString() + "-" + order.Id);
                        createPaymentModel.PaymentUrl = _appSettings.APIBaseUrl + _appSettings.MasterCardInteractionRequestUrl + "?value=" + value;
                        createPaymentModel.RedirectToPaymentPage = true;
                    }
                    else if (order.PaymentMethodId == (int)PaymentMethod.Cash)
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
                    else if (order.PaymentMethodId == (int)PaymentMethod.Wallet)
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
                    else if (order.PaymentMethodId == (int)PaymentMethod.Tabby)
                    {
                        var rootModel = await _tabbyHelper.PrepareOrderRootModel(order);
                        rootModel = await _tabbyHelper.CreateSession(rootModel);
                        if (rootModel != null && rootModel.status.ToLower() == "created" && rootModel.configuration != null && rootModel.configuration.available_products != null &&
                            rootModel.configuration.available_products.installments != null && rootModel.configuration.available_products.installments.Count > 0)
                        {
                            createPaymentModel.PaymentUrl = rootModel.configuration.available_products.installments[0].web_url;
                            createPaymentModel.RedirectToPaymentPage = true;

                            order.TabbySessionId = rootModel.id;
                            order.TabbyPaymentId = rootModel.payment.id;
                            await _orderService.UpdateOrder(order);
                        }
                        else
                        {
                            response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
                            return response;
                        }
                    }

                    if (order.DeviceTypeId == DeviceType.Web)
                        createPaymentModel.PaymentReturnUrl = _appSettings.WebsiteUrl + "ORD/" + order.OrderNumber;
                    else
                        createPaymentModel.PaymentReturnUrl = _appSettings.WebsiteUrl + "paymentresult";

                    createPaymentModel.OrderId = order.Id;
                    createPaymentModel.EntityId = order.Id;
                    createPaymentModel.EntityNumber = order.OrderNumber;
                    createPaymentModel.PaymentRequestTypeId = PaymentRequestType.Order;
                }

                response.Data = createPaymentModel;
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
                if (customer == null || customer.Deleted)
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
                if (customer == null || customer.Deleted)
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

                await _cartService.DeleteCartItemByCustomer(customerId: customer.Id);

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
    }
}