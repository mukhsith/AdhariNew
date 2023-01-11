using Data.Sales;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Enum;
using Utility.Models.Admin.Sales;
using Utility.Models.Frontend.Sales;
using Utility.ResponseMapper;

namespace API.Areas.Backend.Factories
{
    public interface ISubscriptionModelFactory
    {
        //Task<APIResponseModel<SubscriptionSummaryModel>> PrepareSubscriptionSummaryModel(bool isEnglish, int customerId);
        //Task<APIResponseModel<SubscriptionSummaryModel>> SaveSubscriptionAttribute(bool isEnglish, int customerId,
        //    SubscriptionAttributeModel subscriptionAttributeModel);
        //Task<APIResponseModel<SubscriptionCheckOutModel>> PrepareSubscriptionCheckOutModel(bool isEnglish, int customerId);
        //Task<APIResponseModel<CreatePaymentModel>> CreateSubscription(bool isEnglish, int customerId, DeviceType deviceTypeId,
        //    CreatePaymentModel createPaymentModel);
        Task<APIResponseModel<List<SubscriptionModel>>> GetSubscriptions(bool isEnglish, int customerId, int id = 0, string subscriptionNumber = "",
             int limit = 0, int page = 0, SubscriptionStatus? subscriptionStatus = null);
        //Task<APIResponseModel<List<ASSubscriptionModel>>> GetAvailableSubscriptionOrderDeliveryDateAndSlot(bool isEnglish, AvailableSubscriptionOrderDeliveryDate model);
        Task<dynamic> GetSubscriptionsForDataTable(AdminSubscriptionOrderParam param);
        Task<Subscription> UpdateStatus(int id, int status);
        //for admin
        //Task<APIResponseModel<SubscriptionModel>> GetSubscription(bool isEnglish,  int id );
    }
}
