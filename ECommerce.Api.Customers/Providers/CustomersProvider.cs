using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomersProvider> _logger;

        public CustomersProvider(CustomersDbContext dbContext, IMapper mapper, ILogger<CustomersProvider> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (_dbContext.Customers.Any()) return;
            _dbContext.Customers.Add(new Customer { Id = 1, Name = "John Snow", Address = "129 Sugar Beach" });
            _dbContext.Customers.Add(new Customer { Id = 2, Name = "Sara Kia", Address = "20 Sunrise Ave." });
            _dbContext.Customers.Add(new Customer { Id = 3, Name = "Mori Hallaj", Address = "8729 Coxwell" });
            _dbContext.SaveChanges();
        }

        public async Task<(bool IsSuccessful, CustomerDto Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
                if (customer is not null)
                {
                    var result = _mapper.Map<CustomerDto>(customer);
                    return (true, result, null);
                }
                else
                {
                    return (false, null, "Not Found");
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccessful, IEnumerable<CustomerDto> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await _dbContext.Customers.ToListAsync();
                if (customers is not null && customers.Any())
                {
                    var result = _mapper.Map<IEnumerable<CustomerDto>>(customers);
                    return (true, result, null);
                }
                else
                {
                    return (false, null, "Not Found");
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
