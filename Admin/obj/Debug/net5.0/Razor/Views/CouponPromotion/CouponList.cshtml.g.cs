#pragma checksum "D:\Development\Adhari\Admin\Views\CouponPromotion\CouponList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "afee6d90837c1afdb5acc7224eb3b027fd852df2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_CouponPromotion_CouponList), @"mvc.1.0.view", @"/Views/CouponPromotion/CouponList.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"afee6d90837c1afdb5acc7224eb3b027fd852df2", @"/Views/CouponPromotion/CouponList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"42e038141c336d185a06267799a2ba9686e6a703", @"/Views/_ViewImports.cshtml")]
    public class Views_CouponPromotion_CouponList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "2", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/developer/js/CouponPromotion/Coupon_Datatable.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("<header class=\"page-header\">\r\n    <h2>");
#nullable restore
#line 7 "D:\Development\Adhari\Admin\Views\CouponPromotion\CouponList.cshtml"
   Write(SharedHtmlLocalizer["Coupons"]);

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
#line 25 "D:\Development\Adhari\Admin\Views\CouponPromotion\CouponList.cshtml"
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
                    <div class=""col-12 col-lg-4 mb-3"">
                        <!-- ========== Start Simple Input Field ========== -->

                        <label");
            BeginWriteAttribute("for", " for=\"", 1229, "\"", 1235, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"form-label\">");
