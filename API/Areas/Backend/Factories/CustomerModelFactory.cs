using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper; 
using API.Helpers; 
using Utility.API;  
using Utility.Models.Frontend.CustomerManagement;
using API.Areas.Backend.Helpers;
using Services.Backend.CustomerManagement;
using Services.Backend.PushNotification;
using Services.Backend.CouponPromotion.Interface;
using Utility.ResponseMapper;
using Data.CustomerManagement;
using Utility.Helpers;
using Utility.Models.Admin;

namespace API.Areas.Backend.Factories
{
public class CustomerModelFactory : ICustomerModelFactory
{
private readonly ILogger _logger;
        private readonly AppSettingsModel _appSettings;
        private readonly IModelHelper _modelHelper;
        private readonly ICustomerService _customerService; 
        private readonly INotificationService _notificationService;
        private readonly ICommonHelper _commonHelper;  
        private readonly IPromotionService _promotionService;
        private readonly IMapper _mapper;
        public CustomerModelFactory(ILoggerFactory logger,
            IOptions<AppSettingsModel> options,
            IModelHelper modelHelper,
            ICustomerService customerService, 
            INotificationService notificationService,
        ICommonHelper commonHelper,  
            IPromotionService promotionService,
            IMapper mapper)
        {
            _logger = logger.CreateLogger(typeof(CustomerModelFactory).Name);
            _appSettings = options.Value;
            _modelHelper = modelHelper;
            _customerService = customerService;
            _notificationService = notificationService;
            _commonHelper = commonHelper; 
            _promotionService = promotionService;
            _mapper = mapper;
        }
         

        

        public async Task<WalletModel> GetWalletByCustomerId(bool isEnglish, int id)
        {
            WalletModel walletModel = await _customerService.GetWalletByCustomerId(id);
            if (walletModel is not null)
            {
                walletModel.FormattedCashbackBalance = await _commonHelper.ConvertDecimalToString(walletModel.CashbackBalance, isEnglish, countryId: 1, includeZero: true);
                walletModel.FormattedWalletBalance = await _commonHelper.ConvertDecimalToString(walletModel.WalletBalance, isEnglish, countryId: 1, includeZero: true);
            }
            return walletModel;
        }


        public async Task<AdminCustomerModel> GetByCustomerId(bool isEnglish, int id)
        {
            AdminCustomerModel customerModel = await _customerService.GetById(id);
        
            return customerModel;
        }


