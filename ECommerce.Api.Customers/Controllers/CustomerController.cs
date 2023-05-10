using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomersProvider _customers;

        public CustomerController(ICustomersProvider customersProvider)
        {
            this._customers = customersProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await _customers.GetCustomersAsync();
            if (result.IsSuccessful)
            {
                return Ok(result.Customers);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var result = await _customers.GetCustomerAsync(id);
            if (result.IsSuccessful)
            {
                return Ok(result.Customer);
            }
            else { return NotFound(); }
        }
    }
}
