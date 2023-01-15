using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Utility.API;
using Utility.Models.Frontend.CustomizedModel;
using Utility.ResponseMapper;

namespace Web.Components
{
    [ViewComponent]
    public class Header : ViewComponent
    {
        private readonly IAPIHelper _apiHelper;
        private IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly ILogger _logger;
        private readonly AppSettingsModel _appSettingsModel;
        public Header(IAPIHelper apiHelper,
            IStringLocalizer<SharedResource> sharedLocalizer,
            ILoggerFactory logger,
            IOptions<AppSettingsModel> options)
        {
            _apiHelper = apiHelper;
            _sharedLocalizer = sharedLocalizer;
            _logger = logger.CreateLogger(typeof(Header).Name);
            _appSettingsModel = options.Value;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
              
            try
            {
                var responseModel = await _apiHelper.GetAsync<APIResponseModel<PageHeaderModel>>("webapi/common/pageheader");
                if (responseModel.Success && responseModel.Data != null)
                {
                    //var countries = responseModel.Data.Countries;
                    //int.TryParse(Request.Cookies["CountryId"], out int countryId);
                    //if (countryId == 0)
                    //{
                    //    var defaultCountry = countries.Where(a => a.Default).FirstOrDefault();
                    //    if (defaultCountry != null)
                    //    {
                    //        defaultCountry.Active = true;
                    //    }
                    //}
                    //else
                    //{
                    //    var selectedCountry = countries.Where(a => a.Id == countryId).FirstOrDefault();
                    //    if (selectedCountry != null)
                    //    {
                    //        selectedCountry.Active = true;
                    //    }
                    //}

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
