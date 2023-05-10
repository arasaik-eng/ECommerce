using AutoMapper;
using ECommerce.Api.Orders.Data;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrderDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public OrdersProvider(OrderDbContext dbContext, IMapper mapper, ILogger<OrdersProvider> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            SeedData();
        }

        private void SeedData()
        {
            if (_dbContext.Orders.Any()) return;
            _dbContext.Orders.Add(new Order { Id = 1, CustomerId = 1, OrderDate = DateTime.Now, Total = 2000 });
            _dbContext.Orders.Add(new Order { Id = 2, CustomerId = 2, OrderDate = DateTime.Now.AddDays(-3), Total = 3000 });
            _dbContext.Orders.Add(new Order { Id = 3, CustomerId = 1, OrderDate = DateTime.Now.AddDays(-20), Total = 4000 });
            _dbContext.Orders.Add(new Order { Id = 4, CustomerId = 2, OrderDate = DateTime.Now.AddDays(2), Total = 5000 });
            _dbContext.SaveChanges();
        }

        public async Task<(bool IsSuccess, OrderDto Order, string ErrorMessage)> GetOrderAsync(int id)
        {
            try
            {
                var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
                if (order is not null)
                {
                    var result = _mapper.Map<OrderDto>(order);
                    return (true, result, null);
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

        public async Task<(bool IsSuccess, IEnumerable<OrderDto> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var orders = await _dbContext.Orders.Where(x => x.CustomerId == customerId).ToListAsync();
                if (orders is not null)
                {
                    var result = _mapper.Map<IEnumerable<OrderDto>>(orders);
                    return (true, result, null);
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
