using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using OrderAPI.Models;

namespace OrderAPI.Controllers
{
    [Route("api/v1/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderContext _context;
        public OrdersController(OrderContext context) => _context = context;

        //GET: api/v1/orders
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrderItems()
        {
            return _context.OrderItems;
        }
    }
}