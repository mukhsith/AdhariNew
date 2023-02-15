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
using Utility.Enum;
using Utility.Models.Admin.ProductManagement;
using Data.Shop;
using Utility.Models.Frontend.ProductManagement;

namespace Services.Backend.ProductManagement.Interface
{
    public class ProductService : IProductService
    {
        protected readonly ApplicationDbContext _dbcontext;
        protected string ErrorMessage = string.Empty;
        protected IMapper mapper;
        public ProductService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region Product 
        public async Task<IEnumerable<Product>> GetProductsByCategorySeoName(string SeoName)
        {
            List<Product> products = new();

            var category = await _dbcontext.Categories
                                           .Select(x => new Category() { Id = x.Id, SeoName = x.SeoName })
                                           .Where(x => x.SeoName.ToLower() == SeoName)
                                           .AsNoTracking().FirstOrDefaultAsync();

            if (category is null) { return products; }

            products = await _dbcontext.Products
                   .Include(t => t.ProductDetails)
                   .Include(x => x.ItemSize)
                   .Where(x => x.Deleted == false && x.Active == true && x.CategoryId == category.Id)
                   .AsNoTracking().ToListAsync();

            return products;
        }
        public async Task<IList<Product>> GetAll(int categoryId = 0, string keyword = "", ProductType? productType = null,
            bool favorite = false, int customerId = 0, string categorySeoName = "")
        {
            var data = _dbcontext.Products
                .Include(a => a.Category)
                .Include(a => a.ItemSize)
                .Include(a => a.ProductDetails)
                .Where(x => !x.Deleted && x.Active && !x.Category.Deleted && x.Category.Active);

            if (categoryId > 0)
            {
                data = data.Where(a => a.CategoryId == categoryId);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(a => (!string.IsNullOrEmpty(a.NameEn) && a.NameEn.ToLower().Contains(keyword.ToLower())) ||
                     (!string.IsNullOrEmpty(a.NameAr) && a.NameAr.ToLower().Contains(keyword.ToLower())));
            }

            if (!string.IsNullOrEmpty(categorySeoName))
            {
                data = data.Where(a => a.Category.SeoName == categorySeoName);
            }

            if (productType.HasValue)
            {
                data = data.Where(a => a.ProductType == productType);
            }

            //if (favorite)
            //{
            //    var favoriteProductIds = (await GetAllFavorite(customerId: customerId)).GroupBy(a => a.ProductId).Select(a => a.Key).ToList();
            //    data = data.Where(a => favoriteProductIds.Contains(a.Id));
            //}

            return await data.AsNoTracking().ToListAsync();
        }
        public async Task<Product> GetById(int id)
        {
            var data = await _dbcontext.Products
                .Include(a => a.ProductDetails)
                .Include(x => x.Category)
                .Include(x => x.ItemSize)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return data;
        }
        public async Task<Product> GetProductBySeoName(string seoName)
        {
            var data = await _dbcontext.Products
                .Include(a => a.ProductDetails)
                .Include(x => x.Category)
                .Include(x => x.ItemSize)
                .Where(x => x.SeoName.ToLower() == seoName.ToLower())
                .FirstOrDefaultAsync();

            return data;
        }
        #endregion

        public async Task<Product> GetByIdOnlyProduct(int id)
        {
            var data = await _dbcontext.Products
                .Where(x => x.Deleted == false && x.Id == id && x.ProductType == ProductType.BaseProduct)
                .FirstOrDefaultAsync();
            return data;
        }

        //public async Task<Product> GetById(int id)
        //{
        //    try
        //    {

        //        var data = await _dbcontext.Products
        //         .Include(x => x.ProductDetails.Where(x => x.ProductId == x.Id))//.ThenInclude(x => x.Product)
        //         .Where(x => x.Deleted == false & x.ProductType != ProductType.BaseProduct)
        //        .FirstOrDefaultAsync();

        //        return data;
        //    }
        //    catch (Exception exp)
        //    {
        //        return new Product();
        //    }
        //}

