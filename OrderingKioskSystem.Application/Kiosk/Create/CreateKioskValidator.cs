using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace OrderingKioskSystem.Application.Kiosk.Create
{
    public class CreateKioskValidator : AbstractValidator<CreateKioskCommand>
    {
        public CreateKioskValidator()
        {

            RuleFor(command => command.Location)
                .NotEmpty().WithMessage("Location is required.")
                .Matches("^[a-zA-Z0-9]*$").WithMessage("Location must be alphanumeric.");

            RuleFor(command => command.Code)
                .NotEmpty().WithMessage("Code is required.")
                .MinimumLength(5).WithMessage("Code must be at least 5 characters long.")
                .Matches("^[a-zA-Z0-9]*$").WithMessage("Code must be alphanumeric.");

            RuleFor(command => command.PIN)
                .NotEmpty().WithMessage("PIN is required")
                .Must(pin => pin >= 100000 && pin <= 999999).WithMessage("PIN must be a 6-digit number.");

        }
    }

}