#nullable restore
#line 35 "D:\Development\Adhari\Admin\Views\CouponPromotion\CouponList.cshtml"
                                                    Write(SharedHtmlLocalizer["CouponCode"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                        <input type=\"text\" class=\"form-control form-control-lg rounded-4 border-secondary \" name=\"couponCode\" id=\"couponCode\"");
            BeginWriteAttribute("aria-describedby", " aria-describedby=\"", 1441, "\"", 1494, 1);
#nullable restore
#line 36 "D:\Development\Adhari\Admin\Views\CouponPromotion\CouponList.cshtml"
WriteAttributeValue("", 1460, SharedHtmlLocalizer["CouponCode"], 1460, 34, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("placeholder", " placeholder=\"", 1495, "\"", 1509, 0);
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 1510, "\"", 1518, 0);
            EndWriteAttribute();
            WriteLiteral(@">


                        <!-- ========== End Simple Input Field ========== -->
                    </div>
                    <div class=""col-12 col-lg-4 mb-3"">
                        <!-- ========== Start Simple Select Field Block ========== -->
                        <label");
            BeginWriteAttribute("for", " for=\"", 1807, "\"", 1813, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"form-label\">");
#nullable restore
#line 43 "D:\Development\Adhari\Admin\Views\CouponPromotion\CouponList.cshtml"
                                                    Write(SharedHtmlLocalizer["Status"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                        <select class=\"form-select form-select-lg rounded-4 border-secondary\" name=\"statusList\" id=\"statusList\"");
            BeginWriteAttribute("aria-describedby", " aria-describedby=\"", 2001, "\"", 2050, 1);
#nullable restore
#line 44 "D:\Development\Adhari\Admin\Views\CouponPromotion\CouponList.cshtml"
WriteAttributeValue("", 2020, SharedHtmlLocalizer["Status"], 2020, 30, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "afee6d90837c1afdb5acc7224eb3b027fd852df210337", async() => {
#nullable restore
#line 45 "D:\Development\Adhari\Admin\Views\CouponPromotion\CouponList.cshtml"
                                        Write(SharedHtmlLocalizer["All"]);

#line default
#line hidden
#nullable disable
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
            WriteLiteral("\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "afee6d90837c1afdb5acc7224eb3b027fd852df211709", async() => {
#nullable restore
#line 46 "D:\Development\Adhari\Admin\Views\CouponPromotion\CouponList.cshtml"
                                         Write(SharedHtmlLocalizer["Active"]);

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
            WriteLiteral("\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "afee6d90837c1afdb5acc7224eb3b027fd852df213085", async() => {
#nullable restore
#line 47 "D:\Development\Adhari\Admin\Views\CouponPromotion\CouponList.cshtml"
                                         Write(SharedHtmlLocalizer["Expired"]);

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


                        <!-- ========== End Simple Select Field Block ========== -->
                    </div>
                    <div class=""col-12 col-lg-4 mb-3"">
                        <!-- ========== Start Datepicker Field ========== -->
                        <label");
            BeginWriteAttribute("for", " for=\"", 2630, "\"", 2636, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"form-label\">");
#nullable restore
#line 55 "D:\Development\Adhari\Admin\Views\CouponPromotion\CouponList.cshtml"
                                                    Write(SharedHtmlLocalizer["DateCreated"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</label>
                        <div class=""input-group rounded-4"">
                            <span class=""input-group-text rounded-4 border border-secondary"">
                                <i class=""fas fa-calendar-alt""></i>
                            </span>
                            <!-- ========== Start Simple Input Field ========== -->


                            <input type=""text"" data-plugin-datepicker autocomplete=""off"" class=""form-control form-control-lg rounded-4 border-secondary "" name=""createdOn"" id=""createdOn""");
            BeginWriteAttribute("aria-describedby", " aria-describedby=\"", 3238, "\"", 3257, 0);
            EndWriteAttribute();
            BeginWriteAttribute("placeholder", " placeholder=\"", 3258, "\"", 3272, 0);
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 3273, "\"", 3281, 0);
            EndWriteAttribute();
            WriteLiteral(@">


                            <!-- ========== End Simple Input Field ========== -->
                        </div>
                        <!-- ========== End Datepicker Field ========== -->
                    </div>
                    <div class=""col-12 "">
                        <a href=""#"" class=""btn btn-primary btn-lg fw-bold "" onclick=""searchDataTable()"">");
#nullable restore
#line 71 "D:\Development\Adhari\Admin\Views\CouponPromotion\CouponList.cshtml"
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
    <div class=""col"">
        <!-- ========== Start Card Block ========== -->
        <section class=""card mb-3 border border-secondary"">
            <header class=""card-header d-flex justify-content-between align-items-center p-3 border-secondary body-bg-secondary"">
                <h2 class=""card-title text-primary"">");
#nullable restore
#line 82 "D:\Development\Adhari\Admin\Views\CouponPromotion\CouponList.cshtml"
                                               Write(SharedHtmlLocalizer["Coupons"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n                <div class=\"card-actions d-flex position-static top-0 end-0\">\r\n                    <button class=\"btn btn-primary btn-sm \" onclick=\"window.location = \'/couponPromotion/CouponAddEdit?id=0\';\">");
#nullable restore
#line 84 "D:\Development\Adhari\Admin\Views\CouponPromotion\CouponList.cshtml"
                                                                                                                          Write(SharedHtmlLocalizer["AddNewCoupon"]);

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
                            <th data-priority=""10"" class=""text-center"">");
#nullable restore
#line 92 "D:\Development\Adhari\Admin\Views\CouponPromotion\CouponList.cshtml"
                                                                  Write(SharedHtmlLocalizer["ID"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th data-priority=\"1\"");
            BeginWriteAttribute("class", " class=\"", 4899, "\"", 4907, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 93 "D:\Development\Adhari\Admin\Views\CouponPromotion\CouponList.cshtml"
                                                      Write(SharedHtmlLocalizer["Status"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th data-priority=\"1\"");
            BeginWriteAttribute("class", " class=\"", 4995, "\"", 5003, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 94 "D:\Development\Adhari\Admin\Views\CouponPromotion\CouponList.cshtml"
                                                      Write(SharedHtmlLocalizer["CouponCode"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th data-priority=\"2\"");
            BeginWriteAttribute("class", " class=\"", 5095, "\"", 5103, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 95 "D:\Development\Adhari\Admin\Views\CouponPromotion\CouponList.cshtml"
                                                      Write(SharedHtmlLocalizer["StartDate"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th data-priority=\"3\"");
            BeginWriteAttribute("class", " class=\"", 5194, "\"", 5202, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 96 "D:\Development\Adhari\Admin\Views\CouponPromotion\CouponList.cshtml"
                                                      Write(SharedHtmlLocalizer["EndDate"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th data-priority=\"4\"");
            BeginWriteAttribute("class", " class=\"", 5291, "\"", 5299, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 97 "D:\Development\Adhari\Admin\Views\CouponPromotion\CouponList.cshtml"
                                                      Write(SharedHtmlLocalizer["Limited"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th data-priority=\"5\"");
            BeginWriteAttribute("class", " class=\"", 5388, "\"", 5396, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 98 "D:\Development\Adhari\Admin\Views\CouponPromotion\CouponList.cshtml"
                                                      Write(SharedHtmlLocalizer["Quantity"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th data-priority=\"6\"");
            BeginWriteAttribute("class", " class=\"", 5486, "\"", 5494, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 99 "D:\Development\Adhari\Admin\Views\CouponPromotion\CouponList.cshtml"
                                                      Write(SharedHtmlLocalizer["TimesUsed"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th data-priority=\"2\" class=\"text-start\">");
#nullable restore
#line 100 "D:\Development\Adhari\Admin\Views\CouponPromotion\CouponList.cshtml"
                                                                Write(SharedHtmlLocalizer["Validity"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                            <th data-priority=\"6\"");
            BeginWriteAttribute("class", " class=\"", 5693, "\"", 5701, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 101 "D:\Development\Adhari\Admin\Views\CouponPromotion\CouponList.cshtml"
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "afee6d90837c1afdb5acc7224eb3b027fd852df223356", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
#nullable restore
#line 116 "D:\Development\Adhari\Admin\Views\CouponPromotion\CouponList.cshtml"
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
