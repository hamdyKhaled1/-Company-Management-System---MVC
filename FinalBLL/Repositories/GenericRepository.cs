using FinalBLL.Interfaces;
using FinalDAL.Context;
using FinalDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private protected readonly MVCAppContext _dbContext;
        public GenericRepository(MVCAppContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(T item)
        {
            await _dbContext.Set<T>().AddAsync(item);
        }

        public void Delete(T item)
        {
            _dbContext.Set<T>().Remove(item);
        }

        public async Task<T> Get(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Employee))
                return  (IEnumerable < T >) await _dbContext.Employees.Include(E => E.Department).ToListAsync();
            else
          return await _dbContext.Set<T>().ToListAsync();
        }

        public void Update(T item)
        {
            _dbContext.Set<T>().Update(item);
        }
    }
}
