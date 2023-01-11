using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using System.Linq.Dynamic.Core;
using Data.ProductManagement; 
using Utility.Models.Admin.ProductManagement;

namespace Services.Backend.ProductManagement.Interface
{
    public class ProductStockHistoryService : IProductStockHistoryService
    {
        protected readonly ApplicationDbContext _dbcontext;
        protected string ErrorMessage = string.Empty;
        public ProductStockHistoryService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region Category 

        public async Task<IEnumerable<ProductStockHistory>> GetAll()
        {
            IEnumerable<ProductStockHistory> items = await _dbcontext
                                           .ProductStockHistories 
                                           .AsNoTracking()
                                           .ToListAsync();
        return items;
        }


       public async Task<DataTableResult<List<ProductStockHistoryModel>>> GetAllForDataTable(DataTableParam param )
        {
            DataTableResult<List<ProductStockHistoryModel>> result = new() { Draw = param.Draw };
            try
            {
                var items = _dbcontext.ProductStockHistories
                        .Include(x=>x.SystemUser)
                         .Select(x => new ProductStockHistoryModel
                         {
                             Id = x.Id,
                             ProductUpdateType = x.GetProductUpdateType(),
                             OldStock=x.OldStock,
                             ProductActionType = x.GetProductActionType(),
                             InputStock=x.InputStock,
                             UpdatedStock = x.UpdatedStock,
                             ProductType=x.GetProductType(),
                             CreatedBy =x.SystemUser.FullName,
                             CreatedOn = x.CreatedOn,
                             Deleted=x.Deleted
                          }).Where(x => x.Deleted == false);
                //User Search
                if (!string.IsNullOrEmpty(param.SearchValue))
                {
                    var SearchValue = param.SearchValue.ToLower();
                    items = items.Where(obj =>
                     obj.ProductUpdateType.ToLower().Contains(SearchValue) ||
                     obj.ProductActionType.ToLower().Contains(SearchValue)  
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
                    items = items.OrderByDescending(x => x.Id);
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

     
        public async Task<ProductStockHistory> GetById(int id)
        {
            var data = await _dbcontext.ProductStockHistories.FindAsync(id);
            return data;
        }
        
        public async Task<ProductStockHistory> Create(ProductStockHistory model)
        {
            model.CreatedOn = DateTime.Now;
            await _dbcontext.ProductStockHistories.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }

        public async Task<bool> Update(ProductStockHistory model)
        {
            var update = await _dbcontext.ProductStockHistories.FindAsync(model.Id);
            if (update is not null)
            {
                  
                update.CreatedBy = model.CreatedBy;
                update.CreatedOn = DateTime.Now;

            }
            _dbcontext.Update(update);
           return await _dbcontext.SaveChangesAsync() > 0;
              
        }
 
        
        #endregion
       
    }
}
