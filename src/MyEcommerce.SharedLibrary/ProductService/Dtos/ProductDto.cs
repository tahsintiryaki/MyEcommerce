namespace MyEcommerce.SharedLibrary.ProductService.Dtos;

public class ProductDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
}