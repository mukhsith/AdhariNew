using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using System.Linq.Dynamic.Core; 
using Data.CouponPromotion; 
namespace Services.Backend.CouponPromotion.Interface
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

        public async Task<IEnumerable<Coupon>> GetAll()
        {
            IEnumerable<Coupon> items = await _dbcontext
                                           .Coupons 
                                           .Where(x => x.Deleted == false)
                                           .AsNoTracking()
                                           .ToListAsync();
        return items;
        }
        public async Task<bool> Exists(int? Id, string couponCode)
        {

            var result = await _dbcontext
                                .Coupons
                                .Select(x => new { x.Id, x.CouponCode})
                                .Where(x => (x.CouponCode.ToLower() == couponCode.ToLower()))
                                .AsNoTracking()
                                .FirstOrDefaultAsync();
            if (result != null && Id.HasValue)
            {
                return result.Id != Id;
            }
            return false;

        }

        public async Task<DataTableResult<List<Utility.Models.Frontend.CouponPromotion.CouponModel>>> GetAllForDataTable(DataTableParam param, 
           string couponCode=null, int? active=null, DateTime? createdOn=null )
        {
            DataTableResult<List<Utility.Models.Frontend.CouponPromotion.CouponModel>> result = new() { Draw = param.Draw };
            try
            {

                var items = _dbcontext.Coupons
                         .Select(x => new Utility.Models.Frontend.CouponPromotion.CouponModel
                         {
                             Id = x.Id,
                             CouponCode = x.CouponCode,
                             StartDate = x.StartDate,
                             EndDate = x.EndDate,
                             DiscountType = x.DiscountType,
                             DiscountPercentage = x.DiscountPercentage,
                             DiscountAmount = x.DiscountAmount,
                             LimitUsageEnabled = x.LimitUsageEnabled,
                             Quantity = x.Quantity,
                             QuantityUsed = x.QuantityUsed,
                             Validity = x.Expired(),
                             CreatedOn = x.CreatedOn,
                             Active = x.Active,
                             Deleted = x.Deleted
                         }).Where(x => x.Deleted == false);

                if (!string.IsNullOrEmpty(couponCode))
                {
                    items = items.Where(x => x.CouponCode == couponCode);
                }
                if (active.HasValue) //if value is greater than 0 (true)else (false)
                {   
                    items = items.Where(x => x.Active ==  active.Value > 0 ); 
                }
                if (createdOn.HasValue)
                {
                    items = items.Where(x => x.CreatedOn.Date == createdOn.Value.Date);
                }
                //Sorting
                if (!string.IsNullOrEmpty(param.SortColumn) && !string.IsNullOrEmpty(param.SortColumnDirection))
                {
                    //using System.Linq.Dynamic.Core;
                    //NEEDS TO BE INSTALLED FROM NUGET PACKAGE MANAGER
                    items = items.OrderBy(param.SortColumn + " " + param.SortColumnDirection);//.ToList();
                }
                 
                result.RecordsTotal = items.Count();
                result.RecordsFiltered = items.Count();
                result.Data = await items.Skip(param.Skip).Take(param.PageSize).AsNoTracking().ToListAsync();
                
                return result;
            } catch (Exception err) {
                result.Error = err;
            }
            return result;
        }

     
        public async Task<Coupon> GetById(int id)
        {
            var data = await _dbcontext.Coupons.FindAsync(id);
            return data;
        }
        
        public async Task<Coupon> Create(Coupon model)
        {
            model.CreatedOn = DateTime.Now;
            model.DisplayOrder = await GetNextDisplayOrder();
            await _dbcontext.Coupons.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        } 
        private async Task<int> GetNextDisplayOrder()
        {
            var item = await _dbcontext.Coupons.OrderBy(x => x.DisplayOrder).AsNoTracking().LastAsync();
            if (item is not null)
            {
                return item.DisplayOrder + 1;
            }
            return 1;

        }
        public async Task<bool> Update(Coupon model)
        {
            var updateData = await _dbcontext.Coupons.FindAsync(model.Id);
            if (updateData is not null)
            {
                updateData.CouponCode = model.CouponCode;
                updateData.StartDate = model.StartDate;
                updateData.EndDate = model.EndDate;
                updateData.DiscountType = model.DiscountType;
                updateData.DiscountAmount = model.DiscountAmount;
                updateData.DiscountPercentage = model.DiscountPercentage;
                updateData.LimitUsageEnabled = model.LimitUsageEnabled;
                updateData.Active = model.Active;
                updateData.ModifiedOn = DateTime.Now;
            }
           _dbcontext.Update(updateData);
           return await _dbcontext.SaveChangesAsync() > 0;
              
        }
 
        public async Task<bool> Delete(Coupon model)
        {
            var data = await _dbcontext.Coupons.FindAsync(model.Id);
            if (data is not null)
            {
                data.ModifiedOn = DateTime.Now;
                data.Deleted = true;
                _dbcontext.Update(data);
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<bool> ToggleActive(int id)
        {
            var data = await _dbcontext.Coupons.FindAsync(id);
            if (data is not null)
            {
                data.ModifiedOn = DateTime.Now;
                data.Active = !data.Active;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<Coupon> UpdateDisplayOrder(int id, int num = 0)
        {
            var data = await _dbcontext.Coupons.FindAsync(id);
            if (data is not null)
            {
                data.DisplayOrder = num;
                data.ModifiedOn = DateTime.Now;
                await _dbcontext.SaveChangesAsync();
            }
            return data;
        }
        #endregion
       
    }
}
