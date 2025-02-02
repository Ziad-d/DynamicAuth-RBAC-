﻿using DynamicAuth_RBAC_.CQRS.Users.Queries;
using DynamicAuth_RBAC_.DTOs;
using DynamicAuth_RBAC_.DTOs.UserRoleDTOs;
using DynamicAuth_RBAC_.Models;
using DynamicAuth_RBAC_.Repositories;
using MediatR;

namespace DynamicAuth_RBAC_.CQRS.UserRoles.Orchestrators
{
    public record AssignRolesToUserOrchestrator(RolesToUserDTO RolesToUserDTO) : IRequest<ResultDTO>;

    public class AssignRolesToUserOrchestratorHandler : IRequestHandler<AssignRolesToUserOrchestrator, ResultDTO>
    {
        private readonly IRepository<UserRole> _repository;
        private readonly IMediator _mediator;

        public AssignRolesToUserOrchestratorHandler(IRepository<UserRole> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<ResultDTO> Handle(AssignRolesToUserOrchestrator request, CancellationToken cancellationToken)
        {
            if (request == null || request.RolesToUserDTO == null || !request.RolesToUserDTO.RoleIds.Any())
            {
                return ResultDTO.Failure("Invalid inputs!");
            }

            var user = await _mediator.Send(new GetUserByIdQuery(request.RolesToUserDTO.UserId));
            //var role = await _repository.GetByIdAsync(request.addFeaturesToRuleDTO.RoleId);
            if (user == null)
            {
                return ResultDTO.Failure("User is not found!");
            }

            foreach (var roleId in request.RolesToUserDTO.RoleIds)
            {
                var existingUserRole = await _repository.First(
                    ur => ur.UserId == request.RolesToUserDTO.UserId && ur.RoleId == roleId
                );

                if (existingUserRole == null)
                {
                    var userRole = new UserRole
                    {
                        UserId = request.RolesToUserDTO.UserId,
                        RoleId = roleId
                    };

                    await _repository.AddAsync(userRole);
                }
            }

            await _repository.SaveChangesAsync();

            return ResultDTO.Success("Roles assigned to user successfully!");
        }
    }
}
