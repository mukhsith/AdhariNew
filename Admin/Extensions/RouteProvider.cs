using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Admin.Extensions
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
                   pattern: "",
                   defaults: new { controller = "Home", action = "Index" });

            #region Cart

            //cart
            endpointRouteBuilder.MapControllerRoute(
                   name: "cartitems",
                   pattern: "cartitems",
                   defaults: new { controller = "Cart", action = "CartItems" });

            //add cart item
            endpointRouteBuilder.MapControllerRoute(
                   name: "addcartitem",
                   pattern: "addcartitem",
                   defaults: new { controller = "Cart", action = "AddCartItem" });

            //edit cart item
            endpointRouteBuilder.MapControllerRoute(
                   name: "editcartitem",
                   pattern: "editcartitem",
                   defaults: new { controller = "Cart", action = "EditCartItem" });

            //get cart count
            endpointRouteBuilder.MapControllerRoute(
                   name: "getcartcount",
                   pattern: "getcartcount",
                   defaults: new { controller = "Cart", action = "GetCartCount" });

            //delete cart item
            endpointRouteBuilder.MapControllerRoute(
                   name: "deletecartitem",
                   pattern: "deletecartitem",
                   defaults: new { controller = "Cart", action = "DeleteCartItem" });

            #endregion
        }
    }
}
