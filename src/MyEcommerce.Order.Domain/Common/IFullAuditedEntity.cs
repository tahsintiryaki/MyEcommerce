namespace MyEcommerce.Order.Domain.Common;

public interface IFullAuditedEntity : IHasCreation, IHasUpdate, ISoftDeletion, IHasActive
{
}