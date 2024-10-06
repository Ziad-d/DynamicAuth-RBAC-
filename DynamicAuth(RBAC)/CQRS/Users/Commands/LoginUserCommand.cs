using MediatR;
using DynamicAuth_RBAC_.Models;
using DynamicAuth_RBAC_.Repositories;
using DynamicAuth_RBAC_.Helpers;
using DynamicAuth_RBAC_.DTOs;
using DynamicAuth_RBAC_.DTOs.UserDTOs;

namespace DynamicAuth_RBAC_.CQRS.Users.Commands
{
    public record LoginUserCommand(UserLoginDTO UserLoginDTO) : IRequest<ResultDTO>;
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ResultDTO>
    {
        IRepository<User> _repository;
        public LoginUserCommandHandler(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<ResultDTO> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.First(c => c.EmailAddress == request.UserLoginDTO.EmailAddress);

            if (user is null || !BCrypt.Net.BCrypt.Verify(request.UserLoginDTO.Password, user.Password))
            {
                return ResultDTO.Failure("Invalid credentials");
            }

            var userDTO = user.MapOne<UserDTO>();
            var token = TokenGenerator.GenerateToken(userDTO);

            return ResultDTO.Success(token, "User is logged in!");
        }
    }
}
