using System.Collections.Generic;
using Utility.Models.Frontend.CustomerManagement;
using Utility.Models.Frontend.CustomizedModel;
using Utility.Models.Frontend.ProductManagement;

namespace Utility.Models.Frontend.Sales
{
    public class SubscriptionAdminModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string SubscriptionNumber { get; set; }
        public string SubscriptionTitle { get; set; }
        public string FormattedMonthlyAmount { get; set; }
        public int Quantity { get; set; }
        public string Duration { get; set; }
        public string DeliveryDate { get; set; }
        public string FormattedSubTotal { get; set; }
        public string FormattedDeliveryFee { get; set; }
        public string CouponCode { get; set; }
        public string FormattedCouponDiscountAmount { get; set; }
        public string FormattedCashbackAmount { get; set; }
        public string FormattedWalletUsedAmount { get; set; }
        public decimal Total { get; set; }
        public string FormattedTotal { get; set; }
        public string FormattedDate { get; set; }
        public string FormattedTime { get; set; }       
        public AddressModel Address { get; set; }
        public CustomerModel Customer { get; set; }
        public ProductModel Product { get; set; }
        public List<KeyValuPairModel> SubscriptionPackTitles { get; set; } = new();
        public List<SubscriptionOrderAdminModel> SubscriptionOrders { get; set; } = new();


       //new addition from Frontend model
        public decimal UnitPrice { get; set; } 
        public string PaymentResult { get; set; }
        public string PaymentId { get; set; }
        public string PaymentRefId { get; set; }
        public string PaymentTrackId { get; set; }
        public int PaymentStatusId { get; set; }
        public string SubscriptionStatusName { get; set; }
        public string SubscriptionStatusColor { get; set; }
        public Content.PaymentMethodModel PaymentMethod { get; set; }
        public List<KeyValuPairModel> AmountSplitUps { get; set; } = new();
        public List<KeyValuPairModel> SubscriptionDetails { get; set; } = new();
        public List<KeyValuPairModel> PaymentSummary { get; set; } = new();
    }
}
