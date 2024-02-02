using Basket.Api.Repositories;
using Basket.Api.Services;

namespace Basket.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBasketApi(this IServiceCollection services)
        {
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            return services;
        }
    }
}