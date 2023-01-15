using Data.Common;
using System;
using Utility.Enum;

namespace Data.EmailManagement
{
    public partial class QueuedEmail : BaseEntityDate
    {
        public NotificationType NotificationTypeId { get; set; }
        public string From { get; set; }
        public string DisplayName { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public bool BodyHtml { get; set; }
        public string Body { get; set; }
        public string AttachmentFilePaths { get; set; }
        public string AttachmentFileNames { get; set; }
        public DateTime? SentOn { get; set; }
        public string ErrorMessage { get; set; }
    }
}
