using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Data.CouponPromotion;

namespace Services.Frontend.CouponPromotion.Interface
{
    public class CouponService : ICouponService
    {
        protected readonly ApplicationDbContext _dbcontext;
        protected string ErrorMessage = string.Empty;
        public CouponService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region Coupon          
        public async Task<Coupon> GetById(int id)
        {
            var data = await _dbcontext.Coupons.FindAsync(id);
            return data;
        }
        public async Task<Coupon> GetByCode(string code)
        {
            var data = await _dbcontext.Coupons.Where(a => a.CouponCode.ToLower() == code.ToLower()).AsNoTracking().FirstOrDefaultAsync();
            return data;
        }
        public async Task<bool> UpdateCoupon(Coupon model)
        {
            _dbcontext.Update(model);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        #endregion
    }
}
