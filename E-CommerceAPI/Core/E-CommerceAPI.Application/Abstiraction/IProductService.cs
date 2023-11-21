using E_CommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Abstiraction
{
    public interface IProductService
    {
        public List<Product> GetProducts();
    }
}
