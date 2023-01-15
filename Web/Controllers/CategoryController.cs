using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API;
using Utility.Models.Frontend.ProductManagement;
using Utility.ResponseMapper;

namespace Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IAPIHelper _apiHelper;
        private readonly ILogger _logger;
        public CategoryController(IAPIHelper apiHelper, ILoggerFactory logger)
        {
            _apiHelper = apiHelper;
            _logger = logger.CreateLogger(typeof(CategoryController).Name);
        }

        /// <summary>
        /// Get category
        /// </summary>
        public async Task<IActionResult> Category(string seoName)
        {
            var categoryModel = new CategoryModel();
            try
            {
                if (!string.IsNullOrEmpty(seoName))
                {
                    var responseCategoriesModel = await _apiHelper.GetAsync<APIResponseModel<List<CategoryModel>>>("webapi/product/categories?seoName=" + seoName);
                    if (responseCategoriesModel.Success && responseCategoriesModel.Data != null && responseCategoriesModel.Data.Count > 0)
                    {
                        categoryModel = responseCategoriesModel.Data[0];
                    }
                }
                else
                {
                    var responseCategoriesModel = await _apiHelper.GetAsync<APIResponseModel<List<CategoryModel>>>("webapi/category/categories");
                    if (responseCategoriesModel.Success && responseCategoriesModel.Data != null && responseCategoriesModel.Data.Count > 0)
                    {
                        categoryModel = responseCategoriesModel.Data[0];
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return View(categoryModel);
        }
    }
}
