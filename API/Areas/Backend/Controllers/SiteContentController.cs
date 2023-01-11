using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks; 
using Utility.ResponseMapper;
using System.Collections.Generic;

using Utility.Enum;
using Microsoft.Extensions.Logging;
using Data.Content;

using Utility;
using System.Linq;  
using Services.Backend.SystemUserManagement;
using Services.Backend.Content.Interface;
using Utility.API;

namespace API.Areas.Backend.Controllers
{
    public class SiteContentController : BaseController
    {
        private readonly ISiteContentService _get; 
        private readonly ILogger _logger;
        public SiteContentController(
            IOptions<AppSettingsModel> options,
            ISystemUserService systemUserService,
            ISiteContentService get,
            ILoggerFactory logger) : 
            base(options, systemUserService, PermissionTypes.SiteContent)
        {
            _get = get;
            _logger = logger.CreateLogger(typeof(SiteContentController).Name);
             
        }

      

        [HttpGet, Route("api/SiteContent/{type}")]
        public async Task<IActionResult> FirstOrDefault(AppContentType type)
        {
            ResponseMapper<SiteContent> response = new();
            try
            {
                if (!await Allowed((int)type)){return Ok(accessResponse);}

                //item should be active=true
                var item = await _get.GetByType(type);
                item = BuildUrl(item);
                response.GetById(item);

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }
            return Ok(response);
        }

        

        [HttpPost, Route("api/SiteContent/Edit")]
        public async Task<IActionResult> EditAsync([FromForm] SiteContent item)
        {
            ResponseMapper<SiteContent> response = new();
            try
            {
                if (!await Allowed((int)item.AppContentType)) { return Ok(accessResponse); }
                SaveImage(ref item);
                item.ModifiedBy = base.UserId;
               await _get.Update(item);
                response.Update(item);
                

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

        #region Utility
        private void SaveImage(ref SiteContent model)
        {
            if (model.Image != null && model.Image.Length > 0)
            {
                string fileName = MediaHelper.SaveImageToFile(model.Image, "/" + AppSettings.ImageSiteContent, AppSettings.ImageSiteContentResized);
                if (!string.IsNullOrEmpty(fileName))
                    model.ImageName = fileName;
            }

        }

        private void BuildUrls(ref List<SiteContent> items)
        {
            for (var index = 0; index < items.Count; index++)
            {
                items[index] = BuildUrl(items[index]);
            }

        }
        private SiteContent BuildUrl(SiteContent item)
        {
            if (item is null) return item;

            if (!string.IsNullOrEmpty(item.ImageName))
            {
                item.ImageUrl = GetImageUrl(AppSettings.ImageSiteContent ,item.ImageName);

            }

            return item;
        }
        #endregion Utitlity
    }
}

