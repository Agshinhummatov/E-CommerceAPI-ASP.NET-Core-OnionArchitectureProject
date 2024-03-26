using E_CommerceAPI.Domain.Entities;
using E_CommerceAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Persistence.Contexts
{
    public class ECommerceAPIDbContext : DbContext
    {
        public ECommerceAPIDbContext(DbContextOptions options) : base(options) { }
       
       

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        //public DbSet<OrderProduct> OrderProducts { get; set; }


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
