﻿using API.Areas.Frontend.Factories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;
using Utility.Models.Frontend.Content;
using Utility.Models.Frontend.CouponPromotion;
using Utility.Models.Frontend.CustomizedModel;
using Utility.Models.Frontend.Locations;
using Utility.Models.Frontend.ProductManagement;
using Utility.Models.Frontend.Sales;
using Utility.ResponseMapper;

namespace API.Areas.Frontend.Controllers
{
    public class CommonController : BaseController
    {
        private readonly IAppContentModelFactory _appContentModelFactory;
        public CommonController(IOptions<AppSettingsModel> options,
            IAppContentModelFactory appContentModelFactory) : base(options)
        {
            _appContentModelFactory = appContentModelFactory;
        }

        /// <summary>
        /// To get company setting
        /// </summary>
        /// <returns>Company setting</returns>
        [HttpGet, Route("/webapi/common/companysetting")]
        public async Task<APIResponseModel<CompanySettingModel>> GetCompanySetting()
        {
            return await _appContentModelFactory.PrepareCompanySettingModel(isEnglish: isEnglish);
        }

        /// <summary>
        /// To get homepage contents
        /// </summary>
        /// <returns>Homepage contents</returns>
        [HttpGet, Route("/webapi/common/homepagecontents")]
        public async Task<APIResponseModel<HomepageModel>> GetHomepageContents(string customerGuidValue = "")
        {
            return await _appContentModelFactory.PrepareHomepageContent(isEnglish: isEnglish, customerId: LoggedInCustomerId, customerGuidValue: customerGuidValue);
        }

        /// <summary>
        /// GET working
        /// </summary>
        /// <param name="appContentTypeId"></param>
        /// <returns></returns>
        [HttpGet, Route("/webapi/common/sitecontent")]
        public async Task<APIResponseModel<SiteContentModel>> GetSiteContents(AppContentType appContentTypeId)
        {
            return await _appContentModelFactory.PrepareSiteContent(isEnglish, appContentTypeId);
        }

        /// <summary>
        /// create CustomerFeedbacks
        /// </summary>
        [HttpPost, Route("/webapi/common/createcontactus")]
        public async Task<APIResponseModel<object>> CreateContactUs([FromBody] CustomerFeedbackModel customerFeedbackModel)
        {
            return await _appContentModelFactory.CreateFeedbackRequest(isEnglish, customerFeedbackModel);
        }

        /// <summary>
        /// To get governorates
        /// </summary>
        /// <returns>Governorates</returns>
        [HttpGet, Route("/webapi/common/governorates")]
        public async Task<APIResponseModel<List<GovernorateModel>>> GetGovernorates(int? id = null)
        {
            return await _appContentModelFactory.PrepareGovernorates(isEnglish, id: id);
        }

        /// <summary>
        /// To get areas
        /// </summary>
        /// <returns>Areas</returns>
        [HttpGet, Route("/webapi/common/areas")]
        public async Task<APIResponseModel<List<AreaModel>>> GetAreas(int? id = null, int? governorateId = null)
        {
            return await _appContentModelFactory.PrepareAreas(isEnglish, id: id, governorateId: governorateId);
        }

        /// <summary>
        /// To get payment methods
        /// </summary>
        /// <returns>Payment methods</returns>
        [HttpGet, Route("/webapi/common/paymentmethods")]
        [Authorize]
        public async Task<APIResponseModel<List<PaymentMethodModel>>> GetPaymentMethods(PaymentRequestType typeId)
        {
            return await _appContentModelFactory.PreparePaymentMethods(isEnglish, paymentRequestType: typeId, customerId: LoggedInCustomerId);
        }

        /// <summary>
        /// To get categories
        /// </summary>
        /// <returns>Categories</returns>
        [HttpGet, Route("/webapi/common/categories")]
        public async Task<APIResponseModel<List<CategoryModel>>> GetCategories(int? id = null, string seoName = "")
        {
            return await _appContentModelFactory.PrepareCategories(isEnglish, id: id, seoName: seoName);
        }

        /// <summary>
        /// To get wallet packages
        /// </summary>
        /// <returns>Wallet packages</returns>
        [HttpGet, Route("/webapi/common/walletpackages")]
        public async Task<APIResponseModel<List<WalletPackageModel>>> GetWalletPackages(int id = 0)
        {
            return await _appContentModelFactory.PrepareWalletPackages(isEnglish, id: id);
        }

        [HttpGet, Route("/webapi/common/pageheader")]
        public async Task<APIResponseModel<PageHeaderModel>> GetPageHeaders()
        {
            return await _appContentModelFactory.PreparePageHeader(isEnglish);
        }

        [HttpGet, Route("/webapi/common/quickpay")]
        public async Task<APIResponseModel<QuickPaymentModel>> GetQuickPay(string quickPayNumber)
        {
            return await _appContentModelFactory.PrepareQuickPaymentModel(isEnglish: isEnglish, paymentNumber: quickPayNumber);
        }

        [HttpPost, Route("/webapi/customer/createquickpay")]
        public async Task<APIResponseModel<CreatePaymentModel>> CreateQuickpay([FromBody] CreatePaymentModel createPaymentModel)
        {
            return await _appContentModelFactory.CreateQuickpay(isEnglish: isEnglish, createPaymentModel: createPaymentModel);
        }

        [HttpPost, Route("/webapi/common/testpush")]
        public async Task<APIResponseModel<bool>> CreateQuickpay(string title, string message, string token)
        {
            return await _appContentModelFactory.TestPush(isEnglish: isEnglish, title: title, message: message, token: token);
        }
    }
}
