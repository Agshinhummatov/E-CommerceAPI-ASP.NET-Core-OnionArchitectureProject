using E_CommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace E_CommerceAPI.Persistence
{
    public static class ServiceRegaration
    {
        public static void AddPresistenceServices(this IServiceCollection services)
        {

            services.AddDbContext<ECommerceAPIDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));
        }

    }
}
