using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace OrderAPI.Controllers
{
    [Route("api/v1/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderContext _context;
        public OrdersController(OrderContext context) => _context = context;

        //GET: api/v1/orders?orderType={orderType}
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrderItemsByOrderType(string orderType)
        {
            var orderItems = _context.OrderItems.Where(o => o.OrderType == orderType);

            return orderItems.ToList();
        }

        //POST: api/v1/orders
        [HttpPost]
        public ActionResult<Order> PostOrderItem(Order order)
        {
            _context.OrderItems.Add(order);

            try 
            {
                _context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return CreatedAtAction("GetOrderItem", new Order{Id = order.Id}, order);
        }

        //PUT: api/v1/orders/{Id}
        [HttpPut("{id}")]
        public ActionResult PutOrderItem(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        //DELETE api/v1/orders/{Id}
        [HttpDelete("{id}")]
        public ActionResult<Order> DeleteOrderItem(int id)
        {
            var orderItem = _context.OrderItems.Find(id);

            if (orderItem == null)
                return NotFound();

            _context.OrderItems.Remove(orderItem);
            _context.SaveChanges();

            return orderItem;
        }
    }
}