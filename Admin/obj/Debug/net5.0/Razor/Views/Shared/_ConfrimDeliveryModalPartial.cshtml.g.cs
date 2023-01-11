#pragma checksum "D:\Development\Adhari\Admin\Views\Shared\_ConfrimDeliveryModalPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fd69ec890dd7c714f2600e91ddee15922568505c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__ConfrimDeliveryModalPartial), @"mvc.1.0.view", @"/Views/Shared/_ConfrimDeliveryModalPartial.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fd69ec890dd7c714f2600e91ddee15922568505c", @"/Views/Shared/_ConfrimDeliveryModalPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"42e038141c336d185a06267799a2ba9686e6a703", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__ConfrimDeliveryModalPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""modal fade"" id=""confirm-delivery-modal"" data-bs-keyboard=""false"" tabindex=""-1"" aria-labelledby=""confirm-delivery-modalLabel"" aria-hidden=""true"">
    <div class=""modal-dialog  modal-dialog-scrollable modal-dialog-centered"">
        <div class=""modal-content border-secondary"">

            <div class=""modal-header border-secondary body-bg-secondary"">
                <h5 class=""modal-title fw-bold text-primary"" id=""confirm-delivery-modalLabel"">Confirm Order Delivery</h5>
                <button type=""button"" class=""btn-close"" data-bs-dismiss=""modal"" aria-label=""Close""></button>
                <input type=""hidden"" id=""dialogOrderId""");
            BeginWriteAttribute("value", " value=\"", 663, "\"", 671, 0);
            EndWriteAttribute();
            WriteLiteral(@" />
                <input type=""hidden"" id=""dialogOrderTypeId"" value=""1"" />
            </div>
            <div class=""modal-body"">
                <div class=""text-center"">
                    <p>Are you sure the order is delivered?</p>
                </div>
            </div>

            <div class=""modal-footer"">
                <button type=""button"" class=""btn btn-danger"" data-bs-dismiss=""modal"">Close</button>
                <button type=""button"" class=""btn btn-success save-btn"" onclick=""confirmOrderDelivery()"">Confirm</button>
            </div>
        </div>
    </div>
</div>
<script>
    $(document).ready(function () {
        confirmOrderDelivery = () => {
            let orderId = $(""#confirm-delivery-modal .modal-header #dialogOrderId"").val();
            let dialogOrderTypeId = $(""#confirm-delivery-modal .modal-header #dialogOrderTypeId"").val();

            let submitData = new FormData();
            submitData.append(""orderId"", orderId);
            submitData.append");
            WriteLiteral("(\"orderTypeId\", dialogOrderTypeId);\r\n            submitData.append(\"orderStatusId\", ");
#nullable restore
#line 35 "D:\Development\Adhari\Admin\Views\Shared\_ConfrimDeliveryModalPartial.cshtml"
                                           Write((Int16)OrderStatus.Delivered);

#line default
#line hidden
#nullable disable
            WriteLiteral(@");
            ajaxPost(""driver/UpdateOrderStatus"", submitData, cbconfirmOrderDeliverySuccess);

        }

    });

    cbconfirmOrderDeliverySuccess = (data) => {
        if (data.success) {
            ToastAlert('success', 'Confirm Delivery', 'Order status changed successfully');
            reloadPage();
        } else {
            ToastAlert('error', 'Confirm Delivery', data.message);
        }
    }
</script>");
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
