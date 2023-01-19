using System.Collections.Generic;
using Utility.Models.Frontend.CustomizedModel;

namespace Utility.Models.Frontend.Sales
{
    public class SubscriptionPaymentModel
    {
        public string Title { get; set; }
        public List<KeyValuPairModel> SubscriptionPayment { get; set; } = new();
    }
}
