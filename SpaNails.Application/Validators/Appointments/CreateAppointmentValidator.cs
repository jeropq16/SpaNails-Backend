using System;
using FluentValidation;
using SpaNails.Application.DTOs.Appointments;

namespace SpaNails.Application.Validators.Appointments
{
    public class CreateAppointmentValidator : AbstractValidator<CreateAppointmentDto>
    {
        public CreateAppointmentValidator()
        {
            RuleFor(x => x.ClientId).NotEmpty();
            RuleFor(x => x.ManicuristId).NotEmpty();
            RuleFor(x => x.ServiceId).NotEmpty();
            RuleFor(x => x.ScheduledAt).GreaterThan(DateTime.UtcNow).WithMessage("Scheduled date must be in the future.");
        }
    }
}
