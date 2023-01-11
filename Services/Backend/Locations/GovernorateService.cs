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

namespace Services.Backend.Locations.Interface
{
    public class GovernorateService : IGovernorateService
    {
        protected readonly ApplicationDbContext _dbcontext;
        protected string ErrorMessage = string.Empty;
        public GovernorateService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region Governorate Service 

        public async Task<IEnumerable<Governorate>> GetAll()
        {
            IEnumerable<Governorate> items = await _dbcontext
                                           .Governorates
                                           .Where(x => x.Deleted == false)
                                           .AsNoTracking()
                                           .ToListAsync();
            return items;
        }


       public async Task<DataTableResult<List<Governorate>>> GetAllForDataTable(DataTableParam param)
        {
            DataTableResult<List<Governorate>> result = new() { Draw = param.Draw };
            try
            {
                var items = _dbcontext.Governorates
                         .Select(x => new Governorate
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
                if (!string.IsNullOrEmpty(param.SearchValue))
                {
                    var SearchValue = param.SearchValue.ToLower();
                    items = items.Where(obj =>
                     obj.NameEn.ToLower().Contains(SearchValue) ||
                     obj.NameAr.ToLower().Contains(SearchValue)  
                     );
                }

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

        public async Task<bool> Exists(int? Id, string titleEn, string titleAr)
        {

            var result = await _dbcontext
                                .Governorates
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
        public async Task<Governorate> GetById(int id)
        {
            var data = await _dbcontext.Governorates.FindAsync(id);
            return data;
        }
        
        public async Task<Governorate> Create(Governorate model)
        {
            model.CreatedOn = DateTime.Now;
            model.DisplayOrder = await GetNextDisplayOrder(); 
            await _dbcontext.Governorates.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        } 
        private async Task<int> GetNextDisplayOrder()
        {
            var item = await _dbcontext.Governorates.OrderBy(x => x.DisplayOrder).AsNoTracking().LastAsync();
            if (item is not null)
            {
                return item.DisplayOrder + 1;
            }
            return 1;

        }
        public async Task<bool> Update(Governorate model)
        {
            var updateData = await _dbcontext.Governorates.FindAsync(model.Id);
            if (updateData is not null)
            {
                updateData.NameEn = model.NameEn;
                updateData.NameAr = model.NameAr;
                updateData.ModifiedOn = DateTime.Now;
            }
           _dbcontext.Update(updateData);
           return await _dbcontext.SaveChangesAsync() > 0;
              
        }
 
        public async Task<bool> Delete(Governorate model)
        {
            var data = await _dbcontext.Governorates.FindAsync(model.Id);
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
            var data = await _dbcontext.Governorates.FindAsync(id);
            if (data is not null)
            {
                data.ModifiedOn = DateTime.Now;
                data.Active = !data.Active;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<Governorate> UpdateDisplayOrder(int id, int num = 0)
        {
            var data = await _dbcontext.Governorates.FindAsync(id);
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
