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
using Utility.Models.Frontend.ProductManagement;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Services.Frontend.ProductManagement.Interface
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

        public async Task<IEnumerable<CategoryModel>> GetAll(bool isEnglish)
        {
            IEnumerable<CategoryModel> items = await _dbcontext
                                           .Categories
                                           .Where(x => x.Deleted == false && x.Active == true)
                                           .Select(x => new CategoryModel()
                                           {
                                               Id = x.Id,
                                               Title = isEnglish ? x.NameEn : x.NameAr,
                                               SeoName = x.SeoName,
                                               Active = x.Active,
                                               ProductTypeId = x.ProductTypeId,
                                               DisplayOrder = x.DisplayOrder
                                           })
                                           .OrderBy(x => x.DisplayOrder).ThenBy(x => x.ProductTypeId)
                                           .AsNoTracking()
                                           .ToListAsync();
            return items;
        }

        public async Task<IEnumerable<CategoryModel>> GetAllHero(bool isEnglish, string baseUrl)
        {


            return await _dbcontext.Categories
                           .Where(x => x.Deleted == false && x.Active == true)
                           .Select(x => new CategoryModel()
                           {
                               Id = x.Id,
                               Title = isEnglish ? x.NameEn : x.NameAr,
                               SeoName = x.SeoName,
                               HoverImageUrl = baseUrl + x.ImageName,
                               ImageUrl = baseUrl + x.ImageNormalIconName,
                               SelectedImageUrl = baseUrl + x.ImageSelectedIconName,
                               ImageDesktopUrl = baseUrl + x.ImageDesktopName,
                               ImageMobileUrl = baseUrl + x.ImageMobileName,
                               ProductTypeId = x.ProductTypeId,
                               DisplayOrder = x.DisplayOrder
                           })
                           .OrderBy(x => x.DisplayOrder).ThenBy(x => x.ProductTypeId)
                           .AsNoTracking()
                           .ToListAsync();
            // IEnumerable<CategoryModel> items return items;
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
                             DisplayOrder = x.DisplayOrder,
                             NameEn = x.NameEn,
                             NameAr = x.NameAr,
                             SeoName = x.SeoName,
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
            }
            catch (Exception err)
            {
                result.Error = err;
            }
            return result;
        }


        public async Task<Category> GetById(int id)
        {
            var data = await _dbcontext.Categories.FindAsync(id);
            return data;
        }


        #endregion

    }
}
