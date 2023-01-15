using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;

namespace Utility.Models.Admin.Sales
{
    public class AdminSubscriptionModel
    {
        public int SubscriptionId { get; set; }
        public DateTime  CreatedOn { get; set; }
        public SubscriptionStatus SubscriptionStatusId { get; set; }
        public string SubscriptionNumber { get; set; }
        public int CustomerId { get; set; }
        public decimal Total { get; set; }
        public string FormattedTotal { get; set; }
        public int? PaymentMethodId { get; set; }
        public PaymentStatus? PaymentStatusId { get; set; }
        public bool Delivered { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}
