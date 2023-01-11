using Data.EmailManagement;
using Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Frontend.EmailManagement
{
    public interface IQueuedEmailService : IRepository<QueuedEmail>
    {
        Task<IList<QueuedEmail>> GetAll();
    }
}
