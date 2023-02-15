using Data.Content;
using Data.ProductManagement;
using Data.SystemUserManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;

namespace Services.Backend.ProductManagement.Interface
{
    public interface ICategoryService
    {
        #region Category Service 
        Task<bool> Exists(int? Id, string titleEn, string titleAr);
        Task<bool> ExistsCategory(int Id, ProductType productType);
        Task<IEnumerable<Category>> GetAll();
        Task<DataTableResult<List<Category>>> GetAllForDataTable(DataTableParam param, string baseImageUrl);    
        Task<Category> GetById(int id); 
        Task<Category> Create(Category model);
        Task<bool> Update(Category model);
        Task<bool> Delete(Category model);

        Task<bool> ToggleActive(int id);
        Task<Category> UpdateDisplayOrder(int id, int num = 0);
        #endregion
        
        
    }
}
