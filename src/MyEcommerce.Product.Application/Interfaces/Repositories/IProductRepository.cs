namespace MyEcommerce.Product.Application.Interfaces.Repositories;

public interface IProductRepository : IMongoDbRepository<Domain.Entities.Product, string>
{
    Task<List<string>> GetNonExistingProductIdsAsync(List<string> ids);
}