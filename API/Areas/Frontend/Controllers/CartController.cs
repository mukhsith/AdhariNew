using API.Areas.Frontend.Factories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Utility.API;
using Utility.Models.Frontend.Shop;
using Utility.ResponseMapper;

namespace API.Areas.Frontend.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartModelFactory _cartModelFactory;
        public CartController(IOptions<AppSettingsModel> options,
            ICartModelFactory cartModelFactory) : base(options)
        {
            _cartModelFactory = cartModelFactory;
        }

        /// <summary>
        /// Get cart items
        /// </summary>
        [HttpGet, Route("/webapi/cart/getcartitemcount")]
        public async Task<APIResponseModel<object>> GetCartItemCount(string customerGuidValue = "")
        {
            return await _cartModelFactory.PrepareCartItemCount(isEnglish: isEnglish, customerId: LoggedInCustomerId, customerGuidValue: customerGuidValue);
        }

        /// <summary>
        /// Get cart items
        /// </summary>
        [HttpGet, Route("/webapi/cart/getcartitem")]
        public async Task<APIResponseModel<CartModel>> GetCartItem(string customerGuidValue = "")
        {
            return await _cartModelFactory.PrepareCart(isEnglish: isEnglish, customerId: LoggedInCustomerId, customerGuidValue: customerGuidValue);
        }

        /// <summary>
        /// Add items to shopping cart
        /// </summary>
        [HttpPost, Route("/webapi/cart/addcartitem")]
        public async Task<APIResponseModel<bool>> AddCartItem([FromBody] CartItemModel cartItemModel)
        {
            cartItemModel.CustomerId = LoggedInCustomerId;
            return await _cartModelFactory.AddCartItem(isEnglish: isEnglish, cartItemModel: cartItemModel);
        }

        /// <summary>
        /// edit items in shopping cart
        /// </summary>
        [HttpPost, Route("/webapi/cart/editcartitem")]
        public async Task<APIResponseModel<CartModel>> EditCartItem(CartItemModel cartItemModel)
        {
            cartItemModel.CustomerId = LoggedInCustomerId;
            return await _cartModelFactory.EditCartItem(isEnglish: isEnglish, cartItemModel: cartItemModel);
        }

        /// <summary>
        /// To delete product from cart
        /// </summary>
        [HttpDelete, Route("/webapi/cart/deletecartitem")]
        public async Task<APIResponseModel<CartModel>> DeleteCartItem(int id)
        {
            return await _cartModelFactory.DeleteCartItem(isEnglish: isEnglish, id: id);
        }

        /// <summary>
        /// Get cart summary
        /// </summary>
        [HttpGet, Route("/webapi/cart/getcartsummary")]
        [Authorize]
        public async Task<APIResponseModel<CartSummaryModel>> GetCartSummary()
        {
            return await _cartModelFactory.PrepareCartSummaryModel(isEnglish: isEnglish, customerId: LoggedInCustomerId);
        }

        /// <summary>
        /// Validate cart
        /// </summary>
        [HttpGet, Route("/webapi/cart/validatecart")]
        public async Task<APIResponseModel<bool>> ValidateCart(string customerGuidValue = "")
        {
            return await _cartModelFactory.ValidateCart(isEnglish: isEnglish, customerGuidValue: customerGuidValue, customerId: LoggedInCustomerId);
        }

        /// <summary>
        /// Save cart attributes
        /// </summary>
        [HttpPost, Route("/webapi/cart/savecartattributes")]
        [Authorize]
        public async Task<APIResponseModel<CartSummaryModel>> SaveCartAttributes([FromBody] CartAttributeModel cartAttributeModel)
        {
            return await _cartModelFactory.SaveCartAttribute(isEnglish: isEnglish, customerId: LoggedInCustomerId, cartAttributeModel: cartAttributeModel);
        }

        /// <summary>
        /// Get checkout summary
        /// </summary>
        [HttpGet, Route("/webapi/cart/getcheckoutsummary")]
        [Authorize]
        public async Task<APIResponseModel<CheckOutModel>> GetCheckOutSummary()
        {
            return await _cartModelFactory.PrepareCheckOutModel(isEnglish: isEnglish, customerId: LoggedInCustomerId);
        }
    }
}
