using MyEcommerce.Product.Application.Dtos;
using MyEcommerce.SharedLibrary.ProductService.Dtos;


namespace MyEcommerce.Product.Application.Interfaces.Services;

public interface IProductService
{
    public Task<ProductDto> CreateProduct(CreateProductDto request);
    public Task<List<ProductDto>> GetProducts();
}