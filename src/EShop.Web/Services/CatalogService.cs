using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EShop.Web.Models.Catalog;

namespace EShop.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;
        public CatalogService(HttpClient client, ILogger<CatalogService> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task CreateCatalog(CatalogItemModel model)
        {   
            Console.WriteLine("Model contents: " + model.Description);
            var content = new StringContent(
            JsonSerializer.Serialize(model),
            Encoding.UTF8,
            "application/json");
            
            var request = await _client.PostAsync("/api/products", content);
           if (request.IsSuccessStatusCode)
           {
            
           }
           else
            {
                var errorMessage = request != null
                    ? $"Error calling API. Status Code: {request.StatusCode}, Reason: {request.ReasonPhrase}"
                    : "Error calling API. The response is null.";

                throw new Exception(errorMessage);
            }

        }

        public Task<CatalogItemModel> CreateBrand(CatalogBrandModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CatalogBrandModel>> GetBrands()
        {
             var response = await _client.GetAsync("/api/brands");
            if (response != null && response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<List<CatalogBrandModel>>();
                return content ?? throw new Exception("The response content is empty after updating the catalog.");
            }
            else
            {
                var errorMessage = response != null
                    ? $"Error calling API. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}"
                    : "Error calling API. The response is null.";

                throw new Exception(errorMessage);
            }

        }

        public async Task<IEnumerable<CatalogItemModel>> GetCatalog()
        {
            var response = await _client.GetAsync("/api/products");
            if (response != null && response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<List<CatalogItemModel>>();
                return content ?? throw new Exception("The response content is empty after updating the catalog.");
            }
            else
            {
                var errorMessage = response != null
                    ? $"Error calling API. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}"
                    : "Error calling API. The response is null.";

                throw new Exception(errorMessage);
            }

        }

        public async Task<CatalogItemModel> GetCatalog(string id)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CatalogItemModel>> GetCatalogByCategory(string category)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CatalogTypeModel>> GetCategories()
        {
            var response = await _client.GetAsync("/api/types");
            if (response != null && response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<List<CatalogTypeModel>>();
                return content ?? throw new Exception("The response content is empty after updating the catalog.");
            }
            else
            {
                var errorMessage = response != null
                    ? $"Error calling API. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}"
                    : "Error calling API. The response is null.";

                throw new Exception(errorMessage);
            }
        }

        public async Task<IEnumerable<CatalogTypeModel>> GetType(int id)
        {
            var response = await _client.GetAsync($"/api/type/{id}");
            if (response != null && response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<List<CatalogTypeModel>>();
                return content ?? throw new Exception("The response content is empty after updating the catalog.");
            }
            else
            {
                var errorMessage = response != null
                    ? $"Error calling API. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}"
                    : "Error calling API. The response is null.";

                throw new Exception(errorMessage);
            }
        }

      
    }
}