using Data.Content;
using Data.SystemUserManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API; 

namespace Services.Backend.Content.Interface
{
    public interface IBannerService
    {
        #region Banner Service 
        Task<IEnumerable<Banner>> GetAll();
        Task<DataTableResult<List<Banner>>> GetAllForDataTable(DataTableParam param, string baseImageUrl);    
        Task<Banner> GetById(int id); 
        Task<Banner> Create(Banner model);
        Task<bool> Update(Banner model);
        Task<bool> Delete(Banner model);
        Task<bool> ToggleActive(int id);
        Task<Banner> UpdateDisplayOrder(int id, int num = 0);
        #endregion
        
        
    }
}
