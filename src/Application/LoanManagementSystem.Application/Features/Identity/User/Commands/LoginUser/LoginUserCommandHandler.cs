using LoanManagementSystem.Application.Common.Exceptions;
using LoanManagementSystem.Application.Common.Interfaces;
using LoanManagementSystem.Application.Common.Models;
using MediatR;

namespace LoanManagementSystem.Application.Features.Identity.User.Commands.LoginUser
{
    public class LoginUserCommandHandler(IIdentityService identityService) : IRequestHandler<LoginUserCommand, Result>
    {    
        public async Task<Result> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await identityService.GetUserAsync(request.Username);

            if (user == null) {
                throw new Ardalis.GuardClauses.NotFoundException("user","user is not found");
            }

            await identityService.PasswordSignInAsync(user, request.Password);

            var result = identityService.GeneratJwtToken(user);

            return Result<string>.Success(result);
        }
    }
}
