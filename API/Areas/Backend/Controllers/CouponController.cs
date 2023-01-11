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
   public class CouponController : BaseController 
    {
        private readonly ICouponService _get;
        private readonly ILogger _logger;
        public CouponController(
            IOptions<AppSettingsModel> options,
            ISystemUserService systemUserService,
            ICouponService get,
            ILoggerFactory logger) : 
            base(options, systemUserService, PermissionTypes.Coupon)
        {
            _get = get;
            _logger = logger.CreateLogger(typeof(CouponController).Name);
             
        }

        /// <summary>
        /// HTTP Status 405 - Method not allowed (Check Get or POST)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        [HttpGet, Route("api/Coupon/ById")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseMapper<Coupon> response = new();
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

        [HttpPost, Route("api/Coupon/AddEdit")]
        public async Task<IActionResult> AddEdit([FromForm] Coupon item)
        {
            ResponseMapper<Coupon> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                if (item.EndDate < DateTime.Now )
                {
                    accessResponse.Message = "Coupon end date cannot be less than today";
                    accessResponse.Success = false;
                    accessResponse.StatusCode = 200;
                    return Ok(accessResponse);
                }
                if (item.EndDate < item.StartDate)
                { 
                   accessResponse.Message = "Coupon end date is less than start Date";
                   accessResponse.Success = false;
                   accessResponse.StatusCode = 200;
                   return Ok(accessResponse);
                }
                if (await _get.Exists(item.Id, item.CouponCode))    
                {
                    accessResponse.Message = "Coupon code already exists";
                    accessResponse.Success = false;
                    accessResponse.StatusCode = 200;
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

        [HttpPost, Route("api/Coupon/ToggleActive")]
        public async Task<IActionResult> ToggleActiveAsync(int Id)
        {
            ResponseMapper<Coupon> response = new();
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

        [HttpPost, Route("api/Coupon/UpdateDisplayOrder")]
        public async Task<IActionResult> UpdateDisplayOrder(int Id, int num = 0)
        {
            ResponseMapper<Coupon> response = new();
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

        [HttpGet, Route("api/Coupon/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            ResponseMapper<List<Coupon>> response = new();
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

        [HttpPost, Route("api/Coupon/GetAllForDataTable")]
        public async Task<IActionResult> GetAllForDataTable()
        { 
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                var couponCode = Request.Form["couponCode"].FirstOrDefault();
                var active = Request.Form["active"].FirstOrDefault();
                var createdOn = Request.Form["createdOn"].FirstOrDefault();

                int? _active = Utility.Helpers.Common.ConvertTextToIntOptional(active);
                var _createdOn = Utility.Helpers.Common.ConvertTextToDate(createdOn);
                var items = await _get.GetAllForDataTable(base.GetDataTableParameters,couponCode, _active, _createdOn);
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
