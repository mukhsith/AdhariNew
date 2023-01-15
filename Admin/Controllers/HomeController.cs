using Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using Utility.Helpers;

namespace Admin.Controllers
{
    
    public class HomeController : BaseController
    {
       
        public HomeController(IHttpContextAccessor httpContextAccessor, IOptions<AppSettingsModel> options, ILoggerFactory logger) :
            base(httpContextAccessor, options,
                logger.CreateLogger(typeof(HomeController).Name))
        {
            //_appSettings = options.Value;
        }
        //public  override IActionResult Index()
        //{
        //    //if (HttpContext.Request.Cookies.ContainsKey(Constants.ClaimAuthenticationToken))
        //    //{
        //    //    var token = HttpContext.Request.Cookies[Constants.ClaimAuthenticationToken].ToString();
        //    //    var auth = APIHelper.IsValidToken(token, base._appSettings);
        //    //    if (auth.TokenExpired)
        //    //    {
        //    //        return RedirectToActionPermanent(Constants.ActionLogout, Constants.ControllerAccount);
        //    //    } 
        //    //}
        //    return View();

        //}


        public IActionResult Dashboard()
        {

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
