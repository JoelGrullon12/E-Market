﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T t);
        Task UpdateAsync(T t);
        Task DeleteAsync(T t);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllWithIncludesAsync(List<string> props);
        Task<T> GetByIdWithIncludeAsync(int id, List<string> props);
    }
}
