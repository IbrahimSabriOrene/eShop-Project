using Catalog.Api.Models;
using Catalog.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace Catalog.Api.Controllers;


public static class CatalogApi
{
    public static IEndpointRouteBuilder MapCatalogApi(this IEndpointRouteBuilder app)
    {
        //Products
        app.MapGet("/products", GetAllProducts);
        app.MapGet("/products/by", GetProductsByIds);
        app.MapGet("/products/{id:int}", GetProductById);
        app.MapGet("/products/by/{name:minlength(1)}", GetProductsByName);
        app.MapGet("/products/{catalogProductId:int}/pic", GetProductPictureById);
        app.MapGet("/products/type/{typeId}/brand/{brandId?}", GetProductsByBrandAndTypeId);
        app.MapGet("/products/type/all/brand/{brandId:int?}", GetProductsByBrandId);
        app.MapPut("/products", UpdateProduct);
        app.MapPost("/products", async (
            [FromServices] CatalogService services,
            [FromBody] CatalogItem product) => await services.Repository.CreateCatalogItem(product));

        app.MapDelete("/products/{id:int}", DeleteProductById);

        //Brands
        app.MapGet("/brands", GetCatalogBrands);
        app.MapGet("/brands/create", CreateCatalogBrand);
        app.MapGet("/brands/update/{id:int}", UpdateCatalogBrand);
        app.MapGet("/brands/delete/{id:int}", DeleteCatalogBrand);

        //Types
        app.MapGet("/types/{id:int}", GetCatalogTypes);
        app.MapGet("/type/create", CreateCatalogType);
        app.MapGet("/type/update/{id:int}", UpdateCatalogType);
        app.MapGet("/type/delete/{id:int}", DeleteCatalogType);

        return app;
    }

    private static async Task DeleteCatalogBrand([FromServices] CatalogService services)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    private static async Task UpdateCatalogBrand([FromServices] CatalogService services)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    private static async Task CreateCatalogBrand([FromServices] CatalogService services)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    private static async Task DeleteCatalogType([FromServices] CatalogService services)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    private static async Task UpdateCatalogType([FromServices] CatalogService services)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    private static async Task CreateCatalogType([FromServices] CatalogService services)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    //private static async Task<CatalogItem> CreateProduct(
    //[FromServices] CatalogService services,
    //[FromBody] CatalogItem product)
    //{
    //    await Task.CompletedTask;
    //    return await services.Repository.CreateCatalogItem(product);
    //}

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
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    //  Change this part
    private static async Task<IEnumerable<CatalogType>> GetCatalogTypes([FromServices] CatalogService services)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    private static async Task<CatalogItem> GetCatalogBrands([FromServices] CatalogService services)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    private static async Task<CatalogItem> GetProductsByBrandId(int? brandId, [FromServices] CatalogService services)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    private static async Task<CatalogItem> GetProductsByBrandAndTypeId(
        [FromServices] CatalogService services,
        int typeId,
         int? brandId)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    private static async Task<CatalogItem> GetProductPictureById([FromServices] CatalogService services, int catalogProductId)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    private static async Task<CatalogItem> GetProductsByName([FromServices] CatalogService services, string name)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    private static async Task<CatalogItem> GetProductById([FromServices] CatalogService services, int id)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    private static async Task<IEnumerable<CatalogItem>> GetProductsByIds([FromServices] CatalogService services, [FromBody] int[] ids)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    private static async Task<IEnumerable<CatalogItem>> GetAllProducts([FromServices] CatalogService services)
    {
        await Task.CompletedTask;
        var products = await services.Repository.GetCatalogItems();
        return products;
    }
}

