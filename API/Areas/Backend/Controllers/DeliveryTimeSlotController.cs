using Data.DeliveryManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Backend.DeliveryManagement.Interface;
using Services.Backend.SystemUserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;
using Utility.ResponseMapper;

namespace API.Areas.Backend.Controllers
{

    public class DeliveryTimeSlotController : BaseController 
    {
        private readonly IDeliveryTimeSlotService _get;
        private readonly ILogger _logger;
        public DeliveryTimeSlotController(
            IOptions<AppSettingsModel> options,
            ISystemUserService systemUserService,
            IDeliveryTimeSlotService get,
            ILoggerFactory logger) : 
            base(options, systemUserService, PermissionTypes.ItemSize)
        {
            _get = get;
            _logger = logger.CreateLogger(typeof(DeliveryTimeSlotController).Name);
             
        }

        /// <summary>
        /// HTTP Status 405 - Method not allowed (Check Get or POST)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        [HttpGet, Route("api/DeliveryTimeSlot/ById")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseMapper<DeliveryTimeSlot> response = new();
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

        [HttpPost, Route("api/DeliveryTimeSlot/AddEdit")]
        public async Task<IActionResult> AddEdit([FromForm] DeliveryTimeSlot item)
        {
            ResponseMapper<DeliveryTimeSlot> response = new();
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

        [HttpPost, Route("api/DeliveryTimeSlot/ToggleActive")]
        public async Task<IActionResult> ToggleActiveAsync(int Id)
        {
            ResponseMapper<DeliveryTimeSlot> response = new();
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

       

        [HttpGet, Route("api/DeliveryTimeSlot/GetAll")]
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
        
        [HttpPost, Route("api/DeliveryTimeSlot/UpdateAll")] 
        public async Task<IActionResult> UpdateAll([FromForm] List<DeliveryTimeSlot> deliveryTimeSlots)
        {
            ResponseMapper<bool> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                if (deliveryTimeSlots.Count() > 0)
                {   
                    var updated = await _get.UpdateAll(deliveryTimeSlots,this.UserId);
                    response.Update(updated);
                }

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }
            return Ok(response);
        }
        [HttpPost, Route("api/DeliveryTimeSlot/GetAllForDataTable")]
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
