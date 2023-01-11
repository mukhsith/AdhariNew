using Data.EntityFramework;
using Data.Sales;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Enum;

namespace Services.Frontend.Sales
{
    public class SubscriptionService : ISubscriptionService
    {
        protected readonly ApplicationDbContext _dbcontext;
        public SubscriptionService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region Subscription
        public async Task<List<Subscription>> GetAllSubscription(int? customerId = null, SubscriptionStatus? subscriptionStatus = null)
        {
            var data = _dbcontext
                           .Subscriptions
                           .Include(a => a.Product)
                           .Include(a => a.SubscriptionOrders)
                           .Where(x => x.Deleted == false);

            if (customerId != null && customerId.Value > 0)
            {
                data = data.Where(a => a.CustomerId == customerId);
            }

            if (subscriptionStatus != null)
            {
                data = data.Where(a => a.SubscriptionStatusId == subscriptionStatus);
            }

            return await data.AsNoTracking().ToListAsync();
        }
        public async Task<Subscription> GetSubscriptionById(int id)
        {
            var data = await _dbcontext.Subscriptions
                .Include(a => a.Product)
                .Include(a => a.Customer)
                .Include(a => a.Address).ThenInclude(a => a.Area).ThenInclude(a => a.Governorate)
                .Include(a => a.Coupon)
                .Include(a => a.SubscriptionItemDetails)
                .Include(a => a.SubscriptionOrders)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            return data;
        }
        public async Task<Subscription> GetSubscriptionBySubscriptionNumber(string subscriptionNumber)
        {
            var data = await _dbcontext.Subscriptions
                .Include(a => a.Product)
                .Include(a => a.Customer)
                .Include(a => a.Address).ThenInclude(a => a.Area).ThenInclude(a => a.Governorate)
                .Include(a => a.Coupon)
                .Include(a => a.SubscriptionItemDetails)
                .Include(a => a.SubscriptionOrders)
                .Where(a => a.SubscriptionNumber == subscriptionNumber)
                .FirstOrDefaultAsync();

            return data;
        }
        public async Task<Subscription> CreateSubscription(Subscription model)
        {
            await _dbcontext.Subscriptions.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
        public async Task<bool> UpdateSubscription(Subscription model)
        {
            _dbcontext.Update(model);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        public async Task UpdateSubscriptions(List<Subscription> models)
        {
            _dbcontext.UpdateRange(models);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task<bool> DeleteSubscription(Subscription model)
        {
            model.Deleted = true;
            return await UpdateSubscription(model);
        }
        public async Task UpdateSubscriptionStatus(Subscription subscription, int subscriptionStatusId)
        {
            subscription.SubscriptionStatusId = (SubscriptionStatus)subscriptionStatusId;
            _dbcontext.Subscriptions.Update(subscription);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task<int> GetSubscriptionCountByCouponAndCustomer(int couponId, int? customerId = null)
        {
            var data = _dbcontext.Subscriptions.Where(a => a.CouponId == couponId && a.PaidInitialPayment);

            if (customerId != null && customerId.Value > 0)
            {
                data = data.Where(a => a.CustomerId == customerId);
            }

            return await data.CountAsync();
        }
        public async Task<List<SubscriptionItemDetail>> GetAllSubscriptionItemDetail(int subscriptionId)
        {
            var data = _dbcontext
                           .SubscriptionItemDetails
                           .Include(a => a.Subscription)
                           .Where(x => x.Deleted == false && x.SubscriptionId == subscriptionId);

            return await data.AsNoTracking().ToListAsync();
        }
        public async Task<List<Subscription>> GetAllSubscriptionByNextExpectedDelivery(DateTime nextExpectedDelivery)
        {
            var data = await _dbcontext.Subscriptions
                             .Include(a => a.SubscriptionOrders)
                             .Where(a => !a.Deleted && a.SubscriptionStatusId == SubscriptionStatus.Confirmed
                                         && a.NextExpectedDelivery.Date == nextExpectedDelivery.Date
                                         && !a.SubscriptionOrders.Any(b => !b.Deleted && b.DeliveryDate.Month == nextExpectedDelivery.Month
                                                                          && b.DeliveryDate.Year == nextExpectedDelivery.Year)).ToListAsync();

            return data;
        }
        #endregion

        #region Subscription Order
        public async Task<List<SubscriptionOrder>> GetAllSubscriptionOrder(int? customerId = null)
        {
            var data = _dbcontext
                           .SubscriptionOrders.Include(a => a.Subscription)
                           .Where(x => x.Deleted == false);

            if (customerId != null && customerId.Value > 0)
            {
                data = data.Where(a => a.Subscription.CustomerId == customerId);
            }

            return await data.AsNoTracking().ToListAsync();
        }
        public async Task<SubscriptionOrder> GetSubscriptionOrderById(int id)
        {
            var data = await _dbcontext.SubscriptionOrders
                .Include(a => a.Subscription).ThenInclude(a => a.Product)
                .Include(a => a.Subscription).ThenInclude(a => a.SubscriptionItemDetails)
                .Include(a => a.Subscription).ThenInclude(a => a.Coupon)
                .Include(a => a.Subscription).ThenInclude(a => a.Customer)
                .Include(a => a.Subscription).ThenInclude(a => a.Address).ThenInclude(a => a.Area).ThenInclude(a => a.Governorate)
                .Include(a => a.PaymentMethod)
                .Include(a => a.DeliveryTimeSlot)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            return data;
        }
        public async Task<SubscriptionOrder> GetSubscriptionOrderByOrderNumber(string orderNumber)
        {
            var data = await _dbcontext.SubscriptionOrders
                .Include(a => a.Subscription).ThenInclude(a => a.Product)
                .Include(a => a.Subscription).ThenInclude(a => a.SubscriptionItemDetails)
                .Include(a => a.Subscription).ThenInclude(a => a.Coupon)
                .Include(a => a.Subscription).ThenInclude(a => a.Customer)
                .Include(a => a.Subscription).ThenInclude(a => a.Address).ThenInclude(a => a.Area).ThenInclude(a => a.Governorate)
                .Include(a => a.PaymentMethod)
                .Include(a => a.DeliveryTimeSlot)
                .Where(a => a.OrderNumber == orderNumber)
                .FirstOrDefaultAsync();

            return data;
        }
        public async Task<SubscriptionOrder> CreateSubscriptionOrder(SubscriptionOrder model)
        {
            await _dbcontext.SubscriptionOrders.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
        public async Task CreateSubscriptionOrders(List<SubscriptionOrder> models)
        {
            await _dbcontext.SubscriptionOrders.AddRangeAsync(models);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task<bool> UpdateSubscriptionOrder(SubscriptionOrder model)
        {
            _dbcontext.Update(model);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteSubscriptionOrder(SubscriptionOrder model)
        {
            model.Deleted = true;
            return await UpdateSubscriptionOrder(model);
        }
        public async Task<int> GetSubscriptionOrderCountByDeliveryTimeSlotId(int deliveryTimeSlotId, DateTime dateTime)
        {
            var data = await _dbcontext.SubscriptionOrders
                             .Where(a => a.DeliveryTimeSlotId == deliveryTimeSlotId && a.Confirmed && a.DeliveryDate == dateTime)
                             .CountAsync();

            return data;
        }
        #endregion
    }
}
