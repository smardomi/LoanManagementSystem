using FluentValidation;

namespace LoanManagementSystem.Application.Features.Identity.User.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {

        public RegisterUserCommandValidator()
        {
            RuleFor(v => v.Firstname)
           .NotEmpty()
           .MaximumLength(200);

           RuleFor(v => v.Lastname)
          .NotEmpty()
          .MaximumLength(200);

           RuleFor(x => x.ConfirmPassword)
          .Equal(x => x.Password)
            .WithMessage("Password and Confirmed Password must match.");

        }
    }
}
