#pragma checksum "D:\Development\Adhari\Admin\Views\SiteContent\TermsConditionEdit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "353d7bc76be323696eff7cc56acd34e59d5963fd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_SiteContent_TermsConditionEdit), @"mvc.1.0.view", @"/Views/SiteContent/TermsConditionEdit.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"353d7bc76be323696eff7cc56acd34e59d5963fd", @"/Views/SiteContent/TermsConditionEdit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"42e038141c336d185a06267799a2ba9686e6a703", @"/Views/_ViewImports.cshtml")]
    public class Views_SiteContent_TermsConditionEdit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Data.Common.BaseEntityId>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("dataForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", new global::Microsoft.AspNetCore.Html.HtmlString("dataForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/developer/js/SiteContent/TermsCondition_CRUD.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("<header class=\"page-header\">\r\n    <h2>");
#nullable restore
#line 3 "D:\Development\Adhari\Admin\Views\SiteContent\TermsConditionEdit.cshtml"
   Write(SharedHtmlLocalizer["TermsAndConditions"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n    <input type=\"hidden\" id=\"typeId\"");
            BeginWriteAttribute("value", " value=\"", 156, "\"", 173, 1);
#nullable restore
#line 4 "D:\Development\Adhari\Admin\Views\SiteContent\TermsConditionEdit.cshtml"
WriteAttributeValue("", 164, Model.Id, 164, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />
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
#line 20 "D:\Development\Adhari\Admin\Views\SiteContent\TermsConditionEdit.cshtml"
                                               Write(SharedHtmlLocalizer["TermsAndConditions"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n                <div class=\"card-actions d-flex position-static top-0 end-0\">\r\n                    <!-- ========== Start ios7 Switch ========== -->\r\n\r\n");
            WriteLiteral("                    <!-- ========== End ios7 Switch ========== -->\r\n\r\n\r\n                </div>\r\n            </header>\r\n            <div class=\"card-body \">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "353d7bc76be323696eff7cc56acd34e59d5963fd8891", async() => {
                WriteLiteral("\r\n                    ");
#nullable restore
#line 35 "D:\Development\Adhari\Admin\Views\SiteContent\TermsConditionEdit.cshtml"
               Write(await Html.PartialAsync("_FormHiddenFieldsPartial"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                    <div class=""row"">
                        <div class=""col-12 col-lg-6 mb-3"">
                            <!-- ========== Start Summernote WYSIWYG ========== -->
                            <label for=""contentEn"" class=""form-label"">");
#nullable restore
#line 39 "D:\Development\Adhari\Admin\Views\SiteContent\TermsConditionEdit.cshtml"
                                                                 Write(SharedHtmlLocalizer["TermsAndConditionsTextEn"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</label>
                            <div class=""summernote "" name=""contentEn"" id=""contentEn""></div>

                            <!-- ========== End Summernote WYSIWYG ========== -->
                        </div>
                        <div class=""col-12 col-lg-6 mb-3"">
                            <!-- ========== Start Summernote WYSIWYG ========== -->
                            <label for=""contentAr"" class=""form-label"">");
#nullable restore
#line 46 "D:\Development\Adhari\Admin\Views\SiteContent\TermsConditionEdit.cshtml"
                                                                 Write(SharedHtmlLocalizer["TermsAndConditionsTextAr"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</label>
                            <div class=""summernote dir-rtl"" dir=""rtl"" name=""contentAr"" id=""contentAr""></div>


                            <!-- ========== End Summernote WYSIWYG ========== -->
                        </div>

                    </div>
                    <div class=""row mt-3"">
                        <div class=""col-12"">
                            <button id=""btnSave"" class=""btn btn-primary btn-lg save-btn  fw-bold"">");
#nullable restore
#line 56 "D:\Development\Adhari\Admin\Views\SiteContent\TermsConditionEdit.cshtml"
                                                                                             Write(SharedHtmlLocalizer["Save"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("</button>\r\n                            <button type=\"button\" class=\"btn btn-warning\" onclick=\"history.go(-1)\">");
#nullable restore
#line 57 "D:\Development\Adhari\Admin\Views\SiteContent\TermsConditionEdit.cshtml"
                                                                                              Write(SharedHtmlLocalizer["Back"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("</button>\r\n\r\n                        </div>\r\n                    </div>\r\n                ");
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "353d7bc76be323696eff7cc56acd34e59d5963fd13643", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
#nullable restore
#line 68 "D:\Development\Adhari\Admin\Views\SiteContent\TermsConditionEdit.cshtml"
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
