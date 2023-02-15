using Data.ProductManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Backend.ProductManagement.Interface;
using Services.Backend.SystemUserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using Utility.API;
using Utility.Enum;
using Utility.Models.Admin.ProductManagement;
using Utility.ResponseMapper;

namespace API.Areas.Backend.Controllers
{
    public class BundledProductController : BaseController 
    {
        private readonly IProductService _get;
        private readonly ILogger _logger;
        public BundledProductController(
            IOptions<AppSettingsModel> options,
            ISystemUserService systemUserService,
            IProductService get,
            ILoggerFactory logger) : 
            base(options, systemUserService, PermissionTypes.BundledProduct)
        {
            _get = get;
            _logger = logger.CreateLogger(typeof(BundledProductController).Name);
             
        }

        /// <summary>
        /// HTTP Status 405 - Method not allowed (Check Get or POST)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        [HttpGet, Route("api/BundledProduct/ById")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                if (id > 0)
                { // GetBaseImageUrl(AppSettings.ImageProduct)
                    var item = await _get.GetById(id, GetBaseImageUrl(AppSettings.ImageProduct),ProductType.BundledProduct);
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
        
       [HttpGet, Route("api/BundledProduct/GetAllProductAndCategory")]
        public async Task<IActionResult> GetAllProductAndCategory()
        {
            ResponseMapper<ProductAndCategoryModel> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                var productImagePath = GetBaseImageUrl(AppSettings.ImageProduct);
                var item = await _get.GetAllProductAndCategory(productImagePath);
                response.GetById(item);


            }
            catch (Exception ex)
            {
                response.CacheException(ex);
                _logger.LogError(ex.Message);
            }

            return Ok(response);
        }
        [HttpPost, Route("api/BundledProduct/AddEdit")]
        public async Task<IActionResult> AddEdit([FromForm] Product item)
        {
            ResponseMapper<Product> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }
                if (await _get.Exists(item.Id, item.NameEn, item.NameAr, ProductType.BundledProduct))
                {
                    accessResponse.Message = "Bundled Product Name Already Exists";
                    accessResponse.Success = false;
                    accessResponse.StatusCode = 300;
                    return Ok(accessResponse);
                }
                //for english seo fix space with dash
                if (item.NameEn is not null)
                {
                    item.SeoName = item.NameEn.Replace(" ", "-").Replace(" & ","");
                }
                
                item.ProductType = ProductType.BundledProduct;
                SaveImage(ref item);
                if (item.Id > 0)
                {
                    item.ModifiedBy = UserId;
                    await _get.UpdateBundle(item);
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

        [HttpPost, Route("api/BundledProduct/ToggleActive")]
        public async Task<IActionResult> ToggleActiveAsync(int Id)
        {
            ResponseMapper<Product> response = new();
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

        [HttpPost, Route("api/BundledProduct/UpdateDisplayOrder")]
        public async Task<IActionResult> UpdateDisplayOrder(int Id, int num = 0)
        {
            ResponseMapper<Product> response = new();
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

        [HttpGet, Route("api/BundledProduct/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            ResponseMapper<List<Product>> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }

                var items = await _get.GetAll(ProductType.BundledProduct);
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

        [HttpPost, Route("api/BundledProduct/GetAllForDataTable")]
        public async Task<IActionResult> GetAllForDataTable()
        {
            //var dateOne = DateTime.Now;
            ResponseMapper<dynamic> response = new();
            try
            {
                if (!await Allowed()) { return Ok(accessResponse); }


                AdminProductSearchParam param = new();

                var productName = HttpContext.Request.Form["productName"].FirstOrDefault();
                var categoryID = HttpContext.Request.Form["categoryID"].FirstOrDefault();

                param.productName = productName;
                param.categoryID = Utility.Helpers.Common.ConvertTextToIntOptional(categoryID);

                var items = await _get.GetAllForDataTableByProductType(base.GetDataTableParameters, GetBaseImageUrl(AppSettings.ImageProduct), ProductType.BundledProduct, param);
                //// response.GetAll(items);
                //var dateTwo = DateTime.Now;
                //var diff = dateTwo.Subtract(dateOne);
                //var res = String.Format("{0}:{1}:{2}:{3}", diff.Hours, diff.Minutes, diff.Seconds, diff.Milliseconds);
                //_logger.LogInformation(res);
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
        private void SaveImage(ref Product model)
        {
            if (model.Image != null && model.Image.Length > 0)
            {
                string fileName = MediaHelper.SaveImageToFile(model.Image, "/" + AppSettings.ImageProduct, AppSettings.ImageProductResized);
                if (!string.IsNullOrEmpty(fileName))
                    model.ImageName = fileName;
            }

           

        }

        private List<Product> BuildUrls(List<Product> items)
        {
            for (var index = 0; index < items.Count; index++)
            {
                items[index] = BuildUrl(items[index]);
            }
            return items;
        }
        private Product BuildUrl(Product item)
        {

            if (!string.IsNullOrEmpty(item.ImageName))
            {
                item.ImageUrl = GetImageUrl(AppSettings.ImageProduct,item.ImageName);

            }
            
            return item;
        }

        #endregion Utitlity
         

       

    }
}
