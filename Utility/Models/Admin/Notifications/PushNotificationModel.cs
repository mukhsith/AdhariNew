using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;

namespace Utility.Models.Admin.Notifications
{
    public class PushNotificationModel
    {
        public int Id { get; set; }
        public string TitleEn { get; set; }

         public string TitleAr { get; set; }
        public string MessageEn { get; set; }
        public string MessageAr { get; set; }
        public bool FromNotificationScreen { get; set; }
        public string CustomerMobile { get; set; }
        public bool Sent { get; set; }
        public DateTime? SendDate { get; set; } = null;
        public List<AdminCustomerModel> Customer { get; set; }
    }
}
