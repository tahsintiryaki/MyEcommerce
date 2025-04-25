namespace MyEcommerce.Order.Application.Dtos;

public class OrderDetailsDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Amount { get; set; }
}