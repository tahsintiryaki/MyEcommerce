namespace MyEcommerce.User.Domain.Common;

public interface IFullAuditedEntity : IHasCreation, IHasUpdate, ISoftDeletion, IHasActive
{
}