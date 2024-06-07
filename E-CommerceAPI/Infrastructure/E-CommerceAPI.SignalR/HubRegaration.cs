using E_CommerceAPI.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SignalR
{
    public static class HubRegaration
    {
        public static void MapHubs(this WebApplication webApplication)
        {
            webApplication.MapHub<ProductHub>("/products-hub"); 
            webApplication.MapHub<OrderHub>("/orders-hub"); 
        }
    }
}
