using E_CommerceAPI.Application.Abstiraction;
using E_CommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Persistence.Concreties
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts() => new()
        {
            new() { Id = Guid.NewGuid() , Name = "Product1", Stock = 20, Price= 30},
            new() { Id = Guid.NewGuid() , Name = "Product2", Stock = 50, Price= 40},
            new() { Id = Guid.NewGuid() , Name = "Product3", Stock = 30, Price= 50},
            new() { Id = Guid.NewGuid() , Name = "Product4", Stock = 50, Price= 30}
        };
    }
}
