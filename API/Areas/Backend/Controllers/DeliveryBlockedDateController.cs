using Data.DeliveryManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Backend.DeliveryManagement.Interface;
using Services.Backend.SystemUserManagement;
using System;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;
using Utility.ResponseMapper;
namespace API.Areas.Backend.Controllers
{

    public class DeliveryBlockedDateController : BaseController 
    {
        private readonly IDeliveryBlockedDateService _get;
        private readonly ILogger _logger;
        public DeliveryBlockedDateController(
            IOptions<AppSettingsModel> options,
            ISystemUserService systemUserService,
            IDeliveryBlockedDateService get,
            ILoggerFactory logger) : 
            base(options, systemUserService, PermissionTypes.DeliveryBlockedDates)
        {
            _get = get;
            _logger = logger.CreateLogger(typeof(DeliveryBlockedDateController).Name);
             
        }

        /// <summary>
        /// HTTP Status 405 - Method not allowed (Check Get or POST)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        [HttpGet, Route("api/DeliveryBlockedDate/ById")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseMapper<DeliveryBlockedDate> response = new();
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

        [HttpPost, Route("api/DeliveryBlockedDate/AddEdit")]
        public async Task<IActionResult> AddEdit([FromForm] DeliveryBlockedDate item)
        {
            ResponseMapper<dynamic> response = new();
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

        [HttpPost, Route("api/DeliveryBlockedDate/ToggleActive")]
        public async Task<IActionResult> ToggleActiveAsync(int Id)
        {
            ResponseMapper<DeliveryBlockedDate> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var item = await _get.ToggleActive(Id,this.UserId);

                response.ToggleActive(item);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }

       

        [HttpGet, Route("api/DeliveryBlockedDate/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var items = await _get.GetAll();
                response.GetAll(items);

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }
            return Ok(response);
        }
        
        
        [HttpPost, Route("api/DeliveryBlockedDate/GetAllForDataTable")]
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
