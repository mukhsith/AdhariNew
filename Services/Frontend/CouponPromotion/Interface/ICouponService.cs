using Data.CouponPromotion;
using System.Threading.Tasks;

namespace Services.Frontend.CouponPromotion.Interface
{
    public interface ICouponService
    {
        #region Coupon Service 
        Task<Coupon> GetById(int id);
        Task<Coupon> GetByCode(string code);
        Task<bool> UpdateCoupon(Coupon model);
        #endregion
    }
}
