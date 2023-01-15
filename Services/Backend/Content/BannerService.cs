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
    public class BannerService : IBannerService
    {
        protected readonly ApplicationDbContext _dbcontext;
        protected string ErrorMessage = string.Empty;
        public BannerService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region Banner 

        public async Task<IEnumerable<Banner>> GetAll()
        {
            IEnumerable<Banner> items = await _dbcontext
                                           .Banners 
                                           .Where(x => x.Deleted == false)
                                           .AsNoTracking()
                                           .ToListAsync();
            return items;
        }


       public async Task<DataTableResult<List<Banner>>> GetAllForDataTable(DataTableParam param, string imageBaseUrl)
        {
            DataTableResult<List<Banner>> result = new() { Draw = param.Draw };
            try
            {
                var items = _dbcontext.Banners
                         .Include(x=>x.Product)
                         .Select(x => new Banner
                          {
                              Id = x.Id,
                              DisplayOrder=x.DisplayOrder,
                              ImageNameEn = x.ImageNameEn,
                              ImageNameAr=x.ImageNameAr,
                              ImageUrlEn = (x.ImageNameEn != null ? imageBaseUrl +  x.ImageNameEn : null),
                              ImageUrlAr = (x.ImageNameAr != null ? imageBaseUrl +  x.ImageNameAr : null),
                              LinkEnabled = x.LinkEnabled,
                              LinkType = x.LinkType,
                              LinkUrl = x.LinkUrl,
                              ProductId = x.ProductId,
                              Product = x.Product, 
                              CreatedOn = x.CreatedOn,
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
                } else
                {
                    items = items.OrderBy(x => x.DisplayOrder);
                }
                result.RecordsTotal = items.Count();
                result.RecordsFiltered = items.Count();
                result.Data = await items.Skip(param.Skip).Take(param.PageSize).ToListAsync();
                //for(var index=0; index < result.Data.Count; index++)
                //{ var row = result.Data[index];
                    
                //}
                return result;
            } catch (Exception err) {
                result.Error = err;
            }
            return result;
        }

     
        public async Task<Banner> GetById(int id)
        {
            var data = await _dbcontext.Banners.FindAsync(id);
            return data;
        }
        
        public async Task<Banner> Create(Banner model)
        {
            model.CreatedOn = DateTime.Now; 
            model.DisplayOrder = await GetNextDisplayOrder();
            await _dbcontext.Banners.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
        
        private async Task<int> GetNextDisplayOrder()
        {
            var item = await _dbcontext.Banners.OrderBy(x => x.DisplayOrder).AsNoTracking().LastAsync();
            if (item is not null)
            {
                return item.DisplayOrder + 1;
            }
            return 1;

        }
        public async Task<bool> Update(Banner model)
        {
            var updateData = await _dbcontext.Banners.FindAsync(model.Id);
            if (updateData is not null)
            {
                if (!string.IsNullOrEmpty(model.ImageNameEn))
                { updateData.ImageNameEn = model.ImageNameEn; }

                if (!string.IsNullOrEmpty(model.ImageNameAr))
                { updateData.ImageNameAr = model.ImageNameAr; }
                updateData.LinkEnabled = model.LinkEnabled;
                updateData.LinkType = model.LinkType;
                updateData.LinkUrl = model.LinkUrl;
                updateData.ProductId = model.ProductId;
                updateData.ModifiedOn = DateTime.Now;
            }
           _dbcontext.Update(updateData);
           return await _dbcontext.SaveChangesAsync() > 0;
              
        }
 
        public async Task<bool> Delete(Banner model)
        {
            var data = await _dbcontext.Banners.FindAsync(model.Id);
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
            var data = await _dbcontext.Banners.FindAsync(id);
            if (data is not null)
            {
                data.ModifiedOn = DateTime.Now;
                data.Active = !data.Active;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<Banner> UpdateDisplayOrder(int id, int num = 0)
        {
            var data = await _dbcontext.Banners.FindAsync(id);
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
