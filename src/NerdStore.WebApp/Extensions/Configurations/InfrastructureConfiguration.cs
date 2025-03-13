using Microsoft.EntityFrameworkCore;
using NerdStore.Catalog.Data.Contexts;

namespace NerdStore.WebApp.Extensions.Configurations
{
    public static class InfrastructureConfiguration
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CatalogConnectionString"));
            });
        }
    }
}
