﻿using E_Market.Core.Application.Interfaces.Repositories;
using E_Market.Core.Domain.Entities;
using E_Market.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Infrastructure.Persistence.Repositories
{
    public class AdvertRepository : GenericRepository<Advert>, IAdvertRepository
    {
        private readonly EMarketContext _dbContext;

        public AdvertRepository(EMarketContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Advert>> GetAllWithCategoryAsync()
        {
            return await _dbContext.Set<Advert>().Include("Category").ToListAsync();
        }
    }
}
