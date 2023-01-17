using Data.ProductManagement;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.Models.Frontend.ProductManagement;

namespace Services.Frontend.ProductManagement
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAll();
        Task<Category> GetById(int id);
        Task<Category> GetBySeoName(string seoName);
    }
}
