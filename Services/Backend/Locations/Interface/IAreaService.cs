using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using System.Linq.Dynamic.Core;
using Data.ProductManagement;
using Data.Locations;

namespace Services.Backend.Locations.Interface
{
     
    public interface IAreaService
    {
        #region   Area Service  
        Task<IEnumerable<Area>> GetAll();
        Task<DataTableResult<List<Area>>> GetAllForDataTable(DataTableParam param, int? governorateId=null);
        Task<bool> Exists(int? Id, string titleEn, string titleAr);
       
       Task<Area> GetById(int id);
        Task<Area> Create(Area model);
        Task<bool> Update(Area model);
        Task<bool> Delete(Area model);
        Task<bool> ToggleActive(int id);
        Task<Area> UpdateDisplayOrder(int id, int num = 0);
        #endregion


    }
}
