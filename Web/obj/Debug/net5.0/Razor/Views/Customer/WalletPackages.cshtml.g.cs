#pragma checksum "D:\Development\AdhariNew\Web\Views\Customer\WalletPackages.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5577467ea57430f0dc001253b131393433424d07"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Customer_WalletPackages), @"mvc.1.0.view", @"/Views/Customer/WalletPackages.cshtml")]
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
#line 3 "D:\Development\AdhariNew\Web\Views\Customer\WalletPackages.cshtml"
using Utility.Enum;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5577467ea57430f0dc001253b131393433424d07", @"/Views/Customer/WalletPackages.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fd8610713b70f7256c3429c827c060fa488c518d", @"/Views/_ViewImports.cshtml")]
    public class Views_Customer_WalletPackages : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IList<Utility.Models.Frontend.CouponPromotion.WalletPackageModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 4 "D:\Development\AdhariNew\Web\Views\Customer\WalletPackages.cshtml"
  
    var breadCrumbItems = new List<BreadcrumbModel>();
    breadCrumbItems.Add(new BreadcrumbModel() { Url = Url.RouteUrl("mywallet", new { walletType = (int)(WalletType.Wallet) }), Title = SharedHtmlLocalizer["MyWallet"].Value });
    breadCrumbItems.Add(new BreadcrumbModel() { Url = "#", Title = SharedHtmlLocalizer["WalletPrepaidCards"].Value });

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 10 "D:\Development\AdhariNew\Web\Views\Customer\WalletPackages.cshtml"
Write(await Html.PartialAsync("_PageHeading", breadCrumbItems));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<input type=""hidden"" id=""selectedPackageAmount"" value=""0"" />

<section class=""main-content py-5 mb-5"">
    <div class=""container"">
        <div class=""row"">
            <div class=""col-12 mb-4 packages-cards"">
                <h4 class=""text-primary text-start text-md-center fw-bold mb-3"">");
#nullable restore
#line 18 "D:\Development\AdhariNew\Web\Views\Customer\WalletPackages.cshtml"
                                                                           Write(SharedHtmlLocalizer["SelectWalletPrepaidCard"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                <div class=\"row justify-content-center\">\r\n\r\n");
#nullable restore
#line 21 "D:\Development\AdhariNew\Web\Views\Customer\WalletPackages.cshtml"
                     foreach (var item in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <div class=\"col-12 col-md-6 col-lg-4 col-xl-3 mb-4\">\r\n                            <div class=\"package-card py-4 px-3 d-flex flex-column body-bg-secondary border border-secondary rounded-4 align-items-center h-100\" data-amount=\"");
#nullable restore
#line 24 "D:\Development\AdhariNew\Web\Views\Customer\WalletPackages.cshtml"
                                                                                                                                                                        Write(item.FormattedAmount);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">\r\n                                <h4 class=\"mb-1 fw-bold text-primary text-center\">");
#nullable restore
#line 25 "D:\Development\AdhariNew\Web\Views\Customer\WalletPackages.cshtml"
                                                                             Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                                <h5 class=\"mb-3 fw-normal text-center\">");
#nullable restore
#line 26 "D:\Development\AdhariNew\Web\Views\Customer\WalletPackages.cshtml"
                                                                  Write(item.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                                <div class=\"d-flex flex-column w-100 mt-auto text-center\">\r\n                                    <a href=\"javascript:;\" class=\"btn btn-primary rounded-pill mb-0 fw-bold\">");
