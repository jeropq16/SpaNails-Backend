using FluentValidation;
using SpaNails.Application.DTOs.Users;

namespace SpaNails.Application.Validators.Users
{
    public class CreateUserValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(150);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.Role).NotEmpty().Must(role => role == "Admin" || role == "Manicurist" || role == "Client")
                .WithMessage("Role must be Admin, Manicurist, or Client.");
        }
    }
}
