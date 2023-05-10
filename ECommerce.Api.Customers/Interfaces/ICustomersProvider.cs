using ECommerce.Api.Customers.Models;

namespace ECommerce.Api.Customers.Interfaces
{
    public interface ICustomersProvider
    {
        Task<(bool IsSuccessful, IEnumerable<CustomerDto> Customers, string ErrorMessage)> GetCustomersAsync();
        Task<(bool IsSuccessful, CustomerDto Customer, string ErrorMessage)> GetCustomerAsync(int id);

    }
}
