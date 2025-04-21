using AutoMapper;
using MyEcommerce.User.Application.Dtos.User;
using MyEcommerce.User.Application.Interfaces.Repository;
using MyEcommerce.User.Application.Interfaces.Services;

namespace MyEcommerce.User.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<GetUserDto> GetById(MyEcommerce.User.Domain.Entities.User user)
    {
        var selectedUser = await _userRepository.GetAsync(user.Id);

        return new GetUserDto()
        {
            Name = selectedUser.Name,
            Surname = selectedUser.Surname
        };
    }

    public async Task CreateUser(CreateUserDto createUserDto)
    {
        var createdUser = _mapper.Map<MyEcommerce.User.Domain.Entities.User>(createUserDto);
        await _userRepository.InsertAsync(createdUser, true);
    }

    public async Task<List<GetUserDto>> GetAllUsers()
    {
        var getAllUsers = await _userRepository.GetListAsync();
        return _mapper.Map<List<GetUserDto>>(getAllUsers);
    }
}