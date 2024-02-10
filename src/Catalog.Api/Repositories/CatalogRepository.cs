using Dapper;
using Catalog.Api.Models;
using Catalog.Api.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Npgsql;

namespace Catalog.Api.Repositories
{
    public class CatalogRepository
    {
        private readonly DbContext _dbContext;
        public CatalogRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private enum CatalogQuery
        {
            GetAllCatalogItems,
            GetCatalogItemById,
            GetCatalogItemsByBrand,
            GetCatalogItemsByType,
            GetCatalogBrands,
            GetCatalogBrand,
            CreateCatalogBrand,
            GetCatalogTypes,
            CreateCatalogType,
            GetCatalogType,
            UpdateCatalogType,
            CreateCatalogItem,
            UpdateCatalogItem,
            DeleteCatalogItem,
            GetCatalogItemsByTypeAndBrandId,
            GetCatalogItemByName,
            GetCatalogItemByIds,
            UpdateCatalogBrand
        }
        private static string GetQuery(CatalogQuery catalogQuery)
        {
            var sql = new StringBuilder();

            switch (catalogQuery)
            {
                case CatalogQuery.GetAllCatalogItems:
                    sql.Append("SELECT * FROM CatalogItem");
                    break;

                case CatalogQuery.GetCatalogItemById:
                    sql.Append("SELECT * FROM CatalogItem WHERE Id = @id");
                    break;
                case CatalogQuery.GetCatalogItemByIds:
                    sql.Append("SELECT * FROM CatalogItem WHERE Id = ANY(@ids)");
                    break;

                case CatalogQuery.CreateCatalogBrand:
                    sql.Append("INSERT INTO CatalogBrand (Brand) VALUES (@Brand)");
                    break;
                case CatalogQuery.GetCatalogItemsByBrand:
                    sql.Append("SELECT * FROM CatalogItem WHERE CatalogBrandId = @brandId");
                    break;
                case CatalogQuery.GetCatalogItemsByType:
                    sql.Append("SELECT * FROM CatalogItem WHERE CatalogTypeId = @typeId");
                    break;
                case CatalogQuery.GetCatalogBrands:
                    sql.Append("SELECT * FROM CatalogBrand");
                    break;
                case CatalogQuery.GetCatalogBrand:
                    sql.Append("SELECT * FROM CatalogBrand WHERE Id = @id");
                    break;
                case CatalogQuery.UpdateCatalogBrand:
                    sql.Append("UPDATE CatalogBrand SET Brand = @Brand WHERE Id = @Id");
                    break;
                case CatalogQuery.GetCatalogTypes:
                    sql.Append("SELECT * FROM CatalogType");
                    break;
                case CatalogQuery.CreateCatalogType:
                    sql.Append("INSERT INTO CatalogType (Type) VALUES (@Type)");
                    break;
                case CatalogQuery.GetCatalogType:
                    sql.Append("SELECT * FROM CatalogType WHERE Id = @id");
                    break;
                case CatalogQuery.UpdateCatalogType:
                    sql.Append("UPDATE CatalogType SET Type = @Type WHERE Id = @Id");
                    break;
                case CatalogQuery.DeleteCatalogItem:
                    sql.Append("DELETE FROM CatalogItem WHERE Id = @id");
                    break;
                case CatalogQuery.UpdateCatalogItem:
                    sql.Append("UPDATE CatalogItem SET Name = @Name, Description = @Description, Price = @Price, ImageFile = @ImageFile, CatalogTypeId = @CatalogTypeId, CatalogBrandId = @CatalogBrandId WHERE Id = @Id");
                    break;
                case CatalogQuery.CreateCatalogItem:
                    sql.Append(@"INSERT INTO CatalogItem (Name, Description, Price, ImageFile, CatalogTypeId, CatalogBrandId)
                     VALUES (@Name, @Description, @Price, @ImageFile, @CatalogTypeId, @CatalogBrandId) RETURNING Id");
                    break;
                case CatalogQuery.GetCatalogItemsByTypeAndBrandId:
                    sql.Append("SELECT * FROM CatalogItem WHERE CatalogTypeId = @typeId AND CatalogBrandId = @brandId");
                    break;
                case CatalogQuery.GetCatalogItemByName:
                    sql.Append("SELECT * FROM CatalogItem WHERE Name = @name");
                    break;


                default:
                    throw new ArgumentOutOfRangeException(nameof(catalogQuery), catalogQuery, null);
            }

            return sql.ToString();
        }

        public async Task<IEnumerable<CatalogItem?>> GetCatalogItems()
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<CatalogItem>(GetQuery(CatalogQuery.GetAllCatalogItems));
        }

