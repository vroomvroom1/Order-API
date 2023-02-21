using System.Collections.Generic;
using OrderAPI.Models;

namespace OrderAPI.Data
{
    public interface IOrderAPIRepo
    {
        bool SaveChanges();

        IEnumerable<Order> GetOrderItemsByOrderType(string orderType);
        IEnumerable<Order> GetOrderItems();
        Order GetOrderItemById(Guid id);
        void PostOrderItem(Order order);
        void PutOrderItem(Order order);
        void DeleteOrderItem(Order order);
    }
}