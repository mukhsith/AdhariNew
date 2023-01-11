using Data.Common;
using Data.CustomerManagement;

namespace Data.PushNotification
{
    public partial class DeviceToken : BaseEntityCommon
    {
        public int DeviceTypeId { get; set; }
        public string DeviceId { get; set; }
        public string Token { get; set; }
        public int PushCounter { get; set; }
        public bool Logged { get; set; }
        public bool NotificationDisabled { get; set; }
        public int LanguageId { get; set; }
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
