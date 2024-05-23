using E_CommerceAPI.Application.Abstractions.Hubs;
using E_CommerceAPI.SignalR.HubSevices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SignalR
{
    public static class ServiceRegaration
    {
        public static void AddSiganRServices(this IServiceCollection collection)
        {
            collection.AddTransient<IProductHubService, ProductHubService>();
            collection.AddSignalR();
        }
    }
}
