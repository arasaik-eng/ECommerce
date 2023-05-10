using ECommerce.Api.Search.Interfaces;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService _ordersService;
        private readonly IProductsService _productService;
        private readonly ICustomerService _customerService;

        public SearchService(IOrdersService ordersService, IProductsService productService, ICustomerService customerService)
        {
            this._ordersService = ordersService;
            this._productService = productService;
            this._customerService = customerService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResult)> SearchAsync(int customerId)
        {
            var orderResult = await _ordersService.GetOrdersAsync(customerId);
            var productResult = await _productService.GetProductsAsync();
            var customerResult = await _customerService.GetCustomersAsync(customerId);
            if (orderResult.IsSuccess)
            {
                foreach (var order in orderResult.Orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productResult.IsSuccess ?
                            productResult.Products.FirstOrDefault(x => x.Id == item.ProductId)?.Name :
                            "Product Info Is Not Available.";
                    }
                }

                var result = new
                {
                    Customers = customerResult.IsSuccess ? customerResult.Customers : new { Name = "Customer info is not availbale." },
                    orderResult.Orders,
                };
                return (true, result);
            }
            else
            {
                return (false, null);
            }
        }
    }
}