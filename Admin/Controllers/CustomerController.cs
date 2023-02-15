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
using Utility.Models.Admin.Sales;

namespace Admin.Controllers
{
    
    public class CustomerController : BaseController
    { 
        public CustomerController(IHttpContextAccessor httpContextAccessor, IOptions<AppSettingsModel> options, ILoggerFactory logger) :
            base(httpContextAccessor, options,  logger.CreateLogger(typeof(CustomerController).Name))
        { }

        [Route("customer/cashback-history")]
        public IActionResult  CashbackHistory(int customerId)
        {
            return View(new BaseEntityId { Id = customerId });
        }
        [HttpGet, Route("customer/wallet-history")]
        public IActionResult WalletHistory(int customerId)
        {
            return View(new BaseEntityId { Id = customerId });
        }


        [Route("customer/CustomerDetails")]
        public IActionResult CustomerDetails(int customerId)
        {
            return View(new BaseEntityId { Id = customerId });
        }


        public IActionResult CustomerList()
        {
            return View();
        }



        [Route("customer/CustomerAddress")]
        public IActionResult CustomerAddress(int customerId)
        {
            return View(new BaseEntityId { Id = customerId });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
