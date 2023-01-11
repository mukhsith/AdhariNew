//using API.Areas.Frontend.Helpers;
using API.Areas.Backend.Helpers;
using API.Helpers;
using Data.Sales;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options; 
using Services.Backend.CustomerManagement; 
using Services.Backend.Sales; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using Utility.API;
using Utility.Enum;
using Utility.Helpers;
using Utility.Models.Admin.Sales;
using Utility.Models.Frontend.Sales;
using Utility.ResponseMapper;
namespace API.Areas.Backend.Factories
{
    
     
        public class SubscriptionModelFactory : ISubscriptionModelFactory
        {
            private readonly ILogger _logger;
            private readonly AppSettingsModel _appSettings;
            private readonly IModelHelper _modelHelper;

            private readonly ICommonHelper _commonHelper;
            private readonly ICustomerService _customerService; 
            private readonly ISubscriptionService _subscriptionService; 
            public SubscriptionModelFactory(ILoggerFactory logger,
                IOptions<AppSettingsModel> options,
                IModelHelper modelHelper,
                ICommonHelper  commonHelper,
                ICustomerService customerService,  
                ISubscriptionService subscriptionService  )
            {
                _logger = logger.CreateLogger(typeof(SubscriptionModelFactory).Name);
                _appSettings = options.Value;
                _modelHelper = modelHelper;
                _customerService = customerService;  
                _subscriptionService = subscriptionService;
                _commonHelper = commonHelper;
            }

        public async Task<APIResponseModel<List<SubscriptionModel>>> GetSubscriptions(bool isEnglish, int customerId, int id = 0, string subscriptionNumber = "",
            int limit = 0, int page = 0, SubscriptionStatus? subscriptionStatus = null)
        {
            var response = new APIResponseModel<List<SubscriptionModel>>();
            try
            {
                var subscriptions = new List<Subscription>();

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
                    var subscription = await _subscriptionService.GetSubscriptionById(id);
                    if (subscription != null && !subscription.Deleted && subscription.Customer != null)
                    {
                        if (subscription.CustomerId != customerId)
                        {
                            response.Message = isEnglish ? Messages.InvalidCustomer : MessagesAr.InvalidCustomer;
                            return response;
                        }

                        subscriptions.Add(subscription);
                    }
                }
                else if (!string.IsNullOrEmpty(subscriptionNumber))
                {
                    loadDetails = true;
                    var subscription = await _subscriptionService.GetSubscriptionBySubscriptionNumber(subscriptionNumber);
                    if (subscription != null && !subscription.Deleted)
                        subscriptions.Add(subscription);
                }
                else
                {
                    subscriptions = await _subscriptionService.GetAllSubscription(customerId: customerId, subscriptionStatus: subscriptionStatus);
                }

                subscriptions = subscriptions.OrderByDescending(a => a.Id).ToList();

                response.DataRecordCount = subscriptions.Count;

                if (limit > 0 && page > 0)
                {
                    subscriptions = subscriptions.Skip((page - 1) * limit).Take(limit).ToList();
                }

                var subscriptionModels = new List<SubscriptionModel>();
                foreach (var subscription in subscriptions)
                {
                    var subscriptionModel = await _modelHelper.PrepareSubscriptionModel(subscription, isEnglish, loadDetails: loadDetails);
                    subscriptionModels.Add(subscriptionModel);
                }

                response.Data = subscriptionModels;
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

        public async Task<dynamic> GetSubscriptionsForDataTable(AdminSubscriptionOrderParam param)
        {
            DataTableResult<List<AdminSubscriptionModel>> result = new() { Draw = param.DatatableParam.Draw };
            try
            {
                result = await _subscriptionService.GetSubscriptions(param);
                foreach (var item in result.Data)
                {
                    //item.Total = await _commonHelper.ConvertDecimalToString(item.Amount, param.IsEnglish, 1, true);
                    item.FormattedTotal = await _commonHelper.ConvertDecimalToString(item.Total, param.IsEnglish, 1, true);
                }

                return result;
            }
            catch (Exception exp)
            {
                _logger.LogError("OrderModelFactor:", exp.Message);
            }

            return result;
        }

        public async Task<Subscription> UpdateStatus(int id, int status)
        {
             Subscription  result = new();
            try
            {
                result = await _subscriptionService.UpdateStatus(id,(SubscriptionStatus)status);
                 
                return result;
            }
            catch (Exception exp)
            {
                _logger.LogError("OrderModelFactor: "+ typeof(SubscriptionModelFactory).Name + " UpdateStatus" , exp.Message);
            }

            return result;
        }

       
    }
}
 
