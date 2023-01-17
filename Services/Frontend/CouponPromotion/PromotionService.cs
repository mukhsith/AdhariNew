using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Data.CouponPromotion;

namespace Services.Frontend.CouponPromotion
{
    public class PromotionService : IPromotionService
    {
        protected readonly ApplicationDbContext _dbcontext;
        public PromotionService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<Promotion> GetDefault()
        {
            return await _dbcontext.Promotions.AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
