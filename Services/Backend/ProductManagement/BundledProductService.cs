using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using System.Linq.Dynamic.Core;
using Data.ProductManagement;
using AutoMapper;
using Utility.Models.Backend.ProductManagement;

namespace Services.Backend.ProductManagement.Interface
{
    public class BundledProductService : IBundledProductService
    {
        protected readonly ApplicationDbContext _dbcontext;
        protected string ErrorMessage = string.Empty;
        private readonly IMapper _mapper;
        public BundledProductService(ApplicationDbContext dbcontext,  IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        #region Bundled Product 

        public async Task<IEnumerable<BundledProduct>> GetAll()
        {
            IEnumerable<BundledProduct> items = await _dbcontext
                                           .BundledProducts
                                           .Where(x => x.Deleted == false)
                                           .AsNoTracking()
                                           .ToListAsync();
        return items;
        }

       
       public async Task<dynamic> GetAllForDataTable(DataTableParam param, string imageBaseUrl)
        {
            DataTableResult<dynamic> result = new() { Draw = param.Draw };

            try
            {
                //var items = _dbcontext.BundledProducts
                //            .Where(x => x.Deleted == false)
                //            .AsQueryable()
                //            .Include(x => x.BundledProductDetails)
                //            .ThenInclude(product => product.Product).ThenInclude(c => c.Category);
                 
               var items = _dbcontext.BundledProducts 
                          .Include(t => t.BundledProductDetails)
                          .ThenInclude(t => t.Product).ThenInclude(x=>  x.ItemSize )
                          .Where(t => t.Deleted==false)
                          .Select(x => new
                          {
                              x.Id,
                              x.DisplayOrder,
                              x.NameEn,
                              x.NameAr,
                              ImageUrl =  (x.ImageName != null ? imageBaseUrl + x.ImageName : null),
                              x.CreatedOn,
                              x.Active,
                              x.Deleted,
                              Packages = x.BundledProductDetails.Select(x=> 
                                          new {
                                              ProductName = x.Product.NameEn,
                                              Quantity = x.Quantity,
                                              PackageSize = x.Product.PiecesPerPacking,
                                              ItemSize = x.Product.ItemSize.NameEn,
                                          })
                          });


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

     
        public async Task<BundledProduct> GetById(int id)
        {
            var data = await _dbcontext.BundledProducts
                .Where(x=>x.Deleted==false)
                .Include(x => x.BundledProductDetails).ThenInclude(x=>x.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return data;
        }
        //lookup
        public async  Task<ProductAndCategoryModel> GetAllProductAndCategory(string productImagePath)
        {
            ProductAndCategoryModel model = new();
            model.Products = await _dbcontext.Products
                                .Select(x =>
                                new ProductSmallModel()
                                {
                                    Id = x.Id,
                                    NameEn = x.NameEn,
                                    NameAr = x.NameAr,
                                    ImageUrl = (x.ImageName != null ? productImagePath + x.ImageName : null),
                                    Price = x.Price,
                                    DisplayOrder = x.DisplayOrder,
                                    Active = x.Active,
                                    Deleted = x.Deleted
                                })
                                .Where(x => x.Deleted == false && x.Active == true)
                                .OrderBy(x => x.DisplayOrder)
                                .AsNoTracking().ToListAsync();

            model.Categories = await _dbcontext.Categories
                                .Select(x => new CategoryModel()
                                {
                                    Id = x.Id,
                                    NameEn = x.NameEn,
                                    NameAr = x.NameAr,
                                    DisplayOrder = x.DisplayOrder,
                                    Active = x.Active,
                                    Deleted = x.Deleted
                                })
                                .Where(x => x.Deleted == false && x.Active == true)
                                .OrderBy(x => x.DisplayOrder)
                                .AsNoTracking().ToListAsync();

            return model;
             
        }
        public async Task<bool> BundledProductExists(int? Id, string titleEn, string titleAr)
        {

            var result = await _dbcontext
                                .BundledProducts
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

        public async Task<BundledProduct> Create(BundledProduct model)
        {
            model.CreatedOn = DateTime.Now;
            model.ProductType = Utility.Enum.ProductType.Bundle;
            await _dbcontext.BundledProducts.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }

        public async Task<bool> Update(BundledProduct model)
        {
            var update = await _dbcontext.BundledProducts
                                .Where(x => x.Id == model.Id)
                                .Include(x => x.BundledProductDetails)
                                .FirstOrDefaultAsync();
            if (update is not null)
            {
                update.NameEn = model.NameEn;
                update.NameAr = model.NameAr;
                update.DescriptionEn = model.DescriptionEn;
                update.DescriptionAr = model.DescriptionAr;
                 
                
                update.ProductType = Utility.Enum.ProductType.Bundled; // bundled =1, subscription=2
                
                update.BundledProductDetails.Clear();
                update.BundledProductDetails = model.BundledProductDetails; //list of bundled products

                update.Price = model.Price;
                update.DiscountedPrice = model.DiscountedPrice;
                update.DiscountFromDate = model.DiscountFromDate;
                update.DiscountToDate = model.DiscountToDate;

                update.B2BPriceEnabled = model.B2BPriceEnabled;
                update.B2BPrice = model.B2BPrice;
                update.B2BDiscountedPrice = model.B2BDiscountedPrice;
                update.B2BDiscountFromDate = model.B2BDiscountFromDate;
                update.B2BDiscountToDate = model.B2BDiscountToDate;

                update.CategoryId = model.CategoryId;

                if (!string.IsNullOrEmpty(model.ImageName))
                { update.ImageName = model.ImageName; }

                update.ModifiedBy = model.ModifiedBy;
                update.ModifiedOn = DateTime.Now;
            }
            _dbcontext.Update(update);
           return await _dbcontext.SaveChangesAsync() > 0;
              
        }
 
        public async Task<bool> Delete(BundledProduct model)
        {
            var data = await _dbcontext.BundledProducts.FindAsync(model.Id);
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
            var data = await _dbcontext.BundledProducts.FindAsync(id);
            if (data is not null)
            {
                data.ModifiedOn = DateTime.Now;
                data.Active = !data.Active;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<BundledProduct> UpdateDisplayOrder(int id, int num = 0)
        {
            var data = await _dbcontext.BundledProducts.FindAsync(id);
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
