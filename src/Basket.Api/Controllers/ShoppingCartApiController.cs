using Microsoft.AspNetCore.Mvc;
using Basket.Api.Services;
using Basket.Api.Entities;

namespace Basket.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class ShoppingCartApiController : ControllerBase
    {

        private readonly ILogger<ShoppingCartApiController> _logger;
        private readonly IShoppingCartService _cart;
        public ShoppingCartApiController(ILogger<ShoppingCartApiController> logger, IShoppingCartService cart)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cart = cart ?? throw new ArgumentNullException(nameof(logger));
        }


        [HttpGet("GetShoppingCartId/{userName}")]
        public async Task<IActionResult> GetShoppingCartAsync(string userName)
        {
            var cart = await _cart.GetShoppingCartAsync(userName);
            return Ok(cart);
        }

        [HttpPost("UpdateShoppingCartAsync")]
        public async Task<IActionResult> UpdateShoppingCartAsync(ShoppingCart cart)
        {
            var basket = await _cart.UpdateShoppingCartAsync(cart);
            return Ok(basket);
        }

    }
}