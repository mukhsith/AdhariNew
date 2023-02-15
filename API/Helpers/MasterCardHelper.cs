using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
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
        public async Task<MasterCardTransaction> CreateApplepayRequest(decimal amount, string orderNumber, PaymentTokenModel paymentTokenModel)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var byteArray = Encoding.ASCII.GetBytes(_appSettings.MasterCardUsername + ":" + _appSettings.MasterCardPassword);
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                    string url = _appSettings.MasterCardUrl + "api/rest/version/" + _appSettings.MasterCardVersion + "/merchant/" + _appSettings.MasterCardMerchant + "/order/" + orderNumber + "/transaction/" + ("TR" + orderNumber);

                    var paymentToken = JsonConvert.SerializeObject(paymentTokenModel);

                    var root = new MasterRoot
                    {
                        apiOperation = "PAY",
                        order = new MasterOrder
                        {
                            currency = _appSettings.MasterCardCurrency,
                            amount = amount.ToString(),
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
                                        paymentToken = paymentToken
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
                    if (response.StatusCode.ToString() == "Created")
                    {
                        string contents = await response.Content.ReadAsStringAsync();

                        MasterCardTransaction masterCardTransaction = JsonConvert.DeserializeObject<MasterCardTransaction>(contents);
                        return masterCardTransaction;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }
        public async Task<bool> ValidateApplepayMerchant(string requestUrl)
        {
            try
            {
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Pdfs");
                var certificate = new X509Certificate2(filepath + "\\" + "apple_pay.cer");

                var extension = certificate.Extensions["1.2.840.113635.100.6.32"];
                var merchantId = Encoding.ASCII.GetString(extension.RawData).Substring(2);

                var payload = new
                {
                    merchantIdentifier = merchantId,
                    displayName = "Adhari",
                    domainName = "https://adhari.mpp.com.kw"
                };

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(AcceptAllCertifications);
                //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });

                var handler = new HttpClientHandler();
                handler.ClientCertificates.Add(certificate);
                handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                var httpClient = new HttpClient(handler, disposeHandler: true);
                var jsonPayload = JsonConvert.SerializeObject(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(requestUrl, content);
                //response.EnsureSuccessStatusCode();

                var merchantSessionJson = await response.Content.ReadAsStringAsync();
                var merchantSession = JObject.Parse(merchantSessionJson);
                //return Json(merchantSession);
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
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
        public bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
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
