using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
