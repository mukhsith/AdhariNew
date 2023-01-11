using System.Collections.Generic;
using Utility.Models.Frontend.CouponPromotion;
using Utility.Models.Frontend.CustomerManagement;
using Utility.Models.Frontend.CustomizedModel;

namespace Utility.Models.Frontend.Sales
{
    public class WalletPackageOrderModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string OrderNumber { get; set; }
        public decimal Amount { get; set; }
        public string FormattedAmount { get; set; }
        public decimal WalletAmount { get; set; }
        public string FormattedWalletAmount { get; set; }
        public string FormattedDate { get; set; }
        public string FormattedTime { get; set; }
        public string PaymentResult { get; set; }
        public string PaymentId { get; set; }
        public string PaymentRefId { get; set; }
        public string PaymentTrackId { get; set; }
        public int PaymentStatusId { get; set; }
        public WalletPackageModel WalletPackage { get; set; }
        public Content.PaymentMethodModel PaymentMethod { get; set; }
        public CustomerModel Customer { get; set; }
        public List<KeyValuPairModel> PaymentSummary { get; set; } = new();
    }
}
