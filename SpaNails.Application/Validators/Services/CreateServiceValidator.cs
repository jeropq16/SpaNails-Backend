using FluentValidation;
using SpaNails.Application.DTOs.Services;

namespace SpaNails.Application.Validators.Services
{
    public class CreateServiceValidator : AbstractValidator<CreateServiceDto>
    {
        public CreateServiceValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.DurationInMinutes).GreaterThan(0);
        }
    }
}
