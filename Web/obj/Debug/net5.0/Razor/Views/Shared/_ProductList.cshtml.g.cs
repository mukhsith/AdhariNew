#pragma checksum "D:\Development\Adhari\Web\Views\Shared\_ProductList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8c5d6367444251b70e6d45b6294c930868729f3c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__ProductList), @"mvc.1.0.view", @"/Views/Shared/_ProductList.cshtml")]
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
#line 2 "D:\Development\Adhari\Web\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

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
#line 10 "D:\Development\Adhari\Web\Views\_ViewImports.cshtml"
using Utility.Enum;

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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8c5d6367444251b70e6d45b6294c930868729f3c", @"/Views/Shared/_ProductList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"46a65b4b2ada0dd24edf72a9f6a2735722af620c", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__ProductList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IList<Utility.Models.Frontend.ProductManagement.ProductModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Development\Adhari\Web\Views\Shared\_ProductList.cshtml"
 foreach (var item in Model)
{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Development\Adhari\Web\Views\Shared\_ProductList.cshtml"
Write(await Html.PartialAsync("_ProductBox", item));

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Development\Adhari\Web\Views\Shared\_ProductList.cshtml"
                                                 
}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IList<Utility.Models.Frontend.ProductManagement.ProductModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
