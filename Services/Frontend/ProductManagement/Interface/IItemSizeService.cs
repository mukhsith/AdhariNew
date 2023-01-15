using Data.Content;
using Data.Locations;
using Data.ProductManagement;
using Data.SystemUserManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API; 

namespace Services.Frontend.ProductManagement.Interface
{
    public interface IItemSizeService
    {
        #region Item Size Service 
        Task<IEnumerable<ItemSize>> GetAll();
        Task<DataTableResult<List<ItemSize>>> GetAllForDataTable(DataTableParam param);
        Task<ItemSize> GetById(int id);
        
        #endregion


    }
}
