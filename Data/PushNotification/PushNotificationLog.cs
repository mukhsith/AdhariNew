using Data.Common;

namespace Data.PushNotification
{
    public partial class PushNotificationLog : BaseEntityCommon
    {
        public int NotificationId { get; set; }
        public int? CustomerId { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }
        public string MessageEn { get; set; }
        public string MessageAr { get; set; }
        public bool Sent { get; set; }
        public int CustomerLanguageId { get; set; }
        public string Token { get; set; }
    }
}
