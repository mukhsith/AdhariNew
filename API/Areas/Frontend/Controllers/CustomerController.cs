using API.Areas.Frontend.Factories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;
using Utility.Models.Frontend.CustomerManagement;
using Utility.Models.Frontend.CustomizedModel;
using Utility.Models.Frontend.Sales;
using Utility.ResponseMapper;

namespace API.Areas.Frontend.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ICustomerModelFactory _customerModelFactory;
        public CustomerController(IOptions<AppSettingsModel> options,
            ICustomerModelFactory customerModelFactory) : base(options)
        {
            _customerModelFactory = customerModelFactory;
        }

        /// <summary>
        /// Login by customer mobile number
        /// </summary>
        [HttpPost, Route("/webapi/customer/login")]
        [AllowAnonymous]
        public async Task<APIResponseModel<CustomerModel>> Login([FromBody] CustomerModel customerModel)
        {
            return await _customerModelFactory.Login(isEnglish: isEnglish, customerModel: customerModel);
        }

        /// <summary>
        /// Register with verification
        /// </summary>
        [HttpPost, Route("/webapi/customer/register")]
        [AllowAnonymous]
        public async Task<APIResponseModel<CustomerModel>> Register([FromBody] CustomerRegisterModel customerModel)
        {
            return await _customerModelFactory.Register(isEnglish: isEnglish, customerModel: customerModel);
        }

        /// <summary>
        /// Resend OTP
        /// </summary>
        [HttpGet, Route("/webapi/customer/resendotp")]
        [AllowAnonymous]
        public async Task<APIResponseModel<CustomerModel>> ResendOTP(int otpDetailId)
        {
            return await _customerModelFactory.ResendOTP(isEnglish: isEnglish, otpDetailId: otpDetailId);
        }

        /// <summary>
        /// Verify otp
        /// </summary>
        [HttpPost, Route("/webapi/customer/verifyotp")]
        [AllowAnonymous]
        public async Task<APIResponseModel<CustomerModel>> VerifyOTP(CustomerModel customerModel)
        {
            return await _customerModelFactory.VerifyOTP(isEnglish: isEnglish, otpDetailId: customerModel.OTPDetailId, otp: customerModel.OTP,
                         customerGuidValue: customerModel.CustomerGuidValue, deviceId: customerModel.DeviceId, deviceToken: customerModel.DeviceToken);
        }

        /// <summary>
        /// To get customer data
        /// </summary>
        [HttpGet, Route("/webapi/customer/getcustomer")]
        [Authorize]
        public async Task<APIResponseModel<CustomerModel>> GetCustomer()
        {
            return await _customerModelFactory.PrepareCustomer(isEnglish: isEnglish, id: LoggedInCustomerId);
        }

        /// <summary>
        /// To delete account
        /// </summary>
        [HttpDelete, Route("/webapi/customer/deletecustomer")]
        [Authorize]
        public async Task<APIResponseModel<CustomerModel>> DeleteCustomer()
        {
            return await _customerModelFactory.DeleteCustomer(isEnglish: isEnglish, id: LoggedInCustomerId);
        }

        /// <summary>
        /// To edit profile
        /// </summary>
        [HttpPut, Route("/webapi/customer/editprofile")]
        [Authorize]
        public async Task<APIResponseModel<CustomerModel>> EditProfile([FromBody] CustomerModel customerModel)
        {
            customerModel.Id = LoggedInCustomerId;
            return await _customerModelFactory.UpdateCustomer(isEnglish: isEnglish, customerModel: customerModel);
        }

        /// <summary>
        /// To get all address
        /// </summary>
        /// <param name="id">Address identifier</param>
        /// <returns></returns>
        [HttpGet, Route("/webapi/customer/getaddress")]
        [Authorize]
        public async Task<APIResponseModel<List<AddressModel>>> GetAddress(int id, RelatedEntityType? typeId = null)
        {
            return await _customerModelFactory.GetAddress(isEnglish: isEnglish, customerId: LoggedInCustomerId, id: id, relatedEntityType: typeId);
        }

        /// <summary>
        /// To add address
        /// </summary>
        /// <param name="addressDto">Address dto</param>
        /// <returns></returns>
        [HttpPost, Route("/webapi/customer/addaddress")]
        [Authorize]
        public async Task<APIResponseModel<object>> AddAddress([FromBody] AddressModel addressModel)
        {
            addressModel.CustomerId = LoggedInCustomerId;
            return await _customerModelFactory.AddAddress(isEnglish: isEnglish, addressModel: addressModel);
        }

        /// <summary>
        /// To add address with select
        /// </summary>
        /// <param name="addressDto">Address dto</param>
        /// <returns></returns>
        [HttpPost, Route("/webapi/customer/addaddresswithselect")]
        [Authorize]
        public async Task<APIResponseModel<AddressModel>> AddAddressWithSelect([FromBody] AddressModel addressModel)
        {
            addressModel.CustomerId = LoggedInCustomerId;
            return await _customerModelFactory.AddAddressWithSelect(isEnglish: isEnglish, addressModel: addressModel);
        }

        /// <summary>
        /// To update address
        /// </summary>
        /// <param name="addressDto">Address dto</param>
        /// <returns></returns>
        [HttpPut, Route("/webapi/customer/updateaddress")]
        [Authorize]
        public async Task<APIResponseModel<object>> UpdateAddress([FromBody] AddressModel addressModel)
        {
            addressModel.CustomerId = LoggedInCustomerId;
            return await _customerModelFactory.UpdateAddress(isEnglish: isEnglish, addressModel: addressModel);
        }

        /// <summary>
        /// To delete address
        /// </summary>
        /// <param name="id">Address identifier</param>
        /// <returns></returns>
        [HttpDelete, Route("/webapi/customer/deleteaddress")]
        [Authorize]
        public async Task<APIResponseModel<object>> DeleteAddress(int id)
        {
            return await _customerModelFactory.DeleteAddress(isEnglish: isEnglish, id: id, customerId: LoggedInCustomerId);
        }

        /// <summary>
        /// To get all notifications
        /// </summary>
        /// <param name="id">Address identifier</param>
        /// <returns></returns>
        [HttpGet, Route("/webapi/customer/getnotification")]
        [Authorize]
        public async Task<APIResponseModel<List<NotificationModel>>> GetNotification(int limit = 0, int page = 0)
        {
            return await _customerModelFactory.GetNotification(isEnglish: isEnglish, customerId: LoggedInCustomerId, limit: limit, page: page);
        }

        /// <summary>
        /// To get all wallet transactions
        /// </summary>
        /// <param name="id">Wallet transaction identifier</param>
        /// <returns></returns>
        [HttpGet, Route("/webapi/customer/getwallettransactions")]
        [Authorize]
        public async Task<APIResponseModel<WalletModel>> GetWalletTransactions(WalletType? walletType = null)
        {
            return await _customerModelFactory.GetWalletTransactions(isEnglish: isEnglish, customerId: LoggedInCustomerId, walletType);
        }

        /// <summary>
        /// To switch the selected language
        /// </summary>
        /// <param name="language">language</param>
        /// <returns></returns>
        [HttpGet, Route("/webapi/customer/switchlanguage")]
        public async Task<APIResponseModel<object>> SwitchLanguage(string language, string deviceId)
        {
            return await _customerModelFactory.SwitchLanguage(isEnglish: isEnglish, language: language, deviceId: deviceId, customerId: LoggedInCustomerId);
        }

        /// <summary>
        /// Create device token
        /// </summary>
        /// <param name="deviceTokenDto">Device token dto</param>
        /// <returns></returns>
        [HttpPost, Route("/webapi/customer/createdevicetoken")]
        [AllowAnonymous]
        public async Task<APIResponseModel<object>> CreateDeviceToken([FromBody] DeviceTokenModel deviceTokenModel)
        {
            deviceTokenModel.CustomerId = LoggedInCustomerId;
            return await _customerModelFactory.CreateDeviceToken(isEnglish: isEnglish, deviceTokenModel: deviceTokenModel);
        }

        /// <summary>
        /// To refresh the access token
        /// </summary>
        /// <param name="refreshTokenRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("/webapi/customer/refreshtoken")]
        public async Task<APIResponseModel<RefreshTokenModel>> RefreshToken(RefreshTokenModel refreshTokenModel)
        {
            return await _customerModelFactory.RefreshToken(isEnglish: isEnglish, refreshTokenModel: refreshTokenModel);
        }

        /// <summary>
        /// Create wallet package order
        /// </summary>
        [HttpPost, Route("/webapi/customer/createwalletpackageorder")]
        public async Task<APIResponseModel<CreatePaymentModel>> CreateWalletPackageOrder([FromBody] CreatePaymentModel createPaymentModel)
        {
            return await _customerModelFactory.CreateWalletPackageOrder(isEnglish: isEnglish, customerId: LoggedInCustomerId, deviceTypeId: HeaderDeviceTypeId, createPaymentModel: createPaymentModel);
        }

        /// <summary>
        /// To get wallet package orders
        /// </summary>
        /// <returns>Orders</returns>
        [HttpGet, Route("/webapi/customer/walletpackageorders")]
        public async Task<APIResponseModel<List<WalletPackageOrderModel>>> GetWalletPackageOrders(int id = 0, string orderNumber = "", int limit = 0, int page = 0)
        {
            return await _customerModelFactory.GetWalletPackageOrders(isEnglish: isEnglish, customerId: LoggedInCustomerId, id: id, orderNumber: orderNumber,
                limit: limit, page: page);
        }
    }
}
