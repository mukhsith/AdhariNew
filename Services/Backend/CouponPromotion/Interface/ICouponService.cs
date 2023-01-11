using Data.CouponPromotion;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API;  
namespace Services.Backend.CouponPromotion.Interface
{
    public interface ICouponService
    {
        #region Coupon Service
        Task<IEnumerable<Coupon>> GetAll();
        Task<bool> Exists(int? Id, string couponCode);
        Task<DataTableResult<List<Utility.Models.Frontend.CouponPromotion.CouponModel>>> GetAllForDataTable(DataTableParam param, 
            string couponCode = null, int? active = null, DateTime? createdOn = null);
        Task<Coupon> GetById(int id);
        Task<Coupon> Create(Coupon model);
        Task<bool> Update(Coupon model);
        Task<bool> Delete(Coupon model);
        Task<bool> ToggleActive(int id);
        Task<Coupon> UpdateDisplayOrder(int id, int num = 0);
        #endregion


    }
}
