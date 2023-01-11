using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System; 
using System.Threading.Tasks; 
using Data.CouponPromotion;

namespace Services.Backend.CouponPromotion.Interface
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
            var promotion = await _dbcontext.Promotions.AsNoTracking().FirstOrDefaultAsync();
            if (promotion is null)
            {
                promotion = await Create(new Promotion());
            }
            return promotion;
        }

        public async Task<Promotion> Update(Promotion model)
        {
            var updateData = await _dbcontext.Promotions.FirstOrDefaultAsync();
            if (updateData is not null)
            {
                updateData.SignupEnabled = model.SignupEnabled;
                updateData.SignupCashbackValue = model.SignupCashbackValue;
                updateData.SignupCashbackValueExpiryInNoOfDays = model.SignupCashbackValueExpiryInNoOfDays;
                updateData.SignupFromDate = model.SignupFromDate;
                updateData.SignupToDate = model.SignupToDate;

                updateData.CashbackOnPurchaseEnabled = model.CashbackOnPurchaseEnabled;
                updateData.CashbackOnPurchaseMinOrderAmount = model.CashbackOnPurchaseMinOrderAmount;
                updateData.CashbackOnPurchaseValue = model.CashbackOnPurchaseValue;
                updateData.CashbackOnPurchaseExpiryInNoOfDays = model.CashbackOnPurchaseExpiryInNoOfDays; 
                updateData.CashbackOnPurchaseToDate = model.CashbackOnPurchaseToDate;
                updateData.CashbackOnPurchaseFromDate = model.CashbackOnPurchaseFromDate;

                updateData.CashbackRedeemEnabled = model.CashbackRedeemEnabled;
                updateData.CashbackRedeemMinOrderAmount = model.CashbackRedeemMinOrderAmount;
                updateData.CashbackRedeemMinWalletAmount = model.CashbackRedeemMinWalletAmount;
                updateData.CashbackValueToDeduct = model.CashbackValueToDeduct;
                updateData.ModifiedBy = model.ModifiedBy;
                updateData.ModifiedOn = DateTime.Now;
                _dbcontext.Update(updateData);
                await _dbcontext.SaveChangesAsync();
            }
            else
            {
                updateData = await Create(model);

            }
            return updateData;
        }
        private async Task<Promotion> Create(Promotion model)
        {
            model.CreatedOn = DateTime.Now;  
            await _dbcontext.Promotions.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
 
       

        #endregion

    }
}
