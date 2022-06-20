using E_Market.Core.Application.Interfaces.Repositories;
using E_Market.Infrastructure.Persistence.Contexts;
using E_Market.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Infrastructure.Persistence
{
    public static class PServiceRegistration
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("useInMemoryDB"))
            {
                services.AddDbContext<EMarketContext>(
                    options => options.UseInMemoryDatabase("EMarketDB"));
            }
            else {
                services.AddDbContext<EMarketContext>(
                    options => options.UseSqlServer(
                        configuration.GetConnectionString("EMarketConnection"),
                        m=>m.MigrationsAssembly(typeof(EMarketContext).Assembly.FullName)));
            }

            #region dependencies
            //services.AddTransient<interface, class>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IAdvertRepository, AdvertRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            #endregion
        }
    }
}
