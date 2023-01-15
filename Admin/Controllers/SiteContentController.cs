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
using Utility.Enum;

namespace Admin.Controllers
{
    
    public class SiteContentController : BaseController
    { 
        public SiteContentController(IHttpContextAccessor httpContextAccessor, IOptions<AppSettingsModel> options, ILoggerFactory logger) :
            base(httpContextAccessor, options, logger.CreateLogger(typeof(SiteContentController).Name))
        { }
        public IActionResult BannerList()
        { 
            return View();
        }

        public IActionResult BannerAddEdit(int id)
        {   
            return View(new BaseEntityId { Id = id });
        }
        public IActionResult AboutUsEdit()
        {
            return View(new BaseEntityId { Id = (int)AppContentType.AboutUs });
        }

        public IActionResult TermsConditionEdit()
        {
            return View(new BaseEntityId { Id = (int)AppContentType.TermsCondition });
        }

        public IActionResult PrivacyPolicyEdit()
        {
            return View(new BaseEntityId { Id = (int)AppContentType.PrivacyPolicy });
        }
        public IActionResult RefundPolicyEdit()
        {
            return View(new BaseEntityId { Id = (int)AppContentType.RefundPolicy });
        }
        public IActionResult ContactDetailEdit()
        {
            return View(new BaseEntityId { Id = (int)AppContentType.RefundPolicy });
        }
        public IActionResult SocialMediaLinksEdit()
        {
            return View(new BaseEntityId { Id = (int)AppContentType.RefundPolicy });
        }
        public IActionResult CustomerFeedbackList()
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
