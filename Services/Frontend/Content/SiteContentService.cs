using Data.Content;
using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Utility.Enum;

namespace Services.Frontend.Content
{
    public class SiteContentService : ISiteContentService
    {
        protected readonly ApplicationDbContext _dbcontext;
        public SiteContentService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<SiteContent> GetByType(AppContentType appContentType)
        {
            var data = await _dbcontext
                       .SiteContents
                       .Where(x => x.Deleted == false && x.AppContentType == appContentType)
                       .AsNoTracking()
                       .FirstOrDefaultAsync();

            if (data is not null)
            {
                data.Active = await GetDisplayWebControl(data);
            }

            return data;
        }
        private async Task<bool> GetDisplayWebControl(SiteContent item)
        {
            var webcontrol = await _dbcontext.DisplayWebControls.Where(x => x.ControlId == (int)item.AppContentType).AsNoTracking().FirstOrDefaultAsync();
            if (webcontrol is not null)
            {
                return webcontrol.Active;
            }
            return false;
        }
    }
}
