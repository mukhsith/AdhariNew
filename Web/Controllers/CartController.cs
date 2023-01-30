using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;
using Utility.Models.Frontend.Content;
using Utility.Models.Frontend.CustomerManagement;
using Utility.Models.Frontend.CustomizedModel;
using Utility.Models.Frontend.Shop;
using Utility.ResponseMapper;

namespace Web.Controllers
{
    public class CartController : BaseController
    {
        private readonly IAPIHelper _apiHelper;
        private readonly ILogger _logger;
        private readonly AppSettingsModel _appSettingsModel;
        public CartController(IAPIHelper apiHelper,
            ILoggerFactory logger,
            IOptions<AppSettingsModel> options,
             IRazorViewEngine razorViewEngine) : base(razorViewEngine)
        {
            _apiHelper = apiHelper;
            _logger = logger.CreateLogger(typeof(CartController).Name);
            _appSettingsModel = options.Value;
        }

        /// <summary>
        /// Get cart count
        /// </summary>
        public virtual async Task<JsonResult> GetCartCount()
        {
            string countDetails = string.Empty;
            try
            {
                var customerGuidValue = Convert.ToString(Request.Cookies["CustomerGuidValue"]);
                if (string.IsNullOrEmpty(customerGuidValue))
                {
                    customerGuidValue = Guid.NewGuid().ToString();
                    Response.Cookies.Append("CustomerGuidValue", customerGuidValue, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
                }

                var responseModel = await _apiHelper.GetAsync<APIResponseModel<object>>("webapi/cart/getcartitemcount?customerGuidValue=" + customerGuidValue);
                if (responseModel.Success)
                {
                    countDetails = responseModel.Data.ToString();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return Json(countDetails);
        }

        /// <summary>
        /// Get cart items
        /// </summary>
        public virtual async Task<JsonResult> CartItems()
        {
            string cartContents = string.Empty;
            try
            {
                var customerGuidValue = Convert.ToString(Request.Cookies["CustomerGuidValue"]);
                if (string.IsNullOrEmpty(customerGuidValue))
                {
                    customerGuidValue = Guid.NewGuid().ToString();
                    Response.Cookies.Append("CustomerGuidValue", customerGuidValue, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
                }

                var responseModel = await _apiHelper.GetAsync<APIResponseModel<CartModel>>("webapi/cart/getcartitem?customerGuidValue=" + customerGuidValue);
                if (responseModel.Success && responseModel.Data != null)
                {
                    cartContents = await RenderPartialViewToStringAsync("_SideCart", responseModel.Data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return Json(new
            {
                CartContents = cartContents
            });
        }

        /// <summary>
        /// Add item to cart
        /// </summary>
        [HttpPost]
        public virtual async Task<JsonResult> AddCartItem(CartItemModel cartItemModel)
        {
            APIResponseModel<bool> responseModel = new();
            try
            {
                if (ModelState.IsValid)
                {
                    var customerGuidValue = Convert.ToString(Request.Cookies["CustomerGuidValue"]);
                    if (string.IsNullOrEmpty(customerGuidValue))
                    {
                        customerGuidValue = Guid.NewGuid().ToString();
                        Response.Cookies.Append("CustomerGuidValue", customerGuidValue, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
                    }

                    cartItemModel.CustomerGuidValue = customerGuidValue;
                    responseModel = await _apiHelper.PostAsync<APIResponseModel<bool>>("webapi/cart/addcartitem", cartItemModel);
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    if (errors.Count() > 0)
                    {
                        responseModel.Message = errors.FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return Json(responseModel);
        }

        /// <summary>
        /// edit item in cart
        /// </summary>
        [HttpPost]
        public virtual async Task<JsonResult> EditCartItem(CartItemModel cartItemModel)
        {
            string cartContents = string.Empty;
            bool success = false;
            bool data = false;
            string message = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    var customerGuidValue = Convert.ToString(Request.Cookies["CustomerGuidValue"]);
                    if (string.IsNullOrEmpty(customerGuidValue))
                    {
                        customerGuidValue = Guid.NewGuid().ToString();
                        Response.Cookies.Append("CustomerGuidValue", customerGuidValue, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
                    }

                    cartItemModel.CustomerGuidValue = customerGuidValue;
                    var responseModel = await _apiHelper.PostAsync<APIResponseModel<CartModel>>("webapi/cart/editcartitem", cartItemModel);
                    message = responseModel.Message;
                    if (responseModel.Success && responseModel.Data != null)
                    {
                        data = true;
                        success = true;
                        cartContents = await RenderPartialViewToStringAsync("_SideCart", responseModel.Data);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return Json(new
            {
                Data = data,
                Success = success,
                CartContents = cartContents,
                Message = message
            });
        }

        /// <summary>
        /// delete cart item
        /// </summary>
        [HttpGet]
        public virtual async Task<JsonResult> DeleteCartItem(int id)
        {
            var responseModel = new APIResponseModel<CartModel>();
            try
            {
                if (id > 0)
                {
                    responseModel = await _apiHelper.DeleteAsync<APIResponseModel<CartModel>>("webapi/cart/deletecartitem?id=" + id);
                    if (responseModel.Success && responseModel.Data != null)
                    {
                        return Json(new
                        {
                            CartContents = await RenderPartialViewToStringAsync("_SideCart", responseModel.Data),
                            Success = responseModel.Success,
                            Message = responseModel.Message
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return Json(new
            {
                CartContents = "",
                Success = responseModel.Success,
                Message = responseModel.Message
            });
        }

        /// <summary>
        /// clear cart
        /// </summary>
        [HttpGet]
        public virtual async Task<JsonResult> DeleteCartItems()
        {
            var responseModel = new APIResponseModel<bool>();
            try
            {
                var customerGuidValue = Convert.ToString(Request.Cookies["CustomerGuidValue"]);
                if (string.IsNullOrEmpty(customerGuidValue))
                {
                    customerGuidValue = Guid.NewGuid().ToString();
                    Response.Cookies.Append("CustomerGuidValue", customerGuidValue, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
                }

                responseModel = await _apiHelper.DeleteAsync<APIResponseModel<bool>>("webapi/cart/deletecartitems?customerGuidValue=" + customerGuidValue);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return Json(responseModel);
        }

        /// <summary>
        /// Get checkout
        /// </summary>
        public virtual IActionResult Checkout()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToRoute("checkoutaddress");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return View();
        }

        /// <summary>
        /// Get checkout
        /// </summary>
        [Authorize]
        public virtual async Task<IActionResult> CheckoutAddress()
        {
            List<AddressModel> addressModels = new();
            try
            {
                var responseModel = await _apiHelper.GetAsync<APIResponseModel<List<AddressModel>>>("webapi/customer/getaddress?typeId=" + RelatedEntityType.Order);
                if (responseModel.MessageCode == 401)
                {
                    return RedirectToRoute("checkout");
                }

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

        /// <summary>
        /// Get cart sumary for payment
        /// </summary>
        public virtual async Task<JsonResult> CartSummary(bool loadCoupon = false)
        {
            try
            {
                var responseModel = await _apiHelper.GetAsync<APIResponseModel<CheckOutModel>>("webapi/cart/getcartsummary");
                if (responseModel.Success && responseModel.Data != null)
                {
                    return Json(new
                    {
                        CartSummary = await RenderPartialViewToStringAsync("_CartSummary", responseModel.Data),
                        Success = responseModel.Success,
                        Message = responseModel.Message
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return Json(new
            {
                CartSummary = "",
                Success = false,
                Message = ""
            });
        }

        /// <summary>
        /// Save cart attributes
        /// </summary>
        [HttpPost]
        public virtual async Task<JsonResult> SaveCartAttributes(CartAttributeModel cartAttributeModel)
        {
            var responseModel = new APIResponseModel<CartSummaryModel>();
            try
            {
                responseModel = await _apiHelper.PostAsync<APIResponseModel<CartSummaryModel>>("webapi/cart/savecartattributes", cartAttributeModel);
                if (responseModel.Success && responseModel.Data != null)
                {
                    return Json(new
                    {
                        Success = true,
                        Message = responseModel.Message,
                        FormattedCartSummary = await RenderPartialViewToStringAsync("_CartSummary", responseModel.Data),
                        CartSummary = responseModel.Data
                    });
                }
            }
            catch (Exception ex)
            {
                responseModel.Message = ex.Message;
                _logger.LogInformation(ex.Message);
            }

            return Json(new
            {
                Success = false,
                Message = responseModel.Message,
                FormattedCartSummary = string.Empty,
                CartSummary = new object()
            });
        }

        [Authorize]
        public virtual async Task<IActionResult> CheckoutSummary(int addressId)
        {
            try
            {
                var responseModel = await _apiHelper.GetAsync<APIResponseModel<CheckOutModel>>("webapi/cart/getcheckoutsummary");
                if (responseModel.MessageCode == 401)
                {
                    return RedirectToRoute("checkout");
                }

                if (responseModel.Success && responseModel.Data != null)
                {
                    var checkOutModel = responseModel.Data;

                    var responsePaymentModel = await _apiHelper.GetAsync<APIResponseModel<List<PaymentMethodModel>>>("webapi/common/paymentmethods?typeId=" + PaymentRequestType.Order);
                    if (responsePaymentModel.Success && responsePaymentModel.Data != null && responsePaymentModel.Data.Count > 0)
                    {
                        checkOutModel.PaymentMethods = responsePaymentModel.Data;
                    }

                    return View(checkOutModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return RedirectToRoute("home");
        }
    }
}
