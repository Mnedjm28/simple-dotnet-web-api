using AutoMapper;
using SimpleDotNetWebApiApp.Application.Dtos.User;
using SimpleDotNetWebApiApp.Domain.Entities;

namespace SimpleDotNetWebApiApp.Shared.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ForMember(dest => dest.Role, opt => opt.MapFrom(src => (RoleEnum)src.RoleId)); ;
            CreateMap<UserDto, User>().ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => (int)src.Role))
                                      .ForMember(dest => dest.Role, opt => opt.Ignore());

            CreateMap<User, LoginDto>();
            CreateMap<LoginDto, User>();
        }
    }
}
