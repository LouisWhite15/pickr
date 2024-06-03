using Microsoft.EntityFrameworkCore;
using Selections.Infrastructure;

namespace Selections.API;

public static class ApiModule
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        // Pooling is disabled because of the following error:
        // Unhandled exception. System.InvalidOperationException:
        // The DbContext of type 'OrderingContext' cannot be pooled because it does not have a public constructor accepting a single parameter of type DbContextOptions or has more than one constructor.
        builder.Services.AddDbContext<SelectionsContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("selectionsdb"));
        });
        builder.EnrichNpgsqlDbContext<SelectionsContext>();
    }
}