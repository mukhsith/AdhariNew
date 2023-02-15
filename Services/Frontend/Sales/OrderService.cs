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
    public class OrderService : IOrderService
    {
        protected readonly ApplicationDbContext _dbcontext;
        public OrderService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region Order
        public async Task<List<Order>> GetAllOrder(int? customerId = null, PaymentStatus? paymentStatus = null, OrderStatus? orderStatus = null,
            string customerGuidValue = "", int? countryId = null)
        {
            var data = _dbcontext
                           .Orders
                           .Where(x => x.Deleted == false);

            if (customerId != null && customerId.Value > 0)
            {
                data = data.Where(a => a.CustomerId == customerId);
            }

            if (paymentStatus != null)
            {
                data = data.Where(a => a.PaymentStatusId == paymentStatus);
            }

            if (orderStatus != null)
            {
                data = data.Where(a => a.OrderStatusId == orderStatus);
            }

            return await data.AsNoTracking().ToListAsync();
        }
        public async Task<Order> GetOrderById(int id)
        {
            var data = await _dbcontext.Orders
                .Include(a => a.Customer)
                .Include(a => a.Address).ThenInclude(a => a.Area).ThenInclude(a => a.Governorate)
                .Include(a => a.Coupon)
                .Include(a => a.OrderItems).ThenInclude(a => a.OrderItemDetails)
                .Include(a => a.OrderItems).ThenInclude(a => a.Product)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            return data;
        }
        public async Task<Order> GetOrderByOrderNumber(string orderNumber)
        {
            var data = await _dbcontext.Orders
                .Include(a => a.Customer)
                .Include(a => a.Address).ThenInclude(a => a.Area).ThenInclude(a => a.Governorate)
                .Include(a => a.Coupon)
                .Include(a => a.OrderItems).ThenInclude(a => a.OrderItemDetails)
                .Include(a => a.OrderItems).ThenInclude(a => a.Product)
                .Where(a => a.OrderNumber == orderNumber)
                .FirstOrDefaultAsync();

            return data;
        }
        public async Task<Order> CreateOrder(Order model)
        {
            await _dbcontext.Orders.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
        public async Task<bool> UpdateOrder(Order model)
        {
            _dbcontext.Update(model);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteOrder(Order model)
        {
            model.Deleted = true;
            return await UpdateOrder(model);
        }
        public async Task UpdateOrderStatus(Order order, int orderStatusId)
        {
            order.OrderStatusId = (OrderStatus)orderStatusId;
            _dbcontext.Orders.Update(order);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task<int> GetOrderCountByCouponAndCustomer(int couponId, int? customerId = null, string customerGuidValue = "")
        {
            var data = _dbcontext.Orders.Where(a => a.CouponId == couponId && a.PaymentStatusId == PaymentStatus.Captured);

            if (customerId != null && customerId.Value > 0)
            {
                data = data.Where(a => a.CustomerId == customerId);
            }

            return await data.CountAsync();
        }
        public async Task<int> GetOrderCountByDeliveryTimeSlotId(int deliveryTimeSlotId, DateTime dateTime)
        {
            var data = await _dbcontext.Orders
                             .Where(a => a.DeliveryTimeSlotId == deliveryTimeSlotId && a.PaymentStatusId == PaymentStatus.Captured &&
                             a.DeliveryDate.Date == dateTime.Date)
                             .CountAsync();

            return data;
        }
        public async Task<Order> GetLastOrderByCustomer(int customerId)
        {
            var data = await _dbcontext.Orders.OrderByDescending(a => a.Id).
                FirstOrDefaultAsync(x => x.CustomerId == customerId);
            return data;
        }
        #endregion

        #region Order items
        public async Task<List<OrderItem>> GetAllOrderItem(int orderId)
        {
            var data = _dbcontext
                           .OrderItems
                           .Include(a => a.Order)
                           .Where(x => x.Deleted == false && x.OrderId == orderId);

            return await data.AsNoTracking().ToListAsync();
        }
        #endregion
    }
}
