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

            RuleFor(command => command.location)
                .NotEmpty().WithMessage("Location is required.");

        }
    }

}
