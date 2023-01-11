using Data.Content;
using Data.CouponPromotion;
using Data.ProductManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Backend.Content.Interface;
using Services.Backend.CouponPromotion.Interface;
using Services.Backend.ProductManagement.Interface;
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
     
    public class PromotionController : BaseController 
    {
        private readonly IPromotionService _get;
        private readonly ILogger _logger;
        public PromotionController(
            IOptions<AppSettingsModel> options,
            ISystemUserService systemUserService,
            IPromotionService get,
            ILoggerFactory logger) : 
            base(options, systemUserService, PermissionTypes.Promotion)
        {
            _get = get;
            _logger = logger.CreateLogger(typeof(PromotionController).Name);
             
        }

        /// <summary>
        /// HTTP Status 405 - Method not allowed (Check Get or POST)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        [HttpGet, Route("api/Promotion/GetDefault")]
        public async Task<IActionResult> GetDefault()
        {
            ResponseMapper<Promotion> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                  var item = await _get.GetDefault();
                response.GetDefault(item);
                  
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

        [HttpPost, Route("api/Promotion/Update")]
        public async Task<IActionResult> Update([FromForm] Promotion item)
        {
            ResponseMapper<Promotion> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                item.CreatedBy = UserId;

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







    }
}
