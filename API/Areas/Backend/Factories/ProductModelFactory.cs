
using API.Areas.Backend.Helpers;
using Data.CustomerManagement;
using Data.ProductManagement;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Backend.CustomerManagement;
using Services.Backend.ProductManagement.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using Utility.Helpers;
using Utility.Models.Frontend.ProductManagement;
using Utility.Models.QueryParameters;
using Utility.ResponseMapper;

namespace API.Areas.Backend.Factories
{
    public class ProductModelFactory : IProductModelFactory
    {
        private readonly ILogger _logger;
        private readonly AppSettingsModel _appSettings;
        private readonly IModelHelper _modelHelper;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        public ProductModelFactory(ILoggerFactory logger,
            IOptions<AppSettingsModel> options,
            IModelHelper modelHelper,
            IProductService productService,
            ICustomerService customerService)
        {
            _logger = logger.CreateLogger(typeof(ProductModelFactory).Name);
            _appSettings = options.Value;
            _modelHelper = modelHelper;
            _productService = productService;
            _customerService = customerService;
        }
        public async Task<APIResponseModel<List<ProductModel>>> PrepareProducts(bool isEnglish, ProductQueryParameters p)
        {
            var response = new APIResponseModel<List<ProductModel>>();
            try
            {
                IList<Product> products = new List<Product>();
                Customer customer = null;
                bool loadDescription = false;

                if (p.CustomerId > 0)
                {
                    customer = await _customerService.GetCustomerById(p.CustomerId);
                }

                if (p.Id > 0)
                {
                    loadDescription = true;
                    var product = await _productService.GetById(p.Id);
                    if (product != null && !product.Deleted && product.Active)
                    {
                        products.Add(product);
                    }
                }
                else if (!string.IsNullOrEmpty(p.SeoName))
                {
                    loadDescription = true;
                    var product = await _productService.GetProductBySeoName(p.SeoName);
                    if (product != null && !product.Deleted && product.Active)
                    {
                        products.Add(product);
                    }
                }
                else
                {
                    products = await _productService.GetAll(categoryId: p.CategoryId, keyword: p.Keyword, productType: p.ProductType,
                        favorite: p.Favorite, customerId: p.CustomerId, categorySeoName: p.CategorySeoName);

                    products = products.OrderBy(a => a.DisplayOrder).ThenByDescending(a => a.Id).ToList();

                    response.DataRecordCount = products.Count;
                    products = products.Skip((p.Page - 1) * p.Limit).Take(p.Limit).ToList();
                }

                List<ProductModel> productModels = new();
                foreach (var product in products)
                {
                    var productModel = await _modelHelper.PrepareProductModel(product: product, isEnglish: isEnglish, customerGuidValue: p.CustomerGuidValue,
                        customer: customer, loadDescription: loadDescription, loadPrice: true, calculateStock: true, loadCategory: true);
                    productModels.Add(productModel);
                }

                response.Data = productModels;
                response.Message = isEnglish ? Messages.Success : MessagesAr.Success;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }
      }
}
