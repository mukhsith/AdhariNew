#pragma checksum "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "45cc1ca63674a692a05ef82b2b48a572b1eb2e96"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Customer_CustomerList), @"mvc.1.0.view", @"/Views/Customer/CustomerList.cshtml")]
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
#line 4 "D:\Development\Adhari\Admin\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Development\Adhari\Admin\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Development\Adhari\Admin\Views\_ViewImports.cshtml"
using Microsoft.Extensions.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Development\Adhari\Admin\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Builder;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Development\Adhari\Admin\Views\_ViewImports.cshtml"
using Microsoft.Extensions.Options;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "D:\Development\Adhari\Admin\Views\_ViewImports.cshtml"
using Admin;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "D:\Development\Adhari\Admin\Views\_ViewImports.cshtml"
using Admin.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "D:\Development\Adhari\Admin\Views\_ViewImports.cshtml"
using Utility.Models.Admin.Delivery;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "D:\Development\Adhari\Admin\Views\_ViewImports.cshtml"
using Utility.Enum;

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "D:\Development\Adhari\Admin\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "D:\Development\Adhari\Admin\Views\_ViewImports.cshtml"
using Utility.Models.Admin.Sales;

#line default
#line hidden
#nullable disable
#nullable restore
#line 22 "D:\Development\Adhari\Admin\Views\_ViewImports.cshtml"
using Utility.ResponseMapper;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"45cc1ca63674a692a05ef82b2b48a572b1eb2e96", @"/Views/Customer/CustomerList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"42e038141c336d185a06267799a2ba9686e6a703", @"/Views/_ViewImports.cshtml")]
    public class Views_Customer_CustomerList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/developer/js/Customer/customer_Datatable.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
  
    // ViewData["Title"] = "Home";
    //Layout = "~/Views/Shared/_Layout.cshtml";


#line default
#line hidden
#nullable disable
            WriteLiteral("<header class=\"page-header\">\r\n    <h2>");
#nullable restore
#line 7 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
   Write(SharedHtmlLocalizer["Customers"]);

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
    <div class=""col-12"">
        <!-- ========== Start Card Block ========== -->
        <section class=""card mb-3 border border-secondary"">
            <header class=""card-header d-flex justify-content-between align-items-center p-3 border-secondary body-bg-secondary"">
                <h2 class=""card-title text-primary"">");