        public async Task<dynamic> GetById(int id, string baseImageUrl, ProductType productType)
        {
            try
            {


                var item = _dbcontext.Products
                         .Include(t => t.ProductDetails)
                         .Include(x => x.ItemSize)
                         .Where(t => t.Deleted == false && t.Id == id && (t.ProductType == productType))
                         .Select(x => new
                         {
                             x.Id,
                             x.NameEn,
                             x.NameAr,
                             x.DescriptionEn,
                             x.DescriptionAr,
                             x.PiecesPerPacking,
                             x.Stock,
                             x.SpecialPackage,
                             x.SubscriptionDurationId,
                             x.Price,
                             x.DiscountedPrice,
                             x.DiscountFromDate,
                             x.DiscountToDate,
                             x.B2BPriceEnabled,
                             x.B2BPrice,
                             x.B2BDiscountedPrice,
                             x.B2BDiscountFromDate,
                             x.B2BDiscountToDate,
                             x.ItemSizeId,
                             x.CategoryId,
                             x.ProductType,
                             ImageUrl = (x.ImageName != null ? baseImageUrl + x.ImageName : null),
                             x.CreatedOn,
                             x.Active,
                             x.MinCartQuantity,
                             x.MaxCartQuantity,
                             x.B2BMinCartQuantity,
                             x.B2BMaxCartQuantity,
                             x.SubscriptionDurationIds,
                             x.Deleted,
                             ProductDetails = from pd in x.ProductDetails
                                              join p in _dbcontext.Products.Include(x => x.ItemSize).Where(t => t.Deleted == false && t.ProductType == ProductType.BaseProduct)
                                              on pd.ChildProductId equals p.Id
                                              select new
                                              {
                                                  pd.Id,
                                                  pd.ChildProductId,
                                                  pd.ProductId,
                                                  ProductName = p.NameEn,
                                                  imageUrl = (x.ImageName != null ? baseImageUrl + x.ImageName : null),
                                                  pd.Quantity,
                                                  p.PiecesPerPacking,
                                                  p.Price,
                                                  itemSizeNameEn = p.ItemSize.NameEn,
                                              }
                         });
                /*                id: bp.id, //new item will be with zero id
                                    parentId: parentId, //addEdit edit mode id will be > 0
                                    productId: product.id,
                                    imageUrl: product.imageUrl,
                                    nameEn: product.nameEn + ' ' + product.itemSizeNameEn,
                                    price: product.price,
                                    quantity: bp.quantity
                */
                //var data = await _dbcontext.Products
                //     .Include(x => x.ProductDetails.Where(x => x.ProductId == x.Id))//.ThenInclude(x => x.Product)
                //     .Where(x => x.Deleted == false & x.ProductType != ProductType.BaseProduct)
                //    .FirstOrDefaultAsync();

                return await item.AsNoTracking().FirstOrDefaultAsync();
            }
            catch (Exception exp)
            {
                ErrorMessage = exp.Message;
                return new Product();
            }
        }

