using API.Areas.Frontend.Factories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API;
using Utility.Models.Frontend.ProductManagement;
using Utility.Models.QueryParameters;
using Utility.ResponseMapper;

namespace API.Areas.Frontend.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductModelFactory _productModelFactory;
        public ProductController(IOptions<AppSettingsModel> options, 
            IProductModelFactory productModelFactory) : base(options)
        {
            _productModelFactory = productModelFactory;
        }

        /// <summary>
        /// To get products
        /// </summary>
        /// <returns>Products</returns>
        [HttpPost, Route("/webapi/product/products")]
        public async Task<APIResponseModel<List<ProductModel>>> GetProducts(ProductQueryParameters p)
        {
            p.CustomerId = LoggedInCustomerId;
            return await _productModelFactory.PrepareProducts(isEnglish: isEnglish, p: p);
        }

        /// <summary>
        /// To add or remove favourites
        /// </summary>
        [HttpGet, Route("/webapi/product/addorremovefavourite")]
        [Authorize]
        public async Task<APIResponseModel<bool>> AddOrRemoveFavourite(int productId)
        {
            return await _productModelFactory.AddOrRemoveFavourite(isEnglish: isEnglish, customerId: LoggedInCustomerId, productId: productId);
        }

        /// <summary>
        /// To add or remove product availability notify request
        /// </summary>
        [HttpGet, Route("/webapi/product/addorremoveproductavailabilitynotifyrequest")]
        [Authorize]
        public async Task<APIResponseModel<bool>> AddOrRemoveProductAvailabilityNotifyRequest(int productId)
        {
            return await _productModelFactory.AddOrRemoveProductAvailabilityNotifyRequest(isEnglish: isEnglish, customerId: LoggedInCustomerId, productId: productId);
        }
    }
}
