using Data.CustomerManagement;
using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using System.Linq.Dynamic.Core;
using Utility.Enum;
using Utility.Models.Admin;
using Utility.Models.Admin.CustomerManagement;
using Utility.Models.Frontend.CustomerManagement;
using Utility.Helpers;
using Utility.Models.Admin.WalletPackage;

namespace Services.Backend.CustomerManagement
{
    public class CustomerService : ICustomerService
    {
        protected readonly ApplicationDbContext _dbcontext;
        public CustomerService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region Customer


        public async Task<Customer> GetCustomerById(int? id)
        {
            var data = await _dbcontext.Customers.Where(a => a.Id == id).Include(a => a.Country).AsNoTracking().FirstOrDefaultAsync();
            return data;
        }
        public async Task<List<AdminSmallCustomerModel>> GetAllForSMSNotification()
        {

            var items = await _dbcontext.Customers
                .Select(x => new AdminSmallCustomerModel() { Id = x.Id, Name = x.Name, MobileNumber = x.MobileNumber, Deleted = x.Deleted }).Where(a => a.Deleted == false).AsNoTracking().ToListAsync();
            return items;
        }
        public async Task<AdminCustomerModel> GetByMobileNumber(string mobileNumber)
        {
            AdminCustomerModel customer = new();
            var data = await _dbcontext.Customers.Where(a => !a.Deleted && a.MobileNumber.ToLower() == mobileNumber.ToLower()).AsNoTracking().FirstOrDefaultAsync();
            if (data is not null)
            {
                customer.Id = data.Id;
                customer.Name = data.Name;
                customer.MobileNumber = data.MobileNumber;
                customer.EmailAddress = data.EmailAddress;
                customer.B2B = data.B2B;
                //customer.Wallet = await GetWalletByCustomerId(customer.Id);
                var addresses = await _dbcontext.Addresses.Where(x => x.CustomerId == data.Id).AsNoTracking().ToListAsync();
                //foreach(var ad in addresses)
                //{
                //    customer.Addresses.Add(ad.Name + ad.Block + ad.Street + ad.Avenue + ad.Avenue);
                //}

            }
            return customer;
        }


        public async Task<AdminCustomerModel> GetById(int Id)
        {
            AdminCustomerModel customer = new();
            var data = await _dbcontext.Customers.Where(a => !a.Deleted && a.Id== Id).AsNoTracking().FirstOrDefaultAsync();
            if (data is not null)
            {
                customer.Id = data.Id;
                customer.Name = data.Name;
                customer.MobileNumber = data.MobileNumber;
                customer.EmailAddress = data.EmailAddress;
                customer.B2B = data.B2B;
                //customer.Wallet = await GetWalletByCustomerId(customer.Id);
                var addresses = await _dbcontext.Addresses.Where(x => x.CustomerId == data.Id).AsNoTracking().ToListAsync();
                //foreach(var ad in addresses)
                //{
                //    customer.Addresses.Add(ad.Name + ad.Block + ad.Street + ad.Avenue + ad.Avenue);
                //}

            }
            return customer;
        }

        public async Task<AdminCustomerModel> CreateCustomer(string name, string MobileNumber, string EmailAddress, bool b2b, int userId, int isEnglish)
        {
            var existingCustomer = await GetByMobileNumber(MobileNumber);
            if (existingCustomer == null)
            {
                var newCustomer = new Customer()
                {
                    Name = name,
                    MobileNumber = MobileNumber,
                    EmailAddress = EmailAddress,
                    B2B = b2b,
                    LanguageId = isEnglish,
                    CreatedBy = userId,
                    CreatedOn = DateTime.Now,
                    Active = true
                };
                await CreateCustomer(newCustomer);
                return await GetByMobileNumber(newCustomer.MobileNumber);
            }
            else
            {
                return existingCustomer;
            }
        }
        public async Task<Customer> CreateCustomer(Customer model)
        {
            //BKD default customer country Id is 1 (KUWAIT)
            model.CountryId = 1;
            await _dbcontext.Customers.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }

