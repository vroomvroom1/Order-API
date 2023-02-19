using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Controllers;
using OrderAPI.Models;
namespace OrderAPI.Tests
{
    public class OrdersControllerTests : IDisposable
    {
        DbContextOptionsBuilder<OrderContext> optionsBuilder;
        OrderContext dbContext;
        OrdersController controller;
        public OrdersControllerTests()
        {
            optionsBuilder = new DbContextOptionsBuilder<OrderContext>();
            optionsBuilder.UseInMemoryDatabase("UnitTestInMemBD");
            dbContext = new OrderContext(optionsBuilder.Options);
            controller = new OrdersController(dbContext);
        }

        public void Dispose()
        {
            optionsBuilder = null;
            foreach (var cmd in dbContext.OrderItems)
            {
                dbContext.OrderItems.Remove(cmd);
            }
            dbContext.SaveChanges();
            dbContext.Dispose();
            controller = null;
        }
            
        [Fact]
        public void GetOrderItems_ReturnsZeroItems_WhenDBIsEmpty()
        {
            //DBContext
            var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();
            optionsBuilder.UseInMemoryDatabase("UnitTestInMemBD");
            var dbContext = new OrderContext(optionsBuilder.Options);

            //Controller
            var controller = new OrdersController(dbContext);

            var result = controller.GetOrderItemsByOrderType("Standard");

            Assert.Empty(result.Value);
        }
    } 
}