using System.Threading.Tasks;
using Utility.Enum;

namespace API.Helpers
{
    public interface IEmailHelper
    {
        Task SendEmail(NotificationType notificationTypeId, string emailIds, string subject, string emailBody, bool htmlContent = false, string ccEmailIds = "",
            string bccEmailIds = "", string attachmentFilePaths = "", string attachmentFileNames = "");
    }
}
