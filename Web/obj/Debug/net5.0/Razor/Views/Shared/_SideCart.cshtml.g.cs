#pragma checksum "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "11d41fc0407e5497d0bb7dc5f7914c1eba781ceb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__SideCart), @"mvc.1.0.view", @"/Views/Shared/_SideCart.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"11d41fc0407e5497d0bb7dc5f7914c1eba781ceb", @"/Views/Shared/_SideCart.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fd8610713b70f7256c3429c827c060fa488c518d", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__SideCart : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Utility.Models.Frontend.Shop.CartModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<div class=\"offcanvas-header border-bottom\">\r\n    <h5 id=\"offcanvasCartLabel\" class=\"mb-0\">");
#nullable restore
#line 3 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
                                        Write(SharedHtmlLocalizer["Cart"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n    <button type=\"button\" class=\"btn-close text-reset\" data-bs-dismiss=\"offcanvas\"");
            BeginWriteAttribute("aria-label", " aria-label=\"", 255, "\"", 297, 1);
#nullable restore
#line 4 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
WriteAttributeValue("", 268, SharedHtmlLocalizer["Close"], 268, 29, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("></button>\r\n</div>\r\n<div class=\"offcanvas-body p-0\">\r\n    <ul class=\"cart-list list-group list-group-flush\">\r\n");
#nullable restore
#line 8 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
         foreach (var CartItem in Model.CartItems)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li class=\"list-group-item px-1 py-2 cart-item\">\r\n                <div class=\"d-flex bg-grey rounded-4 flex-row p-2\">\r\n                    <img");
            BeginWriteAttribute("src", " src=\"", 626, "\"", 658, 1);
#nullable restore
#line 12 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
WriteAttributeValue("", 632, CartItem.Product.ImageUrl, 632, 26, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"me-3 rounded-3\"");
            BeginWriteAttribute("alt", " alt=\"", 682, "\"", 711, 1);
#nullable restore
#line 12 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
WriteAttributeValue("", 688, CartItem.Product.Title, 688, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" height=\"75\">\r\n                    <div class=\"d-flex flex-column me-auto\">\r\n                        <p class=\"mb-0 text-muted fw-bold\">");
#nullable restore
#line 14 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
                                                      Write(CartItem.Product.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                        <div class=""btn-with-input-spinner w-50"">
                            <input type=""number"" value=""1"" min=""0"" max=""50"" step=""1"" class=""input-spinner text-white bg-secondary fs-5 border-0"" style=""display: none;"">
                            <div class=""input-group rounded-5 side-cart-spinner""");
            BeginWriteAttribute("style", " style=\"", 1194, "\"", 1202, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                <button style=""min-width: 2.5rem"" class=""btn btn-decrement btn-secondary text-white fw-bold fs-5 rounded-5 rounded-end btn-minus-side-cart"" type=""button"">
                                    <strong>−</strong>
                                </button>
                                <input type=""text"" inputmode=""decimal"" style=""text-align: center"" class=""form-control input-spinner text-white bg-secondary fs-5 border-0""");
            BeginWriteAttribute("value", "\r\n                                       value=\"", 1663, "\"", 1729, 1);
#nullable restore
#line 22 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
WriteAttributeValue("", 1711, CartItem.Quantity, 1711, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-cartid=\"");
#nullable restore
#line 22 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
                                                                          Write(CartItem.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-maxquantity=\"");
#nullable restore
#line 22 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
                                                                                                          Write(CartItem.Product.StockQuantity);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""" disabled>
                                <button style=""min-width: 2.5rem"" class=""btn btn-increment btn-secondary text-white fw-bold fs-5 rounded-5 rounded-start btn-plus-side-cart"" type=""button"">
                                    <strong>+</strong>
                                </button>
                            </div>
                        </div>
                        <!-- <label class=""mt-2 mb-0"">Unit Price:</label> -->
");
#nullable restore
#line 29 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
                         if (CartItem.Product.DiscountedPrice > 0)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <h6 class=\"fw-bold mb-0 text-muted my-2\">");
#nullable restore
#line 31 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
                                                                Write(SharedHtmlLocalizer["UnitPrice"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(": <span class=\"text-primary\">");
#nullable restore
#line 31 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
                                                                                                                              Write(CartItem.Product.DiscountedPrice);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></h6>\r\n");
#nullable restore
#line 32 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
                        }
                        else
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <h6 class=\"fw-bold mb-0 text-muted my-2\">");
#nullable restore
#line 35 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
                                                                Write(SharedHtmlLocalizer["UnitPrice"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(": <span class=\"text-primary\">");
#nullable restore
#line 35 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
                                                                                                                              Write(CartItem.Product.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></h6>\r\n");
#nullable restore
#line 36 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\r\n                    <div class=\"d-flex flex-column align-items-start\">\r\n                        <a href=\"#\"");
            BeginWriteAttribute("onclick", " onclick=\"", 2942, "\"", 2987, 4);
            WriteAttributeValue("", 2952, "return", 2952, 6, true);
            WriteAttributeValue(" ", 2958, "deletecartitem(", 2959, 16, true);
#nullable restore
#line 39 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
WriteAttributeValue("", 2974, CartItem.Id, 2974, 12, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2986, ")", 2986, 1, true);
            EndWriteAttribute();
            WriteLiteral("><i class=\"fa fa-trash color-danger font-24 mb-3\"></i></a>\r\n                    </div>\r\n                </div>\r\n            </li>\r\n");
#nullable restore
#line 43 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </ul>\r\n</div>\r\n<div class=\"offcanvas-footer border-top p-3 text-center\">\r\n    <p class=\"text-uppercase text-primary mb-2\">");
#nullable restore
#line 47 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
                                           Write(SharedHtmlLocalizer["GrandTotal"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n    <h5 class=\"text-uppercase fw-bold mb-3\">");
#nullable restore
#line 48 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
                                       Write(Model.FormattedSubTotal);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n    <div class=\"d-flex flex-row\">\r\n        <a");
            BeginWriteAttribute("href", " href=\"", 3416, "\"", 3448, 1);
#nullable restore
#line 50 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
WriteAttributeValue("", 3423, Url.RouteUrl("checkout"), 3423, 25, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-primary rounded-pill text-white fw-bold w-100\">");
#nullable restore
#line 50 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
                                                                                                     Write(SharedHtmlLocalizer["Checkout"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n");
            WriteLiteral("    </div>\r\n    <div class=\"d-grid gap-2 mt-3\">\r\n        <a href=\"#\" id=\"a-side-cart-continue\" class=\"btn btn-outline-primary rounded-pill fw-bold w-100\">");
#nullable restore
#line 54 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
                                                                                                    Write(SharedHtmlLocalizer["ContinueShopping"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</a>
    </div>
</div>
<script>
    $(document).ready(function () {
        $("".btn-plus-side-cart"").click(function () {
            var quantity = $(this).closest("".side-cart-spinner"").find(""input"").val();
            var finalQuantity = parseInt(quantity) + 1;

            var maxQuantity = $(this).closest("".side-cart-spinner"").find(""input"").attr(""data-maxquantity"");
            if (finalQuantity > maxQuantity) {
                return;
            }

            $(this).closest("".side-cart-spinner"").find(""input"").val(finalQuantity);
            var cartId = $(this).closest("".side-cart-spinner"").find(""input"").attr(""data-cartid"");
            editcartitem(cartId, finalQuantity);
        });

        $("".btn-minus-side-cart"").click(function () {
            var quantity = $(this).closest("".side-cart-spinner"").find(""input"").val();
            var finalQuantity = parseInt(quantity) - 1;
            if (finalQuantity == 0)
                return;

            $(this).closest("".side-cart-");
            WriteLiteral(@"spinner"").find(""input"").val(finalQuantity);
            var cartId = $(this).closest("".side-cart-spinner"").find(""input"").attr(""data-cartid"");
            editcartitem(cartId, finalQuantity);
        });

        $(""#a-side-cart-continue"").click(function () {
            window.location.href = """);
