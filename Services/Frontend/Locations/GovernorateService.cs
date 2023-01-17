using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Data.Locations;

namespace Services.Frontend.Locations
{
    public class GovernorateService : IGovernorateService
    {
        protected readonly ApplicationDbContext _dbcontext;
        public GovernorateService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IEnumerable<Governorate>> GetAll()
        {
            IEnumerable<Governorate> items = await _dbcontext
                                           .Governorates
                                           .Where(x => x.Deleted == false)
                                           .AsNoTracking()
                                           .OrderBy(a => a.DisplayOrder).ThenByDescending(a => a.Id)
                                           .ToListAsync();
            return items;
        }
        public async Task<Governorate> GetById(int id)
        {
            var data = await _dbcontext.Governorates.FindAsync(id);
            return data;
        }
    }
}
