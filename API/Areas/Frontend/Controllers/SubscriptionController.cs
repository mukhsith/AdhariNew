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
        private static readonly object controllerLock = new object();
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
        [Authorize]
        public APIResponseModel<CreatePaymentModel> CreateSubscription([FromBody] CreatePaymentModel createPaymentModel)
        {
            APIResponseModel<CreatePaymentModel> response = new();
            lock (controllerLock)
            {
                response = _subscriptionModelFactory.CreateSubscription(isEnglish: isEnglish, customerId: LoggedInCustomerId, deviceTypeId: HeaderDeviceTypeId, createPaymentModel: createPaymentModel).Result;
            }
            return response;
        }

        /// <summary>
        /// To get subscriptions
        /// </summary>
        /// <returns>Subscriptions</returns>
        [HttpGet, Route("/webapi/subscription/subscriptions")]
        [Authorize]
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
        /// To get subscription in pdf
        /// </summary>
        /// <returns>Order pdf</returns>
        [HttpGet, Route("/webapi/subscription/getsubscriptionpdf")]
        [Authorize]
        public async Task<APIResponseModel<object>> GetSubscriptionPdf(int id)
        {
            return await _subscriptionModelFactory.GetSubscriptionPdf(isEnglish: isEnglish, customerId: LoggedInCustomerId, id: id);
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
        /// To get subscription in pdf for admin
        /// </summary>
        /// <returns>Order pdf for admin</returns>
        [HttpGet, Route("/webapi/subscription/getsubscriptionpdfadmin")]
        public async Task<APIResponseModel<object>> GetSubscriptionPdfAdmin(int id, int customerId, bool isEnglish = false)
        {
            return await _subscriptionModelFactory.GetSubscriptionPdf(isEnglish: isEnglish, customerId: customerId, id: id);
        }

        /// <summary>
        /// To get subscription by subscription number
        /// </summary>
        /// <returns>Subscriptions</returns>
        [HttpGet, Route("/webapi/subscription/subscriptionbysubscriptionnumber")]
        public async Task<APIResponseModel<List<SubscriptionModel>>> GetSubscriptionBySubscriptionNumber(string subscriptionNumber = "")
        {
            return await _subscriptionModelFactory.GetSubscriptionBySubscriptionNumber(isEnglish: isEnglish, subscriptionNumber: subscriptionNumber);
        }
    }
}
