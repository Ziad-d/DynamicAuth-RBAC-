﻿using DynamicAuth_RBAC_.CQRS.Roles.Queries;
using DynamicAuth_RBAC_.DTOs;
using DynamicAuth_RBAC_.DTOs.RoleFeatureDTOs;
using DynamicAuth_RBAC_.Models;
using DynamicAuth_RBAC_.Repositories;
using MediatR;

namespace DynamicAuth_RBAC_.CQRS.RoleFeatures.Orchestrators
{
    public record AssignFeaturesToRoleOrchestrator(FeaturesToRoleDTO FeaturesToRoleDTO) : IRequest<ResultDTO>;

    public class AssignFeaturesToRoleOrchestratorHandler : IRequestHandler<AssignFeaturesToRoleOrchestrator, ResultDTO>
    {
        private readonly IRepository<RoleFeature> _repository;
        private readonly IMediator _mediator;

        public AssignFeaturesToRoleOrchestratorHandler(IRepository<RoleFeature> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<ResultDTO> Handle(AssignFeaturesToRoleOrchestrator request, CancellationToken cancellationToken)
        {
            if (request == null || request.FeaturesToRoleDTO == null || !request.FeaturesToRoleDTO.Features.Any())
            {
                return ResultDTO.Failure("Invalid inputs!");
            }

            var role = await _mediator.Send(new GetRoleByIdQuery(request.FeaturesToRoleDTO.RoleId));
            //var role = await _repository.GetByIdAsync(request.addFeaturesToRuleDTO.RoleId);
            if (role == null)
            {
                return ResultDTO.Failure("Invalid RoleID!");
            }

            foreach (var feature in request.FeaturesToRoleDTO.Features)
            {
                var existingRoleFeature = await _repository.First(
                    rf => rf.RoleID == request.FeaturesToRoleDTO.RoleId && rf.Feature == feature
                );

                if (existingRoleFeature == null)
                {
                    var roleFeature = new RoleFeature
                    {
                        RoleID = request.FeaturesToRoleDTO.RoleId,
                        Feature = feature
                    };

                    await _repository.AddAsync(roleFeature);
                }
            }

            await _repository.SaveChangesAsync();

            return ResultDTO.Success("Features assigned to role successfully!");
        }
    }
}
