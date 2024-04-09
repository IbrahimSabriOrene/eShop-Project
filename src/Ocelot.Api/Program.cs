using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Ocelot.Api
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;
            var services = builder.Services;

            config.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);
            {

                services.AddControllers();
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen();
                services.AddOcelot(config);

            }


            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers();
                        });

            app.UseOcelot().Wait();

            app.Run();
        }
    }
}