using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using System.Linq.Dynamic.Core;
using Data.ProductManagement;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Utility.Enum;

namespace Services.Backend.ProductManagement.Interface
{
    public class CategoryService : ICategoryService
    {
        protected readonly ApplicationDbContext _dbcontext;
        protected string ErrorMessage = string.Empty;
        public CategoryService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region Category 

        public async Task<bool> Exists(int? Id, string titleEn, string titleAr)
        {

            var result = await _dbcontext
                                .Categories
                                .Select(x => new { x.Id, x.NameEn, x.NameAr })
                                .Where(x => (x.NameEn.ToLower() == titleEn.ToLower() ||
                                 x.NameAr.ToLower() == titleAr.ToLower()))
                                .AsNoTracking()
                                .FirstOrDefaultAsync();
            if (result != null && Id.HasValue)
            {
                return result.Id != Id;
            }
            return false;

        }
        public async Task<bool> ExistsCategory(int  Id, ProductType productType) 
        {
            var result = await _dbcontext
                                .Categories
                                .Select(x => new { x.Id, x.ProductTypeId })
                                .Where(x => (x.ProductTypeId== productType))
                                .AsNoTracking()
                                .FirstOrDefaultAsync();

            if (result != null )
            {
                return result.Id != Id;
            }
            return false;
           
        }
        public async Task<IEnumerable<Category>> GetAll()
        {
            IEnumerable<Category> items = await _dbcontext
                                           .Categories 
                                           .Where(x => x.Deleted == false)
                                           .AsNoTracking()
                                           .ToListAsync();
        return items;
        }


       public async Task<DataTableResult<List<Category>>> GetAllForDataTable(DataTableParam param, string imageBaseUrl)
        {
            DataTableResult<List<Category>> result = new() { Draw = param.Draw };
            try
            {
                var items = _dbcontext.Categories
                         .Select(x => new Category
                          {
                              Id = x.Id,
                              DisplayOrder=x.DisplayOrder,
                              NameEn = x.NameEn,
                              NameAr = x.NameAr, 
                              ImageUrl = (x.ImageName != null ? imageBaseUrl + x.ImageName : null),
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
                //for(var index=0; index < result.Data.Count; index++)
                //{ var row = result.Data[index];
                //    row.ImageUrl  = row.ImageName  != null ? imageBaseUrl + row.ImageName  : null; 
                //}
                return result;
            } catch (Exception err) {
                result.Error = err;
            }
            return result;
        }

     
        public async Task<Category> GetById(int id)
        {
            var data = await _dbcontext.Categories.FindAsync(id);
            return data;
        }
        
        public async Task<Category> Create(Category model)
        {
            model.CreatedOn = DateTime.Now;
            model.DisplayOrder = await GetNextDisplayOrder();
            await _dbcontext.Categories.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        } 
        private async Task<int> GetNextDisplayOrder()
        {
            var item = await _dbcontext.Categories.OrderBy(x => x.DisplayOrder).AsNoTracking().LastAsync();
            if (item is not null)
            {
                return item.DisplayOrder + 1;
            }
            return 1;

        }
        public async Task<bool> Update(Category model)
        {
            var update = await _dbcontext.Categories.FindAsync(model.Id);
            if (update is not null)
            {
                update.NameEn = model.NameEn;
                update.NameAr = model.NameAr;
                update.SeoName = model.SeoName;
                update.ProductTypeId = model.ProductTypeId;
                if (!string.IsNullOrEmpty(model.ImageName))
                { update.ImageName = model.ImageName; }
                //for mobile application
                if (!string.IsNullOrEmpty(model.ImageNormalIconName))
                { update.ImageNormalIconName = model.ImageNormalIconName; }
                if (!string.IsNullOrEmpty(model.ImageSelectedIconName))
                { update.ImageSelectedIconName = model.ImageSelectedIconName; }


                if (!string.IsNullOrEmpty(model.ImageDesktopName))
                { update.ImageDesktopName = model.ImageDesktopName; }
                 
                if (!string.IsNullOrEmpty(model.ImageMobileName))
                { update.ImageMobileName = model.ImageMobileName; }

               
                update.ModifiedBy = model.ModifiedBy;
                update.ModifiedOn = DateTime.Now;

            }
            _dbcontext.Update(update);
           return await _dbcontext.SaveChangesAsync() > 0;
              
        }
 
        public async Task<bool> Delete(Category model)
        {
            var data = await _dbcontext.Categories.FindAsync(model.Id);
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
            var data = await _dbcontext.Categories.FindAsync(id);
            if (data is not null)
            {
                data.ModifiedOn = DateTime.Now;
                data.Active = !data.Active;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<Category> UpdateDisplayOrder(int id, int num = 0)
        {
            var data = await _dbcontext.Categories.FindAsync(id);
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
