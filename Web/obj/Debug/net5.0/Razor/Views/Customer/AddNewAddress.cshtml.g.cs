#pragma checksum "D:\Development\AdhariNew\Web\Views\Customer\AddNewAddress.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "21bac6d1ec23ae35a09a34066602fdfb3acc6f6f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Customer_AddNewAddress), @"mvc.1.0.view", @"/Views/Customer/AddNewAddress.cshtml")]
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
#line 1 "D:\Development\AdhariNew\Web\Views\_ViewImports.cshtml"
using Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Development\AdhariNew\Web\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Development\AdhariNew\Web\Views\_ViewImports.cshtml"
using Microsoft.Extensions.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Development\AdhariNew\Web\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Builder;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Development\AdhariNew\Web\Views\_ViewImports.cshtml"
using Microsoft.Extensions.Options;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\Development\AdhariNew\Web\Views\_ViewImports.cshtml"
using Utility.Enum;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\Development\AdhariNew\Web\Views\_ViewImports.cshtml"
using Utility.Models.Base;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\Development\AdhariNew\Web\Views\_ViewImports.cshtml"
using Utility.Models.Frontend.CustomizedModel;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "D:\Development\AdhariNew\Web\Views\_ViewImports.cshtml"
using Utility.Models.Frontend.CustomerManagement;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "D:\Development\AdhariNew\Web\Views\_ViewImports.cshtml"
using Utility.Models.Frontend.Content;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "D:\Development\AdhariNew\Web\Views\_ViewImports.cshtml"
using Utility.Models.Frontend.Shop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "D:\Development\AdhariNew\Web\Views\_ViewImports.cshtml"
using Utility.Models.Frontend.Sales;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"21bac6d1ec23ae35a09a34066602fdfb3acc6f6f", @"/Views/Customer/AddNewAddress.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fd8610713b70f7256c3429c827c060fa488c518d", @"/Views/_ViewImports.cshtml")]
    public class Views_Customer_AddNewAddress : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Development\AdhariNew\Web\Views\Customer\AddNewAddress.cshtml"
  
    var breadCrumbItems = new List<BreadcrumbModel>();
    breadCrumbItems.Add(new BreadcrumbModel() { Url = Url.RouteUrl("myaddresses"), Title = SharedHtmlLocalizer["MyAddresses"].Value });
    breadCrumbItems.Add(new BreadcrumbModel() { Url = "#", Title = SharedHtmlLocalizer["AddNewAddress"].Value });

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Development\AdhariNew\Web\Views\Customer\AddNewAddress.cshtml"
Write(await Html.PartialAsync("_PageHeading", breadCrumbItems));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<section class=""main-content py-5 mb-5"">
    <div class=""container"">
        <div class=""row"">
            <div class=""col-12  mx-auto"">
                <div class=""card rounded-5 mt-3 mt-md-0 border-secondary shadow"">
                    <div class=""card-body"">
                        <div class=""delivery-address mb-4 g-3 p-3"">
                            <h4 class=""text-primary fw-bold mb-3"">");
#nullable restore
#line 16 "D:\Development\AdhariNew\Web\Views\Customer\AddNewAddress.cshtml"
                                                             Write(SharedHtmlLocalizer["DeliveryAddress"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                            ");
#nullable restore
#line 17 "D:\Development\AdhariNew\Web\Views\Customer\AddNewAddress.cshtml"
                       Write(await Html.PartialAsync("_AddNewAddress"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                        </div>
                        <div class=""col-12 text-center"">
                            <a href=""javascript:;"" id=""btn-save-address"" class=""btn btn-secondary rounded-pill px-5 fw-bold text-light"">
                                ");
#nullable restore
#line 21 "D:\Development\AdhariNew\Web\Views\Customer\AddNewAddress.cshtml"
                           Write(SharedHtmlLocalizer["SaveAddress"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>
<script>
    $(document).ready(function () {
        $(""#btn-save-address"").click(function () {
            addAddress();
            return false;
        });
    });

    function addAddress() {
        $(""#frm-address"").validate();
        if ($(""#frm-address"").valid()) {
            $.post(""");
#nullable restore
#line 41 "D:\Development\AdhariNew\Web\Views\Customer\AddNewAddress.cshtml"
               Write(Url.RouteUrl("addaddress"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""", {
                TypeId: $(""#hdnAddressTypeId"").val(),
                Name: $(""#AddressName"").val(),
                GovernorateId: $(""#Governorate"").val(),
                AreaId: $(""#Area"").val(),
                Block: $(""#Block"").val(),
                Street: $(""#Street"").val(),
                Avenue: $(""#Avenue"").val(),
                HouseNumber: $(""#HouseNo"").val(),
                BuildingNumber: $(""#BuildingNo"").val(),
                FloorNumber: $(""#FloorNo"").val(),
                FlatNumber: $(""#FlatNo"").val(),
                SchoolName: $(""#SchoolName"").val(),
                MosqueName: $(""#MosqueName"").val(),
                GovernmentEntity: $(""#GovernmentEntity"").val(),
                Notes: $(""#Notes"").val()
            }, function (result) {
                if (result.success && result.data != null) {
                    window.location.href = """);
#nullable restore
#line 59 "D:\Development\AdhariNew\Web\Views\Customer\AddNewAddress.cshtml"
                                       Write(Url.RouteUrl("myaddresses"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\";\r\n                }\r\n                else {\r\n                    if (result.statusCode == 401) {\r\n                        var returnUrl = \"");
#nullable restore
#line 63 "D:\Development\AdhariNew\Web\Views\Customer\AddNewAddress.cshtml"
                                     Write(string.IsNullOrEmpty(Context.Request.Path) ? "/" : $"{Context.Request.Path.Value}{Context.Request.QueryString}");

#line default
#line hidden
#nullable disable
            WriteLiteral(@""";
                        window.location.href = ""/login?returnUrl="" + returnUrl;
                        return;
                    }

                    if (result.message != """")
                        ToastAlert('error', '', result.message);
                }
            });
        }
    }
</script>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IHtmlLocalizer<SharedResource> SharedHtmlLocalizer { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IOptions<RequestLocalizationOptions> LocOptions { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IStringLocalizer<SharedResource> SharedLocalizer { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IViewLocalizer localizer { get; private set; }
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
