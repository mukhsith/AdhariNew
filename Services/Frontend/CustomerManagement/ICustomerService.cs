using Data.CustomerManagement;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Enum;

namespace Services.Frontend.CustomerManagement
{
    public interface ICustomerService
    {
        #region Customer
        Task<IList<Customer>> GetAllCustomer(bool showHidden = false);
        Task<Customer> GetCustomerById(int id);
        Task<Customer> GetCustomerByEmailAddress(string emailAddress, int? id = null);
        Task<Customer> GetCustomerByMobileNumber(string mobileNumber, int? id = null);
        Task<Customer> GetCustomerByRequestPasswordChangeGuid(string requestPasswordChangeGuid);
        Task<Customer> CreateCustomer(Customer model);
        Task<bool> UpdateCustomer(Customer model);
        Task<bool> DeleteCustomer(Customer model);
        Task<bool> ToggleActiveCustomer(int id);
        Task<bool> UpdateDisplayOrderCustomer(int id, int num = 0);
        #endregion

        #region Customer register request        
        Task<CustomerRegisterRequest> GetCustomerRegisterRequestById(int id);
        Task<CustomerRegisterRequest> GetCustomerRegisterRequestByEmailAddress(string emailAddress);
        Task<CustomerRegisterRequest> GetCustomerRegisterRequestByMobileNumber(string mobileNumber);
        Task<CustomerRegisterRequest> CreateCustomerRegisterRequest(CustomerRegisterRequest model);
        #endregion

        #region Customer address
        Task<IList<Address>> GetAllAddress(int? customerId = null);
        Task<Address> GetAddressById(int id);
        Task<Address> CreateAddress(Address model);
        Task<bool> UpdateAddress(Address model);
        Task<bool> DeleteAddress(Address model);
        Task<bool> ToggleActiveAddress(int id);
        Task<bool> UpdateDisplayOrderAddress(int id, int num = 0);
        #endregion

        #region Wallet transactions
        Task<WalletTransaction> CreateWalletTransaction(WalletTransaction walletTransaction);
        Task CreateWalletTransactions(List<WalletTransaction> walletTransactions);
        Task DeleteWalletTransaction(WalletTransaction walletTransaction);
        Task<List<WalletTransaction>> GetAllWalletTransaction(int customerId = 0, WalletType? walletType = null, int relatedEntityTypeId = 0,
              int relatedEntityId = 0, bool forRedeem = false);
        Task<WalletTransaction> GetWalletTransactionById(int Id);
        Task UpdateWalletTransaction(WalletTransaction walletTransaction);
        Task UpdateWalletTransactions(List<WalletTransaction> walletTransactions);
        Task<decimal> GetWalletBalanceByCustomerId(int id, WalletType walletTypeId);
        Task<List<WalletTransaction>> GetAllExpiredWalletTransaction(int customerId = 0, WalletType? walletType = null);
        #endregion
    }
}
