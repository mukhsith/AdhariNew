using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Data.ProductManagement;

namespace Services.Frontend.ProductManagement
{
    public class CategoryService : ICategoryService
    {
        protected readonly ApplicationDbContext _dbcontext;
        public CategoryService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<List<Category>> GetAll()
        {
            var items = await _dbcontext.Categories
                                        .Where(x => !x.Deleted && x.Active)
                                        .OrderBy(x => x.DisplayOrder).ThenByDescending(x => x.Id)
                                        .AsNoTracking()
                                        .ToListAsync();
            return items;
        }
        public async Task<Category> GetById(int id)
        {
            var data = await _dbcontext.Categories.Where(a => a.Id == id).FirstOrDefaultAsync();
            return data;
        }
        public async Task<Category> GetBySeoName(string seoName)
        {
            var data = await _dbcontext.Categories.Where(a => a.SeoName == seoName).FirstOrDefaultAsync();
            return data;
        }
    }
}
