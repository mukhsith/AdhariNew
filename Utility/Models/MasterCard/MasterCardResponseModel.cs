using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Utility.Models.MasterCard
{
    public class MasterCard3ds
    {
        public string acsEci { get; set; }
        public string authenticationToken { get; set; }
        public string transactionId { get; set; }
    }
    public class MasterCard3ds1
    {
        public string paResStatus { get; set; }
        public string veResEnrolled { get; set; }
    }
    public class MasterCardAcquirer
    {
        public string merchantId { get; set; }
        public string id { get; set; }
        public int? batch { get; set; }
        public string date { get; set; }
        public string settlementDate { get; set; }
        public string timeZone { get; set; }
    }
    public class MasterCardAddress
    {
        public string city { get; set; }
        public string country { get; set; }
        public string postcodeZip { get; set; }
        public string stateProvince { get; set; }
        public string street { get; set; }
    }
    public class MasterCardAuthentication
    {
        [JsonProperty(PropertyName = "3ds")]
        public MasterCard3ds _3ds { get; set; }

        [JsonProperty(PropertyName = "3ds1")]
        public MasterCard3ds1 _3ds1 { get; set; }
        public string acceptVersions { get; set; }
        public string channel { get; set; }
        public string payerInteraction { get; set; }
        public string purpose { get; set; }
        public MasterCardRedirect redirect { get; set; }
        public string version { get; set; }
        public string transactionId { get; set; }
    }
    public class MasterCardAuthorizationResponse
    {
        public string avsCode { get; set; }
        public string cardSecurityCodeError { get; set; }
        public string commercialCard { get; set; }
        public string commercialCardIndicator { get; set; }
        public string date { get; set; }
        public string posData { get; set; }
        public string posEntryMode { get; set; }
        public string processingCode { get; set; }
        public string responseCode { get; set; }
        public string returnAci { get; set; }
        public string stan { get; set; }
        public string time { get; set; }
    }
    public class MasterCardAvs
    {
        public string acquirerCode { get; set; }
        public string gatewayCode { get; set; }
    }
    public class MasterCardBilling
    {
        public MasterCardAddress address { get; set; }
    }
    public class MasterCardCard
    {
        public string brand { get; set; }
        public MasterCardExpiry expiry { get; set; }
        public string fundingMethod { get; set; }
        public string nameOnCard { get; set; }
        public string number { get; set; }
        public string scheme { get; set; }
        public string storedOnFile { get; set; }




       
        public MasterCardDevicePayment devicePayment { get; set; }
        public MasterCardDeviceSpecificExpiry deviceSpecificExpiry { get; set; }
        public string deviceSpecificNumber { get; set; }
        public string encryption { get; set; }
    }
    public class MasterCardCardholderVerification
    {
        public MasterCardAvs avs { get; set; }
    }
    public class MasterCardCardSecurityCode
    {
        public string acquirerCode { get; set; }
        public string gatewayCode { get; set; }
    }
    public class MasterCardChargeback
    {
        public int amount { get; set; }
        public string currency { get; set; }
    }
    public class MasterCardCustomer
    {
        public string firstName { get; set; }
    }
    public class MasterCardDevice
    {
        public string browser { get; set; }
        public string ipAddress { get; set; }
    }
    public class MasterCardExpiry
    {
        public string month { get; set; }
        public string year { get; set; }
    }
    public class MasterCardOrder
    {
        public double amount { get; set; }
        public string authenticationStatus { get; set; }
        public MasterCardChargeback chargeback { get; set; }
        public DateTime creationTime { get; set; }
        public string currency { get; set; }
        public string id { get; set; }
        public DateTime lastUpdatedTime { get; set; }
        public double merchantAmount { get; set; }
        public string merchantCategoryCode { get; set; }
        public string merchantCurrency { get; set; }
        public string reference { get; set; }
        public string customerReference { get; set; }
        public string status { get; set; }
        public double totalAuthorizedAmount { get; set; }
        public double totalCapturedAmount { get; set; }
        public double totalDisbursedAmount { get; set; }
        public double totalRefundedAmount { get; set; }
        public MasterCardValueTransfer valueTransfer { get; set; }
        public string description { get; set; }
        public string certainty { get; set; }
        public string walletProvider { get; set; }
    }
    public class MasterCardProvided
    {
        public MasterCardCard card { get; set; }
    }
    public class MasterCardRedirect
    {
        public string domainName { get; set; }
    }
    public class MasterCardResponse
    {
        public string gatewayCode { get; set; }
        public string gatewayRecommendation { get; set; }
        public string acquirerCode { get; set; }
        public MasterCardCardSecurityCode cardSecurityCode { get; set; }
        public MasterCardCardholderVerification cardholderVerification { get; set; }
        public string acquirerMessage { get; set; }
    }
    public class MasterCardRootModel
    {
        [JsonProperty(PropertyName = "3dsAcsEci")]
        public string _3dsAcsEci { get; set; }
        public double amount { get; set; }
        public MasterCardAuthentication authentication { get; set; }
        public string authenticationStatus { get; set; }
        public string authenticationVersion { get; set; }
        public MasterCardBilling billing { get; set; }
        public MasterCardChargeback chargeback { get; set; }
        public DateTime creationTime { get; set; }
        public string currency { get; set; }
        public MasterCardCustomer customer { get; set; }
        public string description { get; set; }
        public MasterCardDevice device { get; set; }
        public string id { get; set; }
        public DateTime lastUpdatedTime { get; set; }
        public string merchant { get; set; }
        public double merchantAmount { get; set; }
        public string merchantCategoryCode { get; set; }
        public string merchantCurrency { get; set; }
        public string reference { get; set; }
        public string result { get; set; }
        public MasterCardSourceOfFunds sourceOfFunds { get; set; }
        public string status { get; set; }
        public double totalAuthorizedAmount { get; set; }
        public double totalCapturedAmount { get; set; }
        public double totalDisbursedAmount { get; set; }
        public double totalRefundedAmount { get; set; }
        public List<MasterCardTransaction> transaction { get; set; }
    }
    public class MasterCardSourceOfFunds
    {
        public MasterCardProvided provided { get; set; }
        public string type { get; set; }
    }
    public class MasterCardTransaction
    {
        public MasterCardAuthentication authentication { get; set; }
        public MasterCardBilling billing { get; set; }
        public MasterCardCustomer customer { get; set; }
        public MasterCardDevice device { get; set; }
        public string merchant { get; set; }
        public MasterCardOrder order { get; set; }
        public MasterCardResponse response { get; set; }
        public string result { get; set; }
        public MasterCardSourceOfFunds sourceOfFunds { get; set; }
        public DateTime timeOfLastUpdate { get; set; }
        public DateTime timeOfRecord { get; set; }
        public MasterCardTransaction transaction { get; set; }
        public string version { get; set; }
        public string gatewayEntryPoint { get; set; }
        public MasterCardAuthorizationResponse authorizationResponse { get; set; }
        public MasterCardAcquirer acquirer { get; set; }
        public double amount { get; set; }
        public string authenticationStatus { get; set; }
        public string currency { get; set; }
        public string id { get; set; }
        public string stan { get; set; }
        public string type { get; set; }
        public string receipt { get; set; }
        public string source { get; set; }
        public string terminal { get; set; }
        public string authorizationCode { get; set; }
    }
    public class MasterCardValueTransfer
    {
        public string accountType { get; set; }
    }
    public class MasterCardDevicePayment
    {
        public string cryptogramFormat { get; set; }
    }
    public class MasterCardDeviceSpecificExpiry
    {
        public string month { get; set; }
        public string year { get; set; }
    }
}
