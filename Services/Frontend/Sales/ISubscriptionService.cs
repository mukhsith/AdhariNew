using Data.Sales;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Enum;

namespace Services.Frontend.Sales
{
    public interface ISubscriptionService
    {
        #region Subscription
        Task<List<Subscription>> GetAllSubscription(int? customerId = null, SubscriptionStatus? subscriptionStatus = null);
        Task<Subscription> GetSubscriptionById(int id);
        Task<Subscription> GetSubscriptionBySubscriptionNumber(string subscriptionNumber);
        Task<Subscription> CreateSubscription(Subscription model);
        Task<bool> UpdateSubscription(Subscription model);
        Task UpdateSubscriptions(List<Subscription> models);
        Task<bool> DeleteSubscription(Subscription model);
        Task UpdateSubscriptionStatus(Subscription subscription, int subscriptionStatusId);
        Task<int> GetSubscriptionCountByCouponAndCustomer(int couponId, int? customerId = null);
        Task<List<SubscriptionItemDetail>> GetAllSubscriptionItemDetail(int subscriptionId);
        Task<List<Subscription>> GetAllSubscriptionByNextExpectedDelivery(DateTime nextExpectedDelivery);
        Task<Subscription> GetLastSubscriptionByCustomer(int customerId);
        #endregion

        #region Subscription Order
        Task<List<SubscriptionOrder>> GetAllSubscriptionOrder(int? customerId = null);
        Task<SubscriptionOrder> GetSubscriptionOrderById(int id);
        Task<SubscriptionOrder> GetSubscriptionOrderByOrderNumber(string orderNumber);
        Task<SubscriptionOrder> CreateSubscriptionOrder(SubscriptionOrder model);
        Task CreateSubscriptionOrders(List<SubscriptionOrder> models);
        Task<bool> UpdateSubscriptionOrder(SubscriptionOrder model);
        Task<bool> DeleteSubscriptionOrder(SubscriptionOrder model);
        Task<int> GetSubscriptionOrderCountByDeliveryTimeSlotId(int deliveryTimeSlotId, DateTime dateTime);
        #endregion
    }
}
