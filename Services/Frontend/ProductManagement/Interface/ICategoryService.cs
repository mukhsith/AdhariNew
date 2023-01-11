using Data.ProductManagement;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API;
using Utility.Models.Frontend.ProductManagement;

namespace Services.Frontend.ProductManagement.Interface
{
    public interface ICategoryService
    {
        #region Category Service 
        Task<IEnumerable<CategoryModel>> GetAll(bool isEnglish);
        Task<IEnumerable<CategoryModel>> GetAllHero(bool isEnglish, string baseUrl);
        Task<DataTableResult<List<Category>>> GetAllForDataTable(DataTableParam param, string baseImageUrl);    
        Task<Category> GetById(int id); 
        
        #endregion
        
        
    }
}
