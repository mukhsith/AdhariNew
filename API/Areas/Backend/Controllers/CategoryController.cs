using Data.Content;
using Data.ProductManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Backend.Content.Interface;
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
    public class CategoryController : BaseController 
    {
        private readonly ICategoryService _get;
        private readonly ILogger _logger;
        public CategoryController(
            IOptions<AppSettingsModel> options,
            ISystemUserService systemUserService,
            ICategoryService get,
            ILoggerFactory logger) : 
            base(options, systemUserService, PermissionTypes.Category)
        {
            _get = get;
            _logger = logger.CreateLogger(typeof(CategoryController).Name);
             
        }

        /// <summary>
        /// HTTP Status 405 - Method not allowed (Check Get or POST)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        [HttpGet, Route("api/Category/ById")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseMapper<Category> response = new();
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

        [HttpPost, Route("api/Category/AddEdit")]
        public async Task<IActionResult> AddEdit([FromForm] Category item)
        {
            ResponseMapper<Category> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                if (await _get.Exists(item.Id, item.NameEn, item.NameAr))
                {
                    accessResponse.Message = "Category Name Already Exists";
                    accessResponse.Success = false;
                    accessResponse.StatusCode = 300;
                    return Ok(accessResponse);
                }
                if (item.ProductTypeId == ProductType.SubscriptionProduct) //only subscription is allowed to create
                {
                    if (await _get.ExistsCategory(item.Id, ProductType.SubscriptionProduct))
                    {
                        accessResponse.Message = "Subscription Category is Already Exists";
                        accessResponse.Success = false;
                        accessResponse.StatusCode = 300;
                        return Ok(accessResponse);
                    }
                }
                //for seo fix space with dash
                if (item.NameEn is not null)
                {
                    item.SeoName = item.NameEn.Replace(" ", "-");
                }
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

        [HttpPost, Route("api/Category/ToggleActive")]
        public async Task<IActionResult> ToggleActiveAsync(int Id)
        {
            ResponseMapper<Category> response = new();
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

        [HttpPost, Route("api/Category/UpdateDisplayOrder")]
        public async Task<IActionResult> UpdateDisplayOrder(int Id, int num = 0)
        {
            ResponseMapper<Category> response = new();
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

        [HttpGet, Route("api/Category/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            ResponseMapper<List<Category>> response = new();
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

        [HttpPost, Route("api/Category/GetAllForDataTable")]
        public async Task<IActionResult> GetAllForDataTable()
        { 
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var items = await _get.GetAllForDataTable(base.GetDataTableParameters, GetBaseImageUrl(AppSettings.ImageCategory));
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
        private void SaveImage(ref Category model)
        {
            if (model.Image != null && model.Image.Length > 0)
            {
                string fileName = MediaHelper.SaveImageToFile(model.Image, "/" + AppSettings.ImageCategory, AppSettings.ImageCategoryResized);
                if (!string.IsNullOrEmpty(fileName))
                    model.ImageName = fileName;
            }
            //mobile normal icon image
            if (model.ImageNormalIcon != null && model.ImageNormalIcon.Length > 0)
            {
                string fileName = MediaHelper.SaveImageToFile(model.ImageNormalIcon, "/" + AppSettings.ImageCategory, AppSettings.ImageCategoryResized);
                if (!string.IsNullOrEmpty(fileName))
                    model.ImageNormalIconName = fileName;
            }
            //mobile selected icon image
            if (model.ImageSelectedIcon != null && model.ImageSelectedIcon.Length > 0)
            {
                string fileName = MediaHelper.SaveImageToFile(model.ImageSelectedIcon, "/" + AppSettings.ImageCategory, AppSettings.ImageCategoryResized);
                if (!string.IsNullOrEmpty(fileName))
                    model.ImageSelectedIconName = fileName;
            }
            //Desktop image
            if (model.ImageDesktop != null && model.ImageDesktop.Length > 0)
            {
                string fileName = MediaHelper.SaveImageToFile(model.ImageDesktop, "/" + AppSettings.ImageCategory, AppSettings.ImageCategoryResized);
                if (!string.IsNullOrEmpty(fileName))
                    model.ImageDesktopName = fileName;
            }

            //Mobile image
            if (model.ImageMobile != null && model.ImageMobile.Length > 0)
            {
                string fileName = MediaHelper.SaveImageToFile(model.ImageMobile, "/" + AppSettings.ImageCategory, AppSettings.ImageCategoryResized);
                if (!string.IsNullOrEmpty(fileName))
                    model.ImageMobileName = fileName;
            }
            //Desktop image
            if (model.ImageDesktopAr != null && model.ImageDesktopAr.Length > 0)
            {
                string fileName = MediaHelper.SaveImageToFile(model.ImageDesktopAr, "/" + AppSettings.ImageCategory, AppSettings.ImageCategoryResized);
                if (!string.IsNullOrEmpty(fileName))
                    model.ImageDesktopNameAr = fileName;
            }

            //Mobile image
            if (model.ImageMobileAr != null && model.ImageMobileAr.Length > 0)
            {
                string fileName = MediaHelper.SaveImageToFile(model.ImageMobileAr, "/" + AppSettings.ImageCategory, AppSettings.ImageCategoryResized);
                if (!string.IsNullOrEmpty(fileName))
                    model.ImageMobileNameAr = fileName;
            }

        }

        private List<Category> BuildUrls(List<Category> items)
        {
            for (var index = 0; index < items.Count; index++)
            {
                items[index] = BuildUrl(items[index]);
            }
            return items;
        }
        private Category BuildUrl(Category item)
        {

            if (!string.IsNullOrEmpty(item.ImageName))
            {
                item.ImageUrl = GetImageUrl(AppSettings.ImageCategory,item.ImageName);

            }

            if (!string.IsNullOrEmpty(item.ImageNormalIconName))
            {
                item.ImageNormalIconUrl = GetImageUrl(AppSettings.ImageCategory, item.ImageNormalIconName);

            }

            if (!string.IsNullOrEmpty(item.ImageSelectedIconName))
            {
                item.ImageSelectedIconUrl = GetImageUrl(AppSettings.ImageCategory, item.ImageSelectedIconName);

            }

            if (!string.IsNullOrEmpty(item.ImageDesktopName))
            {
                item.ImageDesktopUrl = GetImageUrl(AppSettings.ImageCategory, item.ImageDesktopName);

            }

            if (!string.IsNullOrEmpty(item.ImageMobileName))
            {
                item.ImageMobileUrl = GetImageUrl(AppSettings.ImageCategory, item.ImageMobileName);

            }

            if (!string.IsNullOrEmpty(item.ImageDesktopNameAr))
            {
                item.ImageDesktopUrlAr = GetImageUrl(AppSettings.ImageCategory, item.ImageDesktopNameAr);

            }

            if (!string.IsNullOrEmpty(item.ImageMobileNameAr))
            {
                item.ImageMobileUrlAr = GetImageUrl(AppSettings.ImageCategory, item.ImageMobileNameAr);

            }

            return item;
        }

        #endregion Utitlity
         

       

    }
}
