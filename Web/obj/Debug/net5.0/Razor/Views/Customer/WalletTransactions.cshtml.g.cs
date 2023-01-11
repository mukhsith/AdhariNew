#pragma checksum "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9072d945342644c1d814950909d829e7816c5f65"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Customer_WalletTransactions), @"mvc.1.0.view", @"/Views/Customer/WalletTransactions.cshtml")]
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
#nullable restore
#line 3 "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml"
using Utility.Helpers;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9072d945342644c1d814950909d829e7816c5f65", @"/Views/Customer/WalletTransactions.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fd8610713b70f7256c3429c827c060fa488c518d", @"/Views/_ViewImports.cshtml")]
    public class Views_Customer_WalletTransactions : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Utility.Models.Frontend.CustomerManagement.WalletModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 4 "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml"
  
    var breadCrumbItems = new List<BreadcrumbModel>();
    var walletTypeId = ViewBag.WalletType;
    var walletTransactionsType = (int)(WalletType.Wallet);
    var transactionTypeClass = " text-light py-0 px-2 rounded-pill";
    var transactionAmountClass = " fw-bold";
    if (walletTypeId == walletTransactionsType)
    {
        breadCrumbItems.Add(new BreadcrumbModel() { Url = "#", Title = SharedHtmlLocalizer["MyWallet"].Value });
    }
    else
    {
        breadCrumbItems.Add(new BreadcrumbModel() { Url = "#", Title = SharedHtmlLocalizer["MyCashback"].Value });
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml"
Write(await Html.PartialAsync("_PageHeading", breadCrumbItems));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<section class=""main-content py-5 mb-5"">
    <div class=""container"">
        <div class=""row justify-content-center"">
            <div class=""col-6 col-md-4 text-center"" id=""dv-wallet-header"">
                <span class=""fa-stack fa-2x mb-2"">
                    <i class=""fa-solid fa-circle fa-stack-2x text-primary""></i>
                    <i class=""fa-solid fa-money-bill-1-wave fa-stack-1x fa-inverse""></i>
                </span>
");
#nullable restore
#line 28 "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml"
                 if (walletTypeId == walletTransactionsType)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <h5 class=\"text-center text-primary mb-2\">");
#nullable restore
#line 30 "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml"
                                                         Write(SharedHtmlLocalizer["WalletAmount"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                    <h2 class=\"text-center text-primary fw-bold mb-0\">");
#nullable restore
#line 31 "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml"
                                                                 Write(Model.FormattedWalletBalance);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n                    <a");
            BeginWriteAttribute("href", " href=\"", 1566, "\"", 1604, 1);
#nullable restore
#line 32 "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml"
WriteAttributeValue("", 1573, Url.RouteUrl("walletpackages"), 1573, 31, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-primary rounded-pill text-white fw-bold mt-3\">");
#nullable restore
#line 32 "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml"
                                                                                                                      Write(SharedHtmlLocalizer["TopUpWallet"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n");
#nullable restore
#line 33 "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <h5 class=\"text-center text-primary mb-2\">");
#nullable restore
#line 36 "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml"
                                                         Write(SharedHtmlLocalizer["CashbackAmount"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                    <h2 class=\"text-center text-primary fw-bold mb-0\">");
#nullable restore
#line 37 "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml"
                                                                 Write(Model.FormattedCashbackBalance);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n");
#nullable restore
#line 38 "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n            <div class=\"col-12 col-xl-10 mx-auto\" id=\"dv-wallet\">\r\n");
#nullable restore
#line 41 "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml"
                 foreach (var transactionDates in Model.WalletTransactionByDates)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <h6 class=\"mb-1 mt-5 text-primary\">");
#nullable restore
#line 43 "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml"
                                                  Write(transactionDates.FormattedDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\r\n");
#nullable restore
#line 44 "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml"
                     foreach (var transactions in transactionDates.WalletTransactions)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        <div class=""bg-grey rounded-4 p-4 pb-1 mx-0 mb-3 position-relative"">
                            <div class=""row"">
                                <div class=""col-12"">
                                    <div class=""row row-cols-2 row-cols-md-3 row-cols-lg-4"">
");
#nullable restore
#line 50 "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml"
                                         foreach (var item in transactions.PaymentSummary)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <div class=\"col mb-3\">\r\n                                                <p class=\"mb-0 fw-bold\">");
#nullable restore
#line 53 "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml"
                                                                   Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                                <span");
            BeginWriteAttribute("class", " class=\"", 3029, "\"", 3270, 2);
            WriteAttributeValue("", 3037, "mb-0", 3037, 4, true);
#nullable restore
#line 54 "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml"
WriteAttributeValue(" ", 3041, item.Title == MessagesAr.TransactionType || item.Title == Messages.TransactionType ? transactionTypeClass : item.Title == MessagesAr.TransactionAmount || item.Title == Messages.TransactionAmount ? transactionAmountClass : "" , 3042, 228, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("style", "\r\n                                                      style=\"", 3271, "\"", 3596, 4);
            WriteAttributeValue("", 3334, "background-color:", 3334, 17, true);
#nullable restore
#line 55 "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml"
WriteAttributeValue("", 3351, item.Title == MessagesAr.TransactionType || item.Title == Messages.TransactionType ? transactions.ColorCode : "none", 3351, 119, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3470, ";color:", 3470, 7, true);
#nullable restore
#line 55 "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml"
WriteAttributeValue("", 3477, item.Title == MessagesAr.TransactionAmount || item.Title == Messages.TransactionAmount ? transactions.ColorCode : "", 3477, 119, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 55 "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml"
                                                                                                                                                                                                                                                                                                                                Write(item.Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                                </div>\r\n");
#nullable restore
#line 57 "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml"
                                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    </div>\r\n                                </div>\r\n                            </div>\r\n                        </div>\r\n");
#nullable restore
#line 62 "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml"
                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 62 "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml"
                     
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </div>
        </div>
        <div class=""row mt-5"" id=""dv-empty-wallet"">
            <div class=""col-12 text-center"">
                <i class=""display-1 fas fa-wallet text-center text-primary""></i>
            </div>
            <div class=""col-12 text-center"">
                <h5 class=""mt-3 text-primary"">");
#nullable restore
#line 71 "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml"
                                         Write(SharedHtmlLocalizer["NoTransactions"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n            </div>\r\n        </div>\r\n        <div id=\"dv-loadmore\" style=\"text-align: center;display:none;\">\r\n            <a id=\"a-more\" class=\"btn btn-primary rounded-pill text-white fw-bold text-uppercase mb-md-4 mb-0\">");
#nullable restore
#line 75 "D:\Development\AdhariNew\Web\Views\Customer\WalletTransactions.cshtml"
                                                                                                          Write(SharedHtmlLocalizer["View More"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</a>
        </div>
    </div>
</section>
<script>
    $(document).ready(function () {
        if ($('#dv-wallet').children().length > 0) {
            $(""#dv-empty-wallet"").hide();
        }
        else {
            $(""#dv-empty-wallet"").show();
        }
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Utility.Models.Frontend.CustomerManagement.WalletModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
