using Data.ProductManagement;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Enum;

namespace Services.Frontend.ProductManagement
{
    public interface IProductService
    {
        #region Product 
        Task<IEnumerable<Product>> GetProductsByCategorySeoName(string SeoName);
        Task<IList<Product>> GetAll(int categoryId = 0, string keyword = "", ProductType? productType = null,
            bool favorite = false, int customerId = 0, string categorySeoName = "");
        Task<Product> GetById(int id);
        Task<Product> GetProductBySeoName(string seoName);
        Task<IList<ProductDetail>> GetAllProductDetail(int productId);
        Task<List<Product>> GetAllLowStockProduct(int lowStockThreshold);
        #endregion

        #region Product Stock Management       
        Task AdjustStockQuantity(Product product, int stock, RelatedEntityType relatedEntityType, int relatedEntityId,
            ProductActionType productActionType);
        #endregion

        #region Misc
        Task<int> GetAvailableStockQuantity(int productId, int? customerId = null, string customerGuidValue = "");
        #endregion

        #region Favourites
        Task<IList<Favorite>> GetAllFavorite(int? customerId = null, int? productId = null);
        Task<Favorite> GetFavoriteById(int id);
        Task<Favorite> CreateFavorite(Favorite model);
        Task<bool> UpdateFavorite(Favorite model);
        Task<bool> DeleteFavorite(Favorite model);
        #endregion

        #region Availability notify request
        Task<ProductAvailabilityNotifyRequest> GetProductAvailabilityNotifyRequest(int customerId, int productId);
        Task<ProductAvailabilityNotifyRequest> CreateProductAvailabilityNotifyRequest(ProductAvailabilityNotifyRequest model);
        Task<bool> UpdateProductAvailabilityNotifyRequest(ProductAvailabilityNotifyRequest model);
        Task<bool> DeleteProductAvailabilityNotifyRequest(ProductAvailabilityNotifyRequest model);
        #endregion

        #region Subscription Duraion
        Task<IList<SubscriptionDuration>> GetAllSubscriptionDuration();
        Task<SubscriptionDuration> GetSubscriptionDurationById(int id);
        #endregion

        #region Subscription Delivery Date
        Task<IList<SubscriptionDeliveryDate>> GetAllSubscriptionDeliveryDate();
        Task<SubscriptionDeliveryDate> GetSubscriptionDeliveryDateById(int id);
        #endregion
    }
}
