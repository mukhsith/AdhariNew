using Data.ProductManagement;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;
using Utility.Models.Admin.ProductManagement;

namespace Services.Backend.ProductManagement.Interface
{
    public interface IProductService
    {
        #region Product Service 
        Task<IList<Product>> GetAll(int categoryId = 0, string keyword = "", ProductType? productType = null,
           bool favorite = false, int customerId = 0, string categorySeoName = "");
        Task<Product> GetById(int id);
        Task<Product> GetProductBySeoName(string seoName);
        Task<IList<ProductDetail>> GetAllProductDetail(int productId);

        Task<IEnumerable<Product>> GetAll(ProductType? productType=null);
        Task<IEnumerable<SubscriptionDuration>> GetAllSubscriptionDurations();
        Task<dynamic> GetAllForDataTable(DataTableParam param, string baseImageUrl, AdminProductSearchParam Searchparam);
        Task<dynamic> GetAllForDataTableByProductType(DataTableParam param, string baseImageUrl, ProductType productType, AdminProductSearchParam Searchparam);
        Task<dynamic> GetById(int id, string baseImageUrl, ProductType productType);
         
        Task<Product> GetByIdOnlyProduct(int id);//only for product NOT ( bundle product, subscription) 
        //Task<bool> Exists(int? Id, string titleEn, string titleAr);
        Task<bool> Exists(int? Id, string titleEn, string titleAr, ProductType prdocutType);
        Task<Product> Create(Product model);
        Task<bool> Update(Product model);
        Task<bool> UpdateBundle(Product model);
        Task<bool> Delete(Product model);
        Task<bool> ToggleActive(int id);
        Task<Product> UpdateDisplayOrder(int id, int num = 0);

        Task<CategoryAndItemSizeModel> GetAllCategoryItemSize();
        Task<ProductAndCategoryModel> GetAllProductAndCategory(string productImagePath);
        #endregion

        #region Create Offline Order
        Task<ProductAndCategoryModel> GetAllProductForOfflineOrder(string productImagePath, string customerId);
        #endregion

          

        #region Product Stock

        Task<bool> ProductUpdateStock(ProductStockHistory ProductStockHistory);
        Task<dynamic> ProductHistoryGetAllForDataTable(DataTableParam param, int productId);
        #endregion

        #region Product Stock Management       
        Task DeductStockQuantity(Product product, int stockNeedtoDeduct, RelatedEntityType relatedEntityType, int relatedEntityId);
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
