using Data.Content;
using Data.SystemUserManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API; 

namespace Services.Backend.Content.Interface
{
    public interface ICustomerFeedbackService
    {
        #region Customer Feedback Service 
        
        Task<DataTableResult<List<CustomerFeedback>>> GetAllForDataTable(DataTableParam param);    
        Task<CustomerFeedback> GetById(int id);        
        //Task<CustomerFeedback> UpdateStatus(int id, int status = 0);
        Task<CustomerFeedback> UpdateStatus(CustomerFeedback model);
        //Task<CustomerFeedback> Create(CustomerFeedback model);
        //Task<bool> Update(CustomerFeedback model);
        //Task<bool> Delete(CustomerFeedback model);
        //Task<bool> ToggleActive(int id);
        #endregion


    }
}
