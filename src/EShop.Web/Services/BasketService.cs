using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basket.Api.Entities;

namespace EShop.Web.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _client;

        public BasketService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<ShoppingCartModel> GetBasket(string userName)
        {
            var response = await _client.GetAsync($"/Basket/{userName}");
            return await response.Content.ReadFromJsonAsync<ShoppingCartModel>() ?? throw new Exception("Something went wrong when calling api.");
        }

        public async Task<ShoppingCartModel> UpdateBasket(ShoppingCartModel model)
        {
            var response = await _client.PostAsJsonAsync($"/Basket", model);

            if (response != null && response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<ShoppingCartModel>();
                return content ?? throw new Exception("The response content is empty after updating the basket.");
            }
            else
            {
                var errorMessage = response != null
                    ? $"Error calling API. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}"
                    : "Error calling API. The response is null.";

                throw new Exception(errorMessage);
            }
        }


        //public async Task CheckoutBasket(BasketCheckoutModel model)
        //{
        //    var response = await _client.PostAsJson($"/Basket/Checkout", model);
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        throw new Exception("Something went wrong when calling api.");
        //    }
        //}
    }
}