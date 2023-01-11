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

namespace Services.Frontend.Locations.Interface
{
     
    public interface IGovernorateService
    {
        #region Item Size Service 
        Task<IEnumerable<Governorate>> GetAll();
        Task<DataTableResult<List<Governorate>>> GetAllForDataTable(DataTableParam param);
        Task<bool>  Exists(int? Id, string titleEn, string titleAr);
        Task<Governorate> GetById(int id);
        Task<Governorate> Create(Governorate model);
        Task<bool> Update(Governorate model);
        Task<bool> Delete(Governorate model);
        Task<bool> ToggleActive(int id);
        Task<Governorate> UpdateDisplayOrder(int id, int num = 0);
        #endregion


    }
}
