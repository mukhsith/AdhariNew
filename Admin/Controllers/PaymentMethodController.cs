using Admin.Models;
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
    
    public class PaymentMethodController : BaseController
    { 
        public PaymentMethodController(IHttpContextAccessor httpContextAccessor, IOptions<AppSettingsModel> options, ILoggerFactory logger) :
            base(httpContextAccessor, options, logger.CreateLogger(typeof(PaymentMethodController).Name))
        { }
         
        
        public IActionResult Payments()
        {
            return View();
        }
        
         
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
