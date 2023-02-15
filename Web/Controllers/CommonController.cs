using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility.API;
using Utility.Enum;
using Utility.Models.Frontend.Content;
using Utility.Models.Frontend.Locations;
using Utility.ResponseMapper;

namespace Web.Controllers
{
    public class CommonController : Controller
    {
        private readonly IAPIHelper _apiHelper;
        private readonly ILogger _logger;
        public CommonController(IAPIHelper apiHelper, ILoggerFactory logger)
        {
            _apiHelper = apiHelper;
            _logger = logger.CreateLogger(typeof(CommonController).Name);
        }

        /// <summary>
        /// About us page
        /// </summary>
        public async Task<IActionResult> AboutUs()
        {
            var siteContentModel = new SiteContentModel();
            try
            {
                var responseSiteContentModels = await _apiHelper.GetAsync<APIResponseModel<SiteContentModel>>("webapi/common/sitecontent?appContentTypeId=" + (int)AppContentType.AboutUs);
                if (responseSiteContentModels.Success && responseSiteContentModels.Data != null)
                {
                    siteContentModel = responseSiteContentModels.Data;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return View(siteContentModel);
        }

        /// <summary>
        /// create contact us request
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> CreateContactUsRequest()
        {
            var responseContactRequestModel = await _apiHelper.GetAsync<APIResponseModel<CompanySettingModel>>("webapi/common/companysetting");
            if (responseContactRequestModel.Success && responseContactRequestModel.Data != null)
            {
                return View(responseContactRequestModel.Data);
            }

            return View(new ContactDetailModel());
        }

        /// <summary>
        /// create contact us request
        /// </summary>
        [HttpPost]
        public virtual async Task<JsonResult> CreateContactUsRequest(CustomerFeedbackModel customerFeedbackModel)
        {
            APIResponseModel<CustomerFeedbackModel> response = new();
            try
            {
                var data = await _apiHelper.PostAsync<APIResponseModel<CustomerFeedbackModel>>("webapi/common/createcontactus", customerFeedbackModel);
                response.Success = data.Success;
                response.Message = data.Message;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                _logger.LogInformation(ex.Message);
            }

            return Json(response);
        }

        /// <summary>
        /// GetSiteContents
        /// </summary>
        public async Task<JsonResult> GetSiteContents(AppContentType appContentType)
        {
            try
            {
                var responseSiteContents = await _apiHelper.GetAsync<APIResponseModel<SiteContentModel>>("webapi/common/sitecontent?appContentTypeId=" + appContentType);
                if (responseSiteContents.Success && responseSiteContents.Data != null)
                {
                    return Json(responseSiteContents.Data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return Json("");
        }

        /// <summary>
        /// Get governorates
        /// </summary>
        public async Task<JsonResult> GetGovernorates()
        {
            var reponse = new APIResponseModel<List<GovernorateModel>>();

            try
            {
                reponse = await _apiHelper.GetAsync<APIResponseModel<List<GovernorateModel>>>("webapi/common/governorates");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return Json(reponse);
        }

        /// <summary>
        /// Get areas
        /// </summary>
        public async Task<JsonResult> GetAreas(int governorateId)
        {
            var reponse = new APIResponseModel<List<AreaModel>>();

            try
            {
                reponse = await _apiHelper.GetAsync<APIResponseModel<List<AreaModel>>>("webapi/common/areas?governorateId=" + governorateId);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return Json(reponse);
        }
        public async Task<JsonResult> GetPaymentMehods(PaymentRequestType paymentRequestType)
        {
            var reponse = new APIResponseModel<List<PaymentMethodModel>>();

            try
            {
                reponse = await _apiHelper.GetAsync<APIResponseModel<List<PaymentMethodModel>>>("webapi/common/paymentmethods?typeId=" + paymentRequestType);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return Json(reponse);
        }

        /// <summary>
        /// payment result
        /// </summary>
        public IActionResult PaymentResult()
        {
            return View();
        }

        /// <summary>
        /// Terms And Conditions
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> TermsAndConditions()
        {
            var siteContentModel = new SiteContentModel();
            try
            {
                var responseSiteContentModels = await _apiHelper.GetAsync<APIResponseModel<SiteContentModel>>("webapi/common/sitecontent?appContentTypeId=" + (int)AppContentType.TermsCondition);
                if (responseSiteContentModels.Success && responseSiteContentModels.Data != null)
                {
                    siteContentModel = responseSiteContentModels.Data;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return View(siteContentModel);
        }

        /// <summary>
        /// Privacy Policy
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> PrivacyPolicy()
        {
            var siteContentModel = new SiteContentModel();
            try
            {
                var responseSiteContentModels = await _apiHelper.GetAsync<APIResponseModel<SiteContentModel>>("webapi/common/sitecontent?appContentTypeId=" + (int)AppContentType.PrivacyPolicy);
                if (responseSiteContentModels.Success && responseSiteContentModels.Data != null)
                {
                    siteContentModel = responseSiteContentModels.Data;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return View(siteContentModel);
        }

        /// <summary>
        /// Refund Policy
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> RefundPolicy()
        {
            var siteContentModel = new SiteContentModel();
            try
            {
                var responseSiteContentModels = await _apiHelper.GetAsync<APIResponseModel<SiteContentModel>>("webapi/common/sitecontent?appContentTypeId=" + (int)AppContentType.RefundPolicy);
                if (responseSiteContentModels.Success && responseSiteContentModels.Data != null)
                {
                    siteContentModel = responseSiteContentModels.Data;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return View(siteContentModel);
        }

        /// <summary>
        /// Site content mobile
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> SiteContentMobile(int appContentTypeId, bool isEnglish)
        {
            var siteContentModel = new SiteContentModel();
            try
            {
                var responseSiteContentModels = await _apiHelper.GetAsync<APIResponseModel<SiteContentModel>>("webapi/common/sitecontent?appContentTypeId=" + appContentTypeId);
                if (responseSiteContentModels.Success && responseSiteContentModels.Data != null)
                {
                    siteContentModel = responseSiteContentModels.Data;

                    var currentLanguage = string.Empty;
                    var customerLanguage = isEnglish ? "en" : "ar";
                    if (!string.IsNullOrEmpty(CultureInfo.CurrentCulture.Name))
                    {
                        currentLanguage = CultureInfo.CurrentCulture.Name.ToLower();
                    }

                    if (currentLanguage != customerLanguage)
                    {
                        var cultureInfo = new CultureInfo(customerLanguage);
                        Thread.CurrentThread.CurrentUICulture = cultureInfo;
                        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);

                        Response.Cookies.Append(
                        CookieRequestCultureProvider.DefaultCookieName,
                        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(customerLanguage == "en" ? "en-US" : "ar-KW")),
                        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

                        return RedirectToRoute("sitecontentmobile", new { appContentTypeId = appContentTypeId, isEnglish= isEnglish });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return View(siteContentModel);
        }

        /// <summary>
        /// Download app
        /// </summary>
        public async Task<IActionResult> DownloadApp()
        {
            var siteContentModel = new CompanySettingModel();
            try
            {
                var responseCompanySettingModel = await _apiHelper.GetAsync<APIResponseModel<CompanySettingModel>>("webapi/common/companysetting");
                if (responseCompanySettingModel.Success && responseCompanySettingModel.Data != null)
                {
                    siteContentModel = responseCompanySettingModel.Data;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return View(siteContentModel);
        }
    }
}
