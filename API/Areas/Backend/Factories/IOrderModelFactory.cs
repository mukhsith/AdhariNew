using Data.Sales;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;
using Utility.Models.Admin.Delivery;
using Utility.Models.Admin.Sales;
using Utility.Models.Frontend.Sales;
using Utility.ResponseMapper;

namespace API.Areas.Backend.Factories
{
    public interface IOrderModelFactory
    {
        Task<OrderModel> PrepareOrder(bool isEnglish, int id);

        Task<DailyOrderSummaryModel> GetTodaySales(bool isEnglish);
        Task<DailySubscriptionSummaryModel> GetTodaySubscriptionSales(bool isEnglish);

        Task<DailySubscriptionSummaryModel> GetCustomerSales(bool isEnglish, int customerId);

        //Task<APIResponseModel<AdminCreateOrderModel>> CreateOrder(bool isEnglish, int customerId, DeviceType deviceTypeId, AdminCreateOrderModel adminCreateOrderModel);
        Task<APIResponseModel<AdminCreateOrderModel>> CreateOrder(bool isEnglish, int customerId, DeviceType deviceTypeId, AdminCreateOrderModel createPaymentModel);
        Task<APIResponseModel<OrderModel>> GetOrder(bool isEnglish, int id, int customerId);
        //Task<APIResponseModel<AdminOrderModel>> GetOrder(bool isEnglish, int id, int customerId);
        //Task<APIResponseModel<List<AdminOrderModel>>> GetOrders(bool isEnglish, int customerId, int id = 0, string orderNumber = "",
        //   int limit = 0, int page = 0, OrderStatus? orderStatus = null);
        Task<APIResponseModel<List<OrderModel>>> GetOrders(bool isEnglish, int customerId, int id = 0, string orderNumber = "",
            int limit = 0, int page = 0, OrderStatus? orderStatus = null);
        Task<APIResponseModel<bool>> ReOrder(bool isEnglish, int customerId, int id);
        Task<bool> UpdateOrderStatus(int orderId, OrderStatus orderStatusId, bool refundDeliveryFee = false,string notes=null);
        Task<bool> UpdateDriverOrderStatus(int orderId, int orderType, OrderStatus orderStatusId, bool refundDeliveryFee = false, string notes = "");
        //Task<bool> AddDriver(int orderId, int driverId, int OrderType);

        Task<APIResponseModel<bool>> AddQPay(int CustomerId, int orderID, string OrderNumber, decimal Ordertotal, int OrderType);


        Task<bool> RemoveDriver(int orderId);
        Task<bool> RescheduleDelivery(int orderId, DateTime? dateTime = null);


        Task<bool> RescheduleAdminDelivery(int orderId, int OrderTypeId, DateTime? dateTime = null);

        //Task<DataTableResult<List<DeliveriesDashboard>>> GetNotDispatchedDataTable(
        //    DataTableParam param, bool isEnglish, string orderNumber = null,  DateTime? startDate = null, 
        //    int? orderModeId = null, int? orderTypeId = null, int? areaId = null, int? driverId = null);

        //Task<DataTableResult<List<DeliveriesDashboard>>> GetDispatchedDataTable(
        //    DataTableParam param, bool isEnglish, string orderNumber = null, DateTime? startDate = null, 
        //    int? orderModeId = null, int? orderTypeId = null, int? areaId = null, int? driverId = null);

        Task<DataTableResult<List<DeliveriesDashboard>>> GetDeliveriesForDataTable(AdminOrderDeliveriesParam param);

        Task<DailySubscriptionSummaryModel> GetCustomerSummary(AdminOrderDeliveriesParam param);

    }
}
