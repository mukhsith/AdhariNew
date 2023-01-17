using Data.Content;
using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Services.Frontend.Content
{
    public class ContactDetailService : IContactDetailService
    {
        protected readonly ApplicationDbContext _dbcontext;
        public ContactDetailService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<ContactDetail> GetDefault()
        {
            var data = await _dbcontext
                        .ContactDetails
                        .AsNoTracking()
                        .FirstOrDefaultAsync();
            return data;
        }
    }
}
