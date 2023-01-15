using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;

namespace Utility.Models.Admin.Notifications
{
    public class AdminNotification
    {
        public int Id { get; set; }
        public bool LowStockEnabled { get; set; }
        public int LowStockThresholdQuantity { get; set; }
        public string LowStockToEmailAddress { get; set; }
        public string LowStockCCEmailAddress { get; set; }

        public bool NewOrderNotificationEnabled { get; set; }
        public string NewOrderNotificationToEmailAddress { get; set; }
        public string NewOrderNotificationCCEmailAddress { get; set; }

        public bool OrderConfirmationEnabled { get; set; }
        public string OrderConfirmationToEmailAddress { get; set; }
        public string OrderConfirmationCCEmailAddress { get; set; }

    }



}
