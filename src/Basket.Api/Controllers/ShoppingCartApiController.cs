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
            _logger = logger;
            _cart = cart;
        }


        [HttpGet("GetShoppingCartId/{userId}")]
        public async Task<IActionResult> GetShoppingCartAsync(string userId)
        {
            var cart = await _cart.GetShoppingCartAsync(userId);
            return Ok(cart);
        }

        [HttpPost("UpdateShoppingCartAsync")]
        public async Task<IActionResult> UpdateShoppingCartAsync(ShoppingCart cart)
        {
            var basket = await _cart.UpdateShoppingCartAsync(cart);
            return Ok(basket);
        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok();
        }
    }
}