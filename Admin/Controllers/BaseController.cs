using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
//using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
//using System.Linq.Dynamic.Core.Tokenizer;
using System.Text;
using Utility.API;
using Utility.Helpers;

namespace Admin.Controllers
{
    
    public class BaseController : Controller 
    {

        protected ILogger Logger; 
//        protected IConfiguration Config { get;}
        protected readonly AppSettingsModel _appSettings;
        protected string FullName = "Unknow";
        protected int UserId = 0;
        protected int RoleId = 0;
        protected string ImagePath = "";







        public BaseController(  IHttpContextAccessor httpContextAccessor,
                                IOptions<AppSettingsModel> options, 
                                ILogger  logger)
        {
            Logger = logger;
            _appSettings = options.Value;

            //Config = config;
            //if (httpContextAccessor.HttpContext == null || 
            //    !httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(Constants.ClaimAuthenticationToken))
            //{   
            //    Logger.Log(LogLevel.Error, "Redirect to Account Controller, Token not found");
            //    httpContextAccessor.HttpContext.Response.Redirect(Constants.ControllerAccount);
            //}

            //if (httpContextAccessor.HttpContext.Request.Cookies[Constants.ClaimTypeRoleId] == null)
            //{
            //    Logger.Log(LogLevel.Error, "Redirect to Account ClaimTypeRoleId is null");
            //    Response.Redirect(Constants.RedirectToAccount);
            //}
        }

        public virtual IActionResult Index()
        {
            //if token not exits, logout
            if (!HttpContext.Request.Cookies.ContainsKey(Constants.ClaimAuthenticationToken))
            {
                return RedirectToActionPermanent(Constants.ActionLogout, Constants.ControllerAccount);
            }  //if token exists
            else if (HttpContext.Request.Cookies.ContainsKey(Constants.ClaimAuthenticationToken))
            {
                var token = HttpContext.Request.Cookies[Constants.ClaimAuthenticationToken].ToString();
                var auth = APIHelper.IsValidToken(token, _appSettings);
                //if token exists
                if (auth.TokenExpired) 
                {
                    return RedirectToActionPermanent(Constants.ActionLogout, Constants.ControllerAccount);
                }
            }
            return View();
        }
       

    }
}


