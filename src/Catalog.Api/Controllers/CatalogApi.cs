using Catalog.Api.Models;
using Catalog.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace Catalog.Api.Controllers;


public static class CatalogApi
{
    public static IEndpointRouteBuilder MapCatalogApi(this IEndpointRouteBuilder app)
    {
        // Products
        app.MapGet("/api/products", GetAllCatalogItems);
        app.MapPost("/api/products/by", GetCatalogItemsByIds);
        app.MapGet("/api/products/{id:int}", GetCatalogItemsById);
        app.MapGet("/api/products/by-name/{name:minlength(1)}", GetCatalogItemsByName);
        app.MapGet("/api/products/by-type/{typeId}/by-brand/{brandId?}", GetProductsByBrandAndTypeId);
        app.MapGet("/api/products/by-brand/{brandId:int?}", GetProductsByBrandId);
        app.MapPut("/api/products", UpdateProduct);
        app.MapPost("/api/products", CreateProduct);
        app.MapDelete("/api/products/{id:int}", DeleteProductById);

        // Brands
        app.MapGet("/api/brands", GetCatalogBrands);
        app.MapPost("/api/brands/create", CreateCatalogBrand);
        app.MapPut("/api/brands/update/{id:int}", UpdateCatalogBrand);
        app.MapDelete("/api/brands/delete/{id:int}", DeleteCatalogBrand);

        // Types
        app.MapGet("/api/types", GetCatalogTypes);
        app.MapGet("/api/types/{id:int}", GetCatalogType);
        app.MapPost("/api/types/create", CreateCatalogType);
        app.MapPut("/api/types/update/{id:int}", UpdateCatalogType);
        app.MapDelete("/api/types/delete/{id:int}", DeleteCatalogType);


        return app;
    }

    private static async Task<string> DeleteCatalogBrand([FromServices] CatalogService services, int id)
    {
        try
        {
            var brand = await services.Repository.GetCatalogItem(id) ?? throw new KeyNotFoundException();
            await services.Repository.DeleteCatalogItem(id);
            return "Deleted brand with id: " + id;  
        }   
        catch (Exception ex)
        {
            throw new Exception("Error deleting brand with id: " + id, ex); // Change this to logger
        }
    }

