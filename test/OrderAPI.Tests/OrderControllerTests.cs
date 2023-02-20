using System;
using Xunit;
using Moq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Controllers;
using OrderAPI.Data;
using OrderAPI.Profiles;
using OrderAPI.Dtos;
using OrderAPI.Models;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json.Serialization;

namespace OrderAPI.Tests
{
    public class OrdersControllerTests : IDisposable
    {
        Mock<IOrderAPIRepo> mockRepo;
        OrdersProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;
        public OrdersControllerTests()
        {
            mockRepo = new Mock<IOrderAPIRepo>();
            realProfile = new OrdersProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
        }

        public void Dispose()
        {
            mockRepo = null;
            mapper = null;
            configuration = null;
            realProfile = null;
        }
            
        [Fact]
        public void GetAllOrders_ReturnsZeroResources_WhenDBIsEmpty()
        {
            mockRepo.Setup(repo =>
              repo.GetOrderItemsByOrderType("Standard")).Returns(GetOrders(0));

            var controller = new OrdersController(mockRepo.Object, mapper);
            var result = controller.GetOrderItemsByOrderType("Standard");

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllOrders_ReturnsOneResource_WhenDBHasOneResource()
        {
            mockRepo.Setup(repo =>
              repo.GetOrderItemsByOrderType("Standard")).Returns(GetOrders(1));

            var controller = new OrdersController(mockRepo.Object, mapper);
            var result = controller.GetOrderItemsByOrderType("Standard");
            var okResult = result.Result as OkObjectResult;
            var orders = okResult.Value as List<OrderGetDto>;

            Assert.Single(orders);
        }

        [Fact]
        public void GetAllOrders_Returns200OK_WhenDBHasOneResource()
        { 
            mockRepo.Setup(repo =>
              repo.GetOrderItemsByOrderType("Standard")).Returns(GetOrders(1));

            var controller = new OrdersController(mockRepo.Object, mapper);
            var result = controller.GetOrderItemsByOrderType("Standard");

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllOrders_ReturnsCorrectType_WhenDBHasOneResource()
        { 
            mockRepo.Setup(repo =>
              repo.GetOrderItemsByOrderType("Standard")).Returns(GetOrders(1));

            var controller = new OrdersController(mockRepo.Object, mapper);
            var result = controller.GetOrderItemsByOrderType("Standard");

            Assert.IsType<ActionResult<IEnumerable<OrderGetDto>>>(result);
        }

        [Fact]
        public void GetOrderByID_Returns404NotFound_WhenNonExistentIDProvided()
        {
            var id = new Guid("8baceb64-46a8-499b-9495-49160fbcb039");

            mockRepo.Setup(repo =>
              repo.GetOrderItemById(id)).Returns(() => null);

            var controller = new OrdersController(mockRepo.Object, mapper);
            var result = controller.GetOrderItemById(id);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetOrderByID_Returns200OK__WhenValidIDProvided()
        {
            var id = new Guid("8baceb64-46a8-499b-9495-49160fbcb039");
            var order = new Order
            {
                Id = new Guid("8baceb64-46a8-499b-9495-49160fbcb039"),
                OrderType = "Standard",
                CustomerName = "Matt Vroom",
                CreatedDate = new DateTime(),
                CreatedByUsername = "mvroom"
            };

            mockRepo.Setup(repo =>
              repo.GetOrderItemById(id)).Returns(order);

            var controller = new OrdersController(mockRepo.Object, mapper);
            var result = controller.GetOrderItemById(id);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetOrderByID_ReturnsCorrectResouceType_WhenValidIDProvided()
        {
            var id = new Guid("8baceb64-46a8-499b-9495-49160fbcb039");
            var order = new Order
            {
                Id = new Guid("8baceb64-46a8-499b-9495-49160fbcb039"),
                OrderType = "Standard",
                CustomerName = "Matt Vroom",
                CreatedDate = new DateTime(),
                CreatedByUsername = "mvroom"
            };

            mockRepo.Setup(repo =>
              repo.GetOrderItemById(id)).Returns(order);

            var controller = new OrdersController(mockRepo.Object, mapper);
            var result = controller.GetOrderItemById(id);

            Assert.IsType<ActionResult<OrderGetDto>>(result);
        }

        [Fact]
        public void PostOrder_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            var id = new Guid("8baceb64-46a8-499b-9495-49160fbcb039");
            var order = new Order
            {
                Id = new Guid("8baceb64-46a8-499b-9495-49160fbcb039"),
                OrderType = "Standard",
                CustomerName = "Matt Vroom",
                CreatedDate = new DateTime(),
                CreatedByUsername = "mvroom"
            };

            mockRepo.Setup(repo =>
              repo.GetOrderItemById(id)).Returns(order);

            var controller = new OrdersController(mockRepo.Object, mapper);
            var result = controller.PostOrderItem(new OrderPostDto { });

            Assert.IsType<ActionResult<OrderPostDto>>(result);
        }

        [Fact]
        public void PostCommand_Returns201Created_WhenValidObjectSubmitted()
        {
            var id = new Guid("8baceb64-46a8-499b-9495-49160fbcb039");
            var order = new Order
            {
                Id = new Guid("8baceb64-46a8-499b-9495-49160fbcb039"),
                OrderType = "Standard",
                CustomerName = "Matt Vroom",
                CreatedDate = new DateTime(),
                CreatedByUsername = "mvroom"
            };

            mockRepo.Setup(repo =>
              repo.GetOrderItemById(id)).Returns(order);

            var controller = new OrdersController(mockRepo.Object, mapper);
            var result = controller.PostOrderItem(new OrderPostDto { });

            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public void PutCommand_Returns204NoContent_WhenValidObjectSubmitted()
        {
            var id = new Guid("8baceb64-46a8-499b-9495-49160fbcb039");
            var order = new Order
            {
                Id = new Guid("8baceb64-46a8-499b-9495-49160fbcb039"),
                OrderType = "Standard",
                CustomerName = "Matt Vroom",
                CreatedDate = new DateTime(),
                CreatedByUsername = "mvroom"
            };

            mockRepo.Setup(repo =>
              repo.GetOrderItemById(id)).Returns(order);

            var controller = new OrdersController(mockRepo.Object, mapper);
            var result = controller.PutOrderItem(id, new OrderPutDto { });

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void PutOrder_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            var id = new Guid("8baceb64-46a8-499b-9495-49160fbcb039");
            var order = new Order
            {
                Id = new Guid("8baceb64-46a8-499b-9495-49160fbcb039"),
                OrderType = "Standard",
                CustomerName = "Matt Vroom",
                CreatedDate = new DateTime(),
                CreatedByUsername = "mvroom"
            };

            mockRepo.Setup(repo =>
              repo.GetOrderItemById(id)).Returns(() => null);

            var controller = new OrdersController(mockRepo.Object, mapper);
            var result = controller.PutOrderItem(id, new OrderPutDto { });

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteCommand_Returns200OK_WhenValidResourceIDSubmitted()
        {
            var id = new Guid("8baceb64-46a8-499b-9495-49160fbcb039");
            var order = new Order
            {
                Id = new Guid("8baceb64-46a8-499b-9495-49160fbcb039"),
                OrderType = "Standard",
                CustomerName = "Matt Vroom",
                CreatedDate = new DateTime(),
                CreatedByUsername = "mvroom"
            };

            mockRepo.Setup(repo =>
              repo.GetOrderItemById(id)).Returns(order);

            var controller = new OrdersController(mockRepo.Object, mapper);
            var result = controller.DeleteOrderItem(id);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteCommand_Returns_404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            var id = new Guid("8baceb64-46a8-499b-9495-49160fbcb039");
            var order = new Order
            {
                Id = new Guid("8baceb64-46a8-499b-9495-49160fbcb039"),
                OrderType = "Standard",
                CustomerName = "Matt Vroom",
                CreatedDate = new DateTime(),
                CreatedByUsername = "mvroom"
            };

            mockRepo.Setup(repo =>
              repo.GetOrderItemById(id)).Returns(() => null);

            var controller = new OrdersController(mockRepo.Object, mapper);
            var result = controller.DeleteOrderItem(id);

            Assert.IsType<NotFoundResult>(result);           
        }


        private List<Order> GetOrders(int num)
        {
            var orders = new List<Order>();
            if (num > 0)
            {
                orders.Add(new Order
                {
                    Id = new Guid("8baceb64-46a8-499b-9495-49160fbcb039"),
                    OrderType = "Standard",
                    CustomerName = "Matt Vroom",
                    CreatedDate = new DateTime(),
                    CreatedByUsername = "mvroom"
                });
            }
            return orders;
        }
    } 
}