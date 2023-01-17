using API.Areas.Frontend.Helpers;
using API.Helpers;
using AutoMapper;
using Data.CustomerManagement;
using Data.PushNotification;
using Data.Sales;
using Data.SMS;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Frontend.Content;
using Services.Frontend.CouponPromotion;
using Services.Frontend.CustomerManagement;
using Services.Frontend.PushNotification;
using Services.Frontend.Shop;
using Services.Frontend.SMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using Utility.API;
using Utility.Enum;
using Utility.Helpers;
using Utility.Models.Frontend.CustomerManagement;
using Utility.Models.Frontend.CustomizedModel;
using Utility.Models.Frontend.Sales;
using Utility.Models.KNET;
using Utility.ResponseMapper;

namespace API.Areas.Frontend.Factories
{
    public class CustomerModelFactory : ICustomerModelFactory
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly AppSettingsModel _appSettings;
        private readonly IModelHelper _modelHelper;
        private readonly ICustomerService _customerService;
        private readonly INotificationService _notificationService;
        private readonly ICommonHelper _commonHelper;
        private readonly INotificationTemplateService _notificationTemplateService;
        private readonly ICartService _cartService;
        private readonly IPromotionService _promotionService;
        private readonly IWalletPackageService _walletPackageService;
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly IAPIHelper _apiHelper;
        public CustomerModelFactory(ILoggerFactory logger,
            IMapper mapper,
            IOptions<AppSettingsModel> options,
            IModelHelper modelHelper,
            ICustomerService customerService,
            INotificationService notificationService,
            ICommonHelper commonHelper,
            INotificationTemplateService notificationTemplateService,
            ICartService cartService,
            IPromotionService promotionService,
            IWalletPackageService walletPackageService,
            IPaymentMethodService paymentMethodService,
            IAPIHelper apiHelper)
        {
            _logger = logger.CreateLogger(typeof(CustomerModelFactory).Name);
            _mapper = mapper;
            _appSettings = options.Value;
            _modelHelper = modelHelper;
            _customerService = customerService;
            _notificationService = notificationService;
            _commonHelper = commonHelper;
            _notificationTemplateService = notificationTemplateService;
            _cartService = cartService;
            _promotionService = promotionService;
            _walletPackageService = walletPackageService;
            _paymentMethodService = paymentMethodService;
            _apiHelper = apiHelper;
        }

