using MyEcommerce.User.Application.Interfaces.Repository;

namespace MyEcommerce.User.Application;

public class UserSeeder
{
    private readonly IUserRepository _userRepository;

    public UserSeeder(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task SeedData()
    {
        var allusers = await _userRepository.GetListAsync();
        if (allusers.Count == 0)
        {
            var user = new Domain.Entities.User()
            {
                Id = Guid.NewGuid(),
                Name = "Tahsin",
                Surname = "Tiryaki",
                IsActive = true,
                IsDeleted = false
            };
            await _userRepository.InsertAsync(user, true);
        }
    }
}