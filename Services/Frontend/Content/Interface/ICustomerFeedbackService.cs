using Data.Content;
using Data.SystemUserManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API; 

namespace Services.Frontend.Content.Interface
{
    public interface ICustomerFeedbackService
    {
        #region Customer Feedback Service 
        Task<CustomerFeedback> Add(CustomerFeedback model);
        #endregion


    }
}
