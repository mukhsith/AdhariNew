using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API;
using Data.DeliveryManagement;

namespace Services.Backend.DeliveryManagement.Interface
{

    public interface IDeliveryTimeSlotService
    {
        #region  Delivery TimeSolt Service
        Task<IEnumerable<DeliveryTimeSlot>> GetAll();
        Task<DataTableResult<List<DeliveryTimeSlot>>> GetAllForDataTable(DataTableParam param);
        Task<DeliveryTimeSlot> GetById(int id);
        Task<DeliveryTimeSlot> Create(DeliveryTimeSlot model);
        Task<bool> Update(DeliveryTimeSlot model);
        Task<bool> UpdateAll(List<DeliveryTimeSlot> deliveryTimeSlots, int UserId);
        Task<bool> Delete(DeliveryTimeSlot model);
        Task<bool> ToggleActive(int id);
        Task<DeliveryTimeSlot> UpdateDisplayOrder(int id, int num = 0);
        #endregion


    }
}
