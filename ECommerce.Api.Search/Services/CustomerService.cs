using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using System.Text.Json;

namespace ECommerce.Api.Search.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(IHttpClientFactory httpClientFactory, ILogger<CustomerService> logger)
        {
            this._httpClientFactory = httpClientFactory;
            this._logger = logger;
        }
        public async Task<(bool IsSuccess, dynamic Customers, string ErrorMessage)> GetCustomersAsync(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("CustomerService");
                var response = await client.GetAsync($"api/customer/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var customer = JsonSerializer.Deserialize<dynamic>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = false });
                    return (true, customer, null);
                }
                else
                {
                    return (false, null, response.ReasonPhrase);
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
