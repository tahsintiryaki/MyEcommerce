using MyEcommerce.Order.Application.Dtos;

namespace MyEcommerce.Order.Application.Interfaces.Services;

public interface IOrderService
{
    Task CreateOrderWithOrderDetails(CreateOrderDto request);
    Task<List<OrderResponseDto>> GetAll();
}