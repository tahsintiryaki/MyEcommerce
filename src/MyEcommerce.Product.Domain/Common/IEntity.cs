namespace MyEcommerce.Product.Domain.Common;

public interface IEntity<TKey>
{
    public TKey Id { get; set; }
}