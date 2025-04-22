using AutoMapper;
using MyEcommerce.Cache;
using MyEcommerce.Product.Application.Dtos;
using MyEcommerce.Product.Application.Interfaces.Repositories;
using MyEcommerce.Product.Application.Interfaces.Services;
using MyEcommerce.SharedLibrary.ProductService.Dtos;
using Newtonsoft.Json;

namespace MyEcommerce.Product.Persistence.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    public ProductService(IProductRepository productRepository, IMapper mapper, ICacheService cacheService)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<ProductDto> CreateProduct(CreateProductDto request)
    {
        var mapping = _mapper.Map<Domain.Entities.Product>(request);
        var response = await _productRepository.InsertAsync(mapping);
        return _mapper.Map<ProductDto>(response);
    }

    public async Task<List<ProductDto>> GetProducts()
    {
        var products = await _cacheService.GetAsync<List<ProductDto>>("ProductList");
        if (products is null)
        {
            var response = await _productRepository.GetAsync();
            var mappingProducts = _mapper.Map<List<ProductDto>>(response);
            await _cacheService.SetAsync("ProductList", mappingProducts);
            return mappingProducts;
        }

        return products;
    }
}