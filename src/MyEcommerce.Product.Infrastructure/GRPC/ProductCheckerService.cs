using Grpc.Core;
using MyEcommerce.Product.Application.Interfaces.Repositories;
using MyEcommerce.SharedLibrary;


namespace MyEcommerce.Product.Infrastructure.GRPC;

public class ProductCheckerService : ProductChecker.ProductCheckerBase
{
    private readonly IProductRepository _productRepository;

    public ProductCheckerService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public override async Task<ProductsResponse> CheckProducts(ProductsRequest request, ServerCallContext context)
    {
        var missingIds = await _productRepository.GetNonExistingProductIdsAsync(request.ProductIds.ToList());
        return new ProductsResponse { ProductIds = { missingIds } };
    }
}