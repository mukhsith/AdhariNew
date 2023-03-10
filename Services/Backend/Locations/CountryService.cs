using Data.EntityFramework;
using Data.Locations;
using Microsoft.EntityFrameworkCore;
using Services.Base;
using Services.Backend.Locations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Backend.Locations
{
    public class CountryService : Repository<Country>, ICountryService
    {
        public CountryService(ApplicationDbContext dbcontext) : base(dbcontext) { }
        public async Task<IList<Country>> GetAll(bool showHidden = false)
        {
            var data = await _dbcontext
                        .Countries
                        .Where(x => x.Deleted == false)
                        .ToListAsync();

            if (!showHidden)
            {
                data = data.Where(a => a.Active).ToList();
            }

            return data;
        }
        public async Task<Country> GetDefaultCountry()
        {
            var data = await _dbcontext.Countries.Where(a => a.Default).FirstOrDefaultAsync();
            return data;
        }
        
    }
}
