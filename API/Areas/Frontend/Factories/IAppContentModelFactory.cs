﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Enum;
using Utility.Models.Frontend.Content;
using Utility.Models.Frontend.CouponPromotion;
using Utility.Models.Frontend.CustomizedModel;
using Utility.Models.Frontend.Locations;
using Utility.Models.Frontend.ProductManagement;
using Utility.Models.Frontend.Sales;
using Utility.ResponseMapper;

namespace API.Areas.Frontend.Factories
{
    public interface IAppContentModelFactory
    {
        Task<APIResponseModel<CompanySettingModel>> PrepareCompanySettingModel(bool isEnglish);
        Task<APIResponseModel<HomepageModel>> PrepareHomepageContent(bool isEnglish, int customerId = 0, string customerGuidValue = "");
        Task<APIResponseModel<SiteContentModel>> PrepareSiteContent(bool isEnglish, AppContentType appContentTypeId);
        Task<APIResponseModel<object>> CreateFeedbackRequest(bool isEnglish, CustomerFeedbackModel customerFeedback);
        Task<APIResponseModel<List<GovernorateModel>>> PrepareGovernorates(bool isEnglish, int? id = null);
        Task<APIResponseModel<List<AreaModel>>> PrepareAreas(bool isEnglish, int? id = null, int? governorateId = null);
        Task<APIResponseModel<List<PaymentMethodModel>>> PreparePaymentMethods(bool isEnglish, PaymentRequestType paymentRequestType, int customerId);
        Task<APIResponseModel<List<CategoryModel>>> PrepareCategories(bool isEnglish, int? id = null, string seoName = "");
        Task<APIResponseModel<List<WalletPackageModel>>> PrepareWalletPackages(bool isEnglish, int id = 0);
        Task<APIResponseModel<PageHeaderModel>> PreparePageHeader(bool isEnglish);
        Task<APIResponseModel<QuickPaymentModel>> PrepareQuickPaymentModel(bool isEnglish, string paymentNumber);
        Task<APIResponseModel<CreatePaymentModel>> CreateQuickpay(bool isEnglish, CreatePaymentModel createPaymentModel);
        Task<APIResponseModel<bool>> TestPush(bool isEnglish, string title, string message, string token);
    }
}
