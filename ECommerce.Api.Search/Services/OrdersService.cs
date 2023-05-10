using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace ECommerce.Api.Search.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<OrdersService> _logger;

        public OrdersService(IHttpClientFactory httpClientFactory, ILogger<OrdersService> logger)
        {
            this._httpClientFactory = httpClientFactory;
            this._logger = logger;
        }
        public async Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("OrderService");
                var response = await client.GetAsync($"api/order/{customerId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content, options);
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
