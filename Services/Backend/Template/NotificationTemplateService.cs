using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using System.Linq.Dynamic.Core;
using Data.NotifyTemplate;
using Data.SMS;
using Utility.Enum;

namespace Services.Backend.Template.Interface
{
    public class NotificationTemplateService : INotificationTemplateService
    {
        protected readonly ApplicationDbContext _dbcontext;
        protected string ErrorMessage = string.Empty;
        public NotificationTemplateService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region NotificationTemplate Service 

        public async Task<IEnumerable<NotificationTemplate>> GetAll()
        {
            IEnumerable<NotificationTemplate> items = await _dbcontext
                                           .NotificationTemplates
                                           .Where(x => x.Deleted == false)
                                           .AsNoTracking()
                                           .ToListAsync();
            return items;
            }


       public async Task<DataTableResult<List<NotificationTemplate>>> GetAllForDataTable(DataTableParam param )
        {
            DataTableResult<List<NotificationTemplate>> result = new() { Draw = param.Draw };
            try
            {
                var items = _dbcontext.NotificationTemplates 
                          .Where(x => x.Deleted == false);
                                  
                //User Search
                if (!string.IsNullOrEmpty(param.SearchValue))
                {
                    var SearchValue = param.SearchValue.ToLower();
                    items = items.Where(obj =>
                     obj.Title.ToLower().Contains(SearchValue)  
                      
                     );
                }

                //Sorting
                if (!string.IsNullOrEmpty(param.SortColumn) && !string.IsNullOrEmpty(param.SortColumnDirection))
                {
                    //using System.Linq.Dynamic.Core;
                    //NEEDS TO BE INSTALLED FROM NUGET PACKAGE MANAGER
                    items = items.OrderBy(param.SortColumn + " " + param.SortColumnDirection);//.ToList();
                }
                else
                {
                    items = items.OrderBy(x => x.DisplayOrder);
                }
                result.RecordsTotal = items.Count();
                result.RecordsFiltered = items.Count();
                result.Data = await items.Skip(param.Skip).Take(param.PageSize).ToListAsync();
                
                return result;
            } catch (Exception err) {
                result.Error = err;
            }
            return result;
        }

        public async Task<NotificationTemplate> GetNotificationTemplateByTypeId(NotificationType notificationType)
        {
            var data = await _dbcontext.NotificationTemplates.Where(a => a.TypeId == notificationType).FirstOrDefaultAsync();
            return data;
        }

        public async Task<NotificationTemplate> GetById(int id)
        {
            var data = await _dbcontext.NotificationTemplates.FindAsync(id);
            return data;
        }
        
        public async Task<NotificationTemplate> Create(NotificationTemplate model)
        {
            model.CreatedOn = DateTime.Now;
            await _dbcontext.NotificationTemplates.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }

        public async Task<bool> Update(NotificationTemplate model)
        {
            var updateData = await _dbcontext.NotificationTemplates.FindAsync(model.Id);
            if (updateData is not null)
            {
                updateData.SMSEnabled = model.SMSEnabled;
                updateData.SMSMessageEn = model.SMSMessageEn;
                updateData.SMSMessageAr = model.SMSMessageAr;

                updateData.EmailEnabled = model.EmailEnabled;
                updateData.EmailMessageEn = model.EmailMessageEn;
                updateData.EmailMessageAr = model.EmailMessageAr;

                updateData.PushEnabled = model.PushEnabled;
                updateData.PushMessageEn = model.PushMessageEn;
                updateData.PushMessageAr = model.PushMessageAr;

                updateData.ModifiedOn = DateTime.Now;
            }
           _dbcontext.Update(updateData);
           return await _dbcontext.SaveChangesAsync() > 0;
              
        }
 
        public async Task<bool> Delete(NotificationTemplate model)
        {
            var data = await _dbcontext.NotificationTemplates.FindAsync(model.Id);
            if (data is not null)
            {
                data.ModifiedOn = DateTime.Now;
                data.Deleted = true;
                _dbcontext.Update(data);
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<bool> ToggleActive(int id)
        {
            var data = await _dbcontext.NotificationTemplates.FindAsync(id);
            if (data is not null)
            {
                data.ModifiedOn = DateTime.Now;
                data.Active = !data.Active;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async Task<NotificationTemplate> UpdateDisplayOrder(int id, int num = 0)
        {
            var data = await _dbcontext.NotificationTemplates.FindAsync(id);
            if (data is not null)
            {
                data.DisplayOrder = num;
                data.ModifiedOn = DateTime.Now;
                await _dbcontext.SaveChangesAsync();
            }
            return data;
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
        public async Task<bool> CreateSMSPush(string message, string mobileNumber, int languageId = 0)
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
            return await _dbcontext.SaveChangesAsync()>1;
        }


        public async Task<bool> CreateSMSNotification(string message, string mobileNumber, int languageId = 0)
        {
            SMSNotification smsNotification = new();
            smsNotification.Message = message;
            smsNotification.CustomerLanguageId = (byte)languageId;
            smsNotification.CreatedOn = DateTime.Now;

            smsNotification.ScheduleDate = DateTime.Now; 
          

            await _dbcontext.SMSNotifications.AddAsync(smsNotification);
            //await _dbcontext.SaveChangesAsync();

            return await _dbcontext.SaveChangesAsync() > 1;
        }


        public async Task CreateQpaySMSPush(string message, string mobileNumber, int languageId = 0)
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


        }
        //public Task<bool> UpdateDisplayOrderSMSTemplate(int id, int num = 0)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion
    }
}
