using Catalog.Api.Controllers;
using Catalog.Api.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Catalog.Api
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

             var configuration = new ConfigurationBuilder()
                       .AddEnvironmentVariables() 
                       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                       .Build();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCatalogServices();
            builder.Services.Configure<DbSettings>(builder.Configuration.GetSection(nameof(DbSettings)));


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            {
                using var scope = app.Services.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<DbContext>();
                await context.Init();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapCatalogApi();

            app.Run();
        }
    }
}