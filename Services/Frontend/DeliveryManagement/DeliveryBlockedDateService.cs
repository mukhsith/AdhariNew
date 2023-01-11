using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Services.Frontend.DeliveryManagement.Interface;
using Data.DeliveryManagement;

namespace Services.Frontend.Locations.Interface
{
    public class DeliveryBlockedDateService : IDeliveryBlockedDateService
    {
        protected readonly ApplicationDbContext _dbcontext;
        public DeliveryBlockedDateService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IList<DeliveryBlockedDate>> GetAll()
        {
            var data = await _dbcontext
                        .DeliveryBlockedDates
                        .Where(x => x.Deleted == false && x.Active)
                        .ToListAsync();

            return data;
        }
        public async Task<DeliveryBlockedDate> GetById(int id)
        {
            var data = await _dbcontext.DeliveryBlockedDates.FindAsync(id);
            return data;
        }
    }
}
