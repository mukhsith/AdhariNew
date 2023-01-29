using Data.CustomerManagement;
using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Enum;

namespace Services.Frontend.CustomerManagement
{
    public class CustomerService : ICustomerService
    {
        protected readonly ApplicationDbContext _dbcontext;
        public CustomerService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region Customer
        public async Task<IList<Customer>> GetAllCustomer(bool showHidden = false)
        {
            var data = await _dbcontext
                           .Customers
                           .Where(x => x.Deleted == false)
                           .AsNoTracking()
                           .ToListAsync();

            if (!showHidden)
            {
                data = data.Where(a => a.Active).ToList();
            }

            return data;
        }
        public async Task<Customer> GetCustomerById(int id)
        {
            var data = await _dbcontext.Customers.Where(a => a.Id == id).Include(a => a.Country).FirstOrDefaultAsync();
            return data;
        }
        public async Task<Customer> GetCustomerByEmailAddress(string emailAddress, int? id = null)
        {
            var data = _dbcontext.Customers.Include(a => a.Country).Where(a => !a.Deleted && a.EmailAddress.ToLower() == emailAddress.ToLower());

            if (id != null && id.Value > 0)
            {
                data = data.Where(a => a.Id != id.Value);
            }

            return await data.FirstOrDefaultAsync();
        }
        public async Task<Customer> GetCustomerByMobileNumber(string mobileNumber, int? id = null)
        {
            var data = _dbcontext.Customers.Include(a => a.Country).Where(a => !a.Deleted && a.MobileNumber == mobileNumber);

            if (id != null && id.Value > 0)
            {
                data = data.Where(a => a.Id != id.Value);
            }

            return await data.FirstOrDefaultAsync();
        }
        public async Task<Customer> GetCustomerByRequestPasswordChangeGuid(string requestPasswordChangeGuid)
        {
            var data = await _dbcontext.Customers.Where(a => a.RequestPasswordChangeGuid.ToLower() == requestPasswordChangeGuid.ToLower()).Include(a => a.Country).FirstOrDefaultAsync();
            return data;
        }
        public async Task<Customer> CreateCustomer(Customer model)
        {
            //BKD default customer country Id is 1 (KUWAIT)
            model.CountryId = 1;
            await _dbcontext.Customers.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
        public async Task<bool> UpdateCustomer(Customer model)
        {
            _dbcontext.Update(model);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteCustomer(Customer model)
        {
            model.Deleted = true;
            return await UpdateCustomer(model);
        }
        public async Task<bool> ToggleActiveCustomer(int id)
        {
            var data = await _dbcontext.Customers.FindAsync(id);
            if (data is not null)
            {
                data.ModifiedOn = DateTime.Now;
                data.Active = !data.Active;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<bool> UpdateDisplayOrderCustomer(int id, int num = 0)
        {
            var data = await _dbcontext.Customers.FindAsync(id);
            if (data is not null)
            {
                data.DisplayOrder = num;
                data.ModifiedOn = DateTime.Now;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        #endregion

        #region Customer register request        
        public async Task<CustomerRegisterRequest> GetCustomerRegisterRequestById(int id)
        {
            var data = await _dbcontext.CustomerRegisterRequests.FindAsync(id);
            return data;
        }
        public async Task<CustomerRegisterRequest> GetCustomerRegisterRequestByEmailAddress(string emailAddress)
        {
            var data = await _dbcontext.CustomerRegisterRequests.Where(a => a.EmailAddress.ToLower() == emailAddress.ToLower()).FirstOrDefaultAsync();
            return data;
        }
        public async Task<CustomerRegisterRequest> GetCustomerRegisterRequestByMobileNumber(string mobileNumber)
        {
            var data = await _dbcontext.CustomerRegisterRequests.Where(a => a.MobileNumber.ToLower() == mobileNumber.ToLower()).FirstOrDefaultAsync();
            return data;
        }
        public async Task<CustomerRegisterRequest> CreateCustomerRegisterRequest(CustomerRegisterRequest model)
        {
            await _dbcontext.CustomerRegisterRequests.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
        #endregion

        #region Customer address
        public async Task<IList<Address>> GetAllAddress(int? customerId = null)
        {
            var data = _dbcontext
                           .Addresses
                           .Include(a => a.Area).ThenInclude(a => a.Governorate)
                           .Where(x => x.Deleted == false && !x.AddressId.HasValue);

            if (customerId != null)
            {
                data = data.Where(a => a.CustomerId == customerId.Value);
            }

            return await data.ToListAsync();
        }
        public async Task<Address> GetAddressById(int id)
        {
            var data = await _dbcontext.Addresses.Include(a => a.Area).ThenInclude(a => a.Governorate).Where(a => a.Id == id).FirstOrDefaultAsync();
            return data;
        }
        public async Task<Address> CreateAddress(Address model)
        {
            await _dbcontext.Addresses.AddAsync(model);
            await _dbcontext.SaveChangesAsync();

            model = await _dbcontext.Addresses.Include(a => a.Area).ThenInclude(a => a.Governorate).Where(a => a.Id == model.Id).FirstOrDefaultAsync();
            return model;
        }
        public async Task<bool> UpdateAddress(Address model)
        {
            _dbcontext.Update(model);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteAddress(Address model)
        {
            model.Deleted = true;
            return await UpdateAddress(model);
        }
        public async Task<bool> ToggleActiveAddress(int id)
        {
            var data = await _dbcontext.Addresses.FindAsync(id);
            if (data is not null)
            {
                data.ModifiedOn = DateTime.Now;
                data.Active = !data.Active;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<bool> UpdateDisplayOrderAddress(int id, int num = 0)
        {
            var data = await _dbcontext.Addresses.FindAsync(id);
            if (data is not null)
            {
                data.DisplayOrder = num;
                data.ModifiedOn = DateTime.Now;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        #endregion

        #region Wallet transactions
        public async Task<WalletTransaction> CreateWalletTransaction(WalletTransaction walletTransaction)
        {
            await _dbcontext.WalletTransactions.AddAsync(walletTransaction);
            await _dbcontext.SaveChangesAsync();

            var result = await _dbcontext.WalletTransactions.FirstOrDefaultAsync(x => x.Id == walletTransaction.Id);
            return result;
        }
        public async Task CreateWalletTransactions(List<WalletTransaction> walletTransactions)
        {
            await _dbcontext.WalletTransactions.AddRangeAsync(walletTransactions);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task DeleteWalletTransaction(WalletTransaction walletTransaction)
        {
            walletTransaction.Deleted = true;
            _dbcontext.WalletTransactions.Update(walletTransaction);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task<List<WalletTransaction>> GetAllWalletTransaction(int customerId = 0, WalletType? walletType = null, int relatedEntityTypeId = 0,
            int relatedEntityId = 0, bool forRedeem = false)
        {
            var result = _dbcontext.WalletTransactions.Where(a => !a.Deleted);

            if (customerId > 0)
            {
                result = result.Where(a => a.CustomerId == customerId);
            }

            if (walletType != null)
            {
                result = result.Where(a => a.WalletTypeId == walletType);

                if (forRedeem)
                {
                    result = result.Where(a => (!a.ExpiryDate.HasValue || DateTime.Now.Date <= a.ExpiryDate.Value.Date) && a.RemainingCredit > 0);
                }
            }

            if (relatedEntityTypeId > 0)
            {
                result = result.Where(a => (int)a.RelatedEntityTypeId == relatedEntityTypeId);
            }

            if (relatedEntityId > 0)
            {
                result = result.Where(a => a.RelatedEntityId == relatedEntityId);
            }

            if (forRedeem)
            {
                result = result.OrderBy(a => a.Id);
            }

            return await result.ToListAsync();
        }
        public async Task<WalletTransaction> GetWalletTransactionById(int Id)
        {
            var result = await _dbcontext.WalletTransactions.FirstOrDefaultAsync(x => x.Id == Id);
            return result;
        }
        public async Task UpdateWalletTransaction(WalletTransaction walletTransaction)
        {
            _dbcontext.WalletTransactions.Update(walletTransaction);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task UpdateWalletTransactions(List<WalletTransaction> walletTransactions)
        {
            _dbcontext.WalletTransactions.UpdateRange(walletTransactions);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task<decimal> GetWalletBalanceByCustomerId(int id, WalletType walletTypeId)
        {
            var result = _dbcontext.WalletTransactions.Where(a => !a.Deleted && a.CustomerId == id && a.WalletTypeId == walletTypeId &&
                               (!a.ExpiryDate.HasValue || DateTime.Now.Date <= a.ExpiryDate.Value.Date));

            decimal balance = 0;
            if (walletTypeId == WalletType.Cashback)
            {
                result = result.Where(a => a.RemainingCredit > 0);
                balance = await result.SumAsync(a => a.RemainingCredit);
            }
            else
            {
                balance = (await result.SumAsync(a => a.Credit)) - (await result.SumAsync(a => a.Debit));
            }

            return balance;
        }
        public async Task<List<WalletTransaction>> GetAllExpiredWalletTransaction(int customerId = 0, WalletType? walletType = null)
        {
            var result = _dbcontext.WalletTransactions.Where(a => !a.Deleted && a.ExpiryDate.HasValue &&
            a.ExpiryDate.Value.Date < DateTime.Now.Date && a.RemainingCredit > 0 && !a.ExpiredEntryAdded);

            if (customerId > 0)
            {
                result = result.Where(a => a.CustomerId == customerId);
            }

            if (walletType != null)
            {
                result = result.Where(a => a.WalletTypeId == walletType);
            }

            return await result.ToListAsync();
        }
        #endregion
    }
}
