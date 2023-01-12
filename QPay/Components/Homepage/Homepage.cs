using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Utility.API;
using Utility.Models.Frontend.CustomizedModel;
using Utility.ResponseMapper;

namespace Web.Components
{
    [ViewComponent]
    public class Homepage : ViewComponent
    {
        private readonly IAPIHelper _apiHelper;
        private readonly ILogger _logger;
        public Homepage(IAPIHelper apiHelper,
            ILoggerFactory logger)
        {
            _apiHelper = apiHelper;
            _logger = logger.CreateLogger(typeof(Homepage).Name);
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var customerGuidValue = Convert.ToString(Request.Cookies["CustomerGuidValue"]);
                if (string.IsNullOrEmpty(customerGuidValue))
                {
                    customerGuidValue = Guid.NewGuid().ToString();
                    HttpContext.Response.Cookies.Append("CustomerGuidValue", customerGuidValue, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
                }

                var responseModel = await _apiHelper.GetAsync<APIResponseModel<HomepageModel>>("webapi/common/homepagecontents?customerGuidValue=" + customerGuidValue);
                if (responseModel.Success && responseModel.Data != null)
                {
                    return View(responseModel.Data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return Content("");
        }
    }
}
