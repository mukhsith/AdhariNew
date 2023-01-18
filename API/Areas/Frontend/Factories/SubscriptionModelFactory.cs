using API.Areas.Frontend.Helpers;
using API.Helpers;
using Data.CouponPromotion;
using Data.CustomerManagement;
using Data.ProductManagement;
using Data.Sales;
using Data.Shop;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Frontend.Content;
using Services.Frontend.CouponPromotion;
using Services.Frontend.CustomerManagement;
using Services.Frontend.Locations;
using Services.Frontend.ProductManagement;
using Services.Frontend.Sales;
using Services.Frontend.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using Utility.API;
using Utility.Enum;
using Utility.Helpers;
using Utility.Models.Frontend.CustomizedModel;
using Utility.Models.Frontend.Sales;
using Utility.Models.Frontend.Shop;
using Utility.Models.KNET;
using Utility.ResponseMapper;

namespace API.Areas.Frontend.Factories
{
    public class SubscriptionModelFactory : ISubscriptionModelFactory
    {
        private readonly ILogger _logger;
        private readonly AppSettingsModel _appSettings;
        private readonly IModelHelper _modelHelper;
        private readonly ICustomerService _customerService;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly ICommonHelper _commonHelper;
        private readonly ICouponService _couponService;
        private readonly IAreaService _areaService;
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IAPIHelper _apiHelper;
        private readonly IPaymentHelper _paymentHelper;
        private readonly ITabbyHelper _tabbyHelper;
        public SubscriptionModelFactory(ILoggerFactory logger,
            IOptions<AppSettingsModel> options,
            IModelHelper modelHelper,
            ICustomerService customerService,
            ICartService cartService,
            IProductService productService,
            ICommonHelper commonHelper,
            ICouponService couponService,
            IAreaService areaService,
            IPaymentMethodService paymentMethodService,
            ISubscriptionService subscriptionService,
            IAPIHelper apiHelper,
            IPaymentHelper paymentHelper,
            ITabbyHelper tabbyHelper)
        {
            _logger = logger.CreateLogger(typeof(SubscriptionModelFactory).Name);
            _appSettings = options.Value;
            _modelHelper = modelHelper;
            _customerService = customerService;
            _cartService = cartService;
            _productService = productService;
            _commonHelper = commonHelper;
            _couponService = couponService;
            _areaService = areaService;
            _paymentMethodService = paymentMethodService;
            _subscriptionService = subscriptionService;
            _apiHelper = apiHelper;
            _paymentHelper = paymentHelper;
            _tabbyHelper = tabbyHelper;
        }
        public async Task<APIResponseModel<SubscriptionSummaryModel>> PrepareSubscriptionSummaryModel(bool isEnglish, int customerId)
        {
            var response = new APIResponseModel<SubscriptionSummaryModel>();
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

                var cartSummaryModel = await _modelHelper.PrepareSubscriptionSummaryModel(isEnglish: isEnglish, customer: customer);

                response.Data = cartSummaryModel;
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
        public async Task<APIResponseModel<SubscriptionSummaryModel>> SaveSubscriptionAttribute(bool isEnglish, int customerId,
            SubscriptionAttributeModel subscriptionAttributeModel, bool app = true)
        {
            var response = new APIResponseModel<SubscriptionSummaryModel>();
            try
            {
                if (customerId == 0 || subscriptionAttributeModel.AttributeTypeId == 0)
                {
                    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    return response;
                }

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

                var b2bCustomer = customer != null && customer.B2B;

                if (subscriptionAttributeModel.AttributeTypeId == AttributeType.Subscribe)
                {
                    if (!subscriptionAttributeModel.ProductId.HasValue || !subscriptionAttributeModel.Quantity.HasValue ||
                        !subscriptionAttributeModel.DurationId.HasValue || !subscriptionAttributeModel.DeliveryDateId.HasValue)
                    {
                        response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                        return response;
                    }

                    if (subscriptionAttributeModel.Quantity.Value > _appSettings.MaximumSubscriptionQuantityToPurchase)
                    {
                        response.Message = isEnglish ? string.Format(Messages.SubscriptionQuantityValidation, _appSettings.MaximumSubscriptionQuantityToPurchase) :
                           string.Format(MessagesAr.SubscriptionQuantityValidation, _appSettings.MaximumSubscriptionQuantityToPurchase);
                        return response;
                    }
                }

                if (subscriptionAttributeModel.AttributeTypeId == AttributeType.SelectAddress)
                {
                    if (!subscriptionAttributeModel.AddressId.HasValue)
                    {
                        response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                        return response;
                    }

                    var address = await _customerService.GetAddressById(subscriptionAttributeModel.AddressId.Value);
                    if (address == null)
                    {
                        response.Message = isEnglish ? Messages.AddressNotExists : MessagesAr.AddressNotExists;
                        return response;
                    }

                    if (address.CustomerId != customer.Id)
                    {
                        response.Message = isEnglish ? Messages.AddressNotExists : MessagesAr.AddressNotExists;
                        return response;
                    }
                }

                if (subscriptionAttributeModel.AttributeTypeId == AttributeType.AddWalletAmount)
                {
                    var walletBalance = await _customerService.GetWalletBalanceByCustomerId(id: customerId, walletTypeId: WalletType.Wallet);
                    if (walletBalance <= 0)
                    {
                        response.Message = isEnglish ? Messages.WalletBalanceLessThanActualAmount : MessagesAr.WalletBalanceLessThanActualAmount;
                        return response;
                    }
                }

                if (subscriptionAttributeModel.AttributeTypeId == AttributeType.ApplyCoupon)
                {
                    if (string.IsNullOrEmpty(subscriptionAttributeModel.CouponCode))
                    {
                        response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                        return response;
                    }
                }

                if (subscriptionAttributeModel.AttributeTypeId == AttributeType.SelectPaymentMethod)
                {
                    if (!subscriptionAttributeModel.PaymentMethodId.HasValue)
                    {
                        response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                        return response;
                    }
                }

                decimal subscriptionPrice = 0;
                var subscriptionAttribute = await _cartService.GetSubscriptionAttributeByCustomerId(customerId: customerId);
                if (subscriptionAttribute == null)
                {
                    var product = await _productService.GetById(subscriptionAttributeModel.ProductId.Value);
                    if (product == null)
                    {
                        response.Message = isEnglish ? Messages.ProductNotExists : MessagesAr.ProductNotExists;
                        return response;
                    }

                    var productValidationResponse = await ValidateSubscriptionProduct(product: product, isEnglish: isEnglish, quantity: subscriptionAttributeModel.Quantity.Value,
                        customerId: customer.Id);
                    if (!productValidationResponse.Success)
                    {
                        response.Message = productValidationResponse.Message;
                        return response;
                    }

                    decimal price = product.GetPriceFrontend(b2bCustomer);
                    decimal discountedPrice = product.GetDiscountedPriceFrontend(b2bCustomer);

                    subscriptionPrice = (discountedPrice > 0 ? discountedPrice : price) * subscriptionAttributeModel.Quantity.Value;

                    if (subscriptionAttributeModel.AttributeTypeId == AttributeType.Subscribe)
                    {
                        subscriptionAttribute = await _cartService.CreateSubscriptionAttribute(new SubscriptionAttribute
                        {
                            CustomerId = customerId,
                            ProductId = subscriptionAttributeModel.ProductId,
                            Quantity = subscriptionAttributeModel.Quantity,
                            DurationId = subscriptionAttributeModel.DurationId,
                            DeliveryDateId = subscriptionAttributeModel.DeliveryDateId
                        });
                    }
                }
                else
                {
                    var product = await _productService.GetById(subscriptionAttribute.ProductId.Value);
                    if (product == null)
                    {
                        response.Message = isEnglish ? Messages.ProductNotExists : MessagesAr.ProductNotExists;
                        return response;
                    }

                    decimal price = product.GetPriceFrontend(b2bCustomer);
                    decimal discountedPrice = product.GetDiscountedPriceFrontend(b2bCustomer);

                    subscriptionPrice = (discountedPrice > 0 ? discountedPrice : price) * subscriptionAttribute.Quantity.Value;

                    if (subscriptionAttributeModel.AttributeTypeId == AttributeType.Subscribe)
                    {
                        var productValidationResponse = await ValidateSubscriptionProduct(product: product, isEnglish: isEnglish, quantity: subscriptionAttributeModel.Quantity.Value,
                        customerId: customer.Id);
                        if (!productValidationResponse.Success)
                        {
                            response.Message = productValidationResponse.Message;
                            return response;
                        }

                        subscriptionAttribute.ProductId = subscriptionAttributeModel.ProductId;
                        subscriptionAttribute.Quantity = subscriptionAttributeModel.Quantity;
                        subscriptionAttribute.DurationId = subscriptionAttributeModel.DurationId;
                        subscriptionAttribute.DeliveryDateId = subscriptionAttributeModel.DeliveryDateId;
                        await _cartService.UpdateSubscriptionAttribute(subscriptionAttribute);
                    }
                    else if (subscriptionAttributeModel.AttributeTypeId == AttributeType.SelectAddress)
                    {
                        subscriptionAttribute.AddressId = subscriptionAttributeModel.AddressId;
                        await _cartService.UpdateSubscriptionAttribute(subscriptionAttribute);
                    }
                    else if (subscriptionAttributeModel.AttributeTypeId == AttributeType.AddWalletAmount)
                    {
                        subscriptionAttribute.UseWalletAmount = true;
                        await _cartService.UpdateSubscriptionAttribute(subscriptionAttribute);
                    }
                    else if (subscriptionAttributeModel.AttributeTypeId == AttributeType.RemoveWalletAmount)
                    {
                        subscriptionAttribute.UseWalletAmount = false;
                        if (subscriptionAttribute.PaymentMethodId == (int)PaymentMethod.Wallet)
                        {
                            subscriptionAttribute.PaymentMethodId = subscriptionAttribute.OtherPaymentMethodId;
                            subscriptionAttribute.OtherPaymentMethodId = null;
                            //if (!subscriptionAttribute.PaymentMethodId.HasValue)
                            //{
                            //    var paymentMethods = await _paymentMethodService.GetAllPaymentMethod(paymentRequestType: PaymentRequestType.Order);
                            //    if (paymentMethods.Count > 0)
                            //    {
                            //        subscriptionAttribute.PaymentMethodId = paymentMethods.FirstOrDefault().Id;
                            //    }
                            //}
                        }
                        await _cartService.UpdateSubscriptionAttribute(subscriptionAttribute);
                    }
                    else if (subscriptionAttributeModel.AttributeTypeId == AttributeType.ApplyCoupon)
                    {
                        var coupon = await _couponService.GetByCode(subscriptionAttributeModel.CouponCode);
                        if (coupon == null)
                        {
                            response.Message = isEnglish ? Messages.CouponNotExists : MessagesAr.CouponNotExists;
                            return response;
                        }

                        var validationMessage = _commonHelper.CouponValidation(coupon: coupon, isEnglish: isEnglish, total: subscriptionPrice);
                        if (!string.IsNullOrEmpty(validationMessage))
                        {
                            response.Message = validationMessage;
                            return response;
                        }

                        subscriptionAttribute.CouponId = coupon.Id;
                        await _cartService.UpdateSubscriptionAttribute(subscriptionAttribute);
                    }
                    else if (subscriptionAttributeModel.AttributeTypeId == AttributeType.RemoveCoupon)
                    {
                        subscriptionAttribute.CouponId = null;
                        await _cartService.UpdateSubscriptionAttribute(subscriptionAttribute);
                    }
                    else if (subscriptionAttributeModel.AttributeTypeId == AttributeType.SelectPaymentMethod)
                    {
                        subscriptionAttribute.PaymentMethodId = subscriptionAttributeModel.PaymentMethodId;
                        await _cartService.UpdateSubscriptionAttribute(subscriptionAttribute);
                    }
                    else if (subscriptionAttributeModel.AttributeTypeId == AttributeType.Notes)
                    {
                        subscriptionAttribute.Notes = subscriptionAttributeModel.Notes;
                        await _cartService.UpdateSubscriptionAttribute(subscriptionAttribute);
                    }
                }

                var subscriptionSummaryModel = await _modelHelper.PrepareSubscriptionSummaryModel(isEnglish: isEnglish, customer: customer, app: app);

                response.Data = subscriptionSummaryModel;
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
        public async Task<APIResponseModel<SubscriptionCheckOutModel>> PrepareSubscriptionCheckOutModel(bool isEnglish, int customerId, bool app = true)
        {
            var response = new APIResponseModel<SubscriptionCheckOutModel>();
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

                var subscriptionCheckOutModel = await _modelHelper.PrepareSubscriptionCheckOutModel(isEnglish: isEnglish, customer: customer, app: app);

                response.Data = subscriptionCheckOutModel;
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
        public async Task<APIResponseModel<CreatePaymentModel>> CreateSubscription(bool isEnglish, int customerId, DeviceType deviceTypeId,
            CreatePaymentModel createPaymentModel)
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

                var subscriptionAttribute = await _cartService.GetSubscriptionAttributeByCustomerId(customerId: customerId);
                if (subscriptionAttribute == null)
                {
                    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    return response;
                }

                if (!subscriptionAttribute.ProductId.HasValue || !subscriptionAttribute.Quantity.HasValue ||
                !subscriptionAttribute.DurationId.HasValue || !subscriptionAttribute.DeliveryDateId.HasValue ||
                !subscriptionAttribute.AddressId.HasValue || !subscriptionAttribute.PaymentMethodId.HasValue)
                {
                    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    return response;
                }

                if (subscriptionAttribute.Quantity.Value > _appSettings.MaximumSubscriptionQuantityToPurchase)
                {
                    response.Message = isEnglish ? string.Format(Messages.SubscriptionQuantityValidation, _appSettings.MaximumSubscriptionQuantityToPurchase) :
                       string.Format(MessagesAr.SubscriptionQuantityValidation, _appSettings.MaximumSubscriptionQuantityToPurchase);
                    return response;
                }

                var product = await _productService.GetById(subscriptionAttribute.ProductId.Value);
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

                if (product.ProductType != ProductType.SubscriptionProduct)
                {
                    response.Message = isEnglish ? string.Format(Messages.YouCannotSubscribeThisProduct, name) : string.Format(MessagesAr.YouCannotSubscribeThisProduct, name);
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

                if (subscriptionAttribute.Quantity.Value <= 0)
                {
                    response.Message = isEnglish ? string.Format(Messages.CartZeroQuantityValidation, name) : string.Format(MessagesAr.CartZeroQuantityValidation, name);
                    return response;
                }

                var productStockQuantity = 0;
                int lowStockProduct = 0;
                var productDetails = product.ProductDetails.ToList();
                foreach (var productDetail in productDetails)
                {
                    var childProductStockQuantity = await _productService.GetAvailableStockQuantity(productId: productDetail.ChildProductId,
                customerId: customer.Id);
                    if (childProductStockQuantity <= (subscriptionAttribute.Quantity.Value * productDetail.Quantity))
                        lowStockProduct++;
                }

                if (lowStockProduct > 0)
                    productStockQuantity = 0;
                else
                    productStockQuantity = subscriptionAttribute.Quantity.Value;

                if (subscriptionAttribute.Quantity.Value > productStockQuantity)
                {
                    response.Message = isEnglish ? string.Format(Messages.ProductIsOutOfStock, productStockQuantity) : string.Format(MessagesAr.ProductIsOutOfStock, productStockQuantity);
                    return response;
                }

                Address address = await _customerService.GetAddressById(subscriptionAttribute.AddressId.Value);
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

                var paymentMethod = await _paymentMethodService.GetPaymentMethodById(subscriptionAttribute.PaymentMethodId.Value);
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

                if (!paymentMethod.SubscriptionCheckoutRegisteredCustomer)
                {
                    response.Message = isEnglish ? Messages.PaymentMethodNotAvailable : MessagesAr.PaymentMethodNotAvailable;
                    return response;
                }

                var subscriptionDuration = await _productService.GetSubscriptionDurationById(subscriptionAttribute.DurationId.Value);
                if (subscriptionDuration == null || subscriptionDuration.Deleted)
                {
                    response.Message = isEnglish ? Messages.SubscriptionDurationNotExists : MessagesAr.SubscriptionDurationNotExists;
                    return response;
                }

                if (!subscriptionDuration.Active)
                {
                    response.Message = isEnglish ? Messages.SubscriptionDurationNotActive : MessagesAr.SubscriptionDurationNotActive;
                    return response;
                }

                if (product.SpecialPackage)
                {
                    if (subscriptionDuration.Id != product.SubscriptionDurationId)
                    {
                        response.Message = isEnglish ? Messages.SubscriptionDurationNotAvailable : MessagesAr.SubscriptionDurationNotAvailable;
                        return response;
                    }
                }

                var subscriptionDeliveryDate = await _productService.GetSubscriptionDeliveryDateById(subscriptionAttribute.DeliveryDateId.Value);
                if (subscriptionDeliveryDate == null || subscriptionDeliveryDate.Deleted)
                {
                    response.Message = isEnglish ? Messages.SubscriptionDeliveryDateNotExists : MessagesAr.SubscriptionDeliveryDateNotExists;
                    return response;
                }

                if (!subscriptionDeliveryDate.Active)
                {
                    response.Message = isEnglish ? Messages.SubscriptionDeliveryDateNotActive : MessagesAr.SubscriptionDeliveryDateNotActive;
                    return response;
                }

                decimal walletBalance = 0;
                if (subscriptionAttribute.UseWalletAmount)
                {
                    walletBalance = await _customerService.GetWalletBalanceByCustomerId(id: customerId, walletTypeId: WalletType.Wallet);
                    if (walletBalance <= 0)
                    {
                        response.Message = isEnglish ? Messages.WalletBalanceLessThanActualAmount : MessagesAr.WalletBalanceLessThanActualAmount;
                        return response;
                    }
                }

                bool b2bCustomer = customer != null && customer.B2B;
                decimal price = product.GetPriceFrontend(b2bCustomer);
                decimal discountedPrice = product.GetDiscountedPriceFrontend(b2bCustomer);

                bool fullPayment = false;
                decimal subscriptionPrice = (discountedPrice > 0 ? discountedPrice : price) * subscriptionAttribute.Quantity.Value;

                if (subscriptionAttribute.PaymentMethodId.Value == (int)PaymentMethod.Tabby)
                {
                    fullPayment = true;
                    if (!product.SpecialPackage)
                        subscriptionPrice *= subscriptionDuration.NumberOfMonths;
                }
                else
                {
                    if (product.SpecialPackage)
                        fullPayment = true;
                }

                List<SubscriptionItemDetail> subscriptionItemDetails = new();
                foreach (var productDetail in productDetails)
                {
                    var subscriptionItemDetail = new SubscriptionItemDetail
                    {
                        ProductId = productDetail.ProductId,
                        ChildProductId = productDetail.ChildProductId,
                        Quantity = productDetail.Quantity
                    };

                    subscriptionItemDetails.Add(subscriptionItemDetail);
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

                decimal discountAmount = discountedPrice > 0 ? price - discountedPrice : 0;

                string notes = string.Empty;
                if (deviceTypeId == DeviceType.Web)
                    notes = createPaymentModel.Notes;
                else
                    notes = subscriptionAttribute.Notes;

                var subscription = new Subscription
                {
                    CustomerId = customer.Id,
                    SubscriptionNumber = string.Empty,
                    SubscriptionStatusId = SubscriptionStatus.Pending,
                    ProductId = subscriptionAttribute.ProductId.Value,
                    Quantity = subscriptionAttribute.Quantity.Value,
                    DurationId = subscriptionAttribute.DurationId.Value,
                    NumberOfMonths = subscriptionDuration.NumberOfMonths,
                    DeliveryDateId = subscriptionAttribute.DeliveryDateId.Value,
                    UnitPrice = price,
                    B2BPrice = b2bCustomer,
                    DiscountType = discountAmount > 0 ? DiscountType.Amount : DiscountType.NoDiscount,
                    DiscountValueApplied = discountAmount,
                    DiscountAmount = discountAmount,
                    SubTotal = subscriptionPrice,
                    DeliveryFee = fullPayment ? deliveryFee * subscriptionDuration.NumberOfMonths : deliveryFee,
                    CustomerLanguageId = isEnglish ? 1 : 2,
                    CustomerIp = createPaymentModel.CustomerIp,
                    CreatedOn = DateTime.Now,
                    DeviceTypeId = deviceTypeId,
                    SpecialPackage = product.SpecialPackage,
                    FullPayment = fullPayment,
                    Address = address,
                    SubscriptionItemDetails = subscriptionItemDetails,
                    Notes = notes
                };

                Coupon coupon = null;
                if (subscriptionAttribute.CouponId.HasValue)
                {
                    coupon = await _couponService.GetById(subscriptionAttribute.CouponId.Value);
                    if (coupon == null)
                    {
                        response.Message = isEnglish ? Messages.CouponNotValid : MessagesAr.CouponNotValid;
                        return response;
                    }

                    var couponValidation = _commonHelper.CouponValidation(coupon: coupon, isEnglish: isEnglish, total: subscription.SubTotal);
                    if (!string.IsNullOrEmpty(couponValidation))
                    {
                        response.Message = couponValidation;
                        return response;
                    }

                    subscription.CouponId = subscriptionAttribute.CouponId;
                    subscription.CouponDiscountType = coupon.DiscountType;
                    subscription.CouponDiscountValueApplied = coupon.DiscountType == DiscountType.Percentage ? coupon.DiscountPercentage : coupon.DiscountAmount;
                    subscription.CouponDiscountAmount = coupon.ApplyCouponDiscount2(subscription.SubTotal);
                }

                subscription.CashbackAmount = await _commonHelper.GetCashbackAmount(customerId: customerId, amount: subscription.SubTotal - subscription.CouponDiscountAmount);

                if (subscriptionAttribute.UseWalletAmount)
                {
                    subscription.WalletUsedAmount = walletBalance;
                    decimal grossTotal = subscription.SubTotal - subscription.CouponDiscountAmount + subscription.DeliveryFee - subscription.CashbackAmount;

                    if (subscription.WalletUsedAmount > grossTotal)
                    {
                        subscription.WalletUsedAmount = grossTotal;

                        if (subscriptionAttribute.PaymentMethodId != (int)PaymentMethod.Wallet)
                        {
                            response.Message = isEnglish ? Messages.PaymentMethodNotAvailable : MessagesAr.PaymentMethodNotAvailable;
                            return response;
                        }
                    }
                    else
                    {
                        if (subscriptionAttribute.PaymentMethodId == (int)PaymentMethod.Wallet)
                        {
                            response.Message = isEnglish ? Messages.PaymentMethodNotAvailable : MessagesAr.PaymentMethodNotAvailable;
                            return response;
                        }
                    }
                }

                subscription.Total = subscription.SubTotal + subscription.DeliveryFee - subscription.CouponDiscountAmount - subscription.WalletUsedAmount
                    - subscription.CashbackAmount;
                if (subscriptionAttribute.PaymentMethodId == (int)PaymentMethod.KNET || subscriptionAttribute.PaymentMethodId == (int)PaymentMethod.VISAMASTER &&
                    subscriptionAttribute.PaymentMethodId == (int)PaymentMethod.Tabby)
                {
                    if (subscription.Total <= 0)
                    {
                        response.Message = isEnglish ? Messages.SubscriptionAmountValidation : MessagesAr.SubscriptionAmountValidation;
                        return response;
                    }
                }

                var dateAndSlot = await _commonHelper.GetAvailableSubscriptionOrderDeliveryDateAndSlot(subscriptionDeliveryDate);
                decimal orderSubTotal;
                decimal orderDeliveryFee;
                decimal orderTotal;
                if (subscription.FullPayment)
                {
                    decimal monthlyAmount = subscription.Total / subscription.NumberOfMonths;
                    orderSubTotal = monthlyAmount;
                    orderDeliveryFee = 0;
                    orderTotal = monthlyAmount;
                }
                else
                {
                    orderSubTotal = subscription.SubTotal - subscription.CouponDiscountAmount;
                    orderDeliveryFee = subscription.DeliveryFee;
                    orderTotal = subscription.SubTotal - subscription.CouponDiscountAmount + subscription.DeliveryFee;
                }

                var subscriptionOrder = new SubscriptionOrder
                {
                    SubTotal = orderSubTotal,
                    DeliveryFee = orderDeliveryFee,
                    Total = orderTotal,
                    PaymentMethodId = subscriptionAttribute.PaymentMethodId.Value,
                    PaymentStatusId = PaymentStatus.Canceled,
                    DeliveryDate = dateAndSlot.Item1,
                    DeliveryTimeSlotId = dateAndSlot.Item2,
                    FirstOrder = true
                };

                DateTime nextExpectedDelivery = new(subscriptionOrder.DeliveryDate.AddMonths(1).Year, subscriptionOrder.DeliveryDate.AddMonths(1).Month,
                    subscriptionDeliveryDate.FromDay);
                subscription.NextExpectedDelivery = nextExpectedDelivery;

                List<SubscriptionOrder> subscriptionOrders = new();
                subscriptionOrders.Add(subscriptionOrder);
                subscription.SubscriptionOrders = subscriptionOrders;

                subscription = await _subscriptionService.CreateSubscription(subscription);
                if (subscription != null)
                {
                    var subscriptionNumber = string.Empty;
                    Subscription subscriptionBySubscriptionNumber = null;
                    do
                    {
                        subscriptionNumber = "10" + Common.GenerateRandomNo();
                        subscriptionBySubscriptionNumber = await _subscriptionService.GetSubscriptionBySubscriptionNumber(subscriptionNumber);
                    }
                    while (subscriptionBySubscriptionNumber != null);

                    subscription.SubscriptionNumber = subscriptionNumber;
                    await _subscriptionService.UpdateSubscription(subscription);

                    var subscriptionOrderNumber = string.Empty;
                    subscriptionOrder = subscription.SubscriptionOrders.Where(a => a.FirstOrder).FirstOrDefault();
                    SubscriptionOrder subscriptionOrderByOrderNumber = null;
                    do
                    {
                        subscriptionOrderNumber = "11" + Common.GenerateRandomNo();
                        subscriptionOrderByOrderNumber = await _subscriptionService.GetSubscriptionOrderByOrderNumber(subscriptionOrderNumber);
                    }
                    while (subscriptionOrderByOrderNumber != null);

                    subscriptionOrder.OrderNumber = subscriptionOrderNumber;
                    await _subscriptionService.UpdateSubscriptionOrder(subscriptionOrder);

                    await _cartService.CreateSubscriptionHolding(new SubscriptionHolding
                    {
                        CustomerId = subscription.CustomerId,
                        ProductId = subscription.ProductId,
                        Quantity = subscription.Quantity,
                        HoldUntil = DateTime.Now.AddMinutes(5)
                    });

                    if (subscriptionAttribute.PaymentMethodId == (int)PaymentMethod.KNET)
                    {
                        var paymentUrlRequestModel = new PaymentUrlRequestModel
                        {
                            LangId = subscription.CustomerLanguageId.ToString(),
                            Amount = subscription.Total.ToString("N3"),
                            TrackId = subscription.SubscriptionNumber.ToString(),
                            EntityId = subscriptionOrder.Id.ToString(),
                            CustomerId = subscription.CustomerId.ToString(),
                            RequestType = PaymentRequestType.SubscriptionOrder.ToString()
                        };

                        var paymentUrl = await _apiHelper.PostAsync<string>("Home/GetPaymentUrl", paymentUrlRequestModel, baseUrl: _appSettings.PaymentAPIUrl);
                        if (!string.IsNullOrEmpty(paymentUrl))
                        {
                            createPaymentModel.PaymentUrl = paymentUrl;
                            createPaymentModel.RedirectToPaymentPage = true;
                        }
                    }
                    else if (subscriptionAttribute.PaymentMethodId == (int)PaymentMethod.VISAMASTER)
                    {
                        var value = Cryptography.Encrypt(PaymentRequestType.SubscriptionOrder.ToString() + "-" + subscriptionOrder.Id);
                        createPaymentModel.PaymentUrl = _appSettings.APIBaseUrl + _appSettings.MasterCardInteractionRequestUrl + "?value=" + value;
                        createPaymentModel.RedirectToPaymentPage = true;
                    }
                    else if (subscriptionAttribute.PaymentMethodId == (int)PaymentMethod.Cash)
                    {
                        var paymentResponseModel = new PaymentResponseModel()
                        {
                            Amount = subscription.Total.ToString("N3"),
                            EntityId = subscriptionOrder.Id.ToString(),
                            RequestType = PaymentRequestType.SubscriptionOrder.ToString(),
                            Result = "captured"
                        };

                        var url = await _paymentHelper.UpdatePaymentEntity(paymentResponseModel);
                    }
                    else if (subscriptionAttribute.PaymentMethodId == (int)PaymentMethod.Wallet)
                    {
                        var paymentResponseModel = new PaymentResponseModel()
                        {
                            Amount = subscription.Total.ToString("N3"),
                            EntityId = subscriptionOrder.Id.ToString(),
                            RequestType = PaymentRequestType.SubscriptionOrder.ToString(),
                            Result = "captured"
                        };

                        var url = await _paymentHelper.UpdatePaymentEntity(paymentResponseModel);
                    }
                    else if (subscriptionAttribute.PaymentMethodId == (int)PaymentMethod.Tabby)
                    {
                        var rootModel = await _tabbyHelper.PrepareSubscriptionRootModel(subscription);
                        rootModel = await _tabbyHelper.CreateSession(rootModel);
                        if (rootModel != null && rootModel.status.ToLower() == "created" && rootModel.configuration != null && rootModel.configuration.available_products != null &&
                            rootModel.configuration.available_products.installments != null && rootModel.configuration.available_products.installments.Count > 0)
                        {
                            createPaymentModel.PaymentUrl = rootModel.configuration.available_products.installments[0].web_url;
                            createPaymentModel.RedirectToPaymentPage = true;

                            subscription.TabbySessionId = rootModel.id;
                            subscription.TabbyPaymentId = rootModel.payment.id;
                            await _subscriptionService.UpdateSubscription(subscription);
                        }
                        else
                        {
                            response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
                            return response;
                        }
                    }

                    if (subscription.DeviceTypeId == DeviceType.Web)
                        createPaymentModel.PaymentReturnUrl = _appSettings.WebsiteUrl + "SUB/" + subscription.SubscriptionNumber;
                    else
                        createPaymentModel.PaymentReturnUrl = _appSettings.WebsiteUrl + "paymentresult";

                    createPaymentModel.EntityId = subscription.Id;
                    createPaymentModel.EntityNumber = subscription.SubscriptionNumber;
                    createPaymentModel.PaymentRequestTypeId = PaymentRequestType.SubscriptionOrder;
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
        public async Task<APIResponseModel<List<SubscriptionModel>>> GetSubscriptions(bool isEnglish, int customerId, int id = 0, string subscriptionNumber = "",
            int limit = 0, int page = 0, SubscriptionStatus? subscriptionStatus = null)
        {
            var response = new APIResponseModel<List<SubscriptionModel>>();
            try
            {
                var subscriptions = new List<Subscription>();

                if (string.IsNullOrEmpty(subscriptionNumber))
                {
                    if (customerId == 0)
                    {
                        return response;
                    }

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
                }

                bool loadDetails = false;
                if (id > 0)
                {
                    loadDetails = true;
                    var subscription = await _subscriptionService.GetSubscriptionById(id);
                    if (subscription != null && !subscription.Deleted && subscription.Customer != null)
                    {
                        if (subscription.CustomerId != customerId)
                        {
                            response.Message = isEnglish ? Messages.InvalidCustomer : MessagesAr.InvalidCustomer;
                            return response;
                        }

                        subscriptions.Add(subscription);
                    }
                }
                else if (!string.IsNullOrEmpty(subscriptionNumber))
                {
                    loadDetails = true;
                    var subscription = await _subscriptionService.GetSubscriptionBySubscriptionNumber(subscriptionNumber);
                    if (subscription != null && !subscription.Deleted)
                        subscriptions.Add(subscription);
                }
                else
                {
                    subscriptions = await _subscriptionService.GetAllSubscription(customerId: customerId, subscriptionStatus: subscriptionStatus);
                }

                subscriptions = subscriptions.OrderByDescending(a => a.Id).ToList();

                response.DataRecordCount = subscriptions.Count;

                if (limit > 0 && page > 0)
                {
                    subscriptions = subscriptions.Skip((page - 1) * limit).Take(limit).ToList();
                }

                var subscriptionModels = new List<SubscriptionModel>();
                foreach (var subscription in subscriptions)
                {
                    var subscriptionModel = await _modelHelper.PrepareSubscriptionModel(subscription, isEnglish, loadDetails: loadDetails);
                    subscriptionModels.Add(subscriptionModel);
                }

                response.Data = subscriptionModels;
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
        public async Task<APIResponseModel<List<SubscriptionAdminModel>>> GetSubscriptionsAdmin(bool isEnglish, int id = 0, string subscriptionNumber = "",
            int limit = 0, int page = 0, SubscriptionStatus? subscriptionStatus = null)
        {
            var response = new APIResponseModel<List<SubscriptionAdminModel>>();
            try
            {
                var subscriptions = new List<Subscription>();

                bool loadDetails = false;
                if (id > 0)
                {
                    loadDetails = true;
                    var subscription = await _subscriptionService.GetSubscriptionById(id);
                    if (subscription != null && !subscription.Deleted && subscription.Customer != null)
                    {
                        subscriptions.Add(subscription);
                    }
                }
                else if (!string.IsNullOrEmpty(subscriptionNumber))
                {
                    loadDetails = true;
                    var subscription = await _subscriptionService.GetSubscriptionBySubscriptionNumber(subscriptionNumber);
                    if (subscription != null && !subscription.Deleted)
                        subscriptions.Add(subscription);
                }
                else
                {
                    subscriptions = await _subscriptionService.GetAllSubscription(subscriptionStatus: subscriptionStatus);
                }

                subscriptions = subscriptions.OrderByDescending(a => a.Id).ToList();

                response.DataRecordCount = subscriptions.Count;

                if (limit > 0 && page > 0)
                {
                    subscriptions = subscriptions.Skip((page - 1) * limit).Take(limit).ToList();
                }

                var subscriptionModels = new List<SubscriptionAdminModel>();
                foreach (var subscription in subscriptions)
                {
                    var subscriptionModel = await _modelHelper.PrepareSubscriptionModel1(subscription, isEnglish, loadDetails: loadDetails);
                    subscriptionModels.Add(subscriptionModel);
                }

                response.Data = subscriptionModels;
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
        public async Task<APIResponseModel<object>> CreateSubscriptionOrders(bool isEnglish, string apiKey)
        {
            var response = new APIResponseModel<object>();

            try
            {
                if (_appSettings.ServiceAPIKey != apiKey)
                {
                    response.Message = isEnglish ? Messages.AccessRightInvalid : MessagesAr.AccessRightInvalid;
                    return response;
                }

                var subscriptions = await _subscriptionService.GetAllSubscriptionByNextExpectedDelivery(DateTime.Now.Date);
                foreach (var subscription in subscriptions)
                {
                    var subscriptionDeliveryDate = await _productService.GetSubscriptionDeliveryDateById(subscription.DeliveryDateId);
                    if (subscriptionDeliveryDate != null)
                    {
                        var dateAndSlot = await _commonHelper.GetAvailableSubscriptionOrderDeliveryDateAndSlot(subscriptionDeliveryDate);
                        if (dateAndSlot != null)
                        {
                            decimal orderSubTotal;
                            decimal orderDeliveryFee;
                            decimal orderTotal;
                            if (subscription.FullPayment)
                            {
                                decimal monthlyAmount = subscription.Total / subscription.NumberOfMonths;
                                orderSubTotal = monthlyAmount;
                                orderDeliveryFee = 0;
                                orderTotal = monthlyAmount;
                            }
                            else
                            {
                                orderSubTotal = subscription.SubTotal - subscription.CouponDiscountAmount;
                                orderDeliveryFee = subscription.DeliveryFee;
                                orderTotal = subscription.SubTotal - subscription.CouponDiscountAmount + subscription.DeliveryFee;
                            }

                            var subscriptionOrder = new SubscriptionOrder
                            {
                                SubscriptionId = subscription.Id,
                                SubTotal = orderSubTotal,
                                DeliveryFee = orderDeliveryFee,
                                Total = orderTotal,
                                DeliveryDate = dateAndSlot.Item1,
                                DeliveryTimeSlotId = dateAndSlot.Item2,
                                BankServiceCharge = 0,
                                Delivered = false,
                                Confirmed = subscription.FullPayment
                            };

                            subscriptionOrder = await _subscriptionService.CreateSubscriptionOrder(subscriptionOrder);
                            if (subscriptionOrder != null)
                            {
                                DateTime nextExpectedDelivery = new(subscriptionOrder.DeliveryDate.AddMonths(1).Year,
                                                                       subscriptionOrder.DeliveryDate.AddMonths(1).Month, subscriptionDeliveryDate.FromDay);
                                subscription.NextExpectedDelivery = nextExpectedDelivery;
                                await _subscriptionService.UpdateSubscription(subscription);
                            }
                        }
                    }
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

        #region Utilities
        public async Task<APIResponseModel<object>> ValidateSubscriptionProduct(Product product, bool isEnglish, int quantity, int customerId)
        {
            var response = new APIResponseModel<object>();

            if (product.Deleted)
            {
                response.Message = isEnglish ? Messages.ProductNotExists : MessagesAr.ProductNotExists;
                return response;
            }

            string productName = isEnglish ? product.NameEn : product.NameAr;

            if (!product.Active)
            {
                response.Message = isEnglish ? string.Format(Messages.ProductNotActiveWithName, productName) : string.Format(MessagesAr.ProductNotActiveWithName, productName);
                return response;
            }

            if (product.ProductType != ProductType.SubscriptionProduct)
            {
                response.Message = isEnglish ? Messages.YouCannotSubscribeThisProduct : MessagesAr.YouCannotSubscribeThisProduct;
                return response;
            }

            if (quantity <= 0)
            {
                response.Message = isEnglish ? Messages.CartQuantityValidation : MessagesAr.CartQuantityValidation;
                return response;
            }

            int lowStockProduct = 0;
            var productDetails = product.ProductDetails.ToList();
            foreach (var productDetail in productDetails)
            {
                var childProductStockQuantity = await _productService.GetAvailableStockQuantity(productId: productDetail.ChildProductId,
                                                    customerId: customerId);
                if (childProductStockQuantity < (quantity * productDetail.Quantity))
                    lowStockProduct++;
            }

            int productStockQuantity;
            if (lowStockProduct > 0)
                productStockQuantity = 0;
            else
                productStockQuantity = quantity;

            if (quantity > productStockQuantity)
            {
                response.Message = isEnglish ? string.Format(Messages.ProductIsOutOfStock, productStockQuantity) : string.Format(MessagesAr.ProductIsOutOfStock, productStockQuantity);
                return response;
            }

            response.Message = isEnglish ? Messages.Success : MessagesAr.Success;
            response.Success = true;

            return response;
        }
        #endregion
    }
}
