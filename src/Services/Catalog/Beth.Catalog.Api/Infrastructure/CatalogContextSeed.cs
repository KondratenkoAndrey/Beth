using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beth.Catalog.Api.Models;

namespace Beth.Catalog.Api.Infrastructure;

public static class CatalogContextSeed
{
    public static async Task SeedAsync(CatalogContext context)
    {
        if (!context.CatalogBrands.Any())
        {
            await context.CatalogBrands.AddRangeAsync(GetPreconfiguredCatalogBrands());
            await context.SaveChangesAsync();
        }

        if (!context.CatalogTypes.Any())
        {
            await context.CatalogTypes.AddRangeAsync(GetPreconfiguredCatalogTypes());
            await context.SaveChangesAsync();
        }

        if (!context.CatalogItems.Any())
        {
            await context.CatalogItems.AddRangeAsync(GetPreconfiguredItems());
            await context.SaveChangesAsync();
        }
    }
    
    private static IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands()
    {
        return new List<CatalogBrand>()
        {
            new() { Brand = "Nike"},
            new() { Brand = "Intel" },
            new() { Brand = "Ikea" },
            new() { Brand = "New Balance" }
        };
    }
    
    private static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
    {
        return new List<CatalogType>()
        {
            new() { Type = "Clothes"},
            new() { Type = "Electronics" },
            new() { Type = "Furniture" },
            new() { Type = "Shoes" }
        };
    }
    
    private static IEnumerable<CatalogItem> GetPreconfiguredItems()
    {
        return new List<CatalogItem>()
        {
            new() { CatalogTypeId = 1, CatalogBrandId = 1, AvailableStock = 100, Description = "special cotton shirt for men", Name = "shirt for men", Price = 19.5M, PictureFileName = "1.png" },
            new() { CatalogTypeId = 1, CatalogBrandId = 1, AvailableStock = 100, Description = "igh quality men distress skinny blue jeans", Name = "skinny blue jeans", Price= 8.50M, PictureFileName = "2.png" },
            new() { CatalogTypeId = 1, CatalogBrandId = 1, AvailableStock = 100, Description = "men casual shoes sports running sneakers", Name = "casual shoes", Price = 12, PictureFileName = "3.png" },
            new() { CatalogTypeId = 2, CatalogBrandId = 2, AvailableStock = 100, Description = "Intel Core i9 (12th Gen) i9-12900KS Hexadeca-core (16 Core) 2.50 GHz Processor", Name = "i9-12900KS", Price = 1200, PictureFileName = "4.png" },
            new() { CatalogTypeId = 2, CatalogBrandId = 2, AvailableStock = 100, Description = "Intel Core i7-12700KF Desktop Processor 12 (8P+4E) Cores up to 5.0 GHz Unlocked  LGA1700 600 Series Chipset 125W", Name = "i7-12700KF", Price = 356.5M, PictureFileName = "5.png" },
            new() { CatalogTypeId = 2, CatalogBrandId = 2, AvailableStock = 100, Description = "Intel® Core™ i5-11600KF Desktop Processor 6 Cores up to 4.9 GHz Unlocked LGA1200 (Intel® 500 Series & Select 400 Series Chipset) 125W", Name = "i5-11600KF", Price = 340, PictureFileName = "6.png" },
            new() { CatalogTypeId = 3, CatalogBrandId = 3, AvailableStock = 100, Description = "Organizer, plastic/beige", Name = "Organizer", Price = 0.59M, PictureFileName = "7.png" },
            new() { CatalogTypeId = 3, CatalogBrandId = 3, AvailableStock = 100, Description = "Cabinet, white", Name = "Cabinet", Price = 139.5M, PictureFileName = "8.png" },
            new() { CatalogTypeId = 3, CatalogBrandId = 3, AvailableStock = 100, Description = "Bed frame", Name = "Bed", Price = 239, PictureFileName = "9.png" },
            new() { CatalogTypeId = 4, CatalogBrandId = 4, AvailableStock = 100, Description = "This model runs large, compared to previous versions. You may consider ordering down from your normal size.", Name = "Fresh Foam X 1080v12", Price = 159, PictureFileName = "10.png" },
            new() { CatalogTypeId = 4, CatalogBrandId = 4, AvailableStock = 100, Description = "Our Numeric 440 blends premier technology with soft cushioning to create a go-to shoe for both everyday skate and wear.", Name = "NB Numeric 440", Price = 89.99M, PictureFileName = "11.png" },
            new() { CatalogTypeId = 4, CatalogBrandId = 4, AvailableStock = 100, Description = "Our off-field inspired FreezeLX v4 Turf has a specially designed NDurance outsole for traction, durability and versatility on field turf surfaces.", Name = "FreezeLX v4 Turf", Price = 104.99M, PictureFileName = "12.png" },
        };
    }
}
