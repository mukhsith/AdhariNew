using Data.EntityFramework;
using Data.Locations;
using Data.Sales;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Utility.API;
using System.Runtime.InteropServices.ComTypes;
using Utility.Models.Admin.Sales;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Utility.Enum;

namespace Services.Backend.Sales
{
    public class QuickPaymentService : IQuickPaymentService
    {
        protected readonly ApplicationDbContext _dbcontext;
        public QuickPaymentService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<QuickPayment> Create(QuickPayment model)
        {
            model.CreatedOn = DateTime.Now;
            await _dbcontext.QuickPayments.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }

        public async Task<List<QuickPayment>> GetPaymentById(PaymentRequestType type, int entityId)
        {
            var items = await _dbcontext.QuickPayments
                                 .Include(a => a.PaymentMethod)
                                 .Where(x => x.Deleted == false && x.PaymentRequestTypeId == type && x.EntityId == entityId)
                                 .AsNoTracking()
                                 .ToListAsync();
            // x.PaymentTrackId,
             //x.PaymentAuth,
               //                      x.PaymentMethodId,
            return items;
        }

        public async Task<dynamic> GetAllForDataTable(QuickPaymentParam param )
        {
            DataTableResult<dynamic> result = new() { Draw = param.DataTableParam.Draw };
            try
            {
                var items =    _dbcontext.QuickPayments
                                .Include(a => a.PaymentMethod) 
                                 .Select(x => new  
                                 {
                                     x.Id,
                                     x.CreatedOn,
                                     x.MobileNumber,
                                     x.Amount,
                                     x.PaymentResult,
                                     x.PaymentTrackId,
                                     x.PaymentAuth,
                                     x.PaymentId,
                                     x.PaymentMethodId,
                                     PaymentMethodName=x.PaymentMethod.NameEn,
                                     x.PaymentStatusId,
                                     x.Deleted
                                 }).Where(x => x.Deleted == false);
                if (param.SelectedTab == 0)
                {
                    items = items.Where(x => x.PaymentStatusId == Utility.Enum.PaymentStatus.Captured);
                } else
                {
                    items = items.Where(x => x.PaymentStatusId != Utility.Enum.PaymentStatus.Captured);
                }
                if (param.PaymentLinkId.HasValue)
                {
                    items = items.Where(a => a.Id == param.PaymentLinkId.Value);
                }
                if (!string.IsNullOrEmpty(param.CustomerMobile))
                {
                    items = items.Where(a => a.MobileNumber == param.CustomerMobile);
                }
                if (param.StartDate.HasValue)
                {
                    items = items.Where(a => a.CreatedOn.Date == param.StartDate.Value.Date);
                }
                if (param.EndDate.HasValue)
                {
                    items = items.Where(a => a.CreatedOn.Date == param.EndDate.Value.Date);
                }
                if (param.PaymentMethodId.HasValue)
                {
                    items = items.Where(a => a.PaymentMethodId == param.PaymentMethodId);
                }
                 
                //Sorting
                if (!string.IsNullOrEmpty(param.DataTableParam.SortColumn) && !string.IsNullOrEmpty(param.DataTableParam.SortColumnDirection))
                {
                    //using System.Linq.Dynamic.Core;
                    //NEEDS TO BE INSTALLED FROM NUGET PACKAGE MANAGER
                    items = items.OrderBy(param.DataTableParam.SortColumn + " " + param.DataTableParam.SortColumnDirection);//.ToList();
                }
                else
                {
                    items = items.OrderByDescending(x => x.CreatedOn);
                }
                result.RecordsTotal = await items.CountAsync();
                result.RecordsFiltered = await items.CountAsync();
                result.Data = await items.Skip(param.DataTableParam.Skip).Take(param.DataTableParam.PageSize).ToListAsync();

                return result;
            }
            catch (Exception err)
            {
                result.Error = err;
            }
            return result;
        }

        public async  Task<bool> Update(int id)
        {
            var data = await _dbcontext.QuickPayments.Where(a => a.Id == id).FirstOrDefaultAsync();
            if(data is not null)
            {
                 data.Used = true;
                _dbcontext.Update(data);
                return await _dbcontext.SaveChangesAsync() > 1;
            }

            return false;
        }


        public async Task<QuickPayment> GetqpayByNumber(string qpayNumber)
        {
            var data = await _dbcontext.QuickPayments
                .Where(a => a.PaymentNumber == qpayNumber)
                .FirstOrDefaultAsync();

            return data;
        }

    }
}
