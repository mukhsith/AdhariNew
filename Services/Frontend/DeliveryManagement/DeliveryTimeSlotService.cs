using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Services.Frontend.DeliveryManagement;
using Data.DeliveryManagement;

namespace Services.Frontend.Locations
{
    public class DeliveryTimeSlotService : IDeliveryTimeSlotService
    {
        protected readonly ApplicationDbContext _dbcontext;
        public DeliveryTimeSlotService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IList<DeliveryTimeSlot>> GetAll(int dayId = 0)
        {
            var data = _dbcontext
                        .DeliveryTimeSlots
                        .Where(x => !x.Deleted && x.Active);

            if (dayId > 0)
            {
                data = data.Where(a => a.DayId == dayId);
            }

            return await data.ToListAsync();
        }
        public async Task<DeliveryTimeSlot> GetById(int id)
        {
            var data = await _dbcontext.DeliveryTimeSlots.FindAsync(id);
            return data;
        }
        public async Task<DeliveryTimeSlot> GetByDayId(int dayId)
        {
            var data = await _dbcontext.DeliveryTimeSlots.Where(a => a.DayId == dayId && !a.Deleted).FirstOrDefaultAsync();
            return data;
        }
    }
}