#nullable restore
#line 28 "D:\Development\AdhariNew\Web\Views\Customer\WalletPackages.cshtml"
                                                                                                        Write(SharedHtmlLocalizer["Buy"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 28 "D:\Development\AdhariNew\Web\Views\Customer\WalletPackages.cshtml"
                                                                                                                                    Write(item.FormattedAmount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n                                </div>\r\n                            </div>\r\n                        </div>\r\n");
#nullable restore
#line 32 "D:\Development\AdhariNew\Web\Views\Customer\WalletPackages.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </div>\r\n            </div>\r\n            <div class=\"col-12 col-lg-8 mx-auto payment-method mb-4\" style=\"display: none;\">\r\n                <h4 class=\"text-primary text-start text-md-center fw-bold mb-3\">");
#nullable restore
#line 37 "D:\Development\AdhariNew\Web\Views\Customer\WalletPackages.cshtml"
                                                                           Write(SharedHtmlLocalizer["PaymentMethod"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h4>
                <div class=""bg-grey rounded-4 p-3 mx-0"">
                    <ul class=""list-group list-group-flush px-0 mb-0"" id=""ul-payment-methods"">
                    </ul>
                </div>
            </div>
            <div class=""col-12 col-lg-8 mx-auto mb-4 payment-method"" style=""display: none;"">
                <div class=""bg-grey row rounded-4 p-3 mx-0"">
                    <ul class=""list-group list-group-flush pe-0"">
                        <li class=""list-group-item border-secondary d-flex justify-content-between fs-5 px-0"">
                            <label class=""text-muted"">");
#nullable restore
#line 47 "D:\Development\AdhariNew\Web\Views\Customer\WalletPackages.cshtml"
                                                 Write(SharedHtmlLocalizer["Total"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</label>
                            <p class=""text-primary mb-0 fw-bold"">
                                <span class=""crossed-out-price text-decoration-line-through text-muted me-2""></span><span class=""to-pay-price"" id=""total-amount""></span>
                            </p>
                        </li>
                    </ul>
                </div>
            </div>
            <div class=""offset-lg-4""></div>
            <div class=""col-12 col-lg-4 mx-auto text-center payment-method"" style=""display: none;"">
                <div class=""d-flex justify-content-center"">
                    <a href=""javascript:;"" class=""btn btn-secondary color-white rounded-pill col-5 me-2 fw-bold d-block d-md-none text-light package-card-back-btn"">Back</a>
                    <a href=""payment-details-for-package.html"" class=""btn btn-primary color-white rounded-pill col-5 fw-bold continue-btn"">Checkout</a>
                </div>
            </div>
        </div>
    </div>
</section>

<script>
    $(docum");
            WriteLiteral("ent).ready(function () {\r\n        getPaymentMethods();\r\n    });\r\n\r\n    function getPaymentMethods() {\r\n        var _url = \'");
#nullable restore
#line 72 "D:\Development\AdhariNew\Web\Views\Customer\WalletPackages.cshtml"
               Write(Url.RouteUrl("paymentmethod", new { paymentRequestType = (int)PaymentRequestType.WalletPackageOrder }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"'
        $.get(_url, function (result) {
            if (result.success) {
                let template = """";
                $.each(result.data, function (index, item) {
                    template += paymentMethodDiv(item);
                });
                $('#ul-payment-methods').html(template);
            }
            else {
                if (result.message != """")
                    ToastAlert('error', '', result.message);
            }
        });
    };

    function paymentMethodDiv(item) {
        var html = `<div class=""inputGroup border-secondary list-group-item d-flex justify-content-between align-items-center px-0"">
                        <label for=""payment-method-radio${item.id}"" class="""">
                            <img src=""${item.imageUrl}"" class="""" alt=""product-image"" height=""25"">
                        </label>
                        <div class=""d-flex align-items-center"">
                            <label for=""payment-method-radio${item.id}"" class=""text");
            WriteLiteral(@"-end"">${item.name}</label>
                            <input id=""payment-method-radio${item.id}"" name=""payment-method-radio"" class=""ms-1 ms-md-3"" type=""radio"">
                        </div>
                    </div>`;
        return html;
    };

    $("".package-card"").click(function () {
        var amount = $(this).data('amount');
        setLabelValue(""total-amount"", amount);
    });
</script>

<script>
    $("".package-card"").click(function () {
        $("".package-card"").removeClass(""active"");
        $(this).addClass(""active"");
        $("".payment-method"").show();
        // For Mobile
        if ($(window).width() < 480) {
            $("".packages-cards"").hide();
        }
        $(""html, body"").animate({ scrollTop: $(document).height() }, 1000);
    });
    $("".package-card-back-btn"").click(function () {
        // $(""html, body"").animate({ scrollTop: 0 }, 500);

        $("".packages-cards"").show();
        $("".payment-method"").hide();

    });
    $('input[type=radio");
            WriteLiteral(@"][name=payment-method-radio]').change(function () {
        $(this).closest('ul').find('.inputGroup').removeClass('active');
        $(this).closest('.inputGroup').addClass('active');
        // $('.continue-btn').removeClass('disabled');
    });
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IList<Utility.Models.Frontend.CouponPromotion.WalletPackageModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
