using System;
using Xunit;
using OrderAPI.Models;
namespace OrderAPI.Tests
{
    public class OrderTests
    {

        Order testOrder;

        public OrderTests()
        {
            testOrder = new Order
            {
                OrderType = "Standard",
                CustomerName = "Matt Vroom",
                CreatedDate = new DateTime(),
                CreatedByUsername = "mvroom"
            };
        }

        [Fact]
        public void CanChangeCustomerName()
        {
            testOrder.CustomerName = "Joe";

            Assert.Equal("Joe", testOrder.CustomerName);
        } 

        [Fact]
        public void CanChangeOrderType()
        {
            testOrder.OrderType = "SaleOrder";

            Assert.Equal("SaleOrder", testOrder.OrderType);
        }

        [Fact]
        public void CanChangeUsername()
        {
            testOrder.CreatedByUsername = "joe412";

            Assert.Equal("joe412", testOrder.CreatedByUsername);
        }
    }
}