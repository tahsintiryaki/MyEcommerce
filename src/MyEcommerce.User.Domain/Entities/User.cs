using MyEcommerce.User.Domain.Common;

namespace MyEcommerce.User.Domain.Entities;

public class User : FullAuditedEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
}