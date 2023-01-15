using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Models.Admin.Delivery
{

     public class AdminDeliverySummaryModel
    {
        public int DeliveriesDue { get; set; }
        public int OrderDeliveries { get; set; }
        public int SubscriptionDeliveries { get; set; }
        
    }
}
