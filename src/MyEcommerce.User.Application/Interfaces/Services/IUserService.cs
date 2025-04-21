

using MyEcommerce.User.Application.Dtos.User;

namespace MyEcommerce.User.Application.Interfaces.Services;

public interface IUserService
{
    Task<GetUserDto> GetById(MyEcommerce.User.Domain.Entities.User user);
    Task CreateUser(CreateUserDto createUserDto);
    Task<List<GetUserDto>> GetAllUsers();
}