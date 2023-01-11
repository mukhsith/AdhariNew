using Data.Content;
using Data.NotifyTemplate;
using Data.ProductManagement;
using Data.PushNotification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Backend.Content.Interface;
using Services.Backend.ProductManagement.Interface;
using Services.Backend.PushNotification;
using Services.Backend.SystemUserManagement;
using Services.Backend.Template.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using Utility.API;
using Utility.Enum;
using Utility.Models.Admin.CustomerManagement;
using Utility.ResponseMapper;

namespace API.Areas.Backend.Controllers
{
    public class NotificationController : BaseController 
    {
        private readonly INotificationService _get;
        private readonly ILogger _logger;
        public NotificationController(
            IOptions<AppSettingsModel> options,
            ISystemUserService systemUserService,
            INotificationService get,
            ILoggerFactory logger) : 
            base(options, systemUserService, PermissionTypes.NotificationTemplates)
        {
            _get = get;
            _logger = logger.CreateLogger(typeof(NotificationController).Name);
             
        }

        [HttpPost, Route("api/Notification/CreateNotification")]
        public async Task<IActionResult> CreateSMSPush([FromForm] Notification model)
        {
            ResponseMapper<Notification> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                model.CreatedBy = this.UserId;

                var item = await _get.CreateNotification(model);
                response.GetById(item);
                return Ok(response);
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
