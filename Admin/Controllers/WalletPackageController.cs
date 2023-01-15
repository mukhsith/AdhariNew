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
using Utility.Helpers;

namespace Admin.Controllers
{
    
    public class  WalletPackageController : BaseController
    {
       
        public WalletPackageController(IHttpContextAccessor httpContextAccessor, IOptions<AppSettingsModel> options, ILoggerFactory logger) :
            base(httpContextAccessor, options,
                logger.CreateLogger(typeof(WalletPackageController).Name))
        {
            //_appSettings = options.Value;
        }
        public IActionResult WalletPackageList()
        {
            return View();
        }
        public IActionResult AddEdit(int id)
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
