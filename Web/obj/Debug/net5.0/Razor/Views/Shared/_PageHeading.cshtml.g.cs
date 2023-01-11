#pragma checksum "D:\Development\AdhariNew\Web\Views\Shared\_PageHeading.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cf2399f36146c82b3cfebbcc9f244a1a631ea13b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__PageHeading), @"mvc.1.0.view", @"/Views/Shared/_PageHeading.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cf2399f36146c82b3cfebbcc9f244a1a631ea13b", @"/Views/Shared/_PageHeading.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fd8610713b70f7256c3429c827c060fa488c518d", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__PageHeading : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<BreadcrumbModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Development\AdhariNew\Web\Views\Shared\_PageHeading.cshtml"
  
    var counter = 0;
    var title = @Model.Count() == 2 ? @Model[1].Title : @Model.Count() == 1 ? @Model.FirstOrDefault().Title : "";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<section class=""breadcrumb-bg d-flex"">
    <div class=""container d-flex flex-column justify-content-center align-items-center w-100"">
        <ul class=""trail-items breadcrumb d-block d-md-none"">
            <li class=""trail-item trail-begin"">
                <a href=""javascript:history.back(1);"">
                    <i class=""fa fa-angle-double-left""></i>");
#nullable restore
#line 12 "D:\Development\AdhariNew\Web\Views\Shared\_PageHeading.cshtml"
                                                      Write(SharedHtmlLocalizer["Back"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </a>\r\n            </li>\r\n        </ul>\r\n        <h3 class=\"text-center mb-1 mb-md-2\">");
#nullable restore
#line 16 "D:\Development\AdhariNew\Web\Views\Shared\_PageHeading.cshtml"
                                        Write(title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n        <nav class=\"d-flex justify-content-center\" style=\"--bs-breadcrumb-divider: \'|\';\" aria-label=\"breadcrumb\">\r\n            <ol class=\"breadcrumb justify-content-center\">\r\n                <li class=\"breadcrumb-item \">\r\n                    <a");
            BeginWriteAttribute("href", " href=\"", 927, "\"", 955, 1);
#nullable restore
#line 20 "D:\Development\AdhariNew\Web\Views\Shared\_PageHeading.cshtml"
WriteAttributeValue("", 934, Url.RouteUrl("home"), 934, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 20 "D:\Development\AdhariNew\Web\Views\Shared\_PageHeading.cshtml"
                                               Write(SharedHtmlLocalizer["Home"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n                </li>\r\n\r\n");
#nullable restore
#line 23 "D:\Development\AdhariNew\Web\Views\Shared\_PageHeading.cshtml"
                 foreach (var item in Model)
                {
                    counter++;

                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 27 "D:\Development\AdhariNew\Web\Views\Shared\_PageHeading.cshtml"
                     if (counter == Model.Count())//last item should not be active
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <li class=\"breadcrumb-item\" aria-current=\"page\"><a");
            BeginWriteAttribute("href", " href=\"", 1296, "\"", 1326, 1);
#nullable restore
#line 29 "D:\Development\AdhariNew\Web\Views\Shared\_PageHeading.cshtml"
WriteAttributeValue("", 1303, Url.RouteUrl(item.Url), 1303, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 29 "D:\Development\AdhariNew\Web\Views\Shared\_PageHeading.cshtml"
                                                                                                     Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></li>\r\n");
#nullable restore
#line 30 "D:\Development\AdhariNew\Web\Views\Shared\_PageHeading.cshtml"
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <li class=\"breadcrumb-item active\" aria-current=\"page\"><a");
            BeginWriteAttribute("href", " href=\"", 1503, "\"", 1519, 1);
#nullable restore
#line 33 "D:\Development\AdhariNew\Web\Views\Shared\_PageHeading.cshtml"
WriteAttributeValue("", 1510, item.Url, 1510, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 33 "D:\Development\AdhariNew\Web\Views\Shared\_PageHeading.cshtml"
                                                                                              Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></li>\r\n");
#nullable restore
#line 34 "D:\Development\AdhariNew\Web\Views\Shared\_PageHeading.cshtml"

                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 35 "D:\Development\AdhariNew\Web\Views\Shared\_PageHeading.cshtml"
                     

                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </ol>\r\n        </nav>\r\n    </div>\r\n</section>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<BreadcrumbModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591