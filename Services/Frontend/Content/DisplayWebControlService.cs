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
using Utility.Models.Frontend.CustomizedModel;

namespace Services.Frontend.Content.Interface
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
         
        public async Task<ContentStatusModel> GetContentStatus()
        {
            ContentStatusModel statusModel = new();
            var items = await _dbcontext.DisplayWebControls.AsNoTracking().ToListAsync();             
            foreach(var item in items)
            {
                if (item.ControlId == (int)AppContentType.Banner)
                {
                    statusModel.Banner = item.Active;

                }
                else if (item.ControlId == (int)AppContentType.AboutUs)
                {
                    statusModel.AboutUs = item.Active;
                }
                else if (item.ControlId == (int)AppContentType.TermsCondition)
                {
                    statusModel.TermsCondition = item.Active;
                }
                else if (item.ControlId == (int)AppContentType.PrivacyPolicy)
                {
                    statusModel.PrivacyPolicy = item.Active;
                }
                else if (item.ControlId == (int)AppContentType.RefundPolicy)
                {
                    statusModel.RefundPolicy = item.Active;
                }
                else if (item.ControlId == (int)AppContentType.Subscription)
                {
                    statusModel.Subscription = item.Active;
                }
            }
            return statusModel;
        }
         
        #endregion

    }
}
