using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API;
using Utility.Models.Frontend.ProductManagement;
using Utility.Models.QueryParameters;
using Utility.ResponseMapper;

namespace Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IAPIHelper _apiHelper;
        private IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly AppSettingsModel _appSettingsModel;
        private readonly ILogger _logger;
        public ProductController(IAPIHelper apiHelper,
            IStringLocalizer<SharedResource> sharedLocalizer,
            IRazorViewEngine razorViewEngine,
            IOptions<AppSettingsModel> options,
            ILoggerFactory logger) : base(razorViewEngine)
        {
            _apiHelper = apiHelper;
            _sharedLocalizer = sharedLocalizer;
            _appSettingsModel = options.Value;
            _logger = logger.CreateLogger(typeof(ProductController).Name);
        }
        public async Task<IActionResult> Products(string seoName)
        {
            try
            {
                if (!string.IsNullOrEmpty(seoName))
                {
                    var responseCategoriesModel = await _apiHelper.GetAsync<APIResponseModel<List<CategoryModel>>>("webapi/common/categories?seoName=" + seoName);
                    if (responseCategoriesModel.Success && responseCategoriesModel.Data != null && responseCategoriesModel.Data.Count > 0)
                    {
                        var categoryModel = responseCategoriesModel.Data[0];
                        return View(categoryModel);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return RedirectToRoute("home");
        }
        public async Task<JsonResult> ProductsByAjax(string seoName, int limit, int page, bool search = false, string keyword = "")
        {
            ProductQueryParameters query = new();
            var responseModel = new APIResponseModel<List<ProductModel>>();
            try
            {
                var customerGuidValue = Convert.ToString(Request.Cookies["CustomerGuidValue"]);
                if (string.IsNullOrEmpty(customerGuidValue))
                {
                    customerGuidValue = Guid.NewGuid().ToString();
                    Response.Cookies.Append("CustomerGuidValue", customerGuidValue, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
                }

                query.CategorySeoName = seoName;
                query.CustomerGuidValue = customerGuidValue;
                query.Limit = limit;
                query.Page = page;
                query.Keyword = keyword;

                var partialViewName = search ? "_SearchProductList" : "_ProductList";

                responseModel = await _apiHelper.PostAsync<APIResponseModel<List<ProductModel>>>("webapi/product/products", query);
                if (responseModel.Success && responseModel.Data != null)
                {
                    return Json(new
                    {
                        html = await RenderPartialViewToStringAsync(partialViewName, responseModel.Data),
                        TotalProductCount = responseModel.DataRecordCount,
                        ProductCount = responseModel.Data.Count,
                        Success = true,
                        MessageCode = 0
                    });
                }
            }
            catch (Exception ex)
            {
                responseModel.Message = ex.Message;
            }

            var notificationModels = new List<ProductModel>();
            return Json(new
            {
                html = await RenderPartialViewToStringAsync("_ProductList", notificationModels),
                TotalProductCount = 0,
                ProductCount = 0,
                Success = true,
                MessageCode = 0
            });
        }

        /// <summary>
        /// Get product by seo name
        /// </summary>
        public async Task<IActionResult> ProductDetails(string catName = "", string seoName = "")
        {
            try
            {
                ProductQueryParameters query = new();
                query.SeoName = seoName;

                var customerGuidValue = Convert.ToString(Request.Cookies["CustomerGuidValue"]);
                if (string.IsNullOrEmpty(customerGuidValue))
                {
                    customerGuidValue = Guid.NewGuid().ToString();
                    Response.Cookies.Append("CustomerGuidValue", customerGuidValue, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
                }
                query.CustomerGuidValue = customerGuidValue;

                var responseModel = await _apiHelper.PostAsync<APIResponseModel<List<ProductModel>>>("webapi/product/products", query);
                if (responseModel.Success && responseModel.Data != null && responseModel.Data.Count > 0)
                {
                    return View(responseModel.Data[0]);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return RedirectToRoute("home");
        }

        /// <summary>
        /// Get favorite products
        /// </summary>
        [Authorize]
        public async Task<IActionResult> FavoriteProducts()
        {
            ViewBag.isFav = 1;
            List<ProductModel> productModels = new();

            try
            {
                ProductQueryParameters query = new();
                var responseModel = await _apiHelper.PostAsync<APIResponseModel<List<ProductModel>>>("webapi/product/products", query);
                if (responseModel.MessageCode == 401)
                {
                    return RedirectToRoute("login");
                }

                if (responseModel.Success && responseModel.Data != null)
                {
                    productModels = responseModel.Data;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return View(productModels);
        }

        /// <summary>
        /// Add or remove favourites
        /// </summary>
        public async Task<JsonResult> AddOrRemoveFavourite(int productId)
        {
            var responseModel = new APIResponseModel<bool>();
            try
            {
                responseModel = await _apiHelper.GetAsync<APIResponseModel<bool>>("webapi/product/addorremovefavourite?productId=" + productId);
                if (responseModel.MessageCode == 401)
                {
                    return Json(responseModel);
                }
            }
            catch (Exception ex)
            {
                responseModel.Message = ex.Message;
                _logger.LogInformation(ex.Message);
            }

            return Json(responseModel);
        }

        /// <summary>
        /// Add Or Remove Product Availability
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<JsonResult> AddOrRemoveProductAvailability(int productId)
        {
            var responseModel = new APIResponseModel<bool>();
            try
            {
                responseModel = await _apiHelper.GetAsync<APIResponseModel<bool>>("webapi/product/addorremoveproductavailabilitynotifyrequest?productId=" + productId);
                if (responseModel.MessageCode == 401)
                {
                    return Json(responseModel);
                }
            }
            catch (Exception ex)
            {
                responseModel.Message = ex.Message;
                _logger.LogInformation(ex.Message);
            }

            return Json(responseModel);
        }
    }
}
