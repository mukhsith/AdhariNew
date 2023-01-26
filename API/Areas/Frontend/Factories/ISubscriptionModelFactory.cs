using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Enum;
using Utility.Models.Frontend.CustomizedModel;
using Utility.Models.Frontend.Sales;
using Utility.Models.Frontend.Shop;
using Utility.ResponseMapper;

namespace API.Areas.Frontend.Factories
{
    public interface ISubscriptionModelFactory
    {
        Task<APIResponseModel<SubscriptionSummaryModel>> PrepareSubscriptionSummaryModel(bool isEnglish, int customerId);
        Task<APIResponseModel<bool>> ValidateSubscription(bool isEnglish, int customerId,
            int productId, int quantity);
        Task<APIResponseModel<SubscriptionSummaryModel>> SaveSubscriptionAttribute(bool isEnglish, int customerId,
            SubscriptionAttributeModel subscriptionAttributeModel, bool app = true);
        Task<APIResponseModel<SubscriptionCheckOutModel>> PrepareSubscriptionCheckOutModel(bool isEnglish, int customerId, bool app = true);
        Task<APIResponseModel<CreatePaymentModel>> CreateSubscription(bool isEnglish, int customerId, DeviceType deviceTypeId,
            CreatePaymentModel createPaymentModel);
        Task<APIResponseModel<List<SubscriptionModel>>> GetSubscriptions(bool isEnglish, int customerId, int id = 0, string subscriptionNumber = "",
            int limit = 0, int page = 0, SubscriptionStatus? subscriptionStatus = null);
        Task<APIResponseModel<List<SubscriptionAdminModel>>> GetSubscriptionsAdmin(bool isEnglish, int id = 0, string subscriptionNumber = "",
           int limit = 0, int page = 0, SubscriptionStatus? subscriptionStatus = null);
        Task<APIResponseModel<object>> CreateSubscriptionOrders(bool isEnglish, string apiKey);
        Task<APIResponseModel<object>> GetSubscriptionPdf(bool isEnglish, int customerId, int id);
    }
}
