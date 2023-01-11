using Data.Content;
using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Services.Frontend.Content.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Frontend.Content
{
    public class CompanySettingService : ICompanySettingService
    {
        protected readonly ApplicationDbContext _dbcontext;
        protected string ErrorMessage = string.Empty;
        public CompanySettingService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<CompanySetting> GetDefault()
        {
            var data = await _dbcontext
                        .CompanySettings
                        .AsNoTracking()
                        .FirstOrDefaultAsync();
            return data;
        }
    }
}
