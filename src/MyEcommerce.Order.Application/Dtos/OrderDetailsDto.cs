namespace MyEcommerce.Order.Application.Dtos;

public class OrderDetailsDto
{
    public string ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Amount { get; set; }
}