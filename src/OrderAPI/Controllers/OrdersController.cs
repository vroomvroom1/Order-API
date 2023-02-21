using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using OrderAPI.Models;
using OrderAPI.Data;
using OrderAPI.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace OrderAPI.Controllers
{
    [Route("api/v1/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderAPIRepo _repository;
        private readonly IMapper _mapper;

        public OrdersController(IOrderAPIRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //GET: api/v1/orders?orderType={orderType}
        [HttpGet]
        public ActionResult<IEnumerable<OrderGetDto>> GetOrderItemsByOrderType(string orderType)
        {
            var orderItems = _repository.GetOrderItemsByOrderType(orderType);

            if (orderItems == null)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<OrderGetDto>>(orderItems));
        }

        //GET: api/v1/orders/{Id}
        [Authorize]
        [HttpGet("{id}", Name = "GetOrderItemById")]
        public ActionResult<OrderGetDto> GetOrderItemById(Guid Id)
        {
            var orderItem = _repository.GetOrderItemById(Id);

            if (orderItem == null)
                return NotFound();

            return Ok(_mapper.Map<OrderGetDto>(orderItem));
        } 

        //GET: api/v1/orders
        [HttpGet]
        public ActionResult<IEnumerable<OrderGetDto>> GetOrderItems()
        {
            var orderItems = _repository.GetOrderItems();

            if (orderItems == null)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<OrderGetDto>>(orderItems));
        }         

        //POST: api/v1/orders
        [HttpPost]
        public ActionResult<OrderPostDto> PostOrderItem(OrderPostDto orderPostDto)
        {
            var orderModel = _mapper.Map<Order>(orderPostDto);
            _repository.PostOrderItem(orderModel);
            _repository.SaveChanges();

            var orderGetDto = _mapper.Map<OrderGetDto>(orderModel);

            return CreatedAtRoute(nameof(GetOrderItemById), new {Id = orderGetDto.Id}, orderGetDto);
        }

        //PUT: api/v1/orders/{Id}
        [Authorize]
        [HttpPut("{id}")]
        public ActionResult PutOrderItem(Guid id, OrderPutDto orderPutDto)
        {
            var orderModelFromRepo = _repository.GetOrderItemById(id);

            if (orderModelFromRepo == null)
                return NotFound();

            orderModelFromRepo.CustomerName = orderPutDto.CustomerName;
            orderModelFromRepo.CreatedByUsername = orderPutDto.CreatedByUsername;
            orderModelFromRepo.OrderType = orderPutDto.OrderType;

            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/v1/orders/{Id}
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult DeleteOrderItem(Guid id)
        {
            var orderModelFromRepo = _repository.GetOrderItemById(id);
            if(orderModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteOrderItem(orderModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
