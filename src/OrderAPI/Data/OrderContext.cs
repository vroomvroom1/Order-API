using Microsoft.EntityFrameworkCore;

namespace OrderAPI.Models
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {

        }

        public DbSet<Order> OrderItems {get; set;}
    }
}