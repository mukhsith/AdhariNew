using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Models.Frontend.ProductManagement;
using Utility.Models.QueryParameters;
using Utility.ResponseMapper;

namespace API.Areas.Backend.Factories
{
    public interface IProductModelFactory
    {
        Task<APIResponseModel<List<ProductModel>>> PrepareProducts(bool isEnglish, ProductQueryParameters p); 
    }
}
