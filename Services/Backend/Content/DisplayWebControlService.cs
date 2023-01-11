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
using Utility.Enum;
using Utility.Helpers;
using System.Linq.Dynamic.Core;
using Microsoft.Azure.KeyVault.Models;

namespace Services.Backend.Content.Interface
{
    public class DisplayWebControlService : IDisplayWebControlService
    {
        protected readonly ApplicationDbContext _dbcontext;
        protected string ErrorMessage = string.Empty;
        public DisplayWebControlService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region DisplayWebControl 

         
        public async Task<DisplayWebControl> GetDefaultOrUpdate(AppContentType appContentType, bool active)
        {
            var data = await _dbcontext.DisplayWebControls.Where(x => x.ControlId == (int)appContentType).FirstOrDefaultAsync();
            if(data is null)
            { 
                data = await Create(appContentType,active);
            }
            return data;
        }

        public async Task<DisplayWebControl> Create(AppContentType appContentType, bool active)
        {
            DisplayWebControl model = new();
            model.Active = active;
            model.ControlId = (int)appContentType;
            model.Description = appContentType.ToString() + " Display";
            model.CreatedOn = DateTime.Now;
            await _dbcontext.DisplayWebControls.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }

        //public async Task<bool> ToggleActive(int id)
        //{
        //    var data = await _dbcontext.DisplayWebControls.FindAsync(id);
        //    if (data is not null)
        //    {
        //        data.ModifiedOn = DateTime.Now;
        //        data.Active = !data.Active;
        //        return await _dbcontext.SaveChangesAsync() > 0;
        //    }
        //    return false;
        //}

         
        
         
        #endregion

    }
}
