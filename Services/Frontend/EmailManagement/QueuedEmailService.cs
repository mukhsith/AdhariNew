using Data.EmailManagement;
using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Frontend.EmailManagement
{
    public class QueuedEmailService : IQueuedEmailService
    {
        protected readonly ApplicationDbContext _dbcontext;
        public QueuedEmailService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IList<QueuedEmail>> GetAll()
        {
            var data = await _dbcontext
                        .QueuedEmails
                        .Where(x => x.Deleted == false)
                        .ToListAsync();

            return data;
        }
        public async Task<QueuedEmail> CreateQueuedEmail(QueuedEmail model)
        {
            await _dbcontext.QueuedEmails.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
        public async Task<bool> UpdateQueuedEmail(QueuedEmail model)
        {
            _dbcontext.Update(model);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
    }
}
