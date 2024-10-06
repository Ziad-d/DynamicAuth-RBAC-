using DynamicAuth_RBAC_.Models;
using DynamicAuth_RBAC_.Repositories;
using MediatR;

namespace DynamicAuth_RBAC_.CQRS.UserRoles.Queries
{
    public record GetRolesByUserIdQuery(int userId) : IRequest<IEnumerable<int>>;
    public class GetRolesByUserIdQueryHandler : IRequestHandler<GetRolesByUserIdQuery, IEnumerable<int>>
    {
        private readonly IRepository<UserRole> _repository;

        public GetRolesByUserIdQueryHandler(IRepository<UserRole> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<int>> Handle(GetRolesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userRoles = _repository.Get(ur => ur.UserId == request.userId);

            var roleIds = userRoles.Select(r => r.RoleId).ToList();

            return roleIds;
        }
    }
}
