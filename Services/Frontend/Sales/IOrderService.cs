using Data.Sales;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Enum;

namespace Services.Frontend.Sales
{
    public interface IOrderService
    {
        #region Order
        Task<List<Order>> GetAllOrder(int? customerId = null, PaymentStatus? paymentStatus = null, OrderStatus? orderStatus = null,
            string customerGuidValue = "", int? countryId = null);
        Task<Order> GetOrderById(int id);
        Task<Order> GetOrderByOrderNumber(string orderNumber);
        Task<Order> CreateOrder(Order model);
        Task<bool> UpdateOrder(Order model);
        Task<bool> DeleteOrder(Order model);
        Task UpdateOrderStatus(Order order, int orderStatusId);
        Task<int> GetOrderCountByCouponAndCustomer(int couponId, int? customerId = null, string customerGuidValue = "");
        Task<int> GetOrderCountByDeliveryTimeSlotId(int deliveryTimeSlotId, DateTime dateTime);
        Task<Order> GetLastOrderByCustomer(int customerId);
        #endregion

        #region Order items
        Task<List<OrderItem>> GetAllOrderItem(int orderId);
        #endregion
    }
}
