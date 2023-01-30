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
    public class BannerController : BaseController 
    {
        private readonly IBannerService _get;
        private readonly ILogger _logger;
        public BannerController(
            IOptions<AppSettingsModel> options,
            ISystemUserService systemUserService,
            IBannerService get,
            ILoggerFactory logger) : 
            base(options, systemUserService, PermissionTypes.Banner)
        {
            _get = get;
            _logger = logger.CreateLogger(typeof(SystemUserController).Name);
             
        }

        /// <summary>
        /// HTTP Status 405 - Method not allowed (Check Get or POST)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        [HttpGet, Route("api/Banner/ById")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseMapper<Banner> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                if (id > 0)
                {
                    var item = await _get.GetById(id);
                    item = BuildUrl(item);
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

        [HttpPost, Route("api/Banner/AddEdit")]
        public async Task<IActionResult> AddEdit([FromForm] Banner item)
        {
            ResponseMapper<Banner> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                SaveImage(ref item);


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

        [HttpPost, Route("api/Banner/ToggleActive")]
        public async Task<IActionResult> ToggleActiveAsync(int Id)
        {
            ResponseMapper<Banner> response = new();
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

        [HttpPost, Route("api/Banner/UpdateDisplayOrder")]
        public async Task<IActionResult> UpdateDisplayOrder(int Id, int num = 0)
        {
            ResponseMapper<Banner> response = new();
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

        [HttpGet, Route("api/Banner/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            ResponseMapper<List<Banner>> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var items = await _get.GetAll();
                BuildUrls(items.ToList());
                response.GetAll(items.ToList());

            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }
            return Ok(response);
        }

        [HttpPost, Route("api/Banner/GetAllForDataTable")]
        public async Task<IActionResult> GetAllForDataTable()
        { 
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var items = await _get.GetAllForDataTable(base.GetDataTableParameters, GetBaseImageUrl(AppSettings.ImageBanner));
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

        #region Utility
        private void SaveImage(ref Banner model)
        {


            if (model.ImageEn != null && model.ImageEn.Length > 0)
            {
                string fileName = MediaHelper.SaveImageToFile(model.ImageEn, "/" + AppSettings.ImageBanner, AppSettings.ImageBannerResized);
                if (!string.IsNullOrEmpty(fileName))
                    model.ImageNameEn = fileName;
            }

            if (model.ImageAr != null && model.ImageAr.Length > 0)
            {
                string fileName = MediaHelper.SaveImageToFile(model.ImageAr, "/" + AppSettings.ImageBanner, AppSettings.ImageBannerResized);
                if (!string.IsNullOrEmpty(fileName))
                    model.ImageNameAr = fileName;
            }

        }



    

        private List<Banner> BuildUrls(List<Banner> items)
        {
            for (var index = 0; index < items.Count; index++)
            {
                items[index] = BuildUrl(items[index]);
            }
            return items;
        }
        private Banner BuildUrl(Banner item)
        {

            if (!string.IsNullOrEmpty(item.ImageNameEn))
            {
                item.ImageUrlEn = GetImageUrl(AppSettings.ImageBanner,item.ImageNameEn);

            }
            if (!string.IsNullOrEmpty(item.ImageNameAr))
            {
                item.ImageUrlAr  = GetImageUrl(AppSettings.ImageBanner, item.ImageNameAr);

            }
            return item;
        }

        #endregion Utitlity
         

       

    }
}
