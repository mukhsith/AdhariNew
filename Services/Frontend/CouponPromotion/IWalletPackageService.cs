using Data.CouponPromotion;
using Data.Sales;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Enum;

namespace Services.Frontend.CouponPromotion
{
    public interface IWalletPackageService
    {
        #region Wallet Package   
        Task<List<WalletPackage>> GetAllWalletPackage();
        Task<WalletPackage> GetWalletPackageById(int id);
        #endregion

        #region Wallet Package Order
        Task<List<WalletPackageOrder>> GetAllWalletPackageOrder(int? customerId = null, PaymentStatus? paymentStatus = null);
        Task<WalletPackageOrder> GetWalletPackageOrderById(int id);
        Task<WalletPackageOrder> GetWalletPackageOrderByOrderNumber(string orderNumber);
        Task<WalletPackageOrder> CreateWalletPackageOrder(WalletPackageOrder model);
        Task<bool> UpdateWalletPackageOrder(WalletPackageOrder model);
        Task<bool> DeleteWalletPackageOrder(WalletPackageOrder model);
        #endregion
    }
}
