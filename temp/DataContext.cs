
namespace Basket.Api.Helpers;

public class DataContext
{
    private readonly ILogger<DataContext> _logger;
    private readonly DbSettings _dbSettings;
    public DataContext(ILogger<DataContext> logger, IOptions<DbSettings> dbSettings)
    {
        _logger = logger;
        _dbSettings = dbSettings.Value;
    }

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_dbSettings.ConnectionString);
    }

    public async Task Init()
    {
        await InitDatabase();
        await InitTables();
    }

    private async Task InitDatabase()
    {
        // create database if it doesn't exist
        var connectionString = $"Host={_dbSettings.Server}; Database=postgres; Username={_dbSettings.UserId}; Password={_dbSettings.Password};";
        using var connection = new NpgsqlConnection(connectionString);

        var sqlDbCount = $"SELECT COUNT(*) FROM pg_database WHERE datname = '{_dbSettings.Database}';";
        var dbCount = await connection.ExecuteScalarAsync<int>(sqlDbCount);
        if (dbCount == 0)
        {
            _logger.LogInformation("Database was not found. Executing the database query.");
            // Change this sql code
            var sql = $"CREATE DATABASE \"{_dbSettings.Database}\"";
            await connection.ExecuteAsync(sql);
        }
    }

    private async Task InitTables()
    {
        // create tables if they don't exist
        using var connection = CreateConnection();
        await _initUsers();

        async Task _initUsers()
        {
            // Change this sql code
            var sql = $"CREATE TABLE IF NOT EXISTS User (  Id SERIAL PRIMARY KEY, Title VARCHAR,LastName VARCHAR, Email VARCHAR,Role INTEGER,PasswordHash VARCHAR);";
            await connection.ExecuteAsync(sql);
        }

        
    }
}
