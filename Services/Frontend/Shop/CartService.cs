using Data.EntityFramework;
using Data.Shop;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Frontend.Shop
{
    public class CartService : ICartService
    {
        protected readonly ApplicationDbContext _dbcontext;
        public CartService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region Cart
        public async Task<IList<CartItem>> GetAllCartItem(string customerGuidValue = "", int? customerId = null,
            int? productDetailId = null)
        {
            if (string.IsNullOrEmpty(customerGuidValue) && (customerId == null || customerId == 0))
            {
                return new List<CartItem>();
            }

            var data = _dbcontext.CartItems
                           .Include(a => a.Product)
                           .Where(x => x.Deleted == false);

            if (customerId != null && customerId.Value > 0)
            {
                data = data.Where(a => a.CustomerId == customerId);
            }
            else if (!string.IsNullOrEmpty(customerGuidValue))
            {
                data = data.Where(a => a.CustomerGuidValue == customerGuidValue);
            }

            if (productDetailId != null)
            {
                data = data.Where(a => a.ProductId == productDetailId);
            }

            return await data.ToListAsync();
        }
        public async Task<CartItem> GetCartItemById(int id)
        {
            var data = await _dbcontext.CartItems.Where(a => a.Id == id).FirstOrDefaultAsync();
            return data;
        }
        public async Task<CartItem> CreateCartItem(CartItem model)
        {
            await _dbcontext.CartItems.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
        public async Task<bool> UpdateCartItem(CartItem model)
        {
            _dbcontext.CartItems.Update(model);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteCartItem(CartItem model)
        {
            _dbcontext.CartItems.Remove(model);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteCartItems(IList<CartItem> models)
        {
            _dbcontext.CartItems.RemoveRange(models);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        public async Task DeleteCartItemByCustomer(string customerGuidValue = "", int? customerId = null)
        {
            List<CartItem> data = null;
            if (customerId != null && customerId.Value > 0)
            {
                data = await _dbcontext.CartItems.Where(x => x.CustomerId == customerId).ToListAsync();

            }
            else if (!string.IsNullOrEmpty(customerGuidValue))
            {
                data = await _dbcontext.CartItems.Where(x => x.CustomerGuidValue == customerGuidValue).ToListAsync();
            }

            if (data != null && data.Count > 0)
            {
                _dbcontext.CartItems.RemoveRange(data);
                await _dbcontext.SaveChangesAsync();
            }
        }
        public async Task HoldAndReleaseCartItem(bool isHold, int? customerId = null, string customerGuidValue = "")
        {
            IList<CartItem> cartItems = new List<CartItem>();
            if (customerId.HasValue && customerId.Value > 0)
            {
                cartItems = await _dbcontext.CartItems.Where(a => a.CustomerId == customerId.Value).ToListAsync();
            }
            else if (!string.IsNullOrEmpty(customerGuidValue))
            {
                cartItems = await _dbcontext.CartItems.Where(a => a.CustomerGuidValue == customerGuidValue).ToListAsync();
            }

            foreach (var cartItem in cartItems)
            {
                cartItem.ModifiedOn = DateTime.Now;
                if (isHold)
                    cartItem.HoldUntil = DateTime.Now.AddMinutes(5);
                else
                    cartItem.HoldUntil = DateTime.Now;
            }

            _dbcontext.CartItems.UpdateRange(cartItems);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task<List<CartItem>> GetHeldCartItems(int? customerId = null, string customerGuidValue = "")
        {
            var cartItems = _dbcontext.CartItems.Where(a => DateTime.Now <= a.HoldUntil);

            if (customerId != null && customerId.Value > 0)
            {
                cartItems = cartItems.Where(a => a.CustomerId != customerId);
            }
            else if (!string.IsNullOrEmpty(customerGuidValue))
            {
                cartItems = cartItems.Where(a => a.CustomerGuidValue != customerGuidValue);
            }

            var data = from ci in cartItems
                       select new CartItem
                       {
                           Quantity = ci.Quantity,
                           ProductId = ci.ProductId
                       };

            data = data.GroupBy(a => a.ProductId).Select(a => new CartItem
            {
                ProductId = a.Key,
                Quantity = a.Sum(b => b.Quantity),
            });

            return await data.AsNoTracking().ToListAsync();
        }
        #endregion

        #region Cart attributes
        public async Task<List<CartAttribute>> GetAllCartAttribute(string customerGuidValue = "", int? customerId = null)
        {
            var data = await _dbcontext.CartAttributes.ToListAsync();

            if (customerId != null && customerId.Value > 0)
            {
                data = data.Where(a => a.CustomerId == customerId).ToList();
            }

            return data;
        }
        public async Task<CartAttribute> GetCartAttributeById(int Id)
        {
            var data = await _dbcontext.CartAttributes.FirstOrDefaultAsync(x => x.Id == Id);
            return data;
        }
        public async Task<CartAttribute> CreateCartAttribute(CartAttribute model)
        {
            await _dbcontext.CartAttributes.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
        public async Task<bool> UpdateCartAttribute(CartAttribute model)
        {
            _dbcontext.CartAttributes.Update(model);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteCartAttribute(CartAttribute model)
        {
            _dbcontext.CartAttributes.Remove(model);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteCartAttributeByCustomer(string customerGuidValue = "", int? customerId = null)
        {
            List<CartAttribute> data = null;
            if (customerId != null && customerId.Value > 0)
            {
                data = await _dbcontext.CartAttributes.Where(x => x.CustomerId == customerId).ToListAsync();
            }

            if (data != null && data.Count > 0)
            {
                _dbcontext.CartAttributes.RemoveRange(data);
                return await _dbcontext.SaveChangesAsync() > 0;
            }

            return false;
        }
        #endregion

        #region Subscription attributes
        public async Task<SubscriptionAttribute> GetSubscriptionAttributeByCustomerId(int customerId)
        {
            var data = await _dbcontext.SubscriptionAttributes.Where(a => a.CustomerId == customerId).FirstOrDefaultAsync();
            return data;
        }
        public async Task<SubscriptionAttribute> GetSubscriptionAttributeByCustomer(string customerGuidValue = "", int? customerId = null)
        {
            if (string.IsNullOrEmpty(customerGuidValue) && (customerId == null || customerId == 0))
            {
                return new SubscriptionAttribute();
            }

            var data = _dbcontext.SubscriptionAttributes.AsQueryable();

            if (customerId != null && customerId.Value > 0)
            {
                data = data.Where(a => a.CustomerId == customerId);
            }
            else if (!string.IsNullOrEmpty(customerGuidValue))
            {
                data = data.Where(a => a.CustomerGuidValue == customerGuidValue);
            }

            return await data.FirstOrDefaultAsync();
        }
        public async Task<SubscriptionAttribute> GetSubscriptionAttributeById(int Id)
        {
            var data = await _dbcontext.SubscriptionAttributes.FirstOrDefaultAsync(x => x.Id == Id);
            return data;
        }
        public async Task<SubscriptionAttribute> CreateSubscriptionAttribute(SubscriptionAttribute model)
        {
            await _dbcontext.SubscriptionAttributes.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
        public async Task<bool> UpdateSubscriptionAttribute(SubscriptionAttribute model)
        {
            _dbcontext.SubscriptionAttributes.Update(model);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteSubscriptionAttribute(SubscriptionAttribute model)
        {
            _dbcontext.SubscriptionAttributes.Remove(model);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteSubscriptionAttributeByCustomer(int? customerId = null)
        {
            List<SubscriptionAttribute> data = null;
            if (customerId != null && customerId.Value > 0)
            {
                data = await _dbcontext.SubscriptionAttributes.Where(x => x.CustomerId == customerId).ToListAsync();
            }

            if (data != null && data.Count > 0)
            {
                _dbcontext.SubscriptionAttributes.RemoveRange(data);
                return await _dbcontext.SaveChangesAsync() > 0;
            }

            return false;
        }
        #endregion

        #region Subscription holding
        public async Task<IList<SubscriptionHolding>> GetAllSubscriptionHolding(int? customerId = null,
            int? productDetailId = null)
        {
            var data = _dbcontext.SubscriptionHoldings
                           .Include(a => a.Product)
                           .Where(x => x.Deleted == false);

            if (customerId != null && customerId.Value > 0)
            {
                data = data.Where(a => a.CustomerId == customerId);
            }

            if (productDetailId != null)
            {
                data = data.Where(a => a.ProductId == productDetailId);
            }

            return await data.ToListAsync();
        }
        public async Task<SubscriptionHolding> GetSubscriptionHoldingById(int id)
        {
            var data = await _dbcontext.SubscriptionHoldings.Where(a => a.Id == id).FirstOrDefaultAsync();
            return data;
        }
        public async Task<SubscriptionHolding> CreateSubscriptionHolding(SubscriptionHolding model)
        {
            await _dbcontext.SubscriptionHoldings.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
        public async Task<bool> UpdateSubscriptionHolding(SubscriptionHolding model)
        {
            _dbcontext.SubscriptionHoldings.Update(model);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteSubscriptionHolding(SubscriptionHolding model)
        {
            _dbcontext.SubscriptionHoldings.Remove(model);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteSubscriptionHoldings(IList<SubscriptionHolding> models)
        {
            _dbcontext.SubscriptionHoldings.RemoveRange(models);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        public async Task DeleteSubscriptionHoldingByCustomer(string customerGuidValue = "", int? customerId = null)
        {
            List<SubscriptionHolding> data = null;
            if (customerId != null && customerId.Value > 0)
            {
                data = await _dbcontext.SubscriptionHoldings.Where(x => x.CustomerId == customerId).ToListAsync();
            }

            if (data != null && data.Count > 0)
            {
                _dbcontext.SubscriptionHoldings.RemoveRange(data);
                await _dbcontext.SaveChangesAsync();
            }
        }
        public async Task HoldAndReleaseSubscriptionHolding(bool isHold, int? customerId = null, string customerGuidValue = "")
        {
            IList<SubscriptionHolding> subscriptionHoldings = new List<SubscriptionHolding>();
            if (customerId.HasValue && customerId.Value > 0)
            {
                subscriptionHoldings = await _dbcontext.SubscriptionHoldings.Where(a => a.CustomerId == customerId.Value).ToListAsync();
            }

            foreach (var subscriptionHolding in subscriptionHoldings)
            {
                subscriptionHolding.ModifiedOn = DateTime.Now;
                if (isHold)
                    subscriptionHolding.HoldUntil = DateTime.Now.AddMinutes(5);
                else
                    subscriptionHolding.HoldUntil = DateTime.Now;
            }

            _dbcontext.SubscriptionHoldings.UpdateRange(subscriptionHoldings);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task<List<SubscriptionHolding>> GetHeldSubscriptionHoldings(int? customerId = null, string customerGuidValue = "")
        {
            var subscriptionHoldings = _dbcontext.SubscriptionHoldings.Where(a => DateTime.Now <= a.HoldUntil);

            if (customerId != null && customerId.Value > 0)
            {
                subscriptionHoldings = subscriptionHoldings.Where(a => a.CustomerId != customerId);
            }

            var data = from sh in subscriptionHoldings
                       select new SubscriptionHolding
                       {
                           Quantity = sh.Quantity,
                           ProductId = sh.ProductId
                       };

            data = data.GroupBy(a => a.ProductId).Select(a => new SubscriptionHolding
            {
                ProductId = a.Key,
                Quantity = a.Sum(b => b.Quantity),
            });

            return await data.AsNoTracking().ToListAsync();
        }
        #endregion
    }
}