        /// <summary>
        /// To get all address
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <param name="id">Address identifier</param>
        /// <returns></returns>
        public async Task<List<AddressModel>> GetAddress(bool isEnglish, int customerId)
        {
            List<AddressModel> response = new();
            try
            {
                var addresses = await _customerService.GetAllAddress(customerId: customerId);
                addresses = addresses.OrderByDescending(a => a.Id).ToList();

                List<AddressModel> addressModels = new();
                foreach (var address in addresses)
                {
                    var addressModel = await _modelHelper.PrepareAddressModel(address, isEnglish);
                    addressModels.Add(addressModel);
                }
                return addressModels;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return response;
        }


        /// <summary>
        /// To get all address
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <param name="id">Address identifier</param>
        /// <returns></returns>
        public async Task<List<AddressModel>> GetAddressById(bool isEnglish, int id, int customerId)
        {
            List<AddressModel> response = new();
            try
            {
                var addresses = await _customerService.GetAllAddress(customerId: customerId);
                addresses = addresses.Where(x=> x.Id== id).OrderByDescending(a => a.Id).ToList();

                List<AddressModel> addressModels = new();
                foreach (var address in addresses)
                {
                    var addressModel = await _modelHelper.PrepareAddressModel(address, isEnglish);
                    addressModels.Add(addressModel);
                }
                return addressModels;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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

        ///// To switch the selected language
        ///// </summary>
        ///// <param name="language">language</param>
        ///// <returns></returns>
        //public async Task<APIResponseModel<object>> SwitchLanguage(bool isEnglish, string language, string deviceId, int customerId = 0)
        //{
        //    var response = new APIResponseModel<object>();
        //    try
        //    {
        //        if (customerId > 0)
        //        {
        //            var customer = await _customerService.GetCustomerById(customerId);
        //            if (customer == null)
        //            {
        //                response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
        //                return response;
        //            }

        //            if (customer.Deleted)
        //            {
        //                response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
        //                return response;
        //            }

        //            if (!customer.Active)
        //            {
        //                response.Message = isEnglish ? Messages.InactiveCustomer : MessagesAr.InactiveCustomer;
        //                return response;
        //            }

        //            customer.LanguageId = language == "en" ? 1 : 2;
        //            await _customerService.UpdateCustomer(customer);
        //        }
        //        else
        //        {
        //            if (string.IsNullOrEmpty(language) || string.IsNullOrEmpty(deviceId))
        //            {
        //                response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
        //            }

        //            var deviceToken = await _notificationService.GetDeviceTokenByDeviceId(deviceId);
        //            if (deviceToken == null)
        //            {
        //                response.Message = isEnglish ? Messages.DeviceTokenNotExists : MessagesAr.DeviceTokenNotExists;
        //                return response;
        //            }

        //            deviceToken.LanguageId = language == "en" ? 1 : 2;
        //            await _notificationService.UpdateDeviceToken(deviceToken);
        //        }

        //        response.Message = isEnglish ? Messages.UpdateCutomerLanguageSucess : MessagesAr.UpdateCutomerLanguageSucess;
        //        response.Success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
        //    }

        //    return response;
        //}


        ///// <summary>
        ///// Create device token
        ///// </summary>
        ///// <param name="deviceTokenDto">Device token dto</param>
        ///// <returns></returns>
        //public async Task<APIResponseModel<object>> CreateDeviceToken(bool isEnglish, DeviceTokenModel deviceTokenModel)
        //{
        //    var response = new APIResponseModel<object>();
        //    try
        //    {
        //        var deviceToken = _mapper.Map(deviceTokenModel, new DeviceToken());
        //        deviceToken.LanguageId = isEnglish ? 1 : 2;
        //        await _notificationService.CreateDeviceToken(deviceToken);

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
        ///// <summary>
        ///// To refresh the access token
        ///// </summary>
        ///// <param name="refreshTokenRequest"></param>
        ///// <returns></returns>
        //public async Task<APIResponseModel<RefreshTokenModel>> RefreshToken(bool isEnglish, RefreshTokenModel refreshTokenModel)
        //{
        //    var response = new APIResponseModel<RefreshTokenModel>();
        //    try
        //    {
        //        int customerId = _commonHelper.GetCustomerIdByToken(refreshTokenModel.OldToken);
        //        if (customerId > 0)
        //        {
        //            var customer = await _customerService.GetCustomerById(customerId);
        //            if (customer == null)
        //            {
        //                response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
        //                return response;
        //            }

        //            if (customer.Deleted)
        //            {
        //                response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
        //                return response;
        //            }

        //            if (!customer.Active)
        //            {
        //                response.Message = isEnglish ? Messages.InactiveCustomer : MessagesAr.InactiveCustomer;
        //                return response;
        //            }

        //            var token = _commonHelper.CreateAccessToken(customer, out string expiration);

        //            customer.Token = token;
        //            customer.ModifiedOn = DateTime.Now;
        //            await _customerService.UpdateCustomer(customer);

        //            refreshTokenModel.NewToken = token;
        //            refreshTokenModel.Expiration = expiration;
        //            response.Data = refreshTokenModel;
        //            response.Message = isEnglish ? Messages.TokenRefresh : MessagesAr.TokenRefresh;
        //            response.Success = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
        //    }

        //    return response;
        //}

        //#region Utility
        ///// <summary>
        ///// To get all notification
        ///// </summary>
        ///// <param name="customerId">Customer identifier</param>
        ///// <param name="limit">limit per page</param>
        ///// <param name="page">cusrrent page number</param>
        ///// <returns></returns>
        //public async Task<APIResponseModel<List<NotificationModel>>> GetNotification(bool isEnglish, int customerId, int limit = 0, int page = 0)
        //{
        //    var response = new APIResponseModel<List<NotificationModel>>();
        //    try
        //    {
        //        var customer = await _customerService.GetCustomerById(customerId);
        //        if (customer == null)
        //        {
        //            response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
        //            return response;
        //        }

        //        if (customer.Deleted)
        //        {
        //            response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
        //            return response;
        //        }

        //        if (!customer.Active)
        //        {
        //            response.Message = isEnglish ? Messages.InactiveCustomer : MessagesAr.InactiveCustomer;
        //            return response;
        //        }

        //        var notifications = await _notificationService.GetAllNotification(customerId: customerId);

        //        notifications = notifications.OrderByDescending(a => a.Id).ToList();

        //        response.DataRecordCount = notifications.Count;

        //        if (limit > 0 && page > 0)
        //        {
        //            notifications = notifications.Skip((page - 1) * limit).Take(limit).ToList();
        //        }

        //        var notificationModels = notifications.Select(notification =>
        //        {
        //            var notificationModel = _modelHelper.PrepareNotificationModel(notification, isEnglish);
        //            return notificationModel;
        //        }).ToList();

        //        response.Data = notificationModels;
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






        // #endregion
    }
}
