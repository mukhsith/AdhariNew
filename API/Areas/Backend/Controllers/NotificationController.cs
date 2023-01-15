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
using Utility.Models.Admin.Notifications;
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



        [HttpGet, Route("api/Notification/GetAdminNotificationDefault")]
        public async Task<IActionResult> GetAdminNotificationDefault()
        {
            ResponseMapper<AdminNotificationTemplate> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                var item = await _get.GetAdminNotificationDefault();
                response.GetDefault(item);

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

        [HttpPost, Route("api/Notification/UpdateAdminNotificationnew")]
        public async Task<IActionResult> UpdateAdminNotification([FromForm] AdminNotification item)
        {

            AdminNotificationTemplate template = new AdminNotificationTemplate();
            template.Id = item.Id;
            template.LowStockEnabled = item.LowStockEnabled;
            template.LowStockThresholdQuantity = item.LowStockThresholdQuantity;
            template.LowStockToEmailAddress = item.LowStockToEmailAddress;
            template.LowStockCCEmailAddress = item.LowStockCCEmailAddress;

            template.NewOrderNotificationEnabled = item.NewOrderNotificationEnabled;
            template.NewOrderNotificationToEmailAddress = item.NewOrderNotificationToEmailAddress;
            template.NewOrderNotificationCCEmailAddress = item.NewOrderNotificationCCEmailAddress;

            ResponseMapper<AdminNotificationTemplate> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                template.CreatedBy = UserId;

                await _get.UpdateAdminNotification(template);
                //response.Update(item);

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
