using Data.Content;
using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using System.Linq.Dynamic.Core;
using Services.Base;

namespace Services.Frontend.Content.Interface
{

    public class CustomerFeedbackService : Repository<CustomerFeedback>, ICustomerFeedbackService
    {
     
        public CustomerFeedbackService(ApplicationDbContext dbcontext) : base(dbcontext) { }
 
        #region CustomerFeedback 
         
        
        
        public override async Task<CustomerFeedback> GetById(int id)
        {
            var data = await _dbcontext.CustomerFeedbacks.FindAsync(id);
            return data;
        }

        public async Task<CustomerFeedback> Add( CustomerFeedback model)
        {
            var data = await _dbcontext.CustomerFeedbacks.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return data.Entity;
        }
        #endregion

    }
}
