//using API.Areas.Frontend.Helpers;
using API.Areas.Backend.Helpers;
using API.Helpers;
using AutoMapper;
using Data.CouponPromotion;
using Data.Shop;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Frontend.CouponPromotion;
using Services.Frontend.CustomerManagement;
using Services.Frontend.Locations;
using Services.Frontend.ProductManagement;
using Services.Frontend.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;
using Utility.Helpers;
using Utility.Models.Frontend.CustomizedModel;
using Utility.Models.Frontend.Shop;
using Utility.ResponseMapper;

namespace API.Areas.Backend.Factories
{
    public class CartModelFactory : ICartModelFactory
    {
        private readonly ILogger _logger;
        private readonly AppSettingsModel _appSettings;
        private readonly IModelHelper _modelHelper;
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly ICommonHelper _commonHelper;
        private readonly ICategoryService _categoryService;
        private readonly ICouponService _couponService;
        private readonly IPromotionService _promotionService;
        private readonly IAreaService _areaService;
        public CartModelFactory(ILoggerFactory logger,
            IOptions<AppSettingsModel> options,
            IModelHelper modelHelper,
            ICustomerService customerService,
            ICartService cartService,
            IProductService productService,
            ICommonHelper commonHelper,
            IMapper mapper,
            ICategoryService categoryService,
            ICouponService couponService,
            IPromotionService promotionService,
            IAreaService areaService)
        {
            _logger = logger.CreateLogger(typeof(CartModelFactory).Name);
            _appSettings = options.Value;
            _modelHelper = modelHelper;
            _customerService = customerService;
            _cartService = cartService;
            _productService = productService;
            _commonHelper = commonHelper;
            _mapper = mapper;
            _categoryService = categoryService;
            _couponService = couponService;
            _promotionService = promotionService;
            _areaService = areaService;
        }
        public async Task<APIResponseModel<object>> PrepareCartItemCount(bool isEnglish, int customerId = 0, string customerGuidValue = "")
        {
            var response = new APIResponseModel<object>();

            try
            {
                if (customerId == 0 && string.IsNullOrEmpty(customerGuidValue))
                {
                    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    return response;
                }

                if (customerId > 0)
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
                }

                var cartItems = await _cartService.GetAllCartItem(customerGuidValue: customerGuidValue, customerId: customerId);

                string subTotal = string.Empty;
                if (cartItems.Count > 0)
                {
                    var cartModel = await _modelHelper.PrepareCartModel(cartItems, isEnglish);
                    subTotal = cartModel.FormattedSubTotal;
                }
                else
                {
                    subTotal = await _commonHelper.ConvertDecimalToString(value: 0, isEnglish: isEnglish,
                       countryId: 0, includeZero: true);
                }

                response.Data = cartItems.Count + "," + subTotal;
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
        public async Task<APIResponseModel<CartModel>> PrepareCart(bool isEnglish, int customerId = 0, string customerGuidValue = "")
        {
            var response = new APIResponseModel<CartModel>();
            try
            {
                if (customerId == 0 && string.IsNullOrEmpty(customerGuidValue))
                {
                    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    return response;
                }

                if (customerId > 0)
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
                }

                var cartItems = await _cartService.GetAllCartItem(customerGuidValue: customerGuidValue, customerId: customerId);
                var cartModel = await _modelHelper.PrepareCartModel(cartItems, isEnglish);

                response.Data = cartModel;
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
        public async Task<APIResponseModel<bool>> AddCartItem(bool isEnglish, CartItemModel cartItemModel)
        {
            var response = new APIResponseModel<bool>();
            try
            {
                if (cartItemModel.CustomerId.HasValue && cartItemModel.CustomerId.Value > 0)
                {
                    var customer = await _customerService.GetCustomerById(cartItemModel.CustomerId.Value);
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
                else
                {
                    cartItemModel.CustomerId = null;
                }

                var product = await _productService.GetById(cartItemModel.ProductId);
                if (product == null)
                {
                    response.Message = isEnglish ? Messages.ProductNotExists : MessagesAr.ProductNotExists;
                    return response;
                }

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

                if (product.ProductType == ProductType.SubscriptionProduct)
                {
                    response.Message = isEnglish ? Messages.YouCannotAddThisProductToCart : MessagesAr.YouCannotAddThisProductToCart;
                    return response;
                }

                if (cartItemModel.Quantity <= 0)
                {
                    response.Message = isEnglish ? Messages.CartQuantityValidation : MessagesAr.CartQuantityValidation;
                    return response;
                }

                var productStockQuantity = 0;
                if (product.ProductType == ProductType.BaseProduct)
                {
                    productStockQuantity = await _productService.GetAvailableStockQuantity(productId: product.Id,
                    customerId: cartItemModel.CustomerId, customerGuidValue: cartItemModel.CustomerGuidValue);
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
                    customerId: cartItemModel.CustomerId, customerGuidValue: cartItemModel.CustomerGuidValue);
                        if (childProductStockQuantity < (cartItemModel.Quantity * productDetail.Quantity))
                            lowStockProduct++;
                    }

                    if (lowStockProduct > 0)
                        productStockQuantity = 0;
                    else
                        productStockQuantity = cartItemModel.Quantity;
                }

                var existingCart = (await _cartService.GetAllCartItem(productDetailId: product.Id, customerId: cartItemModel.CustomerId, customerGuidValue: cartItemModel.CustomerGuidValue)).FirstOrDefault();
                if (existingCart == null)
                {
                    if (cartItemModel.Id > 0)
                    {
                        var cartItem = await _cartService.GetCartItemById(cartItemModel.Id);
                        if (cartItem != null)
                        {
                            if (cartItemModel.Quantity > productStockQuantity)
                            {
                                response.Message = isEnglish ? string.Format(Messages.ProductIsOutOfStock, productStockQuantity) : string.Format(MessagesAr.ProductIsOutOfStock, productStockQuantity);
                                return response;
                            }

                            cartItem.Quantity = cartItemModel.Quantity;
                            await _cartService.UpdateCartItem(cartItem);
                        }
                    }
                    else
                    {
                        if (cartItemModel.Quantity > productStockQuantity)
                        {
                            response.Message = isEnglish ? string.Format(Messages.ProductIsOutOfStock, productStockQuantity) : string.Format(MessagesAr.ProductIsOutOfStock, productStockQuantity);
                            return response;
                        }

                        var cartItem = _mapper.Map(cartItemModel, new CartItem());
                        cartItem.Quantity = cartItemModel.Quantity;
                        cartItem.ProductId = cartItemModel.ProductId;
                        await _cartService.CreateCartItem(cartItem);
                    }

                    response.Message = isEnglish ? Messages.AddToShoppingCartSuccess : MessagesAr.AddToShoppingCartSuccess;
                }
                else
                {
                    if (cartItemModel.Id > 0 && cartItemModel.Id != existingCart.Id)
                    {
                        int quantity = existingCart.Quantity;
                        await _cartService.DeleteCartItem(existingCart);
                        var cartItem = await _cartService.GetCartItemById(cartItemModel.Id);
                        cartItem = _mapper.Map(cartItemModel, cartItem);
                        cartItem.Quantity = quantity + cartItemModel.Quantity;

                        if (cartItem.Quantity > productStockQuantity)
                        {
                            response.Message = isEnglish ? string.Format(Messages.ProductIsOutOfStock, productStockQuantity) : string.Format(MessagesAr.ProductIsOutOfStock, productStockQuantity);
                            return response;
                        }

                        await _cartService.UpdateCartItem(cartItem);
                    }
                    else
                    {
                        if (cartItemModel.Id > 0)
                            existingCart.Quantity = cartItemModel.Quantity;
                        else
                            existingCart.Quantity = existingCart.Quantity + cartItemModel.Quantity;


                        if (existingCart.Quantity > productStockQuantity)
                        {
                            response.Message = isEnglish ? string.Format(Messages.ProductIsOutOfStock, productStockQuantity) : string.Format(MessagesAr.ProductIsOutOfStock, productStockQuantity);
                            return response;
                        }

                        await _cartService.UpdateCartItem(existingCart);
                    }

                    response.Message = isEnglish ? Messages.UpdateToShoppingCartSuccess : MessagesAr.UpdateToShoppingCartSuccess;
                }

                response.Data = true;
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
        public async Task<APIResponseModel<CartModel>> EditCartItem(bool isEnglish, CartItemModel cartItemModel)
        {
            var response = new APIResponseModel<CartModel>();
            try
            {
                if (cartItemModel.Id <= 0)
                {
                    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    return response;
                }

                if (cartItemModel.Quantity <= 0)
                {
                    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    return response;
                }

                var cartItem = await _cartService.GetCartItemById(cartItemModel.Id);
                if (cartItem == null)
                {
                    response.Message = isEnglish ? Messages.CartNotExists : MessagesAr.CartNotExists;
                    return response;
                }

                cartItemModel.CustomerId = cartItem.CustomerId;
                cartItemModel.CustomerGuidValue = cartItem.CustomerGuidValue;

                var product = await _productService.GetById(cartItem.ProductId);
                if (product == null)
                {
                    response.Message = isEnglish ? Messages.ProductNotExists : MessagesAr.ProductNotExists;
                    return response;
                }

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

                if (product.ProductType == ProductType.SubscriptionProduct)
                {
                    response.Message = isEnglish ? Messages.YouCannotAddThisProductToCart : MessagesAr.YouCannotAddThisProductToCart;
                    return response;
                }

                var productStockQuantity = 0;
                if (product.ProductType == ProductType.BaseProduct)
                {
                    productStockQuantity = await _productService.GetAvailableStockQuantity(productId: product.Id,
                    customerId: cartItemModel.CustomerId, customerGuidValue: cartItemModel.CustomerGuidValue);
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
                    customerId: cartItemModel.CustomerId, customerGuidValue: cartItemModel.CustomerGuidValue);
                        if (childProductStockQuantity < (cartItemModel.Quantity * productDetail.Quantity))
                            lowStockProduct++;
                    }

                    if (lowStockProduct > 0)
                        productStockQuantity = 0;
                    else
                        productStockQuantity = cartItemModel.Quantity;
                }

                if (cartItemModel.Quantity > productStockQuantity)
                {
                    response.Message = isEnglish ? string.Format(Messages.ProductIsOutOfStock, productStockQuantity) : string.Format(MessagesAr.ProductIsOutOfStock, productStockQuantity);
                    return response;
                }

                cartItem.Quantity = cartItemModel.Quantity;

                await _cartService.UpdateCartItem(cartItem);

                var cartItems = await _cartService.GetAllCartItem(customerGuidValue: cartItem.CustomerGuidValue, customerId: cartItem.CustomerId);
                var cartModel = await _modelHelper.PrepareCartModel(cartItems, isEnglish);

                response.Data = cartModel;
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
        public async Task<APIResponseModel<CartModel>> DeleteCartItem(bool isEnglish, int id)
        {
            var response = new APIResponseModel<CartModel>();
            try
            {
                var cartItem = await _cartService.GetCartItemById(id);
                if (cartItem == null)
                {
                    response.Message = isEnglish ? Messages.CartNotExists : MessagesAr.CartNotExists;
                    return response;
                }

                await _cartService.DeleteCartItem(cartItem);

                var cartItems = await _cartService.GetAllCartItem(customerGuidValue: cartItem.CustomerGuidValue, customerId: cartItem.CustomerId);
                if (cartItems.Count == 0)
                {
                    var cartAttribute = (await _cartService.GetAllCartAttribute(customerGuidValue: cartItem.CustomerGuidValue, customerId: cartItem.CustomerId)).FirstOrDefault();
                    if (cartAttribute != null)
                    {
                        await _cartService.DeleteCartAttribute(cartAttribute);
                    }
                }

                var cartModel = await _modelHelper.PrepareCartModel(cartItems, isEnglish);

                response.Data = cartModel;
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
        public async Task<APIResponseModel<bool>> DeleteCartItems(bool isEnglish, int customerId = 0, string customerGuidValue = "")
        {
            var response = new APIResponseModel<bool>();
            try
            {
                if (customerId > 0)
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
                }

                var cartItems = await _cartService.GetAllCartItem(customerGuidValue: customerGuidValue, customerId: customerId);
                await _cartService.DeleteCartItems(cartItems);

                var cartAttribute = (await _cartService.GetAllCartAttribute(customerGuidValue: customerGuidValue, customerId: customerId)).FirstOrDefault();
                if (cartAttribute != null)
                {
                    await _cartService.DeleteCartAttribute(cartAttribute);
                }

                response.Data = true;
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
        //public async Task<APIResponseModel<CartSummaryModel>> PrepareCartSummaryModel(bool isEnglish, int customerId)
        //{
        //    var response = new APIResponseModel<CartSummaryModel>();
        //    try
        //    {
        //        var customer = await _customerService.GetCustomerById(customerId);
        //        if (customer == null || customer.Deleted)
        //        {
        //            response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
        //            return response;
        //        }

        //        if (!customer.Active)
        //        {
        //            response.Message = isEnglish ? Messages.InactiveCustomer : MessagesAr.InactiveCustomer;
        //            return response;
        //        }

        //        var cartItems = await _cartService.GetAllCartItem(customerId: customerId);
        //        if (cartItems.Count == 0)
        //        {
        //            response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
        //            return response;
        //        }

        //        List<CartItemModel> cartItemModels = new();
        //        foreach (var item in cartItems)
        //        {
        //            var cartItemModel = await _modelHelper.PrepareCartItemModel(cartItem: item, isEnglish: isEnglish, customer: customer);
        //            cartItemModels.Add(cartItemModel);
        //        }

        //        var checkOutModel = await _modelHelper.PrepareCartSummaryModel(isEnglish: isEnglish, customerId: customerId, cartItemModels: cartItemModels);

        //        response.Data = checkOutModel;
        //        response.Message = isEnglish ? Messages.Success : MessagesAr.Success;
        //        response.Success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
        //    }

        //    return response;
        //}

        public async Task<APIResponseModel<CartSummaryModel>> PrepareCartSummaryModel(bool isEnglish, int customerId)
        {
            var response = new APIResponseModel<CartSummaryModel>();
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
                    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    return response;
                }

                List<CartItemModel> cartItemModels = new();
                foreach (var item in cartItems)
                {
                    var cartItemModel = await _modelHelper.PrepareCartItemModel(cartItem: item, isEnglish: isEnglish, customer: customer);
                    cartItemModels.Add(cartItemModel);
                }

                var checkOutModel = await _modelHelper.PrepareCartSummaryModel(isEnglish: isEnglish, customerId: customerId, cartItemModels: cartItemModels);

                response.Data = checkOutModel;
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
        public async Task<APIResponseModel<CartSummaryModel>> SaveCartAttribute(bool isEnglish, int customerId, CartAttributeModel cartAttributeModel)
        {
            var response = new APIResponseModel<CartSummaryModel>();
            try
            {
                if (customerId == 0 || cartAttributeModel.AttributeTypeId == 0)
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

                if (cartAttributeModel.AttributeTypeId == AttributeType.SelectAddress)
                {
                    if (!cartAttributeModel.AddressId.HasValue)
                    {
                        response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                        return response;
                    }

                    var address = await _customerService.GetAddressById(cartAttributeModel.AddressId.Value);
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

                if (cartAttributeModel.AttributeTypeId == AttributeType.AddWalletAmount)
                {
                    var walletBalance = await _customerService.GetWalletBalanceByCustomerId(id: customerId, walletTypeId: WalletType.Wallet);
                    if (walletBalance <= 0)
                    {
                        response.Message = isEnglish ? Messages.WalletBalanceLessThanActualAmount : MessagesAr.WalletBalanceLessThanActualAmount;
                        return response;
                    }
                }

                if (cartAttributeModel.AttributeTypeId == AttributeType.ApplyCoupon)
                {
                    if (string.IsNullOrEmpty(cartAttributeModel.CouponCode))
                    {
                        response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                        return response;
                    }
                }

                if (cartAttributeModel.AttributeTypeId == AttributeType.SelectPaymentMethod)
                {
                    if (!cartAttributeModel.PaymentMethodId.HasValue)
                    {
                        response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                        return response;
                    }
                }

                var cartItems = await _cartService.GetAllCartItem(customerId: customerId);
                if (cartItems.Count == 0)
                {
                    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    return response;
                }

                var cartItemModels = new List<CartItemModel>();
                foreach (var cartItem in cartItems)
                {
                    var cartItemModel = await _modelHelper.PrepareCartItemModel(cartItem, isEnglish, customer: customer);
                    cartItemModels.Add(cartItemModel);
                }
                Coupon coupon = null;
                decimal subTotal = cartItemModels.Sum(a => a.Total);
                var cartAttribute = (await _cartService.GetAllCartAttribute(customerId: customerId)).FirstOrDefault();
                if (cartAttribute == null)
                {
                    if (cartAttributeModel.AttributeTypeId == AttributeType.SelectAddress)
                    {
                        cartAttribute = await _cartService.CreateCartAttribute(new CartAttribute
                        {
                            CustomerId = customerId,
                            AddressId = cartAttributeModel.AddressId
                        });
                    }
                    else if (cartAttributeModel.AttributeTypeId == AttributeType.ApplyCoupon)
                    {
                        coupon = await _couponService.GetByCode(cartAttributeModel.CouponCode);
                        if (coupon != null)
                        {
                            cartAttribute = await _cartService.CreateCartAttribute(new CartAttribute
                            {
                                CustomerId = customerId,
                                CouponId = coupon.Id,
                                
                                
                            });
                        }
                    }
                    else if (cartAttributeModel.AttributeTypeId == AttributeType.SelectPaymentMethod)
                    {
                        cartAttribute = await _cartService.CreateCartAttribute(new CartAttribute
                        {
                            CustomerId = customerId,
                            PaymentMethodId = cartAttributeModel.PaymentMethodId
                        });
                    }
                }
                else
                {
                    if (cartAttributeModel.AttributeTypeId == AttributeType.SelectAddress)
                    {
                        cartAttribute.AddressId = cartAttributeModel.AddressId;
                        await _cartService.UpdateCartAttribute(cartAttribute);
                    }
                    else if (cartAttributeModel.AttributeTypeId == AttributeType.AddWalletAmount)
                    {
                        cartAttribute.UseWalletAmount = true;
                        await _cartService.UpdateCartAttribute(cartAttribute);
                    }
                    else if (cartAttributeModel.AttributeTypeId == AttributeType.RemoveWalletAmount)
                    {
                        cartAttribute.UseWalletAmount = false;
                        await _cartService.UpdateCartAttribute(cartAttribute);
                    }
                    else if (cartAttributeModel.AttributeTypeId == AttributeType.ApplyCoupon)
                    {
                         coupon = await _couponService.GetByCode(cartAttributeModel.CouponCode);
                        if (coupon == null)
                        {
                            response.Message = isEnglish ? Messages.CouponNotExists : MessagesAr.CouponNotExists;
                            return response;
                        }

                        var validationMessage = _commonHelper.CouponValidation(coupon: coupon, isEnglish: isEnglish, total: subTotal);
                        if (!string.IsNullOrEmpty(validationMessage))
                        {
                            response.Message = validationMessage;
                            return response;
                        }

                        cartAttribute.CouponId = coupon.Id;
                        await _cartService.UpdateCartAttribute(cartAttribute);
                    }
                    else if (cartAttributeModel.AttributeTypeId == AttributeType.RemoveCoupon)
                    {
                        cartAttribute.CouponId = null;
                        await _cartService.UpdateCartAttribute(cartAttribute);
                    }
                    else if (cartAttributeModel.AttributeTypeId == AttributeType.SelectPaymentMethod)
                    {
                        cartAttribute.PaymentMethodId = cartAttributeModel.PaymentMethodId;
                        await _cartService.UpdateCartAttribute(cartAttribute);
                    }
                }

                var cartSummaryModel = await _modelHelper.PrepareCartSummaryModel(isEnglish: isEnglish, cartItemModels: cartItemModels,
                    customerId: customerId);

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
        public async Task<APIResponseModel<CheckOutModel>> PrepareCheckOutModel(bool isEnglish, int customerId)
        {
            var response = new APIResponseModel<CheckOutModel>();
            try
            {
                if (customerId > 0)
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
                }

                var checkOutModel = await _modelHelper.PrepareCheckOutModel(isEnglish: isEnglish, customerId: customerId);

                response.Data = checkOutModel;
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


    }
}
