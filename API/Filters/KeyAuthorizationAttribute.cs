using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using Utility.API;
using Utility.Models;

namespace API.Filters
{
    public sealed class KeyAuthorizationAttribute : TypeFilterAttribute
    {
        public KeyAuthorizationAttribute() : base(typeof(KeyAuthorizationFilter)) { }
        private class KeyAuthorizationFilter : IActionFilter
        {
            private readonly IConfiguration _config;
            public KeyAuthorizationFilter(IConfiguration config)
            {
                _config = config;
            }
            protected bool IsValidKey(ActionExecutingContext filterContext)
            {
                var headers = filterContext.HttpContext.Request.Headers;
                if (headers.ContainsKey("key"))
                {
                    StringValues keys;
                    headers.TryGetValue("key", out keys);

                    var appSettingsSection = _config.GetSection("AppSettings");
                    var appSettings = appSettingsSection.Get<AppSettingsModel>();
                    var key = appSettings.APIKey;
                    if (keys.FirstOrDefault().ToUpper() == key.ToUpper())
                    {
                        return true;
                    }
                }

                return false;
            }
            public void OnActionExecuting(ActionExecutingContext filterContext)
            {
                if (filterContext == null)
                    throw new ArgumentNullException(nameof(filterContext));

                if (!IsValidKey(filterContext))
                {
                    filterContext.Result = new UnauthorizedObjectResult("User is unauthorized. Please provide the secret key");
                    return;
                }
            }
            public void OnActionExecuted(ActionExecutedContext filterContext) { }
        }
    }
}
