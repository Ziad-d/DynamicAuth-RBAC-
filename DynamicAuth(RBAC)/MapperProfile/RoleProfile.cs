using AutoMapper;
using DynamicAuth_RBAC_.DTOs.RoleDTOs;
using DynamicAuth_RBAC_.Models;

namespace DynamicAuth_RBAC_.MapperProfile
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleCreateDTO, Role>();
            CreateMap<Role, RoleDTO>().ReverseMap();
        }
    }
}
