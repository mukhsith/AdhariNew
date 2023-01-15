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
    public class AreaService : IAreaService
    {
        protected readonly ApplicationDbContext _dbcontext;
        protected string ErrorMessage = string.Empty;
        public AreaService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region Area Service 

        public async Task<IEnumerable<Area>> GetAll()
        {
            IEnumerable<Area> items = await _dbcontext
                                           .Areas
                                           .Where(x => x.Deleted == false)
                                           .AsNoTracking()
                                           .ToListAsync();
            return items;
            }


       public async Task<DataTableResult<List<Area>>> GetAllForDataTable(DataTableParam param, int? governorateId = null)
        {
            DataTableResult<List<Area>> result = new() { Draw = param.Draw };
            try
            {
                var items = _dbcontext.Areas
                          .Include(x=>x.Governorate)
                         .Select(x => new Area   
                          {
                              Id = x.Id,
                              DisplayOrder=x.DisplayOrder,
                              NameEn=x.NameEn,
                              NameAr=x.NameAr,
                              Governorate = x.Governorate,
                              CreatedOn = x.CreatedOn,
                              Active = x.Active,
                              Deleted = x.Deleted
                          }).Where(x => x.Deleted == false);
                if (governorateId.HasValue)
                {
                    items = items.Where(a => a.GovernorateId==governorateId);
                }
                //Packages = x.SubscriptionDetails.Select(x =>
                //                              new {
                //                                  ProductName = x.Product.NameEn,
                //                                  Quantity = x.Quantity,
                //                                  PackageSize = x.Product.PiecesPerPacking,
                //                                  ItemSize = x.Product.ItemSize.NameEn,
                //                              })
                //User Search
                if (!string.IsNullOrEmpty(param.SearchValue))
                {
                    var SearchValue = param.SearchValue.ToLower();
                    items = items.Where(obj =>
                     obj.NameEn.ToLower().Contains(SearchValue) ||
                     obj.NameAr.ToLower().Contains(SearchValue) ||
                     obj.DeliveryFee.ToString().Contains(SearchValue)
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
                                .Areas
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

        public async Task<Area> GetById(int id)
        {
            var data = await _dbcontext.Areas.FindAsync(id);
            return data;
        }
        
        public async Task<Area> Create(Area model)
        {
            model.CreatedOn = DateTime.Now; 
            model.DisplayOrder = await GetNextDisplayOrder();
            await _dbcontext.Areas.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        } 
        private async Task<int> GetNextDisplayOrder()
        {
            var item = await _dbcontext.Areas.OrderBy(x => x.DisplayOrder).AsNoTracking().LastAsync();
            if (item is not null)
            {
                return item.DisplayOrder + 1;
            }
            return 1;

        }
        public async Task<bool> Update(Area model)
        {
            var updateData = await _dbcontext.Areas.FindAsync(model.Id);
            if (updateData is not null)
            {
                updateData.NameEn = model.NameEn;
                updateData.NameAr = model.NameAr;
                updateData.DeliveryFee = model.DeliveryFee;
                updateData.GovernorateId = model.GovernorateId;
                updateData.ModifiedOn = DateTime.Now;
            }
           _dbcontext.Update(updateData);
           return await _dbcontext.SaveChangesAsync() > 0;
              
        }
 
        public async Task<bool> Delete(Area model)
        {
            var data = await _dbcontext.Areas.FindAsync(model.Id);
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
            var data = await _dbcontext.Areas.FindAsync(id);
            if (data is not null)
            {
                data.ModifiedOn = DateTime.Now;
                data.Active = !data.Active;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<Area> UpdateDisplayOrder(int id, int num = 0)
        {
            var data = await _dbcontext.Areas.FindAsync(id);
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
