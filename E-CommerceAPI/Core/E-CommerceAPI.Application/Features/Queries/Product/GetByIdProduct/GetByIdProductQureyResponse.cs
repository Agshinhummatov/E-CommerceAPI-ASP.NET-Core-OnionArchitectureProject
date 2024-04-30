using E_CommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQureyResponse 
    {
        public string Name { get; set; }
        public int Stock { get; set; }

        public float Price { get; set; }

        //public ICollection<Order> Orders { get; set; }

        //public ICollection<ProductImageFile> ProductImagesFile { get; set; }
    }
}
