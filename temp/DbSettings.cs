

namespace Basket.Api.Helpers
{
    public class DbSettings
    {
        private readonly IConfiguration _configuration;

        public DbSettings(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string? ConnectionString => _configuration.GetConnectionString("DefaultConnection");
        public string? Database => _configuration["Database"];
        public string? Server => _configuration["Server"];
        public string? UserId => _configuration["UserId"];
        public string? Password => _configuration["Password"];

        
    }
}