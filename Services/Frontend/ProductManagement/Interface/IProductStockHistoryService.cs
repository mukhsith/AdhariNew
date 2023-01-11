using Data.Content;
using Data.ProductManagement;
using Data.SystemUserManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API; 

namespace Services.Frontend.ProductManagement.Interface
{
    public interface IProductStockHistoryService
    {
        #region  Product Stock History Service    
        Task<ProductStockHistory> Create(ProductStockHistory model); 
         
        #endregion
        
        
    }
}
