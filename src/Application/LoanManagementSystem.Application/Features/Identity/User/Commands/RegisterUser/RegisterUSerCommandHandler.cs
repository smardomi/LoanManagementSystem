using LoanManagementSystem.Application.Common.Interfaces;
using LoanManagementSystem.Application.Common.Models;
using LoanManagementSystem.Application.Common.Extensions;
using MediatR;

namespace LoanManagementSystem.Application.Features.Identity.User.Commands.RegisterUser
{
    public class RegisterUSerCommandHandler : IRequestHandler<RegisterUserCommand, Result>
    {
        private readonly IIdentityService _identityService;

        public RegisterUSerCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new Domain.Entities.Identity.User
            {
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Email = $"{request.Mobile}@test.com",
                IsActice = false,
                UserName = request.Mobile,
                PhoneNumber = request.Mobile,
            };

            var result = await _identityService.CreateUserAsync(user, request.Password);

            return result.ToApplicationResult();
        }
    }
}
