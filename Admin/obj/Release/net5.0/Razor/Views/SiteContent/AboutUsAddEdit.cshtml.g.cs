#pragma checksum "D:\Development\WEB\Client\Adhari\Admin\Views\SiteContent\AboutUsAddEdit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "94b45f34dfd154387161f534fc5195940f5a33c9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_SiteContent_AboutUsAddEdit), @"mvc.1.0.view", @"/Views/SiteContent/AboutUsAddEdit.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"94b45f34dfd154387161f534fc5195940f5a33c9", @"/Views/SiteContent/AboutUsAddEdit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"42e038141c336d185a06267799a2ba9686e6a703", @"/Views/_ViewImports.cshtml")]
    public class Views_SiteContent_AboutUsAddEdit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Data.Common.BaseEntityId>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("dataForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", new global::Microsoft.AspNetCore.Html.HtmlString("dataForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/developer/js/Content/AboutUs_CRUD.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral(@"<header class=""page-header"">
    <h2> Edit About Us  </h2>
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
                <h2 class=""card-title text-primary"">About Us</h2>
                <div class=""card-actions d-flex position-static top-0 end-0"">
                    <!-- ========== Start ios7 Switch ========== -->

                    <div class=""switch switch-sm switch-primary my-0  "">
                        <label");
            BeginWriteAttribute("for", " for=\"", 966, "\"", 972, 0);
            EndWriteAttribute();
            WriteLiteral(@">Display</label>
                        <input type=""checkbox"" name=""displayControl"" id=""displayControl"" data-plugin-ios-switch  />
                    </div>
                    <!-- ========== End ios7 Switch ========== -->


                </div>
            </header>
            <div class=""card-body "">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "94b45f34dfd154387161f534fc5195940f5a33c98472", async() => {
                WriteLiteral("\r\n                    ");
#nullable restore
#line 34 "D:\Development\WEB\Client\Adhari\Admin\Views\SiteContent\AboutUsAddEdit.cshtml"
               Write(await Html.PartialAsync("_FormHiddenFieldsPartial"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                    <div class=""row"">
                        <div class=""col-12 col-lg-6 mb-3"">
                            <!-- ========== Start Summernote WYSIWYG ========== -->
                            <label for=""textEn"" class=""form-label"">About Us Text (En)</label>
                            <div class=""summernote "" name=""textEn"" id=""textEn""></div>

                            <!-- ========== End Summernote WYSIWYG ========== -->
                        </div>
                        <div class=""col-12 col-lg-6 mb-3"">
                            <!-- ========== Start Summernote WYSIWYG ========== -->
                            <label for=""textAr"" class=""form-label"">About Us Text (Ar)</label>
                            <div class=""summernote dir-rtl"" dir=""rtl"" name=""textAr"" id=""textAr""></div>


                            <!-- ========== End Summernote WYSIWYG ========== -->
                        </div>
                        <div class=""col-12 col-lg-6 mb-3"">
                ");
                WriteLiteral(@"            <div class=""row"">
                                <!-- ========== Start Simple Image Upload Block ========== -->
                                <div class=""row"">
                                    <div class=""col-10 mb-3"">
                                        <label for=""formFile"" class=""form-label"">Upload Image</label>
                                        <input class=""form-control form-control-lg border-secondary rounded-4"" type=""file"" id=""imageFile"" name=""imageFile"">
                                        <small class=""form-text text-muted"">For consistency purposes, please upload all image at the size 1260 x 700 px in .png or .jpg format</small>
                                        <div class=""invalid-feedback"">
                                            For consistency purposes, please upload all image at the size 1260 x 700 px in .png or .jpg format
                                        </div>
                                    </div>
                               ");
                WriteLiteral(@"     <div class=""col-2 d-flex flex-column align-items-center justify-content-center"">
                                        <img src=""/assets/img/about-us-img.jpg"" class=""img-fluid preview-image-"" id=""imagePreview"" name=""imagePreview"">
                                        <p class=""text-muted text-center mb-1 mt-2"">Preview</p>
                                    </div>
                                </div>
                                <script>
                                    $(""#imageFile"").change(function (event) {
                                        var image = document.getElementById('imagePreview');
                                        image.src = URL.createObjectURL(event.target.files[0]);
                                    });
                                </script>
                                <!-- ========== End Simple Image Upload Block ========== -->
                            </div>
                        </div>
                    </div>
                 ");
                WriteLiteral(@"   <div class=""row mt-3"">
                        <div class=""col-12"">
                            <button id=""btnSave"" class=""btn btn-primary btn-lg save-btn  fw-bold"">Save</button>
                            <button type=""button"" class=""btn btn-warning"" onclick=""history.go(-1)""> Back </button>

                        </div>
                    </div>
                ");
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "94b45f34dfd154387161f534fc5195940f5a33c914214", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
#nullable restore
#line 92 "D:\Development\WEB\Client\Adhari\Admin\Views\SiteContent\AboutUsAddEdit.cshtml"
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
