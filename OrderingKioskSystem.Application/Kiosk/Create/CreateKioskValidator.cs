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
        }
    }
}
