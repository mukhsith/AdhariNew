using Data.CouponPromotion;
using Data.CustomerManagement;
using Data.ProductManagement;
using Data.Sales;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Enum;
using Utility.Models.Frontend.Sales;

namespace API.Helpers
{
    public interface ICommonHelper
    {
        #region Utilities
        Task<string> ConvertDecimalToString(decimal value, bool isEnglish, int countryId = 0, bool? includeZero = false);
        Tuple<string, string> GetOrderStatusNameAndColorCode(OrderStatus statusId, bool isEnglish);
        string GetOrderStatusName(OrderStatus statusId, bool isEnglish);
        Tuple<string, string> GetSubscriptionStatusNameAndColorCode(SubscriptionStatus statusId, bool isEnglish);
        string GetPaymentResultTitle(PaymentStatus PaymentStatusId, bool isEnglish);
        Tuple<string, string> GetPaymentResultNameAndColorCode(PaymentStatus PaymentStatusId, bool isEnglish);
        string GetAddressTypeTitle(AddressType addressType, bool isEnglish);
        string GetTimeAgo(DateTime dateTime, bool isEnglish);
        #endregion

        #region Token
        string CreateAccessToken(dynamic customer, out string expiration);
        int GetCustomerIdByToken(string token);
        #endregion

        #region Validation
        string CouponValidation(Coupon coupon, bool isEnglish, decimal total);
        #endregion

        #region Delivery
        Task<decimal> GetDeliveryFeeByAreaId(int areaId);
        Task<string> PrepareAddressText(Address address, bool isEnglish);
        Task<Tuple<DateTime, int>> GetAvailableDeliveryDateAndSlot();
        Task<Tuple<DateTime, int>> GetAvailableSubscriptionOrderDeliveryDateAndSlot(SubscriptionDeliveryDate subscriptionDeliveryDate);
        Task<bool> CheckAvailableDeliveryDate(DateTime dateTime);
        #endregion

        #region Promotion
        Task<decimal> GetCashbackAmount(int customerId, decimal amount);
        Task RedeemCashbackAmount(int customerId, decimal amount);
        #endregion

        #region Order 
        string GetOrderPdfUrl(OrderModel order, string apiBaseUrl, bool isEnglish);
        Task<string> GetOrderFrontPdfUrl(OrderModel orderModel, bool isEnglish);
        Task<string> GetOrderBackendPdfUrl(OrderModel orderModel, bool isEnglish);
        string GetOrderDotMatrixUrl(OrderModel order, string apiBaseUrl, bool isEnglish);
        Task<bool> UpdateOrderStatus(Order order, OrderStatus orderStatusId, bool refundDeliveryFee = false, string notes = "");
        Task<bool> RescheduleOrderDelivery(Order order, DateTime? newDeliveryDate = null);
        #endregion

        #region Subscription
        Task MigrateSubscriptionAttribute(string customerGuidValue, int customerId);
        string GetSubscriptionPdfUrl(SubscriptionModel order, string apiBaseUrl, bool isEnglish);
        Task<string> GetSubscriptionFrontPdfUrl(SubscriptionModel subscriptionModel, bool isEnglish);
        Task<bool> UpdateSubscriptionOrderStatus(Subscription subscription, SubscriptionStatus subscriptionStatus, bool refundDeliveryFee = false, string notes = "");
        Task<bool> RescheduleSubscriptionOrderDelivery(SubscriptionOrder subscriptionOrder, DateTime? newDeliveryDate = null);
        #endregion

        #region Cart
        Task MigrateCarts(string customerGuidValue, int customerId, bool clearexistingCartItems = false);
        #endregion

        #region Notifications
        Task SendOrderSMSNotification(OrderModel orderModel, bool isEnglish);
        Task SendSubscriptionSMSNotification(SubscriptionModel subscriptionModel, bool isEnglish);
        Task SendOrderEmailNotification(OrderModel orderModel, bool isEnglish);
        Task SendSubscriptionEmailNotification(SubscriptionModel subscriptionModel, bool isEnglish);
        Task SendOrderAdminEmailNotification(OrderModel orderModel, bool isEnglish, string emailIds, string ccEmailIds);
        Task SendSubscriptionAdminEmailNotification(SubscriptionModel subscriptionModel, bool isEnglish, string emailIds, string ccEmailIds);
        Task SendLowStockEmailNotification(List<Product> products, bool isEnglish, string emailIds, string ccEmailIds);
        #endregion
    }
}
