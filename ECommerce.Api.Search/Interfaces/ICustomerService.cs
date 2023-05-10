using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Interfaces
{
    public interface ICustomerService
    {
        Task<(bool IsSuccess, dynamic Customers, string ErrorMessage)> GetCustomersAsync(int id);
    }
}
