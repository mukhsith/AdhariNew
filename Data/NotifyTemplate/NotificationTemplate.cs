using Data.Common;
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

    public class NotificationTemplate : BaseEntityImage
    {
        public string Title { get; set; }
        public NotificationType TypeId { get; set; } // only for information, login, register, order confirmation
        public bool SMSEnabled { get; set; }
        public string SMSMessageEn { get; set; }
        public string SMSMessageAr { get; set; }

        public bool EmailEnabled { get; set; }
        public string EmailSubjectEn { get; set; }
        public string EmailSubjectAr { get; set; }
        public string EmailMessageEn { get; set; }
        public string EmailMessageAr { get; set; }

        public bool PushEnabled { get; set; }
        public string PushMessageEn { get; set; }
        public string PushMessageAr { get; set; }
    }
}
