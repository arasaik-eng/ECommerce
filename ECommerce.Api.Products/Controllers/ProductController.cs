using ECommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductProvider _product;

        public ProductController(IProductProvider product)
        {
            this._product = product;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await _product.GetProductsAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Products);
            }
            else { return NotFound(); }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await _product.GetProductAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Product);
            }
            else { return NotFound(); }
        }
    }
}
