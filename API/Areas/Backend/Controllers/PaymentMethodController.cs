using Data.Content;
using Data.Locations;
using Data.ProductManagement;
using Microsoft.AspNetCore.Authorization;
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
using Utility.Helpers;
using Utility.ResponseMapper;

namespace API.Areas.Backend.Controllers
{
   // [Authorize(Roles="Admin")]
    [Authorize]
    public class PaymentMethodController : BaseController 
    {
        private readonly IPaymentMethodService _get;
        private readonly ILogger _logger;
        public PaymentMethodController(
            IOptions<AppSettingsModel> options,
            ISystemUserService systemUserService,
            IPaymentMethodService get,
            ILoggerFactory logger) : 
            base(options, systemUserService, PermissionTypes.PaymentMethods)
        {
            _get = get;
            _logger = logger.CreateLogger(typeof(PaymentMethodController).Name);
             
        }

        /// <summary>
        /// HTTP Status 405 - Method not allowed (Check Get or POST)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        [HttpGet, Route("api/PaymentMethod/ById")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseMapper<Data.Content.PaymentMethod> response = new();
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

        [HttpPost, Route("api/PaymentMethod/AddEdit")]
        public async Task<IActionResult> AddEdit([FromForm] Data.Content.PaymentMethod item)
        {
            ResponseMapper<Data.Content.PaymentMethod> response = new();
            try
            { 
                if (!await Allowed()) { return Ok(accessResponse); }

                //if (await _get.Exists(item.Id, item.NameEn, item.NameAr))
                //{
                //    accessResponse.Message = "Area Name Already Exists";
                //    accessResponse.Success = false;
                //    accessResponse.StatusCode = 300;
                //    return Ok(accessResponse);
                //}
                
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
        [HttpPost, Route("api/PaymentMethod/ToggleActiveNormalRegistered")]
        public async Task<IActionResult> ToggleActiveNormalRegistered(int Id)
        {
            ResponseMapper<Data.Content.PaymentMethod> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var item = await _get.ToggleNormalRegistered(Id);


                response.ToggleActive(item);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }
        [HttpPost, Route("api/PaymentMethod/ToggleActiveSubscriptionRegistered")]
        public async Task<IActionResult> ToggleActiveSubscriptionRegistered(int Id)
        {
            ResponseMapper<Data.Content.PaymentMethod> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var item = await _get.ToggleSubscriptionRegistered(Id);
                response.ToggleActive(item);
            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }
        

        [HttpGet, Route("api/PaymentMethod/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            ResponseMapper<List<Data.Content.PaymentMethod>> response = new();
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

        [HttpPost, Route("api/PaymentMethod/GetAllForDataTable")]
        public async Task<IActionResult> GetAllForDataTable()
        {
            ResponseMapper<dynamic> response = new();
            try
            {

                if (!await Allowed()) { return Ok(accessResponse); } 
                var baseImageUrl = this.AppSettings.APIBaseUrl + this.AppSettings.ImagePaymentMethod;

                var items = await _get.GetAllForDataTable(base.GetDataTableParameters, baseImageUrl);


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
