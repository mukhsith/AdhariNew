using Data.Common;
using Data.CustomerManagement;

namespace Data.PushNotification
{
    public partial class NotificationReader : BaseEntityDate
    {        
        public int NotificationId { get; set; }
        public int CustomerId { get; set; }
        public virtual Notification Notification { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
