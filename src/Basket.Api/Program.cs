using Basket.Api.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using StackExchange.Redis;

namespace Basket.Api
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var configuration = new ConfigurationBuilder()
                        .AddEnvironmentVariables() 
                       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                       .Build();
            
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddBasketApi();


            services.AddStackExchangeRedisCache(options =>
                        {
                          options.Configuration = Environment.GetEnvironmentVariable("CacheSettings:ConnectionString");
                        });



            var app = builder.Build();
            {
                app.UseMiddleware<ErrorHandlerMiddleware>();
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.MapControllers();
            }

            app.Run();
        }
    }
}