#pragma checksum "D:\Development\WEB\Client\Adhari\Admin\Views\Customer\CashbackHistory.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e5e047fe170b006711e2568d03f7a14da21ce34a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Customer_CashbackHistory), @"mvc.1.0.view", @"/Views/Customer/CashbackHistory.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e5e047fe170b006711e2568d03f7a14da21ce34a", @"/Views/Customer/CashbackHistory.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"42e038141c336d185a06267799a2ba9686e6a703", @"/Views/_ViewImports.cshtml")]
    public class Views_Customer_CashbackHistory : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Data.Common.BaseEntityId>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/developer/js/Customer/cashbackHistory_Datatable.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral(" \r\n    <header class=\"page-header\">\r\n        <h2>Cashback History</h2>\r\n        <input type=\"hidden\"");
            BeginWriteAttribute("value", " value=\"", 133, "\"", 150, 1);
#nullable restore
#line 5 "D:\Development\WEB\Client\Adhari\Admin\Views\Customer\CashbackHistory.cshtml"
WriteAttributeValue("", 141, Model.Id, 141, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" id=""customerId"" /> 
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
        <div class=""col-12"">
            <!-- ========== Start Card Block ========== -->
            <section class=""card mb-3 border border-secondary"">
                <header class=""card-header d-flex justify-content-between align-items-center p-3 border-secondary body-bg-secondary"">
                    <h2 class=""card-title text-primary"">Cashback History</h2>
                    <div class=""card-actions d-flex position-static top-0 end-0"">

                    </div>
                </header>
                <div class=""card-body "">
                    <!-- ========== Start Table Block ========== -->
                    <table class=""table border-second");
            WriteLiteral(@"ary align-middle mobile-optimised datatable-default-"" id=""datatable-default-"">
                        <thead>
                            <tr>
                                <th data-priority=""0"" class=""text-center"">ID</th>
                                <th data-priority=""1""");
            BeginWriteAttribute("class", " class=\"", 1458, "\"", 1466, 0);
            EndWriteAttribute();
            WriteLiteral(">Date</th>\r\n                                <th data-priority=\"1\"");
            BeginWriteAttribute("class", " class=\"", 1532, "\"", 1540, 0);
            EndWriteAttribute();
            WriteLiteral(">Payment ID</th>\r\n                                <th data-priority=\"1\"");
            BeginWriteAttribute("class", " class=\"", 1612, "\"", 1620, 0);
            EndWriteAttribute();
            WriteLiteral(">Description</th>\r\n                                <th data-priority=\"2\"");
            BeginWriteAttribute("class", " class=\"", 1693, "\"", 1701, 0);
            EndWriteAttribute();
            WriteLiteral(">Credit</th>\r\n                                <th data-priority=\"3\"");
            BeginWriteAttribute("class", " class=\"", 1769, "\"", 1777, 0);
            EndWriteAttribute();
            WriteLiteral(">Debit</th>\r\n                                <th data-priority=\"4\"");
            BeginWriteAttribute("class", " class=\"", 1844, "\"", 1852, 0);
            EndWriteAttribute();
            WriteLiteral(@">Balance</th>
                            </tr>
                        </thead>
                        <tbody class=""border-secondary"">
                        </tbody>
                    </table>

                    <!-- ========== End Table Block ========== -->
                </div>
            </section>
            <!-- ========== End Card Block ========== -->
        </div>
    </div>
    <!-- Modals -->
    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e5e047fe170b006711e2568d03f7a14da21ce34a8896", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 51 "D:\Development\WEB\Client\Adhari\Admin\Views\Customer\CashbackHistory.cshtml"
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
            WriteLiteral("\r\n    <!-- end: page -->\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Data.Common.BaseEntityId> Html { get; private set; }
    }
}
#pragma warning restore 1591
