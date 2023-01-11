using Data.Content;
using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using System.Linq.Dynamic.Core;

namespace Services.Backend.Content.Interface
{
    public class CustomerFeedbackService : ICustomerFeedbackService
    {
        protected readonly ApplicationDbContext _dbcontext;
        protected string ErrorMessage = string.Empty;
        public CustomerFeedbackService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region CustomerFeedback 
         
       public async Task<DataTableResult<List<CustomerFeedback>>> GetAllForDataTable(DataTableParam param)
        {
            DataTableResult<List<CustomerFeedback>> result = new() { Draw = param.Draw };
            try
            {
                var items = _dbcontext.CustomerFeedbacks
                         .Select(x => new CustomerFeedback
                          {
                              Id = x.Id, 
                              Name = x.Name,
                              EmailAddress=x.EmailAddress,
                              MobileNumber = x.MobileNumber,
                              Subject = x.Subject,
                              Message = x.Message,
                              CreatedOn = x.CreatedOn, 
                              Status = x.Status,
                              Deleted = x.Deleted
                          }).Where(x => x.Deleted == false);
                //User Search
                if (!string.IsNullOrEmpty(param.SearchValue))
                {
                    var SearchValue = param.SearchValue.ToLower();
                    items = items.Where(obj =>
                     obj.Name.ToLower().Contains(SearchValue) ||
                     obj.EmailAddress.ToLower().Contains(SearchValue) ||
                     obj.Subject.ToLower().Contains(SearchValue) || 
                     obj.Message.ToLower().Contains(SearchValue) ||
                     obj.MobileNumber.Contains(SearchValue)
                     );
                }

                //Sorting
                if (!string.IsNullOrEmpty(param.SortColumn) && !string.IsNullOrEmpty(param.SortColumnDirection))
                {
                    //using System.Linq.Dynamic.Core;
                    //NEEDS TO BE INSTALLED FROM NUGET PACKAGE MANAGER
                    items = items.OrderBy(param.SortColumn + " " + param.SortColumnDirection);//.ToList();
                } else
                {
                    items = items.OrderByDescending(x => x.Id);
                }
                result.RecordsTotal = items.Count();
                result.RecordsFiltered = items.Count();
                result.Data = await items.Skip(param.Skip).Take(param.PageSize).ToListAsync();
                
                return result;
            } catch (Exception err) {
                result.Error = err;
            }
            return result;
        }

        
        public async Task<CustomerFeedback> GetById(int id)
        {
            var data = await _dbcontext.CustomerFeedbacks.FindAsync(id);
            return data;
        }
        
       
        public async Task<CustomerFeedback> UpdateStatus(CustomerFeedback item)
        {
            var data = await _dbcontext.CustomerFeedbacks.FindAsync(item.Id);
            if (data is not null)
            {
                data.ModifiedOn = DateTime.Now;
                data.Status = item.Status;
               await _dbcontext.SaveChangesAsync();
            }
            data.PendingNotificationBadgeCount = (await _dbcontext.CustomerFeedbacks.Where(x => x.Status == 0).AsNoTracking().ToListAsync()).Count;
            return data;
        }

        //public async Task<CustomerFeedback> UpdateStatus(int id, int status)
        //{
        //    var data = await _dbcontext.CustomerFeedbacks.FindAsync(id);
        //    if (data is not null)
        //    {
        //        data.ModifiedOn = DateTime.Now;
        //        data.Status = status;
        //        await _dbcontext.SaveChangesAsync();
        //    }
        //    return data;
        //}
        #endregion

    }
}
