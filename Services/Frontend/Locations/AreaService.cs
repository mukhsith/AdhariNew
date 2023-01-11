using Data.CustomerManagement;
using Data.EntityFramework;
using Data.Locations;
using Microsoft.EntityFrameworkCore;
using Services.Backend.Locations.Interface;
using Services.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Frontend.Locations
{
    public class AreaService :  IAreaService
    {
        protected readonly ApplicationDbContext _dbcontext;
        public AreaService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IList<Area>> GetAll(bool showHidden = false, int? countryId = null)
        {
            var data = await _dbcontext
                        .Areas
                        .Where(x => x.Deleted == false)
                        .ToListAsync();

            if (!showHidden)
            {
                data = data.Where(a => a.Active).ToList();
            }

            if (countryId.HasValue)
            {
                data = data.Where(a => a.CountryId == countryId).ToList();
            }

            return data;
        }

        public async Task<Area> GetById(int id)
        {
            var data = await _dbcontext.Areas.Where(a => a.Id == id).Include(a => a.Country).FirstOrDefaultAsync();
            return data;
        }
    }
}
