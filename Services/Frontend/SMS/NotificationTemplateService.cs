using Data.EntityFramework;
using Data.NotifyTemplate;
using Data.SMS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Enum;

namespace Services.Frontend.SMS
{
    public class NotificationTemplateService : INotificationTemplateService
    {
        protected readonly ApplicationDbContext _dbcontext;
        public NotificationTemplateService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region Notification template
        public async Task<IList<NotificationTemplate>> GetAllNotificationTemplate()
        {
            var data = await _dbcontext
                           .NotificationTemplates.AsNoTracking().ToListAsync();
            return data;
        }
        public async Task<NotificationTemplate> GetNotificationTemplateById(int id)
        {
            var data = await _dbcontext.NotificationTemplates.FindAsync(id);
            return data;
        }
        public async Task<NotificationTemplate> GetNotificationTemplateByTypeId(NotificationType notificationType)
        {
            var data = await _dbcontext.NotificationTemplates.Where(a => a.TypeId == notificationType).FirstOrDefaultAsync();
            return data;
        }
        public async Task<NotificationTemplate> CreateNotificationTemplate(NotificationTemplate model)
        {
            await _dbcontext.NotificationTemplates.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
        public async Task<bool> UpdateNotificationTemplate(NotificationTemplate model)
        {
            _dbcontext.Update(model);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteNotificationTemplate(NotificationTemplate model)
        {
            model.Deleted = true;
            return await UpdateNotificationTemplate(model);
        }
        public async Task<bool> ToggleActiveNotificationTemplate(int id)
        {
            var data = await _dbcontext.NotificationTemplates.FindAsync(id);
            if (data is not null)
            {
                data.ModifiedOn = DateTime.Now;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }

        #endregion

        #region OTP details
        public async Task<OTPDetail> CreateOTPDetail(OTPDetail otpDetail, string message)
        {
            SMS_Push sms_Push = new SMS_Push();
            sms_Push.Push_ID = 0;
            sms_Push.Push_MessageID = 1;
            sms_Push.Push_Message = message.Replace("{OTP}", otpDetail.OTP);
            sms_Push.Push_ANI = otpDetail.Destination;
            sms_Push.Push_DNIS = _dbcontext.Company_SenderIDs.FirstOrDefault().SenderID_Text.Trim();
            sms_Push.Push_OperatorID = 2;
            sms_Push.Push_Lang = 0;
            sms_Push.Push_ScheduleDate = DateTime.Now;
            sms_Push.Push_Date = DateTime.Now;
            sms_Push.Push_Status = 0;

            await _dbcontext.SMS_Pushes.AddAsync(sms_Push);
            await _dbcontext.SaveChangesAsync();

            var sMS_Push = await _dbcontext.SMS_Pushes.FirstOrDefaultAsync(x => x.Id == sms_Push.Id);
            sms_Push.Push_ID = sms_Push.Id;
            _dbcontext.SMS_Pushes.Update(sms_Push);
            await _dbcontext.SaveChangesAsync();

            await _dbcontext.OTPDetails.AddAsync(otpDetail);
            await _dbcontext.SaveChangesAsync();

            return otpDetail;
        }
        public async Task<OTPDetail> GetOTPDetailByIdAndType(int id, int typeId)
        {
            var data = await _dbcontext.OTPDetails.FirstOrDefaultAsync(x => x.Id == id && x.Type == (NotificationType)typeId);
            return data;
        }
        public async Task<IList<OTPDetail>> GetOTPDetailByCustomerIdAndType(int customerId, int typeId)
        {
            var data = await _dbcontext.OTPDetails.Where(x => x.CustomerId == customerId && x.Type == (NotificationType)typeId).ToListAsync();
            return data;
        }
        public async Task<OTPDetail> GetOTPDetailById(int id)
        {
            var data = await _dbcontext.OTPDetails.FindAsync(id);
            return data;
        }
        #endregion

        #region SMS push
        public async Task CreateSMSPush(string message, string mobileNumber, int languageId = 0)
        {
            SMS_Push sms_Push = new();
            sms_Push.Push_ID = 0;
            sms_Push.Push_MessageID = 1;
            sms_Push.Push_Message = message;
            sms_Push.Push_ANI = mobileNumber;
            sms_Push.Push_DNIS = _dbcontext.Company_SenderIDs.FirstOrDefault().SenderID_Text.Trim();
            sms_Push.Push_OperatorID = 2;
            sms_Push.Push_Lang = (byte)languageId;
            sms_Push.Push_ScheduleDate = DateTime.Now;
            sms_Push.Push_Date = DateTime.Now;
            sms_Push.Push_Status = 0;

            await _dbcontext.SMS_Pushes.AddAsync(sms_Push);
            await _dbcontext.SaveChangesAsync();

            sms_Push = await _dbcontext.SMS_Pushes.FirstOrDefaultAsync(x => x.Id == sms_Push.Id);
            sms_Push.Push_ID = sms_Push.Id;
            _dbcontext.SMS_Pushes.Update(sms_Push);
            await _dbcontext.SaveChangesAsync();
        }

        //public Task<bool> UpdateDisplayOrderSMSTemplate(int id, int num = 0)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion
    }
}
