using API.Areas.Frontend.Factories;
using Data.CustomerManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Backend.SystemUserManagement;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;
using Utility.Models.Frontend.Shop;
using Utility.ResponseMapper;

namespace API.Areas.Backend.Controllers
{
    public class CartController : BaseController
    {
        private readonly ILogger _logger;
        private readonly ICartModelFactory _cartModelFactory;

        public CartController(IOptions<AppSettingsModel> options,
            ISystemUserService systemUserService,
            ICartModelFactory cartModelFactory,
            ILoggerFactory logger) : base(options,systemUserService, PermissionTypes.Orders)
        {
            _cartModelFactory = cartModelFactory;
            _logger = logger.CreateLogger(typeof(CategoryController).Name);
        }

        /// <summary>
        /// Add items to shopping cart
        /// </summary>
        [HttpPost, Route("api/cart/addcartitem")]
        public async Task<APIResponseModel<bool>> AddCartItem([FromBody] CartItemModel cartItemModel)
        {
            return await _cartModelFactory.AddCartItem(isEnglish: IsEnglish, cartItemModel: cartItemModel);
        }

        /// <summary>
        /// Get cart items
        /// </summary>
        [HttpGet, Route("api/cart/getcartitemcount")]
        public async Task<APIResponseModel<object>> GetCartItemCount(int customerId)
        {
            return await _cartModelFactory.PrepareCartItemCount(isEnglish: IsEnglish, customerId: customerId, customerGuidValue: null);
        }

        /// <summary>
        /// Get cart items
        /// </summary>
        [HttpGet, Route("api/cart/getcartitem")]
        public async Task<APIResponseModel<CartModel>> GetCartItem(int customerId)
        {
            return await _cartModelFactory.PrepareCart(IsEnglish, customerId: customerId, customerGuidValue: null);
        }

       

        /// <summary>
        /// edit items in shopping cart
        /// </summary>
        [HttpPost, Route("api/cart/editcartitem")]
        public async Task<APIResponseModel<CartModel>> EditCartItem(CartItemModel cartItemModel)
        { 
            return await _cartModelFactory.EditCartItem(isEnglish: IsEnglish, cartItemModel: cartItemModel);
        }

        /// <summary>
        /// To delete product from cart
        /// </summary>
        [HttpDelete, Route("api/cart/deletecartitem")]
        public async Task<APIResponseModel<CartModel>> DeleteCartItem(int id)
        {
            return await _cartModelFactory.DeleteCartItem(isEnglish: IsEnglish, id: id);
        }

        /// <summary>
        /// Get cart summary
        /// </summary>
        [HttpGet, Route("api/cart/getcartsummary")]
        [Authorize]
        public async Task<APIResponseModel<CartSummaryModel>> GetCartSummary(int customerId)
        {
            return await _cartModelFactory.PrepareCartSummaryModel(isEnglish: IsEnglish, customerId: customerId);
        }

        /// <summary>
        /// Save cart attributes
        /// </summary>
        [HttpPost, Route("api/cart/savecartattributes")]
        [Authorize]
        public async Task<APIResponseModel<CartSummaryModel>> SaveCartAttributes([FromBody] CartAttributeModel cartAttributeModel)
        {
            return await _cartModelFactory.SaveCartAttribute(isEnglish: IsEnglish, customerId: cartAttributeModel.CustomerId, cartAttributeModel: cartAttributeModel);
        }


        /// <summary>
        /// Get checkout summary
        /// </summary>
        //[HttpGet, Route("/webapi/cart/getcheckoutsummary")]
        //[Authorize]
        //public async Task<APIResponseModel<CheckOutModel>> GetCheckOutSummary()
        //{
        //    return await _cartModelFactory.PrepareCheckOutModel(isEnglish: IsEnglish, customerId: LoggedInCustomerId);
        //}
    }
}
