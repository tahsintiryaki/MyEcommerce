

using MyEcommerce.User.Application.Interfaces.Repository;
using MyEcommerce.User.Persistence.Contexts;

namespace MyEcommerce.User.Persistence.Repositories;

public class UserRepository : GeneralRepository<Domain.Entities.User>,IUserRepository
{
    public UserRepository(UserDbContext dbContext) : base(dbContext)
    {
    }
}