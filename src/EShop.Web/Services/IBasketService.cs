using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basket.Api.Entities;

namespace EShop.Web.Services
{
     public interface IBasketService
    {
        Task<ShoppingCartModel> GetBasket(string userName);
        Task<ShoppingCartModel> UpdateBasket(ShoppingCartModel model);
        // Task CheckoutBasket(// BasketCheckoutModel model);
    }
}