using Basket.Api.Entities;

namespace Basket.Api.Repositories
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetShoppingCartAsync(string userId);
        Task<ShoppingCart> UpdateShoppingCartAsync(ShoppingCart basket);
    }
}