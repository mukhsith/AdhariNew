using Data.Content;
using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Services.Backend.Content.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;

namespace Services.Backend.Content
{
    public class PaymentMethodService : IPaymentMethodService
    {
        protected readonly ApplicationDbContext _dbcontext;
        protected string ErrorMessage = string.Empty;
        public PaymentMethodService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region Banner 
        public async Task<IList<Data.Content.PaymentMethod>> GetAllPaymentMethod(PaymentRequestType paymentRequestType)
        {
            var data = _dbcontext
                           .PaymentMethods
                           .Where(x => x.Deleted == false);

            if (paymentRequestType == PaymentRequestType.Order)
            {
                data = data.Where(a => a.NormalCheckoutRegisteredCustomer);
            }
            else if (paymentRequestType == PaymentRequestType.SubscriptionOrder)
            {
                data = data.Where(a => a.SubscriptionCheckoutRegisteredCustomer);
            }
            else if (paymentRequestType == PaymentRequestType.WalletPackageOrder)
            {
                data = data.Where(a => a.ForWalletPackage);
            }
            else if (paymentRequestType == PaymentRequestType.QuickPay)
            {
                data = data.Where(a => a.ForQuickPay);
            }

            data = data.OrderBy(a => a.DisplayOrder).ThenByDescending(a => a.Id);

            return await data.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Data.Content.PaymentMethod>> GetAll()
        {
            IEnumerable<Data.Content.PaymentMethod> items = await _dbcontext
                                           .PaymentMethods
                                           .Where(x => x.Deleted == false)
                                           .AsNoTracking()
                                           .ToListAsync();
            return items;
        }


        public async Task<DataTableResult<List<Data.Content.PaymentMethod>>> GetAllForDataTable(DataTableParam param, string imageBaseUrl)
        {
            DataTableResult<List<Data.Content.PaymentMethod>> result = new() { Draw = param.Draw };
            try
            {
                //ignore wallet row
                int walletId = 5; 
                var items = _dbcontext.PaymentMethods.Where(x => x.Deleted == false && x.Id!=walletId);
                
                

                //Sorting
                if (!string.IsNullOrEmpty(param.SortColumn) && !string.IsNullOrEmpty(param.SortColumnDirection))
                {
                    //using System.Linq.Dynamic.Core;
                    //NEEDS TO BE INSTALLED FROM NUGET PACKAGE MANAGER
                    items = items.OrderBy(param.SortColumn + " " + param.SortColumnDirection);//.ToList();
                }
                else
                {
                    items = items.OrderBy(x => x.DisplayOrder);
                }
                result.RecordsTotal = items.Count();
                result.RecordsFiltered = items.Count();
                result.Data = await items.Skip(param.Skip).Take(param.PageSize).ToListAsync();
                foreach(var item in result.Data)
                {
                    item.ImageUrl = imageBaseUrl + item.ImageName;
                }
                return result;
            }
            catch (Exception err)
            {
                result.Error = err;
            }
            return result;
        }


        public async Task<Data.Content.PaymentMethod> GetById(int id)
        {
            var data = await _dbcontext.PaymentMethods.FindAsync(id);
            return data;
        }

        public async Task<Data.Content.PaymentMethod> Create(Data.Content.PaymentMethod model)
        {
            model.CreatedOn = DateTime.Now;
            model.DisplayOrder = await GetNextDisplayOrder();
            await _dbcontext.PaymentMethods.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }

        private async Task<int> GetNextDisplayOrder()
        {
            var item = await _dbcontext.PaymentMethods.OrderBy(x => x.DisplayOrder).AsNoTracking().LastAsync();
            if (item is not null)
            {
                return item.DisplayOrder + 1;
            }
            return 1;

        }
        public async Task<bool> Update(Data.Content.PaymentMethod model)
        {
            var updateData = await _dbcontext.PaymentMethods.FindAsync(model.Id);
            if (updateData is not null)
            {
                   
     
                if (!string.IsNullOrEmpty(model.ImageName))
                { updateData.ImageName = model.ImageName; }

                 
                updateData.NameEn = model.NameEn;
                updateData.NameAr = model.NameAr;
                updateData.NormalCheckoutRegisteredCustomer = model.NormalCheckoutRegisteredCustomer;
                updateData.NormalCheckoutGuestCustomer = model.NormalCheckoutGuestCustomer;
                updateData.SubscriptionCheckoutRegisteredCustomer = model.SubscriptionCheckoutRegisteredCustomer;
                updateData.SubscriptionCheckoutGuestCustomer = model.SubscriptionCheckoutGuestCustomer;
                updateData.ModifiedBy = model.ModifiedBy;
                updateData.ModifiedOn = DateTime.Now;
            }
            _dbcontext.Update(updateData);
            return await _dbcontext.SaveChangesAsync() > 0;

        }

        
        public async Task<bool> ToggleNormalRegistered(int id)
        {
            var data = await _dbcontext.PaymentMethods.FindAsync(id);
            if (data is not null)
            {
                data.ModifiedOn = DateTime.Now;
                data.NormalCheckoutRegisteredCustomer = !data.Active;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<bool> ToggleSubscriptionRegistered(int id)
        {
            var data = await _dbcontext.PaymentMethods.FindAsync(id);
            if (data is not null)
            {
                data.ModifiedOn = DateTime.Now;
                data.SubscriptionCheckoutRegisteredCustomer = !data.Active;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
       
        #endregion

         
        public async Task<IList<Data.Content.PaymentMethod>> GetAllPaymentMethod(RelatedEntityType relatedEntityType, bool guest)
        {
            var data = _dbcontext.PaymentMethods.Where(x => x.Deleted == false);

            if (relatedEntityType == RelatedEntityType.Order)
            {
                data = data.Where(a => a.NormalCheckoutGuestCustomer && a.NormalCheckoutRegisteredCustomer);

                if (guest)
                    data = data.Where(a => a.NormalCheckoutGuestCustomer);
                else
                    data = data.Where(a => a.NormalCheckoutRegisteredCustomer);
            }
            else if (relatedEntityType == RelatedEntityType.Subscription)
            {
                data = data.Where(a => a.SubscriptionCheckoutGuestCustomer && a.SubscriptionCheckoutRegisteredCustomer);

                if (guest)
                    data = data.Where(a => a.SubscriptionCheckoutGuestCustomer);
                else
                    data = data.Where(a => a.SubscriptionCheckoutRegisteredCustomer);
            }

            data = data.OrderBy(a => a.DisplayOrder).ThenByDescending(a => a.Id);

            return await data.AsNoTracking().ToListAsync();
        }
        public async Task<Data.Content.PaymentMethod> GetPaymentMethodById(int id)
        {
            var data = await _dbcontext.PaymentMethods.Where(a => a.Id == id).FirstOrDefaultAsync();
            return data;
        }
    }
}
