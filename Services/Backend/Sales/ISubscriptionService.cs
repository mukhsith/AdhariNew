using Data.Sales;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;
using Utility.Models.Admin.Sales;

namespace Services.Backend.Sales
{
    public interface ISubscriptionService
    {

        #region Admin  
        Task<Subscription> UpdateStatus(int id, SubscriptionStatus subscriptionStatus);
        Task<List<SubscriptionOrder>> GetOrdersBySubscriptionId(int id);
        Task<Subscription> GetSubscriptionById(int id);
        Task<List<Subscription>> GetAllSubscription(int? customerId = null, SubscriptionStatus? subscriptionStatus = null);
        Task<Subscription> GetSubscriptionBySubscriptionNumber(string subscriptionNumber);
        
        Task<SubscriptionOrder> GetSubscriptionOrderById(int id);

        Task<bool> UpdateOrderPaymentStatus(SubscriptionOrder order, bool isDelivered, int paymentStatusId);

        Task<DailySubscriptionSummaryModel> GetSubscriptionTodaySales();
        Task<DataTableResult<List<AdminSubscriptionModel>>> GetSubscriptions(AdminSubscriptionOrderParam param);
        Task<dynamic> GetSubscriptionSalesOrders(AdminSubscriptionOrderParam param);
        #endregion
    }
}
