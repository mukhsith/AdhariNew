using Data.SystemUserManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utility.Helpers;

namespace Admin.ViewComponents
{
    namespace Admin.ViewComponents
    {
        public class NavigationHeaderViewComponent : ViewComponent
        {
            public IViewComponentResult Invoke()
            {
                SystemUser model = new();
                model.Role = new();
                if (HttpContext.Request.Cookies.Count > 0)
                {
                    if (HttpContext.Request.Cookies.ContainsKey(Constants.ClaimTypeFullName))
                    {
                        model.FullName = HttpContext.Request.Cookies[Constants.ClaimTypeFullName] ?? "Unknown";
                    }
                    if (HttpContext.Request.Cookies.ContainsKey(Constants.ClaimTypeRoleName))
                    {  
                        model.Role.Name = HttpContext.Request.Cookies[Constants.ClaimTypeRoleName];
                    }
                }
                return View(model);
            }
        }
    }
}
