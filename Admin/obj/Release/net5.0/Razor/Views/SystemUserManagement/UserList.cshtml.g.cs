#pragma checksum "D:\Development\WEB\Client\Adhari\Admin\Views\SystemUserManagement\UserList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d75de6067afa01afc187fcb84af3e1405e25b373"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_SystemUserManagement_UserList), @"mvc.1.0.view", @"/Views/SystemUserManagement/UserList.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d75de6067afa01afc187fcb84af3e1405e25b373", @"/Views/SystemUserManagement/UserList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"42e038141c336d185a06267799a2ba9686e6a703", @"/Views/_ViewImports.cshtml")]
    public class Views_SystemUserManagement_UserList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/developer/js/SystemUserManagement/User_Datatable.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral(@"<header class=""page-header"">
    <h2>System Users</h2>
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
                <h2 class=""card-title text-primary"">System Users</h2>
                <div class=""card-actions d-flex position-static top-0 end-0"">
                    <button class=""btn btn-primary btn-sm "" onclick=""window.location = '/systemUserManagement/UserAddEdit';"">Add New  User</button>

                </div>
            </header>
            <div class=""card-body "">
 ");
            WriteLiteral(@"               <table class=""table border-secondary align-middle mb-0 mobile-optimised datatable-default-"" id=""datatable-default-"">
                    <thead>
                        <tr>
                            <th data-priority=""1"">ID</th>
                            <th data-priority=""6"">Status</th>
                            <th data-priority=""1"">Name</th>
                            <th data-priority=""2"">Mobile</th>
                            <th data-priority=""4"">Email</th>
                            <th data-priority=""4"">Gender</th>
                            <th data-priority=""5"">Role</th>
                            <th data-priority=""5"">Created Date</th>
                            <th data-priority=""5"">Modified Date</th>
                            <th data-priority=""3"">Actions</th>
                        </tr>
                    </thead>
                    <tbody class=""border-secondary"">
                        
                    </tbody>
                </table>
");
            WriteLiteral(@"            </div>
        </section>
        <!-- ========== End Card Block ========== -->
    </div>
</div>

<!-- Modals -->
<!-- Vertically centered scrollable modal -->
<div class=""modal fade"" id=""sort_modal"" data-bs-keyboard=""false"" tabindex=""-1"" aria-labelledby=""sort_modalLabel"" aria-hidden=""true"">
    <div class=""modal-dialog  modal-dialog-scrollable modal-dialog-centered"">
        <div class=""modal-content border-secondary"">

            <div class=""modal-header border-secondary body-bg-secondary"">
                <h5 class=""modal-title fw-bold text-primary"" id=""sort_modalLabel"">Display Order</h5>
                <button type=""button"" class=""btn-close"" data-bs-dismiss=""modal"" aria-label=""Close""></button>
            </div>
            <div class=""modal-body"">
                <!-- ========== Start Simple Input Field ========== -->

                <label");
            BeginWriteAttribute("for", " for=\"", 2940, "\"", 2946, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"form-label\">Display Order</label>\r\n                <input type=\"number\" class=\"form-control form-control-lg rounded-4 border-secondary \"");
            BeginWriteAttribute("name", " name=\"", 3091, "\"", 3098, 0);
            EndWriteAttribute();
            BeginWriteAttribute("id", " id=\"", 3099, "\"", 3104, 0);
            EndWriteAttribute();
            WriteLiteral(" aria-describedby=\"Display Order\"");
            BeginWriteAttribute("placeholder", " placeholder=\"", 3138, "\"", 3152, 0);
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 3153, "\"", 3161, 0);
            EndWriteAttribute();
            WriteLiteral(@">


                <!-- ========== End Simple Input Field ========== -->
            </div>

            <div class=""modal-footer"">
                <button type=""button"" class=""btn btn-danger"" data-bs-dismiss=""modal"">Close</button>
                <button type=""button"" class=""btn btn-success save-btn"">Save</button>


            </div>
        </div>
    </div>
</div>


<!-- end: page -->
");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d75de6067afa01afc187fcb84af3e1405e25b3739766", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 84 "D:\Development\WEB\Client\Adhari\Admin\Views\SystemUserManagement\UserList.cshtml"
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
