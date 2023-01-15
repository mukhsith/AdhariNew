using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Models.Frontend.ProductManagement;
using Utility.Models.QueryParameters;
using Utility.ResponseMapper;

namespace API.Areas.Frontend.Factories
{
    public interface IProductModelFactory
    {
        Task<APIResponseModel<List<ProductModel>>> PrepareProducts(bool isEnglish, ProductQueryParameters p);
        Task<APIResponseModel<bool>> AddOrRemoveFavourite(bool isEnglish, int customerId, int productId);
        Task<APIResponseModel<bool>> AddOrRemoveProductAvailabilityNotifyRequest(bool isEnglish, int customerId, int productId);
        Task<APIResponseModel<object>> SendLowStockEmailNotification(bool isEnglish, string apiKey);
    }
}
