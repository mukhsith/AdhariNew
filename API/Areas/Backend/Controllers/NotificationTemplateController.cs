using Data.Content;
using Data.NotifyTemplate;
using Data.ProductManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Backend.Content.Interface;
using Services.Backend.ProductManagement.Interface;
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
    public class NotificationTemplateController : BaseController 
    {
        private readonly INotificationTemplateService _get;
        private readonly ILogger _logger;
        public NotificationTemplateController(
            IOptions<AppSettingsModel> options,
            ISystemUserService systemUserService,
            INotificationTemplateService get,
            ILoggerFactory logger) : 
            base(options, systemUserService, PermissionTypes.NotificationTemplates)
        {
            _get = get;
            _logger = logger.CreateLogger(typeof(NotificationTemplateController).Name);
             
        }

        /// <summary>
        /// HTTP Status 405 - Method not allowed (Check Get or POST)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        [HttpGet, Route("api/NotificationTemplate/ById")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseMapper<NotificationTemplate> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                if (id > 0)
                {
                    var item = await _get.GetById(id);
                    response.GetById(item);
                }


            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

        [HttpPost, Route("api/NotificationTemplate/AddEdit")]
        public async Task<IActionResult> AddEdit([FromForm] NotificationTemplate item)
        {
            ResponseMapper<NotificationTemplate> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                if (item.Id > 0)
                {
                    item.ModifiedBy = UserId;
                    await _get.Update(item);
                    response.Update(item);
                }
                else
                {
                    item.CreatedBy = UserId;
                    await _get.Create(item);
                    response.Create(item);
                }


            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

        [HttpPost, Route("api/NotificationTemplate/ToggleActive")]
        public async Task<IActionResult> ToggleActiveAsync(int Id)
        {
            ResponseMapper<NotificationTemplate> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var item = await _get.ToggleActive(Id);
                response.ToggleActive(item);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

        [HttpPost, Route("api/NotificationTemplate/UpdateDisplayOrder")]
        public async Task<IActionResult> UpdateDisplayOrder(int Id, int num = 0)
        {
            ResponseMapper<NotificationTemplate> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var item = await _get.UpdateDisplayOrder(Id, num);
                response.DisplayOrder(item);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

        [HttpGet, Route("api/NotificationTemplate/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            ResponseMapper<List<NotificationTemplate>> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var items = await _get.GetAll();
                response.GetAll(items.ToList());

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }
            return Ok(response);
        }

        [HttpPost, Route("api/NotificationTemplate/GetAllForDataTable")]
        public async Task<IActionResult> GetAllForDataTable()
        { 
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var items = await _get.GetAllForDataTable(base.GetDataTableParameters);
               // response.GetAll(items);
                return Ok(items);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }
            return Ok(response);
        }
         

       [HttpPost,Route("api/NotificationTemplate/CreateSMSPush")]
       public async Task<IActionResult> CreateSMSPush([FromForm] AdminSMSPushModel model)
        {
            ResponseMapper<bool> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                // var languageId = IsEnglish ? 0 : 64;

                var itemnew = await _get.CreateSMSNotification(model.Message, model.MobileNumber, model.languageId);

                var item = await  _get.CreateSMSPush(model.Message, model.MobileNumber, model.languageId);
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
