namespace MyEcommerce.Product.Persistence.Contexts;

public interface IMongoDbSettings
{
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
}