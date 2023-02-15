using System.Collections.Generic;
using System.Threading.Tasks; 
using Utility.ResponseMapper;
using Utility.Models.Frontend.CustomerManagement;
using Utility.Models.Admin;

namespace API.Areas.Backend.Factories
{
    public interface ICustomerModelFactory
    {
        Task<WalletModel> GetWalletByCustomerId(bool isEnglish, int id);
        //Task<APIResponseModel<CustomerModel>> Login(bool isEnglish, CustomerModel customerModel);
        //Task<APIResponseModel<CustomerModel>> Register(bool isEnglish, CustomerRegisterModel customerModel);
        //Task<APIResponseModel<CustomerModel>> ResendOTP(bool isEnglish, int otpDetailId);
        //Task<APIResponseModel<CustomerModel>> VerifyOTP(bool isEnglish, int otpDetailId, string otp, string customerGuidValue = "", string deviceId = "", string deviceToken = "");
        //Task<APIResponseModel<CustomerModel>> PrepareCustomer(bool isEnglish, int id);
        //Task<APIResponseModel<CustomerModel>> UpdateCustomer(bool isEnglish, CustomerModel customerModel);     
         Task<List<AddressModel>> GetAddress(bool isEnglish, int customerId);
        Task<AdminCustomerModel> GetByCustomerId(bool isEnglish, int id);
        Task<List<AddressModel>> GetAddressById(bool isEnglish, int id, int customerId);

        Task<APIResponseModel<object>> AddAddress(bool isEnglish, AddressModel addressModel);
         Task<APIResponseModel<object>> UpdateAddress(bool isEnglish, AddressModel addressModel);
         Task<APIResponseModel<object>> DeleteAddress(bool isEnglish, int id, int customerId = 0);
        // Task<APIResponseModel<List<NotificationModel>>> GetNotification(bool isEnglish, int customerId, int limit = 0, int page = 0);
        //Task<APIResponseModel<object>> SwitchLanguage(bool isEnglish, string language, string deviceId, int customerId = 0);
        //Task<APIResponseModel<object>> CreateDeviceToken(bool isEnglish, DeviceTokenModel deviceTokenModel);
        ////Task<APIResponseModel<RefreshTokenModel>> RefreshToken(bool isEnglish, RefreshTokenModel refreshTokenModel);



    }
}