        public async Task<DataTableResult<dynamic>> GetAllForDataTable(DataTableParam param,
            string customerName = null, string customerMobile = null, string customerEmail = null, string? customerType = null)
        {
            DataTableResult<dynamic> result = new() { Draw = param.Draw };
            try
            {

                var items = _dbcontext.Customers
                         .Select(x => new AdminCustomerModel
                         {
                             Id = x.Id,
                             Name = x.Name,
                             MobileNumber = x.MobileNumber,
                             EmailAddress = x.EmailAddress,
                             B2B = x.B2B,
                             CreatedOn = x.CreatedOn,
                             Active = x.Active,
                             Deleted = x.Deleted
                         }).Where(x => x.Deleted == false);

                if (!string.IsNullOrEmpty(customerName))
                {
                    items = items.Where(x => x.Name == customerName);
                }
                if (!string.IsNullOrEmpty(customerMobile))
                {
                    items = items.Where(x => x.MobileNumber == customerMobile);
                }
                if (!string.IsNullOrEmpty(customerEmail))
                {
                    items = items.Where(x => x.EmailAddress == customerEmail);
                }
                if (customerType == "1")
                {

                    items = items.Where(x => x.B2B == true);
                }
                else if (customerType == "0")
                {
                    items = items.Where(x => x.B2B == false);
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
            }
            catch (Exception err)
            {
                result.Error = err;
            }
            return result;
        }

        public async Task<AdminCustomerModel> UpdateCustomerType(int userId, bool b2b)
        {
            var data = await _dbcontext.Customers.Where(a => a.Id == userId).FirstOrDefaultAsync();
            if (data is not null)
            {
                data.B2B = b2b;
                _dbcontext.Customers.Update(data);
                await _dbcontext.SaveChangesAsync();
                return new AdminCustomerModel() { Id = data.Id, B2B = data.B2B };
            }
            else
            {
                return null;
            }
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
        #endregion


        #region Customer address
        public async Task<IList<Address>> GetAllAddress(int customerId)
        {
            var data = _dbcontext
                           .Addresses
                           .Include(a => a.Area).ThenInclude(a => a.Governorate)
                           .Where(x => x.Deleted == false && x.CustomerId == customerId && !x.AddressId.HasValue);

            return await data.ToListAsync();
        }

        public async Task<Address> GetAddressById(int id)
        {
            var data = await _dbcontext.Addresses
                     .Include(a => a.Area).ThenInclude(a => a.Governorate)
                .Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            return data;
        }


        public async Task<bool> ToggleActive(int id)
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


        public async Task<Address> CreateAddress(Address model)
        {
            await _dbcontext.Addresses.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
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
        public async Task DeleteWalletTransaction(WalletTransaction walletTransaction)
        {
            walletTransaction.Deleted = true;
            _dbcontext.WalletTransactions.Update(walletTransaction);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task<List<WalletTransaction>> GetAllWalletTransaction(int customerId = 0, int walletTypeId = 0, int relatedEntityTypeId = 0,
            int relatedEntityId = 0)
        {
            var result = _dbcontext.WalletTransactions.Where(a => !a.Deleted);

            if (customerId > 0)
            {
                result = result.Where(a => a.CustomerId == customerId);
            }

            if (walletTypeId > 0)
            {
                result = result.Where(a => (int)a.WalletTypeId == walletTypeId);
            }

            if (relatedEntityTypeId > 0)
            {
                result = result.Where(a => (int)a.RelatedEntityTypeId == relatedEntityTypeId);
            }

            if (relatedEntityId > 0)
            {
                result = result.Where(a => a.RelatedEntityId == relatedEntityId);
            }

            return await result.ToListAsync();
        }

        public async Task<DataTableResult<List<WalletHistoryModel>>> GetCashbackHistoryForDataTable(DataTableParam param, int customerId)
        {
            DataTableResult<List<WalletHistoryModel>> result = new() { Draw = param.Draw };
            try
            {

                var items = _dbcontext.WalletTransactions
                    .Select(x => new WalletHistoryModel()
                    {
                        Id = x.Id,
                        CreatedOn = x.CreatedOn,
                        TransactionNo = x.TransactionNo,
                        WalletTransactionTypeId = x.WalletTransactionTypeId,
                        Credit = x.Credit,
                        Debit = x.Debit,
                        Deleted = x.Deleted,
                        WalletTypeId = x.WalletTypeId,
                        CustomerId = x.CustomerId
                    })
                    .Where(a => !a.Deleted && a.WalletTypeId == WalletType.Cashback);

                if (customerId > 0)
                {
                    items = items.Where(x => x.CustomerId == customerId);
                }

                items = items.OrderByDescending(a => a.Id);

                //Sorting
                if (!string.IsNullOrEmpty(param.SortColumn) && !string.IsNullOrEmpty(param.SortColumnDirection))
                {
                    items = items.OrderBy(param.SortColumn + " " + param.SortColumnDirection);
                }



                result.RecordsTotal = items.Count();
                result.RecordsFiltered = items.Count();
                result.Data = await items.Skip(param.Skip).Take(param.PageSize).AsNoTracking().ToListAsync();

                return result;
            }
            catch (Exception err)
            {
                result.Error = err;
            }
            return result;
        }
        public async Task<DataTableResult<List<WalletHistoryModel>>> GetWalletHistoryForDataTable(DataTableParam param, int customerId)
        {
            DataTableResult<List<WalletHistoryModel>> result = new() { Draw = param.Draw };
            try
            {

                var items = _dbcontext.WalletTransactions
                    .Select(x => new WalletHistoryModel()
                    {
                        Id = x.Id,
                        CreatedOn = x.CreatedOn,
                        TransactionNo = x.TransactionNo,
                        WalletTransactionTypeId = x.WalletTransactionTypeId,
                        Credit = x.Credit,
                        Debit = x.Debit,

                        Deleted = x.Deleted,
                        WalletTypeId = x.WalletTypeId,
                        CustomerId = x.CustomerId,
                    })
                    .Where(a => !a.Deleted && a.WalletTypeId == WalletType.Wallet && a.CustomerId == customerId)
                    .OrderBy(a => a.Id);


                ////Sorting
                //if (!string.IsNullOrEmpty(param.SortColumn) && !string.IsNullOrEmpty(param.SortColumnDirection))
                //{ 
                //    items = items.OrderBy(param.SortColumn + " " + param.SortColumnDirection); 
                //}

                result.RecordsTotal = items.Count();
                result.RecordsFiltered = items.Count();
                result.Data = await items.Skip(param.Skip).Take(param.PageSize).AsNoTracking().ToListAsync();
                decimal balance = 0;
                foreach (var item in result.Data)
                {

                    if (item.Credit > 0)
                    {
                        balance += item.Credit;
                    }
                    else
                    {
                        balance -= item.Debit;
                    }
                    item.Balance = balance;
                    if (item.WalletTransactionTypeId == WalletTransactionType.SignUpPromotion)
                    {
                        item.Description = param.IsEnglish ? Messages.SignUpBonus : MessagesAr.SignUpBonus;
                    }
                    else if (item.WalletTransactionTypeId == WalletTransactionType.CashbackOnPurchase)
                    {
                        item.Description = param.IsEnglish ? Messages.PurchaseBonus : MessagesAr.PurchaseBonus;
                    }
                    else if (item.WalletTransactionTypeId == WalletTransactionType.CashbackRedeem)
                    {
                        item.Description = param.IsEnglish ? Messages.CashbackRedeemOnPurchase : MessagesAr.CashbackRedeemOnPurchase;
                    }
                    else if (item.WalletTransactionTypeId == WalletTransactionType.UseWalletAmount)
                    {
                        item.Description = param.IsEnglish ? Messages.WalletRedeemOnPurchase : MessagesAr.WalletRedeemOnPurchase;
                    }
                    else if (item.WalletTransactionTypeId == WalletTransactionType.CashbackExpiry)
                    {
                        item.Description = param.IsEnglish ? Messages.Expired : MessagesAr.Expired;
                    }
                    //else if (item.WalletTransactionTypeId == WalletTransactionType.RefundWalletAmount)
                    //{
                    //    item.Description = param.IsEnglish ? Messages.RefundWalletAmount : MessagesAr.RefundWalletAmount;
                    //}
                    //else if (item.WalletTransactionTypeId == WalletTransactionType.RefundCashbackOnPurchase)
                    //{
                    //    item.Description = param.IsEnglish ? Messages.RefundCashbackOnPurchase : MessagesAr.RefundCashbackOnPurchase;
                    //}
                    //else if (item.WalletTransactionTypeId == WalletTransactionType.RefundOrderAmount)
                    //{
                    //    item.Description = param.IsEnglish ? Messages.RefundOrderAmount : MessagesAr.RefundOrderAmount;
                    //}
                    //else if (item.WalletTransactionTypeId == WalletTransactionType.RefundSubscriptionAmount)
                    //{
                    //    item.Description = param.IsEnglish ? Messages.RefundOrderAmount : MessagesAr.RefundOrderAmount;
                    //}
                }
                result.Data = result.Data.OrderByDescending(x => x.Id).ToList();
                return result;
            }
            catch (Exception err)
            {
                result.Error = err;
            }
            return result;
        }

        public decimal GetBalance(WalletHistoryModel walletHistoryModel)
        {
            return walletHistoryModel.Credit - walletHistoryModel.Debit;
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
        public async Task<decimal> GetWalletBalanceByCustomerId(int id, int? walletTransactionId = null, WalletType? walletTypeId = null)
        {
            var result = await _dbcontext.WalletTransactions.Where(a => !a.Deleted && a.CustomerId == id).ToListAsync();

            if (walletTransactionId != null)
            {
                result = result.Where(a => a.Id <= walletTransactionId.Value).ToList();
            }

            if (walletTypeId != null)
            {
                result = result.Where(a => a.WalletTypeId == walletTypeId).ToList();
            }

            var balance = result.Sum(a => a.Credit) - result.Sum(a => a.Debit);

            return balance;
        }

        public async Task<WalletModel> GetWalletByCustomerId(int id)
        {
            WalletModel walletModel = new();
            var result = await _dbcontext.WalletTransactions.Where(a => !a.Deleted && a.CustomerId == id).ToListAsync();
            if (result is not null)
            {
                walletModel.CashbackBalance = result.Where(x => x.WalletTypeId == WalletType.Cashback).Sum(a => a.Credit) - result.Sum(a => a.Debit);
                walletModel.WalletBalance = result.Where(x => x.WalletTypeId == WalletType.Wallet).Sum(a => a.Credit) - result.Sum(a => a.Debit);
            }
            return walletModel;
        }

        #endregion
    }
}
