using Data.Content;
using Data.CouponPromotion;
using Data.Locations;
using Data.ProductManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options; 
using Services.Backend.CouponPromotion.Interface; 
 
using Services.Backend.SystemUserManagement; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using Utility.API;
using Utility.Enum; 
using Utility.Models.Admin.WalletPackage;
using Utility.Models.Frontend.CouponPromotion;
using Utility.ResponseMapper;

namespace API.Areas.Backend.Controllers
{
    [Authorize(Roles="Root,Admin")]
    public class WalletPackageController : BaseController 
    {
        private readonly IWalletPackageService _get;
        private readonly ILogger _logger;
        public WalletPackageController(
            IOptions<AppSettingsModel> options,
            ISystemUserService systemUserService,
            IWalletPackageService get,
            ILoggerFactory logger) : 
            base(options, systemUserService, PermissionTypes.WalletPackages)
        {
            _get = get;
            _logger = logger.CreateLogger(typeof(WalletPackageController).Name);
             
        }

        /// <summary>
        /// HTTP Status 405 - Method not allowed (Check Get or POST)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        [HttpGet, Route("api/WalletPackage/ById")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseMapper<WalletPackage> response = new();
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

        [HttpPost, Route("api/WalletPackage/AddEdit")]
        public async Task<IActionResult> AddEdit([FromForm] WalletPackage item)
        {
            ResponseMapper<WalletPackage> response = new();
            try
            { 
                if (!await Allowed()) { return Ok(accessResponse); }

                if (await _get.Exists(item.Id, item.NameEn, item.NameAr))
                {
                    accessResponse.Message = "Area Name Already Exists";
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

        [HttpPost, Route("api/WalletPackage/ToggleActive")]
        public async Task<IActionResult> ToggleActiveAsync(int Id)
        {
            ResponseMapper<WalletPackage> response = new();
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

        [HttpPost, Route("api/WalletPackage/UpdateDisplayOrder")]
        public async Task<IActionResult> UpdateDisplayOrder(int Id, int num = 0)
        {
            ResponseMapper<WalletPackage> response = new();
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


        

        [HttpGet, Route("api/WalletPackage/GetWalletPackageOrderHeader")]
        public async Task<IActionResult> GetWalletPackageOrderHeader()
        {
           // WalletPackageHeader walletPackageHeader = new();

            WalletPackageHeader response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var items = await _get.GetAll();
                foreach(var item in items)
                {
                    response.DropdownList.Add(new WalletPackageModel() {Id=item.Id, Title = item.NameEn, Description = item.DescriptionEn });
                }
                response.Packages = await _get.GetTopUpSale();
                return Ok(response);

            }
            catch (Exception ex)
            {
              //  response.CacheException(ex);
                _logger.LogError(ex.Message);
            }
            return Ok(response);
        }

        [HttpGet, Route("api/WalletPackage/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            ResponseMapper<List<WalletPackage>> response = new();
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

        [HttpPost, Route("api/WalletPackage/GetAllForDataTable")]
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
