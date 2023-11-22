using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Domain.Entities
{
    public class Customer
    {
        public ICollection<Order> Orders { get; set; }
        public string Name { get; set; }
    }
}
