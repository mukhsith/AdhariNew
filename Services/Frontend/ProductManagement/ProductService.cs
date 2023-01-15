using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Data.ProductManagement;
using Utility.Enum;
using Data.Shop;

namespace Services.Frontend.ProductManagement.Interface
{
    public class ProductService : IProductService
    {
        protected readonly ApplicationDbContext _dbcontext;
        protected string ErrorMessage = string.Empty;
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

            if (favorite)
            {
                var favoriteProductIds = (await GetAllFavorite(customerId: customerId)).GroupBy(a => a.ProductId).Select(a => a.Key).ToList();
                data = data.Where(a => favoriteProductIds.Contains(a.Id));
            }

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
        public async Task<IList<ProductDetail>> GetAllProductDetail(int productId)
        {
            var data = _dbcontext.ProductDetails.Where(x => !x.Deleted && x.ProductId == productId);

            return await data.AsNoTracking().ToListAsync();
        }
        public async Task<List<Product>> GetAllLowStockProduct(int lowStockThreshold)
        {
            var data = _dbcontext.Products.Where(x => !x.Deleted && x.Active && x.ProductType == ProductType.BaseProduct && x.Stock <= lowStockThreshold);
            return await data.AsNoTracking().ToListAsync();
        }
        #endregion

        #region Product Stock Management       
        public async Task AdjustStockQuantity(Product product, int stock, RelatedEntityType relatedEntityType, int relatedEntityId,
            ProductActionType productActionType)
        {
            int oldStock = product.Stock;
            int currentStock = 0;
            if (productActionType == ProductActionType.AddToStock)
            {
                currentStock = oldStock + stock;
            }
            else if (productActionType == ProductActionType.RemoveFromStock)
            {
                currentStock = oldStock - stock;
            }
            if (productActionType == ProductActionType.SetStockTo)
            {
                currentStock = stock;
            }

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
                ProductActionType = productActionType,
                OldStock = oldStock,
                InputStock = stock,
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

            //held subscription quantity
            var subscriptionHoldings = _dbcontext.SubscriptionHoldings.Where(a => a.CustomerId != customerId && DateTime.Now <= a.HoldUntil);
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
