using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Base
{
    public partial interface IRepository<T> where T : class
    {
        abstract Task<T> GetById(int Id);
        abstract Task<T> Create(T model);
        abstract Task<bool> Update(T model);
        Task<bool> Delete(T model);
        abstract Task<bool> ToggleActive(int Id);
        abstract Task<bool> UpdateDisplayOrder(int Id, int num = 0); 
    }
}

