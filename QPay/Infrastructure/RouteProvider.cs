using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Web.Infrastructure
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

            //home
            endpointRouteBuilder.MapControllerRoute(
                   name: "home",
                   pattern: "/{quickPayNumber?}",
                   defaults: new { controller = "Home", action = "Index" });

            //to create quick pay
            endpointRouteBuilder.MapControllerRoute(
                   name: "createqpay",
                   pattern: "createqpay",
                   defaults: new { controller = "Home", action = "CreateQPay" });

            //quick pay result
            endpointRouteBuilder.MapControllerRoute(
                   name: "qpayresult",
                   pattern: "WPP/{quickPayNumber?}",
                   defaults: new { controller = "Home", action = "QPayResult" });
        }
    }
}
