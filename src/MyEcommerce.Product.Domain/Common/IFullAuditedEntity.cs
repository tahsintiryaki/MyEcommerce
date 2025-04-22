using MyEcommerce.Product.Domain.Common;

namespace MyEcommerce.Product.Domain.Common;

public interface IFullAuditedEntity<TKey> : IHasCreation, IHasUpdate, ISoftDeletion, IHasActive
{
}