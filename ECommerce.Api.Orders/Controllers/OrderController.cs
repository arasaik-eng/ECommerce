using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrdersProvider _orders;

        public OrderController(IOrdersProvider orders)
        {
            this._orders = orders;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrdersAsync(int customerId)
        {
            var result = await _orders.GetOrdersAsync(customerId);
            if (result.IsSuccess)
            {
                return Ok(result.Orders);
            }
            return NotFound();
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetOrderAsync(int id)
        //{
        //    var result = await _orders.GetOrderAsync(id);
        //    if (result.IsSuccess)
        //    {
        //        return Ok(result.Order);
        //    }
        //    return NotFound();
        //}
    }
}
