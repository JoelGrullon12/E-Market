using E_Market.Core.Application.Interfaces.Repositories;
using E_Market.Core.Domain.Entities;
using E_Market.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly EMarketContext _dbContext;

        public CategoryRepository(EMarketContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
