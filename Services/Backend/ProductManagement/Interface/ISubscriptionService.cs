using Data.Content;
using Data.ProductManagement; 
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API;
using Utility.Models.Backend.ProductManagement;

namespace Services.Backend.ProductManagement.Interface
{
    public interface ISubscriptionService
    {
        #region Subscription Service 
        Task<IEnumerable<Subscription>> GetAll();
        Task<dynamic> GetAllForDataTable(DataTableParam param, string baseImageUrl);
      //  Task<DataTableResult<List<Subscription>>> GetAllForDataTable(DataTableParam param, string baseImageUrl);
        Task<Subscription> GetById(int id);
        Task<ProductAndCategoryModel> GetAllProductAndCategory(string productImagePath);
        Task<bool> SubscriptionExists(int? Id, string titleEn, string titleAr);
        Task<Subscription> Create(Subscription model);
        Task<bool> Update(Subscription model);
        Task<bool> Delete(Subscription model);
        Task<bool> ToggleActive(int id);
        Task<Subscription> UpdateDisplayOrder(int id, int num = 0);
        #endregion
        
        
    }
}
