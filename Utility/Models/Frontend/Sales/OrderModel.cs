using System.Collections.Generic;
using Utility.Helpers;
using Utility.Models.Admin.Sales;
using Utility.Models.Frontend.CustomerManagement;
using Utility.Models.Frontend.CustomizedModel;

namespace Utility.Models.Frontend.Sales
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string OrderNumber { get; set; }
        public string EstimatedDelivery { get; set; }
        public string EstimatedDeliveryWithoutHeading { get; set; }
        public string FormattedSubTotal { get; set; }
        public string FormattedDeliveryFee { get; set; }
        public string FormattedCouponDiscountAmount { get; set; }
        public string FormattedCashbackAmount { get; set; }
        public string FormattedWalletUsedAmount { get; set; }
        public decimal Total { get; set; }
        public string FormattedTotal { get; set; }
        public string FormattedDate { get; set; }
        public string FormattedTime { get; set; }
        public string PaymentResult { get; set; }
        public string PaymentId { get; set; }
        public string PaymentRefId { get; set; }
        public string PaymentTrackId { get; set; }
        public string PaymentAuth { get; set; }
        public string PaymentTransId { get; set; }
        public int PaymentStatusId { get; set; }
        public string FormattedItemCount { get; set; }
        public string OrderStatusName { get; set; }
        public string OrderStatusColor { get; set; }
        public int CustomerLanguageId { get; set; }
        public AddressModel Address { get; set; }
        public Content.PaymentMethodModel PaymentMethod { get; set; }
        public CustomerModel Customer { get; set; }
        public List<KeyValuPairModel> AmountSplitUps { get; set; } = new();
        public List<KeyValuPairModel> PaymentSummary { get; set; } = new();
        public List<KeyValuPairModel> PaymentSummaryForPrint { get; set; } = new();
        public List<KeyValuPairModel> PaymentDetails { get; set; } = new();
        public List<KeyValuPairModel> OrderDetails { get; set; } = new();
        public List<OrderItemModel> OrderItems { get; set; } = new();

        #region admin
        public string OrderTypeText { get; set; } = Messages.Online;
        public AdminOrderSummaryModel OrderSummary { get; set; }
        public string OrderNotes { get; set; }
        #endregion
    }
}
