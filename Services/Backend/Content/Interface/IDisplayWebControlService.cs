using Data.Content;
using Data.SystemUserManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API;
using Utility.Enum; 

namespace Services.Backend.Content.Interface
{
    public interface IDisplayWebControlService
    {
        #region Display Web Control Service 
        Task<DisplayWebControl> GetDefaultOrUpdate(AppContentType appContentType, bool active); 
        Task<DisplayWebControl> Create(AppContentType appContentType,bool active); 
         //Task<bool> ToggleActive(int id); 
        #endregion
        
        
    }
}
