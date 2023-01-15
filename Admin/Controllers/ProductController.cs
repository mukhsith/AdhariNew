using Admin.Models;
using Data.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;

namespace Admin.Controllers
{

        
    public class ProductController : BaseController
    { 
        public ProductController(IHttpContextAccessor httpContextAccessor, IOptions<AppSettingsModel> options, ILoggerFactory logger) :
            base(httpContextAccessor, options, logger.CreateLogger(typeof(ProductController).Name))
        { }
        public IActionResult CategoryList()
        { 
            return View();
        }

        public IActionResult CategoryAddEdit(int id)
        {   
            return View(new BaseEntityId { Id = id });
        }


        public IActionResult ItemSizeList()
        {
            return View();
        }

        public IActionResult ItemSizeAddEdit(int id)
        {
            return View(new BaseEntityId { Id = id });
        }

        public IActionResult ProductList()
        {
            return View();
        }

        public IActionResult ProductAddEdit(int id)
        {
            return View(new BaseEntityId { Id = id });
        }

        public IActionResult BundledProductList()
        {
            return View();
        }

        public IActionResult BundledProductAddEdit(int id)
        {
            return View(new BaseEntityId { Id = id });
        }
        
        public IActionResult SubscriptionList()
        {
            return View();
        }

        public IActionResult SubscriptionAddEdit(int id)
        {
            return View(new BaseEntityId { Id = id });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
