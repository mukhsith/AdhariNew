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
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using Utility.Backend.Models.SystemUserManagement;
using Utility.Enum;
using Utility.Helpers;
using System.Linq.Dynamic.Core;
using NLog.Layouts;

namespace Services.Backend.Content.Interface
{
    public class AboutUsService : IAboutUsService
    {
        protected readonly ApplicationDbContext _dbcontext;
        protected string ErrorMessage = string.Empty;
        public AboutUsService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

         
        #region About Service  
        public async Task<AboutUs> GetDefault()
        {
            var data = await _dbcontext.AboutUs.FirstOrDefaultAsync();
            if(data is not null)
            {
                data.Active =await GetDisplayWebControl(data);
            }
            return data;
        }
        

        public async Task<bool> Update(AboutUs model)
        {
            var updateData = await GetDefault();
            if (updateData is not null)
            {
                
                updateData.TextEn = model.TextEn;
                updateData.TextAr = model.TextAr;
                updateData.ModifiedBy = model.ModifiedBy;
                updateData.ModifiedOn = DateTime.Now;

                //set status for DisplayWebControl table
                updateData.Active = model.Active;
                _dbcontext.Update(updateData);

                //update web control active status
                await UpdateDisplayWebControl(updateData);


            } else
            {
                await _dbcontext.AddAsync(model);
            }
           
           return await _dbcontext.SaveChangesAsync() > 0;
              
        }

        private async Task<bool> GetDisplayWebControl(AboutUs item)
        {
            var webcontrol = await _dbcontext.DisplayWebControls.Where(x => x.ControlId == item.Id).AsNoTracking().FirstOrDefaultAsync();
            if(webcontrol is not null)
            {
                return webcontrol.Active;
            }
            return false;
        }

        private async Task<bool> UpdateDisplayWebControl(AboutUs item)
        {
            var webcontrol = await _dbcontext.DisplayWebControls.Where(x => x.ControlId == item.Id).FirstOrDefaultAsync();
            if (webcontrol is not null)
            {
                webcontrol.Active = item.Active;
            }
            _dbcontext.Update(webcontrol);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        #endregion

    }
}
