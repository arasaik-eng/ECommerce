using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using System.Text.Json;

namespace ECommerce.Api.Search.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ProductsService> _logger;

        public ProductsService(IHttpClientFactory httpClientFactory, ILogger<ProductsService> logger)
        {
            this._httpClientFactory = httpClientFactory;
            this._logger = logger;
        }
        public async Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ProductsService");
                var response = await client.GetAsync("api/product");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var result = JsonSerializer.Deserialize<IEnumerable<Product>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return (true, result, response.ReasonPhrase);
                }
                else
                {
                    return (false, null, "Not Found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }

        }
    }
}
