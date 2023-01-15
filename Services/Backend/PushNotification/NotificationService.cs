using Data.EntityFramework;
using Data.NotifyTemplate;
using Data.PushNotification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Models.Admin.Notifications;

namespace Services.Backend.PushNotification
{
    public class NotificationService : INotificationService
    {
        protected readonly ApplicationDbContext _dbcontext;
        public NotificationService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region Device token
        public async Task<IList<DeviceToken>> GetAllDeviceToken(bool showHidden = false)
        {
            var data = await _dbcontext
                           .DeviceTokens
                           .Where(x => x.Deleted == false)
                           .ToListAsync();

            if (!showHidden)
            {
                data = data.Where(a => a.Active).ToList();
            }

            return data;
        }
        public async Task<DeviceToken> GetDeviceTokenByDeviceId(string deviceId)
        {
            var data = await _dbcontext.DeviceTokens.FirstOrDefaultAsync(x => x.DeviceId == deviceId);
            return data;
        }
        public async Task<IList<DeviceToken>> GetDeviceTokenByCustomerId(int customerId)
        {
            var result = await _dbcontext.DeviceTokens.Where(x => x.CustomerId == customerId).ToListAsync();
            return result;
        }
        public async Task<DeviceToken> CreateDeviceToken(DeviceToken model)
        {
            var oldDeviceToken = await _dbcontext.DeviceTokens.FirstOrDefaultAsync(x => x.DeviceId == model.DeviceId);
            if (oldDeviceToken == null)
            {
                model.CustomerId = model.CustomerId > 0 ? model.CustomerId : null;
                model.CreatedOn = DateTime.Now;
                await _dbcontext.DeviceTokens.AddAsync(model);
                await _dbcontext.SaveChangesAsync();
            }
            else
            {
                oldDeviceToken.CustomerId = model.CustomerId > 0 ? model.CustomerId : null;
                oldDeviceToken.Token = model.Token;
                oldDeviceToken.ModifiedOn = DateTime.Now;

                _dbcontext.DeviceTokens.Update(oldDeviceToken);
                await _dbcontext.SaveChangesAsync();
            }

            return model;
        }
        public async Task<bool> UpdateDeviceToken(DeviceToken model)
        {
            _dbcontext.Update(model);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        public async Task<bool> EnableDisableNotification(string deviceId)
        {
            var deviceToken = await _dbcontext.DeviceTokens.FirstOrDefaultAsync(x => x.DeviceId == deviceId);
            if (deviceToken is not null)
            {
                deviceToken.ModifiedOn = DateTime.Now;
                deviceToken.NotificationDisabled = deviceToken.NotificationDisabled is true ? false : true;
                _dbcontext.DeviceTokens.Update(deviceToken);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        #endregion

        #region Notification
        public async Task<IList<Notification>> GetAllNotification(bool showHidden = false, bool? isAdmin = null, int? customerId = null)
        {
            var data = await _dbcontext
                .Notifications
                .Where(x => x.Deleted == false)
                .ToListAsync();

            if (!showHidden)
            {
                data = data.Where(a => a.Active).ToList();
            }

            if (isAdmin.HasValue && isAdmin.Value)
            {
                data = data.Where(a => a.FromNotificationScreen).ToList();
            }

            if (customerId.HasValue && customerId.Value > 0)
            {
                var customer = await _dbcontext.Customers.Where(a => a.Id == customerId.Value).FirstOrDefaultAsync();
                if (customer != null)
                {
                    var generalNotifications = data.Where(a => !a.CustomerId.HasValue && (!customer.NotificationFromDate.HasValue || a.CreatedOn > customer.NotificationFromDate)).ToList();
                    data = data.Where(a => a.CustomerId.HasValue && a.CustomerId.Value == customerId.Value && (!customer.NotificationFromDate.HasValue || a.CreatedOn > customer.NotificationFromDate)).ToList();
                    data = data.Concat(generalNotifications).ToList();

                    var readedNotificationIds = await _dbcontext.NotificationReaders.Where(a => a.CustomerId == customerId.Value).Select(a => a.NotificationId).ToListAsync();
                    var notReadedNotifications = data.Where(a => !readedNotificationIds.Contains(a.Id)).ToList();
                    foreach (var notReadedNotification in notReadedNotifications)
                    {
                        var notificationReader = new NotificationReader();
                        notificationReader.NotificationId = notReadedNotification.Id;
                        notificationReader.CustomerId = customerId.Value;
                        notificationReader.CreatedOn = DateTime.Now;
                        await _dbcontext.NotificationReaders.AddAsync(notificationReader);
                        await _dbcontext.SaveChangesAsync();
                    }
                }
            }

            return data;
        }
        public async Task<int> GetNotificationCount(int customerId)
        {
            int count = 0;
            var customer = await _dbcontext.Customers.Where(a => a.Id == customerId).FirstOrDefaultAsync();
            if (customer != null)
            {
                var generalNotifications = await _dbcontext.Notifications.Where(a => !a.CustomerId.HasValue && (!customer.NotificationFromDate.HasValue || a.CreatedOn > customer.NotificationFromDate)).ToListAsync();

                var notifications = await _dbcontext.Notifications.Where(a => a.CustomerId.HasValue && a.CustomerId.Value == customerId && (!customer.NotificationFromDate.HasValue || a.CreatedOn > customer.NotificationFromDate)).ToListAsync();
                notifications = notifications.Concat(generalNotifications).ToList();

                var readedNotifications = await _dbcontext.NotificationReaders.Where(a => a.CustomerId == customerId).Select(a => a.NotificationId).ToListAsync();

                var notReadedNotifications = notifications.Where(a => !readedNotifications.Contains(a.Id)).ToList();
                count = notReadedNotifications.Count;
            }

            return count;
        }
        public async Task<Notification> GetNotificationById(int id)
        {
            var data = await _dbcontext.Notifications.FindAsync(id);
            return data;
        }
        public async Task<Notification> CreateNotification(Notification model)
        {
            await _dbcontext.Notifications.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
        public async Task<bool> UpdateNotification(Notification model)
        {
            _dbcontext.Update(model);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteNotification(Notification model)
        {
            model.Deleted = true;
            return await UpdateNotification(model);
        }
        #endregion

        #region AdminNotification
        //Task<AdminNotification> GetAdminNotificationDefault();
        //Task<AdminNotification> UpdateAdminNotification(AdminNotification model);

        public async Task<AdminNotificationTemplate> GetAdminNotificationDefault()
        {
            var adminNotification = await _dbcontext.AdminNotificationTemplates.AsNoTracking().FirstOrDefaultAsync();
            if (adminNotification is null)
            {
                adminNotification = await CreateAdminNotifications(new AdminNotificationTemplate());
            }
            return adminNotification;
        }

        public async Task<AdminNotificationTemplate> UpdateAdminNotification(AdminNotificationTemplate model)
        {
            var updateData = await _dbcontext.AdminNotificationTemplates.FirstOrDefaultAsync();
            if (updateData is not null)
            {
                updateData.LowStockEnabled = model.LowStockEnabled;
                updateData.LowStockThresholdQuantity = model.LowStockThresholdQuantity;
                updateData.LowStockToEmailAddress = model.LowStockToEmailAddress;
                updateData.LowStockCCEmailAddress = model.LowStockCCEmailAddress;
                updateData.NewOrderNotificationEnabled = model.NewOrderNotificationEnabled;
                updateData.NewOrderNotificationToEmailAddress = model.NewOrderNotificationToEmailAddress;

                updateData.NewOrderNotificationCCEmailAddress = model.NewOrderNotificationCCEmailAddress;
               
                //updateData.ModifiedBy = ;
                updateData.ModifiedOn = DateTime.Now;
                _dbcontext.Update(updateData);
                await _dbcontext.SaveChangesAsync();
            }
            else
            {
                AdminNotificationTemplate nmodel = new AdminNotificationTemplate();
                nmodel.LowStockEnabled = model.LowStockEnabled;
                nmodel.LowStockThresholdQuantity = model.LowStockThresholdQuantity;
                nmodel.LowStockToEmailAddress = model.LowStockToEmailAddress;
                nmodel.LowStockCCEmailAddress = model.LowStockCCEmailAddress;
                nmodel.NewOrderNotificationEnabled = model.NewOrderNotificationEnabled;
                nmodel.NewOrderNotificationToEmailAddress = model.NewOrderNotificationToEmailAddress;

                nmodel.NewOrderNotificationCCEmailAddress = model.NewOrderNotificationCCEmailAddress;

                updateData = await CreateAdminNotifications(nmodel);

            }
            return updateData;
        }
        public async Task<AdminNotificationTemplate> CreateAdminNotifications(AdminNotificationTemplate model)
        {
            model.CreatedOn = DateTime.Now;
            await _dbcontext.AdminNotificationTemplates.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }

        #endregion
    }
}
