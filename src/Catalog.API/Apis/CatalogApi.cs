using Catalog.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Apis;

public static class CatalogApi
{
    public static IEndpointRouteBuilder MapCatalogApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/catalog");

        // Routes for querying catalog items.
        api.MapGet("/items", GetAllItems);
        api.MapGet("/items/{id:guid}", GetItemById);
        api.MapGet("/items/by/{name:minlength(1)}", GetItemsByName);

        // Routes for modifying catalog items.
        api.MapPost("/items", CreateItem);
        api.MapPut("/items", UpdateItem);
        api.MapDelete("/items/{id:guid}", DeleteItemById);

        return app;
    }
    
    public static async Task<Results<Ok<List<CatalogItem>>, BadRequest<string>>> GetAllItems(
        [AsParameters] CatalogServices services)
    {
        var catalogItems = await services.Context.CatalogItems
            .OrderBy(c => c.Name)
            .ToListAsync();

        return TypedResults.Ok(catalogItems);
    }

    public static async Task<Results<Ok<CatalogItem>, NotFound, BadRequest<string>>> GetItemById(
        [AsParameters] CatalogServices services,
        Guid id)
    {
        if (id == Guid.Empty)
            return TypedResults.BadRequest("Id is not valid.");

        var item = await services.Context.CatalogItems.SingleOrDefaultAsync(ci => ci.Id == id);

        if (item is null)
            return TypedResults.NotFound();
        
        return TypedResults.Ok(item);
    }
    
    public static async Task<Ok<List<CatalogItem>>> GetItemsByName(
        [AsParameters] CatalogServices services,
        string name)
    {
        var items = await services.Context.CatalogItems
            .Where(c => c.Name.StartsWith(name))
            .ToListAsync();

        return TypedResults.Ok(items);
    }
    
    public static async Task<Results<Created, NotFound<string>>> UpdateItem(
        [AsParameters] CatalogServices services,
        CatalogItem productToUpdate)
    {
        var catalogItem = await services.Context.CatalogItems.SingleOrDefaultAsync(i => i.Id == productToUpdate.Id);

        if (catalogItem is null)
            return TypedResults.NotFound($"Item with id {productToUpdate.Id} not found.");
        
        // Update current product
        var catalogEntry = services.Context.Entry(catalogItem);
        catalogEntry.CurrentValues.SetValues(productToUpdate);

        await services.Context.SaveChangesAsync();
        
        return TypedResults.Created($"/api/catalog/items/{productToUpdate.Id}");
    }

    public static async Task<Created> CreateItem(
        [AsParameters] CatalogServices services,
        CatalogItem product)
    {
        var item = new CatalogItem
        {
            Id = product.Id,
            Name = product.Name,
            PictureFileName = product.PictureFileName,
        };

        services.Context.CatalogItems.Add(item);
        await services.Context.SaveChangesAsync();

        return TypedResults.Created($"/api/catalog/items/{item.Id}");
    }

    public static async Task<Results<NoContent, NotFound>> DeleteItemById(
        [AsParameters] CatalogServices services,
        Guid id)
    {
        var item = services.Context.CatalogItems.SingleOrDefault(x => x.Id == id);

        if (item is null)
        {
            return TypedResults.NotFound();
        }

        services.Context.CatalogItems.Remove(item);
        await services.Context.SaveChangesAsync();
        return TypedResults.NoContent();
    }
}