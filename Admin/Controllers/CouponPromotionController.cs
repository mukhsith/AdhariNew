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
    
    public class CouponPromotionController : BaseController
    { 
        public CouponPromotionController( IHttpContextAccessor httpContextAccessor, IOptions<AppSettingsModel> options, ILoggerFactory logger) :
            base(httpContextAccessor, options,   logger.CreateLogger(typeof(CouponPromotionController).Name))
        { }

        public IActionResult PromotionEdit()
        {
            return View();
        }
        public IActionResult CouponList()
        {
            return View();
        }
        public IActionResult CouponAddEdit( int id)
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
