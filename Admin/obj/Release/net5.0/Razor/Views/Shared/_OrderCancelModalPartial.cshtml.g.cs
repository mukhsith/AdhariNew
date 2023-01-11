#pragma checksum "D:\Development\WEB\Client\Adhari\Admin\Views\Shared\_OrderCancelModalPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "45da237f92f0f8b9ffb879d06069cdd6ef181059"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__OrderCancelModalPartial), @"mvc.1.0.view", @"/Views/Shared/_OrderCancelModalPartial.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"45da237f92f0f8b9ffb879d06069cdd6ef181059", @"/Views/Shared/_OrderCancelModalPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"42e038141c336d185a06267799a2ba9686e6a703", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__OrderCancelModalPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""modal fade"" id=""order-cancel"" data-bs-keyboard=""false"" tabindex=""-1"" aria-labelledby=""order-cancelLabel"" aria-hidden=""true"">
    <div class=""modal-dialog  modal-dialog-scrollable modal-dialog-centered"">
        <div class=""modal-content border-secondary"">

            <div class=""modal-header border-secondary body-bg-secondary"">
                <h5 class=""modal-title fw-bold text-primary"" id=""order-cancelLabel"">Cancel Order</h5>
                <button type=""button"" class=""btn-close"" data-bs-dismiss=""modal"" aria-label=""Close""></button>
                <input type=""hidden"" id=""dialogOrderId""");
            BeginWriteAttribute("value", " value=\"", 618, "\"", 626, 0);
            EndWriteAttribute();
            WriteLiteral(@" />
                <input type=""hidden"" id=""dialogOrderTypeId"" value=""1"" />
            </div>
            <div class=""modal-body"">

                <div class=""form-check form-check-inline"">
                    <input class=""form-check-input"" type=""radio"" name=""ordercanceloption"" id=""refund_with_total"" value=""1"">
                    <label class=""form-check-label""");
            BeginWriteAttribute("for", " for=\"", 1002, "\"", 1008, 0);
            EndWriteAttribute();
            WriteLiteral(@">Refund with Delivery charges</label>
                </div>
                <div class=""form-check form-check-inline"">
                    <input class=""form-check-input"" type=""radio"" name=""ordercanceloption"" id=""refund_without_total"" value=""2"">
                    <label class=""form-check-label""");
            BeginWriteAttribute("for", " for=\"", 1311, "\"", 1317, 0);
            EndWriteAttribute();
            WriteLiteral(@">Refund wihtout Delivery charges</label>
                </div>
            </div>

            <div class=""modal-footer"">
                <button type=""button"" class=""btn btn-danger"" data-bs-dismiss=""modal"">Close</button>
                <button type=""button"" class=""btn btn-success save-btn"" onclick=""cancelOrder()"">Save</button>
            </div>
        </div>
    </div>
</div>
<script>
    $(document).ready(function () {
        cancelOrder = () => {
            let orderId = $(""#order-cancel .modal-header #dialogOrderId"").val();
            let dialogOrderTypeId = $(""#order-cancel .modal-header #dialogOrderTypeId"").val();
            if ($('#order-cancel .modal-body input[type=radio]').is(':checked') == false) {
                ToastAlert('error', 'Refund', 'Please select refund with or without delivery charges');
            } else {
               
                //onl for two options, becareful
                var withFee = $('#order-cancel .modal-body input[type=radio]')[0].chec");
            WriteLiteral(@"ked;
                 
                let submitData = new FormData();
                submitData.append(""orderId"", orderId);
                submitData.append(""orderTypeId"", dialogOrderTypeId);
                submitData.append(""orderStatusId"", 3);//cancelled
                submitData.append(""refundDeliveryFee"", withFee);
                ajaxPost(""order/UpdateOrderStatus"", submitData, cbCancelOrderSuccess);
            }
        }
          
    });

    cbCancelOrderSuccess = (data) => {
        if (data.success) {
            ToastAlert('success', 'Order', 'Order cancelled successfully');
            reloadPage();
        } else { 
            ToastAlert('error', 'Order', data.message);
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
