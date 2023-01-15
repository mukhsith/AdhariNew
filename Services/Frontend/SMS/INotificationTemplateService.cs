using Data.NotifyTemplate;
using Data.SMS;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Enum;

namespace Services.Frontend.SMS
{
    public interface INotificationTemplateService
    {
        #region Notification template
        Task<IList<NotificationTemplate>> GetAllNotificationTemplate();
        Task<NotificationTemplate> GetNotificationTemplateById(int id);
        Task<NotificationTemplate> GetNotificationTemplateByTypeId(NotificationType notificationType);
        Task<NotificationTemplate> CreateNotificationTemplate(NotificationTemplate model);
        Task<bool> UpdateNotificationTemplate(NotificationTemplate model);
        Task<bool> DeleteNotificationTemplate(NotificationTemplate model);
        Task<bool> ToggleActiveNotificationTemplate(int id);

        #endregion

        #region OTP details
        Task<OTPDetail> CreateOTPDetail(OTPDetail otpDetail, string message);
        Task<OTPDetail> GetOTPDetailByIdAndType(int id, int typeId);
        Task<IList<OTPDetail>> GetOTPDetailByCustomerIdAndType(int customerId, int typeId);
        Task<OTPDetail> GetOTPDetailById(int id);
        #endregion

        #region SMS push
        Task CreateSMSPush(string message, string mobileNumber, int languageId = 0);
        #endregion
    }
}