    private static async Task UpdateCatalogBrand([FromServices] CatalogService services, [FromBody] int id)
    {
        try
        {
            // Change this to logger
            var brand = await services.Repository.GetCatalogItem(id) ?? throw new KeyNotFoundException();
            var update = await services.Repository.UpdateCatalogItem(brand);
            if (update == false)
            {
                throw new KeyNotFoundException();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error updating brand with id: " + id, ex); // Change this to logger
        }
    }


    private static async Task<string> CreateCatalogBrand([FromServices] CatalogService services, [FromBody] CatalogBrand brand)
    {
        try
        {
            var newBrand = await services.Repository.CreateCatalogBrand(brand);
            return  " newBrand.Brand: " + newBrand.Brand + " created.";
        }
        catch (Exception ex)
        {
            throw new Exception("Error creating brand", ex); // Change this to logger
        }
    }

    private static async Task DeleteCatalogType([FromServices] CatalogService services, int id)
    {
        try
        {
            var brand = await services.Repository.GetCatalogType(id) ?? throw new KeyNotFoundException();// Change this to logger
            await services.Repository.DeleteCatalogItem(id);
        }
        catch (Exception ex)
        {
            throw new Exception("Error deleting type with id: " + id, ex);
        }

    }

    private static async Task<string> UpdateCatalogType([FromServices] CatalogService services, [FromBody] int id)
    {
        try
        {
            var brand = await services.Repository.GetCatalogType(id) ?? throw new KeyNotFoundException();
            var update = await services.Repository.UpdateCatalogType(brand) ?? throw new KeyNotFoundException();
            return "Updated type with id: " + id;
        }
        catch (Exception ex)
        {
            throw new Exception("Error updating type with id: " + id, ex); // Change this to logger
        }
    }


    private static async Task<string> CreateCatalogType([FromServices] CatalogService services, [FromBody] CatalogType type)
    {
        try
        {
            var newType = await services.Repository.CreateCatalogType(type) ?? throw new KeyNotFoundException();
            return "newType.Id: " + newType.Id + " newType.Type: " + newType.Type + " created.";
        }
        catch (Exception ex)
        {
            throw new Exception("Error creating type", ex); // Change this to logger
        }
    }

    private static async Task<string> CreateProduct(
    [FromServices] CatalogService services,
    [FromBody] CatalogItem product)
    {
        await Task.CompletedTask;
        await services.Repository.CreateCatalogItem(product);
        return "Product is created.";
    }

    private static async Task<CatalogItem> UpdateProduct([FromServices] CatalogService services,
    [FromBody] CatalogItem product)
    {
        // This is stupid. I'm not going to do this.
        await Task.CompletedTask;
        var update = await services.Repository.UpdateCatalogItem(product);
        if (update == false)
        {
            throw new KeyNotFoundException();
        }
        return product;
    }

    private static async Task<CatalogItem> DeleteProductById([FromServices] CatalogService services, int id)
    {
        try
        {
            var product = await services.Repository.GetCatalogItem(id) ?? throw new KeyNotFoundException();
            await services.Repository.DeleteCatalogItem(id);
            return product;
        }
        catch (Exception ex)
        {
            throw new Exception("Error deleting product with id: " + id, ex); // Change this to logger
        }
    }

    //  Change this part
    private static async Task<IEnumerable<CatalogType>> GetCatalogTypes([FromServices] CatalogService services)
    {
        var types = await services.Repository.GetCatalogTypes();
        return types;
    }
    private static async Task<CatalogType> GetCatalogType([FromServices] CatalogService services, int id)
    {
        try
        {
            var type = await services.Repository.GetCatalogType(id) ?? throw new KeyNotFoundException();
            return type;
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting type with id: " + id, ex); // Change this to logger
        }
    }
    
    
    private static async Task<IEnumerable<CatalogBrand>> GetCatalogBrands([FromServices] CatalogService services)
    {
        try{
        var brands = await services.Repository.GetCatalogBrands() ?? throw new KeyNotFoundException();
        return brands;
        }
        catch(Exception ex)
        {
               throw new Exception("Error getting brands", ex); // Change this to logger
        }
    }

    private static async Task<IEnumerable<CatalogItem>> GetProductsByBrandId(int brandId, [FromServices] CatalogService services)
    {
        try
        {
            var products = await services.Repository.GetCatalogItemsByBrand(brandId) ?? throw new KeyNotFoundException();
            return products;
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting products by brand with id: " + brandId, ex); // Change this to logger
        }
    }

    private static async Task<IEnumerable<CatalogItem>> GetProductsByBrandAndTypeId(
        [FromServices] CatalogService services,
        int typeId,
         int brandId)
    {
        try
        {
            var products = await services.Repository.GetCatalogItemsBrandAndTypeId(typeId, brandId) ?? throw new KeyNotFoundException();
            return products;
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting products by type with id: " + typeId, ex); // Change this to logger
        }
    }

    private static async Task<CatalogItem> GetCatalogItemsByName([FromServices] CatalogService services, string name)
    {
        try
        {
            var product = await services.Repository.GetCatalogItemByName(name) ?? throw new KeyNotFoundException();
            return product;
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting product by name: " + name, ex); // Change this to logger
        }
    }

    private static async Task<CatalogItem> GetCatalogItemsById([FromServices] CatalogService services, int id)
    {
       try
        {
            var product = await services.Repository.GetCatalogItem(id) ?? throw new KeyNotFoundException();
            return product;
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting product by id: " + id, ex); // Change this to logger
        }
    }

    private static async Task<IEnumerable<CatalogItem>> GetCatalogItemsByIds([FromServices] CatalogService services, [FromBody] int[] ids)
    {
        try
        {
            var products = await services.Repository.GetCatalogItemsByIds(ids) ?? throw new KeyNotFoundException();
            return products;
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting products by ids: " + ids, ex); // Change this to logger
        }
    }

    private static async Task<IEnumerable<CatalogItem>> GetAllCatalogItems([FromServices] CatalogService services)
    {
        try
        {
            var products = await services.Repository.GetCatalogItems() ?? throw new KeyNotFoundException();
            
            return products;
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting all products", ex); // Change this to logger
        }
    }
}





