using System.Threading.Tasks;

namespace API.Helpers
{
    public interface IEmailHelper
    {
        Task SendEmail(string emailIds, string subject, string emailBody, bool htmlContent = false, string ccEmailIds = "",
            string bccEmailIds = "", string attachmentFilePaths = "", string attachmentFileNames = "");
    }
}
