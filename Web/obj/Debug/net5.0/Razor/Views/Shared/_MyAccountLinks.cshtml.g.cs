#pragma checksum "D:\Development\AdhariNew\Web\Views\Shared\_MyAccountLinks.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "68a60635abad5a6ea252bc5f13edc81443808217"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__MyAccountLinks), @"mvc.1.0.view", @"/Views/Shared/_MyAccountLinks.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"68a60635abad5a6ea252bc5f13edc81443808217", @"/Views/Shared/_MyAccountLinks.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fd8610713b70f7256c3429c827c060fa488c518d", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__MyAccountLinks : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<li><a class=\"dropdown-item text-primary my-1 rounded-3 fs-6\"");
            BeginWriteAttribute("href", " href=\"", 191, "\"", 224, 1);
#nullable restore
#line 6 "D:\Development\AdhariNew\Web\Views\Shared\_MyAccountLinks.cshtml"
WriteAttributeValue("", 198, Url.RouteUrl("myprofile"), 198, 26, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 6 "D:\Development\AdhariNew\Web\Views\Shared\_MyAccountLinks.cshtml"
                                                                                           Write(SharedHtmlLocalizer["MyProfile"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></li>\r\n<li>\r\n    <hr class=\"dropdown-divider body-bg-secondary border-secondary my-0\">\r\n</li>\r\n<li><a class=\"dropdown-item text-primary my-1 rounded-3 fs-6\"");
            BeginWriteAttribute("href", " href=\"", 419, "\"", 451, 1);
#nullable restore
#line 10 "D:\Development\AdhariNew\Web\Views\Shared\_MyAccountLinks.cshtml"
WriteAttributeValue("", 426, Url.RouteUrl("myorders"), 426, 25, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 10 "D:\Development\AdhariNew\Web\Views\Shared\_MyAccountLinks.cshtml"
                                                                                          Write(SharedHtmlLocalizer["MyOrders"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></li>\r\n<li>\r\n    <hr class=\"dropdown-divider body-bg-secondary border-secondary my-0\">\r\n</li>\r\n<li><a class=\"dropdown-item text-primary my-1 rounded-3 fs-6\"");
            BeginWriteAttribute("href", " href=\"", 645, "\"", 684, 1);
#nullable restore
#line 14 "D:\Development\AdhariNew\Web\Views\Shared\_MyAccountLinks.cshtml"
WriteAttributeValue("", 652, Url.RouteUrl("mysubscriptions"), 652, 32, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 14 "D:\Development\AdhariNew\Web\Views\Shared\_MyAccountLinks.cshtml"
                                                                                                 Write(SharedHtmlLocalizer["MySubscriptions"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></li>\r\n<li>\r\n    <hr class=\"dropdown-divider body-bg-secondary border-secondary my-0\">\r\n</li>\r\n<li><a class=\"dropdown-item text-primary my-1 rounded-3 fs-6\"");
            BeginWriteAttribute("href", " href=\"", 885, "\"", 920, 1);
#nullable restore
#line 18 "D:\Development\AdhariNew\Web\Views\Shared\_MyAccountLinks.cshtml"
WriteAttributeValue("", 892, Url.RouteUrl("myaddresses"), 892, 28, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 18 "D:\Development\AdhariNew\Web\Views\Shared\_MyAccountLinks.cshtml"
                                                                                             Write(SharedHtmlLocalizer["MyAddresses"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></li>\r\n<li>\r\n    <hr class=\"dropdown-divider body-bg-secondary border-secondary my-0\">\r\n</li>\r\n<li><a class=\"dropdown-item text-primary my-1 rounded-3 fs-6\"");
            BeginWriteAttribute("href", " href=\"", 1117, "\"", 1157, 1);
#nullable restore
#line 22 "D:\Development\AdhariNew\Web\Views\Shared\_MyAccountLinks.cshtml"
WriteAttributeValue("", 1124, Url.RouteUrl("favoriteproducts"), 1124, 33, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 22 "D:\Development\AdhariNew\Web\Views\Shared\_MyAccountLinks.cshtml"
                                                                                                  Write(SharedHtmlLocalizer["MyFavorites"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></li>\r\n<li>\r\n    <hr class=\"dropdown-divider body-bg-secondary border-secondary my-0\">\r\n</li>\r\n<li><a class=\"dropdown-item text-primary my-1 rounded-3 fs-6\"");
            BeginWriteAttribute("href", " href=\"", 1354, "\"", 1386, 1);
#nullable restore
#line 26 "D:\Development\AdhariNew\Web\Views\Shared\_MyAccountLinks.cshtml"
WriteAttributeValue("", 1361, Url.RouteUrl("mywallet"), 1361, 25, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 26 "D:\Development\AdhariNew\Web\Views\Shared\_MyAccountLinks.cshtml"
                                                                                          Write(SharedHtmlLocalizer["MyWallet"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></li>\r\n<li>\r\n    <hr class=\"dropdown-divider body-bg-secondary border-secondary my-0\">\r\n</li>\r\n<li><a class=\"dropdown-item text-primary my-1 rounded-3 fs-6\"");
            BeginWriteAttribute("href", " href=\"", 1580, "\"", 1614, 1);
#nullable restore
#line 30 "D:\Development\AdhariNew\Web\Views\Shared\_MyAccountLinks.cshtml"
WriteAttributeValue("", 1587, Url.RouteUrl("mycashback"), 1587, 27, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 30 "D:\Development\AdhariNew\Web\Views\Shared\_MyAccountLinks.cshtml"
                                                                                            Write(SharedHtmlLocalizer["MyCashback"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></li>\r\n<li>\r\n    <hr class=\"dropdown-divider body-bg-secondary border-secondary my-0\">\r\n</li>\r\n<li><a class=\"dropdown-item text-primary my-1 rounded-3 fs-6\"");
            BeginWriteAttribute("href", " href=\"", 1810, "\"", 1849, 1);
#nullable restore
#line 34 "D:\Development\AdhariNew\Web\Views\Shared\_MyAccountLinks.cshtml"
WriteAttributeValue("", 1817, Url.RouteUrl("mynotifications"), 1817, 32, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 34 "D:\Development\AdhariNew\Web\Views\Shared\_MyAccountLinks.cshtml"
                                                                                                 Write(SharedHtmlLocalizer["MyNotifications"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></li>\r\n<li>\r\n    <hr class=\"dropdown-divider body-bg-secondary border-secondary my-0\">\r\n</li>\r\n<li><a class=\"dropdown-item text-primary my-1 rounded-3 fs-6\"");
            BeginWriteAttribute("href", " href=\"", 2050, "\"", 2080, 1);
#nullable restore
#line 38 "D:\Development\AdhariNew\Web\Views\Shared\_MyAccountLinks.cshtml"
WriteAttributeValue("", 2057, Url.RouteUrl("logout"), 2057, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 38 "D:\Development\AdhariNew\Web\Views\Shared\_MyAccountLinks.cshtml"
                                                                                        Write(SharedHtmlLocalizer["SignOut"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></li>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
