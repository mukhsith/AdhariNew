#pragma checksum "D:\Development\AdhariNew\Admin\Views\Product\ItemSizeAddEdit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "18dc4f85072fd269a550044392c317df9914af95"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Product_ItemSizeAddEdit), @"mvc.1.0.view", @"/Views/Product/ItemSizeAddEdit.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"18dc4f85072fd269a550044392c317df9914af95", @"/Views/Product/ItemSizeAddEdit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"42e038141c336d185a06267799a2ba9686e6a703", @"/Views/_ViewImports.cshtml")]
    public class Views_Product_ItemSizeAddEdit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Data.Common.BaseEntityId>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("dataForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", new global::Microsoft.AspNetCore.Html.HtmlString("dataForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/developer/js/Product/ItemSize_CRUD.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "D:\Development\AdhariNew\Admin\Views\Product\ItemSizeAddEdit.cshtml"
     Write(@Model.Id > 0 ? "Edit Item Size - " + @Model.Id : @SharedHtmlLocalizer["NewItemSize"].Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("  </h2>\r\n    <input type=\"hidden\"");
            BeginWriteAttribute("value", " value=\"", 199, "\"", 216, 1);
#nullable restore
#line 4 "D:\Development\AdhariNew\Admin\Views\Product\ItemSizeAddEdit.cshtml"
WriteAttributeValue("", 207, Model.Id, 207, 9, false);

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
#line 20 "D:\Development\AdhariNew\Admin\Views\Product\ItemSizeAddEdit.cshtml"
                                               Write(SharedHtmlLocalizer["ItemSizeDetails"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n                <div class=\"card-actions d-flex position-static top-0 end-0\">\r\n                </div>\r\n            </header>\r\n            <div class=\"card-body \">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "18dc4f85072fd269a550044392c317df9914af958736", async() => {
                WriteLiteral("\r\n                    ");
#nullable restore
#line 26 "D:\Development\AdhariNew\Admin\Views\Product\ItemSizeAddEdit.cshtml"
               Write(await Html.PartialAsync("_FormHiddenFieldsPartial"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                    <div class=""card-body "">
                        <div class=""row"">
                            <div class=""col-12 col-lg-6 mb-3"">
                                <!-- ========== Start Simple Input Field ========== -->
                                <label for=""nameEn"" class=""form-label"">");
#nullable restore
#line 31 "D:\Development\AdhariNew\Admin\Views\Product\ItemSizeAddEdit.cshtml"
                                                                  Write(SharedHtmlLocalizer["TitleEn"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("<span class=\" text-danger\">*</span></label>\r\n                                <input type=\"text\" class=\"form-control form-control-lg rounded-4 border-secondary \" name=\"nameEn\" id=\"nameEn\"");
                BeginWriteAttribute("aria-describedby", " aria-describedby=\"", 1744, "\"", 1794, 1);
#nullable restore
#line 32 "D:\Development\AdhariNew\Admin\Views\Product\ItemSizeAddEdit.cshtml"
WriteAttributeValue("", 1763, SharedHtmlLocalizer["TitleEn"], 1763, 31, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("placeholder", " placeholder=\"", 1795, "\"", 1837, 1);
#nullable restore
#line 32 "D:\Development\AdhariNew\Admin\Views\Product\ItemSizeAddEdit.cshtml"
WriteAttributeValue("", 1809, SharedHtmlLocalizer["Size"], 1809, 28, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("value", " value=\"", 1838, "\"", 1846, 0);
                EndWriteAttribute();
                WriteLiteral(" required>\r\n                                <div class=\"invalid-feedback\">");
#nullable restore
#line 33 "D:\Development\AdhariNew\Admin\Views\Product\ItemSizeAddEdit.cshtml"
                                                         Write(SharedHtmlLocalizer["PleaseEnterSize"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</div>
                                <!-- ========== End Simple Input Field ========== -->
                            </div>
                            <div class=""col-12 col-lg-6 mb-3"">
                                <!-- ========== Start Simple Input Field ========== -->
                                <label for=""nameAr"" class=""form-label"">");
#nullable restore
#line 38 "D:\Development\AdhariNew\Admin\Views\Product\ItemSizeAddEdit.cshtml"
                                                                  Write(SharedHtmlLocalizer["TitleAr"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("<span class=\" text-danger\">*</span></label>\r\n                                <input type=\"text\" dir=\"rtl\" class=\"form-control form-control-lg rounded-4 border-secondary \" name=\"nameAr\" id=\"nameAr\"");
                BeginWriteAttribute("aria-describedby", " aria-describedby=\"", 2542, "\"", 2592, 1);
#nullable restore
#line 39 "D:\Development\AdhariNew\Admin\Views\Product\ItemSizeAddEdit.cshtml"
WriteAttributeValue("", 2561, SharedHtmlLocalizer["TitleAr"], 2561, 31, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("placeholder", " placeholder=\"", 2593, "\"", 2635, 1);
#nullable restore
#line 39 "D:\Development\AdhariNew\Admin\Views\Product\ItemSizeAddEdit.cshtml"
WriteAttributeValue("", 2607, SharedHtmlLocalizer["Size"], 2607, 28, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("value", " value=\"", 2636, "\"", 2644, 0);
                EndWriteAttribute();
                WriteLiteral(" required>\r\n                                <div class=\"invalid-feedback\">");
#nullable restore
#line 40 "D:\Development\AdhariNew\Admin\Views\Product\ItemSizeAddEdit.cshtml"
                                                         Write(SharedHtmlLocalizer["PleaseEnterSize"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</div>
                                <!-- ========== End Simple Input Field ========== -->
                            </div>
                        </div>
                    </div>
                    <div class=""row mt-3"">
                        <div class=""col-12"">
                            <button id=""btnSave"" class=""btn btn-primary btn-lg save-btn fw-bold"">");
#nullable restore
#line 47 "D:\Development\AdhariNew\Admin\Views\Product\ItemSizeAddEdit.cshtml"
                                                                                            Write(SharedHtmlLocalizer["Save"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("</button>\r\n                            <button type=\"button\" class=\"btn btn-warning\" onclick=\"history.go(-1)\">");
#nullable restore
#line 48 "D:\Development\AdhariNew\Admin\Views\Product\ItemSizeAddEdit.cshtml"
                                                                                              Write(SharedHtmlLocalizer["Back"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("</button>\r\n                        </div>\r\n                    </div>\r\n                ");
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "18dc4f85072fd269a550044392c317df9914af9516227", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
#nullable restore
#line 58 "D:\Development\AdhariNew\Admin\Views\Product\ItemSizeAddEdit.cshtml"
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
