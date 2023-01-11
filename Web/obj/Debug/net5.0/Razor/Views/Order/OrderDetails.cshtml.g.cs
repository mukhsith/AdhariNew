#pragma checksum "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a8122b4efd471889be8b7207c1c0e7efaf17c05b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Order_OrderDetails), @"mvc.1.0.view", @"/Views/Order/OrderDetails.cshtml")]
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
#nullable restore
#line 1 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
using Utility.Enum;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a8122b4efd471889be8b7207c1c0e7efaf17c05b", @"/Views/Order/OrderDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fd8610713b70f7256c3429c827c060fa488c518d", @"/Views/_ViewImports.cshtml")]
    public class Views_Order_OrderDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Utility.Models.Frontend.Sales.OrderModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 5 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
  
    var requestCultureFeature = System.Globalization.CultureInfo.CurrentCulture.Name;
    Layout = "_Layout";

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
  
    var breadCrumbItems = new List<BreadcrumbModel>();
    breadCrumbItems.Add(new BreadcrumbModel() { Url = Url.RouteUrl("myorders"), Title = SharedHtmlLocalizer["MyOrders"].Value });
    breadCrumbItems.Add(new BreadcrumbModel() { Url = "#", Title = SharedHtmlLocalizer["OrderDetails"].Value });

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
Write(await Html.PartialAsync("_PageHeading", breadCrumbItems));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<section class=""main-content py-5 mb-5"">
    <div class=""container"">
        <div class=""row"">
            <div class=""col-12 offset-lg-2 col-lg-8"">
                <div class=""order-summary mb-4"">
                    <h4 class=""text-primary text-start text-md-center fw-bold mb-3"">");
#nullable restore
#line 20 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
                                                                               Write(SharedHtmlLocalizer["OrderSummary"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                    <div class=\"bg-grey row rounded-4 p-3 mx-0\">\r\n                        <ul class=\"list-group list-group-flush\">\r\n");
#nullable restore
#line 23 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
                             foreach (var item in Model.AmountSplitUps)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <li class=\"list-group-item border-secondary d-flex justify-content-between px-0\">\r\n                                    <label class=\"text-muted\">");
#nullable restore
#line 26 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
                                                         Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                                    <p class=\"text-primary mb-0 fw-bold\">");
