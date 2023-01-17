using Data.EmailManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Frontend.EmailManagement
{
    public interface IQueuedEmailService 
    {
        Task<IList<QueuedEmail>> GetAll();
        Task<QueuedEmail> CreateQueuedEmail(QueuedEmail model);
        Task<bool> UpdateQueuedEmail(QueuedEmail model);
    }
}
