using DynamicAuth_RBAC_.DTOs;
using DynamicAuth_RBAC_.DTOs.RoleDTOs;
using DynamicAuth_RBAC_.Helpers;
using DynamicAuth_RBAC_.Models;
using DynamicAuth_RBAC_.Repositories;
using MediatR;

namespace DynamicAuth_RBAC_.CQRS.Roles.Queries
{
    public record GetRoleByIdQuery(int id) : IRequest<ResultDTO>;
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, ResultDTO>
    {
        private readonly IRepository<Role> _repository;

        public GetRoleByIdQueryHandler(IRepository<Role> repository)
        {
            _repository = repository;
        }
        public async Task<ResultDTO> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return ResultDTO.Failure("Invalid RoleID!");
            }

            var role = await _repository.GetByIDAsync(request.id);
            var mappedRole = role.MapOne<RoleDTO>();
                
            return ResultDTO.Success(mappedRole, "Role has been retrieved successfully!");
        }
    }
}
