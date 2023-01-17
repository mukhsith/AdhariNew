using Data.CouponPromotion;
using System.Threading.Tasks;

namespace Services.Frontend.CouponPromotion
{
    public interface IPromotionService
    {
        Task<Promotion> GetDefault(); 
    }
}
