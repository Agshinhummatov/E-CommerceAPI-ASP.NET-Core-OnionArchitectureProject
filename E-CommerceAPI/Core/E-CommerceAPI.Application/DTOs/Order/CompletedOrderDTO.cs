using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.DTOs.Order
{
    public class CompletedOrderDTO
    {
        public string OrderCode { get; set; } 
        public DateTime OrderDate { get; set; } 
        public string userName { get; set; } 
      
        public string EMail { get; set; } 
        
        
    }
}
