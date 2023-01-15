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
                   name: "quickpay",
                   pattern: "/{quickPayNumber?}",
                   defaults: new { controller = "Home", action = "Index" });

            //to create quick pay
            endpointRouteBuilder.MapControllerRoute(
                   name: "createqpay",
                   pattern: "qpay/createqpay",
                   defaults: new { controller = "Home", action = "CreateQPay" });

            //quick pay result
            endpointRouteBuilder.MapControllerRoute(
                   name: "qpayresult",
                   pattern: "QPR/{quickPayNumber?}",
                   defaults: new { controller = "Home", action = "QPayResult" });
        }
    }
}
