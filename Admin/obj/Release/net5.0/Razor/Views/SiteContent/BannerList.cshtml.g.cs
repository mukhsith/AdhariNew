#pragma checksum "D:\Development\WEB\Client\Adhari\Admin\Views\SiteContent\BannerList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "871a72023a5f5deb541ee0500dd8672da9b63c19"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_SiteContent_BannerList), @"mvc.1.0.view", @"/Views/SiteContent/BannerList.cshtml")]
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
#line 4 "D:\Development\WEB\Client\Adhari\Admin\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Development\WEB\Client\Adhari\Admin\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Development\WEB\Client\Adhari\Admin\Views\_ViewImports.cshtml"
using Microsoft.Extensions.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Development\WEB\Client\Adhari\Admin\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Builder;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Development\WEB\Client\Adhari\Admin\Views\_ViewImports.cshtml"
using Microsoft.Extensions.Options;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "D:\Development\WEB\Client\Adhari\Admin\Views\_ViewImports.cshtml"
using Admin;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "D:\Development\WEB\Client\Adhari\Admin\Views\_ViewImports.cshtml"
using Admin.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "D:\Development\WEB\Client\Adhari\Admin\Views\_ViewImports.cshtml"
using Utility.Models.Admin.Delivery;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "D:\Development\WEB\Client\Adhari\Admin\Views\_ViewImports.cshtml"
using Utility.Enum;

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "D:\Development\WEB\Client\Adhari\Admin\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "D:\Development\WEB\Client\Adhari\Admin\Views\_ViewImports.cshtml"
using Utility.Models.Admin.Sales;

#line default
#line hidden
#nullable disable
#nullable restore
#line 22 "D:\Development\WEB\Client\Adhari\Admin\Views\_ViewImports.cshtml"
using Utility.ResponseMapper;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"871a72023a5f5deb541ee0500dd8672da9b63c19", @"/Views/SiteContent/BannerList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"42e038141c336d185a06267799a2ba9686e6a703", @"/Views/_ViewImports.cshtml")]
    public class Views_SiteContent_BannerList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/developer/js/SiteContent/Banner_Datatable.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<header class=\"page-header\">\r\n    <h2>");
#nullable restore
#line 2 "D:\Development\WEB\Client\Adhari\Admin\Views\SiteContent\BannerList.cshtml"
   Write(SharedHtmlLocalizer["Banners"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h2>
    <div class=""right-wrapper text-end me-3"">
        <ol class=""breadcrumbs"">
            <li>
                <a href=""/home"">
                    <i class=""bx bx-home-alt""></i>
                </a>
            </li>
            
        </ol>
         
    </div>
</header>
<div class=""row"">
    <div class=""col"">
        <!-- ========== Start Card Block ========== -->
        <section class=""card mb-3 border border-secondary"">
            <header class=""card-header d-flex justify-content-between align-items-center p-3 border-secondary body-bg-secondary"">
                <h2 class=""card-title text-primary"">");
#nullable restore
#line 20 "D:\Development\WEB\Client\Adhari\Admin\Views\SiteContent\BannerList.cshtml"
                                               Write(SharedHtmlLocalizer["Banners"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n                <div class=\"card-actions d-flex position-static top-0 end-0\">\r\n                    <button class=\"btn btn-primary btn-sm \" onclick=\"window.location = \'/SiteContent/BannerAddEdit?id=0\';\">");
#nullable restore
#line 22 "D:\Development\WEB\Client\Adhari\Admin\Views\SiteContent\BannerList.cshtml"
                                                                                                                      Write(SharedHtmlLocalizer["AddNewBanner"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</button>

                </div>
            </header>
            <div class=""card-body "">
                <table class=""table border-secondary align-middle mb-0 mobile-optimised datatable-default-"" id=""datatable-default-"">
                    <thead>
                        <tr>
                            <th data-priority=""1""");
            BeginWriteAttribute("class", " class=\"", 1324, "\"", 1332, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 30 "D:\Development\WEB\Client\Adhari\Admin\Views\SiteContent\BannerList.cshtml"
                                                      Write(SharedHtmlLocalizer["ID"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th data-priority=\"2\"");
            BeginWriteAttribute("class", " class=\"", 1416, "\"", 1424, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 31 "D:\Development\WEB\Client\Adhari\Admin\Views\SiteContent\BannerList.cshtml"
                                                      Write(SharedHtmlLocalizer["DisplayOrder"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th data-priority=\"3\"");
            BeginWriteAttribute("class", " class=\"", 1518, "\"", 1526, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 32 "D:\Development\WEB\Client\Adhari\Admin\Views\SiteContent\BannerList.cshtml"
                                                      Write(SharedHtmlLocalizer["Status"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th data-priority=\"4\"");
            BeginWriteAttribute("class", " class=\"", 1614, "\"", 1622, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 33 "D:\Development\WEB\Client\Adhari\Admin\Views\SiteContent\BannerList.cshtml"
                                                      Write(SharedHtmlLocalizer["ImageEn"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th data-priority=\"5\"");
            BeginWriteAttribute("class", " class=\"", 1711, "\"", 1719, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 34 "D:\Development\WEB\Client\Adhari\Admin\Views\SiteContent\BannerList.cshtml"
                                                      Write(SharedHtmlLocalizer["ImageAr"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th data-priority=\"6\"");
            BeginWriteAttribute("class", " class=\"", 1808, "\"", 1816, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 35 "D:\Development\WEB\Client\Adhari\Admin\Views\SiteContent\BannerList.cshtml"
                                                      Write(SharedHtmlLocalizer["BannerType"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th data-priority=\"7\"");
            BeginWriteAttribute("class", " class=\"", 1908, "\"", 1916, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 36 "D:\Development\WEB\Client\Adhari\Admin\Views\SiteContent\BannerList.cshtml"
                                                      Write(SharedHtmlLocalizer["Content"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th data-priority=\"8\"");
            BeginWriteAttribute("class", " class=\"", 2005, "\"", 2013, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 37 "D:\Development\WEB\Client\Adhari\Admin\Views\SiteContent\BannerList.cshtml"
                                                      Write(SharedHtmlLocalizer["Actions"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</th>
                        </tr>
                    </thead>
                    <tbody class=""border-secondary"">
                        
                    </tbody>
                </table>
            </div>
        </section>
        <!-- ========== End Card Block ========== -->
    </div>
</div>



<!-- end: page -->
");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "871a72023a5f5deb541ee0500dd8672da9b63c1911659", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 53 "D:\Development\WEB\Client\Adhari\Admin\Views\SiteContent\BannerList.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
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
