using Catalog.Api.Helpers;
using Catalog.Api.Repositories;
using Catalog.Api.Services;

namespace Catalog.Api;

public static class DependencyInjection
{

    public static void AddCatalogServices(this IServiceCollection services)
    {
        services.AddScoped<CatalogRepository>();
        services.AddScoped<CatalogService>();
        services.AddScoped<DbContext>();

    }
}