        #region Product 
        public async Task<IList<ProductDetail>> GetAllProductDetail(int productId)
        {
            var data = _dbcontext.ProductDetails.Where(x => !x.Deleted && x.ProductId == productId);

            return await data.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetAll(ProductType? productType = null)
        {
            IQueryable<Product> items = _dbcontext.Products.Where(x => x.Deleted == false);

            if (productType.HasValue)
            {
                items = items.Where(x => x.ProductType == productType);
            }

            return await items.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<SubscriptionDuration>> GetAllSubscriptionDurations()
        {
            return await _dbcontext.SubscriptionDurations.Where(x => x.Deleted == false).AsNoTracking().ToListAsync();
              
        }

        /// <summary>
        /// for normal product only (based Product)
        /// </summary>
        /// <param name="param"></param>
        /// <param name="imageBaseUrl"></param>
        /// <returns></returns>
        public async Task<dynamic> GetAllForDataTable(DataTableParam param, string imageBaseUrl, AdminProductSearchParam Searchparam)
        {
            DataTableResult<dynamic> result = new() { Draw = param.Draw };
            try
            {
                var items = _dbcontext.Products.Include(x=> x.Category)
                          .Select(x => new Product
                          {
                              Id = x.Id,
                              DisplayOrder = x.DisplayOrder,
                              NameEn = x.NameEn,
                              NameAr = x.NameAr,
                              ImageUrl = (x.ImageName != null ? imageBaseUrl + x.ImageName : null),
                              Stock = x.Stock,
                              ImageName = x.ImageName,
                              Category = x.Category,
                              CategoryId= x.CategoryId,
                              ProductType = x.ProductType,
                              CreatedOn = x.CreatedOn,
                              Active = x.Active,
                              Deleted = x.Deleted
                          }).Where(x => x.Deleted == false && x.ProductType == ProductType.BaseProduct);

                //User Search
                if (!string.IsNullOrEmpty(param.SearchValue))
                {
                    var SearchValue = param.SearchValue.ToLower();
                    items = items.Where(obj =>
                     obj.NameEn.ToLower().Contains(SearchValue) ||
                     obj.NameAr.ToLower().Contains(SearchValue) ||
                     obj.Stock.ToString().Contains(SearchValue)
                     );
                }


                if (!string.IsNullOrEmpty(Searchparam.productName))
                {
                    items = items.Where(x => x.NameEn == Searchparam.productName.Trim() || x.NameAr == Searchparam.productName.Trim());
                }



                if (Searchparam.categoryID.HasValue)
                {
                    items = items.Where(x => x.CategoryId == Searchparam.categoryID.Value);
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
            }
            catch (Exception exp)
            {
                ErrorMessage = exp.Message;
                result.Error = exp;
            }
            return result;
        }

        /// <summary>
        /// for Bundle and Subscription Product only
        /// </summary>
        /// <param name="param"></param>
        /// <param name="baseImageUrl"></param>
        /// <param name="productType"></param>
        /// <returns></returns>
        public async Task<dynamic> GetAllForDataTableByProductType(DataTableParam param, string baseImageUrl, ProductType productType, AdminProductSearchParam Searchparam)
        {
            DataTableResult<dynamic> result = new() { Draw = param.Draw };
            try
            {
                var items = _dbcontext.Products.Include(x => x.Category)
                         .Include(t => t.ProductDetails)
                         .Include(x => x.ItemSize)
                         .Where(t => t.Deleted == false && t.ProductType == productType)
                         .Select(x => new
                         {
                             x.Id,
                             x.DisplayOrder,
                             x.NameEn,
                             x.NameAr,
                             x.Category,
                             x.CategoryId,
                             ImageUrl = (x.ImageName != null ? baseImageUrl + x.ImageName : null),
                             x.CreatedOn,
                             x.Active,
                             x.Deleted,
                             ProductDetails = from pd in x.ProductDetails
                                              join p in _dbcontext.Products.Include(x => x.ItemSize).Where(t => t.Deleted == false && t.ProductType == ProductType.BaseProduct)
                                              on pd.ChildProductId equals p.Id
                                              select new
                                              {
                                                  ProductName = p.NameEn,
                                                  Quantity = pd.Quantity,
                                                  PiecesPerPacking = p.PiecesPerPacking,
                                                  ItemSize = p.ItemSize.NameEn,
                                              }
                         });

                if (!string.IsNullOrEmpty(param.SearchValue))
                {
                    var SearchValue = param.SearchValue.ToLower();
                    items = items.Where(obj =>
                     obj.NameEn.ToLower().Contains(SearchValue) ||
                     obj.NameAr.ToLower().Contains(SearchValue)
                     );
                }

                if (!string.IsNullOrEmpty(Searchparam.productName))
                {
                    items = items.Where(x => x.NameEn == Searchparam.productName.Trim() || x.NameAr == Searchparam.productName.Trim());
                }



                if (Searchparam.categoryID.HasValue)
                {
                    items = items.Where(x => x.CategoryId == Searchparam.categoryID.Value);
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
            }
            catch (Exception exp)
            {
                ErrorMessage = exp.Message;
                result.Error = exp;
            }
            return result;
        }

        //public async Task<Product> GetById(int id)
        //{
        //    var data = await _dbcontext.Products.FindAsync(id);
        //    return data;
        //}
        //public async Task<bool> Exists(int? Id, string titleEn, string titleAr)
        //{

        //    var result = await _dbcontext
        //                        .Products
        //                        .Select(x => new { x.Id, x.NameEn, x.NameAr })
        //                        .Where(x => x.NameEn.ToLower() == titleEn.ToLower() ||
        //                         x.NameAr.ToLower() == titleAr.ToLower())
        //                        .AsNoTracking()
        //                        .FirstOrDefaultAsync();
        //    if (result != null && Id.HasValue)
        //    {
        //        return result.Id != Id;
        //    }
        //    return false;

        //}
        public async Task<bool> Exists(int? Id, string titleEn, string titleAr, ProductType productType)
        {

            //var result = await _dbcontext
            //                    .Products
            //                    .Select(x => new { x.Id, x.NameEn, x.NameAr, x.ProductType })
            //                    .Where(x => (x.NameEn.ToLower() == titleEn.ToLower() ||
            //                     x.NameAr.ToLower() == titleAr.ToLower())
            //                     && x.ProductType == productType)
            //                    .AsNoTracking()
            //                    .FirstOrDefaultAsync();
            var result = await _dbcontext
                                .Products
                                .Select(x => new { x.Id, x.NameEn, x.NameAr, x.ProductType })
                                .Where(x => (x.NameEn.ToLower() == titleEn.ToLower()))
                                .AsNoTracking()
                                .FirstOrDefaultAsync();
            if (result != null && Id.HasValue)
            {
                return result.Id != Id;
            }
            return false;

        }
        public async Task<Product> Create(Product model)
        {
            model.CreatedOn = DateTime.Now;
            model.DisplayOrder = await GetNextDisplayOrder();
            await _dbcontext.Products.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }

        private async Task<int> GetNextDisplayOrder()
        {
            var item = await _dbcontext.Products.OrderBy(x => x.DisplayOrder).AsNoTracking().LastAsync();
            if (item is not null)
            {
                return item.DisplayOrder + 1;
            }
            return 1;
        }

        public async Task<bool> Update(Product model)
        {
            var update = await _dbcontext.Products.FindAsync(model.Id);
            if (update is not null)
            {
                update.NameEn = model.NameEn;
                update.NameAr = model.NameAr;
                update.DescriptionEn = model.DescriptionEn;
                update.DescriptionAr = model.DescriptionAr;

                update.PiecesPerPacking = model.PiecesPerPacking;
                if (model.ProductType == ProductType.BaseProduct)
                {
                    update.ItemSize = model.ItemSize;
                    update.CategoryId = model.CategoryId;
                }
                else if (model.ProductType == ProductType.BundledProduct)
                {
                    update.CategoryId = model.CategoryId; 
                }

                update.Price = model.Price;
                update.DiscountedPrice = model.DiscountedPrice;
                update.DiscountFromDate = model.DiscountFromDate;
                update.DiscountToDate = model.DiscountToDate;


                update.MinCartQuantity = model.MinCartQuantity;
                update.MaxCartQuantity = model.MaxCartQuantity;


                update.B2BPriceEnabled = model.B2BPriceEnabled;
                update.B2BPrice = model.B2BPrice;
                update.B2BDiscountedPrice = model.B2BDiscountedPrice;
                update.B2BDiscountFromDate = model.B2BDiscountFromDate;
                update.B2BDiscountToDate = model.B2BDiscountToDate;

                update.B2BMinCartQuantity = model.B2BMinCartQuantity;
                update.B2BMaxCartQuantity = model.B2BMaxCartQuantity;
                update.SubscriptionDurationIds = model.SubscriptionDurationIds;
                if (!string.IsNullOrEmpty(model.ImageName))
                { update.ImageName = model.ImageName; }

                update.ModifiedBy = model.ModifiedBy;
                update.ModifiedOn = DateTime.Now;
            }
            _dbcontext.Update(update);
            return await _dbcontext.SaveChangesAsync() > 0;

        }
        public async Task<bool> UpdateBundle(Product model)
        {
            // var update = await _dbcontext.Products.FindAsync(model.Id);
            var update = await _dbcontext.Products.Where(x => x.Id == model.Id && x.ProductType == model.ProductType)
                                     .Include(x => x.ProductDetails)
                                     .FirstOrDefaultAsync();
            if (update is not null)
            {
                update.NameEn = model.NameEn;
                update.NameAr = model.NameAr;
                update.DescriptionEn = model.DescriptionEn;
                update.DescriptionAr = model.DescriptionAr;
                update.CategoryId = model.CategoryId;
                update.PiecesPerPacking = model.PiecesPerPacking;

                update.ProductType = model.ProductType;
                //only bundled product have a category, subscription does not have (itemsize,category)
                if (model.ProductType == ProductType.BundledProduct)
                {
                    update.CategoryId = model.CategoryId;
                } 
                
                //update for special subscription package
                update.SpecialPackage = model.SpecialPackage;
                update.SubscriptionDurationId = model.SubscriptionDurationId;
                 


                //product details

                update.Price = model.Price;
                update.DiscountedPrice = model.DiscountedPrice;
                update.DiscountFromDate = model.DiscountFromDate;
                update.DiscountToDate = model.DiscountToDate;

                update.MinCartQuantity = model.MinCartQuantity;
                update.MaxCartQuantity = model.MaxCartQuantity;


                update.B2BPriceEnabled = model.B2BPriceEnabled;
                update.B2BPrice = model.B2BPrice;
                update.B2BDiscountedPrice = model.B2BDiscountedPrice;
                update.B2BDiscountFromDate = model.B2BDiscountFromDate;
                update.B2BDiscountToDate = model.B2BDiscountToDate;

                update.B2BMinCartQuantity = model.B2BMinCartQuantity;
                update.B2BMaxCartQuantity = model.B2BMaxCartQuantity;
                update.SubscriptionDurationIds = model.SubscriptionDurationIds;

                if (!string.IsNullOrEmpty(model.ImageName))
                { update.ImageName = model.ImageName; }

                update.ModifiedBy = model.ModifiedBy;
                update.ModifiedOn = DateTime.Now;

                // await UpdateProductDetail(model);
                foreach (var detail in update.ProductDetails)
                {
                    //received product details
                    var found = model.ProductDetails.Where(x => x.Id == detail.Id).FirstOrDefault();
                    if (found is not null) //update existing detail
                    {
                        detail.Quantity = found.Quantity;
                    }
                    else // remove existing detail
                    {
                        update.ProductDetails.Remove(detail);
                    }
                }
                var newItems = model.ProductDetails.Where(x => x.Id == 0).ToList();
                foreach (var newItem in newItems)
                {
                    update.ProductDetails.Add(newItem); //add new detail
                }

            }
            _dbcontext.Update(update);
            return await _dbcontext.SaveChangesAsync() > 0;
        }

        private async Task<bool> UpdateProductDetail(Product model)
        {
            try
            {
                var item = await _dbcontext.Products.Where(x => x.Id == model.Id && x.ProductType == model.ProductType)
                                    .Include(x => x.ProductDetails)
                                    .FirstOrDefaultAsync();

                foreach (var detail in item.ProductDetails)
                {
                    var found = model.ProductDetails.Where(x => x.Id == detail.Id).FirstOrDefault();
                    if (found is not null) //update existing detail
                    {
                        detail.Quantity = found.Quantity;
                    }
                    else // remove existing detail
                    {
                        item.ProductDetails.Remove(detail);
                    }
                }
                var newItems = model.ProductDetails.Where(x => x.Id == 0).ToList();
                foreach (var newItem in newItems)
                {
                    item.ProductDetails.Add(newItem); //add new detail
                }

                _dbcontext.Update(item);
                return await _dbcontext.SaveChangesAsync() > 0;

            }
            catch (Exception exp)
            {
                string m = exp.Message;
                string x = m;
            }
            return true;
        }
        public async Task<bool> Delete(Product model)
        {
            var data = await _dbcontext.Products.FindAsync(model.Id);
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
            var data = await _dbcontext.Products.FindAsync(id);
            if (data is not null)
            {
                data.ModifiedOn = DateTime.Now;
                data.Active = !data.Active;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<Product> UpdateDisplayOrder(int id, int num = 0)
        {
            var data = await _dbcontext.Products.FindAsync(id);
            if (data is not null)
            {
                data.DisplayOrder = num;
                data.ModifiedOn = DateTime.Now;
                await _dbcontext.SaveChangesAsync();
            }
            return data;
        }

        //lookup
        public async Task<CategoryAndItemSizeModel> GetAllCategoryItemSize()
        {
            CategoryAndItemSizeModel model = new();
            model.Category = await _dbcontext.Categories
            .Select(x => new CategoryModel() { Id = x.Id, NameEn = x.NameEn, NameAr = x.NameAr, Deleted = x.Deleted, Active = x.Active })
            .Where(x => x.Deleted == false && x.Active == true)
            .AsNoTracking().ToListAsync();
            model.ItemSize = await _dbcontext.ItemSizes
            .Select(x => new ItemSizeModel() { Id = x.Id, NameEn = x.NameEn, NameAr = x.NameAr, Deleted = x.Deleted, Active = x.Active })
            .Where(x => x.Deleted == false && x.Active == true)
            .AsNoTracking().ToListAsync();
            return model;
        }

        public async Task<ProductAndCategoryModel> GetAllProductForOfflineOrder(string productImagePath, string customerId)
        {
            ProductAndCategoryModel model = new();
            var customer = await _dbcontext.Customers.Where(x => x.Id.ToString() == customerId).AsNoTracking().FirstOrDefaultAsync();

            if (customer is not null)
            {

                model.Products = await _dbcontext.Products
                .Include(x => x.ItemSize)
                .Select(x =>
                new ProductSmallModel()
                {
                    Id = x.Id,
                    NameEn = x.NameEn,
                    NameAr = x.NameAr,
                    ImageUrl = (x.ImageName != null ? productImagePath + x.ImageName : null),
                    Price = x.GetPrice(customer.B2B),
                    DisplayOrder = x.DisplayOrder,
                    Active = x.Active,
                    ProductType = x.ProductType,
                    ItemSizeNameEn = x.ItemSize.NameEn + " (" + x.PiecesPerPacking + ")",
                    ItemSizeNameAr = x.ItemSize.NameAr + " (" + x.PiecesPerPacking + ")",
                    MaxQty = x.Stock,
                    Deleted = x.Deleted
                })
                .Where(x => x.Deleted == false && x.ProductType == ProductType.BaseProduct && x.Active == true)
                .OrderBy(x => x.DisplayOrder)
                .AsNoTracking().ToListAsync();
                //foreach(var item in model.Products)
                //{
                //    Price = customer.B2B ? (item.B2BPrice > 0 : item : x.Price,
                //    item.ItemSizeNameEn = item.NameEn + " (" + item.PiecesPerPacking + ")";
                //    item.ItemSizeNameAr = item.NameAr + " (" + item.PiecesPerPacking + ")";
                //}
                //model.Categories = await _dbcontext.Categories
                //.Select(x => new CategoryModel()
                //{
                //    Id = x.Id,
                //    NameEn = x.NameEn,
                //    NameAr = x.NameAr,
                //    DisplayOrder = x.DisplayOrder,
                //    Active = x.Active,
                //    Deleted = x.Deleted
                //})
                //.Where(x => x.Deleted == false && x.Active == true)
                //.OrderBy(x => x.DisplayOrder)
                //.AsNoTracking().ToListAsync();

            }
            return model;

        }

        public async Task<ProductAndCategoryModel> GetAllProductAndCategory(string productImagePath)
        {
            ProductAndCategoryModel model = new();
            model.Products = await _dbcontext.Products
            .Include(x => x.ItemSize)
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
                ProductType = x.ProductType,
                ItemSizeNameEn = x.ItemSize.NameEn + " (" + x.PiecesPerPacking + ")",
                ItemSizeNameAr = x.ItemSize.NameAr + " (" + x.PiecesPerPacking + ")",
                Deleted = x.Deleted
            })
            .Where(x => x.Deleted == false && x.ProductType == ProductType.BaseProduct && x.Active == true)
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

        //public async Task<ProductAndCategoryModel> GetAllProductAndCategory(string productImagePath)
        //{
        //    ProductAndCategoryModel model = new();
        //    model.Products = await _dbcontext.Products
        //                        .Select(x =>
        //                        new ProductSmallModel()
        //                        {
        //                            Id = x.Id,
        //                            NameEn = x.NameEn,
        //                            NameAr = x.NameAr,
        //                            ImageUrl = (x.ImageName != null ? productImagePath + x.ImageName : null),
        //                            Price = x.Price,
        //                            DisplayOrder = x.DisplayOrder,
        //                            Active = x.Active,
        //                            Deleted = x.Deleted
        //                        })
        //                        .Where(x => x.Deleted == false && x.Active == true)
        //                        .OrderBy(x => x.DisplayOrder)
        //                        .AsNoTracking().ToListAsync();

        //    model.Categories = await _dbcontext.Categories
        //                        .Select(x => new CategoryModel()
        //                        {
        //                            Id = x.Id,
        //                            NameEn = x.NameEn,
        //                            NameAr = x.NameAr,
        //                            DisplayOrder = x.DisplayOrder,
        //                            Active = x.Active,
        //                            Deleted = x.Deleted
        //                        })
        //                        .Where(x => x.Deleted == false && x.Active == true)
        //                        .OrderBy(x => x.DisplayOrder)
        //                        .AsNoTracking().ToListAsync();


        //    return model;

        //}
        #endregion

        #region Product Stock Management
        /* public async Task<bool> AddStockInfo(Product product, ProductActionType productActionType)
          {
              ProductStockHistory history = new();
              history.ProductType = product.ProductType;
              history.ProductId = product.Id;
              history.Price = product.Price;
              history.CreatedBy = product.CreatedBy;
              history.CreatedOn = product.CreatedOn;

              if (productActionType == ProductActionType.AddToStock)
              {
                  history.ProductUpdateType = ProductUpdateType.ManualAdjustment;
                  history.ProductActionType = ProductActionType.AddToStock;
                  history.InputStock = product.Stock;
                  history.OldStock = 0;
                  history.UpdatedStock = 0;
                  history.Note = "Received Shipment from "
                      /// Manual Adjustment->Add to Stock--> Received Shipment from Bahrain. Check Invoice Number 10002345. </td>

              }
              else  if (productActionType == ProductActionType.RemoveFromStock)
              {
                  history.ProductActionType = ProductActionType.AddToStock;
                  history.InputStock = product.Stock;
                  history.OldStock = 0;
                  history.UpdatedStock = 0;
              }

          }
          public async Task<bool> ProductAutomaticDeductionHistory(Product product, int inStock,int oldStock,int updateStock)
          {
              ProductStockHistory history = new();
              history.ProductType = product.ProductType;
              history.ProductId = product.Id;
              history.Price = product.Price;
              history.CreatedBy = 1;
              history.CreatedOn = DateTime.Now;
              history.ProductUpdateType = ProductUpdateType.AutomaticDeduction;
              history.ProductActionType = ProductActionType.RemoveFromStock;
              history.InputStock = inStock;
              history.OldStock = oldStock;
              history.UpdatedStock = 0;
              history.Note = "Product Created";
              await _dbcontext.ProductStockHistories.AddAsync(history);
              return await _dbcontext.SaveChangesAsync() > 0;
          }
          public async Task<bool> ProductCreationHistory(Product product)
          {
              ProductStockHistory history = new();
              history.ProductType = product.ProductType;
              history.ProductId = product.Id;
              history.Price = product.Price;
              history.CreatedBy = product.CreatedBy;
              history.CreatedOn = product.CreatedOn; 
              history.ProductUpdateType = ProductUpdateType.ProductCreation;
              history.ProductActionType = ProductActionType.SetStockTo;
              history.InputStock = product.Stock;
              history.OldStock = 0;
              history.UpdatedStock = 0;
              history.Note = "Product Created";
              await _dbcontext.ProductStockHistories.AddAsync(history);
              return await _dbcontext.SaveChangesAsync() > 0;
          }
        */
        public async Task<bool> ProductUpdateStock(ProductStockHistory ProductStockHistory)
        {
            // .Select(x => new Product() { Id = x.Id, Stock = x.Stock, Price = x.Price })
            var product = await _dbcontext.Products.Where(x => x.Id == ProductStockHistory.ProductId)
                                .FirstOrDefaultAsync();
            if (product is not null)
            {
                //update stock only
                product.Stock = ProductStockHistory.UpdatedStock;

                ProductStockHistory.Price = product.Price;
                ProductStockHistory.CreatedOn = DateTime.Now;
                await _dbcontext.ProductStockHistories.AddAsync(ProductStockHistory);
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }


        /// <summary>
        /// for normal product only (based Product)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<dynamic> ProductHistoryGetAllForDataTable(DataTableParam param, int productId)
        {
            DataTableResult<dynamic> result = new() { Draw = param.Draw };
            try
            {
                var items = _dbcontext.ProductStockHistories
                          .Include(x => x.SystemUser)
                          .Select(x => new ProductStockHistoryModel
                          {
                              Id = x.Id,
                              ProductId = x.ProductId,
                              ProductUpdateType = x.GetProductUpdateType(),
                              OldStock = x.OldStock,
                              ProductActionType = x.GetProductActionType(),
                              InputStock = x.InputStock,
                              UpdatedStock = x.UpdatedStock,
                              CreatedBy = x.CreatedBy != null ? x.SystemUser.FullName : "None",
                              CreatedOn = x.CreatedOn,
                              Note = x.Note != null ? x.Note : "",
                              Deleted = x.Deleted
                          }).Where(x => x.Deleted == false && x.ProductId == productId);

                //User Search
                if (!string.IsNullOrEmpty(param.SearchValue))
                {
                    var SearchValue = param.SearchValue.ToLower();
                    items = items.Where(obj =>
                     obj.ProductType.ToString().ToLower().Contains(SearchValue) ||
                     obj.ProductActionType.ToString().ToLower().Contains(SearchValue) ||
                     obj.OldStock.ToString().ToLower().Contains(SearchValue) ||
                     obj.InputStock.ToString().ToLower().Contains(SearchValue) ||
                     obj.UpdatedStock.ToString().Contains(SearchValue)
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
                return result;
            }
            catch (Exception exp)
            {
                ErrorMessage = exp.Message;
                result.Error = exp;
            }
            return result;
        }


        #endregion


        #region Product Stock Management       
        public async Task DeductStockQuantity(Product product, int stockNeedtoDeduct, RelatedEntityType relatedEntityType, int relatedEntityId)
        {
            int oldStock = product.Stock;
            int currentStock = oldStock - stockNeedtoDeduct;
            if (currentStock < 0)
                currentStock = 0;

            product.Stock = currentStock;
            _dbcontext.Products.Update(product);
            await _dbcontext.SaveChangesAsync();

            await _dbcontext.ProductStockHistories.AddAsync(new ProductStockHistory
            {
                ProductId = product.Id,
                ProductType = product.ProductType,
                ProductUpdateType = ProductUpdateType.AutomaticDeduction,
                ProductActionType = ProductActionType.RemoveFromStock,
                OldStock = oldStock,
                InputStock = stockNeedtoDeduct,
                UpdatedStock = product.Stock,
                Price = product.Price,
                CreatedOn = DateTime.Now,
                RelatedEntityTypeId = relatedEntityType,
                RelatedEntityId = relatedEntityId
            });
            await _dbcontext.SaveChangesAsync();
        }
        #endregion

        #region Misc
        public async Task<int> GetAvailableStockQuantity(int productId, int? customerId = null, string customerGuidValue = "")
        {
            int stockQuantity = 0;
            var heldCartQuantity = 0;

            //stock quantity
            var product = await _dbcontext.Products.Where(a => a.Active && !a.Deleted && a.Id == productId).FirstOrDefaultAsync();
            if (product != null)
            {
                stockQuantity = product.Stock;
            }

            //held cart quantity
            var cartItems = _dbcontext.CartItems.Where(a => DateTime.Now <= a.HoldUntil);

            if (customerId != null && customerId.Value > 0)
            {
                cartItems = cartItems.Where(a => a.CustomerId != customerId);
            }
            else if (!string.IsNullOrEmpty(customerGuidValue))
            {
                cartItems = cartItems.Where(a => a.CustomerGuidValue != customerGuidValue);
            }

            var data = from ci in cartItems.Where(a => a.ProductId == productId && a.Product.ProductType == ProductType.BaseProduct)
                       select new CartItem
                       {
                           Id = ci.Id,
                           Quantity = ci.Quantity,
                           ProductId = ci.ProductId
                       };

            var dataWithBundleProducts = from ci in cartItems.Where(a => a.Product.ProductType == ProductType.BundledProduct)
                                         join pd in _dbcontext.ProductDetails on ci.ProductId equals pd.ProductId
                                         where pd.ChildProductId == productId
                                         select new CartItem
                                         {
                                             Id = ci.Id,
                                             Quantity = ci.Quantity * pd.Quantity,
                                             ProductId = pd.ChildProductId
                                         };

            //held subscription product quantity
            var subscriptionHoldings = _dbcontext.SubscriptionHoldings.Where(a => DateTime.Now <= a.HoldUntil);

            if (customerId != null && customerId.Value > 0)
            {
                subscriptionHoldings = subscriptionHoldings.Where(a => a.CustomerId != customerId);
            }

            var dataWithSubscriptionProducts = from sh in subscriptionHoldings
                                               join pd in _dbcontext.ProductDetails on sh.ProductId equals pd.ProductId
                                               where pd.ChildProductId == productId
                                               select new CartItem
                                               {
                                                   Id = sh.Id,
                                                   Quantity = sh.Quantity * pd.Quantity,
                                                   ProductId = pd.ChildProductId
                                               };

            data = data.Concat(dataWithBundleProducts).Concat(dataWithSubscriptionProducts);
            heldCartQuantity = await data.SumAsync(a => a.Quantity);
            return stockQuantity - heldCartQuantity;
        }

        #endregion

        #region Favourites
        public async Task<IList<Favorite>> GetAllFavorite(int? customerId = null, int? productId = null)
        {
            var data = _dbcontext.Favorites.Include(a => a.Product).Where(x => x.Deleted == false);

            if (customerId != null)
            {
                data = data.Where(a => a.CustomerId == customerId);
            }

            if (productId != null && productId > 0)
            {
                data = data.Where(a => a.ProductId == productId);
            }

            return await data.ToListAsync();
        }
        public async Task<Favorite> GetFavoriteById(int id)
        {
            var data = await _dbcontext.Favorites.FindAsync(id);
            return data;
        }
        public async Task<Favorite> CreateFavorite(Favorite model)
        {
            await _dbcontext.Favorites.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
        public async Task<bool> UpdateFavorite(Favorite model)
        {
            _dbcontext.Update(model);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteFavorite(Favorite model)
        {
            model.Deleted = true;
            return await UpdateFavorite(model);
        }
        #endregion

        #region Availability notify request
        public async Task<ProductAvailabilityNotifyRequest> GetProductAvailabilityNotifyRequest(int customerId, int productId)
        {
            var data = await _dbcontext.ProductAvailabilityNotifyRequests
                .Include(a => a.Product)
                .Include(a => a.Customer)
                .Where(x => x.Deleted == false && x.CustomerId == customerId && x.ProductId == productId && !x.Send).FirstOrDefaultAsync();

            return data;
        }
        public async Task<ProductAvailabilityNotifyRequest> CreateProductAvailabilityNotifyRequest(ProductAvailabilityNotifyRequest model)
        {
            await _dbcontext.ProductAvailabilityNotifyRequests.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
        public async Task<bool> UpdateProductAvailabilityNotifyRequest(ProductAvailabilityNotifyRequest model)
        {
            _dbcontext.Update(model);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteProductAvailabilityNotifyRequest(ProductAvailabilityNotifyRequest model)
        {
            model.Deleted = true;
            return await UpdateProductAvailabilityNotifyRequest(model);
        }
        #endregion

        #region Subscription Duraion
        public async Task<IList<SubscriptionDuration>> GetAllSubscriptionDuration()
        {
            var data = await _dbcontext.SubscriptionDurations.Where(x => !x.Deleted && x.Active).AsNoTracking().ToListAsync();
            return data;
        }
        public async Task<SubscriptionDuration> GetSubscriptionDurationById(int id)
        {
            var data = await _dbcontext.SubscriptionDurations.FindAsync(id);
            return data;
        }
        #endregion

        #region Subscription Delivery Date
        public async Task<IList<SubscriptionDeliveryDate>> GetAllSubscriptionDeliveryDate()
        {
            var data = await _dbcontext.SubscriptionDeliveryDates.Where(x => !x.Deleted && x.Active).AsNoTracking().ToListAsync();
            return data;
        }
        public async Task<SubscriptionDeliveryDate> GetSubscriptionDeliveryDateById(int id)
        {
            var data = await _dbcontext.SubscriptionDeliveryDates.FindAsync(id);
            return data;
        }
        #endregion

    }
}
