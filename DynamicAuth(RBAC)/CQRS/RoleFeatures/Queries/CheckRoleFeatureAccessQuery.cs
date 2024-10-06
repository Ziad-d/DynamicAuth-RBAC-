using DynamicAuth_RBAC_.Enums;
using DynamicAuth_RBAC_.Models;
using DynamicAuth_RBAC_.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DynamicAuth_RBAC_.CQRS.RoleFeatures.Queries
{
    public record CheckRoleFeatureAccessQuery(int roleID, Feature feature) : IRequest<bool>;
    public class CheckRoleFeatureAccessQueryHandler : IRequestHandler<CheckRoleFeatureAccessQuery, bool>
    {
        private readonly IRepository<RoleFeature> _repository;

        public CheckRoleFeatureAccessQueryHandler(IRepository<RoleFeature> repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(CheckRoleFeatureAccessQuery request, CancellationToken cancellationToken)
        {
            var hasAccess = await _repository.Get(r => !r.Deleted &&
                        r.RoleID == request.roleID &&
                        r.Feature == request.feature)
                        .AnyAsync();

            return hasAccess;
        }
    }
}
