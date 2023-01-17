using Data.Content;
using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Services.Frontend.Content
{
    public class SocialMediaLinkService : ISocialMediaLinkService
    {
        protected readonly ApplicationDbContext _dbcontext;
        public SocialMediaLinkService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<SocialMediaLink> GetDefault()
        {
            var data = await _dbcontext.SocialMediaLinks.AsNoTracking().FirstOrDefaultAsync();
            return data;
        }
    }
}
