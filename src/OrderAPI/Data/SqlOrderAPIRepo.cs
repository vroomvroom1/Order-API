using System;
using System.Collections.Generic;
using System.Linq;
using OrderAPI.Models;

namespace OrderAPI.Data
{
    public class SqlOrderAPIRepo : IOrderAPIRepo
    {
        private readonly OrderContext _context;

        public SqlOrderAPIRepo(OrderContext context)
        {
            _context = context;
        }

        public void PostOrderItem(Order order)
        {
            if(order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            if (order.CreatedDate == DateTime.MinValue)
            {
                order.CreatedDate = DateTime.Now;
            }

            order.Id = Guid.NewGuid();
            _context.OrderItems.Add(order);
        }

        public void DeleteOrderItem(Order order)
        {
            if(order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            _context.OrderItems.Remove(order);
        }

        public IEnumerable<Order> GetOrderItemsByOrderType(string orderType)
        {
            return _context.OrderItems.Where(o => o.OrderType == orderType);
        }

        public IEnumerable<Order> GetOrderItems()
        {
            return _context.OrderItems;
        }

        public Order GetOrderItemById(Guid id)
        {
            var order = _context.OrderItems.Find(id);

            if(order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }


            return order;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void PutOrderItem(Order order)
        {
          
        }
    }
}