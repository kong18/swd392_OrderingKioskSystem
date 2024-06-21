using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Kiosk.Update
{
    public class UpdateKioskValidator : AbstractValidator<UpdateKioskCommand>
    {
        public UpdateKioskValidator() {
            RuleFor(cmd => cmd.Id)
                .NotEmpty().WithMessage("ID not null");

            RuleFor(command => command.Location)
                .Matches("^[a-zA-Z0-9]*$").WithMessage("Code must be alphanumeric.");

            RuleFor(command => command.Code)
                .MinimumLength(5).WithMessage("Code must be at least 5 characters long.")
                .Matches("^[a-zA-Z0-9]*$").WithMessage("Code must be alphanumeric.");

            RuleFor(command => command.PIN)
                .GreaterThan(0).WithMessage("PIN must be a numberic")
                .Must(pin => pin >= 100000 && pin <= 999999).WithMessage("PIN must be a 6-digit number.");
        }
    }
}
