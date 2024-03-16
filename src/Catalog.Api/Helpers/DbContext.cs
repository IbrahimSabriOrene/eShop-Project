using System.Data;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;


namespace Catalog.Api.Helpers;
public class DbContext
{
    private readonly DbSettings _dbSettings;

    public DbContext(IOptions<DbSettings> dbSettings)
    {
        _dbSettings = dbSettings.Value ?? throw new ArgumentNullException(nameof(dbSettings));
    }

    public IDbConnection CreateConnection()
    {
        var connectionString = $"Host={_dbSettings.DB_SERVER}; Database={_dbSettings.DB_DATABASE}; Username={_dbSettings.DB_USERID}; Password={_dbSettings.DB_PASSWORD};";
        return new NpgsqlConnection(connectionString);
    }

    public async Task Init()
    {
        await _initDatabase();
        await _initTables();
    }

    private async Task _initDatabase()
    {
        var connectionString = $"Host={_dbSettings.DB_SERVER}; Database=postgres; Username={_dbSettings.DB_USERID}; Password={_dbSettings.DB_PASSWORD};";
        using var connection = new NpgsqlConnection(connectionString);
        
        var sqlDbCount = $"SELECT COUNT(*) FROM pg_database WHERE datname = '{_dbSettings.DB_DATABASE}';";
        var dbCount = await connection.ExecuteScalarAsync<int>(sqlDbCount);
        if (dbCount == 0)
        {
            var sql = $"CREATE DATABASE \"{_dbSettings.DB_DATABASE}\"";
            await connection.ExecuteAsync(sql);
        }
    }
    private async Task _initTables()
    {
        using var connection = CreateConnection();

        await connection.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS CatalogBrand (
                Id SERIAL PRIMARY KEY,
                Brand VARCHAR
            )");

        await connection.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS CatalogType (
                Id SERIAL PRIMARY KEY,
                Type VARCHAR,
                BrandId INT,
                FOREIGN KEY (BrandId) REFERENCES CatalogBrand(Id)
            )");

        await connection.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS CatalogItem (
                Id SERIAL PRIMARY KEY,
                Name VARCHAR,
                Category VARCHAR,
                Summary VARCHAR,
                Description VARCHAR,
                ImageFile VARCHAR,
                Price DECIMAL,
                CatalogBrandId INT,
                CatalogTypeId INT,
                FOREIGN KEY (CatalogBrandId) REFERENCES CatalogBrand(Id),
                FOREIGN KEY (CatalogTypeId) REFERENCES CatalogType(Id)
            )");
    }
}
