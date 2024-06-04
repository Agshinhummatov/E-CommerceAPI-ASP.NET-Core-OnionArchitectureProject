using E_CommerceAPI.Domain.Entities;
using E_CommerceAPI.Domain.Entities.Common;
using E_CommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Persistence.Contexts
{
    public class ECommerceAPIDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public ECommerceAPIDbContext(DbContextOptions options) : base(options) { }
       
       

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        //public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<Domain.Entities.File> Files { get; set; }

        public DbSet<ProductImageFile> ProductImageFiles { get; set; }

        public DbSet<InvoiceFile> InvoiceFiles { get; set; }


        public DbSet<Basket> Baskets { get; set; } 
        public DbSet<BasketItem> BasketItems { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>().HasKey(b => b.Id);

            builder.Entity<Basket>()
                .HasOne(b => b.Order)
                .WithOne(o => o.Basket).
                HasForeignKey<Order>(b => b.Id);

            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            //ChangeTracker : Entityler üzerinden yapılan değişiklerin ya da yeni eklenen verinin yakalanmasını sağlayan propertydir. Update operasyonlarında Track edilen verileri yakalayıp elde etmemizi sağlar.


            //var datas = ChangeTracker.Entries<BaseEntity>();


            //foreach (var data in ChangeTracker.Entries<BaseEntity>())
            //{
            //    switch (data.State)
            //    {
            //        case EntityState.Added:
            //            data.Entity.CreatedDate = DateTime.UtcNow;
            //            break;

            //        case EntityState.Modified:
            //            data.Entity.UpdatedDate = DateTime.UtcNow;
            //            break;
            //    }
            //}


            foreach (var data in ChangeTracker.Entries<BaseEntity>())
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,

                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };

            }

                return await base.SaveChangesAsync(cancellationToken);    
        }

    }
}
