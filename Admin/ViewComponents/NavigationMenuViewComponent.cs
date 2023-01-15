using Data.SystemUserManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API;
using Utility.Helpers;
using Utility.ResponseMapper;

namespace Admin.ViewComponents
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IAPIHelper _apiHelper; 
        private readonly AppSettingsModel _appSettings;
        public NavigationMenuViewComponent( 
            IAPIHelper apiHelper,
            IOptions<AppSettingsModel> options)
        { 
            _apiHelper = apiHelper;
            _appSettings = options.Value;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        { 
            if (HttpContext.Request.Cookies.ContainsKey(Constants.ClaimAuthenticationToken))
            {
                var token = HttpContext.Request.Cookies[Constants.ClaimAuthenticationToken].ToString();
                var auth =  APIHelper.IsValidToken(token, _appSettings);
                if (auth.TokenExpired)
                {
                    return Content("");

                } else if (auth.User.RoleId !=null ) 
                {
                    var responseModel = await _apiHelper.GetAsync<APIResponseModel<List<SystemUserPermission>>>("SystemUser/GetMenuByRoleId/?id=" + auth.User.RoleId);
                    if (responseModel.Success && responseModel.Data != null)
                    {
                        return View(responseModel.Data);
                    }
                }
            }
            return Content("");
        }
        //public IViewComponentResult Invoke()
        //{
        //    SystemUser model = new(); 
        //    string roleId = HttpContext.Request.Cookies[Constants.ClaimTypeRoleId];
        //    if (roleId != null)
        //    {
        //        model.SystemUserRoleId = Convert.ToInt32(roleId);

        //    }
        //    else { model.SystemUserRoleId = 0; } 
        //    return View(model);
        //}
    }
}
