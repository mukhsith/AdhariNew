using Data.Content;
using Data.Locations;
using Data.ProductManagement;
using Data.SystemUserManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API; 

namespace Services.Backend.ProductManagement.Interface
{
    public interface IItemSizeService
    {
        #region Item Size Service 
        Task<IEnumerable<ItemSize>> GetAll();
        Task<DataTableResult<List<ItemSize>>> GetAllForDataTable(DataTableParam param);
        Task<ItemSize> GetById(int id);
        Task<ItemSize> Create(ItemSize model);
        Task<bool> Update(ItemSize model);
        Task<bool> Delete(ItemSize model);
        Task<bool> ToggleActive(int id);
        Task<ItemSize> UpdateDisplayOrder(int id, int num = 0);
        #endregion


    }
}
