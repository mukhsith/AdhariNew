using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;

namespace Utility.Models.Admin.Notifications
{
    public class SMSNotificationModel
    {
        
        public int? Id { get; set; }
        public string Message { get; set; }
        public int CustomerLanguageId { get; set; }
        public string MobileNumber { get; set; }
        public int? CustomerId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public int ScheduleStatus { get; set; }
        public bool Sent { get; set; }
        
        public List<AdminCustomerModel> Customer { get; set; }
    }
}
