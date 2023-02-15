using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace QPay.Infrastructure
{
    /// <summary>
    /// Route provider
    /// </summary>
    public static class RouteProvider
    {
        /// <summary>
        /// Register routes          
        /// </summary>
        /// <param name="endpointRouteBuilder">Endpoint route builder</param>
        public static void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            //website url
            endpointRouteBuilder.MapControllerRoute(
                   name: "websiteurl",
                   pattern: "websiteurl",
                   defaults: new { controller = "Home", action = "WebsiteUrl" });

            //home
            endpointRouteBuilder.MapControllerRoute(
                   name: "home",
                   pattern: "",
                   defaults: new { controller = "Home", action = "Index" });

            //pay
            endpointRouteBuilder.MapControllerRoute(
                   name: "pay",
                   pattern: "pay/{quickPayNumber?}",
                   defaults: new { controller = "Home", action = "Pay" });

            //to create quick pay
            endpointRouteBuilder.MapControllerRoute(
                   name: "createqpay",
                   pattern: "qpay/createqpay",
                   defaults: new { controller = "Home", action = "CreateQPay" });

            //to update quick pay
            endpointRouteBuilder.MapControllerRoute(
                   name: "updateqpay",
                   pattern: "qpay/updateqpay",
                   defaults: new { controller = "Home", action = "UpdateQPay" });

            //quick pay result
            endpointRouteBuilder.MapControllerRoute(
                   name: "qpayresult",
                   pattern: "QPR/{quickPayNumber?}",
                   defaults: new { controller = "Home", action = "QPayResult" });

            //register
            endpointRouteBuilder.MapControllerRoute(
                   name: "register",
                   pattern: "register",
                   defaults: new { controller = "Home", action = "Register" });

            //verify otp
            endpointRouteBuilder.MapControllerRoute(
                   name: "verifyotp",
                   pattern: "verifyotp/{code?}",
                   defaults: new { controller = "Home", action = "VerifyOTP" });

            //verify otp
            endpointRouteBuilder.MapControllerRoute(
                   name: "verifyotp",
                   pattern: "verifyotp/{code?}",
                   defaults: new { controller = "Home", action = "VerifyOTP" });
        }
    }
}
