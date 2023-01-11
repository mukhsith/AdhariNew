using API.Areas.Frontend.Helpers;
using Data.CustomerManagement;
using Data.ProductManagement;
using Microsoft.Extensions.Logging;
using Services.Frontend.CustomerManagement;
using Services.Frontend.ProductManagement.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Helpers;
using Utility.Models.Frontend.ProductManagement;
using Utility.Models.QueryParameters;
using Utility.ResponseMapper;

namespace API.Areas.Frontend.Factories
{
    public class ProductModelFactory : IProductModelFactory
    {
        private readonly ILogger _logger;
        private readonly IModelHelper _modelHelper;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        public ProductModelFactory(ILoggerFactory logger,
            IModelHelper modelHelper,
            IProductService productService,
            ICustomerService customerService)
        {
            _logger = logger.CreateLogger(typeof(ProductModelFactory).Name);
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
                bool loadCartQuantity = false;

                if (p.CustomerId > 0)
                {
                    customer = await _customerService.GetCustomerById(p.CustomerId);
                }

                if (p.Id > 0)
                {
                    loadDescription = true;
                    loadCartQuantity = true;
                    var product = await _productService.GetById(p.Id);
                    if (product != null && !product.Deleted && product.Active)
                    {
                        products.Add(product);
                    }
                }
                else if (!string.IsNullOrEmpty(p.SeoName))
                {
                    loadDescription = true;
                    loadCartQuantity = true;
                    var product = await _productService.GetProductBySeoName(p.SeoName);
                    if (product != null && !product.Deleted && product.Active)
                    {
                        products.Add(product);
                    }
                }
                else
                {
                    loadCartQuantity = true;
                    products = await _productService.GetAll(categoryId: p.CategoryId, keyword: p.Keyword, productType: p.ProductType,
                     favorite: p.Favorite, customerId: p.CustomerId, categorySeoName: p.CategorySeoName);

                    products = products.OrderBy(a => a.DisplayOrder).ThenByDescending(a => a.Id).ToList();

                    response.DataRecordCount = products.Count;
                    products = products.Skip((p.Page - 1) * p.Limit).Take(p.Limit).ToList();
                }

                List<ProductModel> productModels = new();
                foreach (var product in products)
                {
                    var productModel = await _modelHelper.PrepareProductModel(product: product, isEnglish: isEnglish,
                        customerGuidValue: p.CustomerGuidValue, customer: customer, loadDescription: loadDescription, loadPrice: true,
                        calculateStock: true, loadCategory: true, loadSubscriptionAttributes: loadDescription,
                        loadSubscriptionPackTitle: loadDescription, loadCartQuantity: loadCartQuantity);
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
        public async Task<APIResponseModel<bool>> AddOrRemoveFavourite(bool isEnglish, int customerId, int productId)
        {
            var response = new APIResponseModel<bool>();
            try
            {
                string message = string.Empty;
                if (customerId <= 0 || productId <= 0)
                {
                    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    return response;
                }

                var customer = await _customerService.GetCustomerById(customerId);
                if (customer == null || customer.Deleted)
                {
                    response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
                    return response;
                }

                if (!customer.Active)
                {
                    response.Message = isEnglish ? Messages.InactiveCustomer : MessagesAr.InactiveCustomer;
                    return response;
                }

                var product = await _productService.GetById(productId);
                if (product == null)
                {
                    response.Message = isEnglish ? Messages.ProductNotExists : MessagesAr.ProductNotExists;
                    return response;
                }

                var favoritesProduct = (await _productService.GetAllFavorite(customerId: customerId, productId: productId)).FirstOrDefault();
                if (favoritesProduct != null)
                {
                    await _productService.DeleteFavorite(favoritesProduct);

                    response.Message = isEnglish ? Messages.RemoveFavouriteSuccess : MessagesAr.RemoveFavouriteSuccess;
                }
                else
                {
                    await _productService.CreateFavorite(new Favorite
                    {
                        ProductId = productId,
                        CustomerId = customer.Id
                    });

                    response.Message = isEnglish ? Messages.AddFavouriteSuccess : MessagesAr.AddFavouriteSuccess;
                }

                response.Data = true;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = isEnglish ? Messages.InternalServerError : MessagesAr.InternalServerError;
            }

            return response;
        }
        public async Task<APIResponseModel<bool>> AddOrRemoveProductAvailabilityNotifyRequest(bool isEnglish, int customerId, int productId)
        {
            var response = new APIResponseModel<bool>();
            try
            {
                string message = string.Empty;
                if (customerId <= 0 || productId <= 0)
                {
                    response.Message = isEnglish ? Messages.ValidationFailed : MessagesAr.ValidationFailed;
                    return response;
                }

                var customer = await _customerService.GetCustomerById(customerId);
                if (customer == null || customer.Deleted)
                {
                    response.Message = isEnglish ? Messages.CustomerNotExists : MessagesAr.CustomerNotExists;
                    return response;
                }

                if (!customer.Active)
                {
                    response.Message = isEnglish ? Messages.InactiveCustomer : MessagesAr.InactiveCustomer;
                    return response;
                }

                var product = await _productService.GetById(productId);
                if (product == null)
                {
                    response.Message = isEnglish ? Messages.ProductNotExists : MessagesAr.ProductNotExists;
                    return response;
                }

                var productAvailabilityNotifyRequest = await _productService.GetProductAvailabilityNotifyRequest(customerId: customerId, productId: productId);
                if (productAvailabilityNotifyRequest != null)
                {
                    await _productService.DeleteProductAvailabilityNotifyRequest(productAvailabilityNotifyRequest);

                    response.Message = isEnglish ? Messages.RemoveProductAvailabilityNotifyRequestSuccess : MessagesAr.RemoveProductAvailabilityNotifyRequestSuccess;
                }
                else
                {
                    await _productService.CreateProductAvailabilityNotifyRequest(new ProductAvailabilityNotifyRequest
                    {
                        ProductId = productId,
                        CustomerId = customer.Id
                    });

                    response.Message = isEnglish ? Messages.AddProductAvailabilityNotifyRequestSuccess : MessagesAr.AddProductAvailabilityNotifyRequestSuccess;
                }

                response.Data = true;
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
