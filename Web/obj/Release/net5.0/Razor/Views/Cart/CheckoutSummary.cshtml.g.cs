#pragma checksum "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "97a2d224ad75c6f2eaf19ea37df5b00eb75e64d2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Cart_CheckoutSummary), @"mvc.1.0.view", @"/Views/Cart/CheckoutSummary.cshtml")]
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
#line 1 "D:\Development\Adhari\Web\Views\_ViewImports.cshtml"
using Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Development\Adhari\Web\Views\_ViewImports.cshtml"
using Microsoft.Extensions.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Development\Adhari\Web\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Builder;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Development\Adhari\Web\Views\_ViewImports.cshtml"
using Microsoft.Extensions.Options;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\Development\Adhari\Web\Views\_ViewImports.cshtml"
using Utility.Models.Base;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\Development\Adhari\Web\Views\_ViewImports.cshtml"
using Utility.Models.Frontend.CustomizedModel;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "D:\Development\Adhari\Web\Views\_ViewImports.cshtml"
using Utility.Models.Frontend.CustomerManagement;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "D:\Development\Adhari\Web\Views\_ViewImports.cshtml"
using Utility.Models.Frontend.Content;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "D:\Development\Adhari\Web\Views\_ViewImports.cshtml"
using Utility.Models.Frontend.Shop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
using Utility.Enum;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"97a2d224ad75c6f2eaf19ea37df5b00eb75e64d2", @"/Views/Cart/CheckoutSummary.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"46a65b4b2ada0dd24edf72a9f6a2735722af620c", @"/Views/_ViewImports.cshtml")]
    public class Views_Cart_CheckoutSummary : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Utility.Models.Frontend.Shop.CheckOutModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 5 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
  
    var breadCrumbItems = new List<BreadcrumbModel>();
    breadCrumbItems.Add(new BreadcrumbModel() { Url = Url.RouteUrl("checkoutaddress"), Title = SharedHtmlLocalizer["Addresses"].Value });
    breadCrumbItems.Add(new BreadcrumbModel() { Url = "#", Title = SharedHtmlLocalizer["Review"].Value });

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
Write(await Html.PartialAsync("_PageHeading", breadCrumbItems));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<section class=""main-content py-5 mb-5"">
    <div class=""container"">
        <div class=""row"">
            <div class=""col-12 offset-lg-2 col-lg-8"">

                <div class=""customer-details mb-4"">
                    <div class=""d-flex justify-content-between flex-column align-items-start align-items-md-center mb-3"">
                        <h4 class=""text-primary text-start text-lg-center fw-bold mb-0"">");
#nullable restore
#line 18 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                                                   Write(SharedHtmlLocalizer["CustomerDetails"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                        <span class=\"text-muted text-start text-lg-center \">\r\n                            ");
#nullable restore
#line 20 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                       Write(SharedHtmlLocalizer["Not"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <b>");
#nullable restore
#line 20 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                      Write(Model.Customer.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b> <a class=\"text-nowrap\"");
            BeginWriteAttribute("href", " href=\"", 1196, "\"", 1226, 1);
