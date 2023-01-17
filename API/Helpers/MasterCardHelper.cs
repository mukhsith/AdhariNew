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
        public async Task<Tuple<string, string>> CreateRequest2(decimal amount, string orderNumber, string requestType, int entityId, string description)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var byteArray = Encoding.ASCII.GetBytes(_appSettings.MasterCardUsername + ":" + _appSettings.MasterCardPassword);
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                    string url = _appSettings.MasterCardUrl + "api/rest/version/" + _appSettings.MasterCardVersion + "/merchant/TEST900233001/order/" + orderNumber + "/transaction/989566565";

                    var root = new MasterRoot
                    {
                        apiOperation = "AUTHORIZE",
                        order = new MasterOrder
                        {
                            currency = "KWD",
                            amount = "10.10",
                            walletProvider = "APPLE_PAY"
                        },
                        sourceOfFunds = new MasterSourceOfFunds
                        {
                            type = "CARD",
                            provided = new MasterProvided
                            {
                                card = new MasterCard
                                {
                                    devicePayment = new MasterDevicePayment
                                    {
                                        paymentToken = "{\"version\":\"EC_v1\",\"data\":\"+zzZwL2wEMyBz5yewNXxbi8H4OWyi0gwSLKPQbv9MwUFaxL7GF70gcImyvLbAv5ToHEj2FuS5EPnl7L6jL+nu5Ud67FY8XxxhT3ytb9ydF20ikYDz/Clhw9ZNR3uSnNE9K7XtYdbw/LQJjHSlfmjycZlVoYr1seJRMziVfok+Fxw0XObZ0TpZo37xmCZCs+w4sUsFfexfsJBgGXJBDP1iBRp/EFOFNNVaHEF7AQM9BXxozkmZveNf84xH0lEdgXPnvGqc70qTg9xxC3Fr9sQXZGEOXxuWRlIxh7HkT0PX1pmhCxpM2l1SI0YqF4CdQNht+u0dCWh1oUb9Fk395oPcaAFxYU0s7VPUegvymRqyTEPbQsuUQ4hbWwqOopCjSQKq+k+kEQGaoRxp/0YkXXUHhXt/buf/sHrml5Io45p53t6\",\"signature\":\"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\",\"header\":{\"ephemeralPublicKey\":\"MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEQRx3i7ifZ1dYPIPiLhaHxgd7psP4ebbq2HI/PsIPzbsFjMxIYDJ4PNzZXOlaKIFh5CTO/qg8bhyRkmZHUTPLrw==\",\"publicKeyHash\":\"7rWxL0PevFKZTd+cFIc2GF4PB9i+Xdgw/ZAudOPwZyk=\",\"transactionId\":\"b903dbe4f21a72234e3945ddc822e65289f84c4044a54661fa4989f88fc0a86f\"}}"
                                    }
                                }
                            }
                        },
                        transaction = new MasterTransaction
                        {
                            source = "INTERNET"
                        }
                    };

                    StringContent content = new(JsonConvert.SerializeObject(root), Encoding.UTF8, "application/json");
                    var ss = JsonConvert.SerializeObject(root);
                    using var response = await httpClient.PutAsync(url, content);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        var settings = new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                        };
                    }
                    else
                    {
                        
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




    public class MasterCard
    {
        public MasterDevicePayment devicePayment { get; set; }
    }

    public class MasterDevicePayment
    {
        public string paymentToken { get; set; }
    }

    public class MasterOrder
    {
        public string currency { get; set; }
        public string amount { get; set; }
        public string walletProvider { get; set; }
    }

    public class MasterProvided
    {
        public MasterCard card { get; set; }
    }

    public class MasterRoot
    {
        public string apiOperation { get; set; }
        public MasterOrder order { get; set; }
        public MasterSourceOfFunds sourceOfFunds { get; set; }
        public MasterTransaction transaction { get; set; }
    }

    public class MasterSourceOfFunds
    {
        public string type { get; set; }
        public MasterProvided provided { get; set; }
    }

    public class MasterTransaction
    {
        public string source { get; set; }
    }
}
