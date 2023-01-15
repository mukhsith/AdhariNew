using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _dbcontext;
        public Repository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async virtual Task<T> GetById(int Id)
        {
            return await _dbcontext.Set<T>().FindAsync(Id);
        }
        public async virtual Task<T> Create(T model)
        {
            await _dbcontext.Set<T>().AddAsync(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
        public async virtual Task<bool> Update(T model)
        {
            _dbcontext.Update(model);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        public async virtual Task<bool> Delete(T model)
        {
            _dbcontext.Set<T>().Remove(model);
            return await _dbcontext.SaveChangesAsync() > 0;
        }
        public async virtual Task<bool> ToggleActive(int Id)
        {
            dynamic data = await _dbcontext.Set<T>().FindAsync(Id);
            if (data is not null)
            {
                data.ModifiedOn = DateTime.Now;
                data.Active = !data.Active;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
        public async virtual Task<bool> UpdateDisplayOrder(int Id, int num = 0)
        {
            dynamic data = await _dbcontext.Set<T>().FindAsync(Id);
            if (data is not null)
            {
                data.DisplayOrder = num;
                data.ModifiedOn = DateTime.Now;
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
         
        
    }
}
