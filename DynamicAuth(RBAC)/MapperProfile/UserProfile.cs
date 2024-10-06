using AutoMapper;
using DynamicAuth_RBAC_.DTOs.UserDTOs;
using DynamicAuth_RBAC_.Models;

namespace DynamicAuth_RBAC_.MapperProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegisterDTO, User>().ReverseMap();

            CreateMap<User, UserDTO>();
                //.ForMember(dest => dest.RoleIds, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.RoleId).ToList()));

            CreateMap<User, UserReturnDTO>().ReverseMap();
        }
    }
}
