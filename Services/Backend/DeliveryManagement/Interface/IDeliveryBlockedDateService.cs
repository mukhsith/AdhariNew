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
using Data.DeliveryManagement;

namespace Services.Backend.DeliveryManagement.Interface
{
     
    public interface IDeliveryBlockedDateService
    {
        #region  Blocked Date Service
        Task<IEnumerable<DeliveryBlockedDate>> GetAll();
        Task<DataTableResult<List<DeliveryBlockedDate>>> GetAllForDataTable(DataTableParam param);
        Task<DeliveryBlockedDate> GetById(int id);
        Task<DeliveryBlockedDate> Create(DeliveryBlockedDate model);
        Task<bool> Update(DeliveryBlockedDate model);
        Task<bool> Delete(DeliveryBlockedDate model);
        Task<bool> ToggleActive(int id,int SystemUserId);
       // Task<bool> ToggleBlocked(int id, int SystemUserId);
        Task<DeliveryBlockedDate> UpdateDisplayOrder(int id, int num = 0);
        #endregion


    }
}