#nullable restore
#line 23 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
                                               Write(SharedHtmlLocalizer["FilterBy"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h2>
                <div class=""card-actions d-flex position-static top-0 end-0"">

                </div>
            </header>
            <div class=""card-body "">
                <div class=""row"">
                    <div class=""col-12 col-lg-3 mb-3"">
                        <!-- ========== Start Simple Input Field ========== -->

                        <label");
            BeginWriteAttribute("for", " for=\"", 1191, "\"", 1197, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"form-label\">");
#nullable restore
#line 33 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
                                                    Write(SharedHtmlLocalizer["CustomerName"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                        <input type=\"text\" class=\"form-control form-control-lg rounded-4 border-secondary \" name=\"customerName\" id=\"customerName\"");
            BeginWriteAttribute("aria-describedby", " aria-describedby=\"", 1409, "\"", 1464, 1);
#nullable restore
#line 34 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
WriteAttributeValue("", 1428, SharedHtmlLocalizer["CustomerName"], 1428, 36, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("placeholder", " placeholder=\"", 1465, "\"", 1479, 0);
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 1480, "\"", 1488, 0);
            EndWriteAttribute();
            WriteLiteral(@">


                        <!-- ========== End Simple Input Field ========== -->
                    </div>
                    <div class=""col-12 col-lg-3 mb-3"">
                        <!-- ========== Start Simple Input Field ========== -->

                        <label");
            BeginWriteAttribute("for", " for=\"", 1772, "\"", 1778, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"form-label\">");
#nullable restore
#line 42 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
                                                    Write(SharedHtmlLocalizer["MobileNumber"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                        <input type=\"tel\" class=\"form-control form-control-lg rounded-4 border-secondary \" name=\"customerMobile\" id=\"customerMobile\"");
            BeginWriteAttribute("aria-describedby", " aria-describedby=\"", 1993, "\"", 2048, 1);
#nullable restore
#line 43 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
WriteAttributeValue("", 2012, SharedHtmlLocalizer["MobileNumber"], 2012, 36, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("placeholder", " placeholder=\"", 2049, "\"", 2063, 0);
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 2064, "\"", 2072, 0);
            EndWriteAttribute();
            WriteLiteral(@">


                        <!-- ========== End Simple Input Field ========== -->
                    </div>
                    <div class=""col-12 col-lg-3 mb-3"">
                        <!-- ========== Start Simple Input Field ========== -->

                        <label");
            BeginWriteAttribute("for", " for=\"", 2356, "\"", 2362, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"form-label\">");
#nullable restore
#line 51 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
                                                    Write(SharedHtmlLocalizer["CustomerEmail"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                        <input type=\"email\" class=\"form-control form-control-lg rounded-4 border-secondary \" name=\"customerEmail\" id=\"customerEmail\"");
            BeginWriteAttribute("aria-describedby", " aria-describedby=\"", 2578, "\"", 2634, 1);
#nullable restore
#line 52 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
WriteAttributeValue("", 2597, SharedHtmlLocalizer["CustomerEmail"], 2597, 37, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("placeholder", " placeholder=\"", 2635, "\"", 2649, 0);
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 2650, "\"", 2658, 0);
            EndWriteAttribute();
            WriteLiteral(@">


                        <!-- ========== End Simple Input Field ========== -->
                    </div>
                    <div class=""col-12 col-lg-3 mb-3"">
                        <!-- ========== Start Simple Select Field Block ========== -->
                        <label");
            BeginWriteAttribute("for", " for=\"", 2947, "\"", 2953, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"form-label\">");
#nullable restore
#line 59 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
                                                    Write(SharedHtmlLocalizer["CustomerType"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                        <select class=\"form-select form-select-lg rounded-4 border-secondary\" name=\"customerTypeList\" id=\"customerTypeList\"");
            BeginWriteAttribute("aria-describedby", " aria-describedby=\"", 3159, "\"", 3214, 1);
#nullable restore
#line 60 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
WriteAttributeValue("", 3178, SharedHtmlLocalizer["CustomerType"], 3178, 36, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "45cc1ca63674a692a05ef82b2b48a572b1eb2e9612699", async() => {
#nullable restore
#line 61 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
                               Write(SharedHtmlLocalizer["All"]);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "45cc1ca63674a692a05ef82b2b48a572b1eb2e9613856", async() => {
#nullable restore
#line 62 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
                               Write(SharedHtmlLocalizer["B2B"]);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "45cc1ca63674a692a05ef82b2b48a572b1eb2e9615013", async() => {
#nullable restore
#line 63 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
                               Write(SharedHtmlLocalizer["B2C"]);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                        </select>


                        <!-- ========== End Simple Select Field Block ========== -->
                    </div>
                    <div class=""col-12 "">
                        <a href=""#"" class=""btn btn-primary btn-lg fw-bold "" onclick=""searchDataTable()"">");
#nullable restore
#line 70 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
                                                                                                   Write(SharedHtmlLocalizer["Search"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</a>
                    </div>
                </div>
            </div>
        </section>
        <!-- ========== End Card Block ========== -->
    </div>
    <div class=""col-12"">
        <!-- ========== Start Card Block ========== -->
        <section class=""card mb-3 border border-secondary"">
            <header class=""card-header d-flex justify-content-between align-items-center p-3 border-secondary body-bg-secondary"">
                <h2 class=""card-title text-primary"">");
#nullable restore
#line 81 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
                                               Write(SharedHtmlLocalizer["Customers"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h2>
                <div class=""card-actions d-flex position-static top-0 end-0"">

                </div>
            </header>
            <div class=""card-body "">
                <!-- ========== Start Table Block ========== -->
                <table class=""table border-secondary align-middle mobile-optimised datatable-default-"" id=""datatable-default-"">
                    <thead>
                        <tr>
                            <th data-priority=""0"" class=""text-center"">");
#nullable restore
#line 91 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
                                                                 Write(SharedHtmlLocalizer["ID"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th data-priority=\"1\"");
            BeginWriteAttribute("class", " class=\"", 4874, "\"", 4882, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 92 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
                                                      Write(SharedHtmlLocalizer["Status"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th data-priority=\"1\"");
            BeginWriteAttribute("class", " class=\"", 4970, "\"", 4978, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 93 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
                                                      Write(SharedHtmlLocalizer["CustomerName"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th data-priority=\"1\"");
            BeginWriteAttribute("class", " class=\"", 5072, "\"", 5080, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 94 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
                                                      Write(SharedHtmlLocalizer["CustomerMobile"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th data-priority=\"2\"");
            BeginWriteAttribute("class", " class=\"", 5176, "\"", 5184, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 95 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
                                                      Write(SharedHtmlLocalizer["CustomerEmail"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th data-priority=\"3\"");
            BeginWriteAttribute("class", " class=\"", 5279, "\"", 5287, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 96 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
                                                      Write(SharedHtmlLocalizer["RegistrationDate"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th data-priority=\"4\"");
            BeginWriteAttribute("class", " class=\"", 5385, "\"", 5393, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 97 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
                                                      Write(SharedHtmlLocalizer["TotalOrders"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th data-priority=\"5\"");
            BeginWriteAttribute("class", " class=\"", 5486, "\"", 5494, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 98 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
                                                      Write(SharedHtmlLocalizer["CustomerType"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th data-priority=\"6\"");
            BeginWriteAttribute("class", " class=\"", 5588, "\"", 5596, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 99 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
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

                <!-- ========== End Table Block ========== -->
            </div>
        </section>
        <!-- ========== End Card Block ========== -->
    </div>
</div>
<!-- Modals -->
");
#nullable restore
#line 113 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
Write(await Html.PartialAsync("_DisplayWalletModalPartial"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 114 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
Write(await Html.PartialAsync("_DisplayAddressesModalPartial"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 115 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
Write(await Html.PartialAsync("_ChangeCustomerTypeModalPartial"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "45cc1ca63674a692a05ef82b2b48a572b1eb2e9623239", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 116 "D:\Development\Adhari\Admin\Views\Customer\CustomerList.cshtml"
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
            WriteLiteral("\r\n<!-- end: page -->\r\n");
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
