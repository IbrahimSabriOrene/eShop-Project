using Basket.Api.Entities;
using Basket.Api.Repositories;

namespace Basket.Api.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ILogger<ShoppingCartService> _logger;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, ILogger<ShoppingCartService> logger)
        {
            _shoppingCartRepository = shoppingCartRepository ?? throw new ArgumentNullException(nameof(shoppingCartRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ShoppingCart> GetShoppingCartAsync(string userId)
        {
            try
            {
                var shoppingCart = await _shoppingCartRepository.GetShoppingCartAsync(userId);
                return shoppingCart;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting shopping cart {userId}", userId);
                throw;
            }
        }

        public async Task<ShoppingCart> UpdateShoppingCartAsync(ShoppingCart shoppingCart)
        {

            try
            {
                await _shoppingCartRepository.UpdateShoppingCartAsync(shoppingCart);
                return shoppingCart; // change this part as well.
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error while updating shopping cart ");
                throw;
            }


        }
    }
}