using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.API;
using System.Linq.Dynamic.Core;
using Data.ProductManagement; 

namespace Services.Frontend.ProductManagement.Interface
{
    public class ProductStockHistoryService : IProductStockHistoryService
    {
        protected readonly ApplicationDbContext _dbcontext;
        protected string ErrorMessage = string.Empty;
        public ProductStockHistoryService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        #region Category 
         
        
        public async Task<ProductStockHistory> Create(ProductStockHistory model)
        {
            model.CreatedOn = DateTime.Now;
            await _dbcontext.ProductStockHistories.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
         
        #endregion
       
    }
}
