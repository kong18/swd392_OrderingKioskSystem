using FluentValidation;

namespace OrderingKioskSystem.Application.Kiosk.Create
{
    public class CreateKioskValidator : AbstractValidator<CreateKioskCommand>
    {
        public CreateKioskValidator()
        {
            RuleFor(command => command.Location)
                .NotEmpty().WithMessage("Location is required.")
                .Matches("^Floor [1-7]$").WithMessage("Location must be 'Floor 1' to 'Floor 7'.");

            RuleFor(command => command.PIN)
                .NotEmpty().WithMessage("PIN is required.")
                .InclusiveBetween(100000, 999999).WithMessage("PIN must be a 6-digit number.");
        }
    }
}
