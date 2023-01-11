using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Routing;

namespace Plugin.Payment.KNET
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }

    public class BasicAuthentication : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                if (actionContext.Request.Headers.Authorization != null)
                {
                    //Taking the parameter from the header  
                    var accessToken = actionContext.Request.Headers.Authorization.Parameter;
                    //decode the parameter  
                    var decoAccessToken = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(accessToken));
                    //split by colon : and store in variable  
                    var UserNameAndPassword = decoAccessToken.Split(':');
                    //Passing to a function for authorization  
                    if (IsAuthorizedUser(UserNameAndPassword[0], UserNameAndPassword[1]))
                    {
                        // setting current principle  
                        Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(UserNameAndPassword[0]), null);
                    }
                    else
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                    }
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        public static bool IsAuthorizedUser(string Username, string Password)
        {
            // In this method we can handle our database logic here...  
            //Here we have given the hard-coded values   
            return Username == "kPayMW" && Password == "abc123";
        }
    }
}
