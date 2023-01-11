using Data.CouponPromotion;
using Data.EntityFramework;
using Data.Sales;
using Microsoft.EntityFrameworkCore;
using Services.Frontend.CouponPromotion.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Enum;

namespace Services.Frontend.CouponPromotion
{
    public class WalletPackageService : IWalletPackageService
    {
        protected readonly ApplicationDbContext _dbcontext;
        protected string ErrorMessage = string.Empty;
        public WalletPackageService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region Wallet Package   
        public async Task<List<WalletPackage>> GetAllWalletPackage()
        {
            var data = _dbcontext
                           .WalletPackages
                           .Where(x => x.Deleted == false && x.Active)
                           .OrderBy(a => a.DisplayOrder).ThenByDescending(a => a.Id);

            return await data.AsNoTracking().ToListAsync();
        }
        public async Task<WalletPackage> GetWalletPackageById(int id)
        {
            var data = await _dbcontext.WalletPackages.FindAsync(id);
            return data;
        }
        #endregion

        #region Wallet Package Order
        public async Task<List<WalletPackageOrder>> GetAllWalletPackageOrder(int? customerId = null, PaymentStatus? paymentStatus = null)
        {
            var data = _dbcontext
                           .WalletPackageOrders
                           .Where(x => x.Deleted == false);

            if (customerId != null && customerId.Value > 0)
            {
                data = data.Where(a => a.CustomerId == customerId);
            }

            if (paymentStatus != null)
            {
                data = data.Where(a => a.PaymentStatusId == paymentStatus);
            }

            return await data.AsNoTracking().ToListAsync();
        }
        public async Task<WalletPackageOrder> GetWalletPackageOrderById(int id)
        {
            var data = await _dbcontext.WalletPackageOrders
                .Include(a => a.Customer)
                .Include(a => a.WalletPackage)
                .Include(a => a.PaymentMethod)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            return data;
        }
        public async Task<WalletPackageOrder> GetWalletPackageOrderByOrderNumber(string orderNumber)
        {
            var data = await _dbcontext.WalletPackageOrders
                .Include(a => a.Customer)
                .Include(a => a.WalletPackage)
                .Include(a => a.PaymentMethod)
                .Where(a => a.OrderNumber == orderNumber)
                .FirstOrDefaultAsync();

            return data;
        }
        public async Task<WalletPackageOrder> CreateWalletPackageOrder(WalletPackageOrder model)
        {
            await _dbcontext.WalletPackageOrders.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
        public async Task<bool> UpdateWalletPackageOrder(WalletPackageOrder model)
        {
            _dbcontext.Update(model);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteWalletPackageOrder(WalletPackageOrder model)
        {
            model.Deleted = true;
            return await UpdateWalletPackageOrder(model);
        }
        #endregion
    }
}
