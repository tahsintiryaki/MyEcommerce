namespace MyEcommerce.Product.Application.Dtos;

public class CreateProductDto
{
    public string Name { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
}