#nullable restore
#line 85 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
                               Write(Url.RouteUrl("home"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\";\r\n        });\r\n    });\r\n\r\n    function editcartitem(cartId, quantity) {\r\n        if (quantity == \"\" || parseInt(quantity) == 0) {\r\n            showToastErrorMessage(\'");
#nullable restore
#line 91 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
                              Write(SharedHtmlLocalizer["Quantity should be greater than zero"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\');\r\n            return;\r\n        }\r\n\r\n        $.post(\"");
#nullable restore
#line 95 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
           Write(Url.RouteUrl("editcartitem"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""", { id: cartId, quantity: quantity }, function (result) {
            if (result.cartContents != """") {
                updateCartCount();
                $(""#offcanvasCart"").empty();
                $(""#offcanvasCart"").append(result.cartContents);
            }
        });

        return false;
    }

    function deletecartitem(id) {
        var url = """);
#nullable restore
#line 107 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
              Write(Url.RouteUrl("deletecartitem",new { id = "_id" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""";
        url = url.replace(""_id"", id);
        $.get(url, function (result) {
            if (result.success) {
                updateCartCount();

                $(""#offcanvasCart"").empty();
                $(""#offcanvasCart"").append(result.cartContents);

                var cartItems = $("".cart-item"");
                if (cartItems.length == 0) {
                    $('#offcanvasCart').offcanvas('hide');
                }
            }
            else {
                if (result.message != """")
                    showToastErrorMessage(result.message);
            }
        });

        return false;
    }

    function deletecartitems() {
        $.get(""");
#nullable restore
#line 131 "D:\Development\AdhariNew\Web\Views\Shared\_SideCart.cshtml"
          Write(Url.RouteUrl("deletecartitems"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""", function (result) {
            if (result.success) {
                updateCartCount();
                $('#offcanvasCart').offcanvas('hide');
            }
            else {
                if (result.message != """")
                    showToastErrorMessage(result.message);
            }
        });

        return false;
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Utility.Models.Frontend.Shop.CartModel> Html { get; private set; }
    }
}
#pragma warning restore 1591