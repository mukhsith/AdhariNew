using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using System.Linq.Dynamic.Core;
using Data.ProductManagement;

namespace Services.Frontend.ProductManagement.Interface
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
                             DisplayOrder = x.DisplayOrder,
                             NameEn = x.NameEn,
                             NameAr = x.NameAr,
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
            }
            catch (Exception err)
            {
                result.Error = err;
            }
            return result;
        }


        public async Task<ItemSize> GetById(int id)
        {
            var data = await _dbcontext.ItemSizes.FindAsync(id);
            return data;
        }

        
        #endregion

    }
}
