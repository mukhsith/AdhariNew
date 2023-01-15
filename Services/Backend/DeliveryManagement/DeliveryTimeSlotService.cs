using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using System.Linq.Dynamic.Core;
using Services.Backend.DeliveryManagement.Interface;
using Data.DeliveryManagement;

namespace Services.Backend.Locations.Interface
{
    public class DeliveryTimeSlotService : IDeliveryTimeSlotService
    {
        protected readonly ApplicationDbContext _dbcontext;
        protected string ErrorMessage = string.Empty;
        public DeliveryTimeSlotService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region DeliveryTimeSlot Service 

        public async Task<IEnumerable<DeliveryTimeSlot>> GetAll()
        {
            IEnumerable<DeliveryTimeSlot> items = await _dbcontext
                                           .DeliveryTimeSlots
                                            .Select(x => new DeliveryTimeSlot
                                            {
                                                Id = x.Id,
                                                NameEn = x.NameEn,
                                                NameAr = x.NameAr,
                                                Active = x.Active,
                                                StartTime=x.StartTime,
                                                EndTime=x.EndTime,
                                                StartTimeOnly = x.GetEndTime(),
                                                EndTimeOnly = x.GetEndTime(),
                                                MaximumOrders=x.MaximumOrders, 
                                                CreatedOn = x.CreatedOn,
                                                DisplayOrder = x.DisplayOrder,
                                                Deleted = x.Deleted
                                            })  
                                           .AsNoTracking()
                                           .ToListAsync();
            //foreach(var item in items)
            //{


            //    //DateTime time = DateTime.Today.Add(item.StartTime).ToString("hh:mm tt");
            //    string displayTime = DateTime.Today.Add(item.StartTime).ToString("hh:mm tt"); // It will give "03:00 AM"
            //                                                    // var st = item.StartTime.ToString("hh:mm");

            //    var str = item.StartTime.ToString("hh:mm tt");
            //}
    return items;
}


       public async Task<DataTableResult<List<DeliveryTimeSlot>>> GetAllForDataTable(DataTableParam param)
        {
            DataTableResult<List<DeliveryTimeSlot>> result = new() { Draw = param.Draw };
            try
            {
                var items = _dbcontext.DeliveryTimeSlots
                         .Select(x => new DeliveryTimeSlot
                         {
                             Id = x.Id,
                             DisplayOrder = x.DisplayOrder,
                             StartTime = x.StartTime,
                             EndTime = x.EndTime, 
                             CreatedOn = x.CreatedOn,
                             Active = x.Active,
                             Deleted = x.Deleted
                         }).Where(x => x.Deleted == false);
                
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

     
        public async Task<DeliveryTimeSlot> GetById(int id)
        {
            var data = await _dbcontext.DeliveryTimeSlots.FindAsync(id);
            return data;
        }
        
        public async Task<DeliveryTimeSlot> Create(DeliveryTimeSlot model)
        {
            model.CreatedOn = DateTime.Now;
            await _dbcontext.DeliveryTimeSlots.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }

        public async Task<bool> Update(DeliveryTimeSlot model)
        {
            var updateData = await _dbcontext.DeliveryTimeSlots.FindAsync(model.Id);
            if (updateData is not null)
            {
                updateData.StartTime = model.StartTime;
                updateData.EndTime = model.EndTime;
                updateData.MaximumOrders = model.MaximumOrders;
                updateData.ModifiedOn = DateTime.Now;
            }
           _dbcontext.Update(updateData);
            return await _dbcontext.SaveChangesAsync()>0;
             
        }
        public async Task<bool> UpdateAll(List<DeliveryTimeSlot> timeSlots, int UserId)
        {
            var items = await _dbcontext.DeliveryTimeSlots.ToListAsync();
            if (items.Count == 0)
            {  foreach (var item in timeSlots)
                {
                    item.SetStartTime(item.StartTimeOnly);
                    item.SetEndTime(item.EndTimeOnly);
                    item.CreatedBy = UserId;
                    item.CreatedOn = DateTime.Now;
                    _dbcontext.DeliveryTimeSlots.Add(item);
                }
            }
            else
            {
                for (var index = 0; index < items.Count; index++)
                {
                    var newItem = timeSlots[index];
                    var foundItem = items.Find(x => x.DayId == newItem.DayId);
                    if (foundItem is not null)
                    {
                        foundItem.Active = newItem.Active;
                        foundItem.SetStartTime(newItem.StartTimeOnly);
                        foundItem.SetEndTime(newItem.EndTimeOnly);
                        foundItem.MaximumOrders = newItem.MaximumOrders;
                        foundItem.ModifiedBy = UserId;
                        foundItem.ModifiedOn = DateTime.Now;
                        _dbcontext.Update(foundItem);
                    }
                    else
                    {
                        newItem.CreatedBy = UserId;
                        newItem.CreatedOn = DateTime.Now;
                        newItem.SetStartTime(newItem.StartTimeOnly);
                        newItem.SetEndTime(newItem.EndTimeOnly);
                        _dbcontext.DeliveryTimeSlots.Add(newItem);
                    }

                }
                
                
            }
            
              return await _dbcontext.SaveChangesAsync() >0  ; 
        }
        public async Task<bool> Delete(DeliveryTimeSlot model)
        {
            var data = await _dbcontext.DeliveryTimeSlots.FindAsync(model.Id);
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
            var data = await _dbcontext.DeliveryTimeSlots.FindAsync(id);
            if (data is not null)
            {
                data.ModifiedOn = DateTime.Now;
                data.Active = !data.Active;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<DeliveryTimeSlot> UpdateDisplayOrder(int id, int num = 0)
        {
            var data = await _dbcontext.DeliveryTimeSlots.FindAsync(id);
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
