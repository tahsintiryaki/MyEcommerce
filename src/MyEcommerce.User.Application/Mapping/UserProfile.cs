using AutoMapper;
using MyEcommerce.User.Application.Dtos.User;


namespace MyEcommerce.User.Application.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, MyEcommerce.User.Domain.Entities.User>().ReverseMap();
        CreateMap<GetUserDto, MyEcommerce.User.Domain.Entities.User>().ReverseMap();
    }
}