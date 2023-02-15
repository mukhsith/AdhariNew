using System.Collections.Generic;
using Utility.Models.Frontend.Content;
using Utility.Models.Frontend.CustomerManagement;

namespace Utility.Models.Frontend.CustomizedModel
{
    public class QuickPaymentModel
    {
        public string QuickPayNumber { get; set; }
        public decimal Amount { get; set; }
        public int PaymentMethodId { get; set; }
        public string FormattedAmount { get; set; }
        public int PaymentStatusId { get; set; }
        public int CustomerLanguageId { get; set; }
        public string CustomerIp { get; set; }
        public string MobileNumber { get; set; }
        public string Name { get; set; }
        public CustomerModel Customer { get; set; }
        public List<PaymentMethodModel> PaymentMethods { get; set; } = new();
        public List<KeyValuPairModel> PaymentSummary { get; set; } = new();
    }
}
