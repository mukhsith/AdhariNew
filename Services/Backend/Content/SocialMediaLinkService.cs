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

namespace Services.Backend.Content.Interface
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
        public async Task<SocialMediaLink> Create(int UserId)
        {
            var item = new SocialMediaLink()
            {
                FacebookLink = "",
                InstagramLink = "",
                TwitterLink = "",
                YoutubeLink = "",
                WhatsAppLink = "",
                SnapchatLink = "",
                TiktokLink = "",
            CreatedBy = UserId,
                CreatedOn = DateTime.Now
            };
            var newItem = _dbcontext.SocialMediaLinks.Add(item);
            await _dbcontext.SaveChangesAsync();
            return newItem.Entity;
        }
        public async Task<bool> Edit(SocialMediaLink item)
        {
            var data = await _dbcontext.SocialMediaLinks.FirstOrDefaultAsync();
            if (data is not null)
            {
                data.FacebookLink = item.FacebookLink;
                data.InstagramLink = item.InstagramLink;
                data.TwitterLink = item.TwitterLink;
                data.YoutubeLink = item.YoutubeLink;
                data.WhatsAppLink = item.WhatsAppLink;
                data.SnapchatLink = item.SnapchatLink;
                data.TiktokLink = item.TiktokLink;

                data.ModifiedBy = item.ModifiedBy;
                data.ModifiedOn = DateTime.Now;
                
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
         
        #endregion

    }
}
