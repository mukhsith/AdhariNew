using Data.Sales;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;
using Utility.Models.Admin.Delivery;
using Utility.Models.Admin.Sales;

namespace Services.Backend.Sales
{
    public interface IOrderService
    {
        #region Order
//DataTableResult<List<DeliveriesDashboard>>
        Task<DataTableResult<List<DeliveriesDashboard>>> GetDispatchedOrdersDataTable(DataTableParam param, 
                                       string orderNumber = null,
                                       DateTime? startDate = null, int? orderMode = null, int? orderType = null,
                                       int? areaId = null, int? driverId = null);
//
        Task<DataTableResult<List<DeliveriesDashboard>>> GetNotDispatchedOrdersDataTable(DataTableParam param,
                                      string orderNumber = null,
                                      DateTime? startDate = null, int? orderMode = null, int? orderType = null,
                                      int? areaId = null, int? driverId = null);

        Task<DriverDeliverySummaryModel> GetDriverTodayDeliverySummary(int driverId);

        Task<AdminDeliverySummaryModel> GetTodayDeliverySummary();

        Task<DailyOrderSummaryModel> GetTodaySales();
        Task<DataTableResult<List<DeliveriesDashboard>>> GetAllOrdersForDeliveries(AdminOrderDeliveriesParam param);
        Task<dynamic> GetAllSalesOrders(AdminSalesOrderParam param);

        Task<dynamic> GetSalesOrderForDataTable(DataTableParam param, string orderNumber = null,
                                        DateTime? startDate = null, DateTime? endDate = null, string customerName = null,
                                        string mobileNumber = null, string customerEmail = null, int? paymentMethod = null, int? orderMode = null);

        Task<DataTableResult<List<DeliveriesDashboard>>> 
            GetAllForDeliveriesDataTable(DataTableParam param,  string orderNumber = null,
                                       DateTime? startDate = null, int? orderMode = null, int? orderType = null,
                                       int? areaId = null, int? driverId = null);
        
        Task<DataTableResult<List<DeliveriesDashboard>>>
          GetTodayDeliveriesDataTable(DataTableParam param, int  driverId);

        //Task<bool> UpdateOrderStatus(int orderId, OrderStatus orderStatusId);

        #endregion


        #region Order
        Task<List<Order>> GetAllOrder(int? customerId = null, PaymentStatus? paymentStatus = null, OrderStatus? orderStatus = null,
            string customerGuidValue = "", int? countryId = null);
        Task<Order> GetOrderById(int id);
        Task<bool> AddDriver(int id,int driverId);
        Task<bool> RemoveDriver(int id);
        //Task<bool> RescheduleDelivery(int id, DateTime? dateTime = null);
        Task<Order> GetOrderByOrderNumber(string orderNumber);
        Task<Order> CreateOrder(Order model);
        Task<bool> UpdateOrder(Order model);
        Task<bool> DeleteOrder(Order model);
        Task UpdateOrderStatus(Order order, int orderStatusId);
        Task<int> GetOrderCountByCouponAndCustomer(int couponId, int? customerId = null, string customerGuidValue = "");
        #endregion

        #region Order items
        Task<List<OrderItem>> GetAllOrderItem(int orderId);
        #endregion
    }
}
