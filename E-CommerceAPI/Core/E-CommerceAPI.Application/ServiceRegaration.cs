using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application
{
    public static class ServiceRegaration
    {
        public static void AddApplicationServices(this IServiceCollection collection)
        {
            collection.AddMediatR(typeof(ServiceRegaration));
            collection.AddHttpClient();
        }
    }
}
