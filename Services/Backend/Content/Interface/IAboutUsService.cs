using Data.Content;
using Data.SystemUserManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API;
using Utility.Backend.Models.SystemUserManagement;

namespace Services.Backend.Content.Interface
{
    public interface IAboutUsService
    {
        #region  AboutUs Service    
        Task<AboutUs> GetDefault(); 
        Task<bool> Update(AboutUs model);
         
        #endregion
        
        
    }
}
