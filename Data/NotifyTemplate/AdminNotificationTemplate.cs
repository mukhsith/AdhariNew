﻿using Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;
using Utility.Helpers;

namespace Data.NotifyTemplate
{
    public class AdminNotificationTemplate : BaseEntityImage
    {
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
