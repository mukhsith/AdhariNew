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
using NLog.Layouts;

namespace Services.Backend.Content.Interface
{
    public class ContactDetailService : IContactDetailService
    {
        protected readonly ApplicationDbContext _dbcontext;
        protected string ErrorMessage = string.Empty;
        public ContactDetailService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        #region Contact Detail Service  
        public async Task<ContactDetail> GetDefault()
        {
            var data = await _dbcontext.ContactDetails.AsNoTracking().FirstOrDefaultAsync();
            if (data == null)
            {
                var emptyData = await _dbcontext.ContactDetails.AddAsync(new ContactDetail());
                var success = await _dbcontext.SaveChangesAsync();
                //var EmptyDatad = await _dbcontext.ContactDetails.AsNoTracking().FirstOrDefaultAsync();
                return emptyData.Entity;
            } else {  
            return data;
            }
        }

        public async Task<bool> Edit(ContactDetail item)
        {
            var data = await _dbcontext.ContactDetails.FirstOrDefaultAsync();
            if (data is not null)
            {
                data.AddressEn = item.AddressEn;
                data.AddressAr = item.AddressAr;
                data.MobileNumber = item.MobileNumber;
                data.EmailAddress = item.EmailAddress;
                data.ModifiedBy = item.ModifiedBy;
                data.WhatsAppNumber = item.WhatsAppNumber;
                data.ModifiedOn = DateTime.Now;
                
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }

        #endregion

    }
}
