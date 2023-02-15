using Data.CustomerManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;
using Utility.Models.Admin;
using Utility.Models.Admin.CustomerManagement;
using Utility.Models.Admin.WalletPackage;
using Utility.Models.Frontend.CustomerManagement;

namespace Services.Backend.CustomerManagement
{
    public interface ICustomerService
    {
        #region Customer 
        Task<Customer> GetCustomerById(int? id);
        Task<List<AdminSmallCustomerModel>> GetAllForSMSNotification();

        Task<AdminCustomerModel> GetById(int Id);
        Task<AdminCustomerModel> GetByMobileNumber(string mobileNumber);
        Task<AdminCustomerModel> CreateCustomer(string name, string mobile, string email, bool b2b, int userid, int isEnglish);
        Task<Customer> CreateCustomer(Customer model);
        Task<DataTableResult<dynamic>> GetAllForDataTable(DataTableParam param,
          string customerName = null, string customerMobile = null, string customerEmail = null, string? customerType = null); 
        Task<AdminCustomerModel> UpdateCustomerType(int userId, bool b2b);

        Task<bool> UpdateCustomer(Customer model);
        Task<bool> DeleteCustomer(Customer model);

        #endregion

        #region Customer address 
         
        
        Task<IList<Address>> GetAllAddress(int customerId);
        
        Task<Address> GetAddressById(int id);

        Task<bool> ToggleActive(int id);
        Task<Address> CreateAddress(Address model);
        Task<bool> UpdateAddress(Address model);
        Task<bool> DeleteAddress(Address model);
        Task<bool> ToggleActiveAddress(int id);
        Task<bool> UpdateDisplayOrderAddress(int id, int num = 0);
        #endregion



        #region Wallet transactions
        Task<WalletTransaction> CreateWalletTransaction(WalletTransaction walletTransaction);
        Task DeleteWalletTransaction(WalletTransaction walletTransaction);
        Task<List<WalletTransaction>> GetAllWalletTransaction(int customerId = 0, int walletTypeId = 0, int relatedEntityTypeId = 0,
            int relatedEntityId = 0);
        Task<WalletTransaction> GetWalletTransactionById(int Id);
        Task UpdateWalletTransaction(WalletTransaction walletTransaction);
        Task<decimal> GetWalletBalanceByCustomerId(int id, int? walletTransactionId = null, WalletType? walletTypeId = null);
        Task<WalletModel> GetWalletByCustomerId(int id);
        #endregion

        #region
        Task<DataTableResult<List<WalletHistoryModel>>> GetCashbackHistoryForDataTable(DataTableParam param, int customerId);
        Task<DataTableResult<List<WalletHistoryModel>>> GetWalletHistoryForDataTable(DataTableParam param, int customerId);
        #endregion
    }
}
