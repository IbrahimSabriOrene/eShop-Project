using Basket.Api.Entities;
using Basket.Api.Helpers;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Basket.Api.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IDistributedCache _redisCache;
        
        private readonly ILogger<ShoppingCartRepository> _logger;
       
        public ShoppingCartRepository(IDistributedCache redisCache, ILogger<ShoppingCartRepository> logger)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ShoppingCart> GetShoppingCartAsync(string userId)
        {
            
            var basket = await _redisCache.GetStringAsync(userId);
            var result = JsonConvert.DeserializeObject<ShoppingCart>(basket);

            return result ?? throw new AppException("Couldn't find any basket"); // We will made this custom app exception later.
        }

       

        public async Task<ShoppingCart> UpdateShoppingCartAsync(ShoppingCart basket)
        {
            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            
            return await GetShoppingCartAsync(basket.UserName);
        }



    }
}