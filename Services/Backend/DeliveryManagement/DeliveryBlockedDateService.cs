using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using System.Linq.Dynamic.Core;
using Data.ProductManagement;
using Data.Locations;
using Services.Backend.DeliveryManagement.Interface;
using Data.DeliveryManagement;

namespace Services.Backend.Locations.Interface
{
    public class DeliveryBlockedDateService : IDeliveryBlockedDateService
    {
        protected readonly ApplicationDbContext _dbcontext;
        protected string ErrorMessage = string.Empty;
        public DeliveryBlockedDateService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region DeliveryBlockedDate Service 

        public async Task<IEnumerable<DeliveryBlockedDate>> GetAll()
        {
            IEnumerable<DeliveryBlockedDate> items = await _dbcontext
                                           .DeliveryBlockedDates
                                           .Where(x => x.Deleted == false)
                                           .AsNoTracking()
                                           .ToListAsync();
        return items;
        }


       public async Task<DataTableResult<List<DeliveryBlockedDate>>> GetAllForDataTable(DataTableParam param)
        {
            DataTableResult<List<DeliveryBlockedDate>> result = new() { Draw = param.Draw };
            //var itms = await _dbcontext.DeliveryBlockedDates
            //            .Include(x => x.CreatedByUser) 
            //            .Where(x => x.Deleted == false).ToListAsync();


            try


            {
                var items = _dbcontext.DeliveryBlockedDates
                        // .Include(x => x.ModifiedByUser)
                         .Select(x => new DeliveryBlockedDate
                         {
                             Id = x.Id,
                             DisplayOrder = x.DisplayOrder,
                             FromDate = x.FromDate,
                             ToDate = x.ToDate,
                             Note = x.Note,
                             // CreatedBy=x.CreatedBy,
                             // CreatedOn = x.CreatedOn,
                             //// CreatedByUserName = x.CreatedByUser!= null ? x.CreatedByUser.FullName : "", 
                             // ModifiedBy = x.ModifiedBy,
                             // ModifiedOn=x.ModifiedOn,
                             //  ModifiedByUserName = x.ModifiedByUser != null ? x.ModifiedByUser.FullName : "",
                             ModifiedByUserName = x.ModifiedByUser != null ? x.ModifiedByUser.FullName : "",
                             ModifiedOn = x.ModifiedOn,
                             Active = x.Active,
                             Deleted = x.Deleted
                         }).Where(x => x.Deleted == false);
                //User Search
                //if (!string.IsNullOrEmpty(param.SearchValue))
                //{
                //    var SearchValue = param.SearchValue.ToLower();
                //    items = items.Where(obj =>
                //     obj.CustomerName.ToLower().Contains(SearchValue) ||
                //     obj.CustomerEmail.ToLower().Contains(SearchValue) ||
                //     obj.MobileNumber.Contains(SearchValue)
                //     );
                //}

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
                
                return result;
            } catch (Exception err) {
                result.Error = err;
            }
            return result;
        }

     
        public async Task<DeliveryBlockedDate> GetById(int id)
        {
            var data = await _dbcontext.DeliveryBlockedDates.FindAsync(id);
            return data;
        }
        
        public async Task<DeliveryBlockedDate> Create(DeliveryBlockedDate model)
        {
            model.CreatedOn = DateTime.Now;
            await _dbcontext.DeliveryBlockedDates.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }

        public async Task<bool> Update(DeliveryBlockedDate model)
        {
            var updateData = await _dbcontext.DeliveryBlockedDates.FindAsync(model.Id);
            if (updateData is not null)
            {
                updateData.FromDate = model.FromDate;
                updateData.ToDate = model.ToDate;
                updateData.Note = model.Note;
                updateData.ModifiedBy = model.ModifiedBy;
                updateData.ModifiedOn = DateTime.Now;
            }
           _dbcontext.Update(updateData);
           return await _dbcontext.SaveChangesAsync() > 0;
              
        }
 
        public async Task<bool> Delete(DeliveryBlockedDate model)
        {
            var data = await _dbcontext.DeliveryBlockedDates.FindAsync(model.Id);
            if (data is not null)
            {
                data.ModifiedOn = DateTime.Now;
                data.Deleted = true;
                _dbcontext.Update(data);
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<bool> ToggleActive(int id, int SystemUserId)
        {
            var data = await _dbcontext.DeliveryBlockedDates.FindAsync(id);
            if (data is not null)
            {
                data.ModifiedBy = SystemUserId;
                data.ModifiedOn = DateTime.Now;
                data.Active = !data.Active;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }

        //public async Task<bool> ToggleBlocked(int id, int SystemUserId)
        //{
        //    var data = await _dbcontext.DeliveryBlockedDates.FindAsync(id);
        //    if (data is not null)
        //    {
        //        data.ModifiedOn = DateTime.Now;
        //        data.BlockedBy = SystemUserId;
        //        data.Active = !data.Active;
        //        return await _dbcontext.SaveChangesAsync() > 0;
        //    }
        //    return false;
        //}
        public async Task<DeliveryBlockedDate> UpdateDisplayOrder(int id, int num = 0)
        {
            var data = await _dbcontext.DeliveryBlockedDates.FindAsync(id);
            if (data is not null)
            {
                data.DisplayOrder = num;
                data.ModifiedOn = DateTime.Now;
                await _dbcontext.SaveChangesAsync();
            }
            return data;
        }
        #endregion
       
    }
}