        public async Task<CatalogItem?> GetCatalogItem(int id)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<CatalogItem>(GetQuery(CatalogQuery.GetCatalogItemById), new { id }) ?? throw new KeyNotFoundException();
        }

        public async Task<IEnumerable<CatalogItem?>> GetCatalogItemsByBrand(int brandId)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<CatalogItem>(GetQuery(CatalogQuery.GetCatalogItemsByBrand), new { brandId });
        }

        public async Task<IEnumerable<CatalogItem?>> GetCatalogItemsByType(int typeId)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<CatalogItem>(GetQuery(CatalogQuery.GetCatalogItemsByType), new { typeId });
        }

        public async Task<IEnumerable<CatalogBrand?>> GetCatalogBrands()
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<CatalogBrand>(GetQuery(CatalogQuery.GetCatalogBrands));
        }

        public async Task<CatalogBrand?> CreateCatalogBrand(CatalogBrand catalogBrand)
        {
            using var connection = _dbContext.CreateConnection();

            await connection.ExecuteAsync(GetQuery(CatalogQuery.CreateCatalogBrand), catalogBrand);
            return catalogBrand;
        }

        public async Task<IEnumerable<CatalogType?>> GetCatalogTypes()
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<CatalogType>(GetQuery(CatalogQuery.GetCatalogTypes));
        }

        public async Task<int?> CreateCatalogType(CatalogType catalogType)
        {
            using var connection = _dbContext.CreateConnection();
            var result = await connection.ExecuteAsync(GetQuery(CatalogQuery.CreateCatalogType), catalogType);
            return result;
        }

        public async Task<CatalogType?> GetCatalogType(int id)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<CatalogType>(GetQuery(CatalogQuery.GetCatalogType), new { id }) ?? throw new KeyNotFoundException();
        }

        public async Task<CatalogType?> UpdateCatalogType(CatalogType catalogType)
        {
            using var connection = _dbContext.CreateConnection();

            await connection.ExecuteAsync(GetQuery(CatalogQuery.UpdateCatalogType), catalogType);
            return catalogType;
        }

        public async Task<int?> CreateCatalogItem(CatalogItem catalogItem)
        {
            // Returning id part a bit problematic
            using var connection = _dbContext.CreateConnection();

            var id = await connection.ExecuteAsync(GetQuery(CatalogQuery.CreateCatalogItem), catalogItem);
            return id;
        }


        public async Task<bool> UpdateCatalogItem(CatalogItem catalogItem)
        {
            using var connection = _dbContext.CreateConnection();

            return await connection.ExecuteAsync(GetQuery(CatalogQuery.UpdateCatalogItem), catalogItem) > 0;
        }

        public async Task<bool> DeleteCatalogItem(int id)
        {
            using var connection = _dbContext.CreateConnection();

            return await connection.ExecuteAsync(GetQuery(CatalogQuery.DeleteCatalogItem), new { id }) > 0;
        }

        public async Task<IEnumerable<CatalogItem?>> GetCatalogItemsBrandAndTypeId(int typeId, int brandId)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<CatalogItem>(GetQuery(CatalogQuery.GetCatalogItemsByTypeAndBrandId), new { typeId, brandId });

        }

        public async Task<CatalogItem?> GetCatalogItemByName(string name)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<CatalogItem>(GetQuery(CatalogQuery.GetCatalogItemByName), new { name });
        }

        public async Task<IEnumerable<CatalogItem?>> GetCatalogItemsByIds(int[] ids)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<CatalogItem>(GetQuery(CatalogQuery.GetCatalogItemByIds), new { ids }) ?? throw new NpgsqlException();
        }

        public async Task<CatalogBrand?> GetCatalogBrand(int id)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<CatalogBrand>(GetQuery(CatalogQuery.GetCatalogBrand), new { id });
        }

        internal async Task<bool> UpdateCatalogBrand(CatalogBrand brand)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.ExecuteAsync(GetQuery(CatalogQuery.UpdateCatalogBrand), brand) > 0;
        }
    }
}
