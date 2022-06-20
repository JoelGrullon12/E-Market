using E_Market.Core.Application.Interfaces.Repositories;
using E_Market.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly EMarketContext _dbContext;
        public GenericRepository(EMarketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(T t)
        {
            await _dbContext.Set<T>().AddAsync(t);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T t)
        {
            _dbContext.Entry(t).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T t)
        {
            _dbContext.Set<T>().Remove(t);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
    }
}
