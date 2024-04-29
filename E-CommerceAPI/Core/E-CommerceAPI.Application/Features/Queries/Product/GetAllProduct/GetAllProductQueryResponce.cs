using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryResponce
    {
        public int TotalCount { get; set; }
        public object Products { get; set; }
    }
}
