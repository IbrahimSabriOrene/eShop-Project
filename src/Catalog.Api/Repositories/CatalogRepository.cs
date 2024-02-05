using Dapper;
using Catalog.Api.Models;
using Catalog.Api.Helpers;
using Microsoft.Extensions.Options;

namespace Catalog.Api.Repositories
{
    public class CatalogRepository
    {
        private readonly DbContext _dbContext;
        public CatalogRepository( DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CatalogItem>> GetCatalogItems()
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<CatalogItem>("SELECT * FROM CatalogItems");
        }

        public async Task<CatalogItem> GetCatalogItem(int id)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<CatalogItem>("SELECT * FROM CatalogItems WHERE Id = @id", new { id }) ?? throw new KeyNotFoundException();
        }

        public async Task<IEnumerable<CatalogItem>> GetCatalogItemsByBrand(int brandId)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<CatalogItem>("SELECT * FROM CatalogItems WHERE CatalogBrandId = @brandId", new { brandId });
        }

        public async Task<IEnumerable<CatalogItem>> GetCatalogItemsByType(int typeId)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<CatalogItem>("SELECT * FROM CatalogItems WHERE CatalogTypeId = @typeId", new { typeId });
        }

        public async Task<IEnumerable<CatalogBrand>> GetCatalogBrands()
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<CatalogBrand>("SELECT * FROM CatalogBrand");
        }

        public async Task<IEnumerable<CatalogType>> GetCatalogTypes()
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<CatalogType>("SELECT * FROM CatalogType");
        }

        public async Task<CatalogItem> CreateCatalogItem(CatalogItem catalogItem)
        {
            // Returning id part a bit problematic
            using var connection = _dbContext.CreateConnection();
            var sql = "INSERT INTO CatalogItem (Name, Description, Price, ImageFile, CatalogTypeId, CatalogBrandId) VALUES (@Name, @Description, @Price, @ImageFile, @CatalogTypeId, @CatalogBrandId) RETURNING Id";
            var id = await connection.QuerySingleAsync<int>(sql, catalogItem);
            catalogItem.Id = id;
            return catalogItem;
        }


        public async Task<bool> UpdateCatalogItem(CatalogItem catalogItem)
        {
            using var connection = _dbContext.CreateConnection();
            var sql = "UPDATE CatalogItems SET Name = @Name, Description = @Description, Price = @Price, ImageFile = @ImageFile, CatalogTypeId = @CatalogTypeId, CatalogBrandId = @CatalogBrandId WHERE Id = @Id";
            return await connection.ExecuteAsync(sql, catalogItem) > 0;
        }

    }
}
