using Data.Content;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Backend.Content.Interface;
using Services.Backend.SystemUserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using Utility.API;
using Utility.Enum;
using Utility.ResponseMapper;

namespace API.Areas.Backend.Controllers
{
    public class SocialMediaLinkController : BaseController 
    {
        private readonly ISocialMediaLinkService _get;
        private readonly ILogger _logger;
        public SocialMediaLinkController(
            IOptions<AppSettingsModel> options,
            ISystemUserService systemUserService,
            ISocialMediaLinkService get,
            ILoggerFactory logger) : 
            base(options, systemUserService, PermissionTypes.SocialMediaLinks)
        {
            _get = get;
            _logger = logger.CreateLogger(typeof(ContactDetailController).Name);
             
        }

        /// <summary>
        /// HTTP Status 405 - Method not allowed (Check Get or POST)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        [HttpGet, Route("api/SocialMediaLink/GetDefault")]
        public async Task<IActionResult> GetDefault()
        {
            ResponseMapper<SocialMediaLink> response = new();
            try {
                if (!await Allowed()) { return Ok(accessResponse); }
                var item = await _get.GetDefault();
                if(item is null)
                {
                    item = await _get.Create(this.UserId);
                }
               response.GetById(item);

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

        

        [HttpPost, Route("api/SocialMediaLink/Edit")]
        public async Task<IActionResult> Edit([FromForm] SocialMediaLink item)
        {
            ResponseMapper<SocialMediaLink> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                await _get.Edit(item);
                response.GetById(item);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

       

    }
}
