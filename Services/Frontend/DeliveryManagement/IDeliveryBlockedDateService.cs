using System.Collections.Generic;
using System.Threading.Tasks;
using Data.DeliveryManagement;

namespace Services.Frontend.DeliveryManagement
{

    public interface IDeliveryBlockedDateService
    {
        Task<IList<DeliveryBlockedDate>> GetAll();
        Task<DeliveryBlockedDate> GetById(int id);
    }
}
