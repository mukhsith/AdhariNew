#pragma checksum "D:\Development\AdhariNew\Admin\Views\SiteContent\SocialMediaLinksEdit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7172ad86cb0fcc230cec46b1bc743c88a1732c32"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_SiteContent_SocialMediaLinksEdit), @"mvc.1.0.view", @"/Views/SiteContent/SocialMediaLinksEdit.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7172ad86cb0fcc230cec46b1bc743c88a1732c32", @"/Views/SiteContent/SocialMediaLinksEdit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"42e038141c336d185a06267799a2ba9686e6a703", @"/Views/_ViewImports.cshtml")]
    public class Views_SiteContent_SocialMediaLinksEdit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Data.Common.BaseEntityId>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("dataForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", new global::Microsoft.AspNetCore.Html.HtmlString("dataForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/developer/js/SiteContent/SocialMediaLink_CRUD.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "D:\Development\AdhariNew\Admin\Views\SiteContent\SocialMediaLinksEdit.cshtml"
   Write(SharedHtmlLocalizer["SocialMediaLinks"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h2> 
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
#line 19 "D:\Development\AdhariNew\Admin\Views\SiteContent\SocialMediaLinksEdit.cshtml"
                                               Write(SharedHtmlLocalizer["SocialMediaLinks"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h2>
                <div class=""card-actions d-flex position-static top-0 end-0"">
                    <!-- ========== Start ios7 Switch ========== -->
                    <div class=""switch switch-sm switch-primary my-0  "">
                    </div>
                    <!-- ========== End ios7 Switch ========== -->
                </div>
            </header>
            <div class=""card-body "">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7172ad86cb0fcc230cec46b1bc743c88a1732c328601", async() => {
                WriteLiteral("\r\n                    ");
#nullable restore
#line 29 "D:\Development\AdhariNew\Admin\Views\SiteContent\SocialMediaLinksEdit.cshtml"
               Write(await Html.PartialAsync("_FormHiddenFieldsPartial"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                    <div class=""row"">
                        <div class=""col-12 col-lg-6 mb-3"">
                            <!-- ========== Start Simple Input Field ========== -->
                            <label for=""facebookLink"" class=""form-label"">");
#nullable restore
#line 33 "D:\Development\AdhariNew\Admin\Views\SiteContent\SocialMediaLinksEdit.cshtml"
                                                                    Write(SharedHtmlLocalizer["Facebook"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("</label>\r\n                            <input type=\"text\" class=\"form-control form-control-lg rounded-4 border-secondary \" name=\"facebookLink\" id=\"facebookLink\"");
                BeginWriteAttribute("aria-describedby", " aria-describedby=\"", 1792, "\"", 1843, 1);
#nullable restore
#line 34 "D:\Development\AdhariNew\Admin\Views\SiteContent\SocialMediaLinksEdit.cshtml"
WriteAttributeValue("", 1811, SharedHtmlLocalizer["Facebook"], 1811, 32, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" placeholder=\"https://facebook.com\"");
                BeginWriteAttribute("value", " value=\"", 1879, "\"", 1887, 0);
                EndWriteAttribute();
                WriteLiteral(">\r\n                            <div class=\"invalid-feedback\">");
#nullable restore
#line 35 "D:\Development\AdhariNew\Admin\Views\SiteContent\SocialMediaLinksEdit.cshtml"
                                                     Write(SharedHtmlLocalizer["EnterValidURL"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</div>
                            <!-- ========== End Simple Input Field ========== -->
                        </div>
                        <div class=""col-12 col-lg-6 mb-3"">
                            <!-- ========== Start Simple Input Field ========== -->
                            <label for=""instagramPageURL"" class=""form-label"">");
#nullable restore
#line 40 "D:\Development\AdhariNew\Admin\Views\SiteContent\SocialMediaLinksEdit.cshtml"
                                                                        Write(SharedHtmlLocalizer["Instagram"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("</label>\r\n                            <input type=\"text\" class=\"form-control form-control-lg rounded-4 border-secondary \" name=\"instagramLink\" id=\"instagramLink\" aria-describedby=\"Instagram\" placeholder=\"https://instagram.com\"");
                BeginWriteAttribute("value", " value=\"", 2590, "\"", 2598, 0);
                EndWriteAttribute();
                WriteLiteral(">\r\n                            <div class=\"invalid-feedback\">");
#nullable restore
#line 42 "D:\Development\AdhariNew\Admin\Views\SiteContent\SocialMediaLinksEdit.cshtml"
                                                     Write(SharedHtmlLocalizer["EnterValidURL"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</div>
                            <!-- ========== End Simple Input Field ========== -->
                        </div>
                        <div class=""col-12 col-lg-6 mb-3"">
                            <!-- ========== Start Simple Input Field ========== -->
                            <label for=""twitterLink"" class=""form-label"">");
#nullable restore
#line 47 "D:\Development\AdhariNew\Admin\Views\SiteContent\SocialMediaLinksEdit.cshtml"
                                                                   Write(SharedHtmlLocalizer["Twitter"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("</label>\r\n                            <input type=\"text\" class=\"form-control form-control-lg rounded-4 border-secondary \" name=\"twitterLink\" id=\"twitterLink\"");
                BeginWriteAttribute("aria-describedby", " aria-describedby=\"", 3225, "\"", 3244, 0);
                EndWriteAttribute();
                WriteLiteral(">");
#nullable restore
#line 48 "D:\Development\AdhariNew\Admin\Views\SiteContent\SocialMediaLinksEdit.cshtml"
                                                                                                                                                                   Write(SharedHtmlLocalizer["Twitter"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("\" placeholder=\"https://twitter.com\" value=\"\">\r\n                            <div class=\"invalid-feedback\">");
#nullable restore
#line 49 "D:\Development\AdhariNew\Admin\Views\SiteContent\SocialMediaLinksEdit.cshtml"
                                                     Write(SharedHtmlLocalizer["EnterValidURL"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</div>
                            <!-- ========== End Simple Input Field ========== -->
                        </div>
                        <div class=""col-12 col-lg-6 mb-3"">
                            <!-- ========== Start Simple Input Field ========== -->
                            <label for=""youtubeLink"" class=""form-label"">");
#nullable restore
#line 54 "D:\Development\AdhariNew\Admin\Views\SiteContent\SocialMediaLinksEdit.cshtml"
                                                                   Write(SharedHtmlLocalizer["Youtube"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("</label>\r\n                            <input type=\"text\" class=\"form-control form-control-lg rounded-4 border-secondary \" name=\"youtubeLink\" id=\"youtubeLink\"");
                BeginWriteAttribute("aria-describedby", " aria-describedby=\"", 3947, "\"", 3997, 1);
#nullable restore
#line 55 "D:\Development\AdhariNew\Admin\Views\SiteContent\SocialMediaLinksEdit.cshtml"
WriteAttributeValue("", 3966, SharedHtmlLocalizer["Youtube"], 3966, 31, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" placeholder=\"https://youtube.com\"");
                BeginWriteAttribute("value", " value=\"", 4032, "\"", 4040, 0);
                EndWriteAttribute();
                WriteLiteral(">\r\n                            <div class=\"invalid-feedback\">");
#nullable restore
#line 56 "D:\Development\AdhariNew\Admin\Views\SiteContent\SocialMediaLinksEdit.cshtml"
                                                     Write(SharedHtmlLocalizer["EnterValidURL"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</div>
                            <!-- ========== End Simple Input Field ========== -->
                        </div>
                        <div class=""col-12 col-lg-6 mb-3"">
                            <!-- ========== Start Simple Input Field ========== -->
                            <label for=""whatsAppLink"" class=""form-label"">");
#nullable restore
#line 61 "D:\Development\AdhariNew\Admin\Views\SiteContent\SocialMediaLinksEdit.cshtml"
                                                                    Write(SharedHtmlLocalizer["WhatsApp"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("</label>\r\n                            <input type=\"text\" class=\"form-control form-control-lg rounded-4 border-secondary \" name=\"whatsAppLink\" id=\"whatsAppLink\"");
                BeginWriteAttribute("aria-describedby", " aria-describedby=\"", 4671, "\"", 4722, 1);
#nullable restore
#line 62 "D:\Development\AdhariNew\Admin\Views\SiteContent\SocialMediaLinksEdit.cshtml"
WriteAttributeValue("", 4690, SharedHtmlLocalizer["WhatsApp"], 4690, 32, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" placeholder=\"https://wa.me/965XXXXXXXX\"");
                BeginWriteAttribute("value", " value=\"", 4763, "\"", 4771, 0);
                EndWriteAttribute();
                WriteLiteral(">\r\n                            <div class=\"invalid-feedback\">");
#nullable restore
#line 63 "D:\Development\AdhariNew\Admin\Views\SiteContent\SocialMediaLinksEdit.cshtml"
                                                     Write(SharedHtmlLocalizer["EnterValidURL"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</div>
                            <!-- ========== End Simple Input Field ========== -->
                        </div>
                        <div class=""col-12 col-lg-6 mb-3"">
                            <!-- ========== Start Simple Input Field ========== -->
                            <label for=""tiktokLink"" class=""form-label"">");
#nullable restore
#line 68 "D:\Development\AdhariNew\Admin\Views\SiteContent\SocialMediaLinksEdit.cshtml"
                                                                  Write(SharedHtmlLocalizer["Tiktok"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("</label>\r\n                            <input type=\"text\" class=\"form-control form-control-lg rounded-4 border-secondary \" name=\"tiktokLink\" id=\"tiktokLink\"");
                BeginWriteAttribute("aria-describedby", " aria-describedby=\"", 5394, "\"", 5443, 1);
#nullable restore
#line 69 "D:\Development\AdhariNew\Admin\Views\SiteContent\SocialMediaLinksEdit.cshtml"
WriteAttributeValue("", 5413, SharedHtmlLocalizer["Tiktok"], 5413, 30, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" placeholder=\"TikTok ID\"");
                BeginWriteAttribute("value", " value=\"", 5468, "\"", 5476, 0);
                EndWriteAttribute();
                WriteLiteral(">\r\n                            <div class=\"invalid-feedback\">");
#nullable restore
#line 70 "D:\Development\AdhariNew\Admin\Views\SiteContent\SocialMediaLinksEdit.cshtml"
                                                     Write(SharedHtmlLocalizer["EnterValidURL"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</div>
                            <!-- ========== End Simple Input Field ========== -->
                        </div>
                        <div class=""col-12 col-lg-6 mb-3"">
                            <!-- ========== Start Simple Input Field ========== -->
                            <label for=""snapchatURL"" class=""form-label"">");
#nullable restore
#line 75 "D:\Development\AdhariNew\Admin\Views\SiteContent\SocialMediaLinksEdit.cshtml"
                                                                   Write(SharedHtmlLocalizer["Snapchat"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("</label>\r\n                            <input type=\"text\" class=\"form-control form-control-lg rounded-4 border-secondary \" name=\"snapchatLink\" id=\"snapchatLink\" aria-describedby=\"Snapchat\" placeholder=\"https://snapchat.com/add/username\"");
                BeginWriteAttribute("value", " value=\"", 6182, "\"", 6190, 0);
                EndWriteAttribute();
                WriteLiteral(">\r\n                            <div class=\"invalid-feedback\">");
#nullable restore
#line 77 "D:\Development\AdhariNew\Admin\Views\SiteContent\SocialMediaLinksEdit.cshtml"
                                                     Write(SharedHtmlLocalizer["EnterValidURL"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</div>
                            <!-- ========== End Simple Input Field ========== -->
                        </div>
                    </div>
                    <div class=""row mt-3"">
                        <div class=""col-12"">
                            <button id=""btnSave"" class=""btn btn-primary btn-lg save-btn  fw-bold"">");
#nullable restore
#line 83 "D:\Development\AdhariNew\Admin\Views\SiteContent\SocialMediaLinksEdit.cshtml"
                                                                                             Write(SharedHtmlLocalizer["Save"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("</button>\r\n                            <button type=\"button\" class=\"btn btn-warning\" onclick=\"history.go(-1)\">");
#nullable restore
#line 84 "D:\Development\AdhariNew\Admin\Views\SiteContent\SocialMediaLinksEdit.cshtml"
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
            WriteLiteral("\r\n            </div>\r\n        </section>\r\n        <!-- ========== End Card Block ========== -->\r\n    </div>\r\n</div>\r\n \r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7172ad86cb0fcc230cec46b1bc743c88a1732c3223544", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
#nullable restore
#line 94 "D:\Development\AdhariNew\Admin\Views\SiteContent\SocialMediaLinksEdit.cshtml"
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