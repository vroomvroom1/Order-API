using AutoMapper;
using OrderAPI.Dtos;
using OrderAPI.Models;

namespace OrderAPI.Profiles
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            CreateMap<Order, OrderGetDto>();
            CreateMap<OrderPostDto, Order>();
            CreateMap<OrderPutDto, Order>();
        }
    }
}