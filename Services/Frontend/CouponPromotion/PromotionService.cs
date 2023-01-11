using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using System.Linq.Dynamic.Core;
using Data.ProductManagement;
using Data.CouponPromotion;

namespace Services.Frontend.CouponPromotion.Interface
{
    public class PromotionService : IPromotionService
    {
        protected readonly ApplicationDbContext _dbcontext;
        protected string ErrorMessage = string.Empty;
        public PromotionService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region Promotion 
         
        public async Task<Promotion> GetDefault()
        {
            return await _dbcontext.Promotions.AsNoTracking().FirstOrDefaultAsync();
            
        }
         
        #endregion

    }
}
