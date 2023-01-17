using Data.CouponPromotion;
using System.Threading.Tasks;

namespace Services.Frontend.CouponPromotion
{
    public interface ICouponService
    {
        Task<Coupon> GetById(int id);
        Task<Coupon> GetByCode(string code);
        Task<bool> UpdateCoupon(Coupon model);
    }
}
