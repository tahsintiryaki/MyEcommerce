using MyEcommerce.Order.Domain.Entities;

namespace MyEcommerce.Order.Application.Dtos;

public class CreateOrderDto
{
    public Guid UserId { get; set; }
    public string OrderNo { get; set; }
    public OrderStatusEnums Orderstatus { get; set; }
    public List<OrderDetailsDto> OrderDetails { get; set; }
}