using Data.ProductManagement;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API;
using Utility.Models.Admin.ProductManagement;

namespace Services.Backend.ProductManagement.Interface
{
    public interface IProductStockHistoryService
    {
        #region  Product Stock History Service   
        Task<IEnumerable<ProductStockHistory>> GetAll();
        Task<DataTableResult<List<ProductStockHistoryModel>>> GetAllForDataTable(DataTableParam param);    
        Task<ProductStockHistory> GetById(int id); 
        Task<ProductStockHistory> Create(ProductStockHistory model);
        Task<bool> Update(ProductStockHistory model);
         
        #endregion
        
        
    }
}
