using Data.Content;
using Data.ProductManagement;
using Data.SystemUserManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API; 
using Utility.Models.Backend.ProductManagement;

namespace Services.Backend.ProductManagement.Interface
{
    public interface IBundledProductService
    {
        #region BundledProduct Service 
        Task<IEnumerable<BundledProduct>> GetAll();
        Task<dynamic> GetAllForDataTable(DataTableParam param, string baseImageUrl);
      //  Task<DataTableResult<List<BundledProduct>>> GetAllForDataTable(DataTableParam param, string baseImageUrl);
        Task<BundledProduct> GetById(int id);
        Task<ProductAndCategoryModel> GetAllProductAndCategory(string productImagePath);
        Task<bool> BundledProductExists(int? Id, string titleEn, string titleAr);
        Task<BundledProduct> Create(BundledProduct model);
        Task<bool> Update(BundledProduct model);
        Task<bool> Delete(BundledProduct model);
        Task<bool> ToggleActive(int id);
        Task<BundledProduct> UpdateDisplayOrder(int id, int num = 0);
        #endregion
        
        
    }
}
