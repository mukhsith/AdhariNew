using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Utility.API;
using Utility.ResponseMapper;

namespace Web.Controllers
{
    public class CultureController : Controller
    {
        private readonly IAPIHelper _apiHelper;
        private readonly ILogger _logger;
        public CultureController(IAPIHelper apiHelper, ILoggerFactory logger)
        {
            _apiHelper = apiHelper;
            _logger = logger.CreateLogger(typeof(CultureController).Name);
        }

        [HttpPost]
        public async Task<IActionResult> SetCulture(string culture, string returnUrl)
        {
            try
            {
                await _apiHelper.GetAsync<APIResponseModel<object>>("api/accountsmobile/switchlanguage?language=" + culture);
                Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                 new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return LocalRedirect(returnUrl);
        }
        public IActionResult SetCulture()
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("ar")),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            return RedirectToAction("Index", "Home");
        }
    }
}
