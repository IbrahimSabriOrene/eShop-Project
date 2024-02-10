using Basket.Api;
using Basket.Api.Helpers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
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
                options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
            });



var app = builder.Build();
{
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
}

app.Run();
