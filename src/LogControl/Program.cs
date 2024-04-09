using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .WriteTo.Console();

        loggerConfiguration.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
        {
            IndexFormat = $"applogs-{builder.Environment.ApplicationName.ToLower().Replace(".", "-")}-{hostingContext.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-logs-{DateTime.UtcNow:yyyy-MM}",
            AutoRegisterTemplate = true,
            NumberOfShards = 2,
            NumberOfReplicas = 1
        });

    loggerConfiguration
        .Enrich.WithProperty("Environment", hostingContext.HostingEnvironment.EnvironmentName)
        .ReadFrom.Configuration(configuration);
});

var host = builder.Build();

await host.RunAsync();
