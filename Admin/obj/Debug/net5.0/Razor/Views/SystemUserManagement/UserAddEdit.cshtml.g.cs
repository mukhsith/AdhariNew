#pragma checksum "D:\Development\AdhariNew\Admin\Views\SystemUserManagement\UserAddEdit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3b462e98d96c901575c42701232a4efc5cd7b0ab"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_SystemUserManagement_UserAddEdit), @"mvc.1.0.view", @"/Views/SystemUserManagement/UserAddEdit.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3b462e98d96c901575c42701232a4efc5cd7b0ab", @"/Views/SystemUserManagement/UserAddEdit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"42e038141c336d185a06267799a2ba9686e6a703", @"/Views/_ViewImports.cshtml")]
    public class Views_SystemUserManagement_UserAddEdit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Data.Common.BaseEntityId>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "0", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "2", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("dataForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", new global::Microsoft.AspNetCore.Html.HtmlString("dataForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/developer/js/systemUserManagement/User_CRUD.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<header class=\"page-header\">\r\n    <h2> ");
#nullable restore
#line 3 "D:\Development\AdhariNew\Admin\Views\SystemUserManagement\UserAddEdit.cshtml"
     Write(@Model.Id > 0 ? @SharedHtmlLocalizer["EditSystemUser"] : @SharedHtmlLocalizer["NewSystemUser"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n    <input type=\"hidden\"");
            BeginWriteAttribute("value", " value=\"", 200, "\"", 217, 1);
#nullable restore
#line 4 "D:\Development\AdhariNew\Admin\Views\SystemUserManagement\UserAddEdit.cshtml"
WriteAttributeValue("", 208, Model.Id, 208, 9, false);

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
#line 20 "D:\Development\AdhariNew\Admin\Views\SystemUserManagement\UserAddEdit.cshtml"
                                               Write(SharedHtmlLocalizer["SystemUserDetails"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n                <div class=\"card-actions d-flex position-static top-0 end-0\">\r\n                </div>\r\n            </header>\r\n            <div class=\"card-body \">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3b462e98d96c901575c42701232a4efc5cd7b0ab9825", async() => {
                WriteLiteral("\r\n                    ");
#nullable restore
#line 26 "D:\Development\AdhariNew\Admin\Views\SystemUserManagement\UserAddEdit.cshtml"
               Write(await Html.PartialAsync("_FormHiddenFieldsPartial"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    <div class=\"row\">\r\n                        <div class=\"col-12 col-lg-4 mb-3\">\r\n                            <label for=\"fullName\" class=\"form-label\">");
#nullable restore
#line 29 "D:\Development\AdhariNew\Admin\Views\SystemUserManagement\UserAddEdit.cshtml"
                                                                Write(SharedHtmlLocalizer["Name"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("<span class=\" text-danger\">*</span></label>\r\n                            <input type=\"text\" class=\"form-control form-control-lg rounded-4 border-secondary \" name=\"fullName\" id=\"fullName\"");
                BeginWriteAttribute("aria-describedby", " aria-describedby=\"", 1599, "\"", 1646, 1);
#nullable restore
#line 30 "D:\Development\AdhariNew\Admin\Views\SystemUserManagement\UserAddEdit.cshtml"
WriteAttributeValue("", 1618, SharedHtmlLocalizer["Name"], 1618, 28, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" oninput=\"maxlength(event);\" maxlength=\"25\" required >\r\n                        </div>\r\n                        <div class=\"col-12 col-lg-4 mb-3\">\r\n                            <label for=\"mobileNumber\" class=\"form-label\">");
#nullable restore
#line 33 "D:\Development\AdhariNew\Admin\Views\SystemUserManagement\UserAddEdit.cshtml"
                                                                    Write(SharedHtmlLocalizer["MobileNumber"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("<span class=\" text-danger\">*</span></label>\r\n                            <input type=\"tel\" class=\"form-control form-control-lg rounded-4 border-secondary \" name=\"mobileNumber\" id=\"mobileNumber\"");
                BeginWriteAttribute("aria-describedby", " aria-describedby=\"", 2097, "\"", 2152, 1);
#nullable restore
#line 34 "D:\Development\AdhariNew\Admin\Views\SystemUserManagement\UserAddEdit.cshtml"
WriteAttributeValue("", 2116, SharedHtmlLocalizer["MobileNumber"], 2116, 36, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" oninput=\"maxlength(event);\" maxlength=\"8\" required>\r\n                        </div>\r\n                        <div class=\"col-12 col-lg-4 mb-3\">\r\n                            <label for=\"emailAddress\" class=\"form-label\">");
#nullable restore
#line 37 "D:\Development\AdhariNew\Admin\Views\SystemUserManagement\UserAddEdit.cshtml"
                                                                    Write(SharedHtmlLocalizer["EmailAddress"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("<span class=\" text-danger\">*</span></label>\r\n                            <input type=\"email\" class=\"form-control form-control-lg rounded-4 border-secondary \" name=\"emailAddress\" id=\"emailAddress\"");
                BeginWriteAttribute("aria-describedby", " aria-describedby=\"", 2603, "\"", 2658, 1);
#nullable restore
#line 38 "D:\Development\AdhariNew\Admin\Views\SystemUserManagement\UserAddEdit.cshtml"
WriteAttributeValue("", 2622, SharedHtmlLocalizer["EmailAddress"], 2622, 36, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("placeholder", " placeholder=\"", 2659, "\"", 2673, 0);
                EndWriteAttribute();
                WriteLiteral(" required>\r\n                        </div>\r\n                        <div class=\"col-12 col-lg-4 mb-3\">\r\n                            <label");
                BeginWriteAttribute("for", " for=\"", 2812, "\"", 2818, 0);
                EndWriteAttribute();
                WriteLiteral(" class=\"form-label\">");
#nullable restore
#line 41 "D:\Development\AdhariNew\Admin\Views\SystemUserManagement\UserAddEdit.cshtml"
                                                        Write(SharedHtmlLocalizer["Password"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("<span class=\" text-danger\">*</span></label>\r\n                            <input type=\"password\" class=\"form-control form-control-lg rounded-4 border-secondary \" name=\"password\" id=\"password\"");
                BeginWriteAttribute("aria-describedby", " aria-describedby=\"", 3061, "\"", 3112, 1);
#nullable restore
#line 42 "D:\Development\AdhariNew\Admin\Views\SystemUserManagement\UserAddEdit.cshtml"
WriteAttributeValue("", 3080, SharedHtmlLocalizer["Password"], 3080, 32, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" oninput=\"maxlength(event);\" maxlength=\"12\" required>\r\n\r\n                        </div>\r\n                        <div class=\"col-12 col-lg-4 mb-3\">\r\n                            <label for=\"confirmPassword\" class=\"form-label\">");
#nullable restore
#line 46 "D:\Development\AdhariNew\Admin\Views\SystemUserManagement\UserAddEdit.cshtml"
                                                                       Write(SharedHtmlLocalizer["ConfirmPassword"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("<span class=\" text-danger\">*</span></label>\r\n                            <input type=\"password\" class=\"form-control form-control-lg rounded-4 border-secondary \" name=\"confirmPassword\" id=\"confirmPassword\"");
                BeginWriteAttribute("aria-describedby", " aria-describedby=\"", 3581, "\"", 3640, 2);
                WriteAttributeValue("", 3600, ">", 3600, 1, true);
#nullable restore
#line 47 "D:\Development\AdhariNew\Admin\Views\SystemUserManagement\UserAddEdit.cshtml"
WriteAttributeValue("", 3601, SharedHtmlLocalizer["ConfirmPassword"], 3601, 39, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" oninput=\"maxlength(event);\" maxlength=\"12\" required>\r\n\r\n                        </div>\r\n                        <div class=\"col-12 col-lg-4 mb-3\">\r\n                            <label for=\"roleList\" class=\"form-label\">>");
#nullable restore
#line 51 "D:\Development\AdhariNew\Admin\Views\SystemUserManagement\UserAddEdit.cshtml"
                                                                 Write(SharedHtmlLocalizer["Role"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"<span class="" text-danger"">*</span></label>
                            <select class=""form-select form-select-lg rounded-4 border-secondary"" name=""roleList"" id=""roleList"" required>
                            </select>
                        </div>
                        <div class=""col-12 col-lg-4 mb-3"">
                            <label for=""genderList"" class=""form-label"">");
#nullable restore
#line 56 "D:\Development\AdhariNew\Admin\Views\SystemUserManagement\UserAddEdit.cshtml"
                                                                  Write(SharedHtmlLocalizer["Gender"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("<span class=\" text-danger\">*</span></label>\r\n                            <select class=\"form-select form-select-lg rounded-4 border-secondary\" name=\"genderList\" id=\"genderList\" required>\r\n                                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3b462e98d96c901575c42701232a4efc5cd7b0ab17878", async() => {
                    WriteLiteral("---");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3b462e98d96c901575c42701232a4efc5cd7b0ab19131", async() => {
#nullable restore
#line 59 "D:\Development\AdhariNew\Admin\Views\SystemUserManagement\UserAddEdit.cshtml"
                                             Write(SharedHtmlLocalizer["Male"]);

#line default
#line hidden
#nullable disable
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3b462e98d96c901575c42701232a4efc5cd7b0ab20582", async() => {
#nullable restore
#line 60 "D:\Development\AdhariNew\Admin\Views\SystemUserManagement\UserAddEdit.cshtml"
                                             Write(SharedHtmlLocalizer["Female"]);

#line default
#line hidden
#nullable disable
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                            </select>
                        </div>
                    </div>
                    <div class=""row"">
                        <div class=""col-12"">
                            <div class=""row mt-3"">
                                <div class=""col-12"">
                                    <button id=""btnSave"" class=""btn btn-primary btn-lg save-btn fw-bold"">");
#nullable restore
#line 68 "D:\Development\AdhariNew\Admin\Views\SystemUserManagement\UserAddEdit.cshtml"
                                                                                                    Write(SharedHtmlLocalizer["Save"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("</button>\r\n                                    <button type=\"button\" class=\"btn btn-warning\" onclick=\"history.go(-1)\">");
#nullable restore
#line 69 "D:\Development\AdhariNew\Admin\Views\SystemUserManagement\UserAddEdit.cshtml"
                                                                                                      Write(SharedHtmlLocalizer["Back"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("</button>\r\n                                </div>\r\n                            </div>\r\n                        </div>\r\n                    </div> \r\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n        </section>\r\n        <!-- ========== End Card Block ========== -->\r\n    </div>\r\n</div>\r\n \r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3b462e98d96c901575c42701232a4efc5cd7b0ab24923", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
#nullable restore
#line 81 "D:\Development\AdhariNew\Admin\Views\SystemUserManagement\UserAddEdit.cshtml"
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
            WriteLiteral("\r\n \r\n");
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
