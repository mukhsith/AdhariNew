using Data.Content;
using Data.Locations;
using Data.ProductManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Backend.Content.Interface;
using Services.Backend.Locations.Interface;
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
    public class GovernorateController : BaseController 
    {
        private readonly IGovernorateService _get;
        private readonly ILogger _logger;
        public GovernorateController(
            IOptions<AppSettingsModel> options,
            ISystemUserService systemUserService,
            IGovernorateService get,
            ILoggerFactory logger) : 
            base(options, systemUserService, PermissionTypes.Governorates)
        {
            _get = get;
            _logger = logger.CreateLogger(typeof(GovernorateController).Name);
             
        }

        /// <summary>
        /// HTTP Status 405 - Method not allowed (Check Get or POST)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        [HttpGet, Route("api/Governorate/ById")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseMapper<Governorate> response = new();
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

        [HttpPost, Route("api/Governorate/AddEdit")]
        public async Task<IActionResult> AddEdit([FromForm] Governorate item)
        {
            ResponseMapper<Governorate> response = new();
            try
            { 
                if (!await Allowed()) { return Ok(accessResponse); }

                if (await _get.Exists(item.Id, item.NameEn, item.NameAr))
                {
                    accessResponse.Message = "Governorate Name Already Exists";
                    accessResponse.Success = false;
                    accessResponse.StatusCode = 300;
                    return Ok(accessResponse);
                }
                
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

        [HttpPost, Route("api/Governorate/ToggleActive")]
        public async Task<IActionResult> ToggleActiveAsync(int Id)
        {
            ResponseMapper<Governorate> response = new();
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

        [HttpPost, Route("api/Governorate/UpdateDisplayOrder")]
        public async Task<IActionResult> UpdateDisplayOrder(int Id, int num = 0)
        {
            ResponseMapper<Governorate> response = new();
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

        [HttpGet, Route("api/Governorate/ForDropDownList")]
        public async Task<IActionResult> ForDropDownList()
        {
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var items = await _get.GetAll();
                response.GetAll(items.Select(x => new { x.Id, Name = IsEnglish ? x.NameEn : x.NameAr }).ToList());

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }
            return Ok(response);
        }




        [HttpGet, Route("api/Governorate/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            ResponseMapper<List<Governorate>> response = new();
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

        [HttpPost, Route("api/Governorate/GetAllForDataTable")]
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
         

       

    }
}
