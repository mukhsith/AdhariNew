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
    
    public class NotificationController : BaseController
    { 
        public NotificationController(IHttpContextAccessor httpContextAccessor, IOptions<AppSettingsModel> options, ILoggerFactory logger) :
            base(httpContextAccessor, options, logger.CreateLogger(typeof(NotificationController).Name))
        { }

        public IActionResult NotificationTemplateList()
        {
            return View();
        }
        public IActionResult NotificationTemplateEdit(int id)
        {
            return View(new BaseEntityId { Id = id });
        }
       
        public IActionResult SendNotification()
        {
            return View();
        }


        public IActionResult SMSNotificationList()
        {
            return View();
        }

        public IActionResult SMSNotificationAddEdit(int id)
        {
            return View(new BaseEntityId { Id = id });
        }

        public IActionResult PushNotificationList()
        {
            return View();
        }

        public IActionResult PushNotificationAddEdit(int id)
        {
            return View(new BaseEntityId { Id = id });
        }



        public IActionResult AdminNotification()
        {
            return View();
        }
        //public IActionResult NotificationToCustomers()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
