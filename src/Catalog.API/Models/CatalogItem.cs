namespace Catalog.API.Models;

public class CatalogItem
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PictureFileName { get; set; }

    public CatalogItem()
    {
    }
}