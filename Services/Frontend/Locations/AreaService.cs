using Data.EntityFramework;
using Data.Locations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Frontend.Locations
{
    public class AreaService : IAreaService
    {
        protected readonly ApplicationDbContext _dbcontext;
        public AreaService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IList<Area>> GetAll(bool showHidden = false, int? countryId = null)
        {
            var data = _dbcontext
                        .Areas
                        .Where(x => x.Deleted == false);

            if (!showHidden)
            {
                data = data.Where(a => a.Active);
            }

            if (countryId.HasValue)
            {
                data = data.Where(a => a.CountryId == countryId);
            }

            data = data.OrderBy(a => a.DisplayOrder).ThenByDescending(a => a.Id);

            return await data.ToListAsync();
        }
        public async Task<Area> GetById(int id)
        {
            var data = await _dbcontext.Areas.Where(a => a.Id == id).Include(a => a.Country).FirstOrDefaultAsync();
            return data;
        }
    }
}
