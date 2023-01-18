using Data.Common;
using Data.CustomerManagement;
using System;
using System.ComponentModel.DataAnnotations;
using Utility.Helpers;

namespace Data.SMS
{
    public partial class SMSNotification : BaseEntityCommon
    {
        [StringLength(Constants.ExtraLargeDescriptionSize)]
        public string Message { get; set; }
        public int CustomerLanguageId { get; set; }
        public int? CustomerId { get; set; }

        [StringLength(Constants.MobileDataSize)]
        public string MobileNumber { get; set; }
        public DateTime ScheduleDate { get; set; }
        public int ScheduleStatus { get; set; }
        public bool Sent { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
