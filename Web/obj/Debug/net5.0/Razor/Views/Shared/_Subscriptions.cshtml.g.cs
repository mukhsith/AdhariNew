#pragma checksum "D:\Development\AdhariNew\Web\Views\Shared\_Subscriptions.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "164dc7dcabdbee16cf354bc706ec0d02d210054c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__Subscriptions), @"mvc.1.0.view", @"/Views/Shared/_Subscriptions.cshtml")]
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
#line 2 "D:\Development\AdhariNew\Web\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

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
#line 10 "D:\Development\AdhariNew\Web\Views\_ViewImports.cshtml"
using Utility.Enum;

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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"164dc7dcabdbee16cf354bc706ec0d02d210054c", @"/Views/Shared/_Subscriptions.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fd8610713b70f7256c3429c827c060fa488c518d", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__Subscriptions : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Utility.Models.Frontend.Sales.SubscriptionModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Development\AdhariNew\Web\Views\Shared\_Subscriptions.cshtml"
 foreach (var item in Model)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"bg-grey row rounded-4 p-3 mx-0 mb-3\">\r\n        <a");
            BeginWriteAttribute("href", " href=\"", 162, "\"", 256, 1);
#nullable restore
#line 6 "D:\Development\AdhariNew\Web\Views\Shared\_Subscriptions.cshtml"
WriteAttributeValue("", 169, Url.RouteUrl("subscriptiondetails",new { subscriptionNumber=item.SubscriptionNumber }), 169, 87, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n            <div class=\"row\">\r\n                <div class=\"col-11\">\r\n                    <div class=\"row\">\r\n                        <div class=\"col-6 col-md-2 mb-3 mb-xl-0\">\r\n                            <p class=\"mb-0 fw-bold\">");
#nullable restore
#line 11 "D:\Development\AdhariNew\Web\Views\Shared\_Subscriptions.cshtml"
                                               Write(SharedHtmlLocalizer["SubscriptionID"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                            <p class=\"mb-0 text-dark\">");
#nullable restore
#line 12 "D:\Development\AdhariNew\Web\Views\Shared\_Subscriptions.cshtml"
                                                 Write(item.SubscriptionNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        </div>\r\n                        <div class=\"col-6 col-md-2 mb-3 mb-xl-0\">\r\n                            <p class=\"mb-1 fw-bold\">");
#nullable restore
#line 15 "D:\Development\AdhariNew\Web\Views\Shared\_Subscriptions.cshtml"
                                               Write(SharedHtmlLocalizer["Status"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                            <span class=\"mb-0 bg-success text-light py-0 px-2 rounded-pill\">");
#nullable restore
#line 16 "D:\Development\AdhariNew\Web\Views\Shared\_Subscriptions.cshtml"
                                                                                       Write(item.SubscriptionStatusName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                        </div>\r\n                        <div class=\"col-6 col-md-2 mb-3 mb-xl-0\">\r\n                            <p class=\"mb-0 fw-bold\">");
#nullable restore
#line 19 "D:\Development\AdhariNew\Web\Views\Shared\_Subscriptions.cshtml"
                                               Write(SharedHtmlLocalizer["SubscriptionDate"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                            <p class=\"mb-0 text-dark\">");
#nullable restore
#line 20 "D:\Development\AdhariNew\Web\Views\Shared\_Subscriptions.cshtml"
                                                 Write(item.FormattedDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        </div>\r\n                        <div class=\"col-6 col-md-2 mb-3 mb-xl-0\">\r\n                            <p class=\"mb-0 fw-bold\">");
#nullable restore
#line 23 "D:\Development\AdhariNew\Web\Views\Shared\_Subscriptions.cshtml"
                                               Write(SharedHtmlLocalizer["SubscriptionAmount"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                            <p class=\"mb-0 text-dark\">");
#nullable restore
#line 24 "D:\Development\AdhariNew\Web\Views\Shared\_Subscriptions.cshtml"
                                                 Write(item.FormattedTotal);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        </div>\r\n                        <div class=\"col-12 col-md-4 mb-3 mb-xl-0\">\r\n                            <p class=\"mb-0 fw-bold\">");
#nullable restore
#line 27 "D:\Development\AdhariNew\Web\Views\Shared\_Subscriptions.cshtml"
                                               Write(SharedHtmlLocalizer["SubscriptionName"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                            <p class=\"mb-0 text-dark\">");
#nullable restore
#line 28 "D:\Development\AdhariNew\Web\Views\Shared\_Subscriptions.cshtml"
                                                 Write(item.SubscriptionTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                            <ul class=\"body-bg-secondary border border-secondary rounded-3 text-muted p-2 m-0\">\r\n");
#nullable restore
#line 30 "D:\Development\AdhariNew\Web\Views\Shared\_Subscriptions.cshtml"
                                 foreach (var pack in item.SubscriptionPackTitles)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <li class=\"d-flex justify-content-between mb-1\">\r\n                                        <p class=\"m-0\">");
#nullable restore
#line 33 "D:\Development\AdhariNew\Web\Views\Shared\_Subscriptions.cshtml"
                                                  Write(pack.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p> <span>");
#nullable restore
#line 33 "D:\Development\AdhariNew\Web\Views\Shared\_Subscriptions.cshtml"
                                                                        Write(pack.Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                    </li>\r\n");
#nullable restore
#line 35 "D:\Development\AdhariNew\Web\Views\Shared\_Subscriptions.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                            </ul>
                        </div>

                    </div>
                </div>
                <div class=""col-1 d-flex align-items-center justify-content-end pb-3 pb-xl-0"">
                    <i class=""fa fa-chevron-right""></i>
                </div>
            </div>
        </a>
    </div>
");
#nullable restore
#line 47 "D:\Development\AdhariNew\Web\Views\Shared\_Subscriptions.cshtml"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IOptions<RequestLocalizationOptions> LocOptions { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IStringLocalizer<SharedResource> SharedLocalizer { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IViewLocalizer localizer { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IHtmlLocalizer<SharedResource> SharedHtmlLocalizer { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Utility.Models.Frontend.Sales.SubscriptionModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591