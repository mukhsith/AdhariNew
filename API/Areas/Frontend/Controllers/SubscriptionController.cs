using API.Areas.Frontend.Factories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;
using Utility.Models.Frontend.CustomizedModel;
using Utility.Models.Frontend.Sales;
using Utility.Models.Frontend.Shop;
using Utility.ResponseMapper;

namespace API.Areas.Frontend.Controllers
{
    public class SubscriptionController : BaseController
    {
        private readonly ISubscriptionModelFactory _subscriptionModelFactory;
        public SubscriptionController(IOptions<AppSettingsModel> options,
            ISubscriptionModelFactory subscriptionModelFactory) : base(options)
        {
            _subscriptionModelFactory = subscriptionModelFactory;
        }

        /// <summary>
        /// Get subscription summary
        /// </summary>
        [HttpGet, Route("/webapi/subscription/getsubscriptionsummary")]
        [Authorize]
        public async Task<APIResponseModel<SubscriptionSummaryModel>> GetSubscriptionSummary()
        {
            return await _subscriptionModelFactory.PrepareSubscriptionSummaryModel(isEnglish: isEnglish, customerId: LoggedInCustomerId);
        }

        /// <summary>
        /// Validate subscription
        /// </summary>
        [HttpGet, Route("/webapi/subscription/validatesubscription")]
        public async Task<APIResponseModel<bool>> ValidateSubscription(int productId, int quantity)
        {
            return await _subscriptionModelFactory.ValidateSubscription(isEnglish: isEnglish, customerId: LoggedInCustomerId, productId: productId, quantity: quantity);
        }

        /// <summary>
        /// Save subscription attributes
        /// </summary>
        [HttpPost, Route("/webapi/subscription/savesubscriptionattributes")]
        public async Task<APIResponseModel<SubscriptionSummaryModel>> SaveSubscriptionAttributes([FromBody] SubscriptionAttributeModel subscriptionAttributeModel, bool app = true)
        {
            subscriptionAttributeModel.CustomerId = LoggedInCustomerId;
            return await _subscriptionModelFactory.SaveSubscriptionAttribute(isEnglish: isEnglish, subscriptionAttributeModel: subscriptionAttributeModel, app: app);
        }

        /// <summary>
        /// Get subscription checkout summary
        /// </summary>
        [HttpGet, Route("/webapi/subscription/getcheckoutsummary")]
        [Authorize]
        public async Task<APIResponseModel<SubscriptionCheckOutModel>> GetCheckOutSummary(bool app = true)
        {
            return await _subscriptionModelFactory.PrepareSubscriptionCheckOutModel(isEnglish: isEnglish, customerId: LoggedInCustomerId, app: app);
        }

        /// <summary>
        /// Create subscription 
        /// </summary>
        [HttpPost, Route("/webapi/subscription/createsubscription")]
        public async Task<APIResponseModel<CreatePaymentModel>> CreateSubscription([FromBody] CreatePaymentModel createPaymentModel)
        {
            return await _subscriptionModelFactory.CreateSubscription(isEnglish: isEnglish, customerId: LoggedInCustomerId, deviceTypeId: HeaderDeviceTypeId, createPaymentModel: createPaymentModel);
        }

        /// <summary>
        /// To get subscriptions
        /// </summary>
        /// <returns>Subscriptions</returns>
        [HttpGet, Route("/webapi/subscription/subscriptions")]
        public async Task<APIResponseModel<List<SubscriptionModel>>> GetSubscriptions(int id = 0, string subscriptionNumber = "", int limit = 0, int page = 0,
            SubscriptionStatus? subscriptionStatus = null)
        {
            return await _subscriptionModelFactory.GetSubscriptions(isEnglish: isEnglish, customerId: LoggedInCustomerId, id: id, subscriptionNumber: subscriptionNumber,
                limit: limit, page: page, subscriptionStatus: subscriptionStatus);
        }

        /// <summary>
        /// To get subscriptions
        /// </summary>
        /// <returns>Subscriptions</returns>
        [HttpGet, Route("/webapi/subscription/subscriptionsadmin")]
        public async Task<APIResponseModel<List<SubscriptionAdminModel>>> GetSubscriptionsAdmin(int id = 0, string subscriptionNumber = "", int limit = 0, int page = 0,
            SubscriptionStatus? subscriptionStatus = null)
        {
            return await _subscriptionModelFactory.GetSubscriptionsAdmin(isEnglish: isEnglish, id: id, subscriptionNumber: subscriptionNumber,
                limit: limit, page: page, subscriptionStatus: subscriptionStatus);
        }

        /// <summary>
        /// Create subscription order
        /// </summary>
        [HttpGet, Route("/webapi/subscription/createsubscriptionorders")]
        public async Task<APIResponseModel<object>> CreateSubscriptionOrders()
        {
            return await _subscriptionModelFactory.CreateSubscriptionOrders(isEnglish: isEnglish, apiKey: ServiceAPIKey);
        }

        /// <summary>
        /// To get subscription in pdf
        /// </summary>
        /// <returns>Order pdf</returns>
        [HttpGet, Route("/webapi/subscription/getsubscriptionpdf")]
        [Authorize]
        public async Task<APIResponseModel<object>> GetSubscriptionPdf(int id)
        {
            return await _subscriptionModelFactory.GetSubscriptionPdf(isEnglish: isEnglish, customerId: LoggedInCustomerId, id: id);
        }
    }
}
