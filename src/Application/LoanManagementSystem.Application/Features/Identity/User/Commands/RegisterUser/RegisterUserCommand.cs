using LoanManagementSystem.Application.Common.Models;
using MediatR;

namespace LoanManagementSystem.Application.Features.Identity.User.Commands.RegisterUser
{
    public record RegisterUserCommand : IRequest<Result>
    {
        public string Firstname { get; set; } = default!;
        public string Lastname { get; set; } = default!;
        public string Mobile { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;
    }
}
