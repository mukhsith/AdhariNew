using API.Areas.Frontend.Helpers;
using Data.Sales;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;
using Utility.Models.Tabby;

namespace API.Helpers
{
    public class TabbyHelper : ITabbyHelper
    {
        private readonly ILogger _logger;
        private readonly AppSettingsModel _appSettings;
        private readonly IModelHelper _modelHelper;
        public TabbyHelper(ILoggerFactory logger,
            IOptions<AppSettingsModel> options,
            IModelHelper modelHelper)
        {
            _logger = logger.CreateLogger(typeof(TabbyHelper).Name);
            _appSettings = options.Value;
            _modelHelper = modelHelper;
        }
        public async Task<RootModel> PrepareOrderRootModel(Order order)
        {
            RootModel rootModel = new();

            try
            {
                var addressModel = await _modelHelper.PrepareAddressModel(order.Address, order.CustomerLanguageId == 1);
                if (addressModel == null)
                {
                    return null;
                }

                rootModel.lang = order.CustomerLanguageId == 1 ? "en" : "ar";
                rootModel.merchant_code = _appSettings.TabbyMerchantCode;
                rootModel.merchant_urls = new MerchantUrlsModel
                {
                    success = _appSettings.TabbySuccessUrl,
                    cancel = _appSettings.TabbyCancelUrl,
                    failure = _appSettings.TabbyFailureUrl,
                };

                List<ItemModel> itemModels = new();
                foreach (var OrderItem in order.OrderItems)
                {
                    ItemModel itemModel = new()
                    {
                        title = OrderItem.Product.NameEn,
                        quantity = OrderItem.Quantity,
                        unit_price = OrderItem.UnitPrice.ToString(),
                        category = "Water"
                    };

                    itemModels.Add(itemModel);
                }

                PaymentModel paymentModel = new()
                {
                    amount = order.Total.ToString("N2"),
                    currency = _appSettings.TabbyDefaultCurrency,
                    buyer = new BuyerModel
                    {
                        phone = "500000001",
                        email = "successful.payment@tabby.ai",
                        name = "Mukhsith"
                    },
                    shipping_address = new ShippingAddressModel
                    {
                        city = addressModel.GovernorateName,
                        address = addressModel.AddressText,
                        zip = "40001"
                    },
                    order = new OrderModel
                    {
                        reference_id = PaymentRequestType.Order + "-" + order.Id,
                        items = itemModels,
                        updated_at = DateTime.Now
                    }
                    //,
                    //buyer_history = new BuyerHistoryModel
                    //{
                    //    registered_since = DateTime.Now,
                    //    loyalty_level = 0
                    //}
                };
                rootModel.payment = paymentModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                rootModel = null;
            }

            return rootModel;
        }
        public async Task<RootModel> PrepareSubscriptionRootModel(Subscription subscription)
        {
            RootModel rootModel = new();

            try
            {
                var addressModel = await _modelHelper.PrepareAddressModel(subscription.Address, subscription.CustomerLanguageId == 1);
                if (addressModel == null)
                {
                    return null;
                }

                var subscriptionOrder = subscription.SubscriptionOrders.Where(a => a.FirstOrder).FirstOrDefault();
                if (subscriptionOrder == null)
                {
                    return null;
                }

                rootModel.lang = subscription.CustomerLanguageId == 1 ? "en" : "ar";
                rootModel.merchant_code = _appSettings.TabbyMerchantCode;
                rootModel.merchant_urls = new MerchantUrlsModel
                {
                    success = _appSettings.TabbySuccessUrl,
                    cancel = _appSettings.TabbyCancelUrl,
                    failure = _appSettings.TabbyFailureUrl,
                };

                List<ItemModel> itemModels = new();
                ItemModel itemModel = new()
                {
                    title = subscription.Product.NameEn,
                    description = subscription.Product.DescriptionEn,
                    quantity = subscription.Quantity,
                    unit_price = subscription.UnitPrice.ToString(),
                    category = "Water"
                };

                itemModels.Add(itemModel);

                PaymentModel paymentModel = new()
                {
                    amount = subscription.Total.ToString("N2"),
                    currency = _appSettings.TabbyDefaultCurrency,
                    buyer = new BuyerModel
                    {
                        phone = "500000001",
                        email = "successful.payment@tabby.ai",
                        name = "Mukhsith"
                    },
                    shipping_address = new ShippingAddressModel
                    {
                        city = addressModel.GovernorateName,
                        address = addressModel.AddressText,
                        zip = "40001"
                    },
                    order = new OrderModel
                    {
                        reference_id = PaymentRequestType.SubscriptionOrder + "-" + subscriptionOrder.Id,
                        items = itemModels,
                        updated_at = DateTime.Now
                    }
                    //,
                    //buyer_history = new BuyerHistoryModel
                    //{
                    //    registered_since = DateTime.Now,
                    //    loyalty_level = 0
                    //}
                };
                rootModel.payment = paymentModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                rootModel = null;
            }

            return rootModel;
        }
        public async Task<RootModel> CreateSession(RootModel rootModel)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _appSettings.TabbyPublicKey);
                    StringContent content = new(JsonConvert.SerializeObject(rootModel), Encoding.UTF8, "application/json");
                    var ss = JsonConvert.SerializeObject(rootModel);
                    using var response = await httpClient.PostAsync(_appSettings.TabbyBaseUrl + "checkout", content);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        var settings = new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                        };

                        rootModel = JsonConvert.DeserializeObject<RootModel>(apiResponse, settings);
                    }
                    else
                    {
                        rootModel = null;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                rootModel = null;
            }

            return rootModel;
        }
        public async Task<PaymentModel> GetPayment(string id)
        {
            PaymentModel paymentModel = null;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _appSettings.TabbySecretKey);

                    using var response = await httpClient.GetAsync(_appSettings.TabbyBaseUrl + "payments/" + id);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        var settings = new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                        };

                        paymentModel = JsonConvert.DeserializeObject<PaymentModel>(apiResponse, settings);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                paymentModel = null;
            }

            return paymentModel;
        }
        public async Task<PaymentModel> CapturePayment(PaymentCaptureModel paymentCaptureModel, string id)
        {
            PaymentModel paymentModel = new();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _appSettings.TabbySecretKey);
                    StringContent content = new(JsonConvert.SerializeObject(paymentCaptureModel), Encoding.UTF8, "application/json");

                    using var response = await httpClient.PostAsync(_appSettings.TabbyBaseUrl + "payments/" + id + "/captures", content);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        var settings = new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                        };

                        paymentModel = JsonConvert.DeserializeObject<PaymentModel>(apiResponse, settings);
                    }
                    else
                    {
                        paymentModel = null;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                paymentModel = null;
            }

            return paymentModel;
        }
    }
}
