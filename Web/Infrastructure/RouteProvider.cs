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
                   pattern: "",
                   defaults: new { controller = "Home", action = "Index" });


            //About Us 
            endpointRouteBuilder.MapControllerRoute(
                   name: "aboutus",
                   pattern: "aboutus",
                   defaults: new { controller = "Common", action = "AboutUs" });

            //order details
            endpointRouteBuilder.MapControllerRoute(
                   name: "contactus",
                   pattern: "contactus",
                   defaults: new { controller = "Common", action = "CreateContactUsRequest" });

            //payment result
            endpointRouteBuilder.MapControllerRoute(
                   name: "paymentresult",
                   pattern: "paymentresult",
                   defaults: new { controller = "Common", action = "PaymentResult" });


            #region Customer
            //login
            endpointRouteBuilder.MapControllerRoute(
                   name: "login",
                   pattern: "login",
                   defaults: new { controller = "Customer", action = "Login" });
            //logout
            endpointRouteBuilder.MapControllerRoute(
                   name: "logout",
                   pattern: "logout",
                   defaults: new { controller = "Customer", action = "Logout" });
            //register
            endpointRouteBuilder.MapControllerRoute(
                   name: "register",
                   pattern: "register",
                   defaults: new { controller = "Customer", action = "Register" });

            //resend otp
            endpointRouteBuilder.MapControllerRoute(
                   name: "resendotp",
                   pattern: "resendotp",
                   defaults: new { controller = "Customer", action = "ResendOTP" });
            //verify otp
            endpointRouteBuilder.MapControllerRoute(
                   name: "verifyotp",
                   pattern: "verifyotp/{code?}",
                   defaults: new { controller = "Customer", action = "VerifyOTP" });

            //my profile
            endpointRouteBuilder.MapControllerRoute(
                   name: "myprofile",
                   pattern: "myprofile",
                   defaults: new { controller = "Customer", action = "MyProfile" });

            //change password
            endpointRouteBuilder.MapControllerRoute(
                   name: "changepassword",
                   pattern: "changepassword",
                   defaults: new { controller = "Customer", action = "ChangePassword" });

            //forgot password
            endpointRouteBuilder.MapControllerRoute(
                   name: "forgotpassword",
                   pattern: "forgotpassword",
                   defaults: new { controller = "Customer", action = "ForgotPassword" });

            //change password by email
            endpointRouteBuilder.MapControllerRoute(
                   name: "changepasswordbyemail",
                   pattern: "changepasswordbyemail/{code?}",
                   defaults: new { controller = "Customer", action = "ChangePasswordByEmail" });

            //get notifications
            endpointRouteBuilder.MapControllerRoute(
                   name: "mynotifications",
                   pattern: "mynotifications",
                   defaults: new { controller = "Customer", action = "MyNotifications" });

            //get notifications by ajax
            endpointRouteBuilder.MapControllerRoute(
                   name: "notificationsbyajax",
                   pattern: "notificationsbyajax",
                   defaults: new { controller = "Customer", action = "NotificationsByAjax" });

            //get my addresses
            endpointRouteBuilder.MapControllerRoute(
                   name: "myaddresses",
                   pattern: "myaddresses",
                   defaults: new { controller = "Customer", action = "Addresses" });

            //add new addresses
            endpointRouteBuilder.MapControllerRoute(
                   name: "addnewaddress",
                   pattern: "addnewaddress",
                   defaults: new { controller = "Customer", action = "AddNewAddress" });

            //get my addresse details
            endpointRouteBuilder.MapControllerRoute(
                   name: "addressdetails",
                   pattern: "myaddresses/details/{addressId?}",
                   defaults: new { controller = "Customer", action = "AddressDetails" });

            //update address details
            endpointRouteBuilder.MapControllerRoute(
                   name: "updateaddress",
                   pattern: "updateaddress",
                   defaults: new { controller = "Customer", action = "UpdateAddress" });

            //my wallet
            endpointRouteBuilder.MapControllerRoute(
                   name: "mywallet",
                   pattern: "mywallet",
                   defaults: new { controller = "Customer", action = "MyWallet" });

            //my cashback
            endpointRouteBuilder.MapControllerRoute(
                   name: "mycashback",
                   pattern: "mycashback",
                   defaults: new { controller = "Customer", action = "MyCashback" });

            //my cashback
            endpointRouteBuilder.MapControllerRoute(
                   name: "walletpackages",
                   pattern: "walletpackages",
                   defaults: new { controller = "Customer", action = "WalletPackages" });
            #endregion Customer

            #region My Orders
            //to create order
            endpointRouteBuilder.MapControllerRoute(
                   name: "createorder",
                   pattern: "createorder",
                   defaults: new { controller = "Order", action = "CreateOrder" });

            //my orders
            endpointRouteBuilder.MapControllerRoute(
                   name: "myorders",
                   pattern: "myorders",
                   defaults: new { controller = "Order", action = "Orders" });

            //orders by ajax
            endpointRouteBuilder.MapControllerRoute(
                   name: "ordersbyajax",
                   pattern: "ordersbyajax",
                   defaults: new { controller = "Order", action = "OrdersByAjax" });

            //order details
            endpointRouteBuilder.MapControllerRoute(
                   name: "orderdetails",
                   pattern: "order/details/{orderNumber?}/{email?}",
                   defaults: new { controller = "Order", action = "OrderDetails" });

            //order result
            endpointRouteBuilder.MapControllerRoute(
                   name: "orderresult",
                   pattern: "ORD/{orderNumber?}",
                   defaults: new { controller = "Order", action = "OrderResult" });

            //order result
            endpointRouteBuilder.MapControllerRoute(
                   name: "orderresultsms",
                   pattern: "ORDER/{orderNumber?}",
                   defaults: new { controller = "Order", action = "OrderResultSMS" });

            //order result
            endpointRouteBuilder.MapControllerRoute(
                   name: "reorder",
                   pattern: "reorder",
                   defaults: new { controller = "Order", action = "ReOrder" });
            #endregion My Orders

            #region My Subscriptions
            endpointRouteBuilder.MapControllerRoute(
                   name: "mysubscriptions",
                   pattern: "mysubscriptions",
                   defaults: new { controller = "Subscription", action = "Subscriptions" });

            endpointRouteBuilder.MapControllerRoute(
                   name: "savesubscriptionattributes",
                   pattern: "savesubscriptionattributes",
                   defaults: new { controller = "Subscription", action = "SaveSubscriptionAttributes" });

            endpointRouteBuilder.MapControllerRoute(
                   name: "subscriptioncheckout",
                   pattern: "s-checkout",
                   defaults: new { controller = "Subscription", action = "Checkout" });

            endpointRouteBuilder.MapControllerRoute(
                   name: "subscriptioncheckoutaddress",
                   pattern: "s-checkoutaddress",
                   defaults: new { controller = "Subscription", action = "CheckoutAddress" });

            endpointRouteBuilder.MapControllerRoute(
                   name: "subscriptioncheckoutsummary",
                   pattern: "s-checkoutsummary",
                   defaults: new { controller = "Subscription", action = "CheckoutSummary" });

            endpointRouteBuilder.MapControllerRoute(
                   name: "createsubscription",
                   pattern: "createsubscription",
                   defaults: new { controller = "Subscription", action = "CreateSubscription" });

            endpointRouteBuilder.MapControllerRoute(
                   name: "subscriptionresult",
                   pattern: "SUB/{subscriptionNumber?}",
                   defaults: new { controller = "Subscription", action = "SubscriptionResult" });

            endpointRouteBuilder.MapControllerRoute(
                   name: "subscriptiondetails",
                   pattern: "subscription/details/{subscriptionNumber?}",
                   defaults: new { controller = "Subscription", action = "SubscriptionDetails" });
            #endregion My Subscriptions

            #region Misc
            //error
            endpointRouteBuilder.MapControllerRoute(
                   name: "error",
                   pattern: "error",
                   defaults: new { controller = "Home", action = "Error" });

            #endregion Misc
            ////set country
            //endpointRouteBuilder.MapControllerRoute(
            //    name: "setcountry",
            //    pattern: "setcountry",
            //    defaults: new { controller = "Country", action = "SetCountry" });

            #region Products
            ////get product using ajax
            endpointRouteBuilder.MapControllerRoute(
                   name: "products",
                   pattern: "products/{seoName?}",
                   defaults: new { controller = "Product", action = "Products" });

            //get product by ajax
            endpointRouteBuilder.MapControllerRoute(
                   name: "productsbyajax",
                   pattern: "productsbyajax",
                   defaults: new { controller = "Product", action = "ProductsByAjax" });

            ////get product using ajax
            endpointRouteBuilder.MapControllerRoute(
                   name: "productdetails",
                   pattern: "product/{catName?}/{seoName?}",
                   defaults: new { controller = "Product", action = "ProductDetails" });


            ////get product for pop up
            //endpointRouteBuilder.MapControllerRoute(
            //       name: "productdetailspopup",
            //       pattern: "productdetailspopup",
            //       defaults: new { controller = "Product", action = "ProductDetailsPopUp" });
            #endregion Products

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

            //category
            endpointRouteBuilder.MapControllerRoute(
                   name: "category",
                   pattern: "category/{seoName?}/{q?}",
                   defaults: new { controller = "Category", action = "Category" });

            //add or remove favourites
            endpointRouteBuilder.MapControllerRoute(
                   name: "addorremovefavourite",
                   pattern: "addorremovefavourite",
                   defaults: new { controller = "Product", action = "AddOrRemoveFavourite" });

            //My Favorites
            endpointRouteBuilder.MapControllerRoute(
                   name: "favoriteproducts",
                   pattern: "favorites",
                   defaults: new { controller = "Product", action = "FavoriteProducts" });

            //notify or do not notify product
            endpointRouteBuilder.MapControllerRoute(
                   name: "addorremoveproductavailability",
                   pattern: "addorremoveproductavailability",
                   defaults: new { controller = "Product", action = "AddOrRemoveProductAvailability" });

            ////countries
            //endpointRouteBuilder.MapControllerRoute(
            //       name: "countries",
            //       pattern: "countries",
            //       defaults: new { controller = "Common", action = "Countries" });

            ////site contents
            //endpointRouteBuilder.MapControllerRoute(
            //       name: "sitecontents",
            //       pattern: "sitecontents",
            //       defaults: new { controller = "Common", action = "SiteContents" });



            ////terms and conditions
            //endpointRouteBuilder.MapControllerRoute(
            //       name: "termsandconditions",
            //       pattern: "terms",
            //       defaults: new { controller = "Common", action = "TermsAndConditions" });



            ////cart
            //endpointRouteBuilder.MapControllerRoute(
            //       name: "cartitems",
            //       pattern: "cartitems",
            //       defaults: new { controller = "Cart", action = "CartItems" });

            ////add cart item
            //endpointRouteBuilder.MapControllerRoute(
            //       name: "addcartitem",
            //       pattern: "addcartitem",
            //       defaults: new { controller = "Cart", action = "AddCartItem" });

            ////edit cart item
            //endpointRouteBuilder.MapControllerRoute(
            //       name: "editcartitem",
            //       pattern: "editcartitem",
            //       defaults: new { controller = "Cart", action = "EditCartItem" });

            ////delete cart item
            //endpointRouteBuilder.MapControllerRoute(
            //       name: "deletecartitem",
            //       pattern: "deletecartitem",
            //       defaults: new { controller = "Cart", action = "DeleteCartItem" });

            ////delete cart items
            //endpointRouteBuilder.MapControllerRoute(
            //       name: "deletecartitems",
            //       pattern: "deletecartitems",
            //       defaults: new { controller = "Cart", action = "DeleteCartItems" });


            //to save cart attributes
            endpointRouteBuilder.MapControllerRoute(
                   name: "savecartattributes",
                   pattern: "savecartattributes",
                   defaults: new { controller = "Cart", action = "SaveCartAttributes" });

            //to checkout
            endpointRouteBuilder.MapControllerRoute(
                   name: "checkout",
                   pattern: "checkout",
                   defaults: new { controller = "Cart", action = "Checkout" });

            //to checkout address
            endpointRouteBuilder.MapControllerRoute(
                   name: "checkoutaddress",
                   pattern: "checkoutaddress",
                   defaults: new { controller = "Cart", action = "CheckoutAddress" });

            //to checkout address
            endpointRouteBuilder.MapControllerRoute(
                   name: "checkoutsummary",
                   pattern: "checkoutsummary",
                   defaults: new { controller = "Cart", action = "CheckoutSummary" });            

            ////order details
            //endpointRouteBuilder.MapControllerRoute(
            //       name: "guestorderdetails",
            //       pattern: "order/details-g/{orderNumber?}/{email?}",
            //       defaults: new { controller = "Order", action = "GuestOrderDetails" });

            //site contents
            endpointRouteBuilder.MapControllerRoute(
                   name: "getsitecontents",
                   pattern: "getsitecontents/{appContentType}",
                   defaults: new { controller = "Common", action = "GetSiteContents" });

            //Governorates
            endpointRouteBuilder.MapControllerRoute(
                   name: "getgovernorates",
                   pattern: "getgovernorates",
                   defaults: new { controller = "Common", action = "GetGovernorates" });

            //Areas
            endpointRouteBuilder.MapControllerRoute(
                   name: "getareas",
                   pattern: "getareas/{governorateId}",
                   defaults: new { controller = "Common", action = "GetAreas" });

            //Areas
            endpointRouteBuilder.MapControllerRoute(
                   name: "addaddress",
                   pattern: "addaddress",
                   defaults: new { controller = "Customer", action = "AddAddress" });

            //get payment methods
            endpointRouteBuilder.MapControllerRoute(
                   name: "paymentmethod",
                   pattern: "paymentmethod/{paymentRequestType}",
                   defaults: new { controller = "Common", action = "GetPaymentMehods" });

            //to create wallet package order
            endpointRouteBuilder.MapControllerRoute(
                   name: "createwalletpackageorder",
                   pattern: "createwalletpackageorder",
                   defaults: new { controller = "Customer", action = "CreateWalletPackageOrder" });

            //wallet order result
            endpointRouteBuilder.MapControllerRoute(
                   name: "walletorderresult",
                   pattern: "WPP/{orderNumber?}",
                   defaults: new { controller = "Customer", action = "WalletOrderResult" });
        }
    }
}
