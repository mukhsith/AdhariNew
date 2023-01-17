using Data.Common;
using Data.SystemUserManagement;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utility.API;
using Utility.Helpers;
using Utility.ResponseMapper;
using Utility.Models.Base;
using Utility.Models.JWTConfiguration;
using Microsoft.Extensions.Options;
using System.Security.Principal;

namespace Admin.Controllers
{
    public class AccountController : Controller
    {

        //private readonly IConfiguration _config;IConfiguration config,// _config = config;
        private readonly IAPIHelper _apiHelper;
        private readonly AppSettingsModel _appSettings;
        public AccountController(IAPIHelper apiHelper, IOptions<AppSettingsModel> appSettings)
        {

            _apiHelper = apiHelper;
            _appSettings = appSettings.Value;
        }
        [HttpGet]
        public IActionResult Index()
        {
            //pass a user login model for view
            var loginUser = new LoginModel();
            ViewBag.IsLoginSucceeded = false;
            return View(loginUser);
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginModel login)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Validation Error", "Please enter Username or Password");
                return View(login);
            }
            //

            var responseModel = await _apiHelper.PostAsync<APIResponseModel<AuthenticationResult>>("SystemUser/LoginAdminNew", login, PostContentType.applicationJson);

            //var responseModel = await _apiHelper.GetAsync< APIResponse<AuthenticationResult>>("SystemUser/LoginAdmin?emailaddress=" + login.EmailAddress + "&password=" + login.Password);
            if (responseModel == null)
            {
                ModelState.AddModelError("Validation Error", "Email Address or Password is incorrect");
                return View(login);
            }
            if (responseModel.Success && responseModel != null && responseModel.Data.AccessToken != null)
            {


                if (!string.IsNullOrEmpty(responseModel.Data.AccessToken.ToString()))
                {
                    AuthenticationResult auth = Utility.API.APIHelper.IsValidToken(responseModel.Data.AccessToken, _appSettings);
                    //BKD for user info
                    //String[] newRoles = { auth.User.RoleName }; //"Administrator", "Customer"
                    //GenericIdentity newIdentity = new GenericIdentity(auth.User.FullName);
                    //GenericPrincipal newPrincipal = new GenericPrincipal(newIdentity,  newRoles);
                    //this.HttpContext.User = newPrincipal;
                    //BKD
                    if (auth.User is not null)
                    {
                        CookieOptions cookieOptions = new();
                        cookieOptions.Expires = DateTime.Now.AddDays(3);
                        Response.Cookies.Append(Constants.ClaimTypeFullName, auth.User.FullName, cookieOptions);
                        Response.Cookies.Append(Constants.ClaimTypeRoleName, auth.User.RoleName, cookieOptions);
                        Response.Cookies.Append(Constants.ClaimAuthenticationToken, responseModel.Data.AccessToken, cookieOptions);
                        if (auth.User.RoleId == "4")
                        {
                            Response.Redirect(Constants.ControllerDriverHome, true);
                        }
                        else
                        {
                            Response.Redirect(Constants.ControllerHome, true);
                        }
                    }
                    else
                    {  //if no valid token logout
                        return View(nameof(Logout));
                    }

                    // return View();
                }
            }
            return View(nameof(Index)); //if success 
        }

        public IActionResult Logout()
        {
            // HttpContext.Response.Cookies.Delete(Constants.ClaimTypeId);

            //HttpContext.Response.Cookies.Delete(Constants.ClaimTypeMobileNumber);
            //HttpContext.Response.Cookies.Delete(Constants.ClaimTypeEmailAddress);
            //HttpContext.Response.Cookies.Delete(Constants.ClaimTypeRoleId);
            HttpContext.Response.Cookies.Delete(Constants.ClaimTypeRoleName);
            //HttpContext.Response.Cookies.Delete(Constants.ClaimTypeActive);
            HttpContext.Response.Cookies.Delete(Constants.ClaimTypeFullName);
            HttpContext.Response.Cookies.Delete(Constants.ClaimAuthenticationToken);
            return View(nameof(Index));
        }
        public IActionResult SessionExpired()
        {
            return View();
        }
        public IActionResult UnAuthorized()
        {
            return View();
        }
    }
}
