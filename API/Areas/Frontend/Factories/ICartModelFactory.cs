using System.Threading.Tasks;
using Utility.Models.Frontend.Shop;
using Utility.ResponseMapper;

namespace API.Areas.Frontend.Factories
{
    public interface ICartModelFactory
    {
        Task<APIResponseModel<bool>> AddCartItem(bool isEnglish, CartItemModel cartItemModel);
        Task<APIResponseModel<object>> PrepareCartItemCount(bool isEnglish, int customerId = 0, string customerGuidValue = "");
        Task<APIResponseModel<CartModel>> PrepareCart(bool isEnglish, int customerId = 0, string customerGuidValue = "");
        Task<APIResponseModel<CartModel>> EditCartItem(bool isEnglish, CartItemModel cartItemModel);
        Task<APIResponseModel<CartModel>> DeleteCartItem(bool isEnglish, int id);
        Task<APIResponseModel<bool>> DeleteCartItems(bool isEnglish, int customerId = 0, string customerGuidValue = "");
        Task<APIResponseModel<CartSummaryModel>> PrepareCartSummaryModel(bool isEnglish, int customerId);
        Task<APIResponseModel<CartSummaryModel>> SaveCartAttribute(bool isEnglish, int customerId, CartAttributeModel cartAttributeModel);
        Task<APIResponseModel<CheckOutModel>> PrepareCheckOutModel(bool isEnglish, int customerId);
        Task<APIResponseModel<bool>> ValidateCart(bool isEnglish, int customerId = 0, string customerGuidValue = "");
    }
}
