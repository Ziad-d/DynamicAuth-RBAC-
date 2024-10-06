using DynamicAuth_RBAC_.Attributes;
using DynamicAuth_RBAC_.CQRS.RoleFeatures.Orchestrators;
using DynamicAuth_RBAC_.CQRS.Roles.Commands;
using DynamicAuth_RBAC_.CQRS.Roles.Queries;
using DynamicAuth_RBAC_.CQRS.UserRoles.Orchestrators;
using DynamicAuth_RBAC_.DTOs;
using DynamicAuth_RBAC_.DTOs.RoleDTOs;
using DynamicAuth_RBAC_.DTOs.RoleFeatureDTOs;
using DynamicAuth_RBAC_.DTOs.UserRoleDTOs;
using DynamicAuth_RBAC_.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StayCation.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        [TypeFilter(typeof(CustomizedAuthorize), Arguments = new object[] { Feature.CreateRole })]
        public async Task<ResultDTO> CreateRole(RoleCreateDTO roleCreateDTO)
        {
            var command = new CreateRoleCommand(roleCreateDTO);
            var result = await _mediator.Send(command);
            return ResultDTO.Success(result.Data);
        }

        [HttpGet("{id}")]
        [Authorize]
        [TypeFilter(typeof(CustomizedAuthorize), Arguments = new object[] { Feature.GetSingleRole })]
        public async Task<ResultDTO> GetSingleRole(int id)
        {
            var role = await _mediator.Send(new GetRoleByIdQuery(id));
            return ResultDTO.Success(role.Data);
        }

        [HttpPost]
        [Authorize]
        [TypeFilter(typeof(CustomizedAuthorize), Arguments = new object[] { Feature.AssignFeaturesToRole })]
        public async Task<ResultDTO> AssignFeaturesToRole(FeaturesToRoleDTO featuresToRoleDTO)
        {
            var result = await _mediator.Send(new AssignFeaturesToRoleOrchestrator(featuresToRoleDTO));
            return ResultDTO.Success(result.Data);
        }

        [HttpPost]
        [Authorize]
        [TypeFilter(typeof(CustomizedAuthorize), Arguments = new object[] { Feature.AssignRolesToUser })]
        public async Task<ResultDTO> AssignRolesToUser(RolesToUserDTO rolesToUserDTO)
        {
            var result = await _mediator.Send(new AssignRolesToUserOrchestrator(rolesToUserDTO));

            return ResultDTO.Success(result.Data);
        }
    }
}
