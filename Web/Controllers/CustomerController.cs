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
using Utility.Models.Frontend.CouponPromotion;
using Utility.Models.Frontend.CustomerManagement;
using Utility.ResponseMapper;

namespace Web.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly IAPIHelper _apiHelper;
        private readonly ILogger _logger;
        private readonly AppSettingsModel _appSettingsModel;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CustomerController(IAPIHelper apiHelper,
            ILoggerFactory logger,
            IOptions<AppSettingsModel> options,
             IRazorViewEngine razorViewEngine,
             IHttpContextAccessor httpContextAccessor) : base(razorViewEngine)
        {
            _apiHelper = apiHelper;
            _logger = logger.CreateLogger(typeof(OrderController).Name);
            _appSettingsModel = options.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Login by email
        /// </summary>
        public virtual IActionResult Login(string returnUrl = "")
        {
            if (returnUrl == "/register")
            {
                returnUrl = string.Empty;
            }

            CustomerModel customerModel = new();
            customerModel.ReturnUrl = returnUrl;
            return View(customerModel);
        }

        /// <summary>
        /// Login by mobile
        /// </summary>
        [HttpPost]
        public virtual async Task<JsonResult> Login(CustomerModel customerModel)
        {
            APIResponseModel<CustomerModel> response = new();
            try
            {
                if (ModelState.IsValid)
                {
                    response = await _apiHelper.PostAsync<APIResponseModel<CustomerModel>>("webapi/customer/login", customerModel);
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    if (errors.Count() > 0)
                    {
                        response.Message = errors.FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            //CustomerRegisterModel
            return Json(response);
        }

        /// <summary>
        /// Register
        /// </summary>
        public virtual IActionResult Register()
        {
            var customerModel = new CustomerModel();
            return View(customerModel);
        }

        /// <summary>
        /// Register with name+mobile+email
        /// </summary>
        [HttpPost]
        public virtual async Task<JsonResult> Register(CustomerModel customerModel)
        {
            APIResponseModel<CustomerModel> response = new();
            try
            {
                if (ModelState.IsValid)
                {
                    response = await _apiHelper.PostAsync<APIResponseModel<CustomerModel>>("webapi/customer/register", customerModel);
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    if (errors.Count() > 0)
                    {
                        response.Message = errors.FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Json(response);
        }

        /// <summary>
        /// Resend otp
        /// </summary>
        [HttpGet]
        public virtual async Task<JsonResult> ResendOTP(int otpDetailId)
        {
            APIResponseModel<CustomerModel> response = new();
            try
            {
                if (ModelState.IsValid)
                {
                    response = await _apiHelper.GetAsync<APIResponseModel<CustomerModel>>("webapi/customer/resendotp?otpDetailId=" + otpDetailId);
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    if (errors.Count() > 0)
                    {
                        response.Message = errors.FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Json(response);
        }

        /// <summary>
        /// Verify OTP
        /// </summary>
        [HttpPost]
        public virtual async Task<JsonResult> VerifyOTP(VerifyOTPModel verifyOTPModel)
        {
            APIResponseModel<CustomerModel> response = new();
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

                    verifyOTPModel.CustomerGuidValue = customerGuidValue;

                    CustomerModel customerModel = new();
                    customerModel.OTPDetailId = verifyOTPModel.RequestId;
                    customerModel.OTP = verifyOTPModel.OTP;
                    customerModel.CustomerGuidValue = customerGuidValue;

                    response = await _apiHelper.PostAsync<APIResponseModel<CustomerModel>>("webapi/customer/verifyotp", customerModel);
                    if (response.Data != null && response.Success)
                    {
                        Response.Cookies.Append("AuthenticationToken", response.Data.Token, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

                        if (!string.IsNullOrEmpty(customerGuidValue))
                        {
                            Response.Cookies.Append("CustomerGuidValue", customerGuidValue, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
                        }
                    }
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    if (errors.Count() > 0)
                    {
                        response.Message = errors.FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Json(response);
        }

        /// <summary>
        /// Register
        /// </summary>
        public virtual async Task<IActionResult> MyProfile()
        {
            CustomerModel customerModel = new();
            try
            {
                var authenticationToken = Convert.ToString(Request.Cookies["AuthenticationToken"]);
                if (string.IsNullOrEmpty(authenticationToken))
                {
                    return RedirectToRoute("login");
                }

                var responseModel = await _apiHelper.GetAsync<APIResponseModel<CustomerModel>>("webapi/customer/getcustomer");
                if (responseModel.Success && responseModel.Data != null)
                {
                    customerModel = responseModel.Data;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return View(customerModel);
        }

        /// <summary>
        /// My profile
        /// </summary>
        [HttpPost]
        public virtual async Task<JsonResult> MyProfile(CustomerModel customerModel)
        {
            APIResponseModel<CustomerModel> response = new();
            try
            {
                var authenticationToken = Convert.ToString(Request.Cookies["AuthenticationToken"]);
                if (string.IsNullOrEmpty(authenticationToken))
                {
                    response.StatusCode = 401;
                    return Json(response);
                }

                response = await _apiHelper.PutAsync<APIResponseModel<CustomerModel>>("webapi/customer/editprofile", customerModel);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Json(response);
        }

        /// <summary>
        /// Change password
        /// </summary>
        [HttpGet]
        public virtual IActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// Change password
        /// </summary>
        [HttpPost]
        public virtual async Task<JsonResult> ChangePassword(string oldPassword, string newPassword)
        {
            APIResponseModel<CustomerModel> response = new();
            try
            {
                if (ModelState.IsValid)
                {

                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    if (errors.Count() > 0)
                    {
                        response.Message = errors.FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                _logger.LogError(ex.Message);
            }

            return Json(response);
        }

        /// <summary>
        /// forgot password
        /// </summary>
        [HttpGet]
        public virtual IActionResult ForgotPassword()
        {
            return View();
        }

        /// <summary>
        /// request password
        /// </summary>
        [HttpPost]
        public virtual async Task<JsonResult> ForgotPassword(string emailAddress)
        {
            APIResponseModel<CustomerModel> response = new();
            try
            {
                response = await _apiHelper.GetAsync<APIResponseModel<CustomerModel>>("webapi/customer/forgotpassword?emailAddress=" + emailAddress);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                _logger.LogInformation(ex.Message);
            }

            return Json(response);
        }

        /// <summary>
        /// Change password
        /// </summary>
        [HttpGet]
        public virtual IActionResult ChangePasswordByEmail(string code)
        {
            ViewBag.Code = code;
            return View();
        }

        /// <summary>
        /// Change password
        /// </summary>
        [HttpPost]
        public virtual async Task<JsonResult> ChangePasswordByEmail(string code, string password)
        {
            APIResponseModel<CustomerModel> response = new();
            try
            {
                if (ModelState.IsValid)
                {
                    response = await _apiHelper.GetAsync<APIResponseModel<CustomerModel>>("webapi/customer/changepasswordbyemail?code=" + code + "&password=" + password);
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    if (errors.Count() > 0)
                    {
                        response.Message = errors.FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                _logger.LogError(ex.Message);
            }

            return Json(response);
        }

        public IActionResult MyNotifications()
        {
            var authenticationToken = Convert.ToString(Request.Cookies["AuthenticationToken"]);
            if (string.IsNullOrEmpty(authenticationToken))
            {
                return RedirectToRoute("login");
            }

            var notificationModels = new List<NotificationModel>();
            return View(notificationModels);
        }

        /// <summary>
        /// Get notifications by ajax
        /// </summary>
        public async Task<JsonResult> NotificationsByAjax(int? limit = null, int? page = null)
        {
            var responseModel = new APIResponseModel<List<NotificationModel>>();
            try
            {
                responseModel = await _apiHelper.GetAsync<APIResponseModel<List<NotificationModel>>>("webapi/customer/getnotification?limit=" + limit + "&page=" + page);
                if (responseModel.Success && responseModel.Data != null)
                {
                    return Json(new
                    {
                        html = await RenderPartialViewToStringAsync("_Notifications", responseModel.Data),
                        TotalNotificationCount = responseModel.DataRecordCount,
                        NotificationCount = responseModel.Data.Count,
                        Success = true,
                        MessageCode = 0
                    });
                }
            }
            catch (Exception ex)
            {
                responseModel.Message = ex.Message;
            }

            var notificationModels = new List<NotificationModel>();
            return Json(new
            {
                html = await RenderPartialViewToStringAsync("_Notifications", notificationModels),
                TotalNotificationCount = 0,
                NotificationCount = 0,
                Success = true,
                MessageCode = 0
            });
        }

        public async Task<IActionResult> Addresses()
        {
            List<AddressModel> addressModels = new();
            try
            {
                var authenticationToken = Convert.ToString(Request.Cookies["AuthenticationToken"]);
                if (string.IsNullOrEmpty(authenticationToken))
                {
                    return RedirectToRoute("login");
                }

                var responseModel = await _apiHelper.GetAsync<APIResponseModel<List<AddressModel>>>("webapi/customer/getaddress");
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

        [HttpPost]
        public async Task<JsonResult> AddAddress(AddressModel addressModel)
        {
            var response = new APIResponseModel<AddressModel>();
            try
            {
                var authenticationToken = Convert.ToString(Request.Cookies["AuthenticationToken"]);
                if (string.IsNullOrEmpty(authenticationToken))
                {
                    response.StatusCode = 401;
                    return Json(response);
                }

                response = await _apiHelper.PostAsync<APIResponseModel<AddressModel>>("webapi/customer/addaddresswithselect", addressModel);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return Json(response);
        }

        public IActionResult AddNewAddress()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return View();
        }

        public async Task<IActionResult> AddressDetails(int addressId)
        {
            try
            {
                var responseModel = await _apiHelper.GetAsync<APIResponseModel<List<AddressModel>>>("webapi/customer/getaddress?id=" + addressId);
                if (responseModel.Success && responseModel.Data != null && responseModel.Data.Count > 0)
                {
                    return View("AddressDetails", responseModel.Data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return View(new List<AddressModel>());
        }

        [HttpPut]
        public async Task<JsonResult> UpdateAddress(AddressModel addressModel)
        {
            var response = new APIResponseModel<AddressModel>();

            try
            {
                var authenticationToken = Convert.ToString(Request.Cookies["AuthenticationToken"]);
                if (string.IsNullOrEmpty(authenticationToken))
                {
                    response.StatusCode = 401;
                    return Json(response);
                }

                response = await _apiHelper.PutAsync<APIResponseModel<AddressModel>>("webapi/customer/updateaddress", addressModel);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return Json(response);
        }

        public async Task<IActionResult> WalletTransactions(int walletType)
        {
            WalletModel walletModel = new();
            try
            {
                var authenticationToken = Convert.ToString(Request.Cookies["AuthenticationToken"]);
                if (string.IsNullOrEmpty(authenticationToken))
                {
                    return RedirectToRoute("login");
                }

                ViewBag.WalletType = walletType;
                var responseModel = await _apiHelper.GetAsync<APIResponseModel<WalletModel>>("webapi/customer/getwallettransactions?walletType=" + walletType);
                if (responseModel.Success && responseModel.Data != null)
                {
                    walletModel = responseModel.Data;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return View(walletModel);
        }

        public async Task<IActionResult> WalletPackages()
        {
            List<WalletPackageModel> walletPackageModels = new();
            try
            {
                var authenticationToken = Convert.ToString(Request.Cookies["AuthenticationToken"]);
                if (string.IsNullOrEmpty(authenticationToken))
                {
                    return RedirectToRoute("login");
                }

                var responseModel = await _apiHelper.GetAsync<APIResponseModel<List<WalletPackageModel>>>("webapi/common/walletpackages");
                if (responseModel.Success && responseModel.Data != null && responseModel.Data.Count > 0)
                {
                    walletPackageModels = responseModel.Data;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return View(walletPackageModels);
        }

        /// <summary>
        /// Logout clear authentication token+customerId+CustomerGuidValue and redirect to home 
        /// </summary>
        public virtual IActionResult Logout()
        {
            Response.Cookies.Append("AuthenticationToken", "");
            Response.Cookies.Append("CustomerId", "");
            Response.Cookies.Append("CustomerGuidValue", "");

            return RedirectToRoute("home");
        }
    }
}
