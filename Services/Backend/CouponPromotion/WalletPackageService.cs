using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using System.Linq.Dynamic.Core; 
using Data.CouponPromotion;
using Data.Sales;
using Utility.Enum;
using Utility.Models.Admin.WalletPackage;
using Data.CustomerManagement;
using SkiaSharp;
using Utility.Models.Admin.Sales;

namespace Services.Backend.CouponPromotion.Interface
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

        public async Task<IEnumerable<WalletPackage>> GetAll()
        {
            IEnumerable<WalletPackage> items = await _dbcontext
                                           .WalletPackages
                                           .Where(x => x.Deleted == false)
                                           .AsNoTracking()
                                           .ToListAsync();
        return items;
        }
         public async Task<bool> Exists(int? Id, string titleEn, string titleAr)
        {

            var result = await _dbcontext
                                .WalletPackages
                                .Select(x => new { x.Id, x.NameEn, x.NameAr })
                                .Where(x => x.NameEn.ToLower() == titleEn.ToLower() ||
                                 x.NameAr.ToLower() == titleAr.ToLower())
                                .AsNoTracking()
                                .FirstOrDefaultAsync();
            if (result != null && Id.HasValue)
            {
                return result.Id != Id;
            }
            return false;

        }

        public async Task<DataTableResult<List<WalletPackage>>>  GetAllForDataTable(DataTableParam param )
        {
            DataTableResult<List<WalletPackage>> result = new() { Draw = param.Draw };
            try
            { 
                var items = _dbcontext.WalletPackages.Where(x => x.Deleted == false);
                 
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
            } catch (Exception err) {
                result.Error = err;
            }
            return result;
        }

     
        public async Task<WalletPackage> GetById(int id)
        {
            var data = await _dbcontext.WalletPackages.FindAsync(id);
            return data;
        }
        
        public async Task<WalletPackage> Create(WalletPackage model)
        {
            model.CreatedOn = DateTime.Now;
            model.DisplayOrder = await GetNextDisplayOrder();
            await _dbcontext.WalletPackages.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        } 
        private async Task<int> GetNextDisplayOrder()
        {
            var item = await _dbcontext.WalletPackages.OrderBy(x => x.DisplayOrder).AsNoTracking().LastAsync();
            if (item is not null)
            {
                return item.DisplayOrder + 1;
            }
            return 1;

        }
        public async Task<bool> Update(WalletPackage model)
        {
            var updateData = await _dbcontext.WalletPackages.FindAsync(model.Id);
            if (updateData is not null)
            {
                updateData.NameEn = model.NameEn;
                updateData.NameAr = model.NameAr;
                updateData.DescriptionEn = model.DescriptionEn;
                updateData.DescriptionAr = model.DescriptionAr;
                updateData.Amount = model.Amount;
                updateData.WalletAmount = model.WalletAmount; 
                updateData.ModifiedOn = DateTime.Now;
            
            _dbcontext.Update(updateData);
            return await _dbcontext.SaveChangesAsync() > 0;
           }
            return false;
        }
 
        public async Task<bool> Delete(WalletPackage model)
        {
            var data = await _dbcontext.WalletPackages.FindAsync(model.Id);
            if (data is not null)
            {
                data.ModifiedOn = DateTime.Now;
                data.Deleted = true;
                _dbcontext.Update(data);
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<bool> ToggleActive(int id)
        {
            var data = await _dbcontext.WalletPackages.FindAsync(id);
            if (data is not null)
            {
                data.ModifiedOn = DateTime.Now;
                data.Active = !data.Active;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<WalletPackage> UpdateDisplayOrder(int id, int num = 0)
        {
            var data = await _dbcontext.WalletPackages.FindAsync(id);
            if (data is not null)
            {
                data.DisplayOrder = num;
                data.ModifiedOn = DateTime.Now;
                await _dbcontext.SaveChangesAsync();
            }
            return data;
        }
        #endregion


        #region Wallet Package Order
        public async Task<DataTableResult<List<WalletPackageOrderModel>>> GetAllWalletPackageOrder(AdminWalletPackageOrderParam param)
        {
            DataTableResult<List<WalletPackageOrderModel>> result = new() { Draw = param.DatatableParam.Draw };
            try
            {
                var items =   _dbcontext.WalletPackageOrders
                                   .Include(a => a.Customer)
                                   .Include(a => a.WalletPackage)
                                   .Include(a => a.PaymentMethod)
                                   .Where(x => x.Deleted == false)
                                   .Select(x=> new WalletPackageOrderModel()
                                   {
                                     Id=x.Id,
                                     CreatedOn=x.CreatedOn,
                                     CustomerName= x.Customer.Name,
                                     MobileNumber= x.Customer.MobileNumber,
                                     CustomerEmail=x.Customer.EmailAddress,
                                     WalletPackageId= x.WalletPackage.Id,
                                     WalletPackageName= x.WalletPackage.NameEn,
                                     WalletPackageAmount= x.WalletPackage.Amount,
                                     PaymentMethodId= x.PaymentMethodId,
                                     PaymentMethodName =x.PaymentMethod.NameEn,
                                     PaymentId = x.PaymentId,
                                     PaymentStatusId=(int)x.PaymentStatusId,
                                   });
                if (param.SelectedTab == 0)
                {
                    items = items.Where(x => x.PaymentStatusId == (int)PaymentStatus.Captured); // paid
                } else
                {
                    items = items.Where(x => x.PaymentStatusId != (int)PaymentStatus.Captured); //unpiad
                }

                if (!string.IsNullOrEmpty(param.PaymentId))
                {
                    items = items.Where(a => a.PaymentId == param.PaymentId);
                }
                if (param.StartDate.HasValue && param.EndDate.HasValue)
                {
                    items = items.Where(a => a.CreatedOn.Date >= param.StartDate.Value.Date && a.CreatedOn.Date <= param.EndDate.Value.Date);
                }
                if (!string.IsNullOrEmpty(param.CustomerName))
                {
                    items = items.Where(a => a.CustomerName == param.CustomerName);
                }
                if (!string.IsNullOrEmpty(param.MobileNumber))
                {
                    items = items.Where(a => a.MobileNumber == param.MobileNumber);
                }
                if (!string.IsNullOrEmpty(param.CustomerEmail))
                {
                    items = items.Where(a => a.CustomerEmail == param.CustomerEmail);
                }
                if (param.PrepaidCardId.HasValue)
                {
                    items = items.Where(a => a.WalletPackageId == param.PrepaidCardId.Value);
                }
                if (param.PaymentMethodId.HasValue)
                {
                    items = items.Where(a => a.PaymentMethodId == param.PaymentMethodId.Value);
                }


                //Sorting
                if (!string.IsNullOrEmpty(param.DatatableParam.SortColumn) && !string.IsNullOrEmpty(param.DatatableParam.SortColumnDirection))
                {
                    //using System.Linq.Dynamic.Core;
                    //NEEDS TO BE INSTALLED FROM NUGET PACKAGE MANAGER
                    items = items.OrderBy(param.DatatableParam.SortColumn + " " + param.DatatableParam.SortColumnDirection);//.ToList();
                } else
                {
                    items = items.OrderByDescending(items=>items.Id);//.ToList();
                }

                result.RecordsTotal = await items.CountAsync();
                result.RecordsFiltered = await items.CountAsync();
                result.Data = await items.Skip(param.DatatableParam.Skip).Take(param.DatatableParam.PageSize).AsNoTracking().ToListAsync();

                return result;
            }
            catch (Exception err)
            {
                result.Error = err;
            }
            return result;
        }

        
        public async Task<WalletPackageOrderModel> GetWalletPackageOrderById(int id)
        {
            var data = await _dbcontext.WalletPackageOrders
                .Include(a => a.Customer)
                .Include(a => a.WalletPackage)
                .Include(a => a.PaymentMethod)
                .Where(a => a.Id == id)
                .Select(x => new WalletPackageOrderModel()
                {
                    Id = x.Id,
                    CreatedOn = x.CreatedOn,
                    CustomerName = x.Customer.Name,
                    MobileNumber = x.Customer.MobileNumber,
                    CustomerEmail = x.Customer.EmailAddress,
                    WalletPackageId = x.WalletPackageId,
                    WalletPackageName = x.WalletPackage.NameEn,
                    WalletPackageAmount = x.WalletPackage.Amount,
                    PaymentMethodId = x.PaymentMethodId,
                    PaymentMethodName = x.PaymentMethod.NameEn,
                    PaymentId = x.PaymentId
                })
                .FirstOrDefaultAsync();

            return data;
        } 

        public async Task<List<TopUpSale>> GetTopUpSale()
        {
            var groupData = await _dbcontext.WalletPackageOrders.Include(x => x.WalletPackage).Where(x=>x.Deleted==false)
                        .GroupBy(a=> new {name=a.WalletPackage.NameEn,a.Id,a.Deleted})
                        .Select(g => new TopUpSale()
                        {
                            Name = g.Key.name,
                            Total = g.Count()
                        })
                        .GroupBy(a=> new { a.Name, a.Total })
                        .Select(g=>new TopUpSale()
                        {
                            Name= g.Key.Name,
                            Total = g.Sum(o=>o.Total)
                        })
                        .AsNoTracking()
                        .ToListAsync();
            return groupData;
     }

       /* var myData = _context.ActivityItems
                        .GroupBy(a => new { ndt = EF.Property<DateTime>(a, "dt").Date, ntn = a.tn })
                        .Select(g => new
                        {
                            g.Key.ndt,
                            g.Key.ntn,
                            dpv = g.Sum(o => o.pv),
                            dlv = g.Sum(o => o.lv),
                            cnt = g.Count(),
                        })
                        .GroupBy(a => new { ntn = a.ntn })
                        .Select(g => new
                        {
                            g.Key.ntn,
                            sd = g.Min(o => o.ndt),
                            ld = g.Max(o => o.ndt),
                            pSum = g.Sum(o => o.dpv),
                            pMin = g.Min(o => o.dpv),
                            pMax = g.Max(o => o.dpv),
                            pAvg = g.Average(o => o.dpv),
                            lSum = g.Sum(o => o.dlv),
                            lMin = g.Min(o => o.dlv),
                            lMax = g.Max(o => o.dlv),
                            lAvg = g.Average(o => o.dlv),
                            n10s = g.Sum(o => o.cnt),
                            ndays = g.Count()
                        });
       */
        #endregion
    }
}
