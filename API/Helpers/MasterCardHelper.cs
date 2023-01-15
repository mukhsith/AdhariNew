using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Utility.API;
using Utility.Helpers;
using Utility.Models.MasterCard;

namespace API.Helpers
{
    public class MasterCardHelper : IMasterCardHelper
    {
        private readonly AppSettingsModel _appSettings;
        public MasterCardHelper(IOptions<AppSettingsModel> options)
        {
            _appSettings = options.Value;
        }
        public async Task<Tuple<string, string>> CreateRequest(decimal amount, string orderNumber, string requestType, int entityId, string description)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = _appSettings.MasterCardUrl + "api/nvp/version/" + _appSettings.MasterCardVersion;
                    client.BaseAddress = new Uri(url);

                    var content = new FormUrlEncodedContent(new[]
                        {
                            new KeyValuePair<string, string>("apiOperation", _appSettings.MasterCardOperation),
                            new KeyValuePair<string, string>("apiPassword",_appSettings.MasterCardPassword),
                            new KeyValuePair<string, string>("apiUsername", _appSettings.MasterCardUsername),

                            new KeyValuePair<string, string>("merchant", _appSettings.MasterCardMerchant),

                            new KeyValuePair<string, string>("order.id", orderNumber),
                            new KeyValuePair<string, string>("order.amount", string.Format("{0:0.000}", amount)),
                            new KeyValuePair<string, string>("order.currency", _appSettings.MasterCardCurrency),
                            new KeyValuePair<string, string>("order.description", description),
                            new KeyValuePair<string, string>("order.reference", orderNumber),

                            new KeyValuePair<string, string>("interaction.operation", _appSettings.MasterCardInteractionOperation),
                            new KeyValuePair<string, string>("interaction.returnUrl", _appSettings.MasterCardInteractionReturnUrl + orderNumber + "/" + requestType + "/" + entityId),

                            new KeyValuePair<string, string>("interaction.merchant.name", _appSettings.MasterCardMerchantName),
                            new KeyValuePair<string, string>("interaction.merchant.address.line1", _appSettings.MasterCardMerchantAddressLine1),
                            new KeyValuePair<string, string>("interaction.merchant.address.line2", _appSettings.MasterCardMerchantAddressLine2),

                            new KeyValuePair<string, string>("billing.address.street", "Kuwait City"),
                            new KeyValuePair<string, string>("billing.address.city", "Kuwait City"),
                            new KeyValuePair<string, string>("billing.address.postcodeZip", "1111"),
                            new KeyValuePair<string, string>("billing.address.stateProvince", "Kuwait City"),
                            new KeyValuePair<string, string>("billing.address.country", "KWT"),

                            new KeyValuePair<string, string>("interaction.displayControl.billingAddress", "HIDE"),
                            new KeyValuePair<string, string>("interaction.displayControl.customerEmail", "HIDE"),
                            new KeyValuePair<string, string>("interaction.displayControl.shipping", "HIDE"),
                            new KeyValuePair<string, string>("transaction.reference", "TR" + orderNumber)

                });

                    string logText = "";
                    logText = logText + "apiOperation:" + _appSettings.MasterCardOperation;
                    logText = logText + "," + "apiPassword:" + _appSettings.MasterCardPassword;
                    logText = logText + "," + "apiUsername:" + _appSettings.MasterCardUsername;
                    logText = logText + "," + "merchant:" + _appSettings.MasterCardMerchant;
                    logText = logText + "," + "order.id:" + orderNumber;
                    logText = logText + "," + "order.amount:" + string.Format("{0:0.000}", amount);
                    logText = logText + "," + "order.currency:" + _appSettings.MasterCardCurrency;
                    logText = logText + "," + "order.description:" + description;
                    logText = logText + "," + "order.reference:" + orderNumber;
                    logText = logText + "," + "interaction.operation:" + _appSettings.MasterCardInteractionOperation;
                    logText = logText + "," + "interaction.returnUrl:" + _appSettings.MasterCardInteractionReturnUrl + orderNumber + "/" + requestType + "/" + entityId;
                    logText = logText + "," + "interaction.merchant.name:" + _appSettings.MasterCardMerchantName;
                    logText = logText + "," + "interaction.merchant.address.line1:" + _appSettings.MasterCardMerchantAddressLine1;
                    logText = logText + "," + "interaction.merchant.address.line2:" + _appSettings.MasterCardMerchantAddressLine2;
                    logText = logText + "," + "billing.address.street:" + "Kuwait City";
                    logText = logText + "," + "billing.address.city:" + "Kuwait City";
                    logText = logText + "," + "billing.address.postcodeZip:" + "1111";
                    logText = logText + "," + "billing.address.stateProvince:" + "Kuwait City";
                    logText = logText + "," + "billing.address.country:" + "KWT";
                    logText = logText + "," + "interaction.displayControl.billingAddress:" + "HIDE";
                    logText = logText + "," + "interaction.displayControl.customerEmail:" + "HIDE";
                    logText = logText + "," + "interaction.displayControl.shipping:" + "HIDE";
                    logText = logText + "," + "transaction.reference:" + "TR" + orderNumber;
                    Common.SaveMasterCardRequestResponseLog(logText);

                    var response = client.PostAsync($"{url}", content);
                    response.Wait();
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var contents = await response.Result.Content.ReadAsStringAsync();
                        var parsed = HttpUtility.ParseQueryString(contents);
                        var sessionId = parsed["session.id"];
                        var successIndicator = parsed["successIndicator"];
                        return new Tuple<string, string>(sessionId, successIndicator);
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }
        public MasterCardRootModel GetResult(string orderId)
        {
            try
            {
                string url = _appSettings.MasterCardUrl + "api/rest/version/" + _appSettings.MasterCardVersion + "/merchant/" + _appSettings.MasterCardMerchant + "/order/" + orderId;

                System.Threading.Thread.Sleep(10);
                using (var client = new HttpClient())
                {
                    var byteArray = Encoding.ASCII.GetBytes(_appSettings.MasterCardUsername + ":" + _appSettings.MasterCardPassword);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    var contents = response.Content.ReadAsStringAsync().Result;

                    MasterCardRootModel masterCardRootModel = JsonConvert.DeserializeObject<MasterCardRootModel>(contents);
                    return masterCardRootModel;
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }
    }
}
