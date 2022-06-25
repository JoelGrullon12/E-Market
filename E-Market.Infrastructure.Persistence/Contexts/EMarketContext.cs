using E_Market.Core.Application.Helpers;
using E_Market.Core.Application.ViewModels.User;
using E_Market.Core.Domain.Common;
using E_Market.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace E_Market.Infrastructure.Persistence.Contexts
{
    public class EMarketContext:DbContext
    {
        private readonly IHttpContextAccessor _httpContext;

        public EMarketContext(DbContextOptions<EMarketContext> options, IHttpContextAccessor httpContext) : base(options)
        {
            _httpContext = httpContext;
        }

        public DbSet<Advert> Adverts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach(var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = _httpContext.HttpContext.Session.Get<UserViewModel>("user").UserName;
                        break;

                    case EntityState.Modified:
                        entry.Entity.Modified = DateTime.Now;
                        entry.Entity.ModifiedBy = _httpContext.HttpContext.Session.Get<UserViewModel>("user").UserName;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region tables
            modelBuilder.Entity<Advert>().ToTable("Adverts");
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<User>().ToTable("Users");
            #endregion

            #region keys
            modelBuilder.Entity<Advert>().HasKey(t => t.Id);
            modelBuilder.Entity<Category>().HasKey(t => t.Id);
            modelBuilder.Entity<User>().HasKey(t => t.Id);
            #endregion

            #region relations
            //User-Adverts
            modelBuilder.Entity<User>()
                .HasMany<Advert>(user=>user.Adverts)
                .WithOne(ad=>ad.User)
                .HasForeignKey(ad=>ad.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //Category-Adverts
            modelBuilder.Entity<Category>()
                .HasMany<Advert>(cat => cat.Adverts)
                .WithOne(ad => ad.Category)
                .HasForeignKey(ad => ad.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region property configs

            #region Adverts
            modelBuilder.Entity<Advert>().Property(t => t.Name).IsRequired();
            modelBuilder.Entity<Advert>().Property(t => t.Price).IsRequired();
            modelBuilder.Entity<Advert>().Property(t => t.CategoryId).IsRequired();
            modelBuilder.Entity<Advert>().Property(t => t.Description).IsRequired();
            modelBuilder.Entity<Advert>().Property(t => t.ImgUrl1).IsRequired();
            modelBuilder.Entity<Advert>().Property(t => t.PublishDate).IsRequired();
            modelBuilder.Entity<Advert>().Property(t => t.UserId).IsRequired();
            #endregion

            #region Categories
            modelBuilder.Entity<Category>().Property(t => t.Name).IsRequired();
            modelBuilder.Entity<Category>().Property(t => t.Name).IsRequired();
            #endregion

            #region Users
            modelBuilder.Entity<User>().Property(t => t.Name).IsRequired();
            #endregion


            #endregion
        }
    }
}
