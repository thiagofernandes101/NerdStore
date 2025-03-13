using NerdStore.Catalog.Application.Services;
using NerdStore.Catalog.Data.Repositories;
using NerdStore.Catalog.Domain.Repositories;
using NerdStore.Catalog.Domain.Services;
using NerdStore.Core.Bus;

namespace NerdStore.WebApp.Extensions.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddApplicationDependencies(this IServiceCollection service)
        {
            service.AddScoped<IProductApplicationService, ProductApplicationService>();
        }

        public static void AddInfrastructureDependencies(this IServiceCollection service)
        {
            service.AddScoped<IProductRepository, ProductRepository>();
        }

        public static void AddDomainDependencies(this IServiceCollection service)
        {
            service.AddScoped<IStockService, StockService>();
            service.AddScoped<IMediatRHandler, MediatRHandler>();
        }
    }
}
