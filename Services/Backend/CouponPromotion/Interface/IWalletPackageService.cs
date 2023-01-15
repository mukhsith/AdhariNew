using Data.CouponPromotion;
using Data.Sales;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;
using Utility.Models.Admin.Sales;
using Utility.Models.Admin.WalletPackage;

namespace Services.Backend.CouponPromotion.Interface
{
    public interface IWalletPackageService
    {
        #region Wallet Package Service
        Task<IEnumerable<WalletPackage>> GetAll();
        Task<bool> Exists(int? Id, string titleEn, string titleAr);
        Task<DataTableResult<List<WalletPackage>>> GetAllForDataTable(DataTableParam param);
        Task<WalletPackage> GetById(int id);
        Task<WalletPackage> Create(WalletPackage model);
        Task<bool> Update(WalletPackage model);
        Task<bool> Delete(WalletPackage model);
        Task<bool> ToggleActive(int id);
        Task<WalletPackage> UpdateDisplayOrder(int id, int num = 0);
        #endregion

        #region Wallet Package Order
        Task<DataTableResult<List<WalletPackageOrderModel>>> GetAllWalletPackageOrder(AdminWalletPackageOrderParam param);
        Task<WalletPackageOrderModel> GetWalletPackageOrderById(int id);
        Task<List<TopUpSale>> GetTopUpSale();
        #endregion
    }
}
