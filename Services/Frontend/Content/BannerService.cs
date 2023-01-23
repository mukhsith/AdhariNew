using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Data.Content;

namespace Services.Frontend.Content
{
    public class BannerService : IBannerService
    {
        protected readonly ApplicationDbContext _dbcontext;
        public BannerService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<List<Banner>> GetAll()
        {
            var items = await _dbcontext.Banners
                                        .Where(x => !x.Deleted && x.Active)
                                        .Include(a => a.Product).ThenInclude(a => a.Category)
                                        .OrderBy(x => x.DisplayOrder).ThenByDescending(x => x.Id)
                                        .AsNoTracking()
                                        .ToListAsync();
            return items;
        }
    }
}
