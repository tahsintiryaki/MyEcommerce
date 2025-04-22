using MyEcommerce.Product.Application.Interfaces.Repositories;
using MongoDB.Driver;

namespace MyEcommerce.Product.Persistence.Repositories;

public class ProductRepository : MongoRepository<Domain.Entities.Product, string>, IProductRepository
{
    public ProductRepository(IMongoDatabase database) : base(database)
    {
    }
}