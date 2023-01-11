#pragma checksum "D:\Development\AdhariNew\Web\Views\Subscription\SubscriptionResult.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a77f87c2254eabe42d50c0fcf954923c3edfa909"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Subscription_SubscriptionResult), @"mvc.1.0.view", @"/Views/Subscription/SubscriptionResult.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Development\AdhariNew\Web\Views\_ViewImports.cshtml"
using Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Development\AdhariNew\Web\Views\_ViewImports.cshtml"
using Microsoft.Extensions.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Development\AdhariNew\Web\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Builder;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Development\AdhariNew\Web\Views\_ViewImports.cshtml"
using Microsoft.Extensions.Options;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\Development\AdhariNew\Web\Views\_ViewImports.cshtml"
using Utility.Models.Base;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\Development\AdhariNew\Web\Views\_ViewImports.cshtml"
using Utility.Models.Frontend.CustomizedModel;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "D:\Development\AdhariNew\Web\Views\_ViewImports.cshtml"
using Utility.Models.Frontend.CustomerManagement;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "D:\Development\AdhariNew\Web\Views\_ViewImports.cshtml"
using Utility.Models.Frontend.Content;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "D:\Development\AdhariNew\Web\Views\_ViewImports.cshtml"
using Utility.Models.Frontend.Shop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "D:\Development\AdhariNew\Web\Views\_ViewImports.cshtml"
using Utility.Models.Frontend.Sales;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\Development\AdhariNew\Web\Views\Subscription\SubscriptionResult.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Development\AdhariNew\Web\Views\Subscription\SubscriptionResult.cshtml"
using Utility.Enum;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a77f87c2254eabe42d50c0fcf954923c3edfa909", @"/Views/Subscription/SubscriptionResult.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fd8610713b70f7256c3429c827c060fa488c518d", @"/Views/_ViewImports.cshtml")]
    public class Views_Subscription_SubscriptionResult : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Utility.Models.Frontend.Sales.SubscriptionModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 5 "D:\Development\AdhariNew\Web\Views\Subscription\SubscriptionResult.cshtml"
  
    var requestCultureFeature = System.Globalization.CultureInfo.CurrentCulture.Name;
    Layout = "_Layout";

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Development\AdhariNew\Web\Views\Subscription\SubscriptionResult.cshtml"
  
    var breadCrumbItems = new List<BreadcrumbModel>();
    breadCrumbItems.Add(new BreadcrumbModel() { Url = "#", Title = SharedHtmlLocalizer["Checkout"].Value });

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "D:\Development\AdhariNew\Web\Views\Subscription\SubscriptionResult.cshtml"
Write(await Html.PartialAsync("_PageHeading", breadCrumbItems));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<section class=\"main-content py-5 mb-5\">\r\n    <div class=\"container\">\r\n        <div class=\"row justify-content-center\">\r\n            <div class=\"col-12 text-center\">\r\n");
#nullable restore
#line 18 "D:\Development\AdhariNew\Web\Views\Subscription\SubscriptionResult.cshtml"
                 if (Model.PaymentStatusId == (int)PaymentStatus.Captured || Model.PaymentStatusId == (int)PaymentStatus.PendingCash)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <i class=\"fa-solid fa-circle-check text-success success-icon text-center\"></i>\r\n                    <h2 class=\"text-center mt-3\">");
#nullable restore
#line 21 "D:\Development\AdhariNew\Web\Views\Subscription\SubscriptionResult.cshtml"
                                            Write(SharedHtmlLocalizer["PaymentSuccessful"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n");
#nullable restore
#line 22 "D:\Development\AdhariNew\Web\Views\Subscription\SubscriptionResult.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <i class=\"fa-solid fa-circle-xmark text-danger success-icon text-center\"></i>\r\n                    <h2 class=\"text-center mt-3\">");
#nullable restore
#line 26 "D:\Development\AdhariNew\Web\Views\Subscription\SubscriptionResult.cshtml"
                                            Write(SharedHtmlLocalizer["PaymentNotSuccessful"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n");
#nullable restore
#line 27 "D:\Development\AdhariNew\Web\Views\Subscription\SubscriptionResult.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </div>
            <div class=""col-12 col-md-8 col-lg-6"">
                <div class=""mb-4"">
                    <div class=""bg-grey row rounded-4 p-3 mx-0"">
                        <ul class=""list-group list-group-flush list-card rounded-top rounded-bottom"">
");
#nullable restore
#line 33 "D:\Development\AdhariNew\Web\Views\Subscription\SubscriptionResult.cshtml"
                             foreach (var item in Model.PaymentSummary)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <li class=\"list-group-item d-flex justify-content-between border-secondary px-0\">\r\n                                    <p class=\"mb-0 text-muted\">");
#nullable restore
#line 36 "D:\Development\AdhariNew\Web\Views\Subscription\SubscriptionResult.cshtml"
                                                          Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                    <p class=\"mb-0 text-primary fw-bold text-end \">");
#nullable restore
#line 37 "D:\Development\AdhariNew\Web\Views\Subscription\SubscriptionResult.cshtml"
                                                                              Write(item.Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                </li>\r\n");
#nullable restore
#line 39 "D:\Development\AdhariNew\Web\Views\Subscription\SubscriptionResult.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </ul>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n            <div class=\"col-12 text-center\">\r\n                <a id=\"a-checkout\"");
            BeginWriteAttribute("href", " href=\"", 2259, "\"", 2353, 1);
#nullable restore
#line 45 "D:\Development\AdhariNew\Web\Views\Subscription\SubscriptionResult.cshtml"
WriteAttributeValue("", 2266, Url.RouteUrl("subscriptiondetails",new { subscriptionNumber=Model.SubscriptionNumber}), 2266, 87, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-primary color-white rounded-pill fw-bold px-5 continue-btn\">");
#nullable restore
#line 45 "D:\Development\AdhariNew\Web\Views\Subscription\SubscriptionResult.cshtml"
                                                                                                                                                                                                        Write(SharedHtmlLocalizer["OrderDetails"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</section>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IHtmlLocalizer<SharedResource> SharedHtmlLocalizer { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IOptions<RequestLocalizationOptions> LocOptions { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IStringLocalizer<SharedResource> SharedLocalizer { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IViewLocalizer localizer { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Utility.Models.Frontend.Sales.SubscriptionModel> Html { get; private set; }
    }
}
#pragma warning restore 1591