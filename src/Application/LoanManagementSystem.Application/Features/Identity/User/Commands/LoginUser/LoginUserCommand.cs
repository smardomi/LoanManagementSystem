using LoanManagementSystem.Application.Common.Models;
using MediatR;

namespace LoanManagementSystem.Application.Features.Identity.User.Commands.LoginUser
{
    public record LoginUserCommand : IRequest<Result>
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