#nullable restore
#line 20 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
WriteAttributeValue("", 1203, Url.RouteUrl("logout"), 1203, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 20 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                                                                                                     Write(SharedHtmlLocalizer["SignOut"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n                        </span>\r\n                    </div>\r\n                    <div class=\"bg-grey row rounded-4 p-3 mx-0\">\r\n                        <div class=\"col-12 col-md-4\">\r\n                            <label for=\"name\" class=\"fw-bold\">");
#nullable restore
#line 25 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                         Write(SharedHtmlLocalizer["Name"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                            <p class=\"text-primary mb-0\">");
#nullable restore
#line 26 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                    Write(Model.Customer.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        </div>\r\n                        <div class=\"col-12 col-md-4\">\r\n                            <label for=\"email\" class=\"fw-bold\">");
#nullable restore
#line 29 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                          Write(SharedHtmlLocalizer["Email"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                            <p class=\"text-primary mb-0\">");
#nullable restore
#line 30 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                    Write(Model.Customer.EmailAddress);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        </div>\r\n                        <div class=\"col-12 col-md-4\">\r\n                            <label for=\"number\" class=\"fw-bold\">");
#nullable restore
#line 33 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                           Write(SharedHtmlLocalizer["MobileNumber"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                            <p class=\"text-primary mb-0\">");
#nullable restore
#line 34 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                    Write(Model.Customer.FormattedMobile);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                        </div>
                    </div>
                </div>
                <div class=""delivery-details mb-4"">
                    <div class=""d-flex justify-content-start justify-content-lg-center align-items-center mb-3"">
                        <h4 class=""text-primary text-start text-lg-center fw-bold mb-0"">");
#nullable restore
#line 40 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                                                   Write(SharedHtmlLocalizer["DeliveryDetails"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                    </div>\r\n                    <div class=\"bg-grey row rounded-4 p-3 mx-0 mb-3\">\r\n                        <div class=\"col-12\">\r\n                            <label for=\"name\" class=\"fw-bold\">");
#nullable restore
#line 44 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                         Write(SharedHtmlLocalizer["DeliveryAddress"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                            <p class=\"text-primary mb-0\">\r\n                                ");
#nullable restore
#line 46 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                           Write(Model.AddressText);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                            </p>
                        </div>
                    </div>
                    <div class=""bg-grey row rounded-4 p-3 mx-0"">
                        <div class=""col-12"">
                            <label for=""name"" class=""fw-bold"">");
#nullable restore
#line 52 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                         Write(SharedHtmlLocalizer["DeliveryDate"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                            <p class=\"text-primary mb-0\">");
#nullable restore
#line 53 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                    Write(Model.EstimatedDeliveryValue);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                        </div>
                    </div>
                </div>
            </div>
            <div class=""col-12 offset-lg-2 col-lg-8"">
                <div class=""your-order mb-4"">
                    <h4 class=""text-primary text-start text-lg-center fw-bold mb-3"">");
#nullable restore
#line 60 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                                               Write(SharedHtmlLocalizer["YourItems"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                    <ul class=\"list-group list-group-flush\">\r\n");
#nullable restore
#line 62 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                         foreach (var CartItem in Model.CartItems)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <li class=\"list-group-item border-0 px-0 py-2\">\r\n                                <div class=\"d-flex flex-row bg-grey rounded-4 p-2\">\r\n                                    <img");
            BeginWriteAttribute("src", " src=\"", 4048, "\"", 4080, 1);
#nullable restore
#line 66 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
WriteAttributeValue("", 4054, CartItem.Product.ImageUrl, 4054, 26, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"me-3 rounded-3\" alt=\"product-image\" height=\"75\">\r\n                                    <div class=\"d-flex flex-column me-auto w-100\">\r\n                                        <a");
            BeginWriteAttribute("href", " href=\"", 4265, "\"", 4298, 1);
#nullable restore
#line 68 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
WriteAttributeValue("", 4272, CartItem.Product.ImageUrl, 4272, 26, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"mb-0 fw-bold\">");
#nullable restore
#line 68 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                                                             Write(CartItem.Product.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</a>
                                        <div class=""row"">
                                            <div class=""col-5"">
                                                <div class=""w-auto py-1 me-2"">
                                                    <label class=""text-muted""");
            BeginWriteAttribute("for", " for=\"", 4631, "\"", 4637, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 72 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                                                Write(SharedHtmlLocalizer["Quantity"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                                                    <p class=\"text-primary fw-bold fs-51 mb-0\">");
#nullable restore
#line 73 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                                                          Write(CartItem.Quantity);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                                                </div>
                                            </div>
                                            <div class=""col-7"">
                                                <div class=""w-auto py-1 me-2"">
                                                    <label class=""text-muted""");
            BeginWriteAttribute("for", " for=\"", 5130, "\"", 5136, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 78 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                                                Write(SharedHtmlLocalizer["Total"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                                                    <p class=\"text-primary fw-bold fs-51 mb-0\">");
#nullable restore
#line 79 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                                                          Write(CartItem.FormattedTotal);

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
#line 86 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    </ul>
                </div>
            </div>
            <div class=""col-12"">
                <div class=""row"">
                    <div class=""col-12 offset-lg-2 col-lg-8"">
                        <div class=""promotion-code mb-4"">
                            <h4 class=""text-primary text-start text-md-center fw-bold mb-3"">");
#nullable restore
#line 94 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                                                       Write(SharedHtmlLocalizer["PromotionCode"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h4>
                            <div class=""bg-grey rounded-4 p-3 mx-0"">
                                <div class=""w-100 text-center"">
                                    <div class=""input-group promotion-input-field"">
                                        <input type=""text"" id=""txt-coupon-code"" class=""form-control border-secondary body-bg-secondary rounded-end rounded-3""");
            BeginWriteAttribute("value", "\r\n                                               value=\"", 6379, "\"", 6464, 1);
#nullable restore
#line 99 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
WriteAttributeValue("", 6435, Model.CartSummary.CouponCode, 6435, 29, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("placeholder", " placeholder=\"", 6465, "\"", 6512, 1);
#nullable restore
#line 99 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
WriteAttributeValue("", 6479, SharedHtmlLocalizer["EnterCode"], 6479, 33, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                        <button id=\"btn-apply-coupon-code\" class=\"btn btn-outline-secondary rounded-start rounded-3 body-bg-secondary fw-bold promotion-code-button\" type=\"button\">\r\n                                            ");
#nullable restore
#line 101 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                        Write(string.IsNullOrEmpty(Model.CartSummary.CouponCode)? @SharedHtmlLocalizer["Apply"] : @SharedHtmlLocalizer["Remove"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class=""col-12 offset-lg-2 col-lg-8"">
                        <div class=""payment-method mb-4"">
                            <h4 class=""text-primary text-start text-md-center fw-bold mb-3"">");
#nullable restore
#line 110 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                                                       Write(SharedHtmlLocalizer["PaymentMethod"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n");
#nullable restore
#line 111 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                             if (Model.CartSummary.WalletBalanceAmount > 0)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                <div class=""inputGroup border-0 border-secondary rounded-4 body-bg-secondary d-flex justify-content-between align-items-center mb-3 py-3 px-0"">
                                    <div class=""d-flex ms-3"">
                                        <input id=""payment-method-wallet"" class=""form-check-input mt-0"" type=""checkbox"">
                                        <div class=""ms-3"">
                                            <span>");
#nullable restore
#line 117 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                             Write(SharedHtmlLocalizer["PayFromWallet"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                            <strong class=\"ms-2\">");
#nullable restore
#line 118 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                            Write(Model.CartSummary.FormattedWalletBalanceAmount);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</strong>
                                        </div>
                                    </div>
                                    <label for=""payment-method-wallet"" class=""me-1"">
                                        <i class=""fa-solid fa-wallet me-2 fs-4""></i>
                                    </label>
                                </div>
");
#nullable restore
#line 125 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div class=\"bg-grey rounded-4 p-3 mx-0\">\r\n                                <ul class=\"list-group list-group-flush px-0 mb-0\">\r\n");
#nullable restore
#line 128 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                     foreach (var paymentMethod in Model.PaymentMethods)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                        <div class=""inputGroup border-secondary list-group-item d-flex justify-content-between align-items-center px-0"">
                                            <div class=""d-flex"">
                                                <input");
            BeginWriteAttribute("id", " id=\"", 9052, "\"", 9089, 2);
            WriteAttributeValue("", 9057, "payment-method-", 9057, 15, true);
#nullable restore
#line 132 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
WriteAttributeValue("", 9072, paymentMethod.Id, 9072, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" name=\"payment-method-radio\" class=\"me-3\" type=\"radio\"");
            BeginWriteAttribute("value", "\r\n                                                       value=\"", 9144, "\"", 9225, 1);
#nullable restore
#line 133 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
WriteAttributeValue("", 9208, paymentMethod.Id, 9208, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 133 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                                             Write(paymentMethod.Selected ? "checked" : "");

#line default
#line hidden
#nullable disable
            WriteLiteral(">\r\n                                                <label");
            BeginWriteAttribute("for", " for=\"", 9326, "\"", 9364, 2);
            WriteAttributeValue("", 9332, "payment-method-", 9332, 15, true);
#nullable restore
#line 134 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
WriteAttributeValue("", 9347, paymentMethod.Id, 9347, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("class", " class=\"", 9365, "\"", 9373, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 134 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                                                                  Write(paymentMethod.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                                            </div>\r\n                                            <label for=\"payment-method-radio0\"");
            BeginWriteAttribute("class", " class=\"", 9534, "\"", 9542, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                                                <img");
            BeginWriteAttribute("src", " src=\"", 9598, "\"", 9627, 1);
#nullable restore
#line 137 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
WriteAttributeValue("", 9604, paymentMethod.ImageUrl, 9604, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("class", " class=\"", 9628, "\"", 9636, 0);
            EndWriteAttribute();
            WriteLiteral(" alt=\"product-image\" height=\"25\">\r\n                                            </label>\r\n                                        </div>\r\n");
#nullable restore
#line 140 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                </ul>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"col-12 offset-lg-2 col-lg-8 cart-summary\">\r\n                        ");
#nullable restore
#line 146 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                   Write(await Html.PartialAsync("_CartSummary", Model.CartSummary));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                    </div>
                </div>
            </div>
            <div class=""col-12 col-md-6 col-xl-4 mx-auto text-start mb-3"">
                <div class=""form-check"">
                    <input class=""form-check-input mt-0 me-2"" type=""checkbox"" id=""gridCheck"">
                    <label class=""form-check-label d-block"" for=""gridCheck"">
                        ");
#nullable restore
#line 154 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                   Write(SharedHtmlLocalizer["IAgreeToThe"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        <a id=\"a-privacy-policy\" class=\"text-primary fw-bold\" href=\"#\">\r\n                            ");
#nullable restore
#line 156 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                       Write(SharedHtmlLocalizer["PrivacyPolicy"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </a> & <a id=\"a-terms\" class=\"text-primary fw-bold\" href=\"#\">\r\n                            ");
#nullable restore
#line 158 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                       Write(SharedHtmlLocalizer["TermsAndConditions"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                        </a>
                    </label>
                </div>
                <div id=""dvErrorhdnTerms"">
                </div>
            </div>
        </div>
        <div class=""col-12 text-center"">
            <a id=""a-checkout"" href=""#"" class=""btn btn-primary color-white rounded-pill fw-bold px-5 continue-btn"">");
#nullable restore
#line 167 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                                                                              Write(SharedHtmlLocalizer["Checkout"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n        </div>\r\n    </div>\r\n</section>\r\n<input type=\"hidden\" id=\"hdn-applied-coupon-code\" />\r\n<script>\r\n    jQuery(document).ready(function () {\r\n        $(\"#hdn-applied-coupon-code\").val(\'");
#nullable restore
#line 174 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                      Write(Model.CartSummary.CouponCode);

#line default
#line hidden
#nullable disable
            WriteLiteral("\');\r\n        $(\'input[type=radio][name=payment-method-radio]\').change(function () {\r\n            $.post(\"");
#nullable restore
#line 176 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
               Write(Url.RouteUrl("savecartattributes"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\", { PaymentMethodId: this.value, AttributeTypeId: ");
#nullable restore
#line 176 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                                                                      Write((int)AttributeType.SelectPaymentMethod);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" }, function (result) {
                if (result.success) {
                    $("".cart-summary"").empty();
                    $("".cart-summary"").append(result.formattedCartSummary);
                }
                else {
                    if (result.message != """")
                        ToastAlert('error', '', result.message);
                }
            });
        });

        $(""#payment-method-wallet"").change(function () {
            var attributeTypeId = 0;

            if ($(this).is(':checked')) {
                attributeTypeId = ");
#nullable restore
#line 192 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                              Write((int)AttributeType.AddWalletAmount);

#line default
#line hidden
#nullable disable
            WriteLiteral(";\r\n            } else {\r\n                attributeTypeId = ");
#nullable restore
#line 194 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                              Write((int)AttributeType.RemoveWalletAmount);

#line default
#line hidden
#nullable disable
            WriteLiteral(";\r\n            }\r\n\r\n            $.post(\"");
#nullable restore
#line 197 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
               Write(Url.RouteUrl("savecartattributes"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""", { AttributeTypeId: attributeTypeId }, function (result) {
                if (result.success) {
                    $("".cart-summary"").empty();
                    $("".cart-summary"").append(result.formattedCartSummary);
                }
                else {
                    if (result.message != """")
                        ToastAlert('error', '', result.message);
                }
            });
        });

        $('#btn-apply-coupon-code').click(function () {
            var couponCode = $(""#txt-coupon-code"").val();
            var currentCouponCode = $(""#hdn-applied-coupon-code"").val();

            if (currentCouponCode == """") {
                if (couponCode == """") {
                    ToastAlert('error', '', """);
#nullable restore
#line 215 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                        Write(SharedHtmlLocalizer["Enter coupon code"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\");\r\n                    return false;\r\n                }\r\n            }\r\n\r\n            var attributeTypeId = 0;\r\n            if (currentCouponCode != \"\") {\r\n                attributeTypeId = ");
#nullable restore
#line 222 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                              Write((int)AttributeType.RemoveCoupon);

#line default
#line hidden
#nullable disable
            WriteLiteral(";\r\n            }\r\n            else {\r\n                attributeTypeId = ");
#nullable restore
#line 225 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                              Write((int)AttributeType.ApplyCoupon);

#line default
#line hidden
#nullable disable
            WriteLiteral(";\r\n            }\r\n\r\n            $.post(\"");
#nullable restore
#line 228 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
               Write(Url.RouteUrl("savecartattributes"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""", { CouponCode: couponCode, AttributeTypeId: attributeTypeId }, function (result) {
                if (result.success) {
                    $("".cart-summary"").empty();
                    $("".cart-summary"").append(result.formattedCartSummary);

                    currentCouponCode = result.cartSummary.couponCode;
                    $(""#hdn-applied-coupon-code"").val(currentCouponCode);

                    if (currentCouponCode != """" && currentCouponCode != null) {
                        $(""#btn-apply-coupon-code"").text(""");
#nullable restore
#line 237 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                     Write(SharedHtmlLocalizer["Remove"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\");\r\n                    }\r\n                    else {\r\n                        $(\"#txt-coupon-code\").val(\'\');\r\n                        $(\"#btn-apply-coupon-code\").text(\"");
#nullable restore
#line 241 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                     Write(SharedHtmlLocalizer["Apply"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""");
                    }
                }
                else {
                    if (result.message != """")
                        ToastAlert('error', '', result.message);
                }
            });

            return false;
        });

        $(""#a-privacy-policy"").click(function () {
            $.get(""");
#nullable restore
#line 254 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
              Write(Url.RouteUrl("getsitecontents", new { appContentType = (int)AppContentType.PrivacyPolicy }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\", function (result) {\r\n                if (result.content != \"\") {\r\n                    $(\"#common-model-popup-title\").html(\"");
#nullable restore
#line 256 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                    Write(SharedHtmlLocalizer["PrivacyPolicy"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""");
                    $(""#common-model-popup-description"").html(result.content);
                    $('#common-model-popup').modal('show');
                }
            });
        });

        $(""#a-terms"").click(function () {
            $.get(""");
#nullable restore
#line 264 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
              Write(Url.RouteUrl("getsitecontents", new { appContentType = (int)AppContentType.TermsCondition }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\", function (result) {\r\n                if (result.content != \"\") {\r\n                    $(\"#common-model-popup-title\").html(\"");
#nullable restore
#line 266 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                    Write(SharedHtmlLocalizer["TermsAndConditions"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""");
                    $(""#common-model-popup-description"").html(result.content);
                    $('#common-model-popup').modal('show');
                }
            });
        });

        $(""#a-checkout"").click(function () {
            var paymentMethodId = $('input[name=""payment-method-radio""]:checked').val();
            if (paymentMethodId == """" || paymentMethodId == null || paymentMethodId == undefined) {
                ToastAlert('error', '', """);
#nullable restore
#line 276 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                    Write(SharedHtmlLocalizer["PleaseChoosePaymentMethod"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\");\r\n                return false;\r\n            }\r\n\r\n            if ($(\"#gridCheck\").prop(\'checked\') == false) {\r\n                $(\"#dvErrorhdnTerms\").html(\"<div class=\'invalid-feedback has-error\' style=\'display:block !important;\'><span>");
#nullable restore
#line 281 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
                                                                                                                       Write(SharedHtmlLocalizer["Please check 'I agree to the Terms & Conditions'"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></div>\");\r\n                return false;\r\n            }\r\n\r\n            showLoader();\r\n\r\n            $.get(\"");
#nullable restore
#line 287 "D:\Development\Adhari\Web\Views\Cart\CheckoutSummary.cshtml"
              Write(Url.RouteUrl("createorder"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""", function (result) {                
                if (result.data != null) {
                    if (result.data.redirectToPaymentPage) {
                        window.location.href = result.data.paymentUrl;
                    }
                    else {
                        window.location.href = ""/ORD/"" + result.data.entityNumber;
                    }
                }
                else {
                    hideLoader();
                    if (result.message != """")
                        ToastAlert('error', '', result.message);
                }
            });

            return false;
        });
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Utility.Models.Frontend.Shop.CheckOutModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
