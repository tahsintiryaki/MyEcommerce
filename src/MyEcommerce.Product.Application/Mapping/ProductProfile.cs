using AutoMapper;
using MyEcommerce.Product.Application.Dtos;
using MyEcommerce.SharedLibrary.ProductService.Dtos;

namespace MyEcommerce.User.Application.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductDto, Product.Domain.Entities.Product>().ReverseMap();
        CreateMap<ProductDto, Product.Domain.Entities.Product>().ReverseMap();
    }
}