#pragma checksum "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b89c5fc6211a0876321785754e3cf6e356848d41"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_WalletPackage_AddEdit), @"mvc.1.0.view", @"/Views/WalletPackage/AddEdit.cshtml")]
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
#line 4 "D:\Development\AdhariNew\Admin\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Development\AdhariNew\Admin\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Development\AdhariNew\Admin\Views\_ViewImports.cshtml"
using Microsoft.Extensions.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Development\AdhariNew\Admin\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Builder;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Development\AdhariNew\Admin\Views\_ViewImports.cshtml"
using Microsoft.Extensions.Options;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "D:\Development\AdhariNew\Admin\Views\_ViewImports.cshtml"
using Admin;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "D:\Development\AdhariNew\Admin\Views\_ViewImports.cshtml"
using Admin.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "D:\Development\AdhariNew\Admin\Views\_ViewImports.cshtml"
using Utility.Models.Admin.Delivery;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "D:\Development\AdhariNew\Admin\Views\_ViewImports.cshtml"
using Utility.Enum;

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "D:\Development\AdhariNew\Admin\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "D:\Development\AdhariNew\Admin\Views\_ViewImports.cshtml"
using Utility.Models.Admin.Sales;

#line default
#line hidden
#nullable disable
#nullable restore
#line 22 "D:\Development\AdhariNew\Admin\Views\_ViewImports.cshtml"
using Utility.ResponseMapper;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b89c5fc6211a0876321785754e3cf6e356848d41", @"/Views/WalletPackage/AddEdit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"42e038141c336d185a06267799a2ba9686e6a703", @"/Views/_ViewImports.cshtml")]
    public class Views_WalletPackage_AddEdit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Data.Common.BaseEntityId>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("dataForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", new global::Microsoft.AspNetCore.Html.HtmlString("dataForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/developer/js/WalletPackage/WalletPackage_CRUD.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<header class=\"page-header\">\r\n    <h2> ");
#nullable restore
#line 8 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
     Write(@Model.Id > 0 ? @SharedHtmlLocalizer["EditCard"] : @SharedHtmlLocalizer["NewCard"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n    <input type=\"hidden\"");
            BeginWriteAttribute("value", " value=\"", 318, "\"", 335, 1);
#nullable restore
#line 9 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
WriteAttributeValue("", 326, Model.Id, 326, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" id=""Id"" />
    <div class=""end-wrapper text-end me-5"">
        <ol class=""breadcrumbs"">
            <li>
                <a href=""/home"">
                    <i class=""fas fa-home""></i>
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
#line 25 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
                                               Write(SharedHtmlLocalizer["CardDetails"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n                <div class=\"card-actions d-flex position-static top-0 end-0\">\r\n\r\n                </div>\r\n            </header>\r\n            <div class=\"card-body \">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b89c5fc6211a0876321785754e3cf6e356848d418720", async() => {
                WriteLiteral("\r\n                    ");
#nullable restore
#line 32 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
               Write(await Html.PartialAsync("_FormHiddenFieldsPartial"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                    <div class=""card-body "">
                        <div class=""row"">
                            <div class=""col-12 col-lg-6 mb-3"">
                                <!-- ========== Start Simple Input Field ========== -->

                                <label for=""cardNameEn"" class=""form-label"">");
#nullable restore
#line 38 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
                                                                      Write(SharedHtmlLocalizer["TitleEn"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("<span class=\" text-danger\">*</span></label>\r\n                                <input type=\"text\" class=\"form-control form-control-lg rounded-4 border-secondary \"\r\n                                       name=\"nameEn\" id=\"nameEn\"");
                BeginWriteAttribute("aria-describedby", " aria-describedby=\"", 1907, "\"", 1957, 1);
#nullable restore
#line 40 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
WriteAttributeValue("", 1926, SharedHtmlLocalizer["TitleEn"], 1926, 31, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("placeholder", " placeholder=\"", 1958, "\"", 2018, 1);
#nullable restore
#line 40 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
WriteAttributeValue("", 1972, SharedHtmlLocalizer["PleaseEnterTheCardName"], 1972, 46, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral("  >\r\n\r\n                                <div class=\"invalid-feedback\">");
#nullable restore
#line 42 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
                                                         Write(SharedHtmlLocalizer["PleaseEnterTheCardName"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</div>
                                <!-- ========== End Simple Input Field ========== -->
                            </div>
                            <div class=""col-12 col-lg-6 mb-3"">
                                <!-- ========== Start Simple Input Field ========== -->

                                <label for=""cardNameAr"" class=""form-label"">");
#nullable restore
#line 48 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
                                                                      Write(SharedHtmlLocalizer["TitleAr"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("<span class=\" text-danger\">*</span></label>\r\n                                <input type=\"text\" dir=\"rtl\" class=\"form-control form-control-lg rounded-4 border-secondary \"\r\n                                       name=\"nameAr\" id=\"nameAr\"");
                BeginWriteAttribute("aria-describedby", " aria-describedby=\"", 2762, "\"", 2812, 1);
#nullable restore
#line 50 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
WriteAttributeValue("", 2781, SharedHtmlLocalizer["TitleAr"], 2781, 31, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("placeholder", " placeholder=\"", 2813, "\"", 2873, 1);
#nullable restore
#line 50 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
WriteAttributeValue("", 2827, SharedHtmlLocalizer["PleaseEnterTheCardName"], 2827, 46, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n\r\n                                <div class=\"invalid-feedback\">");
#nullable restore
#line 52 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
                                                         Write(SharedHtmlLocalizer["PleaseEnterTheCardName"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</div>
                                <!-- ========== End Simple Input Field ========== -->
                            </div>
                            <div class=""col-12 col-lg-6 mb-3"">
                                <!-- ========== Start Simple Text Area ========== -->
                                <label for=""cardDescEn"" class=""form-label "">");
#nullable restore
#line 57 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
                                                                       Write(SharedHtmlLocalizer["DescriptionEn"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("<span class=\" text-danger\">*</span></label>\r\n                                <textarea class=\"form-control form-control-lg rounded-4 border-secondary \"\r\n                                          name=\"descriptionEn\" id=\"descriptionEn\"");
                BeginWriteAttribute("aria-describedby", " aria-describedby=\"", 3616, "\"", 3672, 1);
#nullable restore
#line 59 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
WriteAttributeValue("", 3635, SharedHtmlLocalizer["DescriptionEn"], 3635, 37, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("placeholder", " placeholder=\"", 3673, "\"", 3740, 1);
#nullable restore
#line 59 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
WriteAttributeValue("", 3687, SharedHtmlLocalizer["PleaseEnterTheCardDescription"], 3687, 53, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" rows=\"3\"></textarea>\r\n\r\n                                <div class=\"invalid-feedback\">");
#nullable restore
#line 61 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
                                                         Write(SharedHtmlLocalizer["PleaseEnterTheCardDescription"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</div>
                                <!-- ========== End Simple Text Area ========== -->
                            </div>
                            <div class=""col-12 col-lg-6 mb-3"">
                                <!-- ========== Start Simple Text Area ========== -->
                                <label for=""cardDescAr"" class=""form-label "">");
#nullable restore
#line 66 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
                                                                       Write(SharedHtmlLocalizer["DescriptionAr"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("<span class=\" text-danger\">*</span></label>\r\n                                <textarea dir=\"rtl\" class=\"form-control form-control-lg rounded-4 border-secondary \"\r\n                                          name=\"descriptionAr\" id=\"descriptionAr\"");
                BeginWriteAttribute("aria-describedby", " aria-describedby=\"", 4518, "\"", 4574, 1);
#nullable restore
#line 68 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
WriteAttributeValue("", 4537, SharedHtmlLocalizer["DescriptionAr"], 4537, 37, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("placeholder", " placeholder=\"", 4575, "\"", 4642, 1);
#nullable restore
#line 68 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
WriteAttributeValue("", 4589, SharedHtmlLocalizer["PleaseEnterTheCardDescription"], 4589, 53, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" rows=\"3\" ></textarea>\r\n\r\n                                <div class=\"invalid-feedback\">");
#nullable restore
#line 70 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
                                                         Write(SharedHtmlLocalizer["PleaseEnterTheCardDescription"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</div>
                                <!-- ========== End Simple Text Area ========== -->
                            </div>
                            <div class=""col-12 col-lg-3 mb-3"">
                                <!-- ========== Start Simple Input Field ========== -->

                                <label for=""Amount"" class=""form-label"">");
#nullable restore
#line 76 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
                                                                  Write(SharedHtmlLocalizer["Amount"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("<span class=\" text-danger\">*</span></label>\r\n                                <input type=\"number\" class=\"form-control form-control-lg rounded-4 border-secondary \"\r\n                                       name=\"amount\" id=\"amount\"");
                BeginWriteAttribute("aria-describedby", " aria-describedby=\"", 5397, "\"", 5446, 1);
#nullable restore
#line 78 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
WriteAttributeValue("", 5416, SharedHtmlLocalizer["Amount"], 5416, 30, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("placeholder", " placeholder=\"", 5447, "\"", 5509, 1);
#nullable restore
#line 78 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
WriteAttributeValue("", 5461, SharedHtmlLocalizer["PleaseEnterTheCardAmount"], 5461, 48, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" value=\"0\" min=\"0\" required>\r\n\r\n                                <div class=\"invalid-feedback\">");
#nullable restore
#line 80 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
                                                         Write(SharedHtmlLocalizer["PleaseEnterTheCardAmount"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</div>
                                <!-- ========== End Simple Input Field ========== -->
                            </div>

                            <div class=""col-12 col-lg-3 mb-3"">
                                <!-- ========== Start Simple Input Field ========== -->

                                <label for=""Credit"" class=""form-label"">");
#nullable restore
#line 87 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
                                                                  Write(SharedHtmlLocalizer["Credit"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("<span class=\" text-danger\">*</span></label>\r\n                                <input type=\"number\" class=\"form-control form-control-lg rounded-4 border-secondary \"\r\n                                       name=\"walletAmount\" id=\"walletAmount\"");
                BeginWriteAttribute("aria-describedby", " aria-describedby=\"", 6281, "\"", 6330, 1);
#nullable restore
#line 89 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
WriteAttributeValue("", 6300, SharedHtmlLocalizer["Credit"], 6300, 30, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("placeholder", " placeholder=\"", 6331, "\"", 6398, 1);
#nullable restore
#line 89 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
WriteAttributeValue("", 6345, SharedHtmlLocalizer["PleaseEnterWalletCreditAmount"], 6345, 53, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" \r\n                                       value=\"0\" min=\"0\" required>\r\n\r\n                                <div class=\"invalid-feedback\">");
#nullable restore
#line 92 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
                                                         Write(SharedHtmlLocalizer["PleaseEnterWalletCreditAmount"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</div>
                                <!-- ========== End Simple Input Field ========== -->
                            </div>


                        </div>
                        <div class=""row mt-3"">
                            <div class=""col-12"">
                                <button id=""btnSave"" class=""btn btn-primary btn-lg save-btn fw-bold"">");
#nullable restore
#line 100 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
                                                                                                Write(SharedHtmlLocalizer["Save"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("</button>\r\n                                <button type=\"button\" class=\"btn btn-warning\" onclick=\"history.go(-1)\">");
#nullable restore
#line 101 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
                                                                                                  Write(SharedHtmlLocalizer["Back"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("</button>\r\n\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n        </section>\r\n        <!-- ========== End Card Block ========== -->\r\n    </div>\r\n</div>\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b89c5fc6211a0876321785754e3cf6e356848d4124555", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
#nullable restore
#line 113 "D:\Development\AdhariNew\Admin\Views\WalletPackage\AddEdit.cshtml"
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
            WriteLiteral("\r\n\r\n");
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