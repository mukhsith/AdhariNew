using Data.EmailManagement;
using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Services.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Frontend.EmailManagement
{
    public class QueuedEmailService : Repository<QueuedEmail>, IQueuedEmailService
    {
        public QueuedEmailService(ApplicationDbContext dbcontext) : base(dbcontext) { }
        public async Task<IList<QueuedEmail>> GetAll()
        {
            var data = await _dbcontext
                        .QueuedEmails
                        .Where(x => x.Deleted == false)
                        .ToListAsync();

            return data;
        }
    }
}
