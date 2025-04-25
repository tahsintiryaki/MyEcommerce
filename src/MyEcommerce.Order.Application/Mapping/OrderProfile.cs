using System.Data;
using AutoMapper;
using MyEcommerce.Order.Application.Dtos;
using MyEcommerce.Order.Domain.Entities;

namespace MyEcommerce.Order.Application.Mapping;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<CreateOrderDto, Orders>().ReverseMap();
        CreateMap<OrderDetailsDto, OrderDetails>().ReverseMap();
        CreateMap<OrderResponseDto, Orders>().ReverseMap();
    }
}