using Data.Content;
using Data.EntityFramework;
using Data.SystemUserManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
 
using System.Threading.Tasks; 

namespace Services.Frontend.Content.Interface
{
    public class SocialMediaLinkService : ISocialMediaLinkService
    {
        protected readonly ApplicationDbContext _dbcontext;
        protected string ErrorMessage = string.Empty;
        public SocialMediaLinkService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        #region Social Media Link Service  
        public async Task<SocialMediaLink> GetDefault()
        {
            var data = await _dbcontext.SocialMediaLinks.AsNoTracking().FirstOrDefaultAsync();
            return data;
        }

        

        #endregion

    }
}
