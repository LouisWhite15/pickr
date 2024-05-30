using Catalog.API.Infrastructure;

namespace Catalog.API.Models;

public class CatalogServices(
    CatalogContext context,
    ILogger<CatalogServices> logger)
{
    public CatalogContext Context { get; } = context;
    public ILogger<CatalogServices> Logger { get; } = logger;
}