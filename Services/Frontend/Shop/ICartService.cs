using Data.Shop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Frontend.Shop
{
    public interface ICartService
    {
        #region Cart
        Task<IList<CartItem>> GetAllCartItem(string customerGuidValue = "", int? customerId = null, int? productDetailId = null);
        Task<CartItem> GetCartItemById(int id);
        Task<CartItem> CreateCartItem(CartItem model);
        Task<bool> UpdateCartItem(CartItem model);
        Task<bool> DeleteCartItem(CartItem model);
        Task<bool> DeleteCartItems(IList<CartItem> models);
        Task DeleteCartItemByCustomer(string customerGuidValue = "", int? customerId = null);
        Task HoldAndReleaseCartItem(bool isHold, int? customerId = null, string customerGuidValue = "");
        Task<List<CartItem>> GetHeldCartItems(int? customerId = null, string customerGuidValue = "");
        #endregion

        #region Cart attributes
        Task<List<CartAttribute>> GetAllCartAttribute(string customerGuidValue = "", int? customerId = null);
        Task<CartAttribute> GetCartAttributeById(int Id);
        Task<CartAttribute> CreateCartAttribute(CartAttribute model);
        Task<bool> UpdateCartAttribute(CartAttribute model);
        Task<bool> DeleteCartAttribute(CartAttribute model);
        Task<bool> DeleteCartAttributeByCustomer(string customerGuidValue = "", int? customerId = null);
        #endregion

        #region Subscription attributes
        Task<SubscriptionAttribute> GetSubscriptionAttributeByCustomerId(int customerId);
        Task<SubscriptionAttribute> GetSubscriptionAttributeById(int Id);
        Task<SubscriptionAttribute> CreateSubscriptionAttribute(SubscriptionAttribute model);
        Task<bool> UpdateSubscriptionAttribute(SubscriptionAttribute model);
        Task<bool> DeleteSubscriptionAttribute(SubscriptionAttribute model);
        Task<bool> DeleteSubscriptionAttributeByCustomer(int? customerId = null);
        #endregion

        #region Subscription holding
        Task<IList<SubscriptionHolding>> GetAllSubscriptionHolding(int? customerId = null,
            int? productDetailId = null);
        Task<SubscriptionHolding> GetSubscriptionHoldingById(int id);
        Task<SubscriptionHolding> CreateSubscriptionHolding(SubscriptionHolding model);
        Task<bool> UpdateSubscriptionHolding(SubscriptionHolding model);
        Task<bool> DeleteSubscriptionHolding(SubscriptionHolding model);
        Task<bool> DeleteSubscriptionHoldings(IList<SubscriptionHolding> models);
        Task DeleteSubscriptionHoldingByCustomer(string customerGuidValue = "", int? customerId = null);
        Task HoldAndReleaseSubscriptionHolding(bool isHold, int? customerId = null, string customerGuidValue = "");
        Task<List<SubscriptionHolding>> GetHeldSubscriptionHoldings(int? customerId = null, string customerGuidValue = "");
        #endregion
    }
}