        /// <summary>
        /// success=true
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="customerModel"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<CustomerModel>> Login(bool isEnglish, CustomerModel customerModel)
        {
            var response = new APIResponseModel<CustomerModel>();
            try
            {
                var customer = await _customerService.GetCustomerByMobileNumber(customerModel.MobileNumber);
                if (customer == null)
                {
                    response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
                    return response;
                }

                if (!customer.Active)
                {
                    response.Message = isEnglish ? Messages.InactiveCustomer : MessagesAr.InactiveCustomer;
                    return response;
                }

                string otpMessage = string.Empty;
                var notificationTemplate = await _notificationTemplateService.GetNotificationTemplateByTypeId(NotificationType.Login);
                if (notificationTemplate == null)
                {
                    otpMessage = isEnglish ? Messages.LoginOTPMessage : MessagesAr.LoginOTPMessage;
                }
                else
                {
                    otpMessage = isEnglish ? notificationTemplate.SMSMessageEn : notificationTemplate.SMSMessageAr;
                }

                int otpValidMins = _appSettings.OTPValidMinutes;
                DateTime validTo = DateTime.Now.AddMinutes(otpValidMins);
                string otp = Common.GenerateRandomNo(1000, 9999);

                if (!string.IsNullOrEmpty(_appSettings.DefaultOTPMobileNumbers))
                {
                    var defaultOTPMobileNumbers = _appSettings.DefaultOTPMobileNumbers.Split(',').ToList();
                    var defaultOTPMobileNumber = defaultOTPMobileNumbers.Where(a => a == customer.MobileNumber).FirstOrDefault();
                    if (defaultOTPMobileNumber != null)
                        otp = _appSettings.DefaultOTPValue;
                }

                var otpDetail = await _notificationTemplateService.CreateOTPDetail(new OTPDetail
                {
                    OTP = otp,
                    Destination = customer.MobileNumber,
                    OTPValidFrom = DateTime.Now,
                    OTPValidTo = validTo,
                    Type = NotificationType.Login,
                    CustomerId = customer.Id
                }, otpMessage);

                customerModel.OTPDetailId = otpDetail.Id;
                customerModel.MillisecondsForExpiry = _appSettings.OTPValidMinutes * 60000;

                response.Data = customerModel;
                response.Message = isEnglish ? string.Format(Messages.LoginSuccess, customer.MobileNumber) : string.Format(MessagesAr.LoginSuccess, customer.MobileNumber);
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }

        /// <summary>
        /// success=true
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="customerModel"></param>
        /// <returns></returns>        
        public async Task<APIResponseModel<CustomerModel>> Register(bool isEnglish, CustomerRegisterModel customerRegisterModel)
        {
            var response = new APIResponseModel<CustomerModel>();
            try
            {
                if (string.IsNullOrEmpty(customerRegisterModel.MobileNumber))
                {
                    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    return response;
                }

                var customer = await _customerService.GetCustomerByMobileNumber(customerRegisterModel.MobileNumber, null);
                if (!customerRegisterModel.Guest)
                {
                    //if (string.IsNullOrEmpty(customerRegisterModel.Name))
                    //{
                    //    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    //    return response;
                    //}

                    //if (string.IsNullOrEmpty(customerRegisterModel.EmailAddress))
                    //{
                    //    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    //    return response;
                    //}

                    if (customer != null)
                    {
                        response.Message = isEnglish ? Messages.MobileNumberExists : MessagesAr.MobileNumberExists;
                        return response;
                    }
                }

                CustomerRegisterRequest customerRegisterRequest = null;
                if (customer == null)
                {
                    customerRegisterRequest = await _customerService.CreateCustomerRegisterRequest(new CustomerRegisterRequest
                    {
                        Name = customerRegisterModel.Name,
                        EmailAddress = customerRegisterModel.EmailAddress,
                        CountryId = customerRegisterModel.CountryId,
                        MobileNumber = customerRegisterModel.MobileNumber,
                        Password = customerRegisterModel.Password,
                        CreatedOn = DateTime.Now
                    });
                }

                string otpMessage = string.Empty;
                var notificationTemplate = await _notificationTemplateService.GetNotificationTemplateByTypeId(NotificationType.Register);
                if (notificationTemplate == null)
                    otpMessage = isEnglish ? Messages.OTPMessage : MessagesAr.OTPMessage;
                else
                    otpMessage = isEnglish ? notificationTemplate.SMSMessageEn : notificationTemplate.SMSMessageAr;

                DateTime validTo = DateTime.Now.AddMinutes(_appSettings.OTPValidMinutes);
                string otp = Common.GenerateRandomNo(1000, 9999);

                var defaultOTPMobileNumbers = _appSettings.DefaultOTPMobileNumbers.Split(',').ToList();
                var defaultOTPMobileNumber = defaultOTPMobileNumbers.Where(a => a == customerRegisterModel.MobileNumber).FirstOrDefault();
                if (defaultOTPMobileNumber != null)
                    otp = _appSettings.DefaultOTPValue;

                var otpDetail = await _notificationTemplateService.CreateOTPDetail(new OTPDetail
                {
                    Destination = customerRegisterModel.MobileNumber,
                    OTP = otp,
                    OTPValidFrom = DateTime.Now,
                    OTPValidTo = validTo,
                    Type = customerRegisterModel.Guest ? NotificationType.GuestLoginWithRegister : NotificationType.Register,
                    CustomerRegisterRequestId = customer == null ? customerRegisterRequest?.Id : null,
                    CustomerId = customer != null ? customer?.Id : null
                }, otpMessage);

                CustomerModel customerModel = new();
                customerModel.OTPDetailId = otpDetail.Id;
                customerModel.MillisecondsForExpiry = _appSettings.OTPValidMinutes * 60000;

                response.Data = customerModel;
                response.Message = isEnglish ? string.Format(Messages.RegisterSuccess, customerRegisterModel.MobileNumber) : string.Format(MessagesAr.RegisterSuccess, customerRegisterModel.MobileNumber);
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }

        /// <summary>
        /// success=true
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="otpDetailId"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<CustomerModel>> ResendOTP(bool isEnglish, int otpDetailId)
        {
            var response = new APIResponseModel<CustomerModel>();
            try
            {
                var otpDetail = await _notificationTemplateService.GetOTPDetailById(otpDetailId);
                if (otpDetail == null)
                {
                    response.Message = isEnglish ? Messages.OTPRequestNotExists : MessagesAr.OTPRequestNotExists;
                    return response;
                }

                string otpMessage = string.Empty;
                var notificationTemplate = await _notificationTemplateService.GetNotificationTemplateByTypeId(NotificationType.Register);
                if (notificationTemplate == null)
                    otpMessage = isEnglish ? Messages.OTPMessage : MessagesAr.OTPMessage;
                else
                    otpMessage = isEnglish ? notificationTemplate.SMSMessageEn : notificationTemplate.SMSMessageAr;

                string destination = otpDetail.Destination;

                DateTime validTo = DateTime.Now.AddMinutes(_appSettings.OTPValidMinutes);
                string otp = Common.GenerateRandomNo(1000, 9999);

                var defaultOTPMobileNumbers = _appSettings.DefaultOTPMobileNumbers.Split(',').ToList();
                var defaultOTPMobileNumber = defaultOTPMobileNumbers.Where(a => a == destination).FirstOrDefault();
                if (defaultOTPMobileNumber != null)
                    otp = _appSettings.DefaultOTPValue;

                var otpDetail1 = await _notificationTemplateService.CreateOTPDetail(new OTPDetail
                {
                    OTP = otp,
                    Destination = destination,
                    OTPValidFrom = DateTime.Now,
                    OTPValidTo = validTo,
                    Type = otpDetail.Type,
                    CustomerId = otpDetail.CustomerId,
                    CustomerRegisterRequestId = otpDetail.CustomerRegisterRequestId
                }, otpMessage);

                CustomerModel customerModel = new();
                customerModel.OTPDetailId = otpDetail1.Id;
                customerModel.MillisecondsForExpiry = _appSettings.OTPValidMinutes * 60000;

                response.Data = customerModel;
                response.Message = isEnglish ? string.Format(Messages.LoginSuccess, destination) : string.Format(MessagesAr.LoginSuccess, destination);
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }

        /// <summary>
        /// success=true
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="otpDetailId"></param>
        /// <param name="otp"></param>
        /// <param name="customerGuidValue"></param>
        /// <param name="deviceId"></param>
        /// <param name="deviceToken"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<CustomerModel>> VerifyOTP(bool isEnglish, int otpDetailId, string otp, string customerGuidValue = "",
            string deviceId = "", string deviceToken = "")
        {
            var response = new APIResponseModel<CustomerModel>();
            try
            {
                string expirationDate = string.Empty;
                var otpDetail = await _notificationTemplateService.GetOTPDetailById(otpDetailId);
                if (otpDetail == null)
                {
                    response.Message = isEnglish ? Messages.OTPRequestNotExists : MessagesAr.OTPRequestNotExists;
                    return response;
                }

                if (otpDetail.OTP != otp)
                {
                    response.Message = isEnglish ? Messages.OTPIncorrect : MessagesAr.OTPIncorrect;
                    return response;
                }

                if (DateTime.Now > otpDetail.OTPValidTo)
                {
                    response.Message = isEnglish ? Messages.OTPExpired : MessagesAr.OTPExpired;
                    return response;
                }

                Customer customer = null;
                if (otpDetail.Type == NotificationType.Register)
                {
                    if (otpDetail.CustomerRegisterRequestId == null)
                    {

                        response.Message = isEnglish ? Messages.CustomerRegisterRequestNotExists : MessagesAr.CustomerRegisterRequestNotExists;
                        return response;
                    }

                    var customerRegisterRequest = await _customerService.GetCustomerRegisterRequestById(otpDetail.CustomerRegisterRequestId.Value);
                    if (customerRegisterRequest == null)
                    {

                        response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
                        return response;
                    }

                    var oldCustomer = await _customerService.GetCustomerByMobileNumber(customerRegisterRequest.MobileNumber);
                    if (oldCustomer != null)
                    {
                        response.Message = isEnglish ? Messages.CustomerExists : MessagesAr.CustomerExists;
                        return response;
                    }

                    customer = await _customerService.CreateCustomer(new Customer
                    {
                        Name = customerRegisterRequest.Name,
                        EmailAddress = customerRegisterRequest.EmailAddress,
                        CountryId = customerRegisterRequest.CountryId,
                        MobileNumber = customerRegisterRequest.MobileNumber,
                        Password = customerRegisterRequest.Password,
                        LanguageId = isEnglish ? 1 : 2,
                        Active = true,
                        CreatedOn = DateTime.Now
                    });

                    if (!string.IsNullOrEmpty(deviceId) && !string.IsNullOrEmpty(deviceToken))
                    {
                        DeviceToken tokenObj = new();
                        tokenObj.CustomerId = customer.Id;
                        tokenObj.DeviceId = deviceId;
                        tokenObj.Token = deviceToken;
                        await _notificationService.CreateDeviceToken(tokenObj);
                    }

                    var token = _commonHelper.CreateAccessToken(customer, out expirationDate);
                    customer.Token = token;
                    await _customerService.UpdateCustomer(customer);

                    if (!string.IsNullOrEmpty(customerGuidValue))
                    {
                        await _commonHelper.MigrateCarts(customerGuidValue, customer.Id);
                    }

                    var promotion = await _promotionService.GetDefault();
                    if (promotion != null && promotion.SignupEnabled)
                    {
                        bool addPromotion = true;

                        if (promotion.SignupFromDate.HasValue)
                        {
                            if (DateTime.Now.Date < promotion.SignupFromDate.Value.Date)
                            {
                                addPromotion = false;
                            }
                        }

                        if (promotion.SignupToDate.HasValue)
                        {
                            if (DateTime.Now.Date > promotion.SignupToDate.Value.Date)
                            {
                                addPromotion = false;
                            }
                        }

                        if (addPromotion)
                        {
                            var walletTransaction = new WalletTransaction
                            {
                                TransactionNo = Common.GenerateRandomNo(10000000, 99999999),
                                CreatedBy = customer.Id,
                                CustomerId = customer.Id,
                                WalletTypeId = WalletType.Cashback,
                                RelatedEntityTypeId = RelatedEntityType.SignUp,
                                RelatedEntityId = customer.Id,
                                Credit = promotion.SignupCashbackValue,
                                RemainingCredit = promotion.SignupCashbackValue,
                                Debit = 0,
                                WalletTransactionTypeId = WalletTransactionType.SignUpPromotion,
                                ExpiryDate = promotion.SignupCashbackValueExpiryInNoOfDays > 0 ? DateTime.Now.AddDays(promotion.SignupCashbackValueExpiryInNoOfDays).Date : null
                            };

                            await _customerService.CreateWalletTransaction(walletTransaction);
                        }
                    }
                }
                else if (otpDetail.Type == NotificationType.GuestLoginWithRegister)
                {
                    if (otpDetail.CustomerId.HasValue)
                    {
                        customer = await _customerService.GetCustomerById(otpDetail.CustomerId.Value);
                        if (customer == null)
                        {
                            response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
                            return response;
                        }
                    }
                    else
                    {
                        if (otpDetail.CustomerRegisterRequestId == null)
                        {
                            response.Message = isEnglish ? Messages.CustomerRegisterRequestNotExists : MessagesAr.CustomerRegisterRequestNotExists;
                            return response;
                        }

                        var customerRegisterRequest = await _customerService.GetCustomerRegisterRequestById(otpDetail.CustomerRegisterRequestId.Value);
                        if (customerRegisterRequest == null)
                        {
                            response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
                            return response;
                        }

                        customer = await _customerService.CreateCustomer(new Customer
                        {
                            Name = customerRegisterRequest.Name,
                            EmailAddress = customerRegisterRequest.EmailAddress,
                            CountryId = customerRegisterRequest.CountryId,
                            MobileNumber = customerRegisterRequest.MobileNumber,
                            Password = customerRegisterRequest.Password,
                            LanguageId = isEnglish ? 1 : 2,
                            Active = true,
                            CreatedOn = DateTime.Now
                        });
                    }

                    if (!string.IsNullOrEmpty(deviceId) && !string.IsNullOrEmpty(deviceToken))
                    {
                        DeviceToken tokenObj = new();
                        tokenObj.CustomerId = customer.Id;
                        tokenObj.DeviceId = deviceId;
                        tokenObj.Token = deviceToken;
                        await _notificationService.CreateDeviceToken(tokenObj);
                    }

                    var token = _commonHelper.CreateAccessToken(customer, out expirationDate);
                    customer.Token = token;
                    await _customerService.UpdateCustomer(customer);

                    if (!string.IsNullOrEmpty(customerGuidValue))
                    {
                        await _commonHelper.MigrateCarts(customerGuidValue, customer.Id, clearexistingCartItems: true);
                    }
                }
                else if (otpDetail.Type == NotificationType.UpdateMobileNumber)
                {
                    customer = await _customerService.GetCustomerById(otpDetail.CustomerId.Value);
                    if (customer == null)
                    {
                        response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
                        return response;
                    }

                    if (string.IsNullOrEmpty(customer.NewMobileNumber))
                    {
                        response.Message = isEnglish ? Messages.MobileNumberUpdatedAlready : MessagesAr.MobileNumberUpdatedAlready;
                        return response;
                    }

                    var customerByNewMobileNumber = await _customerService.GetCustomerByMobileNumber(customer.NewMobileNumber);
                    if (customerByNewMobileNumber != null)
                    {
                        response.Message = isEnglish ? Messages.MobileNumberExists : MessagesAr.MobileNumberExists;
                        return response;
                    }

                    customer.MobileNumber = customer.NewMobileNumber;
                    customer.NewMobileNumber = string.Empty;
                    await _customerService.UpdateCustomer(customer);
                }
                else if (otpDetail.Type == NotificationType.Login)
                {
                    customer = await _customerService.GetCustomerById(otpDetail.CustomerId.Value);
                    if (customer == null || customer.Deleted)
                    {
                        response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
                        return response;
                    }

                    if (!customer.Active)
                    {
                        _logger.LogInformation(Messages.InactiveCustomer);
                        response.Message = isEnglish ? Messages.InactiveCustomer : MessagesAr.InactiveCustomer;
                        return response;
                    }

                    if (!string.IsNullOrEmpty(deviceId) && !string.IsNullOrEmpty(deviceToken))
                    {
                        DeviceToken tokenObj = new();
                        tokenObj.CustomerId = customer.Id;
                        tokenObj.DeviceId = deviceId;
                        tokenObj.Token = deviceToken;
                        await _notificationService.CreateDeviceToken(tokenObj);
                    }

                    var token = _commonHelper.CreateAccessToken(customer, out expirationDate);
                    customer.Token = token;
                    await _customerService.UpdateCustomer(customer);

                    if (!string.IsNullOrEmpty(customerGuidValue))
                    {
                        await _commonHelper.MigrateCarts(customerGuidValue, customer.Id);
                    }
                }

                var customerModel = _modelHelper.PrepareCustomerModel(customer: customer, isEnglish: isEnglish);
                customerModel.Token = customer.Token;
                customerModel.Expiration = expirationDate;

                response.Data = customerModel;
                response.Message = isEnglish ? Messages.OTPVerificationSuccess : MessagesAr.OTPVerificationSuccess;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }

        /// <summary>
        /// success=true
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<CustomerModel>> PrepareCustomer(bool isEnglish, int id)
        {
            var response = new APIResponseModel<CustomerModel>();
            try
            {
                if (id <= 0)
                {
                    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    return response;
                }

                var customer = await _customerService.GetCustomerById(id);
                if (customer == null || customer.Deleted)
                {
                    _logger.LogInformation(Messages.CustomerNotExists);
                    response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
                    return response;
                }

                if (!customer.Active)
                {
                    _logger.LogInformation(Messages.InactiveCustomer);
                    response.Message = isEnglish ? Messages.InactiveCustomer : MessagesAr.InactiveCustomer;
                    return response;
                }

                var customerModel = _modelHelper.PrepareCustomerModel(customer: customer, isEnglish: isEnglish);
                response.Data = customerModel;
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

        /// <summary>
        /// success=true
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<CustomerModel>> DeleteCustomer(bool isEnglish, int id)
        {
            var response = new APIResponseModel<CustomerModel>();
            try
            {
                if (id <= 0)
                {
                    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    return response;
                }

                var customer = await _customerService.GetCustomerById(id);
                if (customer == null || customer.Deleted)
                {
                    _logger.LogInformation(Messages.CustomerNotExists);
                    response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
                    return response;
                }

                if (!customer.Active)
                {
                    _logger.LogInformation(Messages.InactiveCustomer);
                    response.Message = isEnglish ? Messages.InactiveCustomer : MessagesAr.InactiveCustomer;
                    return response;
                }

                customer.Deleted = true;
                await _customerService.UpdateCustomer(customer);

                response.Data = new CustomerModel();
                response.Message = isEnglish ? Messages.DeleteCustomerSuccess : MessagesAr.DeleteCustomerSuccess;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }

        /// <summary>
        /// success=true
        /// </summary>
        /// <param name="isEnglish"></param>
        /// <param name="customerModel"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<CustomerModel>> UpdateCustomer(bool isEnglish, CustomerModel customerModel)
        {
            var response = new APIResponseModel<CustomerModel>();
            try
            {
                //if (string.IsNullOrEmpty(customerModel.Name))
                //{
                //    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                //    return response;
                //}

                //if (string.IsNullOrEmpty(customerModel.EmailAddress))
                //{

                //    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                //    return response;
                //}

                if (string.IsNullOrEmpty(customerModel.MobileNumber))
                {

                    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    return response;
                }

                var customer = await _customerService.GetCustomerById(customerModel.Id);
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

                customer.Name = customerModel.Name;
                customer.EmailAddress = customerModel.EmailAddress;

                bool sendOtp = false;
                if (customer.MobileNumber.ToLower() != customerModel.MobileNumber.ToLower())
                {
                    customer.NewMobileNumber = customerModel.MobileNumber;

                    var customerByMobileNumber = await _customerService.GetCustomerByMobileNumber(mobileNumber: customerModel.MobileNumber.ToLower(), id: customer.Id);
                    if (customerByMobileNumber != null)
                    {
                        response.Message = isEnglish ? Messages.CustomerExists : MessagesAr.CustomerExists;
                        return response;
                    }

                    sendOtp = true;
                }

                await _customerService.UpdateCustomer(customer);

                int otpDetailId = 0;
                if (sendOtp)
                {
                    string otpMessage = string.Empty;
                    string subject = string.Empty;
                    var notificationTemplate = await _notificationTemplateService.GetNotificationTemplateByTypeId(NotificationType.UpdateMobileNumber);
                    if (notificationTemplate == null)
                    {
                        subject = isEnglish ? Messages.UpdateEmailIdEmailSubject : MessagesAr.UpdateEmailIdEmailSubject;
                        otpMessage = isEnglish ? Messages.OTPMessage : MessagesAr.OTPMessage;
                    }
                    else
                    {
                        subject = "No Subject";
                        otpMessage = "No Message";
                    }

                    DateTime validTo = DateTime.Now.AddMinutes(_appSettings.OTPValidMinutes);
                    string otp = Common.GenerateRandomNo(1000, 9999);

                    var defaultOTPMobileNumbers = _appSettings.DefaultOTPMobileNumbers.Split(',').ToList();
                    var defaultOTPMobileNumber = defaultOTPMobileNumbers.Where(a => a == customerModel.MobileNumber).FirstOrDefault();
                    if (defaultOTPMobileNumber != null)
                        otp = _appSettings.DefaultOTPValue;

                    var otpDetail = await _notificationTemplateService.CreateOTPDetail(new OTPDetail
                    {
                        Destination = customerModel.MobileNumber,
                        OTP = otp,
                        OTPValidFrom = DateTime.Now,
                        OTPValidTo = validTo,
                        Type = NotificationType.UpdateMobileNumber,
                        CustomerId = customer.Id
                    }, otpMessage);

                    otpDetailId = otpDetail.Id;
                }

                customerModel = _modelHelper.PrepareCustomerModel(customer: customer, isEnglish: isEnglish);
                customerModel.OTPDetailId = otpDetailId;
                customerModel.MillisecondsForExpiry = sendOtp ? _appSettings.OTPValidMinutes * 60000 : 0;
                response.Data = customerModel;
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

        /// <summary>
        /// To get all address
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <param name="id">Address identifier</param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<AddressModel>>> GetAddress(bool isEnglish, int customerId, int id, RelatedEntityType? relatedEntityType = null)
        {
            var response = new APIResponseModel<List<AddressModel>>();
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

                var addresses = await _customerService.GetAllAddress(customerId: customerId);

                if (id > 0)
                {
                    addresses = addresses.Where(a => a.Id == id).ToList();
                }

                addresses = addresses.OrderByDescending(a => a.Id).ToList();

                List<AddressModel> addressModels = new();
                foreach (var address in addresses)
                {
                    var addressModel = await _modelHelper.PrepareAddressModel(address, isEnglish);
                    addressModels.Add(addressModel);
                }

                if (relatedEntityType != null)
                {
                    if (relatedEntityType.Value == RelatedEntityType.Order)
                    {
                        var cartAttribute = (await _cartService.GetAllCartAttribute(customerId: customer.Id)).FirstOrDefault();
                        if (cartAttribute != null && cartAttribute.AddressId.HasValue)
                        {
                            foreach (var addressModel in addressModels)
                            {
                                if (addressModel.Id == cartAttribute.AddressId.Value)
                                    addressModel.Selected = true;
                            }
                        }
                    }
                    else if (relatedEntityType.Value == RelatedEntityType.Subscription)
                    {
                        var subscriptionAttribute = await _cartService.GetSubscriptionAttributeByCustomerId(customerId: customer.Id);
                        if (subscriptionAttribute != null && subscriptionAttribute.AddressId.HasValue)
                        {
                            foreach (var addressModel in addressModels)
                            {
                                if (addressModel.Id == subscriptionAttribute.AddressId.Value)
                                    addressModel.Selected = true;
                            }
                        }
                    }
                }

                response.Data = addressModels;
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

        /// <summary>
        /// To add address
        /// </summary>
        /// <param name="addressDto">Address dto</param>
        /// <returns></returns>
        public async Task<APIResponseModel<object>> AddAddress(bool isEnglish, AddressModel addressModel)
        {
            var response = new APIResponseModel<object>();
            try
            {
                var customer = await _customerService.GetCustomerById(addressModel.CustomerId);
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

                var address = _mapper.Map(addressModel, new Address());
                address.CreatedOn = DateTime.Now;
                await _customerService.CreateAddress(address);

                response.Message = isEnglish ? Messages.AddAddressSuccess : MessagesAr.AddAddressSuccess;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }

        /// <summary>
        /// To add address
        /// </summary>
        /// <param name="addressDto">Address dto</param>
        /// <returns></returns>
        public async Task<APIResponseModel<AddressModel>> AddAddressWithSelect(bool isEnglish, AddressModel addressModel)
        {
            var response = new APIResponseModel<AddressModel>();
            try
            {
                var customer = await _customerService.GetCustomerById(addressModel.CustomerId);
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

                var address = _mapper.Map(addressModel, new Address());
                address.CreatedOn = DateTime.Now;
                address = await _customerService.CreateAddress(address);

                addressModel = await _modelHelper.PrepareAddressModel(address, isEnglish);

                response.Data = addressModel;
                response.Message = isEnglish ? Messages.AddAddressSuccess : MessagesAr.AddAddressSuccess;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }

        /// <summary>
        /// To update address
        /// </summary>
        /// <param name="addressDto">Address dto</param>
        /// <returns></returns>
        public async Task<APIResponseModel<object>> UpdateAddress(bool isEnglish, AddressModel addressModel)
        {
            var response = new APIResponseModel<object>();
            try
            {
                var customer = await _customerService.GetCustomerById(addressModel.CustomerId);
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

                var address = await _customerService.GetAddressById(addressModel.Id);
                if (address == null)
                {
                    response.Message = isEnglish ? Messages.AddressNotExists : MessagesAr.AddressNotExists;
                    return response;
                }

                _mapper.Map(addressModel, address);
                await _customerService.UpdateAddress(address);

                response.Message = isEnglish ? Messages.UpdateAddressSuccess : MessagesAr.UpdateAddressSuccess;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }

        /// <summary>
        /// To delete address
        /// </summary>
        /// <param name="id">Address identifier</param>
        /// <returns></returns>
        public async Task<APIResponseModel<object>> DeleteAddress(bool isEnglish, int id, int customerId = 0)
        {
            var response = new APIResponseModel<object>();
            try
            {
                var address = await _customerService.GetAddressById(id);
                if (address == null)
                {
                    response.Message = isEnglish ? Messages.AddressNotExists : MessagesAr.AddressNotExists;
                    return response;
                }

                if (address.CustomerId != customerId)
                {
                    response.Message = isEnglish ? Messages.AddressNotExists : MessagesAr.AddressNotExists;
                    return response;
                }

                var customer = await _customerService.GetCustomerById(address.CustomerId);
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

                address.Deleted = true;
                await _customerService.UpdateAddress(address);

                response.Message = isEnglish ? Messages.DeleteAddressSuccess : MessagesAr.DeleteAddressSuccess;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }

        /// <summary>
        /// To get all notification
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <param name="limit">limit per page</param>
        /// <param name="page">cusrrent page number</param>
        /// <returns></returns>
        public async Task<APIResponseModel<List<NotificationModel>>> GetNotification(bool isEnglish, int customerId, int limit = 0, int page = 0)
        {
            var response = new APIResponseModel<List<NotificationModel>>();
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

                var notifications = await _notificationService.GetAllNotification(customerId: customerId);

                notifications = notifications.OrderByDescending(a => a.Id).ToList();

                response.DataRecordCount = notifications.Count;

                if (limit > 0 && page > 0)
                {
                    notifications = notifications.Skip((page - 1) * limit).Take(limit).ToList();
                }

                var notificationModels = notifications.Select(notification =>
                {
                    var notificationModel = _modelHelper.PrepareNotificationModel(notification, isEnglish);
                    return notificationModel;
                }).ToList();

                response.Data = notificationModels;
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

        /// <summary>
        /// To get all wallet transactions
        /// </summary>
        /// <param name="id">Wallet transaction identifier</param>
        /// <returns></returns>
        public async Task<APIResponseModel<WalletModel>> GetWalletTransactions(bool isEnglish, int customerId, WalletType? walletType = null)
        {
            var response = new APIResponseModel<WalletModel>();
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

                var walletTransactions = await _customerService.GetAllWalletTransaction(customerId: customerId, walletType: walletType);

                List<WalletTransactionByDateModel> walletTransactionByDateModels = new();
                var dates = walletTransactions.GroupBy(a => a.CreatedOn.Date).Select(a => a.First().CreatedOn.Date).OrderByDescending(a => a.Date);
                foreach (var date in dates)
                {
                    var walletTransactionByDate = walletTransactions.Where(a => a.CreatedOn.Date == date).OrderByDescending(a => a.Id);

                    var walletTransactionModels = new List<WalletTransactionModel>();
                    foreach (var walletTransaction in walletTransactionByDate)
                    {
                        var walletTransactionModel = await _modelHelper.PrepareWalletTransactionModel(walletTransaction, isEnglish);
                        walletTransactionModels.Add(walletTransactionModel);
                    }

                    walletTransactionByDateModels.Add(new WalletTransactionByDateModel
                    {
                        FormattedDate = date.ToString("dd'/'MM'/'yyyy"),
                        WalletTransactions = walletTransactionModels
                    });
                }

                WalletModel walletModel = new();

                walletModel.WalletBalance = await _customerService.GetWalletBalanceByCustomerId(id: customerId, walletTypeId: WalletType.Wallet);
                walletModel.FormattedWalletBalance = await _commonHelper.ConvertDecimalToString(value: walletModel.WalletBalance, isEnglish: isEnglish,
                    includeZero: true);

                walletModel.CashbackBalance = await _customerService.GetWalletBalanceByCustomerId(id: customerId, walletTypeId: WalletType.Cashback);
                walletModel.FormattedCashbackBalance = await _commonHelper.ConvertDecimalToString(value: walletModel.CashbackBalance, isEnglish: isEnglish,
                    includeZero: true);

                walletModel.WalletTransactionByDates = walletTransactionByDateModels;

                response.Data = walletModel;
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

        /// <summary>
        /// To switch the selected language
        /// </summary>
        /// <param name="language">language</param>
        /// <returns></returns>
        public async Task<APIResponseModel<object>> SwitchLanguage(bool isEnglish, string language, string deviceId, int customerId = 0)
        {
            var response = new APIResponseModel<object>();
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

                    customer.LanguageId = language == "en" ? 1 : 2;
                    await _customerService.UpdateCustomer(customer);
                }
                else
                {
                    if (string.IsNullOrEmpty(language) || string.IsNullOrEmpty(deviceId))
                    {
                        response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    }

                    var deviceToken = await _notificationService.GetDeviceTokenByDeviceId(deviceId);
                    if (deviceToken == null)
                    {
                        response.Message = isEnglish ? Messages.DeviceTokenNotExists : MessagesAr.DeviceTokenNotExists;
                        return response;
                    }

                    deviceToken.LanguageId = language == "en" ? 1 : 2;
                    await _notificationService.UpdateDeviceToken(deviceToken);
                }

                response.Message = isEnglish ? Messages.UpdateCutomerLanguageSucess : MessagesAr.UpdateCutomerLanguageSucess;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }

        /// <summary>
        /// Create device token
        /// </summary>
        /// <param name="deviceTokenDto">Device token dto</param>
        /// <returns></returns>
        public async Task<APIResponseModel<object>> CreateDeviceToken(bool isEnglish, DeviceTokenModel deviceTokenModel)
        {
            var response = new APIResponseModel<object>();
            try
            {
                var deviceToken = _mapper.Map(deviceTokenModel, new DeviceToken());
                deviceToken.LanguageId = isEnglish ? 1 : 2;
                await _notificationService.CreateDeviceToken(deviceToken);

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

        /// <summary>
        /// To refresh the access token
        /// </summary>
        /// <param name="refreshTokenRequest"></param>
        /// <returns></returns>
        public async Task<APIResponseModel<RefreshTokenModel>> RefreshToken(bool isEnglish, RefreshTokenModel refreshTokenModel)
        {
            var response = new APIResponseModel<RefreshTokenModel>();
            try
            {
                int customerId = _commonHelper.GetCustomerIdByToken(refreshTokenModel.OldToken);
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

                    var token = _commonHelper.CreateAccessToken(customer, out string expiration);

                    customer.Token = token;
                    customer.ModifiedOn = DateTime.Now;
                    await _customerService.UpdateCustomer(customer);

                    refreshTokenModel.NewToken = token;
                    refreshTokenModel.Expiration = expiration;
                    response.Data = refreshTokenModel;
                    response.Message = isEnglish ? Messages.TokenRefresh : MessagesAr.TokenRefresh;
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }
        public async Task<APIResponseModel<CreatePaymentModel>> CreateWalletPackageOrder(bool isEnglish, int customerId, DeviceType deviceTypeId, CreatePaymentModel createPaymentModel)
        {
            var response = new APIResponseModel<CreatePaymentModel>();

            try
            {
                if (customerId <= 0 || createPaymentModel.WalletPackageId <= 0 || createPaymentModel.PaymentMethodId <= 0)
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

                var walletPackage = await _walletPackageService.GetWalletPackageById(createPaymentModel.WalletPackageId);
                if (walletPackage == null || walletPackage.Deleted)
                {
                    response.Message = isEnglish ? Messages.WalletPackageNotExists : MessagesAr.WalletPackageNotExists;
                    return response;
                }

                if (!walletPackage.Active)
                {
                    response.Message = isEnglish ? Messages.WalletPackageNotActive : MessagesAr.WalletPackageNotActive;
                    return response;
                }

                var paymentMethod = await _paymentMethodService.GetPaymentMethodById(createPaymentModel.PaymentMethodId);
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

                if (!paymentMethod.ForWalletPackage)
                {
                    response.Message = isEnglish ? Messages.PaymentMethodNotAvailable : MessagesAr.PaymentMethodNotAvailable;
                    return response;
                }

                var walletPackageOrder = new WalletPackageOrder
                {
                    CustomerId = customer.Id,
                    WalletPackageId = createPaymentModel.WalletPackageId,
                    OrderNumber = string.Empty,
                    Amount = walletPackage.Amount,
                    WalletAmount = walletPackage.WalletAmount,
                    CustomerLanguageId = isEnglish ? 1 : 2,
                    CustomerIp = createPaymentModel.CustomerIp,
                    PaymentMethodId = createPaymentModel.PaymentMethodId,
                    PaymentStatusId = PaymentStatus.Canceled,
                    CreatedOn = DateTime.Now,
                    DeviceTypeId = deviceTypeId
                };

                if (walletPackageOrder.Amount <= 0)
                {
                    response.Message = isEnglish ? Messages.OrderAmountValidation : MessagesAr.OrderAmountValidation;
                    return response;
                }

                walletPackageOrder = await _walletPackageService.CreateWalletPackageOrder(walletPackageOrder);
                if (walletPackageOrder != null)
                {
                    var orderNumber = string.Empty;
                    WalletPackageOrder orderByOrderNumber = null;
                    do
                    {
                        orderNumber = "10" + Common.GenerateRandomNo();
                        orderByOrderNumber = await _walletPackageService.GetWalletPackageOrderByOrderNumber(orderNumber);
                    }
                    while (orderByOrderNumber != null);

                    walletPackageOrder.OrderNumber = orderNumber;
                    await _walletPackageService.UpdateWalletPackageOrder(walletPackageOrder);

                    if (walletPackageOrder.PaymentMethodId == (int)PaymentMethod.KNET)
                    {
                        var paymentUrlRequestModel = new PaymentUrlRequestModel
                        {
                            LangId = walletPackageOrder.CustomerLanguageId.ToString(),
                            Amount = walletPackageOrder.Amount.ToString("N3"),
                            TrackId = walletPackageOrder.OrderNumber.ToString(),
                            EntityId = walletPackageOrder.Id.ToString(),
                            CustomerId = walletPackageOrder.CustomerId.ToString(),
                            RequestType = PaymentRequestType.WalletPackageOrder.ToString()
                        };

                        var paymentUrl = await _apiHelper.PostAsync<string>("Home/GetPaymentUrl", paymentUrlRequestModel, baseUrl: _appSettings.PaymentAPIUrl);
                        if (!string.IsNullOrEmpty(paymentUrl))
                        {
                            createPaymentModel.PaymentUrl = paymentUrl;
                            createPaymentModel.RedirectToPaymentPage = true;
                        }
                    }
                    else if (walletPackageOrder.PaymentMethodId == (int)PaymentMethod.VISAMASTER)
                    {
                        var value = Cryptography.Encrypt(PaymentRequestType.WalletPackageOrder.ToString() + "-" + walletPackageOrder.Id);
                        createPaymentModel.PaymentUrl = _appSettings.APIBaseUrl + _appSettings.MasterCardInteractionRequestUrl + "?value=" + value;
                        createPaymentModel.RedirectToPaymentPage = true;
                    }

                    if (walletPackageOrder.DeviceTypeId == DeviceType.Web)
                        createPaymentModel.PaymentReturnUrl = _appSettings.WebsiteUrl + "WPP/" + walletPackageOrder.OrderNumber;
                    else
                        createPaymentModel.PaymentReturnUrl = _appSettings.WebsiteUrl + "paymentresult";

                    createPaymentModel.EntityId = walletPackageOrder.Id;
                    createPaymentModel.PaymentRequestTypeId = PaymentRequestType.WalletPackageOrder;
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
        public async Task<APIResponseModel<List<WalletPackageOrderModel>>> GetWalletPackageOrders(bool isEnglish, int customerId, int id = 0, string orderNumber = "",
            int limit = 0, int page = 0)
        {
            var response = new APIResponseModel<List<WalletPackageOrderModel>>();
            try
            {
                var walletPackageOrders = new List<WalletPackageOrder>();

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
                    var walletPackageOrder = await _walletPackageService.GetWalletPackageOrderById(id);
                    if (walletPackageOrder != null && !walletPackageOrder.Deleted && walletPackageOrder.Customer != null)
                    {
                        if (walletPackageOrder.CustomerId != customerId)
                        {
                            response.Message = isEnglish ? Messages.InvalidCustomer : MessagesAr.InvalidCustomer;
                            return response;
                        }

                        walletPackageOrders.Add(walletPackageOrder);
                    }
                }
                else if (!string.IsNullOrEmpty(orderNumber))
                {
                    loadDetails = true;
                    var walletPackageOrder = await _walletPackageService.GetWalletPackageOrderByOrderNumber(orderNumber);
                    if (walletPackageOrder != null && !walletPackageOrder.Deleted)
                        walletPackageOrders.Add(walletPackageOrder);
                }
                else
                {
                    walletPackageOrders = await _walletPackageService.GetAllWalletPackageOrder(customerId: customerId);
                }

                walletPackageOrders = walletPackageOrders.OrderByDescending(a => a.Id).ToList();

                response.DataRecordCount = walletPackageOrders.Count;

                if (limit > 0 && page > 0)
                {
                    walletPackageOrders = walletPackageOrders.Skip((page - 1) * limit).Take(limit).ToList();
                }

                var walletPackageOrderModels = new List<WalletPackageOrderModel>();
                foreach (var walletPackageOrder in walletPackageOrders)
                {
                    var walletPackageOrderModel = await _modelHelper.PrepareWalletPackageOrderModel(walletPackageOrder, isEnglish, loadDetails: loadDetails);
                    walletPackageOrderModels.Add(walletPackageOrderModel);
                }

                response.Data = walletPackageOrderModels;
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
