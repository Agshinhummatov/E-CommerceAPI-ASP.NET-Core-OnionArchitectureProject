using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.DTOs.Order
{
    public class CreateOrder
    {
        public string Description { get; set; }
        public string Address { get; set; }



        public string? BasketId { get; set; }
    }
}
