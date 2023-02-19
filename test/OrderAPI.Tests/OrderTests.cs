using System;
using Xunit;
using OrderAPI.Models;
namespace OrderAPI.Tests
{
    public class OrderTests
    {
        [Fact]
        public void CanChangeHowTo()
        {
            var testOrder = new Order 
            {
                OrderType = "Standard",
                CustomerName = "Matt Vroom",
                CreatedDate = new DateTime(),
                CreatedByUsername = "mvroom"
            };

            Assert.Equal("Matt Vroom", testOrder.CustomerName);
        } 
    }
}