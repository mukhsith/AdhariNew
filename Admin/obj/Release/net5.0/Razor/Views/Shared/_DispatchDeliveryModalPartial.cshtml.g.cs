#pragma checksum "D:\Development\AdhariNew\Admin\Views\Shared\_DispatchDeliveryModalPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "89bf70d82d39927be1dbaa0f4622a9697caf8f6a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__DispatchDeliveryModalPartial), @"mvc.1.0.view", @"/Views/Shared/_DispatchDeliveryModalPartial.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"89bf70d82d39927be1dbaa0f4622a9697caf8f6a", @"/Views/Shared/_DispatchDeliveryModalPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"42e038141c336d185a06267799a2ba9686e6a703", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__DispatchDeliveryModalPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""modal fade"" id=""dispatch-delivery-modal"" data-bs-keyboard=""false"" tabindex=""-1"" aria-labelledby=""dispatch-delivery-modalLabel"" aria-hidden=""true"">
    <div class=""modal-dialog  modal-dialog-scrollable modal-dialog-centered"">
        <div class=""modal-content border-secondary"">

            <div class=""modal-header border-secondary body-bg-secondary"">
                <h5 class=""modal-title fw-bold text-primary"" id=""dispatch-delivery-modalLabel"">");
#nullable restore
#line 6 "D:\Development\AdhariNew\Admin\Views\Shared\_DispatchDeliveryModalPartial.cshtml"
                                                                                          Write(SharedHtmlLocalizer["DispatchDelivery"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"modal\" aria-label=\"Close\"></button>\r\n                <input type=\"hidden\" id=\"dialogOrderId\"");
            BeginWriteAttribute("value", " value=\"", 677, "\"", 685, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n                <input type=\"hidden\" id=\"dialogOrderTypeId\" value=\"1\" />\r\n            </div>\r\n            <div class=\"modal-body\">\r\n                <label for=\"AssignDriver\" class=\"form-label\">");
#nullable restore
#line 12 "D:\Development\AdhariNew\Admin\Views\Shared\_DispatchDeliveryModalPartial.cshtml"
                                                        Write(SharedHtmlLocalizer["AssignDriver"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                <select class=\"form-select form-select-lg rounded-4 border-secondary\"\r\n                        name=\"popupDriverList\" id=\"popupDriverList\"");
            BeginWriteAttribute("aria-describedby", " aria-describedby=\"", 1084, "\"", 1139, 1);
#nullable restore
#line 14 "D:\Development\AdhariNew\Admin\Views\Shared\_DispatchDeliveryModalPartial.cshtml"
WriteAttributeValue("", 1103, SharedHtmlLocalizer["AssignDriver"], 1103, 36, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "89bf70d82d39927be1dbaa0f4622a9697caf8f6a7408", async() => {
#nullable restore
#line 15 "D:\Development\AdhariNew\Admin\Views\Shared\_DispatchDeliveryModalPartial.cshtml"
                                         Write(SharedHtmlLocalizer["SelectDriver"]);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("selected", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </select>\r\n            </div>\r\n\r\n            <div class=\"modal-footer\">\r\n                <button type=\"button\" class=\"btn btn-danger\" data-bs-dismiss=\"modal\">");
#nullable restore
#line 20 "D:\Development\AdhariNew\Admin\Views\Shared\_DispatchDeliveryModalPartial.cshtml"
                                                                                Write(SharedHtmlLocalizer["Close"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</button>\r\n                <button type=\"button\" class=\"btn btn-success save-btn\" onclick=\"assignDriver()\">");
#nullable restore
#line 21 "D:\Development\AdhariNew\Admin\Views\Shared\_DispatchDeliveryModalPartial.cshtml"
                                                                                           Write(SharedHtmlLocalizer["Assign"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</button>
            </div>
        </div>
    </div>
</div><!-- Vertically centered scrollable modal -->

<script>
    $(document).ready(function () {

        assignDriver = () => {
            let orderId = $(""#dispatch-delivery-modal .modal-header #dialogOrderId"").val();
            let dialogOrderTypeId = $(""#dispatch-delivery-modal .modal-header #dialogOrderTypeId"").val();
             
            let driverId = getSelectedItemValue('popupDriverList');
            if (driverId == """") {
                ToastAlert('error', 'Driver', 'Please select driver for dispatch');
             } else {
                let submitData = new FormData();
                submitData.append(""orderId"", orderId);
                submitData.append(""orderTypeId"", dialogOrderTypeId);
                submitData.append(""driverId"", driverId);
                ajaxPost(""order/AssignDriver"", submitData, cbDriverSuccess);
            }
        }

    });

    cbDriverSuccess = (data) => {
        if (data");
            WriteLiteral(".success) {\r\n            ToastAlert(\'success\', \'Dispatch\', \'Driver assigned successfully\');\r\n            reloadPage();\r\n        } else {\r\n            ToastAlert(\'error\', \'Dispatch\', data.message);\r\n        }\r\n    }\r\n</script>");
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