#nullable restore
#line 27 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
                                                                    Write(item.Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                </li>\r\n");
#nullable restore
#line 29 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <li class=\"list-group-item border-secondary d-flex justify-content-between fs-5 px-0\">\r\n                                <label class=\"text-muted\">");
#nullable restore
#line 31 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
                                                     Write(SharedHtmlLocalizer["Total"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                                <p class=\"text-primary mb-0 fw-bold\">\r\n                                    <span class=\"crossed-out-price text-decoration-line-through text-muted me-2\"></span><span class=\"to-pay-price\">");
#nullable restore
#line 33 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
                                                                                                                                              Write(Model.FormattedTotal);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</span>
                                </p>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class=""col-12 offset-lg-2 col-lg-8"">
                <div class=""payment-summary mb-4"">
                    <h4 class=""text-primary text-start text-md-center fw-bold mb-3"">");
#nullable restore
#line 42 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
                                                                               Write(SharedHtmlLocalizer["PaymentSummary"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                    <div class=\"bg-grey row rounded-4 p-3 mx-0\">\r\n                        <ul class=\"list-group list-group-flush list-card rounded-top rounded-bottom\">\r\n");
#nullable restore
#line 45 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
                             foreach (var item in Model.PaymentSummary)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <li class=\"list-group-item d-flex justify-content-between border-secondary px-0\">\r\n                                    <p class=\"mb-0 text-muted\">");
#nullable restore
#line 48 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
                                                          Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                    <p class=\"mb-0 text-primary fw-bold text-end \">");
#nullable restore
#line 49 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
                                                                              Write(item.Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                </li>\r\n");
#nullable restore
#line 51 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        </ul>
                    </div>
                </div>
            </div>
            <div class=""col-12 offset-lg-2 col-lg-8"">
                <div class=""your-items mb-4"">
                    <h4 class=""text-primary text-start text-md-center fw-bold mb-3"">");
#nullable restore
#line 58 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
                                                                               Write(SharedHtmlLocalizer["YourItems"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                    <div class=\"bg-grey row rounded-4 p-3 mx-0\">\r\n                        <ul class=\"list-group list-group-flush\">\r\n");
#nullable restore
#line 61 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
                             foreach (var item in Model.OrderItems)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <li class=\"list-group-item border-0 px-0 py-2\">\r\n                                    <div class=\"d-flex flex-row bg-grey rounded-4 p-2\">\r\n                                        <img");
            BeginWriteAttribute("src", " src=\"", 3872, "\"", 3900, 1);
#nullable restore
#line 65 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
WriteAttributeValue("", 3878, item.Product.ImageUrl, 3878, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"me-3 rounded-3\" alt=\"product-image\" height=\"75\">\r\n                                        <div class=\"d-flex flex-column me-auto w-100\">\r\n                                            <a");
            BeginWriteAttribute("href", " href=\"", 4093, "\"", 4124, 1);
#nullable restore
#line 67 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
WriteAttributeValue("", 4100, item.Product.DetailsUrl, 4100, 24, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"mb-0 fw-bold\">");
#nullable restore
#line 67 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
                                                                                               Write(item.Product.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</a>
                                            <div class=""row"">
                                                <div class=""col-5"">
                                                    <div class=""w-auto py-1 me-2"">
                                                        <label class=""text-muted""");
            BeginWriteAttribute("for", " for=\"", 4469, "\"", 4475, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 71 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
                                                                                    Write(SharedHtmlLocalizer["Quantity"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                                                        <p class=\"text-primary fw-bold fs-51 mb-0\">");
#nullable restore
#line 72 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
                                                                                              Write(item.Quantity);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                                                    </div>
                                                </div>
                                                <div class=""col-7"">
                                                    <div class=""w-auto py-1 me-2"">
                                                        <label class=""text-muted""");
            BeginWriteAttribute("for", " for=\"", 4988, "\"", 4994, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 77 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
                                                                                    Write(SharedHtmlLocalizer["Total"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                                                        <p class=\"text-primary fw-bold fs-51 mb-0\">");
#nullable restore
#line 78 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
                                                                                              Write(item.FormattedTotal);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </li>
");
#nullable restore
#line 85 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </ul>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n            ");
#nullable restore
#line 90 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
       Write(await Html.PartialAsync("_DeliveryDetails", Model.Address));

#line default
#line hidden
#nullable disable
            WriteLiteral(@";
            <div class=""col-12 col-md-6"">
                <div class="" mb-4"">
                    <h4 class=""text-primary fw-bold mb-3""></h4>
                    <div class=""col-12 mb-2 mb-md-0"">
                    </div>
                </div>
            </div>
        </div>
        <div class=""row"">
            <div class=""col-12 text-center"">
                <a href=""#"" id=""a-reorder"" class=""btn rounded-pill btn-secondary text-light ms-1"">");
#nullable restore
#line 101 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
                                                                                             Write(SharedHtmlLocalizer["Reorder"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</section>\r\n<script>\r\n    jQuery(document).ready(function () {\r\n        $(\"#a-reorder\").click(function (e) {\r\n            e.preventDefault();\r\n            $.get(\"");
#nullable restore
#line 110 "D:\Development\AdhariNew\Web\Views\Order\OrderDetails.cshtml"
              Write(Url.RouteUrl("reorder", new { id=Model.Id }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""", function (result) {
                if (result.success && result.data) {
                    updateCartCount();
                    $(""#a-cart"").css(""cursor"", ""pointer"");
                    $(""#a-cart"").trigger(""click"")
                }
            });
        });
    });
</script>


");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Utility.Models.Frontend.Sales.OrderModel> Html { get; private set; }
    }
}
#pragma warning restore 1591