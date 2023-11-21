using E_CommerceAPI.Application.Abstiraction;
using E_CommerceAPI.Persistence.Concreties;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Persistence
{
    public static class ServiceRegaration
    {
        public static void AddPresistenceServices(this IServiceCollection services){

            services.AddSingleton<IProductService, ProductService>();

        }

    }
}
