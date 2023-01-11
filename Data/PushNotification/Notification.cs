using Data.Common;
using Data.CustomerManagement;
using System.ComponentModel.DataAnnotations;
using Utility.Helpers;

namespace Data.PushNotification
{
    public partial class Notification : BaseEntityCommon
    {        
        [StringLength(Constants.TitleDataSize)]
        public string TitleEn { get; set; }

        [StringLength(Constants.TitleDataSize)]
        public string TitleAr { get; set; }
        public string MessageEn { get; set; }
        public string MessageAr { get; set; }
        public bool FromNotificationScreen { get; set; }
        public int? CustomerId { get; set; }
        public bool Sent { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
