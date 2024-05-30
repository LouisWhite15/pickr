using Catalog.API.Infrastructure;

namespace Catalog.API;

public static class ApiModule
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<CatalogContext>("catalogdb");
    }
}