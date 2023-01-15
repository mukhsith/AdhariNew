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
using Services.Base;

namespace Services.Frontend.Content.Interface
{
    public class ContactDetailService : Repository<ContactDetail>, IContactDetailService
    {
        public ContactDetailService(ApplicationDbContext dbcontext) : base(dbcontext) { }

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
            }
            else
            {
                return data;
            }
        }

        #endregion

    }
}
