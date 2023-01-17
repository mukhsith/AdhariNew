using Data.EmailManagement;
using Microsoft.Extensions.Options;
using Services.Frontend.EmailManagement;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;

namespace API.Helpers
{
    public class EmailHelper : IEmailHelper
    {
        private readonly AppSettingsModel _appSettings;
        private readonly IQueuedEmailService _queuedEmailService;
        public EmailHelper(IOptions<AppSettingsModel> options,
            IQueuedEmailService queuedEmailService)
        {
            _appSettings = options.Value;
            _queuedEmailService = queuedEmailService;
        }
        public async Task SendEmail(NotificationType notificationTypeId, string emailIds, string subject, string emailBody, bool htmlContent = false, string ccEmailIds = "",
            string bccEmailIds = "", string attachmentFilePaths = "", string attachmentFileNames = "")
        {
            string from = _appSettings.EmailFromAddress;
            string smtp = _appSettings.EmailSMTP;
            string displayName = _appSettings.EmailDisplayName;

            var arrEmailIds = emailIds.Split(',');
            var arrCCEmailIds = ccEmailIds.Split(',');
            var arrBCCEmailIds = bccEmailIds.Split(',');
            var arrAttachmentFilePaths = attachmentFilePaths.Split(',');
            var arrAttachmentFileNames = attachmentFileNames.Split(',');

            var queuedEmail = new QueuedEmail
            {
                NotificationTypeId = notificationTypeId,
                From = from,
                DisplayName = displayName,
                To = emailIds,
                CC = ccEmailIds,
                Bcc = bccEmailIds,
                Subject = subject,
                BodyHtml = htmlContent,
                Body = emailBody,
                AttachmentFilePaths = attachmentFilePaths,
                AttachmentFileNames = attachmentFileNames
            };
            queuedEmail = await _queuedEmailService.CreateQueuedEmail(queuedEmail);

            MailMessage message = new()
            {
                IsBodyHtml = htmlContent,
                Subject = subject,
                Body = emailBody,
                From = new MailAddress(from, displayName)
            };

            foreach (var emailId in arrEmailIds)
            {
                if (!string.IsNullOrEmpty(emailId))
                    message.To.Add(emailId);
            }

            foreach (var ccEmailId in arrCCEmailIds)
            {
                if (!string.IsNullOrEmpty(ccEmailId))
                    message.CC.Add(ccEmailId);
            }

            foreach (var bccEmailId in arrBCCEmailIds)
            {
                if (!string.IsNullOrEmpty(bccEmailId))
                    message.Bcc.Add(bccEmailId);
            }

            for (int i = 0; i < arrAttachmentFilePaths.Length; i++)
            {
                if (!string.IsNullOrEmpty(arrAttachmentFilePaths[i]))
                {
                    var attachment = new Attachment(arrAttachmentFilePaths[i]);
                    if (arrAttachmentFileNames.Length == arrAttachmentFilePaths.Length)
                    {
                        attachment.Name = arrAttachmentFileNames[i];
                    }
                    message.Attachments.Add(attachment);
                }
            }

            SmtpClient smtpClient = new SmtpClient(smtp, _appSettings.EmailPortNo);
            smtpClient.EnableSsl = _appSettings.EmailSSLEnabled;
            smtpClient.UseDefaultCredentials = _appSettings.EmailUseDefaultCredentials;
            smtpClient.Credentials = new NetworkCredential(from, _appSettings.EmailPassword);

            if (smtp.ToString().ToUpper().Contains("GMAIL"))
            {
                smtpClient.Timeout = 300000;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            }

            try
            {
                if (_appSettings.SendInBackground)
                {
                    Task t1 = Task.Run(() => smtpClient.Send(message));
                }
                else
                {
                    smtpClient.Send(message);
                }

                queuedEmail.SentOn = DateTime.Now;
                await _queuedEmailService.UpdateQueuedEmail(queuedEmail);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;

                var innerException = ex.InnerException;
                if (innerException != null)
                {
                    do
                    {
                        errorMessage = errorMessage + ", " + innerException.Message;
                        innerException = innerException.InnerException;
                    }
                    while (innerException != null);
                }

                queuedEmail.ErrorMessage = errorMessage;
                await _queuedEmailService.UpdateQueuedEmail(queuedEmail);
            }
        }
    }
}
