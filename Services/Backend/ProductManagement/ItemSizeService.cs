using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using System.Linq.Dynamic.Core;
using Data.ProductManagement;

namespace Services.Backend.ProductManagement.Interface
{
    public class ItemSizeService : IItemSizeService
    {
        protected readonly ApplicationDbContext _dbcontext;
        protected string ErrorMessage = string.Empty;
        public ItemSizeService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region ItemSize 

        public async Task<IEnumerable<ItemSize>> GetAll()
        {
            IEnumerable<ItemSize> items = await _dbcontext
                                           .ItemSizes 
                                           .Where(x => x.Deleted == false)
                                           .AsNoTracking()
                                           .ToListAsync();
return items;
}


       public async Task<DataTableResult<List<ItemSize>>> GetAllForDataTable(DataTableParam param)
        {
            DataTableResult<List<ItemSize>> result = new() { Draw = param.Draw };
            try
            {
                var items = _dbcontext.ItemSizes
                         .Select(x => new ItemSize
                          {
                              Id = x.Id,
                              DisplayOrder=x.DisplayOrder,
                              NameEn=x.NameEn,
                              NameAr=x.NameAr,
                              CreatedOn = x.CreatedOn,
                              Active = x.Active,
                              Deleted = x.Deleted
                          }).Where(x => x.Deleted == false);
                //User Search

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

     
        public async Task<ItemSize> GetById(int id)
        {
            var data = await _dbcontext.ItemSizes.FindAsync(id);
            return data;
        }
        
        public async Task<ItemSize> Create(ItemSize model)
        {
            model.CreatedOn = DateTime.Now;
            model.DisplayOrder = await GetNextDisplayOrder(); 
            await _dbcontext.ItemSizes.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
        
        private async Task<int> GetNextDisplayOrder()
        {
            var item = await _dbcontext.ItemSizes.OrderBy(x => x.DisplayOrder).AsNoTracking().LastAsync();
            if (item is not null)
            {
                return item.DisplayOrder + 1;
            }
            return 1;

        }
        public async Task<bool> Update(ItemSize model)
        {
            var updateData = await _dbcontext.ItemSizes.FindAsync(model.Id);
            if (updateData is not null)
            {
                updateData.NameEn = model.NameEn;
                updateData.NameAr = model.NameAr;
                updateData.ModifiedOn = DateTime.Now;
            }
           _dbcontext.Update(updateData);
           return await _dbcontext.SaveChangesAsync() > 0;
              
        }
 
        public async Task<bool> Delete(ItemSize model)
        {
            var data = await _dbcontext.ItemSizes.FindAsync(model.Id);
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
            var data = await _dbcontext.ItemSizes.FindAsync(id);
            if (data is not null)
            {
                data.ModifiedOn = DateTime.Now;
                data.Active = !data.Active;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<ItemSize> UpdateDisplayOrder(int id, int num = 0)
        {
            var data = await _dbcontext.ItemSizes.FindAsync(id);
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
