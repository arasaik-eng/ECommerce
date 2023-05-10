using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Providers
{
    public class ProductProvider : IProductProvider
    {
        private readonly ProductDbContext _dbContext;
        private readonly ILogger<ProductProvider> _logger;
        private readonly IMapper _mapper;

        public ProductProvider(ProductDbContext dbContext, ILogger<ProductProvider> logger, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._logger = logger;
            this._mapper = mapper;
            //SeedData();
        }

        private void SeedData()
        {
            if (_dbContext != null)
            {
                if (_dbContext.Products.Any()) return;
                _dbContext.Products.Add(new Product { Id = 1, Name = "Monitor", Inventory = "InvTest1", Price = 1200 });
                _dbContext.Products.Add(new Product { Id = 2, Name = "Mouse", Inventory = "InvTest2", Price = 8200 });
                _dbContext.Products.Add(new Product { Id = 3, Name = "Case", Inventory = "InvTest3", Price = 6200 });
                _dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, Product Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var result = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (result == null)
                {
                    return (false, null, "Not Found");
                }
                else
                {
                    var product = _mapper.Map<Product>(result);
                    return (true, product, null);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var result = await _dbContext.Products.ToListAsync();
                if (result != null && result.Any())
                {
                    var product = _mapper.Map<IEnumerable<Product>>(result);
                    return (true, product, null);
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
