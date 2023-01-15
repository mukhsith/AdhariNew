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
    public class ContactDetailController : BaseController 
    {
        private readonly IContactDetailService _get;
        private readonly ILogger _logger;
        public ContactDetailController(
            IOptions<AppSettingsModel> options,
            ISystemUserService systemUserService,
            IContactDetailService get,
            ILoggerFactory logger) : 
            base(options, systemUserService, PermissionTypes.ContactDetails)
        {
            _get = get;
            _logger = logger.CreateLogger(typeof(ContactDetailController).Name);
             
        }

        /// <summary>
        /// HTTP Status 405 - Method not allowed (Check Get or POST)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        [HttpGet, Route("api/ContactDetail/GetDefault")]
        public async Task<IActionResult> GetDefault()
        {
            ResponseMapper<ContactDetail> response = new();
            try {
                if (!await Allowed()) { return Ok(accessResponse); }
                var item = await _get.GetDefault();
               response.GetById(item);

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

        

        [HttpPost, Route("api/ContactDetail/Edit")]
        public async Task<IActionResult> Edit([FromForm] ContactDetail item)
        {
            ResponseMapper<ContactDetail> response = new();
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
