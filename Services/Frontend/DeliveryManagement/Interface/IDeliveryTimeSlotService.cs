using System.Collections.Generic;
using System.Threading.Tasks;
using Data.DeliveryManagement;

namespace Services.Frontend.DeliveryManagement.Interface
{
    public interface IDeliveryTimeSlotService
    {
        Task<IList<DeliveryTimeSlot>> GetAll(int dayId = 0);
        Task<DeliveryTimeSlot> GetById(int id);
        Task<DeliveryTimeSlot> GetByDayId(int dayId);
    }
}
