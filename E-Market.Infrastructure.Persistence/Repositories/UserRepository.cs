using E_Market.Core.Application.Helpers;
using E_Market.Core.Application.Interfaces.Repositories;
using E_Market.Core.Application.ViewModels.User;
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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly EMarketContext _dbContext;

        public UserRepository(EMarketContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> LoginAsync(LoginViewModel vm)
        {
            string pass = Stuff.EncryptSHA256(vm.Password);
            User user = await _dbContext.Set<User>()
                .FirstOrDefaultAsync(user => user.UserName == vm.UserName && user.Password == pass);
            return user;
        }

        public async Task<User> CheckUserAsync(string userName)
        {
            User user = await _dbContext.Set<User>()
                .FirstOrDefaultAsync(user => user.UserName == userName);
            return user;
        }
    }
}
