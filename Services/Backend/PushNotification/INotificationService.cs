﻿
using Data.PushNotification;
using System.Collections.Generic;
using System.Threading.Tasks; 
namespace Services.Backend.PushNotification
{
    public interface INotificationService
    {
        #region Device token
        Task<IList<DeviceToken>> GetAllDeviceToken(bool showHidden = false);
        Task<DeviceToken> GetDeviceTokenByDeviceId(string deviceId);
        Task<IList<DeviceToken>> GetDeviceTokenByCustomerId(int customerId);
        Task<DeviceToken> CreateDeviceToken(DeviceToken model);
        Task<bool> UpdateDeviceToken(DeviceToken model);
        Task<bool> EnableDisableNotification(string deviceId);
        #endregion

        #region Notification
        Task<IList<Notification>> GetAllNotification(bool showHidden = false, bool? isAdmin = null, int? customerId = null);
        Task<int> GetNotificationCount(int customerId);
        Task<Notification> GetNotificationById(int id);
        Task<Notification> CreateNotification(Notification model);
        Task<bool> UpdateNotification(Notification model);
        Task<bool> DeleteNotification(Notification model);
        #endregion
    }
}
