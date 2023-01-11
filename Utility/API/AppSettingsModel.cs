namespace Utility.API
{
    public partial class AppSettingsModel
    {
        public string APIKey { get; set; }
        public string ServiceAPIKey { get; set; }
        public string SecurityKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string APIBaseUrl { get; set; }
        public string APIBaseUrl2 { get; set; }
        public string WebsiteUrl { get; set; }
        public string QuickPaymentWebsiteUrl { get; set; }
        public string PaymentAPIUrl { get; set; }
        public string PaymentAPIAccessToken { get; set; }
        public double TokenTimeout { get; set; }
        public int OTPValidMinutes { get; set; }
        public bool EnableAuthorization { get; set; }
        public string CorsAllowedUrls { get; set; }
        public string DefaultLang { get; set; }
        public double CustomerCookieTimeout { get; set; }
        public int MaximumSubscriptionQuantityToPurchase { get; set; }

        //Image Banner url 
        public string ImageBanner { get; set; }
        public string ImageBannerResized { get; set; }
        public string ImageBannerDefault { get; set; }
        public string ImageBannerSize { get; set; }

        public string ImageAboutUs { get; set; }
        public string ImageAboutUsResized { get; set; }
        public string ImageAboutUsDefault { get; set; }
        public string ImageAboutUsSize { get; set; }

        public string ImageSiteContent { get; set; }
        public string ImageSiteContentResized { get; set; }
        public string ImageSiteContentDefault { get; set; }
        public string ImageSiteContentSize { get; set; }


        public string ImageCategory { get; set; }
        public string ImageCategoryResized { get; set; }
        public string ImageCategoryDefault { get; set; }
        public string ImageCategorySize { get; set; }

        public string ImageProduct { get; set; }
        public string ImageProductResized { get; set; }
        public string ImageProductDefault { get; set; }
        public string ImageProductSize { get; set; }

        public string ImagePaymentMethod { get; set; }
        public string ImagePaymentMethodResized { get; set; }
        public string ImagePaymentMethodDefault { get; set; }
        public string ImagePaymentMethodSize { get; set; }

        public string ImageCommon { get; set; }
        public string ImageCommonResized { get; set; }
        public string ImageCommonDefault { get; set; }
        public string ImageCommonSize { get; set; }

        //aramex
        public string AramexAccountCountryCode { get; set; }
        public string AramexAccountEntity { get; set; }
        public string AramexAccountNumber { get; set; }
        public string AramexAccountPin { get; set; }
        public string AramexUserName { get; set; }
        public string AramexPassword { get; set; }
        public string AramexVersion { get; set; }
        public int AramexSource { get; set; }
        public bool AramexRateCalculatorEnabled { get; set; }
        public bool AramexCreateShippingEnabled { get; set; }
        public string AramexRateCalculatorUrl { get; set; }
        public string AramexCreateShippingUrl { get; set; }
        public string AramexClientCity { get; set; }
        public string AramexClientAddressLine1 { get; set; }
        public string AramexClientCountryCode { get; set; }
        public string AramexClientName { get; set; }
        public string AramexClientCompanyName { get; set; }
        public string AramexClientCellPhone { get; set; }
        public string AramexClientPhoneNumber1 { get; set; }
        public string AramexShipmentDetailsProductGroup { get; set; }
        public string AramexShipmentDetailsProductType { get; set; }
        public string AramexShipmentDetailsPaymentType { get; set; }
        public string AramexShipmentDetailsDescriptionOfGoods { get; set; }
        public string AramexWeightUnit { get; set; }

        //my fatoorah
        public string MyFatoorahToken { get; set; }
        public string MyFatoorahUrl { get; set; }
        public string MyFatoorahCallBackUrl { get; set; }
        public string MyFatoorahErrorUrl { get; set; }

        //Email Configuration
        public string DefaultOTPEmailIds { get; set; }
        public string EmailFromAddress { get; set; }
        public string EmailSMTP { get; set; }
        public int EmailPortNo { get; set; }
        public string EmailPassword { get; set; }
        public string EmailDisplayName { get; set; }
        public bool EmailSSLEnabled { get; set; }
        public bool EmailUseDefaultCredentials { get; set; }
        public string DefaultOTPMobileNumbers { get; set; }
        public string DefaultOTPValue { get; set; } 
        public int DefaultCountryId { get; set; }
        public decimal KNETFee { get; set; }

        //Master Card
        public bool MasterCardEnabled { get; set; }
        public string MasterCardUrl { get; set; }
        public string MasterCardOperation { get; set; }
        public string MasterCardPassword { get; set; }
        public string MasterCardUsername { get; set; }
        public string MasterCardMerchant { get; set; }
        public string MasterCardMerchantName { get; set; }
        public string MasterCardMerchantAddressLine1 { get; set; }
        public string MasterCardMerchantAddressLine2 { get; set; }
        public string MasterCardInteractionOperation { get; set; }
        public string MasterCardInteractionReturnUrl { get; set; }
        public string MasterCardInteractionRequestUrl { get; set; }
        public string MasterCardCurrency { get; set; }
        public string MasterCardVersion { get; set; }
        public decimal MasterCardFee { get; set; }

        //Tabby
        public string TabbyBaseUrl { get; set; }
        public string TabbyMerchantCode { get; set; }
        public string TabbyPublicKey { get; set; }
        public string TabbySecretKey { get; set; }
        public string TabbyDefaultCurrency { get; set; }
        public string TabbySuccessUrl { get; set; }
        public string TabbyCancelUrl { get; set; }
        public string TabbyFailureUrl { get; set; }

        //Push notification
        public string PushBaseUrl { get; set; }
        public string PushAuthorizationKey { get; set; }
        public string PushSenderKey { get; set; }
    }
}
