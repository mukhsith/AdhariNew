using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Enum;
using Utility.Models.Frontend.CustomerManagement;
using Utility.Models.Frontend.CustomizedModel;
using Utility.Models.Frontend.Sales;
using Utility.ResponseMapper;

namespace API.Areas.Frontend.Factories
{
    public interface ICustomerModelFactory
    {
        Task<APIResponseModel<CustomerModel>> Login(bool isEnglish, CustomerModel customerModel);
        Task<APIResponseModel<CustomerModel>> Register(bool isEnglish, CustomerRegisterModel customerModel);
        Task<APIResponseModel<CustomerModel>> ResendOTP(bool isEnglish, int otpDetailId);
        Task<APIResponseModel<CustomerModel>> VerifyOTP(bool isEnglish, int otpDetailId, string otp, string customerGuidValue = "", string deviceId = "", 
            string deviceToken = "", DeviceType? deviceType = null);
        Task<APIResponseModel<CustomerModel>> PrepareCustomer(bool isEnglish, int id);
        Task<APIResponseModel<CustomerModel>> DeleteCustomer(bool isEnglish, int id);
        Task<APIResponseModel<CustomerModel>> UpdateCustomer(bool isEnglish, CustomerModel customerModel);     
        Task<APIResponseModel<List<AddressModel>>> GetAddress(bool isEnglish, int customerId, int id, RelatedEntityType? relatedEntityType = null);
        Task<APIResponseModel<object>> AddAddress(bool isEnglish, AddressModel addressModel);
        Task<APIResponseModel<AddressModel>> AddAddressWithSelect(bool isEnglish, AddressModel addressModel);
        Task<APIResponseModel<object>> UpdateAddress(bool isEnglish, AddressModel addressModel);
        Task<APIResponseModel<AddressModel>> UpdateAddressWeb(bool isEnglish, AddressModel addressModel);
        Task<APIResponseModel<object>> DeleteAddress(bool isEnglish, int id, int customerId = 0);
        Task<APIResponseModel<List<NotificationModel>>> GetNotification(bool isEnglish, int customerId, int limit = 0, int page = 0);
        Task<APIResponseModel<WalletModel>> GetWalletTransactions(bool isEnglish, int customerId, WalletType? walletType = null);
        Task<APIResponseModel<object>> SwitchLanguage(bool isEnglish, string language, string deviceId, int customerId = 0);
        Task<APIResponseModel<object>> CreateDeviceToken(bool isEnglish, DeviceTokenModel deviceTokenModel);
        Task<APIResponseModel<RefreshTokenModel>> RefreshToken(bool isEnglish, RefreshTokenModel refreshTokenModel);
        Task<APIResponseModel<CreatePaymentModel>> CreateWalletPackageOrder(bool isEnglish, int customerId, DeviceType deviceTypeId, CreatePaymentModel createPaymentModel);
        Task<APIResponseModel<List<WalletPackageOrderModel>>> GetWalletPackageOrders(bool isEnglish, int customerId, int id = 0, string orderNumber = "",
                  int limit = 0, int page = 0);
        Task<APIResponseModel<object>> CreateExpiredWalletTransactions(bool isEnglish, string apiKey);
    }
}
