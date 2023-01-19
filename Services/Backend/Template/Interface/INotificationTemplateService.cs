
using Data.NotifyTemplate;
using Data.SMS;
using System.Collections.Generic; 
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum;

namespace Services.Backend.Template.Interface
{
     
    public interface INotificationTemplateService
    {
        #region   NotificationTemplate Service  
        Task<IEnumerable<NotificationTemplate>> GetAll();
        Task<DataTableResult<List<NotificationTemplate>>> GetAllForDataTable(DataTableParam param);

        Task<NotificationTemplate> GetNotificationTemplateByTypeId(NotificationType notificationType);

        Task<NotificationTemplate> GetById(int id);
        Task<NotificationTemplate> Create(NotificationTemplate model);
        Task<bool> Update(NotificationTemplate model);
        Task<bool> Delete(NotificationTemplate model);
        Task<bool> ToggleActive(int id);
        Task<NotificationTemplate> UpdateDisplayOrder(int id, int num = 0);
        #endregion

        #region OTP details
        Task<OTPDetail> CreateOTPDetail(OTPDetail otpDetail, string message);
        Task<OTPDetail> GetOTPDetailByIdAndType(int id, int typeId);
        Task<IList<OTPDetail>> GetOTPDetailByCustomerIdAndType(int customerId, int typeId);
        Task<OTPDetail> GetOTPDetailById(int id);
        #endregion

        #region SMS push
        Task<bool> CreateSMSPush(string message, string mobileNumber, int languageId = 0);

        Task<bool> CreateSMSNotification(string message, string mobileNumber, int languageId = 0);

        Task CreateQpaySMSPush(string message, string mobileNumber, int languageId = 0);
        #endregion
    }
